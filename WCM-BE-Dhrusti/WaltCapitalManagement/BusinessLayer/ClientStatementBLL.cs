using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using System.Net;

namespace BusinessLayer
{
    public class ClientStatementBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;


        public ClientStatementBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
            _dbContext = dbContext;
            _commonRepo = commonRepo;
        }

        public CommonResponse GetReportType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetReportTypeResDTO getReportTypeResDTO = new GetReportTypeResDTO();
                List<string> res = new();
                res = Enum.GetNames(typeof(ReportType)).ToList();
                if (res != null)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                }

                getReportTypeResDTO.ReportType = res;
                commonResponse.Data = getReportTypeResDTO;
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetClientStatementByFundId(GetClientStatementByFundIdReqDTO getClientStatementByFundIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetClientStatementByFundIdResDTO getClientStatementByFundIdResDTO = new GetClientStatementByFundIdResDTO();
                List<ClientStatementDetails> clientList = new List<ClientStatementDetails>();

                if (!string.IsNullOrEmpty(getClientStatementByFundIdReqDTO.SearchString))
                {
                    clientList = (from ctm in _commonRepo.clientTransactionList().Where(x => x.Fund == getClientStatementByFundIdReqDTO.FundId)
                                  join clientUl in _commonRepo.getUserList() on ctm.Client equals clientUl.Id
                                  where clientUl.ClientAccNo.ToLower().Contains(getClientStatementByFundIdReqDTO.SearchString.ToLower()) || clientUl.FirstName.ToLower().Contains(getClientStatementByFundIdReqDTO.SearchString.ToLower()) || clientUl.LastName.ToLower().Contains(getClientStatementByFundIdReqDTO.SearchString.ToLower())
                                  select new ClientStatementDetails
                                  {
                                      ClientAccountNo = clientUl.ClientAccNo,
                                      Name = clientUl.FirstName + " " + clientUl.LastName,
                                  }).Distinct().ToList();
                }
               getClientStatementByFundIdResDTO.ClientList = clientList.ToList();

                if (clientList.Count > 0)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getClientStatementByFundIdResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Data = getClientStatementByFundIdResDTO;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetClientStatementReport(GetClientStatementReportReqDTO getClientStatementReportReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetClientStatementReportResDTO getClientStatementReportResDTO = new GetClientStatementReportResDTO();

                bool IsReportType = Enum.IsDefined(typeof(ReportType), getClientStatementReportReqDTO.ReportType);

                if (IsReportType)
                {

                    var fundDetails = _commonRepo.fundList().Where(x => x.Id == getClientStatementReportReqDTO.FundId).Select(x => new { Id = x.Id, FundCurrency = x.Currency, FundName = x.FundName }).FirstOrDefault();

                    var clientDetails = _commonRepo.getUserList().Where(x => x.ClientAccNo == getClientStatementReportReqDTO.ClientAccNo).Select(x => new { Id = x.Id, Salutation = x.Salutation, FirstName = x.FirstName, LastName = x.LastName, ClientAccNo = x.ClientAccNo, IFA = x.Ifa }).FirstOrDefault();

                    if (clientDetails != null)
                    {
                        var IFADetails = _commonRepo.getUserList().Where(x => x.Ifa == clientDetails.IFA).Select(x => new { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, ContactNo = x.MobileNo }).FirstOrDefault();

                        getClientStatementReportResDTO.IFAId = IFADetails != null ? IFADetails.Id : 0;
                        getClientStatementReportResDTO.IFAName = IFADetails != null ? IFADetails.FirstName + " " + IFADetails.LastName : "";
                        getClientStatementReportResDTO.IFAConsultants = IFADetails != null ? IFADetails.LastName + " " + "Consultants" : "";
                        getClientStatementReportResDTO.ContactNo = IFADetails != null ? IFADetails.ContactNo : "";
                    }

                    getClientStatementReportResDTO.FundName = fundDetails != null ? fundDetails.FundName : "";
                    getClientStatementReportResDTO.FundId = getClientStatementReportReqDTO.FundId;
                    getClientStatementReportResDTO.FundCurrency = fundDetails != null ? fundDetails.FundCurrency : "";
                    getClientStatementReportResDTO.StartDate = getClientStatementReportReqDTO.StartDate;
                    getClientStatementReportResDTO.EndDate = getClientStatementReportReqDTO.EndDate;
                    getClientStatementReportResDTO.ClientId = clientDetails != null ? clientDetails.Id : 0;
                    getClientStatementReportResDTO.ClientAccNo = clientDetails != null ? clientDetails.ClientAccNo : "";
                    getClientStatementReportResDTO.ClientName = clientDetails != null ? clientDetails.Salutation + " " + clientDetails.FirstName + " " + clientDetails.LastName : "";
                    

                    decimal transactionScopeStatus = DepositeTransaction(getClientStatementReportReqDTO.FundId, clientDetails != null ? clientDetails.Id : 0, getClientStatementReportReqDTO.StartDate, getClientStatementReportReqDTO.EndDate);

                    getClientStatementReportResDTO.Deposit = transactionScopeStatus;
                    getClientStatementReportResDTO.ClosingBalanceIncludingTransactionsInProgress = transactionScopeStatus;
                    getClientStatementReportResDTO.ClosingBalancePricedInGold = transactionScopeStatus;




                    if (getClientStatementReportResDTO != null)
                    {
                        commonResponse.Message = "Success";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = getClientStatementReportResDTO;
                    }
                    else
                    {
                        commonResponse.Message = "Data Not Found.";
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        private decimal DepositeTransaction(int fund, int client, DateTime StartDate, DateTime EndDate)
        {
            decimal result = 0;

            var FundWiseClientTransationList = _dbContext.ClientTransactionMsts.Where(x => x.IsActive == true && x.IsDeleted == false && x.Fund == fund && x.Client == client && x.TransactionDate.Date >= StartDate.Date && x.TransactionDate.Date <= EndDate.Date).ToList().OrderBy(x => x.Id).ToList();
            if (FundWiseClientTransationList.Count > 0)
            {
                decimal unitBal = 0.0m, amtBal = 0.0m;
                foreach (var item in FundWiseClientTransationList)
                {
                    if (item.TransactionType.ToLower() == "buy")
                    {
                        unitBal = unitBal + Convert.ToDecimal(item.NumberOfUnits);
                        amtBal = amtBal + Convert.ToDecimal(item.TransactionAmount);

                    }
                    else
                    {
                        unitBal = unitBal - Convert.ToDecimal(item.NumberOfUnits);
                        amtBal = amtBal - Convert.ToDecimal(item.TransactionAmount);

                    }

                    result = amtBal;
                }

            }
            return result;
        }
    }
}
