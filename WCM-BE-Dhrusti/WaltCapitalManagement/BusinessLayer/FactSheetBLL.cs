using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.RegularExpressions;
using System.Transactions;
using static DTO.ResDTO.GetPortfolioPerformanceResDTO;

namespace BusinessLayer
{
    public class FactSheetBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly RunFeesBLL _runFeesBLL;
        private readonly FundDynamicFieldBLL _fundDynamicFieldBLL;
        public FactSheetBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper, RunFeesBLL runFeesBLL, FundDynamicFieldBLL fundDynamicFieldBLL)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _runFeesBLL = runFeesBLL;
            _fundDynamicFieldBLL = fundDynamicFieldBLL;
        }

        public CommonResponse GetFactSheetById(GetFactSheetByIdReqDTO getFactSheetByIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var a = getFactSheetByIdReqDTO.FactSheetId;
                GetFactSheetByIdResDTO getFactSheetByIdResDTO = new GetFactSheetByIdResDTO();
                getFactSheetByIdResDTO = _commonRepo.factSheetList().Where(x => x.Id == a).First().Adapt<GetFactSheetByIdResDTO>();

                if (getFactSheetByIdResDTO != null)
                {
                    commonResponse.Message = "Success.";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getFactSheetByIdResDTO;
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

        public CommonResponse GetFactSheetByFundId(GetFactSheetByFundReqDTO getFactSheetByFundReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                FactSheetMst factSheetMst = new FactSheetMst();
                factSheetMst = _commonRepo.factSheetList().Where(x => x.FundId == getFactSheetByFundReqDTO.FundId).FirstOrDefault() ?? new FactSheetMst();

                FundMst fundMst = new FundMst();
                fundMst = _commonRepo.fundList().Where(x => x.Id == getFactSheetByFundReqDTO.FundId).FirstOrDefault() ?? new FundMst();

                if ((factSheetMst != null && factSheetMst.FundId != 0 && fundMst.Id != 0))
                {
                    GetFactSheetByFundResDTO getFactSheetByFundIdResDTO = new GetFactSheetByFundResDTO();
                    getFactSheetByFundIdResDTO = factSheetMst.Adapt<GetFactSheetByFundResDTO>();
                    getFactSheetByFundIdResDTO.MinInvestment = factSheetMst.MinInvestment;
                    getFactSheetByFundIdResDTO.FundRiskRating = fundMst.FundRiskRating;
                    getFactSheetByFundIdResDTO.Currency = fundMst.Currency == "Rand (R)" ? "Rand" : fundMst.Currency;
                    getFactSheetByFundIdResDTO.CurrentDate = _commonHelper.GetCurrentDateTime();

                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getFactSheetByFundIdResDTO;
                }
                else
                {
                    commonResponse.Data = null;
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

        public CommonResponse AddFactSheetDetails(AddFactSheetReqDTO addFactSheetReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddFactSheetResDTO addFactSheetResDTO = new AddFactSheetResDTO();
            try
            {
                if (addFactSheetReqDTO.FundId != 0)
                {
                    using (var scope = new TransactionScope())
                    {
                        var factSheet = _commonRepo.factSheetList().FirstOrDefault(x => x.FundId == addFactSheetReqDTO.FundId);
                        if (factSheet == null)
                        {
                            FactSheetMst factSheetMst = new FactSheetMst();
                            factSheetMst.FundId = addFactSheetReqDTO.FundId;
                            factSheetMst.InvestmentObjective = addFactSheetReqDTO.InvestmentObjective;
                            factSheetMst.PortfolioManager = addFactSheetReqDTO.PortfolioManager;
                            factSheetMst.Email = addFactSheetReqDTO.Email;
                            factSheetMst.Fsp = addFactSheetReqDTO.Fsp;
                            factSheetMst.Telephone = addFactSheetReqDTO.Telephone;
                            factSheetMst.InceptionDate = addFactSheetReqDTO.InceptionDate;
                            factSheetMst.Sector = addFactSheetReqDTO.Sector;
                            factSheetMst.Target = addFactSheetReqDTO.Target;
                            factSheetMst.ParticipatoryStructure = addFactSheetReqDTO.ParticipatoryStructure;
                            factSheetMst.MinInvestment = addFactSheetReqDTO.MinInvestment;
                            factSheetMst.AnnualFeesUnitA = addFactSheetReqDTO.AnnualFeesUnitA;
                            factSheetMst.AnnualFeesUnitB = addFactSheetReqDTO.AnnualFeesUnitB;
                            factSheetMst.BaseFee = addFactSheetReqDTO.BaseFee;
                            factSheetMst.FeeHurdle = addFactSheetReqDTO.FeeHurdle;
                            factSheetMst.PerformanceFeesUnitA = addFactSheetReqDTO.PerformanceFeesUnitA;
                            factSheetMst.PerformanceFeesUnitB = addFactSheetReqDTO.PerformanceFeesUnitB;
                            factSheetMst.FeeExample = addFactSheetReqDTO.FeeExample;
                            factSheetMst.Method = addFactSheetReqDTO.Method;
                            factSheetMst.Recommended = addFactSheetReqDTO.Recommended;
                            factSheetMst.Disclaimer = addFactSheetReqDTO.Disclaimer;
                            factSheetMst.ShortCommentary = addFactSheetReqDTO.ShortCommentary;
                            factSheetMst.IsActive = true;
                            factSheetMst.IsDeleted = false;
                            factSheetMst.CreatedBy = addFactSheetReqDTO.CreatedBy;
                            factSheetMst.UpdatedBy = addFactSheetReqDTO.CreatedBy;
                            factSheetMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                            factSheetMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                            _dBContext.FactSheetMsts.Add(factSheetMst);
                            _dBContext.SaveChanges();

                            FundMst fundMst = _commonRepo.fundList().Where(x => x.Id == addFactSheetReqDTO.FundId).FirstOrDefault() ?? new FundMst();
                            if (fundMst != null && fundMst.FundName != null)
                            {
                                fundMst.IsActive = true;
                                fundMst.IsFactSheetCreated = true;

                                _dBContext.Entry(fundMst).State = EntityState.Modified;
                                _dBContext.SaveChanges();

                                _runFeesBLL.ActivateRunFeesStatus(fundMst.Id, true);

                                scope.Complete();

                                addFactSheetResDTO.Id = factSheetMst.Id;
                                addFactSheetResDTO.Email = factSheetMst.Email;

                                commonResponse.Message = "FactSheet Details added Successfully!";
                                commonResponse.Status = true;
                                commonResponse.StatusCode = HttpStatusCode.OK;
                                commonResponse.Data = addFactSheetResDTO;
                            }
                            else
                            {
                                commonResponse.Status = false;
                                commonResponse.StatusCode = HttpStatusCode.BadRequest;
                                commonResponse.Message = "Fund does not exist!";
                            }
                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "FundId already exist!";
                        }
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Fund does not exist!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateFactSheet(UpdateFactSheetReqDTO updateFactSheetReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateFactSheetResDTO updateFactSheetResDTO = new UpdateFactSheetResDTO();
            try
            {
                var factsheetDetail = _commonRepo.factSheetList().FirstOrDefault(x => x.Id == updateFactSheetReqDTO.Id);
                if (factsheetDetail != null)
                {
                    FactSheetMst factSheetMst = factsheetDetail;
                    factSheetMst.FundId = updateFactSheetReqDTO.FundId;
                    factSheetMst.InvestmentObjective = updateFactSheetReqDTO.InvestmentObjective;
                    factSheetMst.PortfolioManager = updateFactSheetReqDTO.PortfolioManager;
                    factSheetMst.Email = updateFactSheetReqDTO.Email;
                    factSheetMst.Fsp = updateFactSheetReqDTO.Fsp;
                    factSheetMst.Telephone = updateFactSheetReqDTO.Telephone;
                    factSheetMst.InceptionDate = updateFactSheetReqDTO.InceptionDate;
                    factSheetMst.Sector = updateFactSheetReqDTO.Sector;
                    factSheetMst.Target = updateFactSheetReqDTO.Target;
                    factSheetMst.ParticipatoryStructure = updateFactSheetReqDTO.ParticipatoryStructure;
                    factSheetMst.MinInvestment = updateFactSheetReqDTO.MinInvestment;
                    factSheetMst.AnnualFeesUnitA = updateFactSheetReqDTO.AnnualFeesUnitA;
                    factSheetMst.AnnualFeesUnitB = updateFactSheetReqDTO.AnnualFeesUnitB;
                    factSheetMst.BaseFee = updateFactSheetReqDTO.BaseFee;
                    factSheetMst.FeeHurdle = updateFactSheetReqDTO.FeeHurdle;
                    factSheetMst.PerformanceFeesUnitA = updateFactSheetReqDTO.PerformanceFeesUnitA;
                    factSheetMst.PerformanceFeesUnitB = updateFactSheetReqDTO.PerformanceFeesUnitB;
                    factSheetMst.FeeExample = updateFactSheetReqDTO.FeeExample;
                    factSheetMst.Method = updateFactSheetReqDTO.Method;
                    factSheetMst.Recommended = updateFactSheetReqDTO.Recommended;
                    factSheetMst.Disclaimer = updateFactSheetReqDTO.Disclaimer;
                    factSheetMst.ShortCommentary = updateFactSheetReqDTO.ShortCommentary;
                    factSheetMst.IsActive = true;
                    factSheetMst.IsDeleted = false;
                    factSheetMst.UpdatedBy = updateFactSheetReqDTO.UpdatedBy;
                    factSheetMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dBContext.Entry(factSheetMst).State = EntityState.Modified;
                    _dBContext.SaveChanges();

                    updateFactSheetResDTO.Id = factSheetMst.Id;

                    commonResponse.Data = updateFactSheetResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Successfully Updated...!!!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not update the data...!!!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetModelPortfolio(ModelPortfolioReqDTO modelPortfolioReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                ModelPortfolioResDTO modelPortfolioResDTO = new ModelPortfolioResDTO();

                List<HeaderModel> headerModelList = new List<HeaderModel>();
                headerModelList.Add(new HeaderModel { Value = "duration", Label = "" });
                headerModelList.Add(new HeaderModel { Value = "portfolio", Label = "Portfolio" });
                headerModelList.Add(new HeaderModel { Value = "s&p", Label = "S&P500 (TR)" });

                modelPortfolioResDTO.HeaderList = headerModelList;

                modelPortfolioResDTO.TableDataList = new List<Dictionary<string, string>>();

                Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Since Inception (1 Feb 2022)");
                keyValuePair.Add("portfolio", "52.11" + "%");
                keyValuePair.Add("s&p", "45.44" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Latest 3 Month (90 Days)");
                keyValuePair.Add("portfolio", "6.40" + "%");
                keyValuePair.Add("s&p", "-" + "4.60" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Latest 6 Month (180 Days)");
                keyValuePair.Add("portfolio", "9.72" + "%");
                keyValuePair.Add("s&p", "5.92" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Latest Months (30 Days)");
                keyValuePair.Add("portfolio", "2.22" + "%");
                keyValuePair.Add("s&p", "2.71" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Latest Year (12 Months)");
                keyValuePair.Add("portfolio", "8.75" + "%");
                keyValuePair.Add("s&p", "15.65" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Year To Date");
                keyValuePair.Add("portfolio", "6.40" + "%");
                keyValuePair.Add("s&p", "-" + "4.60" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Annualized");
                keyValuePair.Add("portfolio", "21.38" + "%");
                keyValuePair.Add("s&p", "18.89" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);

                commonResponse.Message = "Model Portfolio List";
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Data = modelPortfolioResDTO;

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetRiskStatistics(GetRiskStatisticsReqDTO getRiskStatisticsReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetRiskStatisticsResDTO getRiskStatisticsResDTO = new GetRiskStatisticsResDTO();

                List<HeaderValueModel> headerList = new List<HeaderValueModel>();
                headerList.Add(new HeaderValueModel { Value = "duration", Label = "" });
                headerList.Add(new HeaderValueModel { Value = "portfolio", Label = "Portfolio" });
                headerList.Add(new HeaderValueModel { Value = "s&p", Label = "S&P500 (TR)" });

                getRiskStatisticsResDTO.HeaderValueList = headerList;
                getRiskStatisticsResDTO.TableDataList = new List<Dictionary<string, string>>();

                Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Standard Deviation");
                keyValuePair.Add("portfolio", "17.03" + "%");
                keyValuePair.Add("s&p", "20.83" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Best Months");
                keyValuePair.Add("portfolio", "8.9" + "%");
                keyValuePair.Add("s&p", "12.08" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Worst Month");
                keyValuePair.Add("portfolio", "-" + "5.8" + "%");
                keyValuePair.Add("s&p", "-" + "12.4" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "Max Drawn Down");
                keyValuePair.Add("portfolio", "18.5" + "%");
                keyValuePair.Add("s&p", "19.6" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "% of Positive Months");
                keyValuePair.Add("portfolio", "65.4" + "%");
                keyValuePair.Add("s&p", "65.4" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("duration", "% of Negative Months");
                keyValuePair.Add("portfolio", "34.6" + "%");
                keyValuePair.Add("s&p", "34.6" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);

                commonResponse.Status = true;
                commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                commonResponse.Data = getRiskStatisticsResDTO;
                commonResponse.Message = "Risk Statistics List";
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetTopHoldings(GetTopHoldingsListReqDTO getTopHoldingsListReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetTopHoldingsListResDTO model = new GetTopHoldingsListResDTO();

                List<HeaderValueModelList> headerList = new List<HeaderValueModelList>();
                headerList.Add(new HeaderValueModelList { Value = "name", Label = "Name" });
                headerList.Add(new HeaderValueModelList { Value = "value", Label = "Value" });

                model.HeaderValuemodel = headerList;
                model.TableDatamodelList = new List<Dictionary<string, string>>();

                Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Stock");
                keyValuePair.Add("value", "20" + "%");
                model.TableDatamodelList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Silver");
                keyValuePair.Add("value", "30" + "%");
                model.TableDatamodelList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Bitcoin");
                keyValuePair.Add("value", "10" + "%");
                model.TableDatamodelList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Cash");
                keyValuePair.Add("value", "40" + "%");
                model.TableDatamodelList.Add(keyValuePair);

                commonResponse.Status = true;
                commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                commonResponse.Data = model;
                commonResponse.Message = "TopHoldings List";
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetMonthlyPerformance(GetMonthlyPerformanceReqDTO getMonthlyPerformanceReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                GetMonthlyPerformanceResDTO getMonthlyPerformanceResDTO = new GetMonthlyPerformanceResDTO();

                getMonthlyPerformanceResDTO.YearList = new List<Dictionary<string, string>>();

                Dictionary<string, string> yearValue = new Dictionary<string, string>();
                yearValue.Add("year", "2020");
                getMonthlyPerformanceResDTO.YearList.Add(yearValue);

                yearValue = new Dictionary<string, string>();
                yearValue.Add("year", "2021");
                getMonthlyPerformanceResDTO.YearList.Add(yearValue);

                yearValue = new Dictionary<string, string>();
                yearValue.Add("year", "2022");
                getMonthlyPerformanceResDTO.YearList.Add(yearValue);

                List<MonthlyPerformanceHead> headerList = new List<MonthlyPerformanceHead>();
                headerList.Add(new MonthlyPerformanceHead { Value = "type", Label = " ", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "jan", Label = "Jan", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "feb", Label = "Feb", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "mar", Label = "Mar", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "april", Label = "April", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "may", Label = "May", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "june", Label = "June", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "july", Label = "July", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "aug", Label = "Aug", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "sep", Label = "Sep", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "oct", Label = "Oct", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "nov", Label = "Nov", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "dec", Label = "Dec", Color = "color" });
                headerList.Add(new MonthlyPerformanceHead { Value = "ytd", Label = "YTD", Color = "color" });

                getMonthlyPerformanceResDTO.MonthlyPerformanceHead = headerList;
                getMonthlyPerformanceResDTO.MonthlyPerformanceTableData = new List<Dictionary<string, string>>();

                Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("type", "Portfolio");
                keyValuePair.Add("jan", "" + "");
                keyValuePair.Add("feb", "0.0" + "%");
                keyValuePair.Add("mar", "0.7" + "%");
                keyValuePair.Add("april", "4.8" + "%");
                keyValuePair.Add("may", "7.8" + "%");
                keyValuePair.Add("june", "4.8" + "%");
                keyValuePair.Add("july", "8.9" + "%");
                keyValuePair.Add("aug", "1.0" + "%");
                keyValuePair.Add("sep", "-" + "1.4" + "%");
                keyValuePair.Add("oct", "3.7" + "%");
                keyValuePair.Add("nov", "6.1" + "%");
                keyValuePair.Add("dec", "8.3" + "%");
                keyValuePair.Add("ytd", "52.8" + "%");

                getMonthlyPerformanceResDTO.MonthlyPerformanceTableData.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("type", "S&P 500");
                keyValuePair.Add("jan", " " + "");
                keyValuePair.Add("feb", "-" + "8.2" + "%");
                keyValuePair.Add("mar", "-" + "12.4" + "%");
                keyValuePair.Add("april", "12.8" + "%");
                keyValuePair.Add("may", "4.8" + "%");
                keyValuePair.Add("june", "2.0" + "%");
                keyValuePair.Add("july", "5.6" + "%");
                keyValuePair.Add("aug", "7.2" + "%");
                keyValuePair.Add("sep", "-" + "3.8" + "%");
                keyValuePair.Add("oct", "-" + "2.7" + "%");
                keyValuePair.Add("nov", "10.9" + "%");
                keyValuePair.Add("dec", "3.8" + "%");
                keyValuePair.Add("ytd", "18.45" + "%");

                getMonthlyPerformanceResDTO.MonthlyPerformanceTableData.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("type", "Portfolio");
                keyValuePair.Add("jan", "-" + "0.3" + "%");
                keyValuePair.Add("feb", "-" + "2.8" + "%");
                keyValuePair.Add("mar", "-" + "5.6" + "%");
                keyValuePair.Add("april", "5.9" + "%");
                keyValuePair.Add("may", "4.1" + "%");
                keyValuePair.Add("june", "-" + "0.4" + "%");
                keyValuePair.Add("july", "-" + "5.8" + "%");
                keyValuePair.Add("aug", "-" + "4.3" + "%");
                keyValuePair.Add("sep", "0.2" + "%");
                keyValuePair.Add("oct", "3.1" + "%");
                keyValuePair.Add("nov", "-" + "4.2" + "%");
                keyValuePair.Add("dec", "4.4" + "%");
                keyValuePair.Add("ytd", "-" + "6.45" + "%");

                getMonthlyPerformanceResDTO.MonthlyPerformanceTableData.Add(keyValuePair);



                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("type", "S&P 500");
                keyValuePair.Add("jan", "-" + "1.0" + "%");
                keyValuePair.Add("feb", "2.8" + "%");
                keyValuePair.Add("mar", "4.4" + "%");
                keyValuePair.Add("april", "5.3" + "%");
                keyValuePair.Add("may", "0.7" + "%");
                keyValuePair.Add("june", "2.3" + "%");
                keyValuePair.Add("july", "2.4" + "%");
                keyValuePair.Add("aug", "3.0" + "%");
                keyValuePair.Add("sep", "-" + "4.7" + "%");
                keyValuePair.Add("oct", "7.0" + "%");
                keyValuePair.Add("nov", "-" + "0.7" + "%");
                keyValuePair.Add("dec", "4.5" + "%");
                keyValuePair.Add("ytd", "28.71" + "%");

                getMonthlyPerformanceResDTO.MonthlyPerformanceTableData.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("type", "Portfolio");
                keyValuePair.Add("jan", "-" + "3.9" + "%");
                keyValuePair.Add("feb", "8.3" + "%");
                keyValuePair.Add("mar", "2.2" + "%");
                keyValuePair.Add("april", "");
                keyValuePair.Add("may", "");
                keyValuePair.Add("june", "");
                keyValuePair.Add("july", "");
                keyValuePair.Add("aug", "");
                keyValuePair.Add("sep", "");
                keyValuePair.Add("oct", "");
                keyValuePair.Add("nov", "");
                keyValuePair.Add("dec", "");
                keyValuePair.Add("ytd", "6.4" + "%");

                getMonthlyPerformanceResDTO.MonthlyPerformanceTableData.Add(keyValuePair);



                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("type", "S&P 500");
                keyValuePair.Add("jan", "-" + "5.2" + "%");
                keyValuePair.Add("feb", "-" + "3.0" + "%");
                keyValuePair.Add("mar", "-" + "3.7" + "%");
                keyValuePair.Add("april", "");
                keyValuePair.Add("may", "");
                keyValuePair.Add("june", "");
                keyValuePair.Add("july", "");
                keyValuePair.Add("aug", "");
                keyValuePair.Add("sep", "");
                keyValuePair.Add("oct", "");
                keyValuePair.Add("nov", "");
                keyValuePair.Add("dec", "");
                keyValuePair.Add("ytd", "-" + "4.60" + "%");

                getMonthlyPerformanceResDTO.MonthlyPerformanceTableData.Add(keyValuePair);

                commonResponse.Status = true;
                commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                commonResponse.Data = getMonthlyPerformanceResDTO;
                commonResponse.Message = "Monthly Performance List";

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;

        }

        public CommonResponse GetPortfolioPerformance(GetPortfolioPerformanceReqDTO getPortfolioPerformanceReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetPortfolioPerformanceResDTO getPortfolioPerformanceResDTO = new GetPortfolioPerformanceResDTO();

                List<GraphDataModelList> graphDataModelList = new List<GraphDataModelList>();
                graphDataModelList.Add(new GraphDataModelList { Value = "value", Label = "Value" });
                graphDataModelList.Add(new GraphDataModelList { Value = "uv", Label = "UV" });
                graphDataModelList.Add(new GraphDataModelList { Value = "pv", Label = "PV" });
                graphDataModelList.Add(new GraphDataModelList { Value = "amount", Label = "Amount" });
                graphDataModelList.Add(new GraphDataModelList { Value = "yaxis", Label = "yAxis" });

                getPortfolioPerformanceResDTO.dataModelList = graphDataModelList;
                getPortfolioPerformanceResDTO.GraphDataModelValueList = new List<Dictionary<string, string>>();

                Dictionary<string, string> graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Feb20");
                graphDataModelValueList.Add("uv", "4000");
                graphDataModelValueList.Add("pv", "2400");
                graphDataModelValueList.Add("amount", "2400");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);

                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Mar20");
                graphDataModelValueList.Add("uv", "3000");
                graphDataModelValueList.Add("pv", "1398");
                graphDataModelValueList.Add("amount", "2210");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);

                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Apr20");
                graphDataModelValueList.Add("uv", "2000");
                graphDataModelValueList.Add("pv", "8800");
                graphDataModelValueList.Add("amount", "2290");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);

                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "May20");
                graphDataModelValueList.Add("uv", "2780");
                graphDataModelValueList.Add("pv", "3908");
                graphDataModelValueList.Add("amount", "2000");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);

                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Jun20");
                graphDataModelValueList.Add("uv", "1890");
                graphDataModelValueList.Add("pv", "4800");
                graphDataModelValueList.Add("amount", "2181");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);

                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Jul20");
                graphDataModelValueList.Add("uv", "2390");
                graphDataModelValueList.Add("pv", "3800");
                graphDataModelValueList.Add("amount", "2500");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);

                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Aug20");
                graphDataModelValueList.Add("uv", "2780");
                graphDataModelValueList.Add("pv", "3908");
                graphDataModelValueList.Add("amount", "2100");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);


                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "sep20");
                graphDataModelValueList.Add("uv", "3323");
                graphDataModelValueList.Add("pv", "4300");
                graphDataModelValueList.Add("amount", "2435");
                graphDataModelValueList.Add("yaxis", "R1.26");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);


                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "oct20");
                graphDataModelValueList.Add("uv", "2390");
                graphDataModelValueList.Add("pv", "3800");
                graphDataModelValueList.Add("amount", "2435");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);


                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Nov20");
                graphDataModelValueList.Add("uv", "1890");
                graphDataModelValueList.Add("pv", "4800");
                graphDataModelValueList.Add("amount", "2435");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);


                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Dec20");
                graphDataModelValueList.Add("uv", "2323");
                graphDataModelValueList.Add("pv", "1212");
                graphDataModelValueList.Add("amount", "2435");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);


                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Jan21");
                graphDataModelValueList.Add("uv", "2780");
                graphDataModelValueList.Add("pv", "3908");
                graphDataModelValueList.Add("amount", "2435");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);


                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Feb21");
                graphDataModelValueList.Add("uv", "2323");
                graphDataModelValueList.Add("pv", "1212");
                graphDataModelValueList.Add("amount", "2435");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);


                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Mar21");
                graphDataModelValueList.Add("uv", "3323");
                graphDataModelValueList.Add("pv", "4300");
                graphDataModelValueList.Add("amount", "2435");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);

                graphDataModelValueList = new Dictionary<string, string>();
                graphDataModelValueList.Add("value", "Apr21");
                graphDataModelValueList.Add("uv", "2323");
                graphDataModelValueList.Add("pv", "1212");
                graphDataModelValueList.Add("amount", "2435");
                graphDataModelValueList.Add("yaxis", "R1.25");
                getPortfolioPerformanceResDTO.GraphDataModelValueList.Add(graphDataModelValueList);

                commonResponse.Status = true;
                commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                commonResponse.Data = getPortfolioPerformanceResDTO;
                commonResponse.Message = "Portfolio Performance List";
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;


        }

        //public CommonResponse GetUnitTypeByFundId(GetUnitTypeByFundIdReqDTO getUnitTypeByFundIdReqDTO)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        var FundId = getUnitTypeByFundIdReqDTO.FundId;
        //        List<string> unitType = new List<string>();
        //        var unitType1 = new List<string>();
        //        if (getUnitTypeByFundIdReqDTO.FundId != 0)
        //        {
        //            GetUnitTypeByFundIdResDTO getUnitTypeByFundIdResDTO = new GetUnitTypeByFundIdResDTO();
        //            var fundDynamicField = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == getUnitTypeByFundIdReqDTO.FundId).ToList();

        //            if (fundDynamicField != null)
        //            {
        //                unitType.Add("Unit A");
        //                unitType.Add("Unit B");
        //                foreach (var item in fundDynamicField)
        //                {
        //                    string label = Convert.ToString(item.Label).ToLower();
        //                    if (label.Contains("management fee"))
        //                    {
        //                        unitType.Add(Regex.Replace(item.Label, "management fee", string.Empty, RegexOptions.IgnoreCase).Trim());
        //                    }
        //                    else if (label.Contains("performance fee"))
        //                    {
        //                        unitType.Add(Regex.Replace(item.Label, "performance fee", string.Empty, RegexOptions.IgnoreCase).Trim());
        //                    }
        //                }
        //                unitType1 = unitType.Distinct().ToList();
        //                getUnitTypeByFundIdResDTO.UnitType = unitType1;

        //                commonResponse.Message = "Success.";
        //                commonResponse.Status = true;
        //                commonResponse.StatusCode = HttpStatusCode.OK;
        //                commonResponse.Data = getUnitTypeByFundIdResDTO;
        //            }
        //            else
        //            {
        //                commonResponse.Message = "Data Not Found";
        //                commonResponse.Status = false;
        //                commonResponse.StatusCode = HttpStatusCode.NotFound;
        //            }
        //        }
        //        else
        //        {
        //            commonResponse.Message = "FundId Does not exist.";
        //            commonResponse.Status = false;
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return commonResponse;
        //}

        public CommonResponse GetFactSheetFieldsFromUnit(GetFactSheetByUnitReqDTO getFactSheetByUnitReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var unit = getFactSheetByUnitReqDTO.Unit;
                double annual = 0;
                double performance = 0;

                GetFactSheetByUnitResDTO getFactSheetByUnitResDTO = new GetFactSheetByUnitResDTO();
                var fundMst = _commonRepo.fundList().Where(x => x.Id == getFactSheetByUnitReqDTO.FundId).FirstOrDefault();

                if (fundMst != null)
                {
                    if (unit.ToLower() == "unit a")
                    {
                        annual = fundMst.ManagementFeeA;
                        performance = fundMst.PerformanceFeeA;
                    }
                    else if (unit.ToLower() == "unit b")
                    {
                        annual = fundMst.ManagementFeeB;
                        performance = fundMst.PerformanceFeeB;
                    }
                    else
                    {
                        var fundDynamicField = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == getFactSheetByUnitReqDTO.FundId).ToList();
                        foreach (var item in fundDynamicField)
                        {
                            if (item.Label.ToLower().Contains("management fee") && item.Label.Contains(unit))
                            {
                                annual = Convert.ToDouble(item.Value);
                            }
                            else if (item.Label.ToLower().Contains("performance fee") && item.Label.Contains(unit))
                            {
                                performance = Convert.ToDouble(item.Value);
                            }
                        }
                    }

                    getFactSheetByUnitResDTO.Annual = annual;
                    getFactSheetByUnitResDTO.Performance = performance;

                    commonResponse.Message = "Success.";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getFactSheetByUnitResDTO;
                }
                else
                {
                    commonResponse.Message = "FundId Does not exist.";
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetUnitTypeByFundId(GetUnitTypeByFundIdReqDTO getUnitTypeByFundIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var FundDetails = _commonRepo.fundList().FirstOrDefault(x => x.IsActive == true && x.Id == getUnitTypeByFundIdReqDTO.FundId);
                //var FundId = getUnitTypeByFundIdReqDTO.FundId;
                List<string> unitType = new List<string>();
                var unitType1 = new List<string>();
                if (FundDetails != null)
                {
                    GetUnitTypeByFundIdResDTO getUnitTypeByFundIdResDTO = new GetUnitTypeByFundIdResDTO();
                    //var fundDynamicField = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundDetails.Id).ToList();

                    //if (fundDynamicField != null)
                    //{
                    //    if (FundDetails.PerformanceFeeA > 0 && FundDetails.ManagementFeeA > 0)
                    //        unitType.Add("Unit A");
                    //    if (FundDetails.PerformanceFeeB > 0 && FundDetails.ManagementFeeB > 0)
                    //        unitType.Add("Unit B");
                    //    foreach (var item in fundDynamicField)
                    //    {
                    //        string label = Convert.ToString(item.Label).ToLower();
                    //        if (label.Contains("management fee"))
                    //        {
                    //            unitType.Add(Regex.Replace(item.Label, "management fee", string.Empty, RegexOptions.IgnoreCase).Trim());
                    //        }
                    //        else if (label.Contains("performance fee"))
                    //        {
                    //            unitType.Add(Regex.Replace(item.Label, "performance fee", string.Empty, RegexOptions.IgnoreCase).Trim());
                    //        }
                    //    }
                    //    unitType1 = unitType.Distinct().ToList();
                    //    getUnitTypeByFundIdResDTO.UnitType = unitType1;
                    //}
                    unitType1 = _fundDynamicFieldBLL.GetUnitTypeList(FundDetails.Id);
                    getUnitTypeByFundIdResDTO.UnitType = unitType1;
                    if (getUnitTypeByFundIdResDTO.UnitType.Count > 0)
                    {
                        commonResponse.Message = "Success.";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = getUnitTypeByFundIdResDTO;
                    }
                    else
                    {
                        commonResponse.Message = "Data Not Found";
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    commonResponse.Message = "Fund Does not exist.";
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
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