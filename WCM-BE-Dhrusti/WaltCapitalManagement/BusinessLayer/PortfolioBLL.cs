using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace BusinessLayer
{
    public class PortfolioBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _configaration;
        private readonly CommonHelper _commonHelper;
        public PortfolioBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, IConfiguration configaration, CommonHelper commonHelper)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _configaration = configaration;
            _commonHelper = commonHelper;
        }

        public CommonResponse GetPortfolioData(PortFolioDataReqDTO portFolioDataReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                int ClientAccessCategoryId = 2; //Clients = 2
                PortFolioDataResDTO portFolioDataResDTO = new PortFolioDataResDTO();
                portFolioDataResDTO.portfolioClientDetail = new PortfolioClientDetail();
                portFolioDataResDTO.PortfolioServiceProviders = new List<PortfolioServiceProvider>();
                portFolioDataResDTO.portfolioClientInvestmentDetail = new PortfolioClientInvestmentDetail();

                var ClientDetail = _commonRepo.getUserList().FirstOrDefault(x => x.AccessCategoryId == ClientAccessCategoryId && x.Id == portFolioDataReqDTO.ClientId);
                if (ClientDetail != null)
                {
                    // Set Client Details
                    portFolioDataResDTO.portfolioClientDetail.ClientId = ClientDetail.Id;
                    portFolioDataResDTO.portfolioClientDetail.PhotoPath = !string.IsNullOrWhiteSpace(ClientDetail.ProfilePhoto) ? ClientDetail.ProfilePhoto : "";
                    portFolioDataResDTO.portfolioClientDetail.ClientName = ClientDetail.FirstName + " " + ClientDetail.LastName;
                    portFolioDataResDTO.portfolioClientDetail.AccountNo = !string.IsNullOrWhiteSpace(ClientDetail.ClientAccNo) ? ClientDetail.ClientAccNo : "";
                    portFolioDataResDTO.portfolioClientDetail.BirthDate = ClientDetail.Dob.ToString("dd MMMM yyyy");
                    portFolioDataResDTO.portfolioClientDetail.JoiningDate = ClientDetail.CreatedDate.ToString("dddd, dd MMMM yyyy");
                    portFolioDataResDTO.portfolioClientDetail.Mobile = !string.IsNullOrWhiteSpace(ClientDetail.MobileNo) ? ClientDetail.MobileNo : "";
                    portFolioDataResDTO.portfolioClientDetail.Phone = !string.IsNullOrWhiteSpace(ClientDetail.WorkNo) ? ClientDetail.WorkNo : "";
                    portFolioDataResDTO.portfolioClientDetail.Email = !string.IsNullOrWhiteSpace(ClientDetail.Email) ? ClientDetail.Email : "";

                    var ServiceProviderList = _commonRepo.serviceProviderList();
                    var ServiceProviderTypeList = _commonRepo.serviceProviderTypeList();
                    var ExternalAccountList = _commonRepo.externalAccountList();
                    var ClientList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == ClientAccessCategoryId).ToList();

                    var csvLogDetail = _commonRepo.getPPMCSVLogList().OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    var csvData = _commonRepo.getPPMCSVDataList();
                    if (csvLogDetail != null)
                    {
                        csvData = csvData.Where(x => !string.IsNullOrWhiteSpace(x.AccountNo) && x.CsvfileId == csvLogDetail.Id);
                    }
                    var ServiceProviderIdList = ExternalAccountList.Select(x => new { x.ServiceProvider, x.Type }).Distinct().ToList();
                    double TotalClientInvestment = 0;
                    // Set Service Provider List Detail with its Type
                    foreach (var item in ServiceProviderIdList)
                    {
                        var ServiceProviderDetail = ServiceProviderList.FirstOrDefault(x => x.Id == item.ServiceProvider);
                        var ServiceProviderTypeDetail = ServiceProviderTypeList.FirstOrDefault(x => x.Id == item.Type);
                        var ExternalAccountDetailList = ExternalAccountList.Where(x => x.ServiceProvider == item.ServiceProvider && x.Type == item.Type).ToList();
                        var ClientIdList = ExternalAccountList.Where(x => x.ServiceProvider == item.ServiceProvider && x.Type == item.Type).Select(x => x.ClientId).Distinct().ToList();
                        var UserClientId = ClientList.Select(x => x.Id).ToList();
                        int ClientCount = ClientList.Where(x => ClientIdList.Contains(x.Id)).Count();

                        PortfolioServiceProvider portfolioServiceProvider = new PortfolioServiceProvider();
                        portfolioServiceProvider.ServiceProviderId = item.ServiceProvider;
                        portfolioServiceProvider.ServiceProviderName = ServiceProviderDetail != null ? ServiceProviderDetail.ServiceProvider : "";
                        portfolioServiceProvider.ServiceProviderTypeId = item.Type;
                        portfolioServiceProvider.ServiceProviderTypeName = ServiceProviderTypeDetail != null ? ServiceProviderTypeDetail.ServiceProviderType : "";
                        portfolioServiceProvider.CurrencyShortName = "ZAR";

                        var AccountNoList = ExternalAccountDetailList.Where(x => !string.IsNullOrWhiteSpace(x.AccountCode) && UserClientId.Contains(x.ClientId)).Select(x => x.AccountCode).ToList();
                        double? SumTotalHoldings = null;
                        if (AccountNoList.Count > 0)
                        {
                            SumTotalHoldings = csvData.Where(x => x.Category == ServiceProviderCategoryConstant.Total_Holdings && AccountNoList.Contains(x.AccountNo)).Select(x => x.Value).Sum();
                        }
                        portfolioServiceProvider.TotalAmount = SumTotalHoldings != null ? SumTotalHoldings.Value : 0;
                        portfolioServiceProvider.InvestedPercentage = 0;
                        portfolioServiceProvider.ClientCount = ClientCount;
                        portfolioServiceProvider.TotalAmountString = _commonHelper.GetFormatedDouble(portfolioServiceProvider.TotalAmount);
                        TotalClientInvestment = TotalClientInvestment + portfolioServiceProvider.TotalAmount;

                        portFolioDataResDTO.PortfolioServiceProviders.Add(portfolioServiceProvider);
                    }

                    foreach (var item in portFolioDataResDTO.PortfolioServiceProviders)
                    {
                        if (item.TotalAmount > 0 && TotalClientInvestment > 0)
                        {
                            item.InvestedPercentage = Math.Round((item.TotalAmount * 100) / TotalClientInvestment, 2);
                        }
                        item.InvestedPercentageString = _commonHelper.GetFormatedDouble(item.InvestedPercentage);
                    }

                    // Call third party API to get Currency Coversion and set variables
                    double PriceInZar = TotalClientInvestment;

                    double PriceInUsd = _commonHelper.GetConvertedCurrency(CurrencySymbolConstant.South_African_Rand, CurrencySymbolConstant.United_States_Dollar, TotalClientInvestment, null).Data;
                    double GoldInOz = _commonHelper.GetConvertedCurrency(CurrencySymbolConstant.South_African_Rand, CurrencySymbolConstant.Gold_troy_ounce, TotalClientInvestment, null).Data;
                    // Gold_troy_ounce to grams : Formula - for an approximate result, multiply the mass value by 28.35
                    double PriceInGold = GoldInOz * 28.35;
                    double PriceInBitcoin = _commonHelper.GetConvertedCurrency(CurrencySymbolConstant.South_African_Rand, CurrencySymbolConstant.Bitcoin, TotalClientInvestment, null).Data;

                    // Set Client Investment Details
                    portFolioDataResDTO.portfolioClientInvestmentDetail.PriceInZar = "R " + _commonHelper.GetFormatedDouble(PriceInZar);
                    portFolioDataResDTO.portfolioClientInvestmentDetail.PriceInUsd = "USD " + _commonHelper.GetFormatedDouble(PriceInUsd);
                    portFolioDataResDTO.portfolioClientInvestmentDetail.PriceInGold = _commonHelper.GetFormatedDouble(PriceInGold) + " grams " + "(" + Convert.ToString(GoldInOz) + " Oz)";
                    portFolioDataResDTO.portfolioClientInvestmentDetail.PriceInBitcoin = "BTC " + _commonHelper.GetFormatedDouble(PriceInBitcoin);

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = portFolioDataResDTO;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.Data = portFolioDataResDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetPortfolioCSVData(GetPortfolioCSVDataReqDTO getPortfolioCSVDataReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                int ClientAccessCategoryId = 2; //Clients = 2
                int Page = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:Page").Value);
                int PageSize = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:PageSize").Value);
                bool? OrderBy = Convert.ToBoolean(_configaration.GetSection("ByDefaultPagination:OrderBy").Value);

                Page = getPortfolioCSVDataReqDTO.PageNumber == 0 ? Page : getPortfolioCSVDataReqDTO.PageNumber;
                PageSize = getPortfolioCSVDataReqDTO.PageSize == 0 ? PageSize : getPortfolioCSVDataReqDTO.PageSize;
                if (getPortfolioCSVDataReqDTO.Orderby != null)
                    OrderBy = getPortfolioCSVDataReqDTO.Orderby.Value;

                GetPortfolioCSVDataResDTO getPortfolioCSVDataResDTO = new GetPortfolioCSVDataResDTO();
                getPortfolioCSVDataResDTO.GetPortfolioCSVDataList = new List<GetPortfolioCSVData>();
                getPortfolioCSVDataResDTO.getPortfolioCSVDataTotal = new GetPortfolioCSVDataTotal();

                if (getPortfolioCSVDataReqDTO.ServiceProviderId == 3) // Service Provider = PPM = 3
                {
                    var csvLog = _commonRepo.getPPMCSVLogList();
                    var csvData = _commonRepo.getPPMCSVDataList().Where(x => !string.IsNullOrWhiteSpace(x.AccountNo));
                    var ExternalAccountList = _commonRepo.externalAccountList().Where(x => x.ServiceProvider == getPortfolioCSVDataReqDTO.ServiceProviderId && x.Type == getPortfolioCSVDataReqDTO.ServiceProviderTypeId).ToList();

                    var ClientIdList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == ClientAccessCategoryId).Select(x => x.Id).ToList();
                    ExternalAccountList = ExternalAccountList.Where(x => ClientIdList.Contains(x.ClientId)).ToList();

                    int currentCSVId = 0;
                    int previousCSVId = 0;
                    var currentCSV = csvLog.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    if (currentCSV != null)
                    {
                        currentCSVId = currentCSV.Id;
                        var previousCSV = csvLog.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id != currentCSVId);
                        if (previousCSV != null)
                        {
                            previousCSVId = previousCSV.Id;
                        }
                        else
                        {
                            previousCSVId = currentCSVId;
                        }
                    }

                    var AccountNoList = ExternalAccountList.Where(x => !string.IsNullOrWhiteSpace(x.AccountCode)).Select(x => x.AccountCode).ToList();
                    var CurrentCSVData = csvData.Where(x => x.CsvfileId == currentCSVId && x.Category == ServiceProviderCategoryConstant.Holding && AccountNoList.Contains(x.AccountNo) && !string.IsNullOrWhiteSpace(x.Share)).ToList();
                    var PreviousCSVData = csvData.Where(x => x.CsvfileId == previousCSVId && x.Category == ServiceProviderCategoryConstant.Holding && AccountNoList.Contains(x.AccountNo) && !string.IsNullOrWhiteSpace(x.Share)).ToList();

                    var SumCashCurrentCSV = csvData.Where(x => x.CsvfileId == currentCSVId && x.Category == ServiceProviderCategoryConstant.Cash && AccountNoList.Contains(x.AccountNo)).Select(x => x.Value).Sum();
                    var SumTotalHoldingsCurrentCSV = csvData.Where(x => x.CsvfileId == currentCSVId && x.Category == ServiceProviderCategoryConstant.Total_Holdings && AccountNoList.Contains(x.AccountNo)).Select(x => x.Value).Sum();
                    var SumTotalValueCurrentCSV = csvData.Where(x => x.CsvfileId == currentCSVId && x.Category == ServiceProviderCategoryConstant.Total_Value && AccountNoList.Contains(x.AccountNo)).Select(x => x.Value).Sum();
                    double TotalPortfolioPercentage = 0;
                    double TotalCashPortfolioPercentage = 0;
                    double TotalValueOpen = 0;
                    double TotalValueNow = 0;
                    TotalCashPortfolioPercentage = SumCashCurrentCSV.Value * 100 / SumTotalValueCurrentCSV.Value;
                    if (CurrentCSVData.Count > 0 && PreviousCSVData.Count > 0)
                    {
                        foreach (var item in CurrentCSVData)
                        {
                            var PreviousDataDetail = PreviousCSVData.FirstOrDefault(x => x.Share == item.Share && x.AccountNo == item.AccountNo && x.InvDate.Value.Date <= item.InvDate.Value.Date);
                            if (PreviousDataDetail != null)
                            {
                                GetPortfolioCSVData portfolioCSVData = new GetPortfolioCSVData();

                                var PercentageChanges = ((PreviousDataDetail.Price - item.Price) / PreviousDataDetail.Price) * 100;
                                var PercentagePortfolio = (item.Value * 100) / SumTotalHoldingsCurrentCSV;

                                portfolioCSVData.Name = !String.IsNullOrWhiteSpace(item.Share) ? item.Share : "";
                                portfolioCSVData.Code = !String.IsNullOrWhiteSpace(item.Share) ? item.Share.Substring(0, 6) : "";
                                portfolioCSVData.Volume = item.Quantity != null ? Convert.ToString(item.Quantity.Value) : "0";
                                portfolioCSVData.CostPrice = "R" + " " + Convert.ToString(PreviousDataDetail.Price);
                                portfolioCSVData.CurrentPrice = "R" + " " + Convert.ToString(item.Price);
                                portfolioCSVData.PerChange = Convert.ToString(Math.Round(PercentageChanges.Value, 2)) + "%";
                                portfolioCSVData.CrrentValue = "R" + " " + Convert.ToString(item.Value);
                                portfolioCSVData.PerPortfolio = Convert.ToString(Math.Round(PercentagePortfolio.Value, 2)) + "%";

                                TotalPortfolioPercentage = TotalPortfolioPercentage + PercentagePortfolio.Value;
                                TotalValueOpen = TotalValueOpen + (PreviousDataDetail.Price.Value * item.Quantity.Value);
                                TotalValueNow = TotalValueNow + item.Value.Value;

                                getPortfolioCSVDataResDTO.GetPortfolioCSVDataList.Add(portfolioCSVData);
                            }
                        }

                        getPortfolioCSVDataResDTO.TotalCSVCount = getPortfolioCSVDataResDTO.GetPortfolioCSVDataList.Count;
                        if (getPortfolioCSVDataResDTO.TotalCSVCount > 0)
                        {
                            getPortfolioCSVDataResDTO.GetPortfolioCSVDataList = getPortfolioCSVDataResDTO.GetPortfolioCSVDataList.Count <= PageSize ? getPortfolioCSVDataResDTO.GetPortfolioCSVDataList : getPortfolioCSVDataResDTO.GetPortfolioCSVDataList.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

                            getPortfolioCSVDataResDTO.GetPortfolioCSVDataList = OrderBy != null && OrderBy.Value ? getPortfolioCSVDataResDTO.GetPortfolioCSVDataList.OrderBy(x => x.Name).ToList() : getPortfolioCSVDataResDTO.GetPortfolioCSVDataList.OrderByDescending(x => x.Name).ToList();

                            getPortfolioCSVDataResDTO.getPortfolioCSVDataTotal.TotalCashCurrentValue = "R " + Convert.ToString(Math.Round(SumCashCurrentCSV.Value, 2));
                            getPortfolioCSVDataResDTO.getPortfolioCSVDataTotal.TotalCashPortfolioPercentage = Convert.ToString(Math.Round(TotalCashPortfolioPercentage, 2)) + "%";
                            getPortfolioCSVDataResDTO.getPortfolioCSVDataTotal.TotalValueOpen = Convert.ToString(Math.Round(TotalValueOpen, 2));
                            getPortfolioCSVDataResDTO.getPortfolioCSVDataTotal.TotalValueNow = "R " + Convert.ToString(Math.Round(TotalValueNow, 2));
                            getPortfolioCSVDataResDTO.getPortfolioCSVDataTotal.TotalPortfolioPercentage = Convert.ToString(Math.Round(TotalPortfolioPercentage, 2)) + "%";

                            getPortfolioCSVDataResDTO.getPortfolioCSVDataTotal.TotalCashCurrentValueString = "Cash";
                            getPortfolioCSVDataResDTO.getPortfolioCSVDataTotal.TotalValueOpenString = "Total Portfolio Value Open";
                            getPortfolioCSVDataResDTO.getPortfolioCSVDataTotal.TotalValueNowString = "Total Portfolio value Now";

                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Success.";
                            commonResponse.Data = getPortfolioCSVDataResDTO;
                        }
                        else
                        {
                            commonResponse.StatusCode = HttpStatusCode.NotFound;
                            commonResponse.Message = "Data Not Found.";
                            commonResponse.Data = getPortfolioCSVDataResDTO;
                        }
                    }
                    else
                    {
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data Not Found.";
                        commonResponse.Data = getPortfolioCSVDataResDTO;
                    }
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.Data = getPortfolioCSVDataResDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetPortfolioClientData(GetPortfolioClientDataReqDTO getPortfolioClientDataReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                int ClientAccessCategoryId = 2; //Clients = 2
                int Page = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:Page").Value);
                int PageSize = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:PageSize").Value);
                bool? OrderBy = Convert.ToBoolean(_configaration.GetSection("ByDefaultPagination:OrderBy").Value);

                Page = getPortfolioClientDataReqDTO.PageNumber == 0 ? Page : getPortfolioClientDataReqDTO.PageNumber;
                PageSize = getPortfolioClientDataReqDTO.PageSize == 0 ? PageSize : getPortfolioClientDataReqDTO.PageSize;
                if (getPortfolioClientDataReqDTO.Orderby != null)
                    OrderBy = getPortfolioClientDataReqDTO.Orderby.Value;

                GetPortfolioClientDataResDTO getPortfolioClientData = new GetPortfolioClientDataResDTO();
                getPortfolioClientData.getPortfolioClientDataList = new List<GetPortfolioClientData>();

                var ClientList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == ClientAccessCategoryId).ToList();
                var ExternalAccountList = _commonRepo.externalAccountList();

                bool HasSearchStockData = false;
                var csvLogDetail = _commonRepo.getPPMCSVLogList().OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                var csvDataList = _commonRepo.getPPMCSVDataList();
                if (csvLogDetail != null)
                {
                    csvDataList = csvDataList.Where(x => !string.IsNullOrWhiteSpace(x.Share) && x.CsvfileId == csvLogDetail.Id && x.Category == ServiceProviderCategoryConstant.Holding).AsQueryable();

                    if (!string.IsNullOrWhiteSpace(getPortfolioClientDataReqDTO.SearchStockString) && getPortfolioClientDataReqDTO.ServiceProviderId == 3) // Service Provider = PPM = 3
                    {
                        csvDataList = csvDataList.Where(x => x.Share.ToLower().Contains(getPortfolioClientDataReqDTO.SearchStockString.ToLower()));
                        HasSearchStockData = true;
                    }
                }
                var CSVAccountNoList = csvDataList.Select(x => x.AccountNo).Distinct().ToList();

                var ClientIdList = ExternalAccountList.Where(x => x.ServiceProvider == getPortfolioClientDataReqDTO.ServiceProviderId && x.Type == getPortfolioClientDataReqDTO.ServiceProviderTypeId && (CSVAccountNoList.Contains(x.AccountCode) || !HasSearchStockData)).Select(x => x.ClientId).Distinct().ToList();

                if (ClientIdList.Count > 0)
                {
                    var ClientDetailList = ClientList.Where(x => ClientIdList.Contains(x.Id)).ToList();
                    foreach (var item in ClientDetailList)
                    {
                        var StockDetail = csvDataList.FirstOrDefault(x => x.AccountNo == item.AccountNo);

                        GetPortfolioClientData getPortfolioClient = new GetPortfolioClientData();
                        getPortfolioClient.AccountNo = !String.IsNullOrWhiteSpace(item.AccountNo) ? item.AccountNo : "";
                        getPortfolioClient.Name = item.FirstName + " " + item.LastName;
                        getPortfolioClient.Email = item.Email;
                        getPortfolioClient.PhoneNo = item.MobileNo;
                        getPortfolioClient.BirthDate = item.Dob.ToString("MMMM dd, yyyy");
                        getPortfolioClient.JoiningDate = item.CreatedDate.ToString("dd/MM/yyyy");
                        getPortfolioClient.InvestmentValue = "R " + Convert.ToString(0);
                        getPortfolioClient.StockName = StockDetail != null && !string.IsNullOrWhiteSpace(StockDetail.Share) ? StockDetail.Share : "";

                        getPortfolioClientData.getPortfolioClientDataList.Add(getPortfolioClient);
                    }
                }

                if (getPortfolioClientData.getPortfolioClientDataList.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(getPortfolioClientDataReqDTO.Alphabet))
                    {
                        getPortfolioClientData.getPortfolioClientDataList = getPortfolioClientData.getPortfolioClientDataList.Where(x => x.Name.ToLower().StartsWith(getPortfolioClientDataReqDTO.Alphabet.ToLower())).ToList();
                    }

                    if (!string.IsNullOrWhiteSpace(getPortfolioClientDataReqDTO.SearchClientString))
                    {
                        getPortfolioClientData.getPortfolioClientDataList = getPortfolioClientData.getPortfolioClientDataList.Where(x => x.Name.ToLower().Contains(getPortfolioClientDataReqDTO.SearchClientString.ToLower())).ToList();
                    }

                    //if (!string.IsNullOrWhiteSpace(getPortfolioClientDataReqDTO.SearchStockString))
                    //{
                    //    getPortfolioClientData.getPortfolioClientDataList = getPortfolioClientData.getPortfolioClientDataList.Where(x => x.Name.Contains(getPortfolioClientDataReqDTO.SearchStockString)).ToList();
                    //}

                    getPortfolioClientData.TotalCount = getPortfolioClientData.getPortfolioClientDataList.Count();

                    getPortfolioClientData.getPortfolioClientDataList = getPortfolioClientData.getPortfolioClientDataList.Count <= PageSize ? getPortfolioClientData.getPortfolioClientDataList : getPortfolioClientData.getPortfolioClientDataList.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

                    getPortfolioClientData.getPortfolioClientDataList = OrderBy != null && OrderBy.Value ? getPortfolioClientData.getPortfolioClientDataList.OrderBy(x => x.Name).ToList() : getPortfolioClientData.getPortfolioClientDataList.OrderByDescending(x => x.Name).ToList();

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getPortfolioClientData;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.Data = getPortfolioClientData;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetPortfolioClientList(GetPortfolioClientListReqDTO getPortfolioClientListReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                int ClientAccessCategoryId = 2; //Clients = 2
                int Page = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:Page").Value);
                int PageSize = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:PageSize").Value);
                bool? OrderBy = Convert.ToBoolean(_configaration.GetSection("ByDefaultPagination:OrderBy").Value);

                Page = getPortfolioClientListReqDTO.PageNumber == 0 ? Page : getPortfolioClientListReqDTO.PageNumber;
                PageSize = getPortfolioClientListReqDTO.PageSize == 0 ? PageSize : getPortfolioClientListReqDTO.PageSize;
                if (getPortfolioClientListReqDTO.Orderby != null)
                    OrderBy = getPortfolioClientListReqDTO.Orderby.Value;

                GetPortfolioClientListResDTO getPortfolioClientListResDTOs = new GetPortfolioClientListResDTO();
                getPortfolioClientListResDTOs.getPortfolioClientLists = new List<GetPortfolioClientList>();

                var ClientList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == ClientAccessCategoryId);
                if (getPortfolioClientListReqDTO.PorfolioUserId > 0)
                {

                }

                if (ClientList.Count() > 0)
                {
                    if (!string.IsNullOrWhiteSpace(getPortfolioClientListReqDTO.Alphabet))
                    {
                        ClientList = ClientList.Where(x => x.LastName.ToLower().StartsWith(getPortfolioClientListReqDTO.Alphabet.ToLower()));
                    }

                    if (!string.IsNullOrWhiteSpace(getPortfolioClientListReqDTO.SearchString))
                    {
                        ClientList = ClientList.Where(x => x.LastName.ToLower().Contains(getPortfolioClientListReqDTO.SearchString.ToLower()) || x.FirstName.ToLower().Contains(getPortfolioClientListReqDTO.SearchString.ToLower()) || (!string.IsNullOrWhiteSpace(x.MiddleName) && x.MiddleName.ToLower().Contains(getPortfolioClientListReqDTO.SearchString.ToLower())));
                    }

                    getPortfolioClientListResDTOs.getPortfolioClientLists = ClientList.Select(x => new GetPortfolioClientList
                    {
                        UserId = x.Id,
                        ClientAccNo = x.ClientAccNo,
                        Name = x.FirstName + " " + (!string.IsNullOrWhiteSpace(x.MiddleName) ? x.MiddleName + " " : "") + x.LastName,
                        Email = x.Email,
                        MobileNo = x.MobileNo,
                        WorkNo = !String.IsNullOrWhiteSpace(x.WorkNo) ? x.WorkNo : "-",
                        Dob = x.Dob,
                        JoiningDate = x.CreatedDate,
                        Status = x.IsActive == true ? "Active" : "InActive",
                    }).ToList();
                    getPortfolioClientListResDTOs.TotalCount = getPortfolioClientListResDTOs.getPortfolioClientLists.Count;

                    getPortfolioClientListResDTOs.getPortfolioClientLists = getPortfolioClientListResDTOs.getPortfolioClientLists.Count <= PageSize ? getPortfolioClientListResDTOs.getPortfolioClientLists : getPortfolioClientListResDTOs.getPortfolioClientLists.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

                    getPortfolioClientListResDTOs.getPortfolioClientLists = OrderBy != null && OrderBy.Value ? getPortfolioClientListResDTOs.getPortfolioClientLists.OrderBy(x => x.Name).ToList() : getPortfolioClientListResDTOs.getPortfolioClientLists.OrderByDescending(x => x.Name).ToList();

                    commonResponse.Data = getPortfolioClientListResDTOs;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.Data = getPortfolioClientListResDTOs;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}