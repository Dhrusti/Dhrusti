using DataLayer.Entities;
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
    }
}
