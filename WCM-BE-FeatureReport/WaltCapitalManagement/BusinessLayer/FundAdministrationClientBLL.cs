using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace BusinessLayer
{
    public class FundAdministrationClientBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _configuration;

        public FundAdministrationClientBLL(WaltCapitalDBContext waltCapitalDBContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration, WaltCapitalDBContext dBContext)
        {
            _dbContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configuration = configuration;
        }





        public CommonResponse GetAllFundAdministrationClient(GetFundAdministrationClientReqDTO getFundAdministrationClientReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            GetFundAdministrationClientResDTO getFundAdministrationClientResDTO = new GetFundAdministrationClientResDTO();

            int number = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:Page").Value);
            int size = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:PageSize").Value);
            bool orderby = Convert.ToBoolean(_configuration.GetSection("ByDefaultPagination:OrderBy").Value);

            number = getFundAdministrationClientReqDTO.PageNumber == 0 ? number : getFundAdministrationClientReqDTO.PageNumber;
            size = getFundAdministrationClientReqDTO.PageSize == 0 ? size : getFundAdministrationClientReqDTO.PageSize;
            orderby = getFundAdministrationClientReqDTO.Orderby;

            try
            {
                if (getFundAdministrationClientReqDTO.FundId != 0)
                {
                    getFundAdministrationClientResDTO.FundAdministrationClientList = new List<FundAdministrationClient>();

                    //Nikunj ---start
                    var UserList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == 2); //AccessCategoryId=2=Client
                    /*var ClientTransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == getFundAdministrationClientReqDTO.FundId && x.UnitType == getFundAdministrationClientReqDTO.UnitType).ToList();

                    if (getFundAdministrationClientReqDTO.UnitType.ToLower() == "all")
                    {
                        ClientTransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == getFundAdministrationClientReqDTO.FundId).ToList();
                    }
                    else
                    {
                        ClientTransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == getFundAdministrationClientReqDTO.FundId && x.UnitType == getFundAdministrationClientReqDTO.UnitType).ToList();
                    }*/
                    // Ajay 29-12-2022   -- Code commented by ajay zala
                    var ClientTransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == getFundAdministrationClientReqDTO.FundId).ToList();
                    var ClientIdList = ClientTransactionList.Select(x => x.Client).Distinct().ToList();
                    UserList = UserList.Where(x => ClientIdList.Contains(x.Id));
                    ClientTransactionList = ClientTransactionList.Where(x => ClientIdList.Contains(x.Client)).ToList();
                    string CurrencySymbol = "$";

                    if (UserList.Count() > 0)
                    {
                        double TotalUserUnitBalance = 0;
                        double TotalUserAmountBalance = 0;
                        foreach (var user in UserList)
                        {
                            FundAdministrationClient fundAdministrationClient = new FundAdministrationClient();

                            fundAdministrationClient.Name = user.LastName + " " + user.FirstName;
                            fundAdministrationClient.Title = user.ResponsiblePerson != null ? user.ResponsiblePerson : "";
                            fundAdministrationClient.AccountNo = user.ClientAccNo;

                            var UserClientTransactionList = ClientTransactionList.Where(x => x.Client == user.Id).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).ToList();

                            double ClientUnitBalance = 0;
                            double ClientAmountBalance = 0;
                            double CostUnitPrice = 0;
                            double CurrentUnitPrice = 0;

                            var firstRecord = UserClientTransactionList.FirstOrDefault();
                            var lastRecord = UserClientTransactionList.LastOrDefault();
                            CostUnitPrice = firstRecord.UnitPrice;
                            CurrentUnitPrice = lastRecord.UnitPrice;

                            foreach (var userTrans in UserClientTransactionList)
                            {
                                if (userTrans.TransactionType.ToLower() == "buy")
                                {
                                    ClientUnitBalance = ClientUnitBalance + userTrans.NumberOfUnits;
                                    ClientAmountBalance = ClientAmountBalance + userTrans.TransactionAmount;
                                    fundAdministrationClient.UnitType = userTrans.UnitType;

                                }
                                else
                                {
                                    ClientUnitBalance = ClientUnitBalance - userTrans.NumberOfUnits;
                                    ClientAmountBalance = ClientAmountBalance - userTrans.TransactionAmount;
                                }
                            }

                            fundAdministrationClient.Units = Math.Abs(ClientUnitBalance);
                            fundAdministrationClient.UnitsString = _commonHelper.GetFormatedDouble(fundAdministrationClient.Units);
                            fundAdministrationClient.Value = Math.Abs(ClientAmountBalance);
                            fundAdministrationClient.ValueString = CurrencySymbol + _commonHelper.GetFormatedDouble(fundAdministrationClient.Value);

                            fundAdministrationClient.CostNAV = CurrencySymbol + _commonHelper.GetFormatedDouble(CostUnitPrice * ClientUnitBalance);
                            // fundAdministrationClient.CostNAV = CurrencySymbol + _commonHelper.GetFormatedDouble(CostUnitPrice);

                            fundAdministrationClient.CurrentUnitPrice = _commonHelper.GetFormatedDouble(CurrentUnitPrice);

                            // TotalUserUnitBalance = TotalUserUnitBalance + ClientUnitBalance;
                            TotalUserUnitBalance = Math.Abs(TotalUserUnitBalance + ClientUnitBalance);
                            TotalUserAmountBalance = Math.Abs(TotalUserAmountBalance + ClientAmountBalance);

                            fundAdministrationClient.OwnerShipStockPercentage = "";
                            fundAdministrationClient.OwnerShipFundPercentage = "";

                            fundAdministrationClient.CurrentNAV = CurrencySymbol + _commonHelper.GetFormatedDouble(CurrentUnitPrice * ClientUnitBalance);
                            // fundAdministrationClient.CurrentNAV = CurrencySymbol + _commonHelper.GetFormatedDouble(CurrentUnitPrice);

                            fundAdministrationClient.TelNo = user.WorkNo != null ? user.WorkNo : "";
                            fundAdministrationClient.MobileNo = user.MobileNo;
                            fundAdministrationClient.Email = user.Email;

                            getFundAdministrationClientResDTO.FundAdministrationClientList.Add(fundAdministrationClient);
                        }

                        foreach (var item in getFundAdministrationClientResDTO.FundAdministrationClientList)
                        {
                            item.OwnerShipStockPercentage = item.Units > 0 && TotalUserUnitBalance > 0 ? _commonHelper.GetFormatedDouble(item.Units * 100 / TotalUserUnitBalance) + "%" : "0%";
                            item.OwnerShipFundPercentage = item.Value > 0 && TotalUserAmountBalance > 0 ? _commonHelper.GetFormatedDouble(item.Value * 100 / TotalUserAmountBalance) + "%" : "0%";

                        }

                        getFundAdministrationClientResDTO.TotalUnitCount = _commonHelper.GetFormatedDouble(TotalUserUnitBalance);
                        getFundAdministrationClientResDTO.TotalValueCount = CurrencySymbol + _commonHelper.GetFormatedDouble(TotalUserAmountBalance);
                        getFundAdministrationClientResDTO.TotalCount = getFundAdministrationClientResDTO.FundAdministrationClientList.Count();

                        //Ajay 29-12-2022
                        if (getFundAdministrationClientReqDTO.UnitType.ToLower() != "all")
                        {
                            getFundAdministrationClientResDTO.FundAdministrationClientList = getFundAdministrationClientResDTO.FundAdministrationClientList.Where(x => x.UnitType == getFundAdministrationClientReqDTO.UnitType).ToList();
                        }
                      
                        foreach (var item in getFundAdministrationClientResDTO.FundAdministrationClientList)
                        {
                            item.OwnerShipStockPercentage = item.Units > 0 && TotalUserUnitBalance > 0 ? _commonHelper.GetFormatedDouble(item.Units * 100 / TotalUserUnitBalance) + "%" : "0%";
                            item.OwnerShipFundPercentage = item.Value > 0 && TotalUserAmountBalance > 0 ? _commonHelper.GetFormatedDouble(item.Value * 100 / TotalUserAmountBalance) + "%" : "0%";

                        }

                        //getFundAdministrationClientResDTO.TotalUnitCount = _commonHelper.GetFormatedDouble(TotalUserUnitBalance);
                        //getFundAdministrationClientResDTO.TotalValueCount = CurrencySymbol + _commonHelper.GetFormatedDouble(TotalUserAmountBalance);
                        getFundAdministrationClientResDTO.TotalUnitCount = _commonHelper.GetFormatedDouble(getFundAdministrationClientResDTO.FundAdministrationClientList.Select(x => x.Units).Sum());
                        getFundAdministrationClientResDTO.TotalValueCount = CurrencySymbol + _commonHelper.GetFormatedDouble(getFundAdministrationClientResDTO.FundAdministrationClientList.Select(x => x.Value).Sum());
                        getFundAdministrationClientResDTO.TotalCount = getFundAdministrationClientResDTO.FundAdministrationClientList.Count();

                        if (!string.IsNullOrWhiteSpace(getFundAdministrationClientReqDTO.Alphabet))
                        {
                            getFundAdministrationClientResDTO.FundAdministrationClientList = getFundAdministrationClientResDTO.FundAdministrationClientList.Where(x => x.Name.ToLower().StartsWith(getFundAdministrationClientReqDTO.Alphabet.ToLower())).ToList();
                        }

                        if (!string.IsNullOrEmpty(getFundAdministrationClientReqDTO.SearchString))
                        {
                            getFundAdministrationClientResDTO.FundAdministrationClientList = getFundAdministrationClientResDTO.FundAdministrationClientList.Where(x => x.Name.ToLower().Contains(getFundAdministrationClientReqDTO.SearchString.ToLower()) || x.AccountNo.ToLower().Contains(getFundAdministrationClientReqDTO.SearchString.ToLower())).ToList();
                        }

                        if (orderby)
                        {
                            getFundAdministrationClientResDTO.FundAdministrationClientList = getFundAdministrationClientResDTO.FundAdministrationClientList.OrderBy(x => x.AccountNo).ToList();
                        }

                        getFundAdministrationClientResDTO.FundAdministrationClientList = getFundAdministrationClientResDTO.FundAdministrationClientList.Skip((number - 1) * size)
                             .Take(size)
                             .ToList();

                        if (getFundAdministrationClientResDTO.FundAdministrationClientList.Count > 0)
                        {
                            getFundAdministrationClientResDTO.PageTotalUnitCount = _commonHelper.GetFormatedDouble(getFundAdministrationClientResDTO.FundAdministrationClientList.Select(x => x.Units).Sum());
                            getFundAdministrationClientResDTO.PageTotalValueCount = CurrencySymbol + _commonHelper.GetFormatedDouble(getFundAdministrationClientResDTO.FundAdministrationClientList.Select(x => x.Value).Sum());

                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Success.";
                            commonResponse.Data = getFundAdministrationClientResDTO;
                        }
                        else
                        {
                            commonResponse.StatusCode = HttpStatusCode.NotFound;
                            commonResponse.Message = "Data not Found.";
                            commonResponse.Data = getFundAdministrationClientResDTO;
                        }
                    }
                    else
                    {
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data not Found.";
                        commonResponse.Data = getFundAdministrationClientResDTO;
                    }

                    //Nikunj ----end
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



        //public CommonResponse GetAllFundAdministrationClient(GetFundAdministrationClientReqDTO getFundAdministrationClientReqDTO)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    GetFundAdministrationClientResDTO getFundAdministrationClientResDTO = new GetFundAdministrationClientResDTO();

        //    int number = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:Page").Value);
        //    int size = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:PageSize").Value);
        //    bool orderby = Convert.ToBoolean(_configuration.GetSection("ByDefaultPagination:OrderBy").Value);

        //    number = getFundAdministrationClientReqDTO.PageNumber == 0 ? number : getFundAdministrationClientReqDTO.PageNumber;
        //    size = getFundAdministrationClientReqDTO.PageSize == 0 ? size : getFundAdministrationClientReqDTO.PageSize;
        //    orderby = getFundAdministrationClientReqDTO.Orderby;

        //    try
        //    {
        //        if (getFundAdministrationClientReqDTO.FundId != 0)
        //        {
        //            getFundAdministrationClientResDTO.FundAdministrationClientList = new List<FundAdministrationClient>();

        //            //Nikunj ---start
        //            var UserList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == 2); //AccessCategoryId=2=Client
        //            var ClientTransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == getFundAdministrationClientReqDTO.FundId && x.UnitType == getFundAdministrationClientReqDTO.UnitType).ToList();

        //            if (getFundAdministrationClientReqDTO.UnitType.ToLower() == "all")
        //            {
        //                ClientTransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == getFundAdministrationClientReqDTO.FundId).ToList();
        //            }
        //            else
        //            {
        //                ClientTransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == getFundAdministrationClientReqDTO.FundId && x.UnitType == getFundAdministrationClientReqDTO.UnitType).ToList();
        //            }
        //            var ClientIdList = ClientTransactionList.Select(x => x.Client).Distinct().ToList();
        //            UserList = UserList.Where(x => ClientIdList.Contains(x.Id));
        //            ClientTransactionList = ClientTransactionList.Where(x => ClientIdList.Contains(x.Client)).ToList();
        //            string CurrencySymbol = "$";

        //            if (UserList.Count() > 0)
        //            {
        //                double TotalUserUnitBalance = 0;
        //                double TotalUserAmountBalance = 0;
        //                foreach (var user in UserList)
        //                {
        //                    FundAdministrationClient fundAdministrationClient = new FundAdministrationClient();

        //                    fundAdministrationClient.Name = user.LastName + " " + user.FirstName;
        //                    fundAdministrationClient.Title = user.Salutation != null ? user.Salutation : "";
        //                    fundAdministrationClient.AccountNo = user.ClientAccNo;

        //                    var UserClientTransactionList = ClientTransactionList.Where(x => x.Client == user.Id).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).ToList();

        //                    double ClientUnitBalance = 0;
        //                    double ClientAmountBalance = 0;
        //                    double CostUnitPrice = 0;
        //                    double CurrentUnitPrice = 0;

        //                    var firstRecord = UserClientTransactionList.FirstOrDefault();
        //                    var lastRecord = UserClientTransactionList.LastOrDefault();
        //                    CostUnitPrice = firstRecord.UnitPrice;
        //                    CurrentUnitPrice = lastRecord.UnitPrice;

        //                    foreach (var userTrans in UserClientTransactionList)
        //                    {
        //                        if (userTrans.TransactionType.ToLower() == "buy")
        //                        {
        //                            ClientUnitBalance = ClientUnitBalance + userTrans.NumberOfUnits;
        //                            ClientAmountBalance = ClientAmountBalance + userTrans.TransactionAmount;
        //                            fundAdministrationClient.UnitType = userTrans.UnitType;

        //                        }
        //                        else
        //                        {
        //                            ClientUnitBalance = ClientUnitBalance - userTrans.NumberOfUnits;
        //                            ClientAmountBalance = ClientAmountBalance - userTrans.TransactionAmount;
        //                        }
        //                    }

        //                    fundAdministrationClient.Units = Math.Abs(ClientUnitBalance);
        //                    fundAdministrationClient.UnitsString = _commonHelper.GetFormatedDouble(fundAdministrationClient.Units);
        //                    fundAdministrationClient.Value = Math.Abs(ClientAmountBalance);
        //                    fundAdministrationClient.ValueString = CurrencySymbol + _commonHelper.GetFormatedDouble(fundAdministrationClient.Value);

        //                    fundAdministrationClient.CostNAV = CurrencySymbol + _commonHelper.GetFormatedDouble(CostUnitPrice * ClientUnitBalance);
        //                    // fundAdministrationClient.CostNAV = CurrencySymbol + _commonHelper.GetFormatedDouble(CostUnitPrice);

        //                    fundAdministrationClient.CurrentUnitPrice = _commonHelper.GetFormatedDouble(CurrentUnitPrice);

        //                    // TotalUserUnitBalance = TotalUserUnitBalance + ClientUnitBalance;
        //                    TotalUserUnitBalance = Math.Abs(TotalUserUnitBalance + ClientUnitBalance);
        //                    TotalUserAmountBalance = Math.Abs(TotalUserAmountBalance + ClientAmountBalance);

        //                    fundAdministrationClient.OwnerShipStockPercentage = "";
        //                    fundAdministrationClient.OwnerShipFundPercentage = "";

        //                    fundAdministrationClient.CurrentNAV = CurrencySymbol + _commonHelper.GetFormatedDouble(CurrentUnitPrice * ClientUnitBalance);
        //                    // fundAdministrationClient.CurrentNAV = CurrencySymbol + _commonHelper.GetFormatedDouble(CurrentUnitPrice);

        //                    fundAdministrationClient.TelNo = user.WorkNo != null ? user.WorkNo : "";
        //                    fundAdministrationClient.MobileNo = user.MobileNo;
        //                    fundAdministrationClient.Email = user.Email;

        //                    getFundAdministrationClientResDTO.FundAdministrationClientList.Add(fundAdministrationClient);
        //                }

        //                foreach (var item in getFundAdministrationClientResDTO.FundAdministrationClientList)
        //                {
        //                    item.OwnerShipStockPercentage = item.Units > 0 && TotalUserUnitBalance > 0 ? _commonHelper.GetFormatedDouble(item.Units * 100 / TotalUserUnitBalance) + "%" : "0%";
        //                    item.OwnerShipFundPercentage = item.Value > 0 && TotalUserAmountBalance > 0 ? _commonHelper.GetFormatedDouble(item.Value * 100 / TotalUserAmountBalance) + "%" : "0%";

        //                }

        //                getFundAdministrationClientResDTO.TotalUnitCount = _commonHelper.GetFormatedDouble(TotalUserUnitBalance);
        //                getFundAdministrationClientResDTO.TotalValueCount = CurrencySymbol + _commonHelper.GetFormatedDouble(TotalUserAmountBalance);
        //                getFundAdministrationClientResDTO.TotalCount = getFundAdministrationClientResDTO.FundAdministrationClientList.Count();

        //                if (!string.IsNullOrWhiteSpace(getFundAdministrationClientReqDTO.Alphabet))
        //                {
        //                    getFundAdministrationClientResDTO.FundAdministrationClientList = getFundAdministrationClientResDTO.FundAdministrationClientList.Where(x => x.Name.ToLower().StartsWith(getFundAdministrationClientReqDTO.Alphabet.ToLower())).ToList();
        //                }

        //                if (!string.IsNullOrEmpty(getFundAdministrationClientReqDTO.SearchString))
        //                {
        //                    getFundAdministrationClientResDTO.FundAdministrationClientList = getFundAdministrationClientResDTO.FundAdministrationClientList.Where(x => x.Name.ToLower().Contains(getFundAdministrationClientReqDTO.SearchString.ToLower()) || x.AccountNo.ToLower().Contains(getFundAdministrationClientReqDTO.SearchString.ToLower())).ToList();
        //                }

        //                if (orderby)
        //                {
        //                    getFundAdministrationClientResDTO.FundAdministrationClientList = getFundAdministrationClientResDTO.FundAdministrationClientList.OrderBy(x => x.AccountNo).ToList();
        //                }

        //                getFundAdministrationClientResDTO.FundAdministrationClientList = getFundAdministrationClientResDTO.FundAdministrationClientList.Skip((number - 1) * size)
        //                     .Take(size)
        //                     .ToList();

        //                if (getFundAdministrationClientResDTO.FundAdministrationClientList.Count > 0)
        //                {
        //                    getFundAdministrationClientResDTO.PageTotalUnitCount = _commonHelper.GetFormatedDouble(getFundAdministrationClientResDTO.FundAdministrationClientList.Select(x => x.Units).Sum());
        //                    getFundAdministrationClientResDTO.PageTotalValueCount = CurrencySymbol + _commonHelper.GetFormatedDouble(getFundAdministrationClientResDTO.FundAdministrationClientList.Select(x => x.Value).Sum());

        //                    commonResponse.Status = true;
        //                    commonResponse.StatusCode = HttpStatusCode.OK;
        //                    commonResponse.Message = "Success.";
        //                    commonResponse.Data = getFundAdministrationClientResDTO;
        //                }
        //                else
        //                {
        //                    commonResponse.StatusCode = HttpStatusCode.NotFound;
        //                    commonResponse.Message = "Data not Found.";
        //                    commonResponse.Data = getFundAdministrationClientResDTO;
        //                }
        //            }
        //            else
        //            {
        //                commonResponse.StatusCode = HttpStatusCode.NotFound;
        //                commonResponse.Message = "Data not Found.";
        //                commonResponse.Data = getFundAdministrationClientResDTO;
        //            }

        //            //Nikunj ----end
        //        }
        //        else
        //        {
        //            commonResponse.Status = false;
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //            commonResponse.Message = "Please Enter Fund";
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return commonResponse;
        //}
    }
}
