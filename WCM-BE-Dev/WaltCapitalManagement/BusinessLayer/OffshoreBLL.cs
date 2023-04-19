using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Net;

namespace BusinessLayer
{
    public class OffshoreBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _configaration;
        public OffshoreBLL(CommonRepo commonRepo, WaltCapitalDBContext dBContext, IConfiguration configaration)
        {
            _commonRepo = commonRepo;
            _dBContext = dBContext;
            _configaration = configaration;
        }

        public CommonResponse GetOffshoreClientList(GetOffshoreClientReqDTO getOffshoreClientReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                int ClientAccessCategoryId = 2; //Clients = 2
                int Page = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:Page").Value);
                int PageSize = Convert.ToInt32(_configaration.GetSection("ByDefaultPagination:PageSize").Value);
                bool OrderBy = Convert.ToBoolean(_configaration.GetSection("ByDefaultPagination:OrderBy").Value);

                Page = getOffshoreClientReqDTO.PageNumber == 0 ? Page : getOffshoreClientReqDTO.PageNumber;
                PageSize = getOffshoreClientReqDTO.PageSize == 0 ? PageSize : getOffshoreClientReqDTO.PageSize;
                if (getOffshoreClientReqDTO.Orderby != null)
                    OrderBy = getOffshoreClientReqDTO.Orderby;

                GetOffshoreClientResDTO getOffshoreClientResDTO = new GetOffshoreClientResDTO();

                List<OffShoreClient> offShoreClientLists = (from F in _commonRepo.fundList().Where(x => x.IsActive == true && x.Currency == getOffshoreClientReqDTO.Currency)
                                                            join C in _commonRepo.clientTransactionList()
                                                            on F.Id equals C.Fund
                                                            join U in _commonRepo.getUserList()
                                                            on C.Client equals U.Id
                                                            select new { F, C, U }).Select(x => new OffShoreClient
                                                            {
                                                                Id = x.U.Id,
                                                                FirstName = x.U.FirstName,
                                                                LastName = x.U.LastName,
                                                                AccountNo = x.U.ClientAccNo,
                                                                Client = x.C.Client,
                                                                Currency = x.F.Currency,
                                                                AccountValue = x.C.AmountBalance,
                                                                TransactionType = x.C.TransactionType,
                                                                TransactionAmount = x.C.TransactionAmount,
                                                                CreatedDate = x.C.UpdatedDate
                                                            }).OrderByDescending(x => x.CreatedDate).ToList();

                var clientIdList = offShoreClientLists.Select(x => x.Client).Distinct();
                List<OffShoreClient> finalOffShoreList = new List<OffShoreClient>();
                foreach (var item in clientIdList)
                {
                    var ClientDetail = offShoreClientLists.FirstOrDefault(x => x.Client == item);
                    if (ClientDetail != null)
                    {
                        OffShoreClient client = new OffShoreClient();
                        client.Id = ClientDetail.Id;
                        client.FirstName = ClientDetail.FirstName;
                        client.LastName = ClientDetail.LastName;
                        client.AccountNo = ClientDetail.AccountNo;
                        decimal AmountBalance = 0;
                        var ClientTransactionList = offShoreClientLists.Where(x => x.Client == item).ToList();
                        if (ClientTransactionList.Count > 0)
                        {
                            foreach (var clientTrans in ClientTransactionList)
                            {
                                if (clientTrans.TransactionType.ToLower() == "buy")
                                {
                                    AmountBalance = AmountBalance + Convert.ToDecimal(clientTrans.TransactionAmount);
                                }
                                else
                                {
                                    AmountBalance = AmountBalance - Convert.ToDecimal(clientTrans.TransactionAmount);
                                }
                            }
                        }
                        client.AccountValue = AmountBalance;
                        client.Currency = ClientDetail.Currency;
                        client.CreatedDate = ClientDetail.CreatedDate;
                        finalOffShoreList.Add(client);
                    }
                }

                offShoreClientLists = new List<OffShoreClient>();
                offShoreClientLists = finalOffShoreList;

                if (getOffshoreClientReqDTO.Alphabet != null && !string.IsNullOrEmpty(getOffshoreClientReqDTO.Alphabet))
                {
                    offShoreClientLists = offShoreClientLists.Where(x => x.LastName.ToLower().StartsWith(getOffshoreClientReqDTO.Alphabet.ToLower())).ToList();

                    getOffshoreClientResDTO.TotalCount = offShoreClientLists.Count();
                }
                if (getOffshoreClientReqDTO.SearchString != null && !string.IsNullOrEmpty(getOffshoreClientReqDTO.SearchString))
                {
                    offShoreClientLists = offShoreClientLists.Where(x => x.FirstName.ToLower().Contains(getOffshoreClientReqDTO.SearchString.ToLower()) || x.LastName.ToLower().Contains(getOffshoreClientReqDTO.SearchString.ToLower()) || x.AccountNo.ToLower().Contains(getOffshoreClientReqDTO.SearchString.ToLower())).ToList();

                    getOffshoreClientResDTO.TotalCount = offShoreClientLists.Count();
                }

                getOffshoreClientResDTO.TotalCount = offShoreClientLists.Count();

                if (OrderBy)
                {
                    if (offShoreClientLists.Count <= PageSize)
                    {
                        offShoreClientLists = offShoreClientLists.OrderBy(x => x.CreatedDate).ToList();
                    }
                    else
                    {
                        offShoreClientLists = offShoreClientLists.Skip((Page - 1) * PageSize)
                                .Take(PageSize)
                                .OrderBy(x => x.CreatedDate)
                                .ToList();
                    }
                }
                else
                {
                    if (offShoreClientLists.Count <= PageSize)
                    {
                        offShoreClientLists = offShoreClientLists.OrderByDescending(x => x.CreatedDate).ToList();
                    }
                    else
                    {
                        offShoreClientLists = offShoreClientLists.OrderByDescending(x => x.CreatedDate).Skip((Page - 1) * PageSize)
                            .Take(PageSize)
                            .ToList();
                    }
                }

                decimal count = 0m;
                count = Convert.ToDecimal(offShoreClientLists.Sum(x => x.AccountValue));

                OffShoreClient offShoreClient = new OffShoreClient();
                offShoreClient.FirstName = " ";
                offShoreClient.LastName = " ";
                offShoreClient.AccountNo = "Total Portfolios Value";
                offShoreClient.AccountValue = count;
                if (getOffshoreClientReqDTO.Currency == "USD")
                {
                    offShoreClient.Currency = "USD";
                }
                else if (getOffshoreClientReqDTO.Currency == "ZAR")
                {
                    offShoreClient.Currency = "ZAR";
                }
                else
                {
                    offShoreClient.Currency = "RAND";
                }

                getOffshoreClientResDTO.offShoreClientLists = offShoreClientLists;
                getOffshoreClientResDTO.offShoreClient = offShoreClient;
                if (offShoreClientLists.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getOffshoreClientResDTO.Adapt<GetOffshoreClientResDTO>();
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