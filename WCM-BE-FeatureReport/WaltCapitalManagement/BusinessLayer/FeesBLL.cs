using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net;

namespace BusinessLayer
{
    public class FeesBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _configaration;

        public FeesBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configaration = configuration;
        }

        public CommonResponse GetIFAFeeBreakdown(GetIFAFeeBreakdownReqDTO getIFAFeeBreakdownReqDTO)
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

            int number = getIFAFeeBreakdownReqDTO.PageNumber == 0 ? (pagecount) : getIFAFeeBreakdownReqDTO.PageNumber;
            int size = getIFAFeeBreakdownReqDTO.PageSize == 0 ? (pageSize) : getIFAFeeBreakdownReqDTO.PageSize;
            bool orderby = getIFAFeeBreakdownReqDTO.Orderby == true ? (order) : getIFAFeeBreakdownReqDTO.Orderby;

            try
            {
                GetIFAFeeBreakdownResDTO getIFAFeeBreakdownResDTO = new GetIFAFeeBreakdownResDTO();
                List<IfaFeeBreakdown> IfaBreakdownList = new List<IfaFeeBreakdown>();

                bool isCurrencyZAR = false;
                DateTime now = _commonHelper.GetCurrentDateTime();
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();
                if (now.Month == 1)
                {
                    startDate = new DateTime(now.Year - 1, 12, 1);
                }
                else
                {
                    startDate = new DateTime(now.Year, now.Month - 1, 1);
                }
                endDate = startDate.AddMonths(1).AddDays(-1);

                if (getIFAFeeBreakdownReqDTO.FundId != null && getIFAFeeBreakdownReqDTO.FundId != 0)
                {
                    var clientTransactionList = _commonRepo.clientTransactionList()
                          .Where(x => x.Fund == getIFAFeeBreakdownReqDTO.FundId
                          && x.TransactionDate.Month == startDate.Month
                          && x.TransactionDate.Date >= startDate.Date
                          && x.TransactionDate.Date <= endDate.Date).ToList(); // fund Wise

                    var clientTransactionDistinct = clientTransactionList.GroupBy(m => new { m.Ifa }).Select(group => group.First()).ToList();
                    IfaBreakdownList = (from ctm in clientTransactionDistinct
                                        join ifaUlFinal in _commonRepo.getUserList().Where(x => x.AccessCategoryId == 3).ToList() on ctm.Ifa equals ifaUlFinal.Id
                                        select new IfaFeeBreakdown
                                        {
                                            IfaId = ifaUlFinal.Id,
                                            FirstName = ifaUlFinal.FirstName,
                                            LastName = ifaUlFinal.LastName,
                                            TeleNo = ifaUlFinal.MobileNo,
                                            Email = ifaUlFinal.Email,
                                            ClientAccNo = ifaUlFinal.ClientAccNo
                                        }).ToList();

                    decimal zarFeeValue = 0, otherFeeValue = 0, vatPercentage = 0;
                    bool isVatApplicable = false;
                    isCurrencyZAR = false;
                    if (IfaBreakdownList.Count > 0)
                    {
                        int FundId = Convert.ToInt32(getIFAFeeBreakdownReqDTO.FundId);
                        var fundDetails = _commonRepo.fundList().FirstOrDefault(x => x.IsActive == true && x.Id == FundId) ?? new FundMst();
                        var PricingIfafees = _commonRepo.GetPricingList(FundId).Where(x=> x.TransactionDate.Value.Month == startDate.Month
                          && x.TransactionDate.Value.Date >= startDate.Date
                          && x.TransactionDate.Value.Date <= endDate.Date).Sum(x => x.Ifafees);

                        if (fundDetails.Currency.ToLower().Contains("rand") || fundDetails.Currency.ToLower().Contains("zar"))
                        {
                            zarFeeValue = PricingIfafees;
                            isCurrencyZAR = true;
                            if (fundDetails.IsVatapplicable)
                            {
                                isVatApplicable = true;
                                vatPercentage = Convert.ToDecimal(fundDetails.Vat);
                            }
                        }
                        else
                        {
                            otherFeeValue = PricingIfafees;
                        }
                    }

                    foreach (var item in IfaBreakdownList)
                    {
                        var transactionList = clientTransactionList.Where(x => x.Ifa == item.IfaId).ToList();// fund and ifa wise
                        var clientList = transactionList.Select(x => x.Client).Distinct().ToList();
                        decimal IfaAUM = 0;
                        foreach (var subItem in clientList)
                        {
                            var clientTransactions = clientTransactionList.Where(x => x.Client == subItem && x.Ifa == item.IfaId).ToList();
                            foreach (var childItem in clientTransactions)
                            {
                                decimal ClientAmountBalance = 0;
                                if (childItem.TransactionType.ToLower() == "buy")
                                {
                                    ClientAmountBalance = ClientAmountBalance + Convert.ToDecimal(childItem.TransactionAmount);
                                }
                                else
                                {
                                    ClientAmountBalance = ClientAmountBalance - Convert.ToDecimal(childItem.TransactionAmount);
                                }
                                IfaAUM = IfaAUM + ClientAmountBalance;
                            }
                        }
                        item.AUM = Convert.ToString(IfaAUM);

                        decimal VAT = 0;
                        decimal ZARFee = 0;
                        decimal TotalZARFee = 0;
                        decimal OtherFee = 0;
                        if (isCurrencyZAR)
                        {
                            ZARFee = zarFeeValue;
                            if (isVatApplicable)
                            {
                                VAT = (IfaAUM * vatPercentage) / 100;
                            }
                            TotalZARFee = VAT + ZARFee;
                        }
                        else
                        {
                            OtherFee = otherFeeValue;
                        }

                        item.ZARFee = Convert.ToString(ZARFee);
                        item.VAT = Convert.ToString(VAT);
                        item.TotalZARFee = Convert.ToString(TotalZARFee);
                        item.USDFee = Convert.ToString(OtherFee);
                    }
                }
                else
                {
                    var IfaList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == 3).ToList();
                    var fundList = _commonRepo.fundList().Where(x => x.IsActive == true).ToList();
                    var clientTransactionList = (from ifaUl in IfaList
                                                 join ctm in _commonRepo.clientTransactionList() on ifaUl.Id equals ctm.Ifa
                                                 select ctm).ToList();

                    var clientTransactionIfaDistinct = clientTransactionList.GroupBy(m => new { m.Ifa }).Select(group => group.First()).ToList();

                    foreach (var item in clientTransactionIfaDistinct)
                    {
                        IfaFeeBreakdown ifaFeeBreakdown = new IfaFeeBreakdown();
                        var clientTransactionClientList = clientTransactionList.Where(x => x.Ifa == item.Ifa).ToList();
                        var clientTransactionsFundDistinct = clientTransactionClientList.GroupBy(m => new { m.Fund }).Select(group => group.First()).ToList();
                        clientTransactionsFundDistinct = (from fundL in fundList
                                                          join ctfd in clientTransactionsFundDistinct on fundL.Id equals ctfd.Fund
                                                          select ctfd).ToList();

                        decimal IfaAUM = 0;
                        foreach (var subItem in clientTransactionsFundDistinct)
                        {
                            var transactionList = clientTransactionClientList.Where(x => x.Fund == subItem.Fund).ToList();

                            decimal zarFeePercentage = 0, otherFeePercentage = 0, vatPercentage = 0;
                            bool isVatApplicable = false;
                            isCurrencyZAR = false;

                            var fundDetails = fundList.FirstOrDefault(x => x.Id == subItem.Fund);
                            var PricingIfafees = _commonRepo.GetPricingList(subItem.Fund).Sum(x => x.Ifafees);

                            if (fundDetails != null && (fundDetails.Currency.ToLower().Contains("rand") || fundDetails.Currency.ToLower().Contains("zar")))
                            {
                                zarFeePercentage = PricingIfafees;
                                isCurrencyZAR = true;
                                if (fundDetails.IsVatapplicable)
                                {
                                    isVatApplicable = true;
                                    vatPercentage = Convert.ToDecimal(fundDetails.Vat);
                                }
                            }
                            else if (fundDetails != null)
                            {
                                isCurrencyZAR = false;
                                isVatApplicable = false;
                                otherFeePercentage = PricingIfafees;
                            }

                            foreach (var childItem in transactionList)
                            {
                                decimal ClientAmountBalance = 0;

                                if (childItem.TransactionType.ToLower() == "buy")
                                {
                                    ClientAmountBalance = ClientAmountBalance + Convert.ToDecimal(childItem.TransactionAmount);
                                }
                                else
                                {
                                    ClientAmountBalance = ClientAmountBalance - Convert.ToDecimal(childItem.TransactionAmount);
                                }
                                IfaAUM = IfaAUM + ClientAmountBalance;
                            }

                            decimal VAT = 0, ZARFee = 0, TotalZARFee = 0, OtherFee = 0;
                            if (isCurrencyZAR)
                            {
                                ZARFee = (IfaAUM * zarFeePercentage) / 100;
                                if (isVatApplicable)
                                {
                                    VAT = (IfaAUM * vatPercentage) / 100;
                                }
                                TotalZARFee = VAT + ZARFee;
                            }
                            else
                            {
                                OtherFee = (IfaAUM * otherFeePercentage) / 100;
                            }
                            ifaFeeBreakdown.ZARFee = Convert.ToString(ZARFee);
                            ifaFeeBreakdown.VAT = Convert.ToString(VAT);
                            ifaFeeBreakdown.TotalZARFee = Convert.ToString(TotalZARFee);
                            ifaFeeBreakdown.USDFee = Convert.ToString(OtherFee);
                        }


                        var IfaDetails = IfaList.FirstOrDefault(x => x.Id == item.Ifa) ?? new UserMst();

                        ifaFeeBreakdown.IfaId = IfaDetails.Id;
                        ifaFeeBreakdown.FirstName = IfaDetails.FirstName;
                        ifaFeeBreakdown.LastName = IfaDetails.LastName;
                        ifaFeeBreakdown.TeleNo = IfaDetails.MobileNo;
                        ifaFeeBreakdown.Email = IfaDetails.Email;
                        ifaFeeBreakdown.AUM = Convert.ToString(IfaAUM);

                        IfaBreakdownList.Add(ifaFeeBreakdown);
                    }
                }

                getIFAFeeBreakdownResDTO.IsCurrencyZAR = isCurrencyZAR;
                getIFAFeeBreakdownResDTO.TotalCount = IfaBreakdownList.Count();

                if (orderby)
                {
                    if (IfaBreakdownList.Count <= size)
                    {
                        IfaBreakdownList = IfaBreakdownList.OrderBy(x => x.FirstName).ToList();
                    }
                    else
                    {

                        IfaBreakdownList = IfaBreakdownList.Skip((number - 1) * size)
                           .Take(size)
                           .OrderBy(x => x.FirstName)
                           .ToList();
                    }
                }
                else
                {
                    if (IfaBreakdownList.Count <= size)
                    {
                        IfaBreakdownList = IfaBreakdownList.OrderByDescending(x => x.FirstName).ToList();
                    }
                    else
                    {
                        IfaBreakdownList = IfaBreakdownList.OrderByDescending(x => x.FirstName).Skip((number - 1) * size)
                            .Take(size)
                            .ToList();
                    }
                }

                getIFAFeeBreakdownResDTO.CurrentMonthEndDate = endDate;
                getIFAFeeBreakdownResDTO.IfaFeeBreakdown = IfaBreakdownList;

                if (IfaBreakdownList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getIFAFeeBreakdownResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetClientDetailsByIfaId(GetClientDetailsByIfaIdReqDTO getClientDetailsByIfaIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var UserList = _commonRepo.getUserList();
                var ClientTransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == getClientDetailsByIfaIdReqDTO.FundId && x.Ifa == getClientDetailsByIfaIdReqDTO.IfaId);
                var ClientTransactionClientIdList = ClientTransactionList.Select(x => x.Client).ToList();
                var UserDetailList = UserList.Where(x => ClientTransactionClientIdList.Contains(x.Id)).ToList();

                if (UserDetailList.Count > 0)
                {
                    List<GetClientDetailsByIfaIdResDTO> getClientDetails = new List<GetClientDetailsByIfaIdResDTO>();
                    foreach (var client in UserDetailList)
                    {
                        GetClientDetailsByIfaIdResDTO getClient = new GetClientDetailsByIfaIdResDTO();
                        getClient.Id = client.Id;
                        getClient.FullName = client.FirstName + " " + client.LastName;
                        getClient.ClientAccNo = client.ClientAccNo;

                        getClientDetails.Add(getClient);
                    }

                    commonResponse.Data = getClientDetails.Adapt<List<GetClientDetailsByIfaIdResDTO>>();
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

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        #region FundFeesMst

        public CommonResponse AddFundFees(int fundId, string FeesName, int createdBy)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                FundFeesMst fundFeesMst = new FundFeesMst();
                fundFeesMst.FundId = fundId;
                fundFeesMst.FeesName = FeesName;
                fundFeesMst.IsActive = true;
                fundFeesMst.IsDeleted = false;
                fundFeesMst.CreatedBy = createdBy;
                fundFeesMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                fundFeesMst.UpdatedBy = createdBy;
                fundFeesMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                _dbContext.FundFeesMsts.Add(fundFeesMst);
                _dbContext.SaveChanges();

                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Message = "Fees Added Successfully!";
                commonResponse.Data = fundFeesMst;
            }

            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        #endregion

        public CommonResponse GetIfaFeeReport(GetIfaFeeReportReqDTO getIfaFeeReportReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                DateTime CurrentDate = _commonHelper.GetCurrentDateTime();
                DateTime StartDate = CurrentDate;
                DateTime EndDate = CurrentDate;
                decimal TotalFees = 0;

                if (getIfaFeeReportReqDTO.FilterBy == "LatestMonth")
                {
                    StartDate = _commonHelper.GetStartDateOfMonth(CurrentDate.Year, CurrentDate.Month);
                    //EndDate = _commonHelper.GetLastDateOfMonth(CurrentDate.Year, CurrentDate.Month);
                    EndDate = CurrentDate;
                }
                else if (getIfaFeeReportReqDTO.FilterBy == "Last12Months")
                {
                    int Month = DateTime.ParseExact(getIfaFeeReportReqDTO.Month, "MMMM", CultureInfo.CurrentCulture).Month;
                    StartDate = _commonHelper.GetStartDateOfMonth(Convert.ToInt32(getIfaFeeReportReqDTO.Year), Convert.ToInt32(Month));
                    EndDate = _commonHelper.GetLastDateOfMonth(Convert.ToInt32(getIfaFeeReportReqDTO.Year), Convert.ToInt32(Month));
                }
                else if (getIfaFeeReportReqDTO.FilterBy == "DateSpecific")
                {
                    StartDate = getIfaFeeReportReqDTO.StartDate.Value;
                    EndDate = getIfaFeeReportReqDTO.StartDate.Value;
                }
                else if (getIfaFeeReportReqDTO.FilterBy == "RangeSpecific")
                {
                    StartDate = getIfaFeeReportReqDTO.StartDate.Value;
                    EndDate = getIfaFeeReportReqDTO.EndDate.Value;
                }

                var FeesList = _commonRepo.GetFundFeeList(0).ToList();
                var FundDetails = _commonRepo.fundList().FirstOrDefault(x => x.Id == getIfaFeeReportReqDTO.FundId) ?? new FundMst();
                var PricingList = _commonRepo.GetPricingList(getIfaFeeReportReqDTO.FundId).ToList();

                GetIfaFeeReportResDTO getIfaFeeReport = new GetIfaFeeReportResDTO();
                getIfaFeeReport.FeeSummaries = new List<FeeSummary>();
                foreach (var fees in FeesList)
                {
                    decimal CalculatedFee = 0;
                    if (PricingList.Count > 0)
                    {
                        PricingList = PricingList.Where(x => x.TransactionDate.Value.Date >= StartDate.Date && x.TransactionDate.Value.Date <= EndDate.Date).ToList();
                        if (fees.Id == 1) // Audit Fees
                        {
                            CalculatedFee = PricingList.Select(x => x.AuditFees).Sum();
                        }
                        else if (fees.Id == 2) // Compliance Fee
                        {
                            CalculatedFee = PricingList.Select(x => x.ComplianceFees).Sum();
                        }
                        else if (fees.Id == 3) // Management Fee
                        {
                            CalculatedFee = PricingList.Select(x => x.ManFees).Sum();
                        }
                        else if (fees.Id == 4) // Performance Fee
                        {
                            CalculatedFee = PricingList.Select(x => x.PerfFees).Sum();
                        }
                        else if (fees.Id == 5) // IFA Fee
                        {
                            CalculatedFee = PricingList.Select(x => x.Ifafees).Sum();
                        }
                    }

                    FeeSummary feeSummary = new FeeSummary();
                    feeSummary.FeeType = fees.FeesName;
                    feeSummary.Currency = FundDetails.Currency;
                    feeSummary.FeeAmount = _commonHelper.GetFormatedDecimal(CalculatedFee);

                    getIfaFeeReport.FeeSummaries.Add(feeSummary);
                    TotalFees = TotalFees + CalculatedFee;
                }
                getIfaFeeReport.Currency = FundDetails.Currency;
                getIfaFeeReport.TotalFees = _commonHelper.GetFormatedDecimal(TotalFees);

                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Message = "Success!";
                commonResponse.Data = getIfaFeeReport;

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
