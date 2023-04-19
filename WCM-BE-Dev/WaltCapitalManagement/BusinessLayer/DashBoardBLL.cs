using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using System.Net;

namespace BusinessLayer
{
    public class DashBoardBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        public DashBoardBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
            _dbContext = dbContext;
            _commonRepo = commonRepo;
        }

        public CommonResponse GetDashBoardWaltValuation()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetDashBoardWaltValuationResDTO dashBoardWaltValuationResDTO = new GetDashBoardWaltValuationResDTO();



                decimal clienttransactionListR = 0;
                decimal clienttransactionListU = 0;
                var FundList = _commonRepo.fundList().Where(x => x.IsDeleted == false && x.IsActive == true).ToList();
                foreach (var fund in FundList)
                {
                    if (fund.Currency == "Rand (R)" || fund.Currency.ToLower() == "zar")
                    {
                        clienttransactionListR = clienttransactionListR + UpdateTransaction(fund.Currency, fund.Id);
                        //clienttransactionListR++;
                    }
                    else if (fund.Currency.ToLower() == "usd")
                    {
                        clienttransactionListU = clienttransactionListU + UpdateTransaction(fund.Currency, fund.Id);
                    }
                }

                dashBoardWaltValuationResDTO.ZAR = clienttransactionListR;
                dashBoardWaltValuationResDTO.USD = clienttransactionListU;

                if (dashBoardWaltValuationResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = dashBoardWaltValuationResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Data = dashBoardWaltValuationResDTO;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse GetDashBoardOffice()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<Office> officeList = _commonRepo.officeList().ToList().Select(x => new Office
                {
                    office = x.Office
                }).ToList();

                GetDashBoardOfficeResDTO getDashBoardOfficeResDTO = new GetDashBoardOfficeResDTO();

                getDashBoardOfficeResDTO.offices = officeList;

                if (getDashBoardOfficeResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getDashBoardOfficeResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Data = getDashBoardOfficeResDTO;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        private decimal UpdateTransaction(string TransactionIn, int fund)
        {
            decimal result = 0;
            //var FundWiseClientTransationList = _dbContext.ClientTransactionMsts.Where(x => x.IsActive == true && x.IsDeleted == false && x.Fund == fund && x.Client == client).ToList().OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).ToList();   


            var FundWiseClientTransationList = _dbContext.ClientTransactionMsts.Where(x => x.IsActive == true && x.IsDeleted == false && x.TransactionIn == TransactionIn && x.Fund == fund).ToList().ToList();
            if (FundWiseClientTransationList.Count > 0)
            {
                decimal unitBal = 0.0m, amtBal = 0.0m;
                foreach (var item in FundWiseClientTransationList)
                {
                    if (item.TransactionType.ToLower() == "buy")
                    {
                        unitBal = unitBal + Convert.ToDecimal(item.NumberOfUnits);
                        amtBal = amtBal + Convert.ToDecimal(item.TransactionAmount);
                        //if (unitBal < 0 || amtBal < 0)
                        //{
                        //    result = 0;
                        //    break;
                        //}
                        //else
                        //{
                        item.UnitBalance = unitBal;
                        item.AmountBalance = amtBal;
                        result = amtBal;
                        //}
                    }
                    else
                    {
                        unitBal = unitBal - Convert.ToDecimal(item.NumberOfUnits);
                        amtBal = amtBal - Convert.ToDecimal(item.TransactionAmount);
                        //if (unitBal < 0 || amtBal < 0)
                        //{
                        //    result = 0;
                        //    break;
                        //}
                        //else
                        //{
                        item.UnitBalance = unitBal;
                        item.AmountBalance = amtBal;
                        result = amtBal;
                        //}
                    }
                    //_dbContext.Entry(item).State = EntityState.Modified;
                    //_dbContext.SaveChanges();

                }

            }
            //_dbContext.Entry(item).State = EntityState.Modified;
            //_dbContext.SaveChanges();




            return result;
        }
        public CommonResponse MobileGetDashboard(MobileGetDashboardReqDTO mobileGetDashboardReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                int ClientAccessCategoryId = 2; //Clients = 2
                MobileGetDashboardResDTO portFolioDataResDTO = new MobileGetDashboardResDTO();

                var UserDetails = _commonRepo.getUserList().Where(x => x.AccessCategoryId == ClientAccessCategoryId && x.Id == mobileGetDashboardReqDTO.ClientId).Select(x => new { x.FirstName, x.LastName }).FirstOrDefault();

                if (UserDetails != null)
                {
                    var ServiceProviderList = _commonRepo.serviceProviderList();
                    var ServiceProviderTypeList = _commonRepo.serviceProviderTypeList();

                    var ExternalAccountList = _commonRepo.externalAccountList().Where(x => x.ClientId == mobileGetDashboardReqDTO.ClientId).ToList();
                    var CSVLogList = _commonRepo.getPPMCSVLogList().ToList();
                    var CSVFUploadLGrpByServiceProvider = CSVLogList.OrderByDescending(x => x.Id).DistinctBy(x => x.ServiceProviderId).ToList();

                    double TotalClientInvestment = 0;
                    List<ClientInvestmentDetails> clientInvestmentDetails = new List<ClientInvestmentDetails>();
                    foreach (var item in CSVFUploadLGrpByServiceProvider)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(item.ServiceProviderId)))
                        {
                            int serviceProviderId = Convert.ToInt32(item.ServiceProviderId);
                            var AccountCodeandType = ExternalAccountList.Where(x => x.ServiceProvider == serviceProviderId).Select(x => new { x.AccountCode, x.Type }).ToList();

                            foreach (var subItem in AccountCodeandType)
                            {
                                var CSVTotalAmount = subItem != null ? _commonRepo.getPPMCSVDataList().Where(x => x.CsvfileId == item.Id && x.AccountNo == subItem.AccountCode && x.Category == ServiceProviderCategoryConstant.Total_Value).Sum(x => x.Value) : 0;

                                if (CSVTotalAmount > 0)
                                {
                                    var ServiceProviderName = ServiceProviderList.FirstOrDefault(x => x.Id == serviceProviderId)?.ServiceProvider;
                                    var ServiceProviderTypeName = ServiceProviderTypeList.FirstOrDefault(x => x.Id == subItem.Type)?.ServiceProviderType;

                                    clientInvestmentDetails.Add(new ClientInvestmentDetails
                                    {
                                        Id = Convert.ToInt32(item.ServiceProviderId),
                                        Title = (ServiceProviderName ?? string.Empty) + " (" + ServiceProviderTypeName + ")",
                                        Value = "R " + _commonHelper.GetFormatedDouble(CSVTotalAmount ?? 0)
                                    });
                                    TotalClientInvestment += CSVTotalAmount ?? 0;
                                }
                            }
                        }
                    }
                    portFolioDataResDTO.ClientInvestmentDetails = clientInvestmentDetails;

                    // Call third party API to get Currency Coversion and set variables
                    double PriceInZar = TotalClientInvestment;

                    double PriceInUsd = _commonHelper.GetConvertedCurrency(CurrencySymbolConstant.South_African_Rand, CurrencySymbolConstant.United_States_Dollar, TotalClientInvestment, null).Data;
                    double GoldInOz = _commonHelper.GetConvertedCurrency(CurrencySymbolConstant.South_African_Rand, CurrencySymbolConstant.Gold_troy_ounce, TotalClientInvestment, null).Data;
                    // Gold_troy_ounce to grams : Formula - for an approximate result, multiply the mass value by 28.35
                    double PriceInGold = GoldInOz * 28.35;
                    double PriceInBitcoin = _commonHelper.GetConvertedCurrency(CurrencySymbolConstant.South_African_Rand, CurrencySymbolConstant.Bitcoin, TotalClientInvestment, null).Data;

                    ClientInvestmentAmounts clientInvestmentAmounts = new ClientInvestmentAmounts();
                    clientInvestmentAmounts.PriceInZar = _commonHelper.GetFormatedDouble(PriceInZar);
                    clientInvestmentAmounts.PriceInUsd = _commonHelper.GetFormatedDouble(PriceInUsd);
                    clientInvestmentAmounts.PriceInGold = _commonHelper.GetFormatedDouble(PriceInGold);
                    clientInvestmentAmounts.PriceInBitcoin = _commonHelper.GetFormatedDouble(PriceInBitcoin);

                    portFolioDataResDTO.ClientInvestmentAmounts = clientInvestmentAmounts;

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = portFolioDataResDTO;
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
