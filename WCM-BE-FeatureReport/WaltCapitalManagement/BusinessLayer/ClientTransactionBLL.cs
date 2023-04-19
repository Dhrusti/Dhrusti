using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.Net;
using System.Text.RegularExpressions;
using System.Transactions;

namespace BusinessLayer
{
    public class ClientTransactionBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _configaration;
        private readonly FundCurrentBalanceBLL _fundCurrentBalanceBLL;
        private readonly FundDynamicInputPriceBLL _fundDynamicInputPriceBLL;
        private readonly FundPricingBLL _fundPricingBLL;
        private readonly TransactionDetailBLL _transactionDetailBLL;
        private readonly FundFeeCalculationDetailBLL _fundFeeCalculationDetailBLL;

        public ClientTransactionBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, IConfiguration configuration, CommonHelper commonHelper, FundCurrentBalanceBLL fundCurrentBalanceBLL, FundDynamicInputPriceBLL fundDynamicInputPriceBLL, FundPricingBLL fundPricingBLL, TransactionDetailBLL transactionDetailBLL, FundFeeCalculationDetailBLL fundFeeCalculationDetailBLL)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _configaration = configuration;
            _commonHelper = commonHelper;
            _fundCurrentBalanceBLL = fundCurrentBalanceBLL;
            _fundDynamicInputPriceBLL = fundDynamicInputPriceBLL;
            _fundPricingBLL = fundPricingBLL;
            _transactionDetailBLL = transactionDetailBLL;
            _fundFeeCalculationDetailBLL = fundFeeCalculationDetailBLL;
        }

        public CommonResponse GetAllClientTransactionByFundId(GetAllClientTransactionByFundIdReqDTO getAllClientTansactionByFundIdReqDTO)
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


            int number = getAllClientTansactionByFundIdReqDTO.PageNumber == 0 ? (pagecount) : getAllClientTansactionByFundIdReqDTO.PageNumber;
            int size = getAllClientTansactionByFundIdReqDTO.PageSize == 0 ? (pageSize) : getAllClientTansactionByFundIdReqDTO.PageSize;
            bool orderby = getAllClientTansactionByFundIdReqDTO.Orderby == true ? (order) : getAllClientTansactionByFundIdReqDTO.Orderby;

            try
            {
                if (getAllClientTansactionByFundIdReqDTO.FundId != 0)
                {
                    GetAllClientTransactionResDTO getAllClientTransaction = new GetAllClientTransactionResDTO();
                    List<ClientTransactionDetails> clientTransactionDetails = new List<ClientTransactionDetails>();
                    if (getAllClientTansactionByFundIdReqDTO.UnitType.ToLower() != "all")
                    {

                        clientTransactionDetails = (from ctm in _commonRepo.clientTransactionList().Where(x => x.UnitType.ToLower() == getAllClientTansactionByFundIdReqDTO.UnitType.ToLower() && x.Fund == getAllClientTansactionByFundIdReqDTO.FundId)
                                                    join clientUl in _commonRepo.getUserList() on ctm.Client equals clientUl.Id
                                                    into clientUlTemp
                                                    from clientUlFinal in clientUlTemp.DefaultIfEmpty()
                                                    join fundUl in _commonRepo.getUserList() on ctm.Ifa equals fundUl.Id into fundUlTemp
                                                    from fundUlFinal in fundUlTemp.DefaultIfEmpty()
                                                    join fm in _commonRepo.fundList() on ctm.Fund equals fm.Id into fmTemp
                                                    from fmFinal in fmTemp.DefaultIfEmpty()
                                                    select new ClientTransactionDetails
                                                    {
                                                        TransactionNo = ctm.Id,
                                                        FundId = ctm.Fund,
                                                        InvestorNo = clientUlFinal.ClientAccNo,
                                                        FundName = fmFinal.FundName,
                                                        Client = ctm.Client,
                                                        ClientName = clientUlFinal.FirstName + " " + clientUlFinal.LastName,
                                                        Ifa = ctm.Ifa,
                                                        IFAName = fundUlFinal.FirstName + " " + fundUlFinal.LastName,
                                                        IfaupFrontFee = ctm.IfaupFrontFee,
                                                        IfaAnnualFee = ctm.IfaAnnualFee,
                                                        Buy = ctm.TransactionType == "Buy" ? Convert.ToString(ctm.TransactionAmount) : "0",
                                                        Sell = ctm.TransactionType == "Sell" ? Convert.ToString(ctm.TransactionAmount) : "0",
                                                        TransactionIn = ctm.TransactionIn,
                                                        TransactionDate = ctm.TransactionDate,
                                                        NumberOfUnits = ctm.NumberOfUnits,
                                                        UnitPrice = ctm.UnitPrice,
                                                        AllocateTo = ctm.AllocateTo,
                                                        UnitType = ctm.UnitType,
                                                        //IsDeleteIcon = ctm.TransactionDate.Date == DateTime.Now.Date ? true : false
                                                        IsDeleteIcon = DateTime.Now.Date.AddYears(-1) >= ctm.TransactionDate.Date ? false : true
                                                    }).ToList();

                        getAllClientTransaction.TotalCount = clientTransactionDetails.Count;
                    }
                    else
                    {
                        clientTransactionDetails = (from ctm in _commonRepo.clientTransactionList().Where(x => x.Fund == getAllClientTansactionByFundIdReqDTO.FundId)
                                                    join clientUl in _commonRepo.getUserList() on ctm.Client equals clientUl.Id
                                                    //into clientUlTemp
                                                    //from clientUlFinal in clientUlTemp.DefaultIfEmpty()
                                                    join fundUl in _commonRepo.getUserList() on ctm.Ifa equals fundUl.Id into fundUlTemp
                                                    from fundUlFinal in fundUlTemp.DefaultIfEmpty()
                                                    join fm in _commonRepo.fundList() on ctm.Fund equals fm.Id into fmTemp
                                                    from fmFinal in fmTemp.DefaultIfEmpty()
                                                    select new ClientTransactionDetails
                                                    {
                                                        TransactionNo = ctm.Id,
                                                        FundId = ctm.Fund,
                                                        InvestorNo = clientUl.ClientAccNo,
                                                        FundName = fmFinal.FundName,
                                                        Client = ctm.Client,
                                                        ClientName = clientUl.FirstName + " " + clientUl.LastName,
                                                        Ifa = ctm.Ifa,
                                                        IFAName = fundUlFinal.FirstName + " " + fundUlFinal.LastName,
                                                        IfaupFrontFee = ctm.IfaupFrontFee,
                                                        IfaAnnualFee = ctm.IfaAnnualFee,
                                                        Buy = ctm.TransactionType == "Buy" ? Convert.ToString(ctm.TransactionAmount) : "0",
                                                        Sell = ctm.TransactionType == "Sell" ? Convert.ToString(ctm.TransactionAmount) : "0",
                                                        TransactionIn = ctm.TransactionIn,
                                                        TransactionDate = ctm.TransactionDate,
                                                        NumberOfUnits = ctm.NumberOfUnits,
                                                        UnitPrice = ctm.UnitPrice,
                                                        AllocateTo = ctm.AllocateTo,
                                                        UnitType = ctm.UnitType,
                                                        //IsDeleteIcon = ctm.TransactionDate.Date == DateTime.Now.Date ? true : false
                                                        IsDeleteIcon = DateTime.Now.Date.AddYears(-1) >= ctm.TransactionDate.Date ? false : true
                                                    }).ToList();

                        getAllClientTransaction.TotalCount = clientTransactionDetails.Count;

                    }
                    if (orderby)
                    {
                        if (clientTransactionDetails.Count <= size)
                        {
                            clientTransactionDetails = clientTransactionDetails.OrderBy(x => x.TransactionNo).ToList();
                        }
                        else
                        {

                            clientTransactionDetails = clientTransactionDetails.Skip((number - 1) * size)
                               .Take(size)
                               .OrderBy(x => x.TransactionNo)
                               .ToList();
                        }
                    }
                    else
                    {
                        if (clientTransactionDetails.Count <= size)
                        {
                            clientTransactionDetails = clientTransactionDetails.OrderByDescending(x => x.TransactionNo).ToList();
                        }
                        else
                        {
                            clientTransactionDetails = clientTransactionDetails.OrderByDescending(x => x.TransactionNo).Skip((number - 1) * size)
                                .Take(size)
                                .ToList();
                        }
                    }


                    getAllClientTransaction.ClientTransactionList = clientTransactionDetails;


                    if (clientTransactionDetails.Count > 0)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Success.";
                        commonResponse.Data = getAllClientTransaction;
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data not Found.";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Please Enter Fund";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        //public CommonResponse AddClientTransaction(AddClientTransactionReqDTO addClientTransactionReqDTO)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    AddClientTransactionResDTO addClientResTransactionResDTO = new AddClientTransactionResDTO();
        //    bool restrictTransaction = true;
        //    bool ValidTransactionAmount = true;
        //    bool ValidTransactionUnit = true;
        //    try
        //    {
        //        var fund = _commonRepo.fundList().Where(x => x.Id == addClientTransactionReqDTO.Fund).FirstOrDefault();
        //        DateTime CurrentDateTime = DateTime.Now;
        //        if (addClientTransactionReqDTO.TransactionDate.Date >= fund.InceptionDate.Date)
        //        {
        //            if (addClientTransactionReqDTO.TransactionAmount > 0)
        //            {
        //                if (addClientTransactionReqDTO.Client != 0)
        //                {
        //                    //var IsExistClient = _commonRepo.clientTransactionList().Where(x => x.Client == addClientTransactionReqDTO.Client && x.Fund == addClientTransactionReqDTO.Fund && x.TransactionDate.Date<= addClientTransactionReqDTO.TransactionDate.Date).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

        //                    var IsExistClient = _commonRepo.clientTransactionList().Where(x => x.Client == addClientTransactionReqDTO.Client && x.Fund == addClientTransactionReqDTO.Fund && x.TransactionDate.Date <= addClientTransactionReqDTO.TransactionDate.Date).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

        //                    if (IsExistClient == null && addClientTransactionReqDTO.TransactionType.ToLower() == "sell")
        //                    {
        //                        restrictTransaction = false;
        //                    }
        //                    if (IsExistClient != null && addClientTransactionReqDTO.TransactionType.ToLower() == "sell" && (IsExistClient.AmountBalance < Convert.ToDecimal(addClientTransactionReqDTO.TransactionAmount)))
        //                    {
        //                        ValidTransactionAmount = false;
        //                    }
        //                    if (IsExistClient != null && addClientTransactionReqDTO.TransactionType.ToLower() == "sell" && (IsExistClient.UnitBalance < Convert.ToDecimal(addClientTransactionReqDTO.NumberOfUnits)))
        //                    {
        //                        ValidTransactionUnit = false;
        //                    }
        //                    if (restrictTransaction)
        //                    {
        //                        if (ValidTransactionAmount)
        //                        {
        //                            if (ValidTransactionUnit)
        //                            {

        //                                using (var scope = new TransactionScope())
        //                                {
        //                                    if (addClientTransactionReqDTO != null)
        //                                    {
        //                                        ClientTransactionMst clientTransactionMst = new ClientTransactionMst();
        //                                        clientTransactionMst.Fund = addClientTransactionReqDTO.Fund;
        //                                        clientTransactionMst.Client = addClientTransactionReqDTO.Client;
        //                                        clientTransactionMst.Ifa = addClientTransactionReqDTO.Ifa;
        //                                        clientTransactionMst.IfaupFrontFee = addClientTransactionReqDTO.IfaupFrontFee;
        //                                        clientTransactionMst.IfaAnnualFee = addClientTransactionReqDTO.IfaAnnualFee;
        //                                        clientTransactionMst.TransactionType = addClientTransactionReqDTO.TransactionType;
        //                                        clientTransactionMst.TransactionDate = addClientTransactionReqDTO.TransactionDate.Date;
        //                                        clientTransactionMst.TransactionAmount = addClientTransactionReqDTO.TransactionAmount;
        //                                        clientTransactionMst.NumberOfUnits = addClientTransactionReqDTO.NumberOfUnits;
        //                                        clientTransactionMst.UnitPrice = addClientTransactionReqDTO.UnitPrice;
        //                                        clientTransactionMst.TransactionIn = addClientTransactionReqDTO.Currency;
        //                                        clientTransactionMst.AllocateTo = addClientTransactionReqDTO.AllocateTo;
        //                                        clientTransactionMst.UnitType = addClientTransactionReqDTO.UnitType;
        //                                        clientTransactionMst.CreatedBy = addClientTransactionReqDTO.CreatedBy;
        //                                        clientTransactionMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                                        clientTransactionMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
        //                                        clientTransactionMst.UpdatedBy = addClientTransactionReqDTO.CreatedBy;
        //                                        clientTransactionMst.IsActive = true;
        //                                        clientTransactionMst.IsDeleted = false;




        //                                        var clientTotalBalance = _commonRepo.clientTransactionList().Where(x => x.Client == clientTransactionMst.Client && x.Fund == clientTransactionMst.Fund).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

        //                                        if (clientTotalBalance != null)
        //                                        {

        //                                            if (addClientTransactionReqDTO.TransactionType.ToLower() == "buy")
        //                                            {
        //                                                if (clientTransactionMst.TransactionIn.ToLower() == "unit")
        //                                                {
        //                                                    clientTransactionMst.UnitBalance = clientTotalBalance.UnitBalance + (Convert.ToDecimal(clientTransactionMst.NumberOfUnits));
        //                                                    clientTransactionMst.AmountBalance = clientTotalBalance.AmountBalance + Convert.ToDecimal(clientTransactionMst.TransactionAmount);
        //                                                    clientTransactionMst.TransactionDateTime = DateTime.Now;
        //                                                }
        //                                                else
        //                                                {
        //                                                    clientTransactionMst.UnitBalance = clientTotalBalance.UnitBalance + Convert.ToDecimal(clientTransactionMst.NumberOfUnits);
        //                                                    clientTransactionMst.AmountBalance = clientTotalBalance.AmountBalance + Convert.ToDecimal(clientTransactionMst.TransactionAmount);
        //                                                    clientTransactionMst.TransactionDateTime = DateTime.Now;
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                if (clientTransactionMst.TransactionIn.ToLower() == "unit")
        //                                                {
        //                                                    clientTransactionMst.UnitBalance = clientTotalBalance.UnitBalance - Convert.ToDecimal(clientTransactionMst.NumberOfUnits);
        //                                                    clientTransactionMst.AmountBalance = clientTotalBalance.AmountBalance - Convert.ToDecimal(clientTransactionMst.TransactionAmount);
        //                                                    clientTransactionMst.TransactionDateTime = DateTime.Now;
        //                                                }
        //                                                else
        //                                                {
        //                                                    clientTransactionMst.AmountBalance = clientTotalBalance.AmountBalance - Convert.ToDecimal(clientTransactionMst.TransactionAmount);
        //                                                    clientTransactionMst.UnitBalance = clientTotalBalance.UnitBalance - Convert.ToDecimal(clientTransactionMst.NumberOfUnits);
        //                                                    clientTransactionMst.TransactionDateTime = DateTime.Now;
        //                                                }
        //                                            }

        //                                        }
        //                                        else
        //                                        {
        //                                            clientTransactionMst.UnitBalance = Convert.ToDecimal(clientTransactionMst.NumberOfUnits);
        //                                            clientTransactionMst.AmountBalance = Convert.ToDecimal(clientTransactionMst.TransactionAmount);
        //                                            clientTransactionMst.TransactionDateTime = DateTime.Now;
        //                                        }

        //                                        _dbContext.ClientTransactionMsts.Add(clientTransactionMst);
        //                                        var result = _dbContext.SaveChanges();
        //                                        //Added By AZ on 29-11-2022----------------------START--------------------------

        //                                        bool transactionScopeStatus = UpdateTransaction(clientTransactionMst.Fund, clientTransactionMst.Client);
        //                                        if (transactionScopeStatus == false)
        //                                        {
        //                                            scope.Dispose();
        //                                            commonResponse.Status = false;
        //                                            //commonResponse.Message = "Transaction can not be executed, Sell date can not be preceeding Buy 
        //                                            commonResponse.Message = "Transaction Not Allow. Other Transaction Impact In Negative Amount.";
        //                                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //                                            return commonResponse;
        //                                        }

        //                                        //Added By AZ on 29-11-2022----------------------END--------------------------

        //                                        //-----------------Added BY NP on 15-12-22----------------START-----------------

        //                                        // Add ClientTransactionDetail
        //                                        // _transactionDetailBLL.AddTransactionDetail(clientTransactionMst.Id);

        //                                        //-----------------Added BY NP on 15-12-22----------------END-----------------

        //                                        //-----------------Added BY NP on 24-11-22----------------START-----------------

        //                                        //// Add / Update Current Balance 
        //                                        //var res = _fundCurrentBalanceBLL.AddUpdateCurrentBalance(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.TransactionDate, 0, clientTransactionMst.AllocateTo, clientTransactionMst.CreatedBy);
        //                                        bool UseCalculatedBalance = _commonRepo.clientTransactionList().Where(x => x.Fund == fund.Id && x.TransactionDate.Date == fund.InceptionDate.Date).ToList().Count() > 1 ? true : false;

        //                                        if (UseCalculatedBalance)
        //                                        {
        //                                            decimal DynamicInputPricingAmount = _commonRepo.GetFundDynamicInputPriceList(fund.Id).OrderByDescending(x => x.BalanceDate.Date).Where(x => x.UnitType.ToLower() == clientTransactionMst.UnitType.ToLower() && x.Label.ToLower() == clientTransactionMst.AllocateTo.ToLower() && x.BalanceDate.Date==clientTransactionMst.TransactionDate.Date).Select(x => x.Value).Sum();

        //                                            DynamicInputPricingAmount = DynamicInputPricingAmount + Convert.ToDecimal(clientTransactionMst.TransactionAmount);
        //                                            // Add / Update Input Pricing Values 
        //                                            _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.AllocateTo, DynamicInputPricingAmount, clientTransactionMst.TransactionDate, clientTransactionMst.CreatedBy,false);
        //                                        }
        //                                        else
        //                                        {
        //                                            // Add / Update Input Pricing Values 
        //                                            _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.AllocateTo, Convert.ToDecimal(clientTransactionMst.TransactionAmount), clientTransactionMst.TransactionDate, clientTransactionMst.CreatedBy,false);
        //                                        }
        //                                        // Add / Update Fund Fees Calculation Details 
        //                                        //var res = _fundFeeCalculationDetailBLL.AddUpdateFeeCalculationDetail(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.TransactionDate, clientTransactionMst.CreatedBy,true);

        //                                        // Update Pricing
        //                                        //var PricingRes = _fundPricingBLL.UpdatePricing(clientTransactionMst.Fund, clientTransactionMst.TransactionDate, clientTransactionMst.UnitType, 0, clientTransactionMst.AllocateTo, clientTransactionMst.CreatedBy);

        //                                        //-----------------Added BY NP on 24-11-22----------------End-----------------
        //                                        //if (PricingRes.Status)
        //                                        //{
        //                                            scope.Complete();

        //                                            addClientResTransactionResDTO.Id = clientTransactionMst.Id;

        //                                            commonResponse.Message = "Client Transaction Added Successfully!";
        //                                            commonResponse.Status = true;
        //                                            commonResponse.StatusCode = HttpStatusCode.OK;
        //                                            commonResponse.Data = addClientResTransactionResDTO;
        //                                        //}
        //                                        //else
        //                                        //{
        //                                        //    commonResponse = PricingRes;
        //                                        //}
        //                                    }
        //                                    else
        //                                    {
        //                                        commonResponse.Status = false;
        //                                        commonResponse.Message = "Can Not Found The Transaction.";
        //                                        commonResponse.StatusCode = HttpStatusCode.BadRequest;

        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {

        //                                if (IsExistClient.UnitBalance > 0)
        //                                {
        //                                    commonResponse.Status = false;
        //                                    commonResponse.Message = "TransactionUnit Must Be Lower Than " + IsExistClient.UnitBalance;
        //                                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //                                }
        //                                else
        //                                {
        //                                    commonResponse.Status = false;
        //                                    commonResponse.Message = "Not Sufficient Balance For Sell On This Date!";
        //                                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //                                }

        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (IsExistClient.AmountBalance > 0)
        //                            {
        //                                commonResponse.Status = false;
        //                                commonResponse.Message = "TransactionAmount Must Be Lower Than " + IsExistClient.AmountBalance;
        //                                commonResponse.StatusCode = HttpStatusCode.BadRequest;

        //                            }
        //                            else

        //                            {
        //                                commonResponse.Status = false;
        //                                commonResponse.Message = "Not Sufficient Balance For Sell On This Date!";
        //                                commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //                            }

        //                        }
        //                    }
        //                    else
        //                    {
        //                        commonResponse.Status = false;
        //                        commonResponse.Message = "Transaction Not Allowed!";
        //                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //                    }
        //                }
        //                else
        //                {
        //                    commonResponse.Status = false;
        //                    commonResponse.Message = "Please Select Client!";
        //                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //                }
        //            }
        //            else
        //            {
        //                commonResponse.Message = "Please Enter Valid Transaction Amount!";
        //                commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //            }
        //        }
        //        else
        //        {
        //            commonResponse.Message = "Transaction Date Must Be Greater Than Fund Inception Date.";
        //            commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return commonResponse;
        //}

        public CommonResponse UpdateClientTransaction(UpdateClientTransactionReqDTO updateClientTransactionReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateClientTransactionResDTO updateClientResTransactionResDTO = new UpdateClientTransactionResDTO();
            try
            {
                var clientTransaction = _commonRepo.clientTransactionList().FirstOrDefault(x => x.Id == updateClientTransactionReqDTO.Id);
                if (clientTransaction != null)
                {
                    //clientTransaction.fund = updateClientTransactionReqDTO.FundName;
                    //clientTransaction.Client = updateClientTransactionReqDTO.Client;
                    clientTransaction.Ifa = updateClientTransactionReqDTO.Ifa;
                    clientTransaction.IfaupFrontFee = updateClientTransactionReqDTO.IfaupFrontFee;
                    clientTransaction.IfaAnnualFee = updateClientTransactionReqDTO.IfaAnnualFee;
                    //clientTransaction.TransactionType= updateClientTransactionReqDTO.BuySell;
                    clientTransaction.TransactionDate = updateClientTransactionReqDTO.TransactionDate;
                    clientTransaction.TransactionAmount = updateClientTransactionReqDTO.TransactionAmount;
                    clientTransaction.NumberOfUnits = updateClientTransactionReqDTO.NumberOfUnits;
                    // clientTransaction.UnitPrice = updateClientTransactionReqDTO.UnitPrice;
                    clientTransaction.UnitType = updateClientTransactionReqDTO.UnitType;
                    clientTransaction.AllocateTo = updateClientTransactionReqDTO.AllocateTo;
                    clientTransaction.UpdatedBy = updateClientTransactionReqDTO.UpdatedBy;
                    clientTransaction.UpdatedDate = DateTime.UtcNow;



                    var clientTotalBalance = _commonRepo.clientTransactionList().Where(x => x.Client == clientTransaction.Client).OrderBy(x => x.TransactionDateTime).LastOrDefault();
                    if (clientTotalBalance != null)
                    {

                        if ((clientTransaction.TransactionType).ToLower() == "buy")
                        {
                            if (clientTransaction.TransactionIn.ToLower() == "unit")
                            {
                                clientTransaction.UnitBalance = clientTotalBalance.UnitBalance + Convert.ToDecimal(clientTransaction.NumberOfUnits);
                                clientTransaction.AmountBalance = clientTotalBalance.AmountBalance + Convert.ToDecimal(clientTransaction.TransactionAmount);
                                clientTransaction.TransactionDateTime = DateTime.Now;
                            }
                            else
                            {
                                clientTransaction.UnitBalance = clientTotalBalance.UnitBalance + Convert.ToDecimal(clientTransaction.NumberOfUnits);
                                clientTransaction.AmountBalance = clientTotalBalance.AmountBalance + Convert.ToDecimal(clientTransaction.TransactionAmount);
                                clientTransaction.TransactionDateTime = DateTime.Now;
                            }
                        }
                        else
                        {
                            if (clientTransaction.TransactionIn.ToLower() == "unit")
                            {
                                clientTransaction.UnitBalance = System.Math.Abs(
                                    Convert.ToDecimal(clientTotalBalance.UnitBalance - Convert.ToDecimal(clientTransaction.NumberOfUnits)));
                                clientTransaction.AmountBalance = System.Math.Abs(Convert.ToDecimal(clientTotalBalance.AmountBalance - Convert.ToDecimal(clientTransaction.TransactionAmount)));
                                clientTransaction.TransactionDateTime = DateTime.Now;
                            }
                            else
                            {
                                clientTransaction.AmountBalance = System.Math.Abs(Convert.ToDecimal(clientTotalBalance.AmountBalance - Convert.ToDecimal(clientTransaction.TransactionAmount)));
                                clientTransaction.UnitBalance = System.Math.Abs(Convert.ToDecimal(clientTotalBalance.UnitBalance - Convert.ToDecimal(clientTransaction.NumberOfUnits)));
                                clientTransaction.TransactionDateTime = DateTime.Now;
                            }
                        }

                    }
                    else
                    {
                        clientTransaction.UnitBalance = Convert.ToDecimal(clientTransaction.NumberOfUnits);
                        clientTransaction.AmountBalance = Convert.ToDecimal(clientTransaction.TransactionAmount);
                        clientTransaction.TransactionDateTime = DateTime.Now;
                    }
                    _dbContext.Entry(clientTransaction).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    updateClientResTransactionResDTO.Id = clientTransaction.Id;
                    updateClientResTransactionResDTO.IsUpdated = true;

                    commonResponse.Message = "Successfully Updated";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = updateClientResTransactionResDTO;

                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.Message = "Can not update the data!";
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                }


            }
            catch (Exception)
            {
                throw;
            }

            return commonResponse;
        }

        public CommonResponse GetFundForCTByFundId(GetFundForCTByFundIdReqDTO getFundForCTByFundIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetFundForCTByFundIdResDTO getFundForCTByFundIdResDTO = new GetFundForCTByFundIdResDTO();
                var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == getFundForCTByFundIdReqDTO.FundId);
                if (FundDetail != null)
                {
                    getFundForCTByFundIdResDTO.allocatedLists = new List<string>();
                    var PricingInputList = FundDetail.PricingInputs.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
                    if (PricingInputList.Count > 0)
                    {
                        getFundForCTByFundIdResDTO.allocatedLists = PricingInputList;
                    }
                    getFundForCTByFundIdResDTO.UnitStartingPrice = FundDetail.UnitStartingPrice;
                    getFundForCTByFundIdResDTO.FundName = FundDetail.FundName;
                    getFundForCTByFundIdResDTO.Currency = FundDetail.Currency;


                    var unitType = new List<string>();
                    var unitType1 = new List<string>();
                    unitType.Add("Unit A");
                    unitType.Add("Unit B");

                    var dynamicmstList = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == getFundForCTByFundIdReqDTO.FundId && (x.Label.ToLower().Contains("management fee") || x.Label.ToLower().Contains("performance fee"))).Select(x => x.Label).Distinct().ToList();
                    var dynamicmstList1 = dynamicmstList.Distinct().ToList();
                    if (dynamicmstList.Count > 0)
                    {

                        foreach (var dynamicmst in dynamicmstList)
                        {
                            string label = Convert.ToString(dynamicmst).ToLower();
                            if (label.Contains("management fee"))
                            {
                                unitType.Add(Regex.Replace(dynamicmst, "management fee", string.Empty, RegexOptions.IgnoreCase).Trim());
                            }
                            else if (label.Contains("performance fee"))
                            {
                                unitType.Add(Regex.Replace(dynamicmst, "performance fee", string.Empty, RegexOptions.IgnoreCase).Trim());
                            }
                        }
                    }

                    unitType1 = unitType.Distinct().ToList();
                    getFundForCTByFundIdResDTO.UnitType = unitType1;

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Transaction Data Found.";
                    commonResponse.Data = getFundForCTByFundIdResDTO;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Can Not Found The Transaction.";
                    commonResponse.Data = getFundForCTByFundIdResDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllIFAbyClientId(GetAllIFAByClientIdReqDTO getAllIFAByClientIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetAllIFAByClientIdResDTO> ifaList = new List<GetAllIFAByClientIdResDTO>();
                var user = _commonRepo.getUserList().FirstOrDefault(x => x.Id == getAllIFAByClientIdReqDTO.ClientId && x.AccessCategoryId == 2);
                if (user != null)
                {
                    ifaList = _commonRepo.getUserList().Where(x => x.Id == user.Ifa).Select(x => new GetAllIFAByClientIdResDTO
                    {
                        Id = x.Id,
                        IFA = x.FirstName + " " + x.LastName,

                    }).ToList();
                    if (ifaList.Count > 0)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Transaction Data Found.";
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Can Not Found The Transaction.";
                    }
                    commonResponse.Data = ifaList.Adapt<List<GetAllIFAByClientIdResDTO>>();
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Can Not Found The Transaction.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetByClientTransactionId(GetByClientTransactionIdReqDTO getByClientTransactionIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetByClientTransactionIdResDTO getByClientTransactionIdResDTO = new GetByClientTransactionIdResDTO();
                getByClientTransactionIdResDTO = _commonRepo.clientTransactionList().Where(x => x.Id == getByClientTransactionIdReqDTO.ClientTransactionId).FirstOrDefault().Adapt<GetByClientTransactionIdResDTO>();

                if (getByClientTransactionIdResDTO != null)
                {
                    commonResponse.Message = "Transaction Data Found";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getByClientTransactionIdResDTO;
                }
                else
                {
                    commonResponse.Message = "Can Not Found The Transaction.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        //        public CommonResponse DeleteClientTransaction(DeleteClientTransactionReqDTO deleteClientTransactionReqDTO)
        //        {
        //            CommonResponse commonResponse = new CommonResponse();
        //            DeleteClientTransactionResDTO deleteClientTransactionResDTO = new DeleteClientTransactionResDTO();
        //            try
        //            {
        //                using (var scope = new TransactionScope())
        //                {
        //                    DateTime CurrentDateTime = DateTime.Now;
        //                    var IsExistclientTransaction = _commonRepo.clientTransactionList().Where(x => x.Id == deleteClientTransactionReqDTO.Id && x.TransactionDate.Date == CurrentDateTime.Date).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

        //                    bool isScopeDis = true;

        //                    if (IsExistclientTransaction != null)
        //                    {
        //                        var clientTransaction = _commonRepo.clientTransactionList().Where(x => x.TransactionDate.Date == CurrentDateTime.Date).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

        //                        if (clientTransaction != null)
        //                        {

        //                            var client = _commonRepo.clientTransactionList().Where(x => x.Client == IsExistclientTransaction.Client && x.Id == deleteClientTransactionReqDTO.Id && x.TransactionDate.Date == CurrentDateTime.Date).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

        //                            if (client != null)
        //                            {

        //                                IsExistclientTransaction.Id = deleteClientTransactionReqDTO.Id;
        //                                IsExistclientTransaction.UpdatedBy = deleteClientTransactionReqDTO.UpdatedBy;
        //                                IsExistclientTransaction.IsDeleted = true;
        //                                IsExistclientTransaction.UpdatedDate = DateTime.UtcNow;

        //                                _dbContext.Entry(clientTransaction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //                                _dbContext.SaveChanges();
        //                                //Added By AZ on 29-11-2022-----START--------   update all clienttransaction
        //                                bool transactionScopeStatu = UpdateTransaction(IsExistclientTransaction.Fund, IsExistclientTransaction.Client);
        //                                //Added By AZ on 29-11-2022-----END--------
        //                                var maxValue = _commonRepo.clientTransactionList().Max(x => x.Id);
        //                                //var result = _commonRepo.clientTransactionList().First(x => x.Id == maxValue);
        //                                var existclient = _commonRepo.clientTransactionList().Where(x => x.Id > IsExistclientTransaction.Id).ToList();
        //                                isScopeDis = transactionScopeStatu;
        //                               /* if (existclient.Count > 0)
        //                                {

        //                                    var clientTransaction1 = _commonRepo.clientTransactionList().OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).Where(x => x.Client == IsExistclientTransaction.Client && x.Fund == IsExistclientTransaction.Fund && x.TransactionDate.Date == CurrentDateTime.Date).ToList();

        //                                    if (clientTransaction1.Count > 0)
        //                                    {
        //                                        var CTM = _commonRepo.clientTransactionList().Where(x => x.Client == IsExistclientTransaction.Client && x.Fund == IsExistclientTransaction.Fund).ToList();
        //                                        decimal lastUnitBalance = 0.0m;
        //                                        decimal lastAmountBalance = 0.0m;

        //                                        var transactionBeforedelete = _commonRepo.clientTransactionList().Where(x => x.Client == (IsExistclientTransaction.Client) && x.Fund == IsExistclientTransaction.Fund && x.Id < deleteClientTransactionReqDTO.Id).OrderBy(x => x.TransactionDate).Take(1).FirstOrDefault();
        //                                        if (transactionBeforedelete != null)
        //                                        {
        //                                            lastAmountBalance = Convert.ToDecimal(transactionBeforedelete.AmountBalance);
        //                                            lastUnitBalance = Convert.ToDecimal(transactionBeforedelete.UnitBalance);
        //                                        }
        //                                        foreach (var item in CTM)
        //                                        {

        //                                            var res2 = item;

        //                                            foreach (var item2 in existclient)
        //                                            {
        //                                                if (res2.Id == item2.Id)
        //                                                {
        //                                                    if (res2.TransactionType.ToLower() == "buy")
        //                                                    {
        //                                                        if (res2.TransactionIn.ToLower() == "unit")
        //                                                        {
        //                                                            res2.UnitBalance = lastUnitBalance + (Convert.ToDecimal(res2.NumberOfUnits));
        //                                                            res2.AmountBalance = lastAmountBalance + Convert.ToDecimal(res2.TransactionAmount);
        //                                                            res2.TransactionDateTime = DateTime.Now;
        //                                                            _dbContext.Entry(res2).State = EntityState.Modified;
        //                                                            _dbContext.SaveChanges();
        //                                                        }
        //                                                        else
        //                                                        {
        //                                                            res2.UnitBalance = lastUnitBalance + Convert.ToDecimal(res2.NumberOfUnits);
        //                                                            res2.AmountBalance = lastAmountBalance + Convert.ToDecimal(res2.TransactionAmount);
        //                                                            res2.TransactionDateTime = DateTime.Now;
        //                                                            _dbContext.Entry(res2).State = EntityState.Modified;
        //                                                            _dbContext.SaveChanges();
        //                                                        }
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        if (res2.TransactionIn.ToLower() == "unit")
        //                                                        {
        //                                                            res2.UnitBalance = lastUnitBalance - Convert.ToDecimal(res2.NumberOfUnits);
        //                                                            res2.AmountBalance = lastAmountBalance - Convert.ToDecimal(res2.TransactionAmount);
        //                                                            res2.TransactionDateTime = DateTime.Now;
        //                                                            _dbContext.Entry(res2).State = EntityState.Modified;
        //                                                            _dbContext.SaveChanges();
        //                                                            if (res2.UnitBalance < 0)
        //                                                            {
        //                                                                scope.Dispose();
        //                                                                isScopeDis = false;
        //                                                            }

        //                                                        }
        //                                                        else
        //                                                        {
        //                                                            res2.AmountBalance = lastAmountBalance - Convert.ToDecimal(res2.TransactionAmount);
        //                                                            res2.UnitBalance = lastUnitBalance - Convert.ToDecimal(res2.NumberOfUnits);
        //                                                            res2.TransactionDateTime = DateTime.Now;
        //                                                            _dbContext.Entry(res2).State = EntityState.Modified;
        //                                                            _dbContext.SaveChanges();
        //                                                            if (res2.UnitBalance < 0)
        //                                                            {
        //                                                                isScopeDis = false;
        //                                                                scope.Dispose();
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                                lastAmountBalance = Convert.ToDecimal(res2.AmountBalance);
        //                                                lastUnitBalance = Convert.ToDecimal(res2.UnitBalance);

        //                                            }

        //                                            #region update client
        //                                            *//*
        //                                            var index = CTM.IndexOf(item);

        //                                            if (index != 0)
        //                                            {
        //                                                ClientTransactionMst clientTotalBalance = _commonRepo.clientTransactionList().Where(x => x.Client == (res2.Client) && x.Fund == clientTransaction.Fund).OrderBy(x => x.TransactionDate).Skip(index - 1).Take(1).FirstOrDefault() ?? new ClientTransactionMst();

        //                                                if (clientTotalBalance != null)
        //                                                {
        //                                                    foreach (var item2 in existclient)
        //                                                    {
        //                                                        if (res2.Id == item2.Id)
        //                                                        {
        //                                                            if (res2.TransactionType.ToLower() == "buy")
        //                                                            {
        //                                                                if (res2.TransactionIn.ToLower() == "unit")
        //                                                                {
        //                                                                    res2.UnitBalance = clientTotalBalance.UnitBalance + (Convert.ToDecimal(res2.NumberOfUnits));
        //                                                                    res2.AmountBalance = clientTotalBalance.AmountBalance + Convert.ToDecimal(res2.TransactionAmount);
        //                                                                    res2.TransactionDateTime = DateTime.Now;
        //                                                                    _dbContext.Entry(res2).State = EntityState.Modified;
        //                                                                    _dbContext.SaveChanges();
        //                                                                }
        //                                                                else
        //                                                                {
        //                                                                    res2.UnitBalance = clientTotalBalance.UnitBalance + Convert.ToDecimal(res2.NumberOfUnits);
        //                                                                    res2.AmountBalance = clientTotalBalance.AmountBalance + Convert.ToDecimal(res2.TransactionAmount);
        //                                                                    res2.TransactionDateTime = DateTime.Now;
        //                                                                    _dbContext.Entry(res2).State = EntityState.Modified;
        //                                                                    _dbContext.SaveChanges();
        //                                                                }
        //                                                            }
        //                                                            else
        //                                                            {
        //                                                                if (res2.TransactionIn.ToLower() == "unit")
        //                                                                {
        //                                                                    res2.UnitBalance = clientTotalBalance.UnitBalance - Convert.ToDecimal(res2.NumberOfUnits);
        //                                                                    res2.AmountBalance = clientTotalBalance.AmountBalance - Convert.ToDecimal(res2.TransactionAmount);
        //                                                                    res2.TransactionDateTime = DateTime.Now;
        //                                                                    _dbContext.Entry(res2).State = EntityState.Modified;
        //                                                                    _dbContext.SaveChanges();
        //                                                                }
        //                                                                else
        //                                                                {
        //                                                                    res2.AmountBalance = clientTotalBalance.AmountBalance - Convert.ToDecimal(res2.TransactionAmount);
        //                                                                    res2.UnitBalance = clientTotalBalance.UnitBalance - Convert.ToDecimal(res2.NumberOfUnits);
        //                                                                    res2.TransactionDateTime = DateTime.Now;
        //                                                                    _dbContext.Entry(res2).State = EntityState.Modified;
        //                                                                    _dbContext.SaveChanges();
        //                                                                }
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                                else
        //                                                {
        //                                                    res2.UnitBalance = Convert.ToDecimal(res2.NumberOfUnits);
        //                                                    res2.AmountBalance = Convert.ToDecimal(res2.TransactionAmount);
        //                                                    res2.TransactionDateTime = DateTime.Now;

        //                                                }
        //                                            }*//*
        //                                        }
        //                                    }
        //                                    #endregion
        //                                }
        //*/                            }
        //                            if (isScopeDis)
        //                            {
        //                                //-----------------Added BY NP on 28-11-22----------------START-----------------

        //                                // Add / Update Current Balance 
        //                                var res = _fundCurrentBalanceBLL.AddUpdateCurrentBalance(client.Fund, client.UnitType, client.TransactionDate, 0, client.AllocateTo, client.CreatedBy);

        //                                // Add / Update Input Pricing Values 
        //                                _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(client.Fund, client.UnitType, client.AllocateTo, res.Data.FundBalance, client.TransactionDate, client.CreatedBy);

        //                                // Add / Update Pricing
        //                                _fundPricingBLL.AddUpdatePricing(client.Fund, client.TransactionDate, client.UnitType, 0, client.AllocateTo, client.CreatedBy);

        //                                //-----------------Added BY NP on 28-11-22----------------End-----------------

        //                                scope.Complete();
        //                                deleteClientTransactionResDTO.Id = IsExistclientTransaction.Id;
        //                                commonResponse.Data = deleteClientTransactionResDTO;
        //                                commonResponse.Status = true;
        //                                commonResponse.StatusCode = HttpStatusCode.OK;
        //                                commonResponse.Message = "Transaction Deleted Successfully!";
        //                            }
        //                            else
        //                            {
        //                                commonResponse.Status = false;
        //                                commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //                                commonResponse.Message = "Can Not Deleted";
        //                            }



        //                        }
        //                        else
        //                        {
        //                            commonResponse.Status = false;
        //                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
        //                            commonResponse.Message = "Only Current Date Transaction Allow!";
        //                        }

        //                    }
        //                    else
        //                    {
        //                        commonResponse.Status = false;
        //                        commonResponse.Message = "Can Not Found The Transaction.";
        //                        commonResponse.StatusCode = HttpStatusCode.BadRequest;

        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                commonResponse.Message = ex.Message;
        //                throw;
        //            }
        //            return commonResponse;
        //        }

        //public CommonResponse GetTranscationTypeByClientId(GetTranscationTypeByClientIdReqDTO getTranscationTypeByClientIdReqDTO)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        GetTranscationTypeByClientIdResDTO getTranscationTypeByClient = new GetTranscationTypeByClientIdResDTO();

        //        var clientTransactionDetails = _commonRepo.clientTransactionList().Where(x => x.Client == getTranscationTypeByClientIdReqDTO.ClientId && x.Fund == getTranscationTypeByClientIdReqDTO.FundId).OrderByDescending(x => x.TransactionDate).FirstOrDefault();

        //        if (clientTransactionDetails != null)
        //        {
        //            getTranscationTypeByClient.IsFirstTransaction = false;
        //            getTranscationTypeByClient.NoOfUnit = Convert.ToDecimal(clientTransactionDetails.UnitBalance);
        //        }
        //        else
        //        {
        //            getTranscationTypeByClient.IsFirstTransaction = true;
        //            getTranscationTypeByClient.NoOfUnit = 0;
        //        }



        //        commonResponse.Status = true;
        //        commonResponse.StatusCode = HttpStatusCode.OK;
        //        commonResponse.Message = "Transaction Data Found.";
        //        commonResponse.Data = getTranscationTypeByClient;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return commonResponse;
        //}

        public CommonResponse GetTranscationTypeByClientId(GetTranscationTypeByClientIdReqDTO getTranscationTypeByClientIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetTranscationTypeByClientIdResDTO getTranscationTypeByClient = new GetTranscationTypeByClientIdResDTO();

                if (getTranscationTypeByClientIdReqDTO.UnitType.ToLower() != "all")
                {
                    var clientTransactionDetails = _commonRepo.clientTransactionList().Where(x => x.Client == getTranscationTypeByClientIdReqDTO.ClientId && x.Fund == getTranscationTypeByClientIdReqDTO.FundId && x.UnitType.ToLower() == getTranscationTypeByClientIdReqDTO.UnitType.ToLower()).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

                    var fundvalue = _commonRepo.fundList().FirstOrDefault(x => x.Id == getTranscationTypeByClientIdReqDTO.FundId);
                    string fundUnitStartingprice = fundvalue != null ? Convert.ToString(fundvalue.UnitStartingPrice) : "";

                    var GetPricingList = _commonRepo.GetPricingList(getTranscationTypeByClientIdReqDTO.FundId);

                    var pricevalue = GetPricingList.Where(x => x.UnitType.ToLower() == getTranscationTypeByClientIdReqDTO.UnitType.ToLower()).OrderByDescending(x => x.TransactionDate).FirstOrDefault();
                    if (clientTransactionDetails != null)
                    {
                        getTranscationTypeByClient.IsFirstTransaction = false;
                        getTranscationTypeByClient.NoOfUnit = Convert.ToDecimal(clientTransactionDetails.UnitBalance);
                        getTranscationTypeByClient.UnitPrice = Convert.ToString(pricevalue != null ? _commonHelper.GetFormatedDecimal(pricevalue.UnitPriceNav) : fundUnitStartingprice);
                    }
                    else
                    {
                        getTranscationTypeByClient.IsFirstTransaction = true;
                        getTranscationTypeByClient.NoOfUnit = 0;
                        getTranscationTypeByClient.UnitPrice = Convert.ToString(pricevalue != null ? _commonHelper.GetFormatedDecimal(pricevalue.UnitPriceNav) : fundUnitStartingprice);
                    }
                }
           
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Message = "Transaction Data Found.";
                commonResponse.Data = getTranscationTypeByClient;

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        private bool UpdateTransaction(int fund, int client , string unittype)
        {
            bool result = true;
            //var FundWiseClientTransationList = _dbContext.ClientTransactionMsts.Where(x => x.IsActive == true && x.IsDeleted == false && x.Fund == fund && x.Client == client).ToList().OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).ToList();   
            var FundWiseClientTransationList = _dbContext.ClientTransactionMsts.Where(x => x.IsActive == true && x.IsDeleted == false && x.Fund == fund && x.Client == client && x.UnitType == unittype).ToList().OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).ToList();
            if (FundWiseClientTransationList.Count > 0)
            {
                decimal unitBal = 0.0m, amtBal = 0.0m;
                foreach (var item in FundWiseClientTransationList)
                {
                    if (item.TransactionType.ToLower() == "buy")
                    {
                        unitBal = unitBal + Convert.ToDecimal(item.NumberOfUnits);
                        amtBal = amtBal + Convert.ToDecimal(item.TransactionAmount);
                        if (unitBal < 0 || amtBal < 0)
                        {
                            result = false;
                            break;
                        }
                        else
                        {
                            item.UnitBalance = unitBal;
                            item.AmountBalance = amtBal;
                        }
                    }
                    else
                    {
                        unitBal = unitBal - Convert.ToDecimal(item.NumberOfUnits);
                        amtBal = amtBal - Convert.ToDecimal(item.TransactionAmount);
                        if (unitBal < 0 || amtBal < 0)
                        {
                            result = false;
                            break;
                        }
                        else
                        {
                            item.UnitBalance = unitBal;
                            item.AmountBalance = amtBal;
                        }
                    }
                    _dbContext.Entry(item).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                }

            }
            return result;
        }

        public CommonResponse DeleteClientTransaction(DeleteClientTransactionReqDTO deleteClientTransactionReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteClientTransactionResDTO deleteClientTransactionResDTO = new DeleteClientTransactionResDTO();
            try
            {
                using (var scope = new TransactionScope())
                {
                    DateTime CurrentDateTime = DateTime.Now;
                    bool IsCompleteTransaction = true;

                    var clientTransactionDetail = _commonRepo.clientTransactionList().FirstOrDefault(x => x.Id == deleteClientTransactionReqDTO.Id);

                    if (clientTransactionDetail != null)
                    {
                        ClientTransactionMst clientTransactionMst = new ClientTransactionMst();
                        clientTransactionMst = clientTransactionDetail;

                        clientTransactionMst.UpdatedBy = deleteClientTransactionReqDTO.UpdatedBy;
                        clientTransactionMst.IsDeleted = true;
                        clientTransactionMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        _dbContext.Entry(clientTransactionMst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _dbContext.SaveChanges();

                        //Added By AZ on 29-11-2022-----START--------   update all clienttransaction
                        IsCompleteTransaction = UpdateTransaction(clientTransactionMst.Fund, clientTransactionMst.Client,clientTransactionMst.UnitType);
                        //Added By AZ on 29-11-2022-----END--------

                        if (IsCompleteTransaction)
                        {
                            //-----------------Added BY NP on 28-11-22----------------START-----------------
                            // Add / Update Current Balance 
                            //var res = _fundCurrentBalanceBLL.AddUpdateCurrentBalance(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.TransactionDate, 0, clientTransactionMst.AllocateTo, clientTransactionMst.CreatedBy);

                            // Add / Update Input Pricing Values 
                            //_fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.AllocateTo, res.Data.FundBalance, clientTransactionMst.TransactionDate, clientTransactionMst.CreatedBy);

                            // Add / Update Pricing
                            //_fundPricingBLL.AddUpdatePricing(clientTransactionMst.Fund, clientTransactionMst.TransactionDate, clientTransactionMst.UnitType, 0, clientTransactionMst.AllocateTo, clientTransactionMst.CreatedBy);
                            //-----------------Added BY NP on 28-11-22----------------End-----------------

                            // -----------------Added BY NP on 23 - 12 - 22----------------Start----------------


                            // Add / Update Fund Fees Calculation Details 
                            //var res = _fundFeeCalculationDetailBLL.AddUpdateFeeCalculationDetail(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.TransactionDate, clientTransactionMst.CreatedBy,true);

                            //// Add / Update Input Pricing Values 
                            //_fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.AllocateTo, res.Data.BankBalance, clientTransactionMst.TransactionDate, clientTransactionMst.CreatedBy,false);

                            //// Add / Update Pricing
                            //_fundPricingBLL.UpdatePricing(clientTransactionMst.Fund, clientTransactionMst.TransactionDate, clientTransactionMst.UnitType, 0, clientTransactionMst.AllocateTo, clientTransactionMst.CreatedBy);
                            // -----------------Added BY NP on 23 - 12 - 22----------------End----------------

                            scope.Complete();
                            deleteClientTransactionResDTO.Id = clientTransactionMst.Id;
                            commonResponse.Data = deleteClientTransactionResDTO;
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Transaction Deleted Successfully!";
                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            //commonResponse.Message = "Can Not Deleted";
                            commonResponse.Message = "Transaction can not be deleted, Total amount value can not be negative!";
                        }
                    }
                    else
                    {
                        commonResponse.Message = "Can Not Found The Transaction!";
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                    }
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                throw;
            }
            return commonResponse;
        }
       
        public CommonResponse GetTranscationUnitTypeByClientId(GetTransactionUnitTypeByClientIdReqDTO getTransactionUnitTypeByClientIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetTranscationTypeByClientIdResDTO getTranscationTypeByClient = new GetTranscationTypeByClientIdResDTO();

                var clientTransactionDetails = _commonRepo.clientTransactionList().Where(x => x.Client == getTransactionUnitTypeByClientIdReqDTO.ClientId && x.Fund == getTransactionUnitTypeByClientIdReqDTO.FundId && x.UnitType.ToLower() == getTransactionUnitTypeByClientIdReqDTO.UnitType.ToLower()).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

                var fundvalue = _commonRepo.fundList().FirstOrDefault(x => x.Id == getTransactionUnitTypeByClientIdReqDTO.FundId);
                decimal fundUnitStartingprice = fundvalue != null ? Convert.ToDecimal(fundvalue.UnitStartingPrice) : 0;

                var GetPricingList = _commonRepo.GetPricingList(getTransactionUnitTypeByClientIdReqDTO.FundId);

                var pricevalue = GetPricingList.Where(x => x.UnitType.ToLower() == getTransactionUnitTypeByClientIdReqDTO.UnitType.ToLower()).OrderByDescending(x => x.TransactionDate).FirstOrDefault();

                if (clientTransactionDetails != null)
                {
                    getTranscationTypeByClient.IsFirstTransaction = false;
                    getTranscationTypeByClient.NoOfUnit = Convert.ToDecimal(clientTransactionDetails.UnitBalance);
                   // getTranscationTypeByClient.UnitPrice = pricevalue != null ? pricevalue.UnitPriceNav : fundUnitStartingprice;
                }
                else
                {
                    getTranscationTypeByClient.IsFirstTransaction = true;
                    getTranscationTypeByClient.NoOfUnit = 0;
                }



                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Message = "Transaction Data Found.";
                commonResponse.Data = getTranscationTypeByClient;

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddClientTransaction(AddClientTransactionReqDTO addClientTransactionReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddClientTransactionResDTO addClientResTransactionResDTO = new AddClientTransactionResDTO();
            bool restrictTransaction = true;
            bool ValidTransactionAmount = true;
            bool ValidTransactionUnit = true;
            try
            {
                var fund = _commonRepo.fundList().Where(x => x.Id == addClientTransactionReqDTO.Fund).FirstOrDefault();
                DateTime CurrentDateTime = DateTime.Now;
                if (addClientTransactionReqDTO.TransactionDate.Date >= fund.InceptionDate.Date)
                {
                    if (addClientTransactionReqDTO.TransactionAmount > 0)
                    {
                        if (addClientTransactionReqDTO.Client != 0)
                        {
                            //var IsExistClient = _commonRepo.clientTransactionList().Where(x => x.Client == addClientTransactionReqDTO.Client && x.Fund == addClientTransactionReqDTO.Fund && x.TransactionDate.Date<= addClientTransactionReqDTO.TransactionDate.Date).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

                            //var IsExistClient = _commonRepo.clientTransactionList().Where(x => x.Client == addClientTransactionReqDTO.Client && x.Fund == addClientTransactionReqDTO.Fund && x.TransactionDate.Date <= addClientTransactionReqDTO.TransactionDate.Date).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

                            var IsExistClient = _commonRepo.clientTransactionList().Where(x => x.Client == addClientTransactionReqDTO.Client && x.Fund == addClientTransactionReqDTO.Fund && x.TransactionDate.Date <= addClientTransactionReqDTO.TransactionDate.Date && x.UnitType == addClientTransactionReqDTO.UnitType).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();
                           
                            if (IsExistClient == null  && addClientTransactionReqDTO.TransactionType.ToLower() == "sell")
                            {
                                restrictTransaction = false;
                            }
                            if (IsExistClient != null && addClientTransactionReqDTO.TransactionType.ToLower() == "sell" && (IsExistClient.AmountBalance < Convert.ToDecimal(addClientTransactionReqDTO.TransactionAmount)))
                            {
                                ValidTransactionAmount = false;
                            }
                            if (IsExistClient != null && addClientTransactionReqDTO.TransactionType.ToLower() == "sell" && (IsExistClient.UnitBalance < Convert.ToDecimal(addClientTransactionReqDTO.NumberOfUnits)))
                            {
                                ValidTransactionUnit = false;
                            }
                            if (restrictTransaction)
                            {
                                if (ValidTransactionAmount)
                                {
                                    if (ValidTransactionUnit)
                                    {

                                        using (var scope = new TransactionScope())
                                        {
                                            if (addClientTransactionReqDTO != null)
                                            {
                                                ClientTransactionMst clientTransactionMst = new ClientTransactionMst();
                                                clientTransactionMst.Fund = addClientTransactionReqDTO.Fund;
                                                clientTransactionMst.Client = addClientTransactionReqDTO.Client;
                                                clientTransactionMst.Ifa = addClientTransactionReqDTO.Ifa;
                                                clientTransactionMst.IfaupFrontFee = addClientTransactionReqDTO.IfaupFrontFee;
                                                clientTransactionMst.IfaAnnualFee = addClientTransactionReqDTO.IfaAnnualFee;
                                                clientTransactionMst.TransactionType = addClientTransactionReqDTO.TransactionType;
                                                clientTransactionMst.TransactionDate = addClientTransactionReqDTO.TransactionDate.Date;
                                                clientTransactionMst.TransactionAmount = addClientTransactionReqDTO.TransactionAmount;
                                                clientTransactionMst.NumberOfUnits = addClientTransactionReqDTO.NumberOfUnits;
                                                clientTransactionMst.UnitPrice = addClientTransactionReqDTO.UnitPrice;
                                                clientTransactionMst.TransactionIn = addClientTransactionReqDTO.Currency;
                                                clientTransactionMst.AllocateTo = addClientTransactionReqDTO.AllocateTo;
                                                clientTransactionMst.UnitType = addClientTransactionReqDTO.UnitType;
                                                clientTransactionMst.CreatedBy = addClientTransactionReqDTO.CreatedBy;
                                                clientTransactionMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                                clientTransactionMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                                clientTransactionMst.UpdatedBy = addClientTransactionReqDTO.CreatedBy;
                                                clientTransactionMst.IsActive = true;
                                                clientTransactionMst.IsDeleted = false;




                                                var clientTotalBalance = _commonRepo.clientTransactionList().Where(x => x.Client == clientTransactionMst.Client && x.Fund == clientTransactionMst.Fund && x.UnitType == clientTransactionMst.UnitType).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).LastOrDefault();

                                                if (clientTotalBalance != null)
                                                {

                                                    if (addClientTransactionReqDTO.TransactionType.ToLower() == "buy")
                                                    {
                                                        if (clientTransactionMst.TransactionIn.ToLower() == "unit")
                                                        {
                                                            clientTransactionMst.UnitBalance = clientTotalBalance.UnitBalance + (Convert.ToDecimal(clientTransactionMst.NumberOfUnits));
                                                            clientTransactionMst.AmountBalance = clientTotalBalance.AmountBalance + Convert.ToDecimal(clientTransactionMst.TransactionAmount);
                                                            clientTransactionMst.TransactionDateTime = DateTime.Now;
                                                        }
                                                        else
                                                        {
                                                            clientTransactionMst.UnitBalance = clientTotalBalance.UnitBalance + Convert.ToDecimal(clientTransactionMst.NumberOfUnits);
                                                            clientTransactionMst.AmountBalance = clientTotalBalance.AmountBalance + Convert.ToDecimal(clientTransactionMst.TransactionAmount);
                                                            clientTransactionMst.TransactionDateTime = DateTime.Now;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (clientTransactionMst.TransactionIn.ToLower() == "unit")
                                                        {
                                                            clientTransactionMst.UnitBalance = clientTotalBalance.UnitBalance - Convert.ToDecimal(clientTransactionMst.NumberOfUnits);
                                                            clientTransactionMst.AmountBalance = clientTotalBalance.AmountBalance - Convert.ToDecimal(clientTransactionMst.TransactionAmount);
                                                            clientTransactionMst.TransactionDateTime = DateTime.Now;
                                                        }
                                                        else
                                                        {
                                                            clientTransactionMst.AmountBalance = clientTotalBalance.AmountBalance - Convert.ToDecimal(clientTransactionMst.TransactionAmount);
                                                            clientTransactionMst.UnitBalance = clientTotalBalance.UnitBalance - Convert.ToDecimal(clientTransactionMst.NumberOfUnits);
                                                            clientTransactionMst.TransactionDateTime = DateTime.Now;
                                                        }
                                                    }

                                                }
                                                else
                                                {
                                                    clientTransactionMst.UnitBalance = Convert.ToDecimal(clientTransactionMst.NumberOfUnits);
                                                    clientTransactionMst.AmountBalance = Convert.ToDecimal(clientTransactionMst.TransactionAmount);
                                                    clientTransactionMst.TransactionDateTime = DateTime.Now;
                                                }

                                                _dbContext.ClientTransactionMsts.Add(clientTransactionMst);
                                                var result = _dbContext.SaveChanges();
                                                //Added By AZ on 29-11-2022----------------------START--------------------------

                                                bool transactionScopeStatus = UpdateTransaction(clientTransactionMst.Fund, clientTransactionMst.Client,clientTransactionMst.UnitType);
                                                if (transactionScopeStatus == false)
                                                {
                                                    scope.Dispose();
                                                    commonResponse.Status = false;
                                                    //commonResponse.Message = "Transaction can not be executed, Sell date can not be preceeding Buy 
                                                    commonResponse.Message = "Transaction Not Allow. Other Transaction Impact In Negative Amount.";
                                                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                                                    return commonResponse;
                                                }

                                                //Added By AZ on 29-11-2022----------------------END--------------------------

                                                //-----------------Added BY NP on 15-12-22----------------START-----------------

                                                // Add ClientTransactionDetail
                                                // _transactionDetailBLL.AddTransactionDetail(clientTransactionMst.Id);

                                                //-----------------Added BY NP on 15-12-22----------------END-----------------

                                                //-----------------Added BY NP on 24-11-22----------------START-----------------

                                                //// Add / Update Current Balance 
                                                //var res = _fundCurrentBalanceBLL.AddUpdateCurrentBalance(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.TransactionDate, 0, clientTransactionMst.AllocateTo, clientTransactionMst.CreatedBy);
                                                bool UseCalculatedBalance = _commonRepo.clientTransactionList().Where(x => x.Fund == fund.Id && x.TransactionDate.Date == fund.InceptionDate.Date).ToList().Count() > 1 ? true : false;

                                                if (UseCalculatedBalance)
                                                {
                                                    decimal DynamicInputPricingAmount = _commonRepo.GetFundDynamicInputPriceList(fund.Id).OrderByDescending(x => x.BalanceDate.Date).Where(x => x.UnitType.ToLower() == clientTransactionMst.UnitType.ToLower() && x.Label.ToLower() == clientTransactionMst.AllocateTo.ToLower() && x.BalanceDate.Date == clientTransactionMst.TransactionDate.Date).Select(x => x.Value).Sum();

                                                    DynamicInputPricingAmount = DynamicInputPricingAmount + Convert.ToDecimal(clientTransactionMst.TransactionAmount);
                                                    // Add / Update Input Pricing Values 
                                                    _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.AllocateTo, DynamicInputPricingAmount, clientTransactionMst.TransactionDate, clientTransactionMst.CreatedBy, false);
                                                }
                                                else
                                                {
                                                    // Add / Update Input Pricing Values 
                                                    _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.AllocateTo, Convert.ToDecimal(clientTransactionMst.TransactionAmount), clientTransactionMst.TransactionDate, clientTransactionMst.CreatedBy, false);
                                                }
                                                // Add / Update Fund Fees Calculation Details 
                                                //var res = _fundFeeCalculationDetailBLL.AddUpdateFeeCalculationDetail(clientTransactionMst.Fund, clientTransactionMst.UnitType, clientTransactionMst.TransactionDate, clientTransactionMst.CreatedBy,true);

                                                // Update Pricing
                                                //var PricingRes = _fundPricingBLL.UpdatePricing(clientTransactionMst.Fund, clientTransactionMst.TransactionDate, clientTransactionMst.UnitType, 0, clientTransactionMst.AllocateTo, clientTransactionMst.CreatedBy);

                                                //-----------------Added BY NP on 24-11-22----------------End-----------------
                                                //if (PricingRes.Status)
                                                //{
                                                scope.Complete();

                                                addClientResTransactionResDTO.Id = clientTransactionMst.Id;

                                                commonResponse.Message = "Client Transaction Added Successfully!";
                                                commonResponse.Status = true;
                                                commonResponse.StatusCode = HttpStatusCode.OK;
                                                commonResponse.Data = addClientResTransactionResDTO;
                                                //}
                                                //else
                                                //{
                                                //    commonResponse = PricingRes;
                                                //}
                                            }
                                            else
                                            {
                                                commonResponse.Status = false;
                                                commonResponse.Message = "Can Not Found The Transaction.";
                                                commonResponse.StatusCode = HttpStatusCode.BadRequest;

                                            }
                                        }
                                    }
                                    else
                                    {

                                        if (IsExistClient.UnitBalance > 0)
                                        {
                                            commonResponse.Status = false;
                                            commonResponse.Message = "TransactionUnit Must Be Lower Than " + IsExistClient.UnitBalance;
                                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                                        }
                                        else
                                        {
                                            commonResponse.Status = false;
                                            commonResponse.Message = "Not Sufficient Balance For Sell On This Date!";
                                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                                        }

                                    }
                                }
                                else
                                {
                                    if (IsExistClient.AmountBalance > 0)
                                    {
                                        commonResponse.Status = false;
                                        commonResponse.Message = "TransactionAmount Must Be Lower Than " + IsExistClient.AmountBalance;
                                        commonResponse.StatusCode = HttpStatusCode.BadRequest;

                                    }
                                    else

                                    {
                                        commonResponse.Status = false;
                                        commonResponse.Message = "Not Sufficient Balance For Sell On This Date!";
                                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                                    }

                                }
                            }
                            else
                            {
                                commonResponse.Status = false;
                                commonResponse.Message = "Transaction Not Allowed!";
                                commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            }
                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.Message = "Please Select Client!";
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        }
                    }
                    else
                    {
                        commonResponse.Message = "Please Enter Valid Transaction Amount!";
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    commonResponse.Message = "Transaction Date Must Be Greater Than Fund Inception Date.";
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
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
    
      //public CommonResponse GetAllClientTansaction(GetAllClientTansactionByFundIdReqDTO getAllClientTansactionByFundIdReqDTO)
//{
//    CommonResponse commonResponse = new CommonResponse();
//    try
//    {
//        SqlParameter[] sqlParameters = new SqlParameter[]{
//            new SqlParameter("@FundId", getAllClientTansactionByFundIdReqDTO.FundId),
//        };

//        var getUserData = _commonHelper.ExecuteCRUDStoreProcedure("[Get_AllClientTansaction]", sqlParameters, false);
//        List<GetAllClientTransactionResDTO> model = new List<GetAllClientTransactionResDTO>();
//        var jsondata = JsonConvert.SerializeObject(getUserData.Data);
//        model = JsonConvert.DeserializeObject<List<GetAllClientTransactionResDTO>>(jsondata);

//        //TransactionNo = ctm.Id,
//        //FundId = ctm.Fund,
//        //InvestorNo = gul.ClientAccNo,
//        //FundName = fm.FundName,
//        //Client = ctm.Client,
//        //ClientName = gul.FirstName + gul.LastName,
//        //Ifa = ctm.Ifa,
//        //IFAName = ifam.FirstName + ifam.LastName,
//        //IfaupFrontFee = ctm.IfaupFrontFee,
//        //IfaAnnualFee = ctm.IfaAnnualFee,
//        //Buy = ctm.TransactionType == "Buy" ? Convert.ToString(ctm.TransactionAmount) : "0",
//        //Sell = ctm.TransactionType == "Sell" ? Convert.ToString(ctm.TransactionAmount) : "0",
//        //TransactionIn = ctm.TransactionIn,
//        //TransactionDate = ctm.TransactionDate,
//        //NumberOfUnits = ctm.NumberOfUnits,
//        //UnitPrice = ctm.UnitPrice,
//        //AllocateTo = ctm.AllocateTo,


//        if (model.Count > 0)
//        {
//            commonResponse.Status = true;
//            commonResponse.StatusCode = HttpStatusCode.OK;
//            commonResponse.Message = "Success.";
//            commonResponse.Data = model.Adapt<List<GetAllClientTransactionResDTO>>();
//        }
//        else
//        {
//            commonResponse.Status = false;
//            commonResponse.StatusCode = HttpStatusCode.NotFound;
//            commonResponse.Message = "Data not Found.";
//        }



//    }
//    catch (Exception)
//    {
//        throw;
//    }
//    return commonResponse;
//}
//public CommonResponse GetFundForCTByFundId(GetFundForCTByFundIdReqDTO getFundForCTByFundIdReqDTO)
//{
//    CommonResponse commonResponse = new CommonResponse();
//    try
//    {
//        GetFundForCTByFundIdResDTO getFundForCTByFundIdResDTO = new GetFundForCTByFundIdResDTO();
//        var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == getFundForCTByFundIdReqDTO.FundId);
//        if (FundDetail != null)
//        {
//            getFundForCTByFundIdResDTO.allocatedLists = new List<string>();
//            var PricingInputList = FundDetail.PricingInputs.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToList();
//            if (PricingInputList.Count > 0)
//            {
//                getFundForCTByFundIdResDTO.allocatedLists = PricingInputList;
//            }
//            getFundForCTByFundIdResDTO.UnitStartingPrice = FundDetail.UnitStartingPrice;
//            getFundForCTByFundIdResDTO.FundName = FundDetail.FundName;
//            getFundForCTByFundIdResDTO.Currency = FundDetail.Currency;

//            commonResponse.Status = true;
//            commonResponse.StatusCode = HttpStatusCode.OK;
//            commonResponse.Message = "Success.";
//            commonResponse.Data = getFundForCTByFundIdResDTO;
//        }
//        else
//        {
//            commonResponse.StatusCode = HttpStatusCode.NotFound;
//            commonResponse.Message = "Record not Found.";
//            commonResponse.Data = getFundForCTByFundIdResDTO;
//        }
//    }
//    catch (Exception)
//    {
//        throw;
//    }
//    return commonResponse;
//}