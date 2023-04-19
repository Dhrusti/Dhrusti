using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer
{
    public class AllReportsBLL
    {

        private readonly IConfiguration _configuration;
        private readonly CommonRepo _commonRepo;
        private readonly ExternalAccountBLL _externalAccountBLL;

        public AllReportsBLL(IConfiguration configuration, CommonRepo commonRepo, ExternalAccountBLL externalAccountBLL)
        {
            _configuration = configuration;
            _commonRepo = commonRepo;
            _externalAccountBLL = externalAccountBLL;
        }

        public CommonResponse GetTradeStationClientList(GetTradeStationClientListReqDTO getTradeStationClientListReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();

            var pageData = _configuration.GetSection("ByDefaultPagination:Page");
            string pages = pageData.Value.ToString();
            int pagecount = Int32.Parse(pages);

            var totalPage = _configuration.GetSection("ByDefaultPagination:PageSize");
            string Size = totalPage.Value.ToString();
            int pageSize = Int32.Parse(Size);

            var orderBy = _configuration.GetSection("ByDefaultPagination:OrderBy");
            string orders = orderBy.Value.ToString();
            bool order = Boolean.Parse(orders);

            int number = getTradeStationClientListReqDTO.PageNumber == 0 ? (pagecount) : getTradeStationClientListReqDTO.PageNumber;
            int size = getTradeStationClientListReqDTO.PageSize == 0 ? (pageSize) : getTradeStationClientListReqDTO.PageSize;
            bool orderby = getTradeStationClientListReqDTO.Orderby == true ? (order) : getTradeStationClientListReqDTO.Orderby;

            try
            {
                GetTradeStationClientListResDTO getTradeStationClientListRes = new GetTradeStationClientListResDTO();
                List<TradeClientList> TradeClientLists = new List<TradeClientList>();

                var reportsData = _externalAccountBLL.GetAllReportData(5, 0);  // Trade Station = 5 
                if (reportsData.Status == true)
                {
                    List<GetAllServiceProviderReportsResDTO> getAllServiceProviderReportsList = reportsData.Data;
                    getAllServiceProviderReportsList = getAllServiceProviderReportsList.OrderByDescending(x => x.CreatedDate).ToList();
                    TradeClientList tradeClientLists = new TradeClientList();
                    if (getAllServiceProviderReportsList != null)
                    {
                        foreach (var item in getAllServiceProviderReportsList)
                        {
                            tradeClientLists = new TradeClientList();
                            tradeClientLists.ClientName = item.LastName + " " + item.FirstName;
                            tradeClientLists.Account = item.ExternalAccountNo;
                            tradeClientLists.PortfolioValue = item.PortfolioValueStr;
                            tradeClientLists.Email = item.Email;
                            tradeClientLists.ContactNumber = item.Mobile != null  ? item.Mobile : "-";
                            tradeClientLists.PortfolioManager = item.IFAId > 0 ? item.IFAFirstName + " " + item.IFALastName : "-";

                            TradeClientLists.Add(tradeClientLists);
                        }
                        getTradeStationClientListRes.TotalCount = TradeClientLists.Count();

                        if (TradeClientLists.Count > 0)
                        {
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Data Found.";
                            commonResponse.Data = TradeClientLists;
                        }
                        else
                        {
                            commonResponse.StatusCode = HttpStatusCode.NotFound;
                            commonResponse.Message = "Data not Found.";
                        }
                    }
                    else
                    {
                        commonResponse = reportsData;
                    }

                    if (getTradeStationClientListReqDTO.Alphabet != null && !string.IsNullOrEmpty(getTradeStationClientListReqDTO.Alphabet))
                    {
                        TradeClientLists = TradeClientLists.Where(x => x.ClientName.ToLower().StartsWith(getTradeStationClientListReqDTO.Alphabet.ToLower())).ToList();

                        getTradeStationClientListRes.TotalCount = TradeClientLists.Count();
                    }
                    if (getTradeStationClientListReqDTO.SearchString != null && !string.IsNullOrEmpty(getTradeStationClientListReqDTO.SearchString))
                    {
                        TradeClientLists = TradeClientLists.Where(x => x.Account.ToLower().Contains(getTradeStationClientListReqDTO.SearchString.ToLower()) || x.ClientName.ToLower().Contains(getTradeStationClientListReqDTO.SearchString.ToLower())).ToList();

                        getTradeStationClientListRes.TotalCount = TradeClientLists.Count();
                    }

                    getTradeStationClientListRes.TotalCount = TradeClientLists.Count();


                    if (orderby)
                    {
                        if (TradeClientLists.Count <= size)
                        {
                            TradeClientLists = TradeClientLists.ToList();
                        }
                        else
                        {
                            TradeClientLists = TradeClientLists.Skip((number - 1) * size).Take(size).ToList();
                        }
                    }
                    else
                    {
                        if (TradeClientLists.Count <= size)
                        {
                            TradeClientLists = TradeClientLists.ToList();
                        }
                        else
                        {
                            TradeClientLists = TradeClientLists.Skip((number - 1) * size).Take(size).ToList();
                        }
                    }
                    getTradeStationClientListRes.TradeClientLists = TradeClientLists;
                    if (TradeClientLists.Count > 0)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Success.";
                        commonResponse.Data = getTradeStationClientListRes.Adapt<GetTradeStationClientListResDTO>();
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data not Found.";
                        commonResponse.Data = null;
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                    commonResponse.Data = null;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse InteractiveBrokersClientList(GetInteractiveBrokersClientListReqDTO getInteractiveBrokersClientListReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();

            var pageData = _configuration.GetSection("ByDefaultPagination:Page");
            string pages = pageData.Value.ToString();
            int pagecount = Int32.Parse(pages);

            var totalPage = _configuration.GetSection("ByDefaultPagination:PageSize");
            string Size = totalPage.Value.ToString();
            int pageSize = Int32.Parse(Size);

            var orderBy = _configuration.GetSection("ByDefaultPagination:OrderBy");
            string orders = orderBy.Value.ToString();
            bool order = Boolean.Parse(orders);


            int number = getInteractiveBrokersClientListReqDTO.PageNumber == 0 ? (pagecount) : getInteractiveBrokersClientListReqDTO.PageNumber;
            int size = getInteractiveBrokersClientListReqDTO.PageSize == 0 ? (pageSize) : getInteractiveBrokersClientListReqDTO.PageSize;
            bool orderby = getInteractiveBrokersClientListReqDTO.Orderby == true ? (order) : getInteractiveBrokersClientListReqDTO.Orderby;

            try
            {
                GetInteractiveBrokersClientListResDTO getInteractiveBrokersClientListRes = new GetInteractiveBrokersClientListResDTO();
                List<InteractiveBrokersClientList> InteractiveBrokersClientLists = new List<InteractiveBrokersClientList>();

                var reportsData = _externalAccountBLL.GetAllReportData(2, 0);  // Interactive Brokers = 2 
                if (reportsData.Status == true)
                {
                    List<GetAllServiceProviderReportsResDTO> getAllServiceProviderReportsList = reportsData.Data;
                    getAllServiceProviderReportsList = getAllServiceProviderReportsList.OrderByDescending(x => x.CreatedDate).ToList();
                    InteractiveBrokersClientList interactiveBrokers = new InteractiveBrokersClientList();
                    foreach (var item in getAllServiceProviderReportsList)
                    {
                        interactiveBrokers = new InteractiveBrokersClientList();
                        interactiveBrokers.AccountNo = item.ExternalAccountNo;
                        interactiveBrokers.WaltCapNo = item.ClientAccountNo;
                        interactiveBrokers.Name = item.FirstName;
                        interactiveBrokers.Surname = item.LastName;
                        interactiveBrokers.PortfolioManager = item.IFAId > 0 ? item.IFAFirstName + " " + item.IFALastName : "-" ;
                        interactiveBrokers.PortfolioValue = item.PortfolioValueStr;


                        InteractiveBrokersClientLists.Add(interactiveBrokers);
                    }
                    getInteractiveBrokersClientListRes.TotalCount = InteractiveBrokersClientLists.Count();



                    if (InteractiveBrokersClientLists.Count > 0)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Data Found.";
                        commonResponse.Data = InteractiveBrokersClientLists;
                    }
                    else
                    {
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data not Found.";
                    }
                }
                else
                {
                    commonResponse = reportsData;
                }


                if (getInteractiveBrokersClientListReqDTO.Alphabet != null && !string.IsNullOrEmpty(getInteractiveBrokersClientListReqDTO.Alphabet))
                {
                    InteractiveBrokersClientLists = InteractiveBrokersClientLists.Where(x => x.Surname.ToLower().StartsWith(getInteractiveBrokersClientListReqDTO.Alphabet.ToLower())).ToList();

                    getInteractiveBrokersClientListRes.TotalCount = InteractiveBrokersClientLists.Count();
                }
                if (getInteractiveBrokersClientListReqDTO.SearchString != null && !string.IsNullOrEmpty(getInteractiveBrokersClientListReqDTO.SearchString))
                {
                    InteractiveBrokersClientLists = InteractiveBrokersClientLists.Where(x => x.Name.ToLower().Contains(getInteractiveBrokersClientListReqDTO.SearchString.ToLower()) || x.Surname.ToLower().Contains(getInteractiveBrokersClientListReqDTO.SearchString.ToLower()) || x.AccountNo.ToLower().Contains(getInteractiveBrokersClientListReqDTO.SearchString.ToLower())).ToList();

                    getInteractiveBrokersClientListRes.TotalCount = InteractiveBrokersClientLists.Count();
                }

                getInteractiveBrokersClientListRes.TotalCount = InteractiveBrokersClientLists.Count();
                if (orderby)
                {
                    if (InteractiveBrokersClientLists.Count <= size)
                    {
                        InteractiveBrokersClientLists = InteractiveBrokersClientLists.ToList();
                    }
                    else
                    {
                        InteractiveBrokersClientLists = InteractiveBrokersClientLists.Skip((number - 1) * size).Take(size).ToList();
                    }
                }
                else
                {
                    if (InteractiveBrokersClientLists.Count <= size)
                    {
                        InteractiveBrokersClientLists = InteractiveBrokersClientLists.ToList();
                    }
                    else
                    {
                        InteractiveBrokersClientLists = InteractiveBrokersClientLists.Skip((number - 1) * size).Take(size).ToList();
                    }
                }
                getInteractiveBrokersClientListRes.InteractiveBrokersClientLists = InteractiveBrokersClientLists;
                if (InteractiveBrokersClientLists.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getInteractiveBrokersClientListRes.Adapt<GetInteractiveBrokersClientListResDTO>();
                }
                else
                {

                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                    commonResponse.Data = null;

                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AllenGrayClientList(GetAllenGrayClientListReqDTO getAllenGrayClientListReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();


            var pageData = _configuration.GetSection("ByDefaultPagination:Page");
            string pages = pageData.Value.ToString();
            int pagecount = Int32.Parse(pages);

            var totalPage = _configuration.GetSection("ByDefaultPagination:PageSize");
            string Size = totalPage.Value.ToString();
            int pageSize = Int32.Parse(Size);

            var orderBy = _configuration.GetSection("ByDefaultPagination:OrderBy");
            string orders = orderBy.Value.ToString();
            bool order = Boolean.Parse(orders);

            int number = getAllenGrayClientListReqDTO.PageNumber == 0 ? (pagecount) : getAllenGrayClientListReqDTO.PageNumber;
            int size = getAllenGrayClientListReqDTO.PageSize == 0 ? (pageSize) : getAllenGrayClientListReqDTO.PageSize;
            bool orderby = getAllenGrayClientListReqDTO.Orderby == true ? (order) : getAllenGrayClientListReqDTO.Orderby;

            try
            {
                GetAllenGrayClientListResDTO getAllenGrayClientListResDTO = new GetAllenGrayClientListResDTO();
                List<AllenGrayClientList> AllenGrayClientLists = new List<AllenGrayClientList>();

                var reportsData = _externalAccountBLL.GetAllReportData(1, 0);  // Allan Gray = 1 
                if (reportsData.Status == true)
                {
                    List<GetAllServiceProviderReportsResDTO> getAllServiceProviderReportsList = reportsData.Data;
                    getAllServiceProviderReportsList = getAllServiceProviderReportsList.OrderByDescending(x => x.CreatedDate).ToList();
                    AllenGrayClientList allenGrayClientList = new AllenGrayClientList();
                    foreach (var item in getAllServiceProviderReportsList)
                    {
                        allenGrayClientList = new AllenGrayClientList();
                        allenGrayClientList.ClientId = item.Id;
                        //allenGrayClientList.ClientName = item.FirstName + " " + item.MiddleName + " " + item.LastName;
                        allenGrayClientList.ClientName = item.LastName + " " + item.FirstName;
                        allenGrayClientList.ClientAccNo = item.ExternalAccountNo;
                        allenGrayClientList.InvestmentValue = item.InvestmentValueStr;

                        AllenGrayClientLists.Add(allenGrayClientList);
                    }
                  
                    getAllenGrayClientListResDTO.TotalCount = AllenGrayClientLists.Count();



                    if (AllenGrayClientLists.Count > 0)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Data Found.";
                        commonResponse.Data = AllenGrayClientLists;
                    }
                    else
                    {
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data not Found.";
                    }
                }
                else
                {
                    commonResponse = reportsData;
                }


                if (getAllenGrayClientListReqDTO.Alphabet != null && !string.IsNullOrEmpty(getAllenGrayClientListReqDTO.Alphabet))
                {
                    AllenGrayClientLists = AllenGrayClientLists.Where(x => x.ClientName.ToLower().StartsWith(getAllenGrayClientListReqDTO.Alphabet.ToLower())).ToList();

                    getAllenGrayClientListResDTO.TotalCount = AllenGrayClientLists.Count();
                }
                if (getAllenGrayClientListReqDTO.SearchString != null && !string.IsNullOrEmpty(getAllenGrayClientListReqDTO.SearchString))
                {
                    AllenGrayClientLists = AllenGrayClientLists.Where(x => x.ClientName.ToLower().Contains(getAllenGrayClientListReqDTO.SearchString.ToLower()) || x.ClientAccNo.ToLower().Contains(getAllenGrayClientListReqDTO.SearchString.ToLower()) || x.InvestmentValue.ToLower().Contains(getAllenGrayClientListReqDTO.SearchString.ToLower())).ToList();

                    getAllenGrayClientListResDTO.TotalCount = AllenGrayClientLists.Count();
                }

              



                if (orderby)
                {
                    if (AllenGrayClientLists.Count <= size)
                    {
                        AllenGrayClientLists = AllenGrayClientLists.ToList();
                    }
                    else
                    {
                        AllenGrayClientLists = AllenGrayClientLists.Skip((number - 1) * size).Take(size).ToList();
                    }
                }
                else
                {
                    if (AllenGrayClientLists.Count <= size)
                    {
                        AllenGrayClientLists = AllenGrayClientLists.ToList();
                    }
                    else
                    {
                        AllenGrayClientLists = AllenGrayClientLists.Skip((number - 1) * size).Take(size).ToList();
                    }
                }
                if (getAllenGrayClientListReqDTO.ShowZeroBalance == false)
                {
                    AllenGrayClientLists = AllenGrayClientLists.Where(x =>  Convert.ToDecimal(x.InvestmentValue != null? x.InvestmentValue:0) > 0 ).ToList();
                   
                }
               
                getAllenGrayClientListResDTO.AllenGrayClientLists = AllenGrayClientLists;
                getAllenGrayClientListResDTO.TotalCount = AllenGrayClientLists.Count();
                if (AllenGrayClientLists.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getAllenGrayClientListResDTO.Adapt<GetAllenGrayClientListResDTO>();
                }
                else
                {

                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                    commonResponse.Data = null;

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
