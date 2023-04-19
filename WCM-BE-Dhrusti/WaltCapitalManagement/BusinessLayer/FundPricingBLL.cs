using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DataLayer.Entities;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Helper.Models;
using System.Transactions;

namespace BusinessLayer
{
    public class FundPricingBLL
    {
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _iConfiguration;
        private readonly WaltCapitalDBContext _dbContext;
        private readonly FundDynamicInputPriceBLL _fundDynamicInputPriceBLL;
        private readonly FundDynamicFieldBLL _fundDynamicFieldBLL;
        private readonly FundFeeCalculationDetailBLL _fundFeeCalculationDetailBLL;

        public FundPricingBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration iConfiguration, FundDynamicInputPriceBLL fundDynamicInputPriceBLL, FundDynamicFieldBLL fundDynamicFieldBLL, FundFeeCalculationDetailBLL fundFeeCalculationDetailBLL)
        {
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _iConfiguration = iConfiguration;
            _dbContext = dbContext;
            _fundDynamicInputPriceBLL = fundDynamicInputPriceBLL;
            _fundDynamicFieldBLL = fundDynamicFieldBLL;
            _fundFeeCalculationDetailBLL = fundFeeCalculationDetailBLL;
        }

        //public CommonResponse GetPricing(GetPricingReqDTO getPricingReqDTO)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        GetPricingResDTO getPricingResDTO = new GetPricingResDTO();
        //        var FundList = _commonRepo.fundList();
        //        var FundDetail = FundList.FirstOrDefault(x => x.Id == getPricingReqDTO.FundId && x.IsActive == true);
        //        if (FundDetail != null)
        //        {
        //            List<string> PricingInputs = FundDetail != null && !string.IsNullOrWhiteSpace(FundDetail.PricingInputs) ? FundDetail.PricingInputs.Trim().Split(',').ToList() : new List<string>();

