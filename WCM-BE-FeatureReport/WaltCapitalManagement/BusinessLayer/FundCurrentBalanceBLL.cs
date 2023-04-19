using DataLayer.Entities;
using DTO.ReqDTO;
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
    public class FundCurrentBalanceBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public FundCurrentBalanceBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        //public CommonResponse AddCurrentBalance(int FundId)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId);
        //        if (FundDetail != null)
        //        {
        //            var dynamicInputPriceList = _commonRepo.GetFundDynamicInputPriceList(FundDetail.Id).ToList();

        //            List<FundCurrentBalanceMst> fundCurrentBalanceMstList = new List<FundCurrentBalanceMst>();
        //            foreach (var item in dynamicInputPriceList)
        //            {
        //                FundCurrentBalanceMst fundCurrentBalanceMst = new FundCurrentBalanceMst();
        //                fundCurrentBalanceMst.FundId = FundDetail.Id;
        //                fundCurrentBalanceMst.BalanceDate = FundDetail.InceptionDate;
        //                fundCurrentBalanceMst.DynPrcInpId = item.Id;
        //                fundCurrentBalanceMst.DynPrcInpLabel = item.Label;
        //                fundCurrentBalanceMst.UnitType = "";
        //                fundCurrentBalanceMst.UnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                fundCurrentBalanceMst.FundBalance = 0;
        //                fundCurrentBalanceMst.UnitBalance = 0;
        //                fundCurrentBalanceMst.IsActive = true;
        //                fundCurrentBalanceMst.IsDeleted = false;
        //                fundCurrentBalanceMst.CreatedBy = FundDetail.CreatedBy;
        //                fundCurrentBalanceMst.UpdatedBy = FundDetail.CreatedBy;
        //                fundCurrentBalanceMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                fundCurrentBalanceMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                fundCurrentBalanceMstList.Add(fundCurrentBalanceMst);
        //            }

        //            _dbContext.FundCurrentBalanceMsts.AddRange(fundCurrentBalanceMstList);
        //            _dbContext.SaveChanges();

        //            commonResponse.Status = true;
        //            commonResponse.StatusCode = HttpStatusCode.OK;
        //            commonResponse.Message = "Fund Current Balance Added Sucessfully.";
        //            commonResponse.Data = fundCurrentBalanceMstList;
        //        }
        //        else
        //        {
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //            commonResponse.Message = "Fund Id Not Found.";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return commonResponse;
        //}

        //public CommonResponse AddUpdateCurrentBalance(int FundId, string UnitType, DateTime BalanceDate, int DynPrcInpId, string InputPrice, int CreatedBy)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId);
        //        if (FundDetail != null)
        //        {
        //            // Variable Declarations------------------------------------
        //            bool EditMode = false;
        //            bool HasFundTransaction = false;
        //            decimal UnitBalance = 0;
        //            decimal AmountBalance = 0;
        //            decimal CurrentUnitPrice = 0;
        //            decimal TotalFees = 0;
        //            decimal FundManagementFees = 0;
        //            decimal FundPerformanceFees = 0;
        //            decimal ManagementFees = 0;
        //            decimal PerformanceFees = 0;
        //            decimal IFAInitialFee = 0;
        //            decimal IFAAnnualFee = 0;
        //            decimal HWM = 0;
        //            decimal IFAFees = 0;
        //            decimal ComplianceFees = 0;
        //            decimal AuditFee = 0;

        //            // Data Fetching -------------------------------------------
        //            var CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault(x => x.UnitType == UnitType && (x.DynPrcInpLabel.ToLower() == InputPrice.ToLower()));
        //            var TransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == FundId);
        //            var Transactions = TransactionList.Where(x => x.UnitType == UnitType && x.AllocateTo.ToLower() == InputPrice.ToLower() && x.TransactionDate.Date == BalanceDate.Date).ToList();
        //            var dynamicUnitsFieldsList = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundId && x.Label.ToLower().Contains("management fee") && x.Label.ToLower().Contains("performance fee")).ToList();
        //            var CurrentYearTotalDays = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

        //            // Updating Variables -------------------------------------------
        //            EditMode = CurrentBalanceDetail != null ? true : false;
        //            HasFundTransaction = TransactionList.Count() > 1 ? true : false;

        //            foreach (var trans in Transactions)
        //            {
        //                if (trans.TransactionType.ToLower() == "buy")
        //                {
        //                    if (trans.TransactionAmount > 0)
        //                        AmountBalance = AmountBalance + Convert.ToDecimal(trans.TransactionAmount);
        //                    if (trans.IfaupFrontFee > 0)
        //                        IFAInitialFee = IFAInitialFee + ((AmountBalance * Convert.ToDecimal(trans.IfaupFrontFee)) / 100);
        //                    if (trans.IfaAnnualFee > 0)
        //                        IFAAnnualFee = IFAAnnualFee + (((AmountBalance * Convert.ToDecimal(trans.IfaAnnualFee)) / 100) / CurrentYearTotalDays);
        //                }
        //                else
        //                {
        //                    if (trans.TransactionAmount > 0)
        //                        AmountBalance = AmountBalance - Convert.ToDecimal(trans.TransactionAmount);
        //                    if (trans.IfaupFrontFee > 0)
        //                        IFAInitialFee = IFAInitialFee - ((AmountBalance * Convert.ToDecimal(trans.IfaupFrontFee)) / 100);
        //                    if (trans.IfaAnnualFee > 0)
        //                        IFAAnnualFee = IFAAnnualFee - (((AmountBalance * Convert.ToDecimal(trans.IfaAnnualFee)) / 100) / CurrentYearTotalDays);
        //                }
        //            }

        //            if (UnitType.ToLower() == "unit a")
        //            {
        //                if (FundDetail.ManagementFeeA > 0)
        //                    FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeA);
        //                if (FundDetail.PerformanceFeeA > 0)
        //                    FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeA);
        //            }
        //            else if (UnitType.ToLower() == "unit b")
        //            {
        //                if (FundDetail.ManagementFeeB > 0)
        //                    FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeB);
        //                if (FundDetail.PerformanceFeeB > 0)
        //                    FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeB);
        //            }
        //            else
        //            {
        //                foreach (var item in dynamicUnitsFieldsList)
        //                {
        //                    if (Convert.ToDecimal(item.Value) > 0)
        //                    {
        //                        if (item.Label.Contains("management fee") && item.Label == UnitType.ToLower())
        //                        {
        //                            FundManagementFees = FundManagementFees + Convert.ToDecimal(item.Value);
        //                        }
        //                        if (item.Label.Contains("performance fee") && item.Label == UnitType.ToLower())
        //                        {
        //                            FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(item.Value);
        //                        }
        //                    }
        //                }
        //            }

        //            // Calculating and Updating Variables Using Formulas -------------------------------------------
        //            if (AmountBalance > 0 && FundManagementFees > 0)
        //                ManagementFees = (AmountBalance * FundManagementFees) / CurrentYearTotalDays; // formula : Total * Management fee of fund / 365
        //            if (FundPerformanceFees > 0 && ManagementFees > 0)
        //                PerformanceFees = (FundPerformanceFees * ManagementFees) / CurrentYearTotalDays; // formula : performance fee * Management fee / 365

        //            ComplianceFees = Convert.ToDecimal(FundDetail.ComplianceFee);
        //            AuditFee = Convert.ToDecimal(FundDetail.AuditFee);

        //            if (ComplianceFees > 0)
        //                ComplianceFees = ComplianceFees / CurrentYearTotalDays;
        //            if (AuditFee > 0)
        //                AuditFee = AuditFee / CurrentYearTotalDays;
        //            //IFAInitialFee = (AmountBalance * IFAInitialFee) / 100;
        //            //IFAAnnualFee = (AmountBalance * (IFAAnnualFee / 100)) / CurrentYearTotalDays;

        //            IFAFees = IFAInitialFee + IFAAnnualFee; //Formula : IFAFees = Upfront IFA Fees + Anual IFA Fees 
        //            TotalFees = ManagementFees + PerformanceFees + AuditFee + ComplianceFees + IFAInitialFee + IFAAnnualFee;
        //            if (AmountBalance > 0 && AmountBalance >= TotalFees)
        //                HWM = AmountBalance - TotalFees; // Formula : HWM = Total Amount - Total Fees

        //            //Formula : UnitPrice = (Total Amount - Total Fees) / No. of Units
        //            if (!HasFundTransaction || CurrentBalanceDetail == null)
        //            {
        //                if (FundDetail.UnitStartingPrice > 0)
        //                    CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //            }
        //            else
        //            {
        //                if (AmountBalance > 0 && AmountBalance > TotalFees && CurrentBalanceDetail.UnitBalance > 0)
        //                    CurrentUnitPrice = ((AmountBalance - TotalFees) / CurrentBalanceDetail.UnitBalance);
        //                if (CurrentUnitPrice <= 0)
        //                    CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //            }

        //            //CurrentUnitPrice = !HasFundTransaction || CurrentBalanceDetail == null ? Convert.ToDecimal(FundDetail.UnitStartingPrice) : ((AmountBalance - TotalFees) / CurrentBalanceDetail.UnitBalance); //Formula : UnitPrice = (Total Amount - Total Fees) / No. of Units
        //            if (AmountBalance > 0 && CurrentUnitPrice > 0)
        //                UnitBalance = AmountBalance / CurrentUnitPrice;  //Formula : UnitBalance =  TotalAmount / Unit Price

        //            // Adding Or Udating Table Using latest Values
        //            FundCurrentBalanceMst fundCurrentBalanceMst = new FundCurrentBalanceMst();
        //            if (!EditMode)
        //            {
        //                //Add Mode
        //                fundCurrentBalanceMst.FundId = FundDetail.Id;
        //                fundCurrentBalanceMst.BalanceDate = BalanceDate;
        //                fundCurrentBalanceMst.DynPrcInpId = DynPrcInpId;
        //                fundCurrentBalanceMst.DynPrcInpLabel = InputPrice;
        //                fundCurrentBalanceMst.UnitType = UnitType;
        //                fundCurrentBalanceMst.FundBalance = AmountBalance;
        //                fundCurrentBalanceMst.UnitBalance = UnitBalance;
        //                fundCurrentBalanceMst.UnitPrice = CurrentUnitPrice;
        //                fundCurrentBalanceMst.Hwm = HWM;
        //                fundCurrentBalanceMst.ManFees = ManagementFees;
        //                fundCurrentBalanceMst.PerfFees = PerformanceFees;
        //                fundCurrentBalanceMst.ComplianceFees = ComplianceFees;
        //                fundCurrentBalanceMst.AuditFees = AuditFee;
        //                fundCurrentBalanceMst.IfainitialFee = IFAInitialFee;
        //                fundCurrentBalanceMst.IfaannualFee = IFAAnnualFee;
        //                fundCurrentBalanceMst.Ifafees = IFAFees;
        //                //fundCurrentBalanceMst.UnitPriceNav = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                fundCurrentBalanceMst.UnitPriceNav = CurrentUnitPrice;
        //                fundCurrentBalanceMst.IsActive = true;
        //                fundCurrentBalanceMst.IsDeleted = false;
        //                fundCurrentBalanceMst.CreatedBy = CreatedBy;
        //                fundCurrentBalanceMst.UpdatedBy = CreatedBy;
        //                fundCurrentBalanceMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                fundCurrentBalanceMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                _dbContext.FundCurrentBalanceMsts.Add(fundCurrentBalanceMst);
        //                commonResponse.Message = "Fund Current Balance Added Sucessfully.";
        //            }
        //            else
        //            {
        //                //Edit Mode
        //                fundCurrentBalanceMst = CurrentBalanceDetail;

        //                //fundCurrentBalanceMst.FundId = FundDetail.Id;
        //                //fundCurrentBalanceMst.BalanceDate = BalanceDate;
        //                //fundCurrentBalanceMst.DynPrcInpId = DynPrcInpId;
        //                //fundCurrentBalanceMst.DynPrcInpLabel = InputPrice;
        //                //fundCurrentBalanceMst.UnitType = UnitType;
        //                fundCurrentBalanceMst.FundBalance = AmountBalance;
        //                fundCurrentBalanceMst.UnitBalance = UnitBalance;
        //                fundCurrentBalanceMst.UnitPrice = CurrentUnitPrice;
        //                fundCurrentBalanceMst.Hwm = HWM;
        //                fundCurrentBalanceMst.ManFees = ManagementFees;
        //                fundCurrentBalanceMst.PerfFees = PerformanceFees;
        //                fundCurrentBalanceMst.ComplianceFees = ComplianceFees;
        //                fundCurrentBalanceMst.AuditFees = AuditFee;
        //                fundCurrentBalanceMst.IfainitialFee = IFAInitialFee;
        //                fundCurrentBalanceMst.IfaannualFee = IFAAnnualFee;
        //                fundCurrentBalanceMst.Ifafees = IFAFees;
        //                //fundCurrentBalanceMst.UnitPriceNav = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                fundCurrentBalanceMst.UnitPriceNav = CurrentUnitPrice;
        //                //fundCurrentBalanceMst.IsActive = true;
        //                //fundCurrentBalanceMst.IsDeleted = false;
        //                //fundCurrentBalanceMst.CreatedBy = CreatedBy;
        //                fundCurrentBalanceMst.UpdatedBy = CreatedBy;
        //                //fundCurrentBalanceMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                fundCurrentBalanceMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                _dbContext.Entry(fundCurrentBalanceMst).State = EntityState.Modified;
        //                commonResponse.Message = "Fund Current Balance Updated Sucessfully.";
        //            }
        //            _dbContext.SaveChanges();

        //            commonResponse.Status = true;
        //            commonResponse.StatusCode = HttpStatusCode.OK;
        //            commonResponse.Data = fundCurrentBalanceMst;
        //        }
        //        else
        //        {
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //            commonResponse.Message = "Fund Id Not Found.";
        //        }
        //    }
        //    catch (Exception) { throw; }
        //    return commonResponse;
        //}

        //public CommonResponse AddUpdateCurrentBalance(int FundId, string UnitType, DateTime BalanceDate, int DynPrcInpId, string InputPrice, int CreatedBy)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId);
        //        if (FundDetail != null)
        //        {
        //            // Variable Declarations------------------------------------
        //            bool EditMode = false;
        //            bool HasFundTransaction = false;
        //            decimal UnitBalance = 0;
        //            decimal AmountBalance = 0;
        //            decimal CurrentUnitPrice = 0;
        //            decimal TotalFees = 0;
        //            decimal FundManagementFees = 0;
        //            decimal FundPerformanceFees = 0;
        //            decimal ManagementFees = 0;
        //            decimal PerformanceFees = 0;
        //            decimal IFAInitialFee = 0;
        //            decimal IFAAnnualFee = 0;
        //            decimal HWM = 0;
        //            decimal IFAFees = 0;
        //            decimal ComplianceFees = 0;
        //            decimal AuditFee = 0;
        //            decimal UnitPrice = 0;
        //            DateTime PrevBalanceDate = BalanceDate.Date.AddDays(-1);
        //            decimal PrevUnitPrice = 0;
        //            decimal PrevUnitBalance = 0;
        //            decimal PrevHWM = 0;
        //            bool flag = false;
        //            // Data Fetching -------------------------------------------

        //            var TransactionList = _commonRepo.clientTransactionList().Where(x => x.Fund == FundId).ToList();
        //            var ClientList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == 2).ToList();
        //            TransactionList = (from t in TransactionList
        //                               join c in ClientList on t.Client equals c.Id
        //                               select t).ToList();
        //            //var Transactions = TransactionList.Where(x => x.UnitType == UnitType && x.AllocateTo.ToLower() == InputPrice.ToLower()).ToList();
        //            var Transactions = TransactionList;
        //            //var CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault(x => x.UnitType == UnitType && (x.DynPrcInpLabel.ToLower() == InputPrice.ToLower()));
        //            var CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault();
        //            //var PrevCurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).FirstOrDefault(x => x.UnitType == UnitType && (x.DynPrcInpLabel.ToLower() == InputPrice.ToLower()));
        //            var PrevCurrentBalanceList = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).ToList();
        //            var PrevCurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).FirstOrDefault();

        //            if (TransactionList.Where(x => x.TransactionDate.Date == BalanceDate.Date).Count() > 1)
        //            {
        //                Transactions = TransactionList.Where(x => x.UnitType == UnitType).ToList();
        //                CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault(x => x.UnitType == UnitType);
        //                PrevCurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).FirstOrDefault(x => x.UnitType == UnitType);
        //                PrevCurrentBalanceList = PrevCurrentBalanceList.Where(x => x.UnitType == UnitType).ToList();
        //            }

        //            if (PrevCurrentBalanceList.Count() > 0)
        //            {
        //                PrevUnitPrice = PrevCurrentBalanceList.FirstOrDefault().UnitPrice;
        //                PrevUnitBalance = PrevCurrentBalanceList.Sum(x => x.UnitBalance);
        //                PrevHWM = PrevCurrentBalanceList.Sum(x => x.Hwm);
        //            }


        //            var dynamicUnitsFieldsList = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundId && x.Label.ToLower().Contains("management fee") && x.Label.ToLower().Contains("performance fee")).ToList();
        //            var CurrentYearTotalDays = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

        //            // Updating Variables -------------------------------------------
        //            EditMode = CurrentBalanceDetail != null ? true : false;
        //            HasFundTransaction = TransactionList.Count() > 1 ? true : false;

        //            foreach (var trans in Transactions)
        //            {
        //                if (trans.TransactionType.ToLower() == "buy")
        //                {
        //                    if (trans.TransactionAmount > 0)
        //                        AmountBalance = AmountBalance + Convert.ToDecimal(trans.TransactionAmount);
        //                }
        //                else
        //                {
        //                    if (trans.TransactionAmount > 0)
        //                        AmountBalance = AmountBalance - Convert.ToDecimal(trans.TransactionAmount);
        //                }

        //                if (trans.IfaupFrontFee > 0 && AmountBalance > 0)
        //                    //IFAInitialFee = (AmountBalance * IFAInitialFee) / 100;
        //                    IFAInitialFee = IFAInitialFee + ((AmountBalance * Convert.ToDecimal(trans.IfaupFrontFee)) / 100);
        //                if (trans.IfaAnnualFee > 0 && AmountBalance > 0)
        //                {
        //                    decimal IFAnualFeePercentage = (AmountBalance * Convert.ToDecimal(trans.IfaAnnualFee)) / 100;
        //                    if (IFAnualFeePercentage > 0)
        //                        //IFAAnnualFee = (AmountBalance * (IFAAnnualFee / 100)) / CurrentYearTotalDays;
        //                        IFAAnnualFee = IFAAnnualFee + (IFAnualFeePercentage / CurrentYearTotalDays);
        //                }
        //            }

        //            if (UnitType.ToLower() == "unit a")
        //            {
        //                if (FundDetail.ManagementFeeA > 0)
        //                    FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeA);
        //                if (FundDetail.PerformanceFeeA > 0)
        //                    FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeA);
        //            }
        //            else if (UnitType.ToLower() == "unit b")
        //            {
        //                if (FundDetail.ManagementFeeB > 0)
        //                    FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeB);
        //                if (FundDetail.PerformanceFeeB > 0)
        //                    FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeB);
        //            }
        //            else
        //            {
        //                foreach (var item in dynamicUnitsFieldsList)
        //                {
        //                    if (Convert.ToDecimal(item.Value) > 0)
        //                    {
        //                        if (item.Label.Contains("management fee") && item.Label == UnitType.ToLower())
        //                        {
        //                            FundManagementFees = FundManagementFees + Convert.ToDecimal(item.Value);
        //                        }

        //                        if (item.Label.Contains("performance fee") && item.Label == UnitType.ToLower())
        //                        {
        //                            if (Convert.ToDecimal(item.Value) > 0)
        //                                FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(item.Value);
        //                        }
        //                    }
        //                }
        //            }

        //            if (AmountBalance > 0 && FundManagementFees > 0)
        //                //formula : Total* Management fee of fund percentage / 365
        //                ManagementFees = ((AmountBalance * FundManagementFees) / 100) / CurrentYearTotalDays; // 

        //            AuditFee = Convert.ToDecimal(FundDetail.AuditFee);
        //            ComplianceFees = Convert.ToDecimal(FundDetail.ComplianceFee);

        //            if (ComplianceFees > 0)
        //                ComplianceFees = ComplianceFees / CurrentYearTotalDays;
        //            if (AuditFee > 0)
        //                AuditFee = AuditFee / CurrentYearTotalDays;

        //            //Formula : IFAFees = Upfront IFA Fees + Anual IFA Fees 
        //            IFAFees = IFAInitialFee + IFAAnnualFee;

        //            if (!HasFundTransaction)
        //            {
        //                //First Transaction

        //                if (ManagementFees > 0 && FundPerformanceFees > 0)
        //                {
        //                    // formula : performance fee * Management fee / 365
        //                    PerformanceFees = ((ManagementFees * FundPerformanceFees) / 100) / CurrentYearTotalDays;
        //                }

        //                TotalFees = ManagementFees + PerformanceFees + AuditFee + ComplianceFees + IFAInitialFee + IFAAnnualFee;

        //                if (FundDetail.UnitStartingPrice > 0)
        //                {
        //                    CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                }

        //                if (AmountBalance > 0 && CurrentUnitPrice > 0)
        //                {
        //                    UnitBalance = AmountBalance / CurrentUnitPrice;
        //                }

        //                if (AmountBalance > 0 && AmountBalance > TotalFees)
        //                    HWM = AmountBalance - TotalFees;
        //            }
        //            else
        //            {
        //                //Second Transactions--------------------------------------------------------


        //                if (PrevCurrentBalanceDetail != null && FundPerformanceFees > 0)
        //                //if (PrevCurrentBalanceDetail != null && FundPerformanceFees > 0)
        //                {
        //                    //if (PrevUnitPrice > CurrentUnitPrice)
        //                    //{
        //                    //    CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                    //}
        //                    if (CurrentUnitPrice == 0)
        //                    {
        //                        CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                    }

        //                    CurrentUnitPrice = AmountBalance / PrevCurrentBalanceDetail.UnitBalance;
        //                    flag = true;

        //                    if (PrevUnitPrice < CurrentUnitPrice)
        //                    {
        //                        // formula : ((Total Balance - Previous HWM) * Fund Performance Fee) / 100 
        //                        PerformanceFees = ((AmountBalance - PrevHWM) * FundPerformanceFees) / 100;
        //                    }
        //                    else if (PrevCurrentBalanceDetail != null && PrevUnitPrice > CurrentUnitPrice)
        //                    {
        //                        PerformanceFees = 0;
        //                    }
        //                    else if (ManagementFees > 0 && FundPerformanceFees > 0)
        //                    {
        //                        PerformanceFees = ((ManagementFees * FundPerformanceFees) / 100) / CurrentYearTotalDays;
        //                    }
        //                }
        //                else if (PrevCurrentBalanceDetail != null && PrevUnitPrice > CurrentUnitPrice)
        //                {
        //                    PerformanceFees = 0;
        //                }
        //                else if (ManagementFees > 0 && FundPerformanceFees > 0)
        //                {
        //                    PerformanceFees = ((ManagementFees * FundPerformanceFees) / 100) / CurrentYearTotalDays;
        //                }

        //                TotalFees = ManagementFees + PerformanceFees + AuditFee + ComplianceFees + IFAInitialFee + IFAAnnualFee;

        //                if (AmountBalance > 0 && PrevCurrentBalanceDetail != null && PrevUnitBalance > 0 && AmountBalance > TotalFees && !flag)
        //                {
        //                    CurrentUnitPrice = (AmountBalance - TotalFees) / PrevUnitBalance;
        //                }
        //                else if (FundDetail.UnitStartingPrice > 0 && !flag)
        //                {
        //                    CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                }

        //                if (AmountBalance > 0 && CurrentUnitPrice > 0)
        //                {
        //                    UnitBalance = AmountBalance / CurrentUnitPrice;
        //                }

        //                if (PrevCurrentBalanceDetail != null && PrevUnitPrice < CurrentUnitPrice)
        //                {
        //                    if (AmountBalance > 0)
        //                        // Formula : HWM = Total Amount - Total Fees
        //                        HWM = AmountBalance - TotalFees;
        //                }
        //                else if (PrevCurrentBalanceDetail != null && PrevUnitPrice > CurrentUnitPrice && PrevHWM > 0)
        //                {
        //                    HWM = PrevHWM - TotalFees;
        //                }
        //                else if (AmountBalance > 0 && TotalFees > 0)
        //                {
        //                    HWM = AmountBalance - TotalFees;
        //                }
        //            }

        //            // Adding Or Udating Table Using latest Values
        //            FundCurrentBalanceMst fundCurrentBalanceMst = new FundCurrentBalanceMst();
        //            if (!EditMode)
        //            {
        //                //Add Mode
        //                fundCurrentBalanceMst.FundId = FundDetail.Id;
        //                fundCurrentBalanceMst.BalanceDate = BalanceDate;
        //                //fundCurrentBalanceMst.DynPrcInpId = DynPrcInpId;
        //                //fundCurrentBalanceMst.DynPrcInpLabel = InputPrice;
        //                fundCurrentBalanceMst.DynPrcInpId = 0;
        //                fundCurrentBalanceMst.DynPrcInpLabel = "";
        //                fundCurrentBalanceMst.UnitType = UnitType;
        //                fundCurrentBalanceMst.FundBalance = AmountBalance;
        //                fundCurrentBalanceMst.UnitBalance = UnitBalance;
        //                fundCurrentBalanceMst.UnitPrice = CurrentUnitPrice;
        //                fundCurrentBalanceMst.Hwm = HWM;
        //                fundCurrentBalanceMst.ManFees = ManagementFees;
        //                fundCurrentBalanceMst.PerfFees = PerformanceFees;
        //                fundCurrentBalanceMst.ComplianceFees = ComplianceFees;
        //                fundCurrentBalanceMst.AuditFees = AuditFee;
        //                fundCurrentBalanceMst.IfainitialFee = IFAInitialFee;
        //                fundCurrentBalanceMst.IfaannualFee = IFAAnnualFee;
        //                fundCurrentBalanceMst.Ifafees = IFAFees;
        //                //fundCurrentBalanceMst.UnitPriceNav = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                fundCurrentBalanceMst.UnitPriceNav = CurrentUnitPrice;
        //                fundCurrentBalanceMst.IsActive = true;
        //                fundCurrentBalanceMst.IsDeleted = false;
        //                fundCurrentBalanceMst.CreatedBy = CreatedBy;
        //                fundCurrentBalanceMst.UpdatedBy = CreatedBy;
        //                fundCurrentBalanceMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                fundCurrentBalanceMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                _dbContext.FundCurrentBalanceMsts.Add(fundCurrentBalanceMst);
        //                commonResponse.Message = "Fund Current Balance Added Sucessfully.";
        //            }
        //            else
        //            {
        //                //Edit Mode
        //                fundCurrentBalanceMst = CurrentBalanceDetail;

        //                //fundCurrentBalanceMst.FundId = FundDetail.Id;
        //                //fundCurrentBalanceMst.BalanceDate = BalanceDate;
        //                //fundCurrentBalanceMst.DynPrcInpId = DynPrcInpId;
        //                //fundCurrentBalanceMst.DynPrcInpLabel = InputPrice;
        //                //fundCurrentBalanceMst.UnitType = UnitType;
        //                fundCurrentBalanceMst.FundBalance = AmountBalance;
        //                fundCurrentBalanceMst.UnitBalance = UnitBalance;
        //                fundCurrentBalanceMst.UnitPrice = CurrentUnitPrice;
        //                fundCurrentBalanceMst.Hwm = HWM;
        //                fundCurrentBalanceMst.ManFees = ManagementFees;
        //                fundCurrentBalanceMst.PerfFees = PerformanceFees;
        //                fundCurrentBalanceMst.ComplianceFees = ComplianceFees;
        //                fundCurrentBalanceMst.AuditFees = AuditFee;
        //                fundCurrentBalanceMst.IfainitialFee = IFAInitialFee;
        //                fundCurrentBalanceMst.IfaannualFee = IFAAnnualFee;
        //                fundCurrentBalanceMst.Ifafees = IFAFees;
        //                //fundCurrentBalanceMst.UnitPriceNav = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                fundCurrentBalanceMst.UnitPriceNav = CurrentUnitPrice;
        //                //fundCurrentBalanceMst.IsActive = true;
        //                //fundCurrentBalanceMst.IsDeleted = false;
        //                //fundCurrentBalanceMst.CreatedBy = CreatedBy;
        //                fundCurrentBalanceMst.UpdatedBy = CreatedBy;
        //                //fundCurrentBalanceMst.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                fundCurrentBalanceMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                _dbContext.Entry(fundCurrentBalanceMst).State = EntityState.Modified;
        //                commonResponse.Message = "Fund Current Balance Updated Sucessfully.";
        //            }
        //            _dbContext.SaveChanges();

        //            commonResponse.Status = true;
        //            commonResponse.StatusCode = HttpStatusCode.OK;
        //            commonResponse.Data = fundCurrentBalanceMst;
        //        }
        //        else
        //        {
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //            commonResponse.Message = "Fund Id Not Found.";
        //        }
        //    }
        //    catch (Exception) { throw; }
        //    return commonResponse;
        //}

    }
}
