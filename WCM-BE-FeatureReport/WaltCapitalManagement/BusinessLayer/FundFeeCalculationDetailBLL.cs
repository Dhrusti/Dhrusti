using DataLayer.Entities;
using Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class FundFeeCalculationDetailBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        public FundFeeCalculationDetailBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse AddUpdateFeeCalculationDetail(int FundId, string UnitType, DateTime BalanceDate, int CreatedBy, bool IsCalledByPricing)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId);
                if (FundDetail != null)
                {
                    // Variable Declarations------------------------------------

                    decimal PrevTotalFeesDue = 0;
                    decimal PrevFeesPayable = 0;
                    decimal PrevFeesPaid = 0;
                    decimal PrevHWM = 0;
                    decimal PrevPostUnits = 0;
                    decimal PrevUnitPrice = 0;
                    decimal CurrBankBalance = 0;
                    decimal CurrTSTotalValue = 0;
                    decimal CurrTSTheoValue = 0;
                    decimal CurrTotalTheorValue = 0;
                    decimal StartValue = 0;
                    decimal ProfitOffHWM = 0;

                    //Fees
                    decimal FundManagementFees = 0;
                    decimal FundPerformanceFees = 0;
                    decimal ManagementFees = 0;
                    decimal PerformanceFees = 0;
                    decimal IFAInitialFee = 0;
                    decimal IFAAnnualFee = 0;
                    decimal IFAFees = 0;
                    decimal ComplianceFees = 0;
                    decimal AuditFee = 0;

                    decimal CurrFeesPayable = 0;
                    decimal CurrFeesPaid = 0;
                    decimal CurrTotalFeesDue = 0;
                    decimal CurrHWM = 0;
                    decimal PreUnits = 0;
                    decimal UnitChanges = 0;
                    decimal PostUnits = 0;
                    decimal CurrUnitBalance = 0;
                    decimal CurrUnitPrice = 0;

                    decimal ServiceFee = 0;
                    decimal BankToTradeStationTransferFee = 0;
                    decimal TotalPricingInputValues = 0;
                    //decimal BODAccountBalance = 0;
                    //decimal BODMarketValue = 0;
                    decimal CurTransactionAmount = 0;
                    decimal PrevTransactionAmount = 0;
                    decimal TransactionBalance = 0;
                    bool EditMode = false;
                    bool IsFundFirstTransaction = false;
                    DateTime PrevBalanceDate = BalanceDate.Date.AddDays(-1);
                    var CurrentYearTotalDays = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

                    // Data Fetching -------------------------------------------
                    var TransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == FundId).ToList();
                    var ClientList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == 2).ToList();
                    TransactionList = (from t in TransactionList
                                       join c in ClientList on t.Client equals c.Id
                                       select t).ToList();
                    var Transactions = TransactionList;
                    var CurrentFeesDetail = _commonRepo.FundFeeCalculationList(FundDetail.Id, BalanceDate).FirstOrDefault(x => x.UnitType.ToLower() == UnitType.ToLower());
                    var PrevFeesDetail = _commonRepo.FundFeeCalculationList(FundDetail.Id, PrevBalanceDate).Where(x=>x.UnitType.ToLower()==UnitType.ToLower()).FirstOrDefault();
                    var PricingInputValueList = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id);
                    var dynamicUnitsFieldsList = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundId && x.Label.ToLower().Contains("management fee") && x.Label.ToLower().Contains("performance fee")).ToList();
                    var HasFirstTransaction = TransactionList.Where(x => x.UnitType.ToLower() == UnitType.ToLower() && x.TransactionDate.Date == BalanceDate.Date).Count() > 1 ? true : false;
                    //var TransactionDetail = TransactionList.FirstOrDefault(x => x.Id == TransactionId);
                    var HasCurrentFeesDetail = _commonRepo.GetFundFeeCalculationList(FundDetail.Id).FirstOrDefault(x => x.UnitType.ToLower() == UnitType.ToLower()) != null ? true : false;
                    // Assigning Values to Variables
                    EditMode = CurrentFeesDetail != null ? true : false;
                    //TotalPricingInputValues = PricingInputValueList.Where(x => x.BalanceDate.Date == BalanceDate.Date && x.UnitType.ToLower() == UnitType.ToLower() && x.IsAddedFromPricing == false).Select(x => x.Value).Sum();
                    var TotalPricingInputValuesList = PricingInputValueList.Where(x => x.BalanceDate.Date == BalanceDate.Date && x.UnitType.ToLower() == UnitType.ToLower()).ToList();
                    if (TotalPricingInputValuesList.Count > 0)
                        TotalPricingInputValues = PricingInputValueList.Where(x => x.BalanceDate.Date == BalanceDate.Date && x.UnitType.ToLower() == UnitType.ToLower()).Select(x => x.Value).Sum();
                    //TotalPricingInputValues = PricingInputValueList.Where(x => x.UnitType.ToLower() == UnitType.ToLower() && x.IsAddedFromPricing == false).Select(x => x.Value).Sum();
                    var InputAddPricingTotal = PricingInputValueList.Where(x => x.BalanceDate.Date == BalanceDate.Date && x.UnitType.ToLower() == UnitType.ToLower() && x.IsAddedFromPricing).Select(x => x.Value).Sum();
                    CurTransactionAmount = PricingInputValueList.Where(x => x.BalanceDate.Date == BalanceDate.Date && x.UnitType.ToLower() == UnitType.ToLower() && !x.IsAddedFromPricing).Select(x => x.Value).Sum();
                    PrevTransactionAmount = PricingInputValueList.Where(x => x.BalanceDate.Date == PrevBalanceDate.Date && x.UnitType.ToLower() == UnitType.ToLower() && !x.IsAddedFromPricing).Select(x => x.Value).Sum();
                    IsFundFirstTransaction = TransactionList.Count() > 1 ? true : false;
                    //if (!HasFirstTransaction)
                    //{
                    //    PrevPostUnits = TransactionDetail != null ? Convert.ToDecimal(TransactionDetail.NumberOfUnits) : 0;
                    //}

                    if (!HasCurrentFeesDetail)
                    {
                        PrevUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
                    }

                    if (PrevFeesDetail != null)
                    {
                        PrevTotalFeesDue = PrevFeesDetail.TotalFeesDue;
                        PrevFeesPayable = PrevFeesDetail.FeesPayable;
                        PrevFeesPaid = PrevFeesDetail.FeesPaid;
                        if (IsCalledByPricing)
                            PrevHWM = PrevFeesDetail.Hwm;
                        PrevPostUnits = PrevFeesDetail.PostUnits;
                        PrevUnitPrice = PrevFeesDetail.UnitPrice;
                    }

                    if (UnitType.ToLower() == "unit a")
                    {
                        if (FundDetail.ManagementFeeA > 0)
                            FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeA);
                        if (FundDetail.PerformanceFeeA > 0)
                            FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeA);
                    }
                    else if (UnitType.ToLower() == "unit b")
                    {
                        if (FundDetail.ManagementFeeB > 0)
                            FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeB);
                        if (FundDetail.PerformanceFeeB > 0)
                            FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeB);
                    }
                    else
                    {
                        foreach (var item in dynamicUnitsFieldsList)
                        {
                            if (Convert.ToDecimal(item.Value) > 0)
                            {
                                if (item.Label.Contains("management fee") && item.Label == UnitType.ToLower())
                                {
                                    FundManagementFees = FundManagementFees + Convert.ToDecimal(item.Value);
                                }

                                if (item.Label.Contains("performance fee") && item.Label == UnitType.ToLower())
                                {
                                    if (Convert.ToDecimal(item.Value) > 0)
                                        FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(item.Value);
                                }
                            }
                        }
                    }

                    //if (IsCalledByPricing)
                    //{
                    //    PrevTransactionAmount = 0;
                    //    CurTransactionAmount = 0;
                    //}

                    //TotalPricingInputValues = TransactionBalance;
                    //CurrBankBalance = TotalPricingInputValues + ServiceFee + BankToTradeStationTransferFee;
                    CurrBankBalance = PrevTransactionAmount + ServiceFee + BankToTradeStationTransferFee;
                    //CurrTSTotalValue = BODAccountBalance + BODMarketValue;
                    CurrTSTotalValue = InputAddPricingTotal;
                    if (CurrTSTotalValue > 0)
                        CurrTSTheoValue = CurrTSTotalValue - PrevTotalFeesDue;

                    CurrTotalTheorValue = CurrTSTheoValue;

                    foreach (var trans in Transactions.Where(x => x.UnitType.ToLower() == UnitType.ToLower() && x.TransactionDate.Date == PrevBalanceDate.Date))
                    {
                        if (trans.TransactionType.ToLower() == "buy")
                        {
                            if (trans.TransactionAmount > 0)
                                TransactionBalance = TransactionBalance + Convert.ToDecimal(trans.TransactionAmount);
                        }
                        else
                        {
                            if (trans.TransactionAmount > 0)
                                TransactionBalance = TransactionBalance - Convert.ToDecimal(trans.TransactionAmount);
                        }

                        if (trans.IfaupFrontFee > 0 && CurrTotalTheorValue > 0)
                            IFAInitialFee = IFAInitialFee + ((CurrTotalTheorValue * Convert.ToDecimal(trans.IfaupFrontFee)) / 100);
                        if (trans.IfaAnnualFee > 0 && CurrTotalTheorValue > 0)
                        {
                            decimal IFAnualFeePercentage = (CurrTotalTheorValue * Convert.ToDecimal(trans.IfaAnnualFee)) / 100;
                            if (IFAnualFeePercentage > 0)
                                IFAAnnualFee = IFAAnnualFee + (IFAnualFeePercentage / CurrentYearTotalDays);
                        }
                    }

                    //CurrTotalTheorValue = CurrBankBalance + CurrTSTheoValue;
                    StartValue = PrevHWM;
                    //ProfitOffHWM = ((CurrTotalTheorValue - StartValue) - TotalPricingInputValues) < 0 ? 0 : ((CurrTotalTheorValue - StartValue) - TotalPricingInputValues);
                    //if (HasFirstTransaction)
                    //    ProfitOffHWM = ((CurrTotalTheorValue - StartValue) - CurTransactionAmount) < 0 ? 0 : ((CurrTotalTheorValue - StartValue) - CurTransactionAmount);
                    if (IsFundFirstTransaction)
                        ProfitOffHWM = (CurrTotalTheorValue - StartValue - TotalPricingInputValues) < 0 ? 0 : (CurrTotalTheorValue - StartValue - TotalPricingInputValues);
                    if (CurrTotalTheorValue > 0 && FundManagementFees > 0)
                        ManagementFees = ((CurrTotalTheorValue * FundManagementFees) / 100) / CurrentYearTotalDays;

                    PerformanceFees = ProfitOffHWM > 0 ? (ProfitOffHWM * FundPerformanceFees) / 100 : 0;

                    if (CurrTotalTheorValue > 0)
                    {
                        AuditFee = Convert.ToDecimal(FundDetail.AuditFee);
                        ComplianceFees = Convert.ToDecimal(FundDetail.ComplianceFee);

                        if (ComplianceFees > 0)
                            ComplianceFees = ComplianceFees / CurrentYearTotalDays;
                        if (AuditFee > 0)
                            AuditFee = AuditFee / CurrentYearTotalDays;

                        IFAFees = IFAInitialFee + IFAAnnualFee;
                        CurrFeesPayable = ManagementFees + PerformanceFees + AuditFee + ComplianceFees + IFAInitialFee + IFAAnnualFee;
                        CurrFeesPaid = PrevTotalFeesDue;
                        CurrTotalFeesDue = (PrevTotalFeesDue + CurrFeesPayable) - CurrFeesPaid;
                    }

                    //if ((StartValue + TotalPricingInputValues) > 0)
                    //CurrHWM = CurrTotalTheorValue < (StartValue + TotalPricingInputValues) ? ((StartValue + TotalPricingInputValues) - CurrFeesPayable) : (CurrTotalTheorValue - CurrFeesPayable);
                    CurrHWM = CurrTotalTheorValue < (StartValue) ? ((StartValue) - CurrFeesPayable) : (CurrTotalTheorValue - CurrFeesPayable);
                    //if (HasFirstTransaction)
                    if (!IsCalledByPricing)
                        PreUnits = PrevPostUnits;
                    if (TotalPricingInputValues > 0 && PrevUnitPrice > 0)
                        UnitChanges = TotalPricingInputValues / PrevUnitPrice;
                    PostUnits = PreUnits + UnitChanges;
                    CurrUnitBalance = PostUnits;
                    if (CurrTotalTheorValue > 0 && CurrUnitBalance > 0)
                        CurrUnitPrice = CurrTotalTheorValue / CurrUnitBalance;

                    // Adding Or Udating Table Using latest Values
                    FundFeeCalculationDetail fundFeeCalculationDetail = new FundFeeCalculationDetail();
                    if (!EditMode)
                    {
                        //Add Mode

                        fundFeeCalculationDetail.FundId = FundDetail.Id;
                        fundFeeCalculationDetail.BalanceDate = BalanceDate;
                        //fundFeeCalculationDetail.DynPrcInpId = DynPrcInpId;
                        //fundFeeCalculationDetail.DynPrcInpLabel = InputPrice;
                        fundFeeCalculationDetail.DynPrcInpId = 0;
                        fundFeeCalculationDetail.DynPrcInpLabel = "";
                        fundFeeCalculationDetail.UnitType = UnitType;
                        fundFeeCalculationDetail.StartValue = StartValue;
                        fundFeeCalculationDetail.FeesPayable = CurrFeesPayable;
                        fundFeeCalculationDetail.FeesPaid = CurrFeesPaid;
                        fundFeeCalculationDetail.TotalFeesDue = CurrTotalFeesDue;
                        fundFeeCalculationDetail.Hwm = CurrHWM;
                        fundFeeCalculationDetail.ProfitOffHwm = ProfitOffHWM;
                        fundFeeCalculationDetail.PreUnits = PreUnits;
                        fundFeeCalculationDetail.UnitChanges = UnitChanges;
                        fundFeeCalculationDetail.PostUnits = PostUnits;
                        fundFeeCalculationDetail.UnitPrice = CurrUnitPrice;
                        fundFeeCalculationDetail.BankBalance = CurrBankBalance;
                        fundFeeCalculationDetail.TstotalValue = CurrTSTotalValue;
                        fundFeeCalculationDetail.TstheoValue = CurrTSTheoValue;
                        fundFeeCalculationDetail.TotalTheorValue = CurrTotalTheorValue;
                        fundFeeCalculationDetail.ManFees = ManagementFees;
                        fundFeeCalculationDetail.PerfFees = PerformanceFees;
                        fundFeeCalculationDetail.ComplianceFees = ComplianceFees;
                        fundFeeCalculationDetail.AuditFees = AuditFee;
                        fundFeeCalculationDetail.IfainitialFee = IFAInitialFee;
                        fundFeeCalculationDetail.IfaannualFee = IFAAnnualFee;
                        fundFeeCalculationDetail.Ifafees = IFAFees;
                        fundFeeCalculationDetail.UnitPriceNav = CurrUnitPrice;
                        fundFeeCalculationDetail.IsActive = true;
                        fundFeeCalculationDetail.IsDeleted = false;
                        fundFeeCalculationDetail.CreatedBy = CreatedBy;
                        fundFeeCalculationDetail.UpdatedBy = CreatedBy;
                        fundFeeCalculationDetail.CreatedDate = _commonHelper.GetCurrentDateTime();
                        fundFeeCalculationDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        _dbContext.FundFeeCalculationDetails.Add(fundFeeCalculationDetail);
                        commonResponse.Message = "Fund Fees Calculation Details Added Sucessfully.";
                    }
                    else
                    {
                        //Edit Mode
                        fundFeeCalculationDetail = CurrentFeesDetail;

                        fundFeeCalculationDetail.StartValue = StartValue;
                        fundFeeCalculationDetail.FeesPayable = CurrFeesPayable;
                        fundFeeCalculationDetail.FeesPaid = CurrFeesPaid;
                        fundFeeCalculationDetail.TotalFeesDue = CurrTotalFeesDue;
                        fundFeeCalculationDetail.Hwm = CurrHWM;
                        fundFeeCalculationDetail.ProfitOffHwm = ProfitOffHWM;
                        fundFeeCalculationDetail.PreUnits = PreUnits;
                        fundFeeCalculationDetail.UnitChanges = UnitChanges;
                        fundFeeCalculationDetail.PostUnits = PostUnits;
                        fundFeeCalculationDetail.UnitPrice = CurrUnitPrice;
                        fundFeeCalculationDetail.BankBalance = CurrBankBalance;
                        fundFeeCalculationDetail.TstotalValue = CurrTSTotalValue;
                        fundFeeCalculationDetail.TstheoValue = CurrTSTheoValue;
                        fundFeeCalculationDetail.TotalTheorValue = CurrTotalTheorValue;
                        fundFeeCalculationDetail.ManFees = ManagementFees;
                        fundFeeCalculationDetail.PerfFees = PerformanceFees;
                        fundFeeCalculationDetail.ComplianceFees = ComplianceFees;
                        fundFeeCalculationDetail.AuditFees = AuditFee;
                        fundFeeCalculationDetail.IfainitialFee = IFAInitialFee;
                        fundFeeCalculationDetail.IfaannualFee = IFAAnnualFee;
                        fundFeeCalculationDetail.Ifafees = IFAFees;
                        fundFeeCalculationDetail.UnitPriceNav = CurrUnitPrice;
                        fundFeeCalculationDetail.UpdatedBy = CreatedBy;
                        fundFeeCalculationDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        _dbContext.Entry(fundFeeCalculationDetail).State = EntityState.Modified;
                        commonResponse.Message = "Fund Fees Calculation Details Updated Sucessfully.";
                    }
                    _dbContext.SaveChanges();

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = fundFeeCalculationDetail;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Fund Id Not Found.";
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
