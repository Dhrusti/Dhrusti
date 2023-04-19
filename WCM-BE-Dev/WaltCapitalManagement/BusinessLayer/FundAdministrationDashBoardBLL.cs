using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using System.Net;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public class FundAdministrationDashBoardBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        public FundAdministrationDashBoardBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
            _dbContext = dbContext;
            _commonRepo = commonRepo;
        }

        public CommonResponse GetFundAdministrationDashBoardByFundId(GetFundAdministrationDashBoardByFundIdReqDTO getFundAdministrationDashBoardByFundIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                GetFundAdminDashboardDataResDTO getFundAdministrationDashBoard = new GetFundAdminDashboardDataResDTO();

                var funddetails = _commonRepo.fundList().FirstOrDefault(x => x.Id == getFundAdministrationDashBoardByFundIdReqDTO.FundId);
                if (funddetails != null)
                {
                    getFundAdministrationDashBoard.fundAdminDashboardGraphData = new FundAdminDashboardGraphData();

                    var graph = GetFundAdminDashboardGraph(getFundAdministrationDashBoardByFundIdReqDTO.FundId, getFundAdministrationDashBoardByFundIdReqDTO.FundBenchMarkId ?? 0);
                    if (graph != null)
                    {
                        getFundAdministrationDashBoard.fundAdminDashboardGraphData = graph.Data;
                    }

                    getFundAdministrationDashBoard.fundAdminDashboardReturnsData = new List<FundAdminDashboardReturnsData>();
                    var returnsData = GetFundAdministrationDashboardReturnsData(getFundAdministrationDashBoardByFundIdReqDTO.FundId);
                    if (returnsData != null)
                    {
                        getFundAdministrationDashBoard.fundAdminDashboardReturnsData = returnsData.Data;
                    }

                    getFundAdministrationDashBoard.fundAdminDashboardFundStatsData = new List<FundAdminDashboardFundStatsData>();
                    var GetFundAdminDashboardFundStatsRES = GetFundAdminDashboardFundStats(getFundAdministrationDashBoardByFundIdReqDTO.FundId);
                    if (GetFundAdminDashboardFundStatsRES.Status)
                    {
                        getFundAdministrationDashBoard.fundAdminDashboardFundStatsData = GetFundAdminDashboardFundStatsRES.Data;

                    }

                    getFundAdministrationDashBoard.fundAdminDashboardCommentryData = new FundAdminDashboardCommentryData();
                    var res = GetFundAdminDashboardCommentry(getFundAdministrationDashBoardByFundIdReqDTO.FundId);
                    if (res.Status)
                    {
                        getFundAdministrationDashBoard.fundAdminDashboardCommentryData = res.Data;
                    }

                    if (getFundAdministrationDashBoard != null)
                    {
                        commonResponse.Message = "Success";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = getFundAdministrationDashBoard;
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

        public CommonResponse GetFundAdminDashboardFundStats(int FundId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<FundAdminDashboardFundStatsData> fundStatsDatas = new List<FundAdminDashboardFundStatsData>();
                var FundList = _commonRepo.fundList().Where(x => x.IsActive == true);
                var FundDetail = FundList.FirstOrDefault(x => x.Id == FundId);
                if (FundDetail != null)
                {
                    var PricingList = _commonRepo.GetPricingList(FundDetail.Id).OrderByDescending(x => x.TransactionDate.Value.Date).ToList();
                    if (PricingList.Count > 0)
                    {
                        DateTime LastRecordDate = PricingList.FirstOrDefault().TransactionDate.Value;
                        PricingList = PricingList.Where(x => x.TransactionDate.Value.Date == LastRecordDate.Date).ToList();

                        FundAdminDashboardFundStatsData dashboardFundStatsData = new FundAdminDashboardFundStatsData();

                        dashboardFundStatsData.Title = "Inception Date";
                        dashboardFundStatsData.values = FundDetail.InceptionDate.ToString("dd MMMM yyyy");
                        //dashboardFundStatsData.values = Convert.ToString(_commonHelper.TodaysConvertDate(Convert.ToString(FundDetail.InceptionDate)));
                        fundStatsDatas.Add(dashboardFundStatsData);

                        dashboardFundStatsData = new FundAdminDashboardFundStatsData();
                        dashboardFundStatsData.Title = "Fund Size";
                        dashboardFundStatsData.values = FundDetail.Currency == "Rand (R)" ? "R " + _commonHelper.GetFormatedDecimal(PricingList.Select(x => x.DynPrcInpTotal).Sum()) : "$ " + _commonHelper.GetFormatedDecimal(PricingList.Select(x => x.DynPrcInpTotal).Sum());
                        fundStatsDatas.Add(dashboardFundStatsData);

                        foreach (var item in PricingList)
                        {
                            dashboardFundStatsData = new FundAdminDashboardFundStatsData();
                            if (item.UnitType.Contains("Unit"))
                            {
                                item.UnitType = (Regex.Replace(item.UnitType, "Unit", string.Empty, RegexOptions.IgnoreCase).Trim());
                            }
                            dashboardFundStatsData.Title = "Units Issued" + " " + item.UnitType;
                            dashboardFundStatsData.values = _commonHelper.GetFormatedDecimal(item.Units);
                            fundStatsDatas.Add(dashboardFundStatsData);

                            dashboardFundStatsData = new FundAdminDashboardFundStatsData();
                            if (item.UnitType.Contains("Unit"))
                            {
                                item.UnitType = (Regex.Replace(item.UnitType, "Unit", string.Empty, RegexOptions.IgnoreCase).Trim());
                            }
                            dashboardFundStatsData.Title = "Unit Price" + " " + item.UnitType;
                            dashboardFundStatsData.values =FundDetail.Currency == "Rand (R)" ? "R " + _commonHelper.GetFormatedDecimal(item.UnitPriceNav) : "$ " + _commonHelper.GetFormatedDecimal(item.UnitPriceNav);
                            fundStatsDatas.Add(dashboardFundStatsData);
                        }


                        commonResponse.Status = true;
                        commonResponse.Message = "Success!";
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = fundStatsDatas;
                    }
                }
                else
                {
                    commonResponse.Message = "FundNot Found!";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse GetFundAdminDashboardCommentry(int FundId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                FundAdminDashboardCommentryData fundAdminDashboardCommentryData = new FundAdminDashboardCommentryData();

                var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.IsActive == true && x.Id == FundId);
                if (FundDetail != null)
                {
                    var PricingList = _commonRepo.GetPricingList(FundDetail.Id).OrderByDescending(x => x.TransactionDate.Value.Date).ToList();
                    if (PricingList.Count > 0)
                    {
                        DateTime startDate = _commonHelper.GetCurrentDateTime().AddMonths(-1);
                        DateTime endDate = FundDetail.InceptionDate;
                        int startmonth = startDate.Month;
                        int endmonth = endDate.Month;
                        decimal UnitPrice = 0;

                        fundAdminDashboardCommentryData.CommentryHeaderList = new List<CommentryHeaderModel>();
                        fundAdminDashboardCommentryData.CommentryDataList = new List<Dictionary<string, string>>();
                        fundAdminDashboardCommentryData.LatestCommentryDate = startDate;
                        fundAdminDashboardCommentryData.LatestCommentryValue = "Long Equity Growth";
                        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                        for (DateTime i = startDate; i >= endDate; i = i.AddMonths(-1))
                        {
                            int CurrentMonth = i.Month;
                            int CurrentYear = i.Year;
                            DateTime startDatePricing = _commonHelper.GetStartDateOfMonth(CurrentYear, CurrentMonth);
                            DateTime endDatePricing = _commonHelper.GetLastDateOfMonth(CurrentYear, CurrentMonth);

                            decimal startUnitPrice = PricingList.Where(x => x.TransactionDate.Value.Date == startDatePricing.Date).Select(x => x.UnitPriceNav).Sum();
                            decimal endUnitPrice = PricingList.Where(x => x.TransactionDate.Value.Date == endDatePricing.Date).Select(x => x.UnitPriceNav).Sum();

                            if (startUnitPrice > 0)
                                UnitPrice = ((endUnitPrice - startUnitPrice) / startUnitPrice) * 100;
                            string UnitPriceStr = _commonHelper.GetFormatedDecimal(UnitPrice) + "%";

                            CommentryHeaderModel commentryHeaderModel = new CommentryHeaderModel();
                            commentryHeaderModel.Label = i.ToString("MMMyyyy");
                            commentryHeaderModel.Value = i.ToString("MMMyyyy").ToLower();

                            fundAdminDashboardCommentryData.CommentryHeaderList.Add(commentryHeaderModel);


                            keyValuePairs.Add(i.ToString("MMMyyyy").ToLower(), UnitPriceStr);


                        }
                        fundAdminDashboardCommentryData.CommentryDataList.Add(keyValuePairs);
                        commonResponse.Status = true;
                        commonResponse.Message = "Success!";
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = fundAdminDashboardCommentryData;
                    }
                }
                else
                {
                    commonResponse.Message = "FundNot Found!";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse GetFundAdminDashboardGraph(int FundId, int FundBenchMarkId = 0)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                FundAdminDashboardGraphData fundAdminDashboardGraph = new FundAdminDashboardGraphData();
                var fundDetails = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId && x.IsActive == true);
                if (fundDetails != null)
                {
                    DateTime startDate = fundDetails.InceptionDate;
                    DateTime endDate = _commonHelper.GetCurrentDateTime();
                    int startMonth = startDate.Month;
                    int endMonth = endDate.Month;
                    decimal FundUnitPrice = 0;
                    decimal FundBenchmarkPrice = 0;

                    var fundbenchMarkList = _commonRepo.FundBenchMarkList().Where(x => x.FundId == fundDetails.Id && x.IsInDashboard == true && x.IsDeleted == false && x.IsActive == true);
                    var fundBenchMarkDetails = fundbenchMarkList.FirstOrDefault(x => x.Id == FundBenchMarkId);
                    var pricingList = _commonRepo.GetPricingList(fundDetails.Id);

                    fundAdminDashboardGraph.FundPhilosophy = fundDetails.FundPhilosophy;
                    fundAdminDashboardGraph.fundGraphData = new List<FundGraphData>();

                    if (fundBenchMarkDetails != null)
                    {
                        for (DateTime i = startDate; i <= endDate; i = i.AddMonths(6))
                        {
                            FundUnitPrice = pricingList.Where(x => x.TransactionDate.Value.Date == i.Date).Select(x => x.UnitPriceNav).Sum();
                            FundBenchmarkPrice = fundbenchMarkList.Where(x => x.BenchMarkDate.Date == i.Date && x.Id == fundBenchMarkDetails.Id).Select(x => x.BenchMarkValue).Sum();

                            FundGraphData fundGraphData = new FundGraphData();
                            fundGraphData.FundName = fundDetails.FundName;
                            fundGraphData.FundBenchMarkName = fundBenchMarkDetails.BenchMarkName;
                            fundGraphData.Date = i.ToString("yyyy/MM/dd").Replace("-", "/");
                            fundGraphData.FundUnitPrice = FundUnitPrice;
                            fundGraphData.FundBenchmarkUnitPrice = FundBenchmarkPrice;

                            fundAdminDashboardGraph.fundGraphData.Add(fundGraphData);
                        }
                    }

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success!";
                    commonResponse.Data = fundAdminDashboardGraph;
                }
                else
                {
                    commonResponse.Message = "Data not found!";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetFundAdministrationDashboardReturnsData(int FundId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<FundAdminDashboardReturnsData> fundAdminDashboardReturnsDataList = new List<FundAdminDashboardReturnsData>();
                var FundDetails = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId && x.IsActive == true);
                var PricingList = _commonRepo.GetPricingList(FundId).ToList();
                if (FundDetails != null)
                {
                    DateTime CurrentDate = _commonHelper.GetCurrentDateTime();
                    DateTime CurrentFirstDayOfMonth = _commonHelper.GetStartDateOfMonth(CurrentDate.Year, CurrentDate.Month);

                    decimal StartValue = 0;
                    decimal EndValue = 0;

                    DateTime MonthToDateStartDate;
                    DateTime MonthToDateEndDate;
                    decimal MonthToDateValue = 0;
                    decimal SinceInceptionValue = 0;
                    decimal SinceInceptionAnnualizeValue = 0;
                    decimal TwelveMonthRollingValue = 0;
                    string ThreeYearsRollingValue = "0";
                    string FiveYearsRollingValue = "0";

                    //Get value for MonthToDateValue

                    //var FilteredPricingList = PricingList.Where(x => x.TransactionDate.Value.Date <= CurrentDate.Date && x.TransactionDate.Value.Date >= CurrentFirstDayOfMonth).ToList();
                    var FilteredPricingList = PricingList.Where(x => x.TransactionDate.Value.Date <= CurrentDate.Date).ToList();
                    if (FilteredPricingList.Count > 0)
                    {
                        if (FilteredPricingList.Count > 0)
                        {
                            int MonthToDateCurrentYear = DateTime.Now.Year;
                            int MonthToDateCurrentMonth = DateTime.Now.Month;
                            MonthToDateStartDate = _commonHelper.GetStartDateOfMonth(MonthToDateCurrentYear, MonthToDateCurrentMonth);
                            DateTime? transactionStartDate = FilteredPricingList.OrderBy(x => x.TransactionDate).FirstOrDefault().TransactionDate;
                            if (MonthToDateStartDate < transactionStartDate)
                            {
                                MonthToDateStartDate = (DateTime)transactionStartDate;
                            }
                            MonthToDateEndDate = DateTime.Now.AddDays(-1);
                            //MonthToDateStartDate = FilteredPricingList.OrderBy(x => x.TransactionDate.Value.Date).FirstOrDefault().TransactionDate.Value;
                            //MonthToDateEndDate = FilteredPricingList.OrderByDescending(x => x.TransactionDate.Value.Date).FirstOrDefault().TransactionDate.Value;

                            StartValue = FilteredPricingList.Where(x => x.TransactionDate.Value.Date == MonthToDateStartDate.Date).Select(x => x.UnitPriceNav).Sum();
                            EndValue = FilteredPricingList.Where(x => x.TransactionDate.Value.Date == MonthToDateEndDate.Date).Select(x => x.UnitPriceNav).Sum();
                            if(StartValue > 0)
                            MonthToDateValue = ((EndValue - StartValue) / StartValue) * 100;


                            //Get value for SinceInception
                            DateTime SinceInceptionStartDate;
                            DateTime SinceInceptionEndDate;

                            DateTime SinceInceptionED;
                            DateTime SinceInceptionSD;

                            SinceInceptionStartDate = FundDetails.InceptionDate;
                            SinceInceptionEndDate = MonthToDateEndDate;

                            for (int i = SinceInceptionStartDate.Year; i <= SinceInceptionEndDate.Year; i++)
                            {
                                var pricingInceptionSD = PricingList.Where(x => x.TransactionDate.Value.Year == i).OrderBy(x => x.TransactionDate.Value).FirstOrDefault();
                                if (pricingInceptionSD != null)
                                {
                                    SinceInceptionSD = pricingInceptionSD.TransactionDate.Value;
                                    SinceInceptionED = PricingList.Where(x => x.TransactionDate.Value.Year == i).OrderByDescending(x => x.TransactionDate.Value).FirstOrDefault().TransactionDate.Value;

                                    StartValue = PricingList.Where(x => x.TransactionDate.Value.Date == SinceInceptionStartDate.Date).Select(x => x.UnitPriceNav).Sum();
                                    EndValue = PricingList.Where(x => x.TransactionDate.Value.Date == SinceInceptionEndDate.Date).Select(x => x.UnitPriceNav).Sum();
                                    if (StartValue > 0 && EndValue >0)
                                    {
                                        SinceInceptionValue += ((EndValue - StartValue) / StartValue) * 100;
                                    }
                                    
                                }
                            }

                            //Get value for SinceInceptionAnnualized

                            DateTime SinceInceptionAnnualizedStartDate;
                            DateTime SinceInceptionAnnualizedEndDate;

                            DateTime SinceInceptionAnnualizedSD;
                            DateTime SinceInceptionAnnualizedED;
                            SinceInceptionAnnualizedStartDate = FundDetails.InceptionDate;
                            SinceInceptionAnnualizedEndDate = MonthToDateEndDate;

                            var TotalYears = (SinceInceptionAnnualizedEndDate.Year - SinceInceptionAnnualizedStartDate.Year);

                            for (int i = SinceInceptionAnnualizedStartDate.Year; i <= SinceInceptionAnnualizedEndDate.Year; i++)
                            {

                                var pricingInceptionSD = PricingList.Where(x => x.TransactionDate.Value.Year == i).OrderBy(x => x.TransactionDate.Value).FirstOrDefault();
                                if (pricingInceptionSD != null)
                                {
                                    SinceInceptionAnnualizedSD = pricingInceptionSD.TransactionDate.Value;
                                    SinceInceptionAnnualizedED = PricingList.Where(x => x.TransactionDate.Value.Year == i).OrderByDescending(x => x.TransactionDate.Value).FirstOrDefault().TransactionDate.Value;

                                    StartValue = PricingList.Where(x => x.TransactionDate.Value.Date == SinceInceptionAnnualizedStartDate.Date).Select(x => x.UnitPriceNav).Sum();
                                    EndValue = PricingList.Where(x => x.TransactionDate.Value.Date == SinceInceptionAnnualizedEndDate.Date).Select(x => x.UnitPriceNav).Sum();
                                    if (StartValue > 0 && EndValue > 0)
                                    {
                                        SinceInceptionAnnualizeValue += ((EndValue - StartValue) / StartValue) * 100;
                                    }
                                }
                            }
                            decimal SinceInceptionAnnualizeValueRatio = 0;
                            if (SinceInceptionAnnualizeValue != 0 && TotalYears > 0)
                            {
                                SinceInceptionAnnualizeValueRatio = SinceInceptionAnnualizeValue / TotalYears;
                            }


                            //Get value for 12Months Rolling

                            DateTime TwelveMonthStartDate = _commonHelper.GetCurrentDateTime();
                            DateTime TwelveMonthEndDate = FundDetails.InceptionDate;

                            int TwelveMonthStartMonth = TwelveMonthStartDate.Month;
                            int TwelveMonthEndMonth = TwelveMonthEndDate.Month;
                            decimal TwelveMonthUnitPrice = 0;

                            for (DateTime i = TwelveMonthStartDate; i >= TwelveMonthEndDate; i = i.AddMonths(-12))
                            {
                                int CurrentMonth = i.Month;
                                int CurrentYear = i.Year;
                                DateTime startDatePricing = TwelveMonthStartDate;
                                DateTime endDatePricing = TwelveMonthEndDate;

                                decimal startUnitPrice = PricingList.Where(x => x.TransactionDate.Value.Date == startDatePricing.Date).Select(x => x.UnitPriceNav).Sum();
                                decimal endUnitPrice = PricingList.Where(x => x.TransactionDate.Value.Date == endDatePricing.Date).Select(x => x.UnitPriceNav).Sum();

                                if (startUnitPrice > 0)
                                    TwelveMonthUnitPrice = ((endUnitPrice - startUnitPrice) / startUnitPrice) * 100;
                                TwelveMonthRollingValue = Convert.ToDecimal(_commonHelper.GetFormatedDecimal(TwelveMonthUnitPrice));

                            }


                            //Get value for 3Years Rolling
                            DateTime ThreeYearsStartDate = _commonHelper.GetCurrentDateTime().AddYears(-3);
                            DateTime ThreeYearsEndDate = _commonHelper.GetCurrentDateTime();
                            decimal threeYearsUnitPrice = 0;
                            if (FundDetails.InceptionDate.Date <= ThreeYearsStartDate.Date)
                            {
                                decimal startUnitPrice = PricingList.Where(x => x.TransactionDate.Value.Date == ThreeYearsStartDate.Date).Select(x => x.UnitPriceNav).Sum();
                                decimal endUnitPrice = PricingList.Where(x => x.TransactionDate.Value.Date == ThreeYearsEndDate.Date).Select(x => x.UnitPriceNav).Sum();

                                if (startUnitPrice > 0)
                                    threeYearsUnitPrice = ((endUnitPrice - startUnitPrice) / startUnitPrice) * 100;
                                ThreeYearsRollingValue = Convert.ToString(_commonHelper.GetFormatedDecimal(Convert.ToDecimal(_commonHelper.GetFormatedDecimal(threeYearsUnitPrice)))) + "%";
                            }
                            else
                            {
                                ThreeYearsRollingValue = "NaN";
                            }

                            //Get value for 5Years Rolling

                            DateTime FiveYearsStartDate = _commonHelper.GetCurrentDateTime().AddYears(-5);
                            DateTime FiveYearsEndDate = _commonHelper.GetCurrentDateTime();
                            decimal fiveYearsUnitPrice = 0;
                            if (FundDetails.InceptionDate.Date <= ThreeYearsStartDate.Date)
                            {
                                decimal startUnitPrice = PricingList.Where(x => x.TransactionDate.Value.Date == ThreeYearsStartDate.Date).Select(x => x.UnitPriceNav).Sum();
                                decimal endUnitPrice = PricingList.Where(x => x.TransactionDate.Value.Date == ThreeYearsEndDate.Date).Select(x => x.UnitPriceNav).Sum();

                                if (startUnitPrice > 0)
                                    fiveYearsUnitPrice = ((endUnitPrice - startUnitPrice) / startUnitPrice) * 100;
                                FiveYearsRollingValue = Convert.ToString(_commonHelper.GetFormatedDecimal(Convert.ToDecimal(_commonHelper.GetFormatedDecimal(fiveYearsUnitPrice)))) + "%";
                            }
                            else
                            {
                                FiveYearsRollingValue = "NaN";
                            }

                            FundAdminDashboardReturnsData fundAdminDashboardReturnsData = new FundAdminDashboardReturnsData();
                            fundAdminDashboardReturnsData.Title = "Month To Date";
                            fundAdminDashboardReturnsData.values = _commonHelper.GetFormatedDecimal(MonthToDateValue) + "%";
                            fundAdminDashboardReturnsDataList.Add(fundAdminDashboardReturnsData);

                            fundAdminDashboardReturnsData = new FundAdminDashboardReturnsData();
                            fundAdminDashboardReturnsData.Title = "Since Inception";
                            fundAdminDashboardReturnsData.values = _commonHelper.GetFormatedDecimal(SinceInceptionValue) + "%";
                            fundAdminDashboardReturnsDataList.Add(fundAdminDashboardReturnsData);

                            fundAdminDashboardReturnsData = new FundAdminDashboardReturnsData();
                            fundAdminDashboardReturnsData.Title = "Since Inception (Annualized)";
                            fundAdminDashboardReturnsData.values = _commonHelper.GetFormatedDecimal(SinceInceptionAnnualizeValue) + "%";
                            fundAdminDashboardReturnsDataList.Add(fundAdminDashboardReturnsData);

                            fundAdminDashboardReturnsData = new FundAdminDashboardReturnsData();
                            fundAdminDashboardReturnsData.Title = "12 Month Rolling";
                            fundAdminDashboardReturnsData.values = _commonHelper.GetFormatedDecimal(TwelveMonthRollingValue) + "%";
                            fundAdminDashboardReturnsDataList.Add(fundAdminDashboardReturnsData);

                            fundAdminDashboardReturnsData = new FundAdminDashboardReturnsData();
                            fundAdminDashboardReturnsData.Title = "3years Rolling";
                            fundAdminDashboardReturnsData.values = ThreeYearsRollingValue;
                            fundAdminDashboardReturnsDataList.Add(fundAdminDashboardReturnsData);

                            fundAdminDashboardReturnsData = new FundAdminDashboardReturnsData();
                            fundAdminDashboardReturnsData.Title = "5years Rolling";
                            fundAdminDashboardReturnsData.values = FiveYearsRollingValue;
                            fundAdminDashboardReturnsDataList.Add(fundAdminDashboardReturnsData);

                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Success!";
                            commonResponse.Data = fundAdminDashboardReturnsDataList;
                        }
                        else
                        {
                            commonResponse.Data = fundAdminDashboardReturnsDataList;
                            commonResponse.Message = "Data not found!";
                            commonResponse.StatusCode = HttpStatusCode.NotFound;
                        }
                    }
                    else
                    {
                        commonResponse.Message = "Fund not found!";
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                    }
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