        //            List<TableHeaderModel> tableHeaderModelList = new List<TableHeaderModel>();
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "date", Label = "Date" });

        //            foreach (var item in PricingInputs)
        //            {
        //                if (!string.IsNullOrWhiteSpace(item.Trim()))
        //                {
        //                    tableHeaderModelList.Add(new TableHeaderModel { Value = item.ToLower(), Label = item });
        //                }
        //            }
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "unittype", Label = "Unit Type" });
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "total", Label = "Total" });
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "units", Label = "Units" });
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "hwm", Label = "HWM" });
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "manFees", Label = "Man. fees" });
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "perfFees", Label = "Perf. fees" });
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "complianceFees", Label = "Compliance fees" });
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "auditFees", Label = "Audit fees" });
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "ifaFee", Label = "IFA fee" });
        //            //tableHeaderModelList.Add(new TableHeaderModel { Value = "nav", Label = "Nav" });
        //            tableHeaderModelList.Add(new TableHeaderModel { Value = "unitpricenav", Label = "Unit Price Nav" });


        //            getPricingResDTO.HeaderList = tableHeaderModelList;
        //            getPricingResDTO.TableDataList = new List<Dictionary<string, string>>();

        //            Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
        //            keyValuePair.Add("date", FundDetail != null ? FundDetail.InceptionDate.ToString("yyyy-MM-dd") : "");
        //            foreach (var item in PricingInputs)
        //            {
        //                if (!string.IsNullOrWhiteSpace(item.Trim()))
        //                {
        //                    keyValuePair.Add(item.ToLower(), item);
        //                }
        //            }
        //            keyValuePair.Add("unittype", "-");
        //            keyValuePair.Add("total", "0");
        //            keyValuePair.Add("units", "0");
        //            keyValuePair.Add("hwm", "0");
        //            keyValuePair.Add("manFees", "0");
        //            keyValuePair.Add("perfFees", "0");
        //            keyValuePair.Add("complianceFees", "0");
        //            keyValuePair.Add("auditFees", "0");
        //            keyValuePair.Add("ifaFee", "0");
        //            //keyValuePair.Add("nav", "0");
        //            keyValuePair.Add("unitpricenav", Convert.ToString(FundDetail.UnitStartingPrice));
        //            getPricingResDTO.TableDataList.Add(keyValuePair);
        //        }
        //        else
        //        {
        //            getPricingResDTO.HeaderList = new List<TableHeaderModel>();
        //            getPricingResDTO.TableDataList = new List<Dictionary<string, string>>();
        //        }
        //        commonResponse.Message = "Success";
        //        commonResponse.Status = true;
        //        commonResponse.StatusCode = HttpStatusCode.OK;
        //        commonResponse.Data = getPricingResDTO;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return commonResponse;
        //}

        public CommonResponse GetPricing(GetPricingReqDTO getPricingReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetPricingResDTO getPricingResDTO = new GetPricingResDTO();
                //DateTime TransactionDate = _commonHelper.GetDateFromString(getPricingReqDTO.TransactionDate);
                string DATE_FORMAT = "dd/MMM/yyyy";
                DateTime? TransactionDate = null;
                var FundList = _commonRepo.fundList();
                var PricingList = _commonRepo.GetPricingList(getPricingReqDTO.FundId);
                if (getPricingReqDTO.TransactionDate != null)
                {
                    TransactionDate = DateTime.ParseExact(getPricingReqDTO.TransactionDate, DATE_FORMAT, CultureInfo.InvariantCulture);
                }
                else
                {
                    TransactionDate = PricingList.OrderByDescending(x => x.TransactionDate.Value.Date).FirstOrDefault().TransactionDate.Value;
                }

                var FundDetail = FundList.FirstOrDefault(x => x.Id == getPricingReqDTO.FundId);
                if (FundDetail != null)
                {
                    //var DynamicPricingInputList = _commonRepo.GetFundDynamicInputPriceList(getPricingReqDTO.FundId).Where(x => x.BalanceDate.Date == TransactionDate.Date).ToList();
                    var DynamicPricingInputList = _commonRepo.GetFundDynamicInputPriceList(getPricingReqDTO.FundId).Where(x => x.IsAddedFromPricing == true).ToList();
                    if (getPricingReqDTO.TransactionDate != null)
                    {
                        DynamicPricingInputList = DynamicPricingInputList.Where(x => x.BalanceDate.Date <= TransactionDate.Value.Date).ToList();
                    }
                    List<string> PricingInputs = FundDetail != null && !string.IsNullOrWhiteSpace(FundDetail.PricingInputs) ? FundDetail.PricingInputs.Trim().Split(',').ToList() : new List<string>();

                    List<TableHeaderModel> tableHeaderModelList = new List<TableHeaderModel>();

                    tableHeaderModelList.Add(new TableHeaderModel { Value = "date", Label = "Date" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "unitpricenav", Label = "Unit Price Nav" });
                    foreach (var item in PricingInputs)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            tableHeaderModelList.Add(new TableHeaderModel { Value = item.ToLower(), Label = item });
                        }
                    }
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "unittype", Label = "Unit Type" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "total", Label = "Total" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "theoriticalValue", Label = "Theor. Value" });

                    tableHeaderModelList.Add(new TableHeaderModel { Value = "manFees", Label = "Man. fees" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "perfFees", Label = "Perf. fees" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "complianceFees", Label = "Compliance fees" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "auditFees", Label = "Audit fees" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "ifaFee", Label = "IFA fee" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "feesPayable", Label = "Fees Payable" });

                    tableHeaderModelList.Add(new TableHeaderModel { Value = "hwm", Label = "HWM" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "totalFeesDue", Label = "Total Fees Due" });
                    tableHeaderModelList.Add(new TableHeaderModel { Value = "units", Label = "Units" });

                    //tableHeaderModelList.Add(new TableHeaderModel { Value = "nav", Label = "Nav" });


                    getPricingResDTO.HeaderList = tableHeaderModelList;
                    getPricingResDTO.TableDataList = new List<Dictionary<string, string>>();

                    // Applying Fund Filters

                    if (getPricingReqDTO.TransactionDate != null)
                    {
                        PricingList = _commonRepo.GetPricingList(getPricingReqDTO.FundId).Where(x => x.TransactionDate.Value.Date <= TransactionDate.Value.Date);
                    }
                    if (getPricingReqDTO.FeeUnit.ToLower() != "all")
                    {
                        PricingList = PricingList.Where(x => x.UnitType.ToLower() == getPricingReqDTO.FeeUnit.ToLower());
                    }
                    var FilteredPricingList = PricingList.OrderByDescending(x => x.TransactionDate).ThenByDescending(x => x.Id).ToList();

                    //// Add Pricing List into Dynamic Pricing List to Show
                    //foreach (var priceItem in FilteredPricingList)
                    //{
                    //    Dictionary<string, string> keyValuePair = new Dictionary<string, string>();

                    //    keyValuePair.Add("date", priceItem.TransactionDate.Value.ToString("yyyy-MM-dd"));
                    //    foreach (var item in PricingInputs)
                    //    {
                    //        if (!string.IsNullOrWhiteSpace(item.Trim()))
                    //        {
                    //            decimal value = 0;
                    //            bool IsSumInputPricing = true;
                    //            foreach (var dynamicitem in DynamicPricingInputList)
                    //            {
                    //                if (item.ToLower() == dynamicitem.Label.ToLower() && priceItem.UnitType.ToLower() == dynamicitem.UnitType.ToLower())
                    //                {
                    //                    if (priceItem.TransactionDate.Value.Date == dynamicitem.BalanceDate.Date)
                    //                    {
                    //                        value = dynamicitem.Value;
                    //                        keyValuePair.Add(item.ToLower(), _commonHelper.GetFormatedDecimal(value));
                    //                        IsSumInputPricing = false;
                    //                        break;
                    //                    }
                    //                    else
                    //                    {
                    //                        value = value + dynamicitem.Value;
                    //                    }

                    //                }
                    //            }
                    //            if (IsSumInputPricing)
                    //                keyValuePair.Add(item.ToLower(), _commonHelper.GetFormatedDecimal(value));
                    //        }
                    //    }
                    //    keyValuePair.Add("unittype", priceItem.UnitType);
                    //    keyValuePair.Add("total", _commonHelper.GetFormatedDecimal(priceItem.DynPrcInpTotal));
                    //    keyValuePair.Add("theoriticalValue", _commonHelper.GetFormatedDecimal(priceItem.TotalTheorValue));

                    //    keyValuePair.Add("units", _commonHelper.GetFormatedDecimal(priceItem.Units));
                    //    keyValuePair.Add("hwm", _commonHelper.GetFormatedDecimal(priceItem.Hwm));
                    //    keyValuePair.Add("manFees", _commonHelper.GetFormatedDecimal(priceItem.ManFees));
                    //    keyValuePair.Add("perfFees", _commonHelper.GetFormatedDecimal(priceItem.PerfFees));


                    //    keyValuePair.Add("complianceFees", _commonHelper.GetFormatedDecimal(priceItem.ComplianceFees));
                    //    keyValuePair.Add("auditFees", _commonHelper.GetFormatedDecimal(priceItem.AuditFees));
                    //    keyValuePair.Add("ifaFee", _commonHelper.GetFormatedDecimal(priceItem.Ifafees));
                    //    keyValuePair.Add("feesPayable", _commonHelper.GetFormatedDecimal(priceItem.
                    //    FeesPayable));
                    //    keyValuePair.Add("totalFeesDue", _commonHelper.GetFormatedDecimal(priceItem.TotalFeesDue));

                    //    //keyValuePair.Add("nav", "0");
                    //    keyValuePair.Add("unitpricenav", _commonHelper.GetFormatedDecimal(priceItem.UnitPriceNav));

                    //    getPricingResDTO.TableDataList.Add(keyValuePair);
                    //}


                    // Add Pricing List into Dynamic Pricing List to Show
                    foreach (var priceItem in FilteredPricingList)
                    {
                        Dictionary<string, string> keyValuePair = new Dictionary<string, string>();

                        keyValuePair.Add("date", priceItem.TransactionDate.Value.ToString("yyyy-MM-dd"));
                        //keyValuePair.Add("unitpricenav", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.UnitPriceNav));
                        keyValuePair.Add("unitpricenav",priceItem.UnitPriceNav.ToString("0.000"));

                        foreach (var item in PricingInputs)
                        {
                            if (!string.IsNullOrWhiteSpace(item.Trim()))
                            {
                                decimal value = 0;
                                var CurrentDateData = DynamicPricingInputList.Where(x => x.BalanceDate.Date == priceItem.TransactionDate.Value.Date && x.Label.ToLower() == item.ToLower()).ToList();
                                foreach (var dynamicitem in CurrentDateData)
                                {
                                    if (item.ToLower() == dynamicitem.Label.ToLower() && priceItem.UnitType.ToLower() == dynamicitem.UnitType.ToLower() && priceItem.TransactionDate.Value.Date == dynamicitem.BalanceDate.Date)
                                    {
                                        value = value + dynamicitem.Value;
                                    }
                                }
                                keyValuePair.Add(item.ToLower(), _commonHelper.GetFormatedDecimalWithRounfOff(value));
                            }
                        }
                        keyValuePair.Add("unittype", priceItem.UnitType);
                        keyValuePair.Add("total", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.DynPrcInpTotal));
                        keyValuePair.Add("theoriticalValue", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.TotalTheorValue));

                        keyValuePair.Add("manFees", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.ManFees));
                        keyValuePair.Add("perfFees", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.PerfFees));
                        keyValuePair.Add("complianceFees", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.ComplianceFees));
                        keyValuePair.Add("auditFees", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.AuditFees));
                        keyValuePair.Add("ifaFee", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.Ifafees));
                        keyValuePair.Add("feesPayable", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.
                        FeesPayable));
                        keyValuePair.Add("hwm", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.Hwm));
                        keyValuePair.Add("totalFeesDue", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.TotalFeesDue));
                        keyValuePair.Add("units", _commonHelper.GetFormatedDecimalWithRounfOff(priceItem.Units));

                        //keyValuePair.Add("nav", "0");

                        getPricingResDTO.TableDataList.Add(keyValuePair);
                    }
                }
                else
                {
                    getPricingResDTO.HeaderList = new List<TableHeaderModel>();
                    getPricingResDTO.TableDataList = new List<Dictionary<string, string>>();
                }
                commonResponse.Message = "Success";
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Data = getPricingResDTO;
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse GetAddPricingDetail(GetAddPricingDetailReqDTO getAddPricingDetailReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            GetAddPricingDetailResDTO getAddPricingDetailResDTO = new GetAddPricingDetailResDTO();
            try
            {
                var fundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == getAddPricingDetailReqDTO.FundId && x.IsActive == true);
                if (fundDetail != null)
                {
                    List<string> PricingInputs = fundDetail != null && !string.IsNullOrWhiteSpace(fundDetail.PricingInputs) ? fundDetail.PricingInputs.Trim().Split(',').ToList() : new List<string>();

                    getAddPricingDetailResDTO.DynamicPricingInputFields = PricingInputs;

                    List<Dictionary<string, dynamic>> keyValuePairList = new List<Dictionary<string, dynamic>>();
                    foreach (var item in PricingInputs)
                    {
                        if (!string.IsNullOrWhiteSpace(item.Trim()))
                        {
                            Dictionary<string, dynamic> keyValuePair = new Dictionary<string, dynamic>();
                            RequiredModel requireModel = new RequiredModel();
                            RequiredData requiredData = new RequiredData();

                            requiredData.Value = true;
                            requiredData.Message = "Please Enter " + item.ToLower();
                            requireModel.Required = requiredData;
                            keyValuePair.Add(item.ToLower(), requireModel);

                            keyValuePairList.Add(keyValuePair);
                        }
                    }

                    getAddPricingDetailResDTO.DynamicPricingInputValidation = keyValuePairList;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getAddPricingDetailResDTO;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found.";
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        //public CommonResponse AddPricing(AddPricingReqDTO addPricingReqDTO)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        DateTime TransactionDate = _commonHelper.GetDateFromString(addPricingReqDTO.TransactionDate);
        //        var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == addPricingReqDTO.FundId && x.IsActive == true);
        //        if (FundDetail != null)
        //        {
        //            var PricingList = _commonRepo.GetPricingList(FundDetail.Id).ToList();
        //            var FundDynamicFieldList = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundDetail.Id).ToList();
        //            var TransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == FundDetail.Id && x.TransactionDate > FundDetail.InceptionDate).ToList();


        //            List<FundDynamicInputPriceMst> FundDynamicInputPriceMstList = new List<FundDynamicInputPriceMst>();
        //            foreach (var inputItem in addPricingReqDTO.DynamicPricingInputs)
        //            {
        //                foreach (var keyValueItem in inputItem)
        //                {
        //                    _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(FundDetail.Id, "", keyValueItem.Key, keyValueItem.Value, TransactionDate, addPricingReqDTO.CreatedBy);
        //                    FundDynamicInputPriceMst fundDynamicInputPriceMst = new FundDynamicInputPriceMst();
        //                    //fundDynamicInputPriceMst.FundId = FundDetail.Id;
        //                    fundDynamicInputPriceMst.Label = keyValueItem.Key;
        //                    fundDynamicInputPriceMst.Value = keyValueItem.Value;
        //                    //fundDynamicInputPriceMst.UnitType ="";
        //                    //fundDynamicInputPriceMst.BalanceDate = FundDetail.InceptionDate;
        //                    //fundDynamicInputPriceMst.IsActive = true;
        //                    //fundDynamicInputPriceMst.IsDeleted = false;
        //                    //fundDynamicInputPriceMst.IsAddedFromPricing = true;
        //                    //fundDynamicInputPriceMst.CreatedBy = addPricingReqDTO.CreatedBy;
        //                    //fundDynamicInputPriceMst.UpdatedBy = addPricingReqDTO.CreatedBy;
        //                    //fundDynamicInputPriceMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                    //fundDynamicInputPriceMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                    FundDynamicInputPriceMstList.Add(fundDynamicInputPriceMst);
        //                }
        //            }
        //            //_dbContext.FundDynamicInputPriceMsts.AddRange(FundDynamicInputPriceMstList);
        //            //_dbContext.SaveChanges();

        //            if (TransactionList.Count > 0)
        //            {
        //                var GroupByUnits = TransactionList.Select(x => x.UnitType).Distinct().ToList();
        //                decimal PricingInputTotal = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id).ToList().Select(x => x.Value).Sum();

        //                List<PricingMst> PricingMstList = new List<PricingMst>();
        //                foreach (var unit in GroupByUnits)
        //                {
        //                    if (!string.IsNullOrWhiteSpace(unit))
        //                    {
        //                        var Transactions = TransactionList.Where(x => x.UnitType == unit).ToList();

        //                        decimal UnitBalance = 0;
        //                        decimal AmountBalance = 0;
        //                        decimal CurrentUnitPrice = 0;
        //                        decimal TotalFees = 0;
        //                        decimal FundManagementFees = 0;
        //                        decimal FundPerformanceFees = 0;
        //                        decimal ManagementFees = 0;
        //                        decimal PerformanceFees = 0;

        //                        foreach (var trans in Transactions)
        //                        {
        //                            if (trans.TransactionType.ToLower() == "buy")
        //                            {
        //                                AmountBalance = AmountBalance + Convert.ToDecimal(trans.TransactionAmount);
        //                            }
        //                            else
        //                            {
        //                                AmountBalance = AmountBalance - Convert.ToDecimal(trans.TransactionAmount);
        //                            }
        //                        }
        //                        UnitBalance = AmountBalance / Convert.ToDecimal(FundDetail.UnitStartingPrice);  // formula : Total / Unit Price
        //                        CurrentUnitPrice = AmountBalance > 0 && UnitBalance > 0 ? AmountBalance / UnitBalance : 0;

        //                        if (unit.ToLower() == "unit a")
        //                        {
        //                            FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeA);
        //                            FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeA);
        //                        }
        //                        else if (unit.ToLower() == "unit b")
        //                        {
        //                            FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeB);
        //                            FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeB);
        //                        }
        //                        else
        //                        {
        //                            var FieldList = FundDynamicFieldList.Where(x => x.Label.Contains("management fee") && x.Label.Contains("performance fee")).ToList();
        //                            foreach (var item in FieldList)
        //                            {
        //                                if (item.Label.Contains("management fee") && item.Label == unit.ToLower())
        //                                {
        //                                    FundManagementFees = FundManagementFees + Convert.ToDecimal(item.Value);
        //                                }
        //                                if (item.Label.Contains("performance fee") && item.Label == unit.ToLower())
        //                                {
        //                                    FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(item.Value);
        //                                }
        //                            }
        //                        }
        //                        var TotalDays = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;
        //                        ManagementFees = (AmountBalance * FundManagementFees) / TotalDays; // formula : Total * Management fee of fund / 365
        //                        PerformanceFees = (FundPerformanceFees * ManagementFees) / TotalDays; // formula : performance fee * Management fee / 365

        //                        TotalFees = ManagementFees + PerformanceFees;

        //                        PricingMst pricingMst = new PricingMst();
        //                        pricingMst.FundId = FundDetail.Id;
        //                        pricingMst.InceptionDate = FundDetail.InceptionDate;
        //                        pricingMst.TransactionDate = _commonHelper.GetDateFromString(addPricingReqDTO.TransactionDate);
        //                        pricingMst.DynPrcInpFundId = FundDetail.Id;
        //                        pricingMst.UnitType = unit;
        //                        pricingMst.DynPrcInpTotal = AmountBalance;
        //                        pricingMst.Units = UnitBalance;
        //                        pricingMst.ManFees = ManagementFees;
        //                        pricingMst.PerfFees = PerformanceFees;

        //                        //pricingMst.Hwm = FundDetail.InceptionDate;

        //                        pricingMst.ComplianceFees = Convert.ToDecimal(FundDetail.ComplianceFee);
        //                        pricingMst.AuditFees = Convert.ToDecimal(FundDetail.AuditFee);
        //                        //pricingMst.Ifafees = FundDetail.InceptionDate;
        //                        //pricingMst.UnitPriceNav = FundDetail.InceptionDate;
        //                        pricingMst.IsActive = true;
        //                        pricingMst.IsDeleted = false;
        //                        pricingMst.CreatedBy = addPricingReqDTO.CreatedBy;
        //                        pricingMst.UpdatedBy = addPricingReqDTO.CreatedBy;
        //                        pricingMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                        pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                        PricingMstList.Add(pricingMst);
        //                    }
        //                }

        //                _dbContext.PricingMsts.AddRange(PricingMstList);
        //                _dbContext.SaveChanges();
        //            }
        //            else
        //            {
        //                var PricingDetail = PricingList.FirstOrDefault(x => x.TransactionDate.Value.Date == TransactionDate.Date);
        //                var DynPrcInpTotal = FundDynamicInputPriceMstList.Select(x => x.Value).Sum();
        //                PricingMst pricingMst = new PricingMst();
        //                if (PricingDetail != null)
        //                {
        //                    // Edit Mode


        //                    pricingMst = PricingDetail;
        //                    pricingMst.DynPrcInpTotal = DynPrcInpTotal;
        //                    pricingMst.UpdatedBy = addPricingReqDTO.CreatedBy;
        //                    pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
        //                    _dbContext.Entry(pricingMst).State = EntityState.Modified;
        //                }
        //                else
        //                {
        //                    // Add Mode

        //                    pricingMst.FundId = FundDetail.Id;
        //                    pricingMst.InceptionDate = FundDetail.InceptionDate;
        //                    pricingMst.TransactionDate = TransactionDate;
        //                    pricingMst.DynPrcInpFundId = FundDetail.Id;
        //                    pricingMst.UnitType = "";  // set default Unit Type = "";  
        //                    pricingMst.DynPrcInpTotal = DynPrcInpTotal;
        //                    pricingMst.Units = 0;
        //                    pricingMst.Hwm = 0;
        //                    pricingMst.ManFees = 0;
        //                    pricingMst.PerfFees = 0;
        //                    pricingMst.ComplianceFees = 0;
        //                    pricingMst.AuditFees = 0;
        //                    pricingMst.Ifafees = 0;
        //                    pricingMst.UnitPriceNav = 0;
        //                    pricingMst.IsActive = true;
        //                    pricingMst.IsDeleted = false;
        //                    pricingMst.CreatedBy = addPricingReqDTO.CreatedBy;
        //                    pricingMst.UpdatedBy = addPricingReqDTO.CreatedBy;
        //                    pricingMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                    pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                    _dbContext.PricingMsts.Add(pricingMst);

        //                }
        //                _dbContext.SaveChanges();
        //            }

        //            commonResponse.Status = true;
        //            commonResponse.StatusCode = HttpStatusCode.OK;
        //            commonResponse.Message = "Pricing Data Inserted Successfully.";
        //        }
        //        else
        //        {
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //            commonResponse.Message = "Data Not Found.";
        //        }
        //    }
        //    catch (Exception) { throw; }
        //    return commonResponse;
        //}

        //public CommonResponse AddPricing(AddPricingReqDTO addPricingReqDTO)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        DateTime TransactionDate = _commonHelper.GetDateFromString(addPricingReqDTO.TransactionDate);
        //        DateTime BalanceDate = TransactionDate;
        //        DateTime PrevBalanceDate = BalanceDate.Date.AddDays(-1);
        //        var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == addPricingReqDTO.FundId && x.IsActive == true);
        //        if (FundDetail != null)
        //        {
        //            var PricingList = _commonRepo.GetPricingList(FundDetail.Id).ToList();
        //            var FundDynamicFieldList = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundDetail.Id).ToList();
        //            var TransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == FundDetail.Id).ToList();
        //            var TransactionList1 = TransactionList.Where(x => x.TransactionDate > FundDetail.InceptionDate).ToList();
        //            var ClientList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == 2).ToList();

        //            var CurrentFeesDetail = _commonRepo.FundFeeCalculationList(FundDetail.Id, BalanceDate).FirstOrDefault();
        //            var PrevFeesDetail = _commonRepo.FundFeeCalculationList(FundDetail.Id, PrevBalanceDate).FirstOrDefault();
        //            var PricingInputValueList = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id);

        //            var dynamicUnitsFieldsList = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundDetail.Id && x.Label.ToLower().Contains("management fee") && x.Label.ToLower().Contains("performance fee")).ToList();

        //            List<FundDynamicInputPriceMst> FundDynamicInputPriceMstList = new List<FundDynamicInputPriceMst>();
        //            foreach (var inputItem in addPricingReqDTO.DynamicPricingInputs)
        //            {
        //                foreach (var keyValueItem in inputItem)
        //                {
        //                    _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(FundDetail.Id, "", keyValueItem.Key, keyValueItem.Value, TransactionDate, addPricingReqDTO.CreatedBy);
        //                    FundDynamicInputPriceMst fundDynamicInputPriceMst = new FundDynamicInputPriceMst();
        //                    fundDynamicInputPriceMst.Label = keyValueItem.Key;
        //                    fundDynamicInputPriceMst.Value = keyValueItem.Value;

        //                    FundDynamicInputPriceMstList.Add(fundDynamicInputPriceMst);
        //                }
        //            }

        //            if (TransactionList1.Count > 0)
        //            {
        //                var GroupByUnits = TransactionList1.Select(x => x.UnitType).Distinct().ToList();
        //                decimal PricingInputTotal = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id).ToList().Select(x => x.Value).Sum();

        //                List<PricingMst> PricingMstList = new List<PricingMst>();
        //                foreach (var unit in GroupByUnits)
        //                {
        //                    if (!string.IsNullOrWhiteSpace(unit))
        //                    {
        //                        // Variable Declarations------------------------------------

        //                        decimal PrevTotalFeesDue = 0;
        //                        decimal PrevFeesPayable = 0;
        //                        decimal PrevFeesPaid = 0;
        //                        decimal PrevHWM = 0;
        //                        decimal PrevPostUnits = 0;
        //                        decimal PrevUnitPrice = 0;
        //                        decimal CurrBankBalance = 0;
        //                        decimal CurrTSTotalValue = 0;
        //                        decimal CurrTSTheoValue = 0;
        //                        decimal CurrTotalTheorValue = 0;
        //                        decimal StartValue = 0;
        //                        decimal ProfitOffHWM = 0;

        //                        //Fees
        //                        decimal FundManagementFees = 0;
        //                        decimal FundPerformanceFees = 0;
        //                        decimal ManagementFees = 0;
        //                        decimal PerformanceFees = 0;
        //                        decimal IFAInitialFee = 0;
        //                        decimal IFAAnnualFee = 0;
        //                        decimal IFAFees = 0;
        //                        decimal ComplianceFees = 0;
        //                        decimal AuditFee = 0;

        //                        decimal CurrFeesPayable = 0;
        //                        decimal CurrFeesPaid = 0;
        //                        decimal CurrTotalFeesDue = 0;
        //                        decimal CurrHWM = 0;
        //                        decimal PreUnits = 0;
        //                        decimal UnitChanges = 0;
        //                        decimal PostUnits = 0;
        //                        decimal CurrUnitBalance = 0;
        //                        decimal CurrUnitPrice = 0;

        //                        decimal ServiceFee = 0;
        //                        decimal BankToTradeStationTransferFee = 0;
        //                        decimal TotalPricingInputValues = 0;
        //                        //decimal BODAccountBalance = 0;
        //                        //decimal BODMarketValue = 0;
        //                        decimal TransactionBalance = 0;
        //                        bool EditMode = false;


        //                        var CurrentYearTotalDays = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

        //                        // Data Fetching -------------------------------------------
        //                        //var TransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == FundId).ToList();
        //                        TransactionList = (from t in TransactionList
        //                                           join c in ClientList on t.Client equals c.Id
        //                                           select t).ToList();
        //                        var Transactions = TransactionList;


        //                        var HasFirstTransaction = TransactionList.Where(x => x.UnitType.ToLower() == unit.ToLower()).Count() > 1 ? true : false;
        //                        //var TransactionDetail = TransactionList.FirstOrDefault(x => x.Id == TransactionId);
        //                        var TransactionDetail = TransactionList.FirstOrDefault(x => x.UnitType.ToLower() == unit.ToLower());
        //                        var HasCurrentFeesDetail = _commonRepo.GetFundFeeCalculationList(FundDetail.Id).FirstOrDefault() != null ? true : false;
        //                        // Assigning Values to Variables
        //                        EditMode = CurrentFeesDetail != null ? true : false;
        //                        //TotalPricingInputValues = PricingInputValueList.Where(x => x.BalanceDate.Date == BalanceDate.Date && x.UnitType.ToLower() == UnitType.ToLower() && x.IsAddedFromPricing == false).Select(x => x.Value).Sum();
        //                        TotalPricingInputValues = PricingInputValueList.Where(x => x.UnitType.ToLower() == unit.ToLower() && x.IsAddedFromPricing == false).Select(x => x.Value).Sum();
        //                        var InputAddPricingTotal = PricingInputValueList.Where(x => x.BalanceDate.Date == BalanceDate.Date && x.UnitType.ToLower() == unit.ToLower() && x.IsAddedFromPricing == true).Select(x => x.Value).Sum();

        //                        if (PrevFeesDetail != null)
        //                        {
        //                            PrevTotalFeesDue = PrevFeesDetail.TotalFeesDue;
        //                            PrevFeesPayable = PrevFeesDetail.FeesPayable;
        //                            PrevFeesPaid = PrevFeesDetail.FeesPaid;
        //                            PrevHWM = PrevFeesDetail.Hwm;
        //                            PrevPostUnits = PrevFeesDetail.PostUnits;
        //                            PrevUnitPrice = PrevFeesDetail.UnitPrice;
        //                        }

        //                        if (!HasFirstTransaction)
        //                        {
        //                            PrevPostUnits = TransactionDetail != null ? Convert.ToDecimal(TransactionDetail.NumberOfUnits) : 0;
        //                        }

        //                        if (!HasCurrentFeesDetail)
        //                        {
        //                            PrevUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                        }

        //                        if (unit.ToLower() == "unit a")
        //                        {
        //                            if (FundDetail.ManagementFeeA > 0)
        //                                FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeA);
        //                            if (FundDetail.PerformanceFeeA > 0)
        //                                FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeA);
        //                        }
        //                        else if (unit.ToLower() == "unit b")
        //                        {
        //                            if (FundDetail.ManagementFeeB > 0)
        //                                FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeB);
        //                            if (FundDetail.PerformanceFeeB > 0)
        //                                FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeB);
        //                        }
        //                        else
        //                        {
        //                            foreach (var item in dynamicUnitsFieldsList)
        //                            {
        //                                if (Convert.ToDecimal(item.Value) > 0)
        //                                {
        //                                    if (item.Label.Contains("management fee") && item.Label == unit.ToLower())
        //                                    {
        //                                        FundManagementFees = FundManagementFees + Convert.ToDecimal(item.Value);
        //                                    }

        //                                    if (item.Label.Contains("performance fee") && item.Label == unit.ToLower())
        //                                    {
        //                                        if (Convert.ToDecimal(item.Value) > 0)
        //                                            FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(item.Value);
        //                                    }
        //                                }
        //                            }
        //                        }

        //                        CurrBankBalance = TotalPricingInputValues + ServiceFee + BankToTradeStationTransferFee;
        //                        //CurrTSTotalValue = BODAccountBalance + BODMarketValue;
        //                        CurrTSTotalValue = InputAddPricingTotal;
        //                        CurrTSTheoValue = CurrTSTotalValue - PrevTotalFeesDue;
        //                        CurrTotalTheorValue = CurrBankBalance + CurrTSTheoValue;
        //                        StartValue = PrevHWM;
        //                        ProfitOffHWM = ((CurrTotalTheorValue - StartValue) - TotalPricingInputValues) < 0 ? 0 : ((CurrTotalTheorValue - StartValue) - TotalPricingInputValues);
        //                        if (CurrTotalTheorValue > 0 && FundManagementFees > 0)
        //                            ManagementFees = ((CurrTotalTheorValue * FundManagementFees) / 100) / CurrentYearTotalDays;

        //                        PerformanceFees = ProfitOffHWM > 0 ? (ProfitOffHWM * FundPerformanceFees) / 100 : 0;

        //                        foreach (var trans in Transactions)
        //                        {
        //                            if (trans.TransactionType.ToLower() == "buy")
        //                            {
        //                                if (trans.TransactionAmount > 0)
        //                                    TransactionBalance = TransactionBalance + Convert.ToDecimal(trans.TransactionAmount);
        //                            }
        //                            else
        //                            {
        //                                if (trans.TransactionAmount > 0)
        //                                    TransactionBalance = TransactionBalance - Convert.ToDecimal(trans.TransactionAmount);
        //                            }

        //                            if (trans.IfaupFrontFee > 0 && CurrTotalTheorValue > 0)
        //                                IFAInitialFee = IFAInitialFee + ((CurrTotalTheorValue * Convert.ToDecimal(trans.IfaupFrontFee)) / 100);
        //                            if (trans.IfaAnnualFee > 0 && CurrTotalTheorValue > 0)
        //                            {
        //                                decimal IFAnualFeePercentage = (CurrTotalTheorValue * Convert.ToDecimal(trans.IfaAnnualFee)) / 100;
        //                                if (IFAnualFeePercentage > 0)
        //                                    IFAAnnualFee = IFAAnnualFee + (IFAnualFeePercentage / CurrentYearTotalDays);
        //                            }
        //                        }

        //                        AuditFee = Convert.ToDecimal(FundDetail.AuditFee);
        //                        ComplianceFees = Convert.ToDecimal(FundDetail.ComplianceFee);

        //                        if (ComplianceFees > 0)
        //                            ComplianceFees = ComplianceFees / CurrentYearTotalDays;
        //                        if (AuditFee > 0)
        //                            AuditFee = AuditFee / CurrentYearTotalDays;

        //                        IFAFees = IFAInitialFee + IFAAnnualFee;

        //                        CurrFeesPayable = ManagementFees + PerformanceFees + AuditFee + ComplianceFees + IFAInitialFee + IFAAnnualFee;
        //                        //CurrFeesPaid =
        //                        CurrTotalFeesDue = (PrevTotalFeesDue + CurrFeesPayable) - CurrFeesPaid;
        //                        CurrHWM = CurrTotalTheorValue < (StartValue + TotalPricingInputValues) ? ((StartValue + TotalPricingInputValues) - CurrFeesPayable) : (CurrTotalTheorValue - CurrFeesPayable);
        //                        if (HasFirstTransaction)
        //                            PreUnits = PrevPostUnits;
        //                        if (TotalPricingInputValues > 0 && PrevUnitPrice > 0)
        //                            UnitChanges = TotalPricingInputValues / PrevUnitPrice;
        //                        PostUnits = PreUnits + UnitChanges;
        //                        CurrUnitBalance = PostUnits;
        //                        if (CurrTotalTheorValue > 0 && CurrUnitBalance > 0)
        //                            CurrUnitPrice = CurrTotalTheorValue / CurrUnitBalance;

        //                        // Adding Or Udating Table Using latest Values
        //                        PricingMst pricingMst = new PricingMst();
        //                        pricingMst.FundId = FundDetail.Id;
        //                        pricingMst.InceptionDate = FundDetail.InceptionDate;
        //                        pricingMst.TransactionDate = _commonHelper.GetDateFromString(addPricingReqDTO.TransactionDate);
        //                        pricingMst.DynPrcInpFundId = FundDetail.Id;
        //                        pricingMst.UnitType = unit;
        //                        pricingMst.DynPrcInpTotal = CurrBankBalance;
        //                        pricingMst.Units = PostUnits;
        //                        pricingMst.ManFees = ManagementFees;
        //                        pricingMst.PerfFees = PerformanceFees;
        //                        pricingMst.Hwm = CurrHWM;
        //                        pricingMst.ComplianceFees = ComplianceFees;
        //                        pricingMst.AuditFees = AuditFee;
        //                        pricingMst.TotalTheorValue = CurrTotalTheorValue;
        //                        pricingMst.FeesPayable = CurrFeesPayable;
        //                        pricingMst.TotalFeesDue = CurrTotalFeesDue;
        //                        //pricingMst.Ifafees = FundDetail.InceptionDate;
        //                        //pricingMst.UnitPriceNav = FundDetail.InceptionDate;
        //                        pricingMst.IsActive = true;
        //                        pricingMst.IsDeleted = false;
        //                        pricingMst.CreatedBy = addPricingReqDTO.CreatedBy;
        //                        pricingMst.UpdatedBy = addPricingReqDTO.CreatedBy;
        //                        pricingMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                        pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                        PricingMstList.Add(pricingMst);
        //                    }
        //                }

        //                _dbContext.PricingMsts.AddRange(PricingMstList);
        //                _dbContext.SaveChanges();
        //            }
        //            else
        //            {
        //                var PricingDetail = PricingList.FirstOrDefault(x => x.TransactionDate.Value.Date == TransactionDate.Date);
        //                var DynPrcInpTotal = FundDynamicInputPriceMstList.Select(x => x.Value).Sum();
        //                PricingMst pricingMst = new PricingMst();
        //                if (PricingDetail != null)
        //                {
        //                    // Edit Mode


        //                    pricingMst = PricingDetail;
        //                    pricingMst.DynPrcInpTotal = DynPrcInpTotal;
        //                    pricingMst.UpdatedBy = addPricingReqDTO.CreatedBy;
        //                    pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
        //                    _dbContext.Entry(pricingMst).State = EntityState.Modified;
        //                }
        //                else
        //                {
        //                    // Add Mode

        //                    pricingMst.FundId = FundDetail.Id;
        //                    pricingMst.InceptionDate = FundDetail.InceptionDate;
        //                    pricingMst.TransactionDate = TransactionDate;
        //                    pricingMst.DynPrcInpFundId = FundDetail.Id;
        //                    pricingMst.UnitType = "";  // set default Unit Type = "";  
        //                    pricingMst.DynPrcInpTotal = DynPrcInpTotal;
        //                    pricingMst.Units = 0;
        //                    pricingMst.Hwm = 0;
        //                    pricingMst.ManFees = 0;
        //                    pricingMst.PerfFees = 0;
        //                    pricingMst.ComplianceFees = 0;
        //                    pricingMst.AuditFees = 0;
        //                    pricingMst.Ifafees = 0;
        //                    pricingMst.UnitPriceNav = 0;
        //                    pricingMst.IsActive = true;
        //                    pricingMst.IsDeleted = false;
        //                    pricingMst.CreatedBy = addPricingReqDTO.CreatedBy;
        //                    pricingMst.UpdatedBy = addPricingReqDTO.CreatedBy;
        //                    pricingMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                    pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                    _dbContext.PricingMsts.Add(pricingMst);

        //                }
        //                _dbContext.SaveChanges();
        //            }

        //            commonResponse.Status = true;
        //            commonResponse.StatusCode = HttpStatusCode.OK;
        //            commonResponse.Message = "Pricing Data Inserted Successfully.";
        //        }
        //        else
        //        {
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //            commonResponse.Message = "Data Not Found.";
        //        }
        //    }
        //    catch (Exception) { throw; }
        //    return commonResponse;
        //}

        public CommonResponse AddPricing(AddPricingReqDTO addPricingReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                DateTime TransactionDate = _commonHelper.GetDateFromString(addPricingReqDTO.TransactionDate);
                var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == addPricingReqDTO.FundId && x.IsActive.Value);
                if (FundDetail != null)
                {
                    var PricingDetail = _commonRepo.GetPricingList(FundDetail.Id).FirstOrDefault(x => x.TransactionDate.Value.Date == TransactionDate.Date);
                    bool IsTransactionCompleted = false;
                    decimal TotalValue = 0;
                    if (PricingDetail == null)
                    {
                        using (var scope = new TransactionScope())
                        {
                            //Add / Update Dynamic Input pricing table 
                            var list = addPricingReqDTO.DynamicPricingInputs.FirstOrDefault();
                            //foreach (var dynamicInputItem in addPricingReqDTO.DynamicPricingInputs)
                            //{
                            foreach (var keyValueItem in list)
                            {
                                //TotalValue += keyValueItem.Value;
                                List<UnitTypePercentageDetail> UnitTypeOwnershipPercentageList = GetFundOwnershipUnitWise(FundDetail.Id, keyValueItem.Value, keyValueItem.Key).Status ? GetFundOwnershipUnitWise(FundDetail.Id, keyValueItem.Value, keyValueItem.Key).Data : new List<UnitTypePercentageDetail>();

                                foreach (var unitTypeItem in UnitTypeOwnershipPercentageList)
                                {
                                    _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(FundDetail.Id, unitTypeItem.UnitType, keyValueItem.Key, unitTypeItem.UnitTypeValue, TransactionDate, addPricingReqDTO.CreatedBy, true);
                                }
                            }
                            //}

                            List<UnitTypePercentageDetail> UnitTypeList = GetFundOwnershipUnitWise(FundDetail.Id, 0).Status ? GetFundOwnershipUnitWise(FundDetail.Id, 0).Data : new List<UnitTypePercentageDetail>();

                            var FundDynamicInputList = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id).Where(x => x.BalanceDate.Date == TransactionDate.Date).ToList();
                            List<PricingMst> PricingMstList = new List<PricingMst>();
                            foreach (var unitTypeItem in UnitTypeList)
                            {
                                TotalValue = FundDynamicInputList.Where(x => x.UnitType.ToLower() == unitTypeItem.UnitType.ToLower() && x.IsAddedFromPricing).Select(x => x.Value).Sum();
                                var res = _fundFeeCalculationDetailBLL.AddUpdateFeeCalculationDetail(FundDetail.Id, unitTypeItem.UnitType, TransactionDate, addPricingReqDTO.CreatedBy, true);
                                if (!string.IsNullOrWhiteSpace(unitTypeItem.UnitType))
                                {
                                    // Add / Update Fund Fees Calculation Details 
                                    var FeeCalculationDetail = _commonRepo.FundFeeCalculationList(FundDetail.Id, TransactionDate).ToList().FirstOrDefault(x => x.UnitType.ToLower() == unitTypeItem.UnitType.ToLower());

                                    // Add Pricing table 
                                    if (FeeCalculationDetail != null)
                                    {
                                        PricingMst pricingMst = new PricingMst();
                                        pricingMst.FundId = FundDetail.Id;
                                        pricingMst.InceptionDate = FundDetail.InceptionDate;
                                        pricingMst.TransactionDate = TransactionDate;
                                        pricingMst.DynPrcInpFundId = FundDetail.Id;
                                        pricingMst.UnitType = unitTypeItem.UnitType;
                                        //pricingMst.DynPrcInpTotal = FeeCalculationDetail.BankBalance;
                                        pricingMst.DynPrcInpTotal = TotalValue;
                                        pricingMst.Units = FeeCalculationDetail.PostUnits;
                                        pricingMst.Hwm = FeeCalculationDetail.Hwm;
                                        pricingMst.ManFees = FeeCalculationDetail.ManFees;
                                        pricingMst.PerfFees = FeeCalculationDetail.PerfFees;
                                        pricingMst.ComplianceFees = FeeCalculationDetail.ComplianceFees;
                                        pricingMst.AuditFees = FeeCalculationDetail.AuditFees;
                                        pricingMst.Ifafees = FeeCalculationDetail.Ifafees;
                                        pricingMst.UnitPriceNav = FeeCalculationDetail.UnitPriceNav;
                                        pricingMst.IsActive = true;
                                        pricingMst.IsDeleted = false;
                                        pricingMst.CreatedBy = addPricingReqDTO.CreatedBy;
                                        pricingMst.UpdatedBy = addPricingReqDTO.CreatedBy;
                                        pricingMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                        pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                        pricingMst.TotalTheorValue = FeeCalculationDetail.TotalTheorValue;
                                        pricingMst.FeesPayable = FeeCalculationDetail.FeesPayable;
                                        pricingMst.TotalFeesDue = FeeCalculationDetail.TotalFeesDue;

                                        PricingMstList.Add(pricingMst);
                                    }
                                }
                            }
                            _dbContext.PricingMsts.AddRange(PricingMstList);
                            _dbContext.SaveChanges();
                            IsTransactionCompleted = true;

                            if (IsTransactionCompleted)
                            {
                                scope.Complete();
                                commonResponse.Status = true;
                                commonResponse.StatusCode = HttpStatusCode.OK;
                                commonResponse.Message = "Add Pricing Successfully!";
                                commonResponse.Data = PricingMstList;
                            }
                        }
                    }
                    else
                    {
                        commonResponse.Message = "Already Added Data In Pricing For A Day !";
                    }
                }
                else
                {
                    commonResponse.Message = "Fund Not Found!";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        public CommonResponse GetEditPricingDetail(GetEditPricingDetailReqDTO getEditPricingDetailReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetEditPricingDetailResDTO getEditPricingDetailResDTO = new GetEditPricingDetailResDTO();
                DateTime TransactionDate = getEditPricingDetailReqDTO.TransactionDate;
                var fundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == getEditPricingDetailReqDTO.FundId && x.IsActive == true);
                if (fundDetail != null)
                {
                    var DynamicInputList = _commonRepo.GetFundDynamicInputPriceList(fundDetail.Id).Where(x => x.BalanceDate.Date == TransactionDate.Date && x.IsAddedFromPricing).ToList();
                    //var PricingList = _commonRepo.GetPricingList(fundDetail.Id).Where(x => x.TransactionDate.Value.Date == TransactionDate.Date).ToList();

                    List<string> PricingInputs = fundDetail != null && !string.IsNullOrWhiteSpace(fundDetail.PricingInputs) ? fundDetail.PricingInputs.Trim().Split(',').ToList() : new List<string>();

                    getEditPricingDetailResDTO.DynamicPricingInputFields = PricingInputs;
                    decimal InputValue = 0;
                    List<Dictionary<string, dynamic>> keyValuePairList = new List<Dictionary<string, dynamic>>();
                    foreach (var item in PricingInputs)
                    {
                        if (!string.IsNullOrWhiteSpace(item.Trim()))
                        {
                            InputValue = DynamicInputList.Where(x => x.Label.ToLower() == item.ToLower()).Select(x => x.Value).Sum();
                            //InputValue = PricingList.Where(x => x.UnitType.ToLower() == item.ToLower()).Select(x => x.DynPrcInpTotal).Sum();
                            Dictionary<string, dynamic> keyValuePair = new Dictionary<string, dynamic>();

                            RequiredData requiredData = new RequiredData();
                            requiredData.Value = true;
                            requiredData.Message = "Please Enter " + item.ToLower();

                            RequiredDataModel requireDataModel = new RequiredDataModel();
                            requireDataModel.Required = requiredData;
                            requireDataModel.InputValue = InputValue;

                            keyValuePair.Add(item.ToLower(), requireDataModel);

                            keyValuePairList.Add(keyValuePair);
                        }
                    }

                    getEditPricingDetailResDTO.DynamicPricingInputValidation = keyValuePairList;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getEditPricingDetailResDTO;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found.";
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        public CommonResponse UpdatePricing(UpdatePricingReqDTO updatePricingReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                DateTime TransactionDate = _commonHelper.GetDateFromString(updatePricingReqDTO.TransactionDate);
                DateTime PrevTransactionDate = TransactionDate.AddDays(-1);
                bool IsTransactionCompleted = false;
                var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == updatePricingReqDTO.FundId && x.IsActive.Value);
                if (FundDetail != null)
                {
                    var PricingList = _commonRepo.GetPricingList(FundDetail.Id).Where(x => x.TransactionDate.Value.Date >= TransactionDate.Date).OrderBy(x => x.TransactionDate).ToList();
                    var TransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == FundDetail.Id && x.TransactionDate.Date >= TransactionDate.Date).OrderBy(x => x.TransactionDate).ThenBy(x => x.Id).ToList();
                    var DynamicInputPricingList = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id).ToList();

                    if (PricingList.Count > 0)
                    {
                        using (var scope = new TransactionScope())
                        {
                            decimal TotalValue = 0;

                            foreach (var item in PricingList.Select(x => x.TransactionDate.Value).Distinct().ToList())
                            {
                                Dictionary<string, decimal> DynamicInputList = new Dictionary<string, decimal>();
                                if (item.Date == TransactionDate.Date)
                                {
                                    DynamicInputList = updatePricingReqDTO.DynamicPricingInputs.FirstOrDefault();
                                    TransactionDate = item;
                                }
                                else
                                {
                                    TransactionDate = item;

                                    var DynamicInputFilteredList = DynamicInputPricingList.Where(x => x.BalanceDate.Date == item.Date && x.IsAddedFromPricing).ToList();
                                    var DynamicInputKeyList = DynamicInputFilteredList.Select(x => x.Label).Distinct().ToList();


                                    //Dictionary<string, decimal> keyValuePairs = new Dictionary<string, decimal>();
                                    decimal InputValue = 0;
                                    foreach (var keyItem in DynamicInputKeyList)
                                    {
                                        //keyValuePairs = new Dictionary<string, decimal>();
                                        InputValue = DynamicInputFilteredList.Where(x => x.Label.ToLower() == keyItem.ToLower()).Select(x => x.Value).Sum();
                                        DynamicInputList.Add(keyItem, InputValue);
                                    }
                                }

                                // getting List of pricing Inputs (bank,stock)
                                if (DynamicInputList != null)
                                {
                                    // Get Fund Ownership From Pricing Input (bank,stock) for Unit-A, Unit-B etc.
                                    foreach (var keyValueItem in DynamicInputList)
                                    {
                                        List<UnitTypePercentageDetail> UnitTypeOwnershipPercentageList = GetFundOwnershipUnitWise(FundDetail.Id, keyValueItem.Value, keyValueItem.Key, TransactionDate).Status ? GetFundOwnershipUnitWise(FundDetail.Id, keyValueItem.Value, keyValueItem.Key, TransactionDate).Data : new List<UnitTypePercentageDetail>();

                                        // Update Dynamic Input Price Table for selected Date. 
                                        foreach (var unitTypeItem in UnitTypeOwnershipPercentageList)
                                        {
                                            _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(FundDetail.Id, unitTypeItem.UnitType, keyValueItem.Key, unitTypeItem.UnitTypeValue, TransactionDate, updatePricingReqDTO.UpdatedBy, true);
                                        }
                                    }
                                }

                                // Get Unit Type List. for e.g.: Unit-A, Unit-B etc.
                                List<UnitTypePercentageDetail> UnitTypeList = GetFundOwnershipUnitWise(FundDetail.Id, 0, null, TransactionDate).Status ? GetFundOwnershipUnitWise(FundDetail.Id, 0, null, TransactionDate).Data : new List<UnitTypePercentageDetail>();

                                var FundDynamicInputList = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id).Where(x => x.BalanceDate.Date == TransactionDate.Date).ToList();
                                List<PricingMst> PricingMstList = new List<PricingMst>();

                                foreach (var unitTypeItem in UnitTypeList)
                                {
                                    TotalValue = FundDynamicInputList.Where(x => x.UnitType.ToLower() == unitTypeItem.UnitType.ToLower() && x.IsAddedFromPricing == true).Select(x => x.Value).Sum();
                                    var res = _fundFeeCalculationDetailBLL.AddUpdateFeeCalculationDetail(FundDetail.Id, unitTypeItem.UnitType, TransactionDate, updatePricingReqDTO.UpdatedBy, true);

                                    if (!string.IsNullOrWhiteSpace(unitTypeItem.UnitType))
                                    {
                                        // Add / Update Fund Fees Calculation Details 
                                        var FeeCalculationDetail = _commonRepo.FundFeeCalculationList(FundDetail.Id, TransactionDate).ToList().FirstOrDefault(x => x.UnitType.ToLower() == unitTypeItem.UnitType.ToLower());

                                        // Update Pricing table for selected Date for All Unit Types
                                        if (FeeCalculationDetail != null)
                                        {
                                            var PricingDetail = PricingList.FirstOrDefault(x => x.TransactionDate.Value.Date == TransactionDate.Date && x.UnitType.ToLower() == unitTypeItem.UnitType.ToLower());

                                            if (PricingDetail != null)
                                            {
                                                PricingMst pricingMst = new PricingMst();

                                                pricingMst = PricingDetail;
                                                //pricingMst.FundId = FundDetail.Id;
                                                //pricingMst.InceptionDate = FundDetail.InceptionDate;
                                                //pricingMst.TransactionDate = TransactionDate;
                                                //pricingMst.DynPrcInpFundId = FundDetail.Id;
                                                //pricingMst.UnitType = unitTypeItem.UnitType;
                                                pricingMst.DynPrcInpTotal = TotalValue;
                                                pricingMst.Units = FeeCalculationDetail.PostUnits;
                                                pricingMst.Hwm = FeeCalculationDetail.Hwm;
                                                pricingMst.ManFees = FeeCalculationDetail.ManFees;
                                                pricingMst.PerfFees = FeeCalculationDetail.PerfFees;
                                                pricingMst.ComplianceFees = FeeCalculationDetail.ComplianceFees;
                                                pricingMst.AuditFees = FeeCalculationDetail.AuditFees;
                                                pricingMst.Ifafees = FeeCalculationDetail.Ifafees;
                                                pricingMst.UnitPriceNav = FeeCalculationDetail.UnitPriceNav;
                                                pricingMst.UpdatedBy = updatePricingReqDTO.UpdatedBy;
                                                pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                                pricingMst.TotalTheorValue = FeeCalculationDetail.TotalTheorValue;
                                                pricingMst.FeesPayable = FeeCalculationDetail.FeesPayable;
                                                pricingMst.TotalFeesDue = FeeCalculationDetail.TotalFeesDue;

                                                _dbContext.Entry(pricingMst).State = EntityState.Modified;
                                                _dbContext.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }

                            // Updating Client Transaction with new Unit Price and No of Units for selected Dates.
                            ClientTransactionMst clientTransactionMst = new ClientTransactionMst();
                            int ErrorCount = 0;
                            foreach (var item in TransactionList)
                            {
                                var PricingDetailOftransactionDate = PricingList.FirstOrDefault(x => x.TransactionDate.Value.Date == item.TransactionDate.Date && x.UnitType.ToLower() == item.UnitType.ToLower());
                                if (PricingDetailOftransactionDate != null)
                                {
                                    clientTransactionMst = new ClientTransactionMst();
                                    clientTransactionMst = item;

                                    clientTransactionMst.UnitPrice = Convert.ToDouble(PricingDetailOftransactionDate.UnitPriceNav);
                                    if (clientTransactionMst.UnitPrice > 0)
                                    {
                                        clientTransactionMst.NumberOfUnits = clientTransactionMst.TransactionAmount / clientTransactionMst.UnitPrice;

                                        _dbContext.Entry(clientTransactionMst).State = EntityState.Modified;
                                        _dbContext.SaveChanges();
                                    }
                                    else
                                    {
                                        ErrorCount++;
                                        break;
                                    }
                                }
                                else
                                {
                                    ErrorCount++;
                                    break;
                                }
                            }

                            if (ErrorCount == 0)
                            {
                                IsTransactionCompleted = true;
                            }

                            if (IsTransactionCompleted)
                            {
                                scope.Complete();
                                commonResponse.Status = true;
                                commonResponse.StatusCode = HttpStatusCode.OK;
                                commonResponse.Message = "Pricing Data Updated Successfully!";
                                //commonResponse.Data = PricingMstList;
                            }

                        }
                    }
                    else
                    {
                        commonResponse.Message = "Pricing Data Not Found For The Selected Date!";
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    commonResponse.Message = "Fund Not Found!";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }


        #region Private AddUpdatePricing
        public CommonResponse AddPricingWithBlankData(int FundId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId);
                if (FundDetail != null)
                {
                    //get UnitType List
                    var unitTypeList = _fundDynamicFieldBLL.GetUnitTypeList(FundDetail.Id, false);

                    List<PricingMst> pricingMstList = new List<PricingMst>();
                    PricingMst pricingMst = new PricingMst();

                    foreach (var item in unitTypeList)
                    {
                        pricingMst = new PricingMst();
                        pricingMst.FundId = FundDetail.Id;
                        pricingMst.InceptionDate = FundDetail.InceptionDate;
                        pricingMst.TransactionDate = FundDetail.InceptionDate;
                        pricingMst.DynPrcInpFundId = FundDetail.Id;
                        pricingMst.UnitType = item;
                        pricingMst.DynPrcInpTotal = 0;
                        pricingMst.Units = 0;
                        pricingMst.Hwm = 0;
                        pricingMst.ManFees = 0;
                        pricingMst.PerfFees = 0;
                        pricingMst.ComplianceFees = 0;
                        pricingMst.AuditFees = 0;
                        pricingMst.Ifafees = 0;
                        pricingMst.UnitPriceNav = Convert.ToDecimal(FundDetail.UnitStartingPrice);
                        pricingMst.TotalTheorValue = 0;
                        pricingMst.FeesPayable = 0;
                        pricingMst.TotalFeesDue = 0;
                        pricingMst.IsActive = true;
                        pricingMst.IsDeleted = false;
                        pricingMst.CreatedBy = FundDetail.CreatedBy;
                        pricingMst.UpdatedBy = FundDetail.CreatedBy;
                        pricingMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                        pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        pricingMstList.Add(pricingMst);
                    }

                    _dbContext.PricingMsts.AddRange(pricingMstList);
                    _dbContext.SaveChanges();

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Pricing Data Inserted Successfully.";
                    commonResponse.Data = pricingMst;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Fund Not Found.";
                }
            }
            catch (Exception) { throw; }

            return commonResponse;
        }
        public CommonResponse UpdatePricing(int FundId, DateTime TransactionDate, string UnitType, int DynPrcInpId, string InputPrice, int UpdatedBy)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                bool UpdateMode = false;
                decimal InputPriceTotal = 0;        //AmountBalance
                decimal UnitBalance = 0;
                decimal ManagementFees = 0;
                decimal PerformanceFees = 0;
                decimal HWM = 0;
                decimal IFAFees = 0;
                decimal UnitPriceNav = 0;
                decimal ComplianceFees = 0;
                decimal AuditFees = 0;
                decimal TotalTheorValue = 0;
                decimal FeesPayable = 0;
                decimal TotalFeesDue = 0;

                var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId);
                if (FundDetail != null)
                {
                    var PricingDetail = _commonRepo.GetPricingList(FundId).FirstOrDefault(x => x.TransactionDate.Value.Date == TransactionDate.Date && x.UnitType.ToLower() == UnitType.ToLower());
                    var DynamicInputPriceList = _commonRepo.GetFundDynamicInputPriceList(FundId).Where(x => x.BalanceDate.Date == TransactionDate.Date && x.UnitType.ToLower() == UnitType.ToLower()).ToList();
                    //var FundCurrentBalanceList = _commonRepo.GetFundFeeCalculationList(FundId).Where(x => x.UnitType.ToLower() == UnitType.ToLower()).ToList();
                    var FundCurrentBalanceList = _commonRepo.GetFundFeeCalculationList(FundId).Where(x => x.UnitType.ToLower() == UnitType.ToLower() && x.BalanceDate.Date == TransactionDate.Date).ToList();

                    UpdateMode = PricingDetail != null ? true : false;
                    InputPriceTotal = DynamicInputPriceList.Count() > 0 ? DynamicInputPriceList.Select(x => x.Value).Sum() : 0;
                    UnitBalance = FundCurrentBalanceList.Select(x => x.PostUnits).Sum();
                    HWM = FundCurrentBalanceList.Select(x => x.Hwm).Sum();
                    ManagementFees = FundCurrentBalanceList.Select(x => x.ManFees).Sum();
                    PerformanceFees = FundCurrentBalanceList.Select(x => x.PerfFees).Sum();
                    ComplianceFees = FundCurrentBalanceList.Select(x => x.ComplianceFees).Sum();
                    AuditFees = FundCurrentBalanceList.Select(x => x.AuditFees).Sum();
                    IFAFees = FundCurrentBalanceList.Select(x => x.Ifafees).Sum();
                    UnitPriceNav = FundCurrentBalanceList.Select(x => x.UnitPriceNav).Sum();
                    TotalTheorValue = FundCurrentBalanceList.Select(x => x.TotalTheorValue).Sum();
                    FeesPayable = FundCurrentBalanceList.Select(x => x.FeesPayable).Sum();
                    TotalFeesDue = FundCurrentBalanceList.Select(x => x.TotalFeesDue).Sum();

                    //var LastFundFeeCalculationDetail = _commonRepo.GetFundFeeCalculationList(FundId).LastOrDefault(x => x.UnitType.ToLower() == UnitType.ToLower() && x.BalanceDate.Date==TransactionDate.Date);

                    //UpdateMode = PricingDetail != null ? true : false;
                    //InputPriceTotal = DynamicInputPriceList.Count() > 0 ? DynamicInputPriceList.Select(x => x.Value).Sum() : 0;
                    //UnitBalance = LastFundFeeCalculationDetail.PostUnits;
                    //HWM = LastFundFeeCalculationDetail.Hwm;
                    //ManagementFees = LastFundFeeCalculationDetail.ManFees;
                    //PerformanceFees = LastFundFeeCalculationDetail.PerfFees;
                    //ComplianceFees = LastFundFeeCalculationDetail.ComplianceFees;
                    //AuditFees = LastFundFeeCalculationDetail.AuditFees;
                    //IFAFees = LastFundFeeCalculationDetail.Ifafees;
                    //UnitPriceNav = LastFundFeeCalculationDetail.UnitPriceNav;
                    //TotalTheorValue = LastFundFeeCalculationDetail.TotalTheorValue;
                    //FeesPayable = LastFundFeeCalculationDetail.FeesPayable;
                    //TotalFeesDue = LastFundFeeCalculationDetail.TotalFeesDue;

                    PricingMst pricingMst = new PricingMst();
                    if (UpdateMode)
                    {
                        //Update Mode
                        pricingMst = PricingDetail;
                        pricingMst.TransactionDate = TransactionDate;
                        pricingMst.DynPrcInpTotal = InputPriceTotal;
                        pricingMst.Units = UnitBalance;
                        pricingMst.Hwm = HWM;
                        pricingMst.ManFees = ManagementFees;
                        pricingMst.PerfFees = PerformanceFees;
                        pricingMst.ComplianceFees = ComplianceFees;
                        pricingMst.AuditFees = AuditFees;
                        pricingMst.Ifafees = IFAFees;
                        pricingMst.UnitPriceNav = UnitPriceNav;
                        pricingMst.TotalTheorValue = TotalTheorValue;
                        pricingMst.FeesPayable = FeesPayable;
                        pricingMst.TotalFeesDue = TotalFeesDue;
                        pricingMst.UpdatedBy = UpdatedBy;
                        pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        _dbContext.Entry(pricingMst).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Pricing Updated Sucessfully.";
                        commonResponse.Data = pricingMst;
                    }
                    else
                    {
                        commonResponse.Message = "Please Add Pricing For The Day!";
                    }
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Fund Not Found.";
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        public CommonResponse DeletePricing(int PricingId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var PricingDetail = _commonRepo.GetAllPricingList().FirstOrDefault(x => x.Id == PricingId);
                if (PricingDetail != null)
                {
                    PricingMst pricingMst = new PricingMst();
                    pricingMst = PricingDetail;

                    pricingMst.IsDeleted = true;
                    _dbContext.Entry(pricingMst).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = pricingMst;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Fund Not Found.";
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        public CommonResponse GetFundOwnershipUnitWise(int FundId, decimal PricingBalance, string? DynamicInputPricing = null, DateTime? UptoTransactionDate = null)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                UnitTypePercentageModel unitTypePercentageModel = new UnitTypePercentageModel();
                unitTypePercentageModel.UnitTypePercentageList = new List<UnitTypePercentageDetail>();
                var UnitTypeList = _fundDynamicFieldBLL.GetUnitTypeList(FundId);
                var TransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == FundId).ToList();
                //if (!string.IsNullOrWhiteSpace(DynamicInputPricing))
                //{
                //    TransactionList = TransactionList.Where(x => x.AllocateTo.ToLower() == DynamicInputPricing.ToLower()).ToList();
                //}
                if (UptoTransactionDate != null)
                {
                    TransactionList = TransactionList.Where(x => x.TransactionDate.Date <= UptoTransactionDate.Value.Date).ToList();
                }
                decimal TransactionBalance = 0;

                UnitTypePercentageDetail unitTypePercentageDetail = new UnitTypePercentageDetail();
                foreach (var unitTypeItem in UnitTypeList)
                {
                    unitTypePercentageDetail = new UnitTypePercentageDetail();
                    unitTypePercentageDetail.UnitType = unitTypeItem;
                    unitTypePercentageDetail.UnitTypeValue = 0;
                    unitTypePercentageDetail.UnitTypePercentage = 0;

                    unitTypePercentageModel.UnitTypePercentageList.Add(unitTypePercentageDetail);
                }

                foreach (var item in TransactionList)
                {
                    if (item.TransactionAmount > 0)
                    {
                        if (item.TransactionType.ToLower() == "buy")
                        {
                            TransactionBalance = TransactionBalance + Convert.ToDecimal(item.TransactionAmount);
                            foreach (var unitTypeItem in unitTypePercentageModel.UnitTypePercentageList)
                            {
                                if (unitTypeItem.UnitType.ToLower() == item.UnitType.ToLower())
                                    unitTypeItem.UnitTypeValue = unitTypeItem.UnitTypeValue + Convert.ToDecimal(item.TransactionAmount);
                            }
                        }
                        else
                        {
                            TransactionBalance = TransactionBalance - Convert.ToDecimal(item.TransactionAmount);
                            foreach (var unitTypeItem in unitTypePercentageModel.UnitTypePercentageList)
                            {
                                if (unitTypeItem.UnitType.ToLower() == item.UnitType.ToLower())
                                    unitTypeItem.UnitTypeValue = unitTypeItem.UnitTypeValue - Convert.ToDecimal(item.TransactionAmount);
                            }
                        }
                    }
                }

                foreach (var perItem in unitTypePercentageModel.UnitTypePercentageList)
                {
                    if (perItem.UnitTypeValue > 0 && TransactionBalance > 0)
                        perItem.UnitTypePercentage = (perItem.UnitTypeValue * 100) / TransactionBalance;
                }

                foreach (var perItem in unitTypePercentageModel.UnitTypePercentageList)
                {
                    if (perItem.UnitTypePercentage > 0 && PricingBalance > 0)
                        perItem.UnitTypeValue = (PricingBalance * perItem.UnitTypePercentage) / 100;
                    else
                    {
                        perItem.UnitTypeValue = 0;
                    }
                }
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Message = "Success!";
                commonResponse.Data = unitTypePercentageModel.UnitTypePercentageList;
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse GetUnitTypeWiseFundBalance(int FundId, string UnitType)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                decimal FundBalance = 0;
                var dynamicInputPricingList = _commonRepo.GetFundDynamicInputPriceList(FundId).Where(x => x.UnitType.ToLower() == UnitType.ToLower()).ToList();
                if (dynamicInputPricingList.Count > 0)
                {
                    DateTime LastRecordBalanceDate = dynamicInputPricingList.OrderByDescending(x => x.BalanceDate.Date).FirstOrDefault().BalanceDate;

                    FundBalance = dynamicInputPricingList.Where(x => x.BalanceDate.Date == LastRecordBalanceDate.Date).Select(x => x.Value).Sum();
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success!";
                }
                commonResponse.Data = FundBalance;
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdatePricingRecords(int FundId, DateTime TransactionDate)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var PricingList = _commonRepo.GetPricingList(FundId).Where(x => x.TransactionDate.Value.Date >= TransactionDate.Date).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdatePricingUsingTransaction(int FundId, DateTime TransactionDate, decimal TransactionAmount, string DynamicInput, string UnitType, int UpdatedBy)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId && x.IsActive.Value);
                if (FundDetail != null)
                {
                    var PricingList = _commonRepo.GetPricingList(FundDetail.Id).Where(x => x.TransactionDate.Value.Date == TransactionDate.Date).ToList();
                    var DynamicInputList = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id).Where(x => x.BalanceDate.Date == TransactionDate.Date && x.IsAddedFromPricing).ToList();
                    bool IsTransactionCompleted = false;
                    decimal TotalValue = 0;
                    decimal balanceAmount = 0;
                    //if (PricingList.Count > 0)
                    //{
                    //using (var scope = new TransactionScope())
                    //{
                    //Add / Update Dynamic Input pricing table 
                    var PricingInputs = FundDetail != null && !string.IsNullOrWhiteSpace(FundDetail.PricingInputs) ? FundDetail.PricingInputs.Trim().Split(',').ToList() : new List<string>();
                    decimal InputValue = 0;
                    List<Dictionary<string, decimal>> keyValuePairList = new List<Dictionary<string, decimal>>();
                    Dictionary<string, decimal> keyValuePair = new Dictionary<string, decimal>();

                    foreach (var item in PricingInputs)
                    {
                        balanceAmount = 0;
                        if (!string.IsNullOrWhiteSpace(item.Trim()))
                        {
                            InputValue = DynamicInputList.Where(x => x.Label.ToLower() == item.ToLower()).Select(x => x.Value).Sum();
                            if (item.ToLower() == DynamicInput.ToLower())
                            {
                                balanceAmount = InputValue + TransactionAmount;
                            }
                            keyValuePair = new Dictionary<string, decimal>();
                            keyValuePair.Add(item, balanceAmount);
                            keyValuePairList.Add(keyValuePair);
                        }
                    }
                    string PricingInput = "";
                    decimal PricingInputValue = 0;
                    foreach (var keyValueItem in keyValuePairList)
                    {
                        foreach (var item in keyValueItem)
                        {
                            PricingInput = item.Key;
                            PricingInputValue = item.Value;

                            List<UnitTypePercentageDetail> UnitTypeOwnershipPercentageList = GetFundOwnershipUnitWise(FundDetail.Id, PricingInputValue, PricingInput).Status ? GetFundOwnershipUnitWise(FundDetail.Id, PricingInputValue, PricingInput).Data : new List<UnitTypePercentageDetail>();

                            foreach (var unitTypeItem in UnitTypeOwnershipPercentageList)
                            {
                                _fundDynamicInputPriceBLL.AddUpdateFundDynamicInputPrice(FundDetail.Id, unitTypeItem.UnitType, PricingInput, unitTypeItem.UnitTypeValue, TransactionDate, UpdatedBy, true);
                            }
                        }

                    }

                    List<UnitTypePercentageDetail> UnitTypeList = GetFundOwnershipUnitWise(FundDetail.Id, 0).Status ? GetFundOwnershipUnitWise(FundDetail.Id, 0).Data : new List<UnitTypePercentageDetail>();

                    var FundDynamicInputList = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id).Where(x => x.BalanceDate.Date == TransactionDate.Date).ToList();

                    PricingMst pricingMst = new PricingMst();
                    foreach (var unitTypeItem in UnitTypeList)
                    {
                        TotalValue = FundDynamicInputList.Where(x => x.UnitType.ToLower() == unitTypeItem.UnitType.ToLower() && x.IsAddedFromPricing).Select(x => x.Value).Sum();
                        //if (unitTypeItem.UnitType.ToLower() == UnitType.ToLower())
                        //{
                        //    TotalValue = TotalValue + TransactionAmount;
                        //}
                        var res = _fundFeeCalculationDetailBLL.AddUpdateFeeCalculationDetail(FundDetail.Id, unitTypeItem.UnitType, TransactionDate, UpdatedBy, true, TransactionAmount);
                        if (!string.IsNullOrWhiteSpace(unitTypeItem.UnitType))
                        {
                            // Add / Update Fund Fees Calculation Details 
                            var FeeCalculationDetail = _commonRepo.FundFeeCalculationList(FundDetail.Id, TransactionDate).ToList().FirstOrDefault(x => x.UnitType.ToLower() == unitTypeItem.UnitType.ToLower());

                            // Add Pricing table 
                            if (FeeCalculationDetail != null)
                            {
                                var PricingDetail = PricingList.FirstOrDefault(x => x.UnitType.ToLower() == unitTypeItem.UnitType.ToLower());
                                pricingMst = new PricingMst();
                                if (PricingDetail != null)
                                {
                                    pricingMst = PricingDetail;
                                    pricingMst.DynPrcInpTotal = TotalValue;
                                    pricingMst.Units = FeeCalculationDetail.PostUnits;
                                    pricingMst.Hwm = FeeCalculationDetail.Hwm;
                                    pricingMst.ManFees = FeeCalculationDetail.ManFees;
                                    pricingMst.PerfFees = FeeCalculationDetail.PerfFees;
                                    pricingMst.ComplianceFees = FeeCalculationDetail.ComplianceFees;
                                    pricingMst.AuditFees = FeeCalculationDetail.AuditFees;
                                    pricingMst.Ifafees = FeeCalculationDetail.Ifafees;
                                    pricingMst.UnitPriceNav = FeeCalculationDetail.UnitPriceNav;
                                    pricingMst.UpdatedBy = UpdatedBy;
                                    pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                    pricingMst.TotalTheorValue = FeeCalculationDetail.TotalTheorValue;
                                    pricingMst.FeesPayable = FeeCalculationDetail.FeesPayable;
                                    pricingMst.TotalFeesDue = FeeCalculationDetail.TotalFeesDue;
                                    _dbContext.Entry(pricingMst).State = EntityState.Modified;
                                    _dbContext.SaveChanges();
                                }
                                else
                                {
                                    pricingMst = new PricingMst();
                                    pricingMst.FundId = FundDetail.Id;
                                    pricingMst.InceptionDate = FundDetail.InceptionDate;
                                    pricingMst.TransactionDate = TransactionDate;
                                    pricingMst.DynPrcInpFundId = FundDetail.Id;
                                    pricingMst.UnitType = unitTypeItem.UnitType;
                                    pricingMst.DynPrcInpTotal = TotalValue;
                                    pricingMst.Units = FeeCalculationDetail.PostUnits;
                                    pricingMst.Hwm = FeeCalculationDetail.Hwm;
                                    pricingMst.ManFees = FeeCalculationDetail.ManFees;
                                    pricingMst.PerfFees = FeeCalculationDetail.PerfFees;
                                    pricingMst.ComplianceFees = FeeCalculationDetail.ComplianceFees;
                                    pricingMst.AuditFees = FeeCalculationDetail.AuditFees;
                                    pricingMst.Ifafees = FeeCalculationDetail.Ifafees;
                                    pricingMst.UnitPriceNav = FeeCalculationDetail.UnitPriceNav;
                                    pricingMst.TotalTheorValue = FeeCalculationDetail.TotalTheorValue;
                                    pricingMst.FeesPayable = FeeCalculationDetail.FeesPayable;
                                    pricingMst.TotalFeesDue = FeeCalculationDetail.TotalFeesDue;
                                    pricingMst.IsActive = true;
                                    pricingMst.IsDeleted = false;
                                    pricingMst.CreatedBy = UpdatedBy;
                                    pricingMst.UpdatedBy = UpdatedBy;
                                    pricingMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                    pricingMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                    _dbContext.PricingMsts.Add(pricingMst);
                                    _dbContext.SaveChanges();
                                }

                            }
                        }
                    }

                    //IsTransactionCompleted = true;
                    //if (IsTransactionCompleted)
                    //{
                    //    scope.Complete();
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Update Pricing Successfully!";
                    //}
                    //}
                    //}
                    //else
                    //{
                    //    commonResponse.Message = "Please Do Add Pricing For The Day!";
                    //}
                }
                else
                {
                    commonResponse.Message = "Fund Not Found!";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        #endregion Private AddUpdatePricing
    }
}
