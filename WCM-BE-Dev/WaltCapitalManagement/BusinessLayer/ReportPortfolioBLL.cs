using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace BusinessLayer
{
    public class ReportPortfolioBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly ExternalAccountBLL _externalAccountBLL;
        private readonly IConfiguration _configaration;
        public ReportPortfolioBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration, ExternalAccountBLL externalAccountBLL)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configaration = configuration;
            _externalAccountBLL = externalAccountBLL;
        }
        public CommonResponse GetPortfolioManagerFee(GetPortfolioManagerFeeReqDTO getPortfolioManagerFeeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();


            var pageData = _configaration.GetSection("ByDefaultPagination:Page");
            string pages = pageData.Value.ToString();
            int pagecount = Int32.Parse(pages);

            var totalPage = _configaration.GetSection("ByDefaultPagination:PageSize");
            string Size = totalPage.Value.ToString();
            int pageSize = Int32.Parse(Size);

            var orderBy = _configaration.GetSection("ByDefaultPagination:OrderBy");
            string orders = orderBy.Value.ToString();
            bool order = Boolean.Parse(orders);


            int number = getPortfolioManagerFeeReqDTO.PageNumber == 0 ? (pagecount) : getPortfolioManagerFeeReqDTO.PageNumber;
            int size = getPortfolioManagerFeeReqDTO.PageSize == 0 ? (pageSize) : getPortfolioManagerFeeReqDTO.PageSize;
            bool orderby = getPortfolioManagerFeeReqDTO.Orderby == true ? (order) : getPortfolioManagerFeeReqDTO.Orderby;

            try
            {
                GetPortfolioManagerFeeResDTO getPortfolioManagerFeeRes = new GetPortfolioManagerFeeResDTO();
                List<PortfolioDetails> portfolioDetails = new List<PortfolioDetails>();

                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "AManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "BManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "CManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "DManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "EManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "FManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "GManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "HManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "IManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "JManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "KManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "LManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "MManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "NManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "OManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });
                portfolioDetails.Add(new PortfolioDetails { Managementfeeslocal = 0, ManagementfeesOffshore = 0, Minfees = 0, Performancefees = 0, PortfolioManager = "PManger", PreffeesOffshore = 0, Total = 0, VAT = 1 });

                getPortfolioManagerFeeRes.TotalCount = portfolioDetails.Count;

                if (orderby)
                {
                    if (portfolioDetails.Count <= size)
                    {
                        portfolioDetails = portfolioDetails.OrderBy(x => x.PortfolioManager).ToList();
                    }
                    else
                    {
                        portfolioDetails = portfolioDetails.Skip((number - 1) * size).Take(size).OrderBy(x => x.PortfolioManager).ToList();
                    }
                }
                else
                {
                    if (portfolioDetails.Count <= size)
                    {
                        portfolioDetails = portfolioDetails.OrderByDescending(x => x.PortfolioManager).ToList();
                    }
                    else
                    {
                        portfolioDetails = portfolioDetails.OrderByDescending(x => x.PortfolioManager).Skip((number - 1) * size).Take(size).ToList();
                    }
                }

                getPortfolioManagerFeeRes.PortfolioDetails = portfolioDetails;

                if (getPortfolioManagerFeeRes.PortfolioDetails.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getPortfolioManagerFeeRes;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                    commonResponse.Data = getPortfolioManagerFeeRes;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse GetPPMClientList(GetPPMClientListReqDTO getPPMClientListReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();

            var pageData = _configaration.GetSection("ByDefaultPagination:Page");
            string pages = pageData.Value.ToString();
            int pagecount = Int32.Parse(pages);

            var totalPage = _configaration.GetSection("ByDefaultPagination:PageSize");
            string Size = totalPage.Value.ToString();
            int pageSize = Int32.Parse(Size);

            var orderBy = _configaration.GetSection("ByDefaultPagination:OrderBy");
            string orders = orderBy.Value.ToString();
            bool order = Boolean.Parse(orders);

            int number = getPPMClientListReqDTO.PageNumber == 0 ? (pagecount) : getPPMClientListReqDTO.PageNumber;
            int size = getPPMClientListReqDTO.PageSize == 0 ? (pageSize) : getPPMClientListReqDTO.PageSize;
            bool orderby = getPPMClientListReqDTO.Orderby == true ? (order) : getPPMClientListReqDTO.Orderby;

            try
            {
                GetPPMClientListResDTO getPPMClientListRes = new GetPPMClientListResDTO();
                List<PPMClientDetail> pPMClientDetailList = new List<PPMClientDetail>();


                List<GetAllServiceProviderReportsResDTO> res = _externalAccountBLL.GetAllReportData(3, 0).Data;
                // pPMClientTFSA = res.Data;
                res = res.OrderByDescending(x => x.CreatedDate).ToList();
                if (res != null)
                {
                    foreach (var row in res)
                    {
                        PPMClientDetail pPMClientDetail = new PPMClientDetail();
                        pPMClientDetail.Surname = row.LastName;
                        pPMClientDetail.AccountNo = row.ExternalAccountNo;
                        pPMClientDetail.AccValue = row.AccountValue > 0 ? Convert.ToDecimal(row.AccountValue.ToString("###0.00")) : Convert.ToDecimal("0.00"); //row.AccountValue;
                        pPMClientDetail.Currency = row.Currency;
                        pPMClientDetail.Name = row.FirstName;
                        pPMClientDetailList.Add(pPMClientDetail);
                    }



                    if (getPPMClientListReqDTO.Alphabet != null && !string.IsNullOrEmpty(getPPMClientListReqDTO.Alphabet))
                    {
                        pPMClientDetailList = pPMClientDetailList.Where(x => x.Surname.ToLower().StartsWith(getPPMClientListReqDTO.Alphabet.ToLower())).ToList();

                        getPPMClientListRes.TotalCount = pPMClientDetailList.Count();
                    }
                    if (getPPMClientListReqDTO.SearchString != null && !string.IsNullOrEmpty(getPPMClientListReqDTO.SearchString))
                    {
                        pPMClientDetailList = pPMClientDetailList.Where(x => x.Name.ToLower().Contains(getPPMClientListReqDTO.SearchString.ToLower()) || x.Surname.ToLower().Contains(getPPMClientListReqDTO.SearchString.ToLower()) || x.AccountNo.ToLower().Contains(getPPMClientListReqDTO.SearchString.ToLower())).ToList();

                        getPPMClientListRes.TotalCount = pPMClientDetailList.Count();
                    }

                    getPPMClientListRes.TotalCount = pPMClientDetailList.Count;

                    if (orderby)
                    {
                        if (pPMClientDetailList.Count <= size)
                        {
                            pPMClientDetailList = pPMClientDetailList.ToList();
                        }
                        else
                        {
                            pPMClientDetailList = pPMClientDetailList.Skip((number - 1) * size).Take(size).ToList();
                        }
                    }
                    else
                    {
                        if (pPMClientDetailList.Count <= size)
                        {
                            pPMClientDetailList = pPMClientDetailList.ToList();
                        }
                        else
                        {
                            pPMClientDetailList = pPMClientDetailList.Skip((number - 1) * size).Take(size).ToList();
                        }
                    }

                    getPPMClientListRes.PPMClientDetails = pPMClientDetailList;

                    if (getPPMClientListRes.PPMClientDetails.Count > 0)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Success.";
                        commonResponse.Data = getPPMClientListRes;
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data not Found.";
                        commonResponse.Data = getPPMClientListRes;
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
        public CommonResponse GetPPMClientListTFSA(GetPPMClientListTFSAReqDTO getPPMClientListTFSAReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();


            var pageData = _configaration.GetSection("ByDefaultPagination:Page");
            string pages = pageData.Value.ToString();
            int pagecount = Int32.Parse(pages);

            var totalPage = _configaration.GetSection("ByDefaultPagination:PageSize");
            string Size = totalPage.Value.ToString();
            int pageSize = Int32.Parse(Size);

            var orderBy = _configaration.GetSection("ByDefaultPagination:OrderBy");
            string orders = orderBy.Value.ToString();
            bool order = Boolean.Parse(orders);


            int number = getPPMClientListTFSAReqDTO.PageNumber == 0 ? (pagecount) : getPPMClientListTFSAReqDTO.PageNumber;
            int size = getPPMClientListTFSAReqDTO.PageSize == 0 ? (pageSize) : getPPMClientListTFSAReqDTO.PageSize;
            bool orderby = getPPMClientListTFSAReqDTO.Orderby == true ? (order) : getPPMClientListTFSAReqDTO.Orderby;

            try
            {
                GetPPMClientListTFSAResDTO getPPMClientListTFSAResDTO = new GetPPMClientListTFSAResDTO();
                List<PPMClientTFSADetail> pPMClientTFSA = new List<PPMClientTFSADetail>();
               

                List<GetAllServiceProviderReportsResDTO> res = _externalAccountBLL.GetAllReportData(3, 3).Data;
                /// pPMClientTFSA = res.Data;
                res = res.OrderByDescending(x => x.CreatedDate).ToList();
                if (res != null)
                {
                    foreach (var row in res)
                    {
                        PPMClientTFSADetail pPMClientTFSADetail = new PPMClientTFSADetail();
                        pPMClientTFSADetail.Surname = row.LastName;
                        pPMClientTFSADetail.AccountNo = row.ExternalAccountNo;
                        pPMClientTFSADetail.AccValue = row.AccountValue > 0 ? Convert.ToDecimal(row.AccountValue.ToString("###0.00")) : Convert.ToDecimal("0.00");
                        pPMClientTFSADetail.Currency = row.Currency;
                        pPMClientTFSADetail.Name = row.FirstName;
                        pPMClientTFSA.Add(pPMClientTFSADetail);
                    }
                    getPPMClientListTFSAResDTO.TotalCount = pPMClientTFSA.Count;


                    if (getPPMClientListTFSAReqDTO.Alphabet != null && !string.IsNullOrEmpty(getPPMClientListTFSAReqDTO.Alphabet))
                    {
                        pPMClientTFSA = pPMClientTFSA.Where(x => x.Surname.ToLower().StartsWith(getPPMClientListTFSAReqDTO.Alphabet.ToLower())).ToList();

                        getPPMClientListTFSAResDTO.TotalCount = pPMClientTFSA.Count();
                    }
                    if (getPPMClientListTFSAReqDTO.SearchString != null && !string.IsNullOrEmpty(getPPMClientListTFSAReqDTO.SearchString))
                    {
                        


                        pPMClientTFSA = pPMClientTFSA.Where(x => x.Name.ToLower().Contains(getPPMClientListTFSAReqDTO.SearchString.ToLower()) || x.Surname.ToLower().Contains(getPPMClientListTFSAReqDTO.SearchString.ToLower()) || x.AccountNo.ToLower().Contains(getPPMClientListTFSAReqDTO.SearchString.ToLower())).ToList();
                        getPPMClientListTFSAResDTO.TotalCount = pPMClientTFSA.Count();
                    }



                    if (orderby)
                    {
                        if (pPMClientTFSA.Count <= size)
                        {
                            pPMClientTFSA = pPMClientTFSA.ToList();
                        }
                        else
                        {

                            pPMClientTFSA = pPMClientTFSA.Skip((number - 1) * size)
                               .Take(size)
                               .ToList();
                        }
                    }
                    else
                    {
                        if (pPMClientTFSA.Count <= size)
                        {
                            pPMClientTFSA = pPMClientTFSA.ToList();
                        }
                        else
                        {
                            pPMClientTFSA = pPMClientTFSA.Skip((number - 1) * size)
                                .Take(size)
                                .ToList();
                        }
                    }


                    getPPMClientListTFSAResDTO.PPMClientTFSADetails = pPMClientTFSA;
                   

                    if (getPPMClientListTFSAResDTO.PPMClientTFSADetails.Count > 0)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Success.";
                        commonResponse.Data = getPPMClientListTFSAResDTO;
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data not Found.";
                        commonResponse.Data = getPPMClientListTFSAResDTO;
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

    }
}
