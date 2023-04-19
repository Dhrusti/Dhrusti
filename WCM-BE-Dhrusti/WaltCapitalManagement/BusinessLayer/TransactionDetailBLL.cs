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
    public class TransactionDetailBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public TransactionDetailBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        //public CommonResponse AddTransactionDetail(int TransactionId)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        var TransactionList = _commonRepo.clientTransactionList();
        //        var TransactionMstDetail = TransactionList.FirstOrDefault(x => x.Id == TransactionId);
        //        if (TransactionMstDetail != null)
        //        {
        //            var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == TransactionMstDetail.Fund);
        //            if (FundDetail != null)
        //            {
        //                // Variable Declarations------------------------------------
        //                DateTime BalanceDate = TransactionMstDetail.TransactionDate;
        //                string UnitType = TransactionMstDetail.UnitType;
        //                //  bool EditMode = false;
        //                bool HasFundTransaction = false;
        //                decimal UnitBalance = 0;
        //                decimal AmountBalance = 0;
        //                decimal CurrentUnitPrice = 0;
        //                decimal TotalFees = 0;
        //                decimal FundManagementFees = 0;
        //                decimal FundPerformanceFees = 0;
        //                decimal ManagementFees = 0;
        //                decimal PerformanceFees = 0;
        //                decimal IFAInitialFee = 0;
        //                decimal IFAAnnualFee = 0;
        //                decimal HWM = 0;
        //                decimal IFAFees = 0;
        //                decimal ComplianceFees = 0;
        //                decimal AuditFee = 0;
        //                decimal UnitPrice = 0;
        //                DateTime PrevBalanceDate = BalanceDate.Date.AddDays(-1);
        //                decimal PrevUnitPrice = 0;
        //                decimal PrevUnitBalance = 0;
        //                decimal PrevHWM = 0;

        //                // Data Fetching -------------------------------------------

        //                var FilteredTransactionList = TransactionList.Where(x => x.Fund == TransactionMstDetail.Fund && x.Client == TransactionMstDetail.Client).ToList();

        //                var ClientList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == 2).ToList();
        //                FilteredTransactionList = (from t in FilteredTransactionList
        //                                           join c in ClientList on t.Client equals c.Id
        //                                           select t).ToList();
        //                //var Transactions = TransactionList.Where(x => x.UnitType == UnitType && x.AllocateTo.ToLower() == InputPrice.ToLower()).ToList();
        //                var Transactions = FilteredTransactionList;
        //                //var CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault(x => x.UnitType == UnitType && (x.DynPrcInpLabel.ToLower() == InputPrice.ToLower()));
        //                var CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault();
        //                //var PrevCurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).FirstOrDefault(x => x.UnitType == UnitType && (x.DynPrcInpLabel.ToLower() == InputPrice.ToLower()));
        //                var PrevCurrentBalanceList = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).ToList();
        //                var PrevCurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).FirstOrDefault();

        //                if (FilteredTransactionList.Where(x => x.TransactionDate.Date == BalanceDate.Date).Count() > 1)
        //                {
        //                    Transactions = FilteredTransactionList.Where(x => x.UnitType == UnitType).ToList();
        //                    CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault(x => x.UnitType == UnitType);
        //                    PrevCurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).FirstOrDefault(x => x.UnitType == UnitType);
        //                    PrevCurrentBalanceList = PrevCurrentBalanceList.Where(x => x.UnitType == UnitType).ToList();
        //                }

        //                if (PrevCurrentBalanceList.Count() > 0)
        //                {
        //                    PrevUnitPrice = PrevCurrentBalanceList.FirstOrDefault().UnitPrice;
        //                    PrevUnitBalance = PrevCurrentBalanceList.Sum(x => x.UnitBalance);
        //                    PrevHWM = PrevCurrentBalanceList.Sum(x => x.Hwm);
        //                }

        //                var dynamicUnitsFieldsList = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundDetail.Id && x.Label.ToLower().Contains("management fee") && x.Label.ToLower().Contains("performance fee")).ToList();
        //                var CurrentYearTotalDays = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

        //                // Updating Variables -------------------------------------------
        //                // EditMode = CurrentBalanceDetail != null ? true : false;
        //                HasFundTransaction = FilteredTransactionList.Count() > 1 ? true : false;

        //                foreach (var trans in Transactions)
        //                {
        //                    if (trans.TransactionType.ToLower() == "buy")
        //                    {
        //                        if (trans.TransactionAmount > 0)
        //                            AmountBalance = AmountBalance + Convert.ToDecimal(trans.TransactionAmount);
        //                    }
        //                    else
        //                    {
        //                        if (trans.TransactionAmount > 0)
        //                            AmountBalance = AmountBalance - Convert.ToDecimal(trans.TransactionAmount);
        //                    }

        //                    if (trans.IfaupFrontFee > 0 && AmountBalance > 0)
        //                        //IFAInitialFee = (AmountBalance * IFAInitialFee) / 100;
        //                        IFAInitialFee = IFAInitialFee + ((AmountBalance * Convert.ToDecimal(trans.IfaupFrontFee)) / 100);
        //                    if (trans.IfaAnnualFee > 0 && AmountBalance > 0)
        //                    {
        //                        decimal IFAnualFeePercentage = (AmountBalance * Convert.ToDecimal(trans.IfaAnnualFee)) / 100;
        //                        if (IFAnualFeePercentage > 0)
        //                            //IFAAnnualFee = (AmountBalance * (IFAAnnualFee / 100)) / CurrentYearTotalDays;
        //                            IFAAnnualFee = IFAAnnualFee + (IFAnualFeePercentage / CurrentYearTotalDays);
        //                    }
        //                }

        //                if (UnitType.ToLower() == "unit a")
        //                {
        //                    if (FundDetail.ManagementFeeA > 0)
        //                        FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeA);
        //                    if (FundDetail.PerformanceFeeA > 0)
        //                        FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeA);
        //                }
        //                else if (UnitType.ToLower() == "unit b")
        //                {
        //                    if (FundDetail.ManagementFeeB > 0)
        //                        FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeB);
        //                    if (FundDetail.PerformanceFeeB > 0)
        //                        FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeB);
        //                }
        //                else
        //                {
        //                    foreach (var item in dynamicUnitsFieldsList)
        //                    {
        //                        if (Convert.ToDecimal(item.Value) > 0)
        //                        {
        //                            if (item.Label.Contains("management fee") && item.Label == UnitType.ToLower())
        //                            {
        //                                FundManagementFees = FundManagementFees + Convert.ToDecimal(item.Value);
        //                            }

        //                            if (item.Label.Contains("performance fee") && item.Label == UnitType.ToLower())
        //                            {
        //                                if (Convert.ToDecimal(item.Value) > 0)
        //                                    FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(item.Value);
        //                            }
        //                        }
        //                    }
        //                }

        //                if (AmountBalance > 0 && FundManagementFees > 0)
        //                    //formula : Total* Management fee of fund percentage / 365
        //                    ManagementFees = ((AmountBalance * FundManagementFees) / 100) / CurrentYearTotalDays; // 

        //                AuditFee = Convert.ToDecimal(FundDetail.AuditFee);
        //                ComplianceFees = Convert.ToDecimal(FundDetail.ComplianceFee);

        //                if (ComplianceFees > 0)
        //                    ComplianceFees = ComplianceFees / CurrentYearTotalDays;
        //                if (AuditFee > 0)
        //                    AuditFee = AuditFee / CurrentYearTotalDays;

        //                //Formula : IFAFees = Upfront IFA Fees + Anual IFA Fees 
        //                IFAFees = IFAInitialFee + IFAAnnualFee;

        //                if (!HasFundTransaction)
        //                {
        //                    //First Transaction

        //                    if (ManagementFees > 0 && FundPerformanceFees > 0)
        //                    {
        //                        // formula : performance fee * Management fee / 365
        //                        PerformanceFees = ((ManagementFees * FundPerformanceFees) / 100) / CurrentYearTotalDays;
        //                    }

        //                    TotalFees = ManagementFees + PerformanceFees + AuditFee + ComplianceFees + IFAInitialFee + IFAAnnualFee;

        //                    if (FundDetail.UnitStartingPrice > 0)
        //                    {
        //                        CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                    }

        //                    if (AmountBalance > 0 && CurrentUnitPrice > 0)
        //                    {
        //                        UnitBalance = AmountBalance / CurrentUnitPrice;
        //                    }

        //                    if (AmountBalance > 0 && AmountBalance > TotalFees)
        //                        HWM = AmountBalance - TotalFees;
        //                }
        //                else
        //                {
        //                    //Second Transactions--------------------------------------------------------

        //                    if (PrevCurrentBalanceDetail != null && FundPerformanceFees > 0)
        //                    //if (PrevCurrentBalanceDetail != null && FundPerformanceFees > 0)
        //                    {
        //                        if (PrevUnitPrice > CurrentUnitPrice)
        //                        {
        //                            CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                        }

        //                        if (PrevUnitPrice < CurrentUnitPrice)
        //                        {
        //                            // formula : ((Total Balance - Previous HWM) * Fund Performance Fee) / 100 
        //                            PerformanceFees = ((AmountBalance - PrevHWM) * FundPerformanceFees) / 100;
        //                        }
        //                        else if (PrevCurrentBalanceDetail != null && PrevUnitPrice > CurrentUnitPrice)
        //                        {
        //                            PerformanceFees = 0;
        //                        }
        //                        else if (ManagementFees > 0 && FundPerformanceFees > 0)
        //                        {
        //                            PerformanceFees = ((ManagementFees * FundPerformanceFees) / 100) / CurrentYearTotalDays;
        //                        }
        //                    }
        //                    else if (PrevCurrentBalanceDetail != null && PrevUnitPrice > CurrentUnitPrice)
        //                    {
        //                        PerformanceFees = 0;
        //                    }
        //                    else if (ManagementFees > 0 && FundPerformanceFees > 0)
        //                    {
        //                        PerformanceFees = ((ManagementFees * FundPerformanceFees) / 100) / CurrentYearTotalDays;
        //                    }

        //                    TotalFees = ManagementFees + PerformanceFees + AuditFee + ComplianceFees + IFAInitialFee + IFAAnnualFee;

        //                    if (AmountBalance > 0 && PrevCurrentBalanceDetail != null && PrevUnitBalance > 0 && AmountBalance > TotalFees)
        //                    {
        //                        CurrentUnitPrice = (AmountBalance - TotalFees) / PrevUnitBalance;
        //                    }
        //                    else if (FundDetail.UnitStartingPrice > 0)
        //                    {
        //                        CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                    }

        //                    if (AmountBalance > 0 && CurrentUnitPrice > 0)
        //                    {
        //                        UnitBalance = AmountBalance / CurrentUnitPrice;
        //                    }

        //                    if (PrevCurrentBalanceDetail != null && PrevUnitPrice < CurrentUnitPrice)
        //                    {
        //                        if (AmountBalance > 0)
        //                            // Formula : HWM = Total Amount - Total Fees
        //                            HWM = AmountBalance - TotalFees;
        //                    }
        //                    else if (PrevCurrentBalanceDetail != null && PrevUnitPrice > CurrentUnitPrice && PrevHWM > 0)
        //                    {
        //                        HWM = PrevHWM - TotalFees;
        //                    }
        //                    else if (AmountBalance > 0 && TotalFees > 0)
        //                    {
        //                        HWM = AmountBalance - TotalFees;
        //                    }
        //                }

        //                // Adding ClientTransactionDetail Table Using latest Values

        //                ClientTransactionDetail clientTransactionDetail = new ClientTransactionDetail();

        //                clientTransactionDetail.TransactionId = TransactionMstDetail.Id;
        //                clientTransactionDetail.UnitBalance = UnitBalance;
        //                clientTransactionDetail.AmountBalance = AmountBalance;
        //                clientTransactionDetail.UnitPrice = CurrentUnitPrice;
        //                clientTransactionDetail.IfaupFrontFee = IFAInitialFee;
        //                clientTransactionDetail.IfaannualFee = IFAAnnualFee;
        //                clientTransactionDetail.Ifafees = IFAFees;
        //                clientTransactionDetail.ManFees = ManagementFees;
        //                clientTransactionDetail.PerfFees = PerformanceFees;
        //                clientTransactionDetail.TotalFees = TotalFees;
        //                clientTransactionDetail.Hwm = HWM;
        //                clientTransactionDetail.UnitBalanceWithFee = 0;
        //                clientTransactionDetail.AmountBalanceWithFee = 0;
        //                clientTransactionDetail.UnitPriceWithFee = 0;
        //                clientTransactionDetail.UnitPriceNav = 0;
        //                clientTransactionDetail.IsActive = true;
        //                clientTransactionDetail.IsDeleted = false;
        //                clientTransactionDetail.CreatedBy = TransactionMstDetail.CreatedBy;
        //                clientTransactionDetail.UpdatedBy = TransactionMstDetail.UpdatedBy;
        //                clientTransactionDetail.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                clientTransactionDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                _dbContext.ClientTransactionDetails.Add(clientTransactionDetail);
        //                _dbContext.SaveChanges();

        //                commonResponse.Status = true;
        //                commonResponse.StatusCode = HttpStatusCode.OK;
        //                commonResponse.Message = "Client Transaction Details Added Successfully.";
        //                commonResponse.Data = clientTransactionDetail;
        //            }
        //            else
        //            {
        //                commonResponse.StatusCode = HttpStatusCode.NotFound;
        //                commonResponse.Message = "Fund Id Not Found.";
        //            }
        //        }
        //        else
        //        {
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //            commonResponse.Message = "Transaction Id Not Found.";
        //        }
        //    }
        //    catch (Exception ex) { throw; }
        //    return commonResponse;
        //}

        //public CommonResponse AddTransactionDetail(int TransactionId)
        //{
        //    CommonResponse commonResponse = new CommonResponse();
        //    try
        //    {
        //        var TransactionList = _commonRepo.clientTransactionList();
        //        var TransactionMstDetail = TransactionList.FirstOrDefault(x => x.Id == TransactionId);
        //        if (TransactionMstDetail != null)
        //        {
        //            var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == TransactionMstDetail.Fund);
        //            if (FundDetail != null)
        //            {
        //                // Variable Declarations------------------------------------
        //                DateTime BalanceDate = TransactionMstDetail.TransactionDate;
        //                string UnitType = TransactionMstDetail.UnitType;
        //                //  bool EditMode = false;
        //                bool HasFundTransaction = false;
        //                decimal UnitBalance = 0;
        //                decimal AmountBalance = 0;
        //                decimal CurrentUnitPrice = 0;
        //                decimal TotalFees = 0;
        //                decimal FundManagementFees = 0;
        //                decimal FundPerformanceFees = 0;
        //                decimal ManagementFees = 0;
        //                decimal PerformanceFees = 0;
        //                decimal IFAInitialFee = 0;
        //                decimal IFAAnnualFee = 0;
        //                decimal HWM = 0;
        //                decimal IFAFees = 0;
        //                decimal ComplianceFees = 0;
        //                decimal AuditFee = 0;
        //                decimal UnitPrice = 0;
        //                DateTime PrevBalanceDate = BalanceDate.Date.AddDays(-1);
        //                decimal PrevUnitPrice = 0;
        //                decimal PrevUnitBalance = 0;
        //                decimal PrevHWM = 0;
        //                decimal PrevTotalFeesDue = 0;
        //                decimal PrevFeesPayable = 0;
        //                decimal PrevFeesPaid = 0;
        //                decimal ServiceFee = 0;
        //                decimal BankToTradeStationTransferFee = 0;

        //                // Data Fetching -------------------------------------------
        //                var FilteredTransactionList = TransactionList.Where(x => x.Fund == TransactionMstDetail.Fund && x.Client == TransactionMstDetail.Client).ToList();

        //                var ClientList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == 2).ToList();
        //                FilteredTransactionList = (from t in FilteredTransactionList
        //                                           join c in ClientList on t.Client equals c.Id
        //                                           select t).ToList();
        //                //var Transactions = TransactionList.Where(x => x.UnitType == UnitType && x.AllocateTo.ToLower() == InputPrice.ToLower()).ToList();
        //                var Transactions = FilteredTransactionList;
        //                //var CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault(x => x.UnitType == UnitType && (x.DynPrcInpLabel.ToLower() == InputPrice.ToLower()));
        //                var CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault();
        //                //var PrevCurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).FirstOrDefault(x => x.UnitType == UnitType && (x.DynPrcInpLabel.ToLower() == InputPrice.ToLower()));
        //                var PrevCurrentBalanceList = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).ToList();
        //                var PrevCurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).FirstOrDefault();

        //                if (FilteredTransactionList.Where(x => x.TransactionDate.Date == BalanceDate.Date).Count() > 1)
        //                {
        //                    Transactions = FilteredTransactionList.Where(x => x.UnitType == UnitType).ToList();
        //                    CurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, BalanceDate).FirstOrDefault(x => x.UnitType == UnitType);
        //                    PrevCurrentBalanceDetail = _commonRepo.GetFundCurrentBalanceList(FundDetail.Id, PrevBalanceDate).FirstOrDefault(x => x.UnitType == UnitType);
        //                    PrevCurrentBalanceList = PrevCurrentBalanceList.Where(x => x.UnitType == UnitType).ToList();
        //                }

        //                if (PrevCurrentBalanceList.Count() > 0)
        //                {
        //                    PrevUnitPrice = PrevCurrentBalanceList.FirstOrDefault().UnitPrice;
        //                    PrevUnitBalance = PrevCurrentBalanceList.Sum(x => x.UnitBalance);
        //                    PrevHWM = PrevCurrentBalanceList.Sum(x => x.Hwm);
        //                }

        //                var dynamicUnitsFieldsList = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == FundDetail.Id && x.Label.ToLower().Contains("management fee") && x.Label.ToLower().Contains("performance fee")).ToList();
        //                var CurrentYearTotalDays = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

        //                // Updating Variables -------------------------------------------
        //                // EditMode = CurrentBalanceDetail != null ? true : false;
        //                HasFundTransaction = FilteredTransactionList.Count() > 1 ? true : false;

        //                foreach (var trans in Transactions)
        //                {
        //                    if (trans.TransactionType.ToLower() == "buy")
        //                    {
        //                        if (trans.TransactionAmount > 0)
        //                            AmountBalance = AmountBalance + Convert.ToDecimal(trans.TransactionAmount);
        //                    }
        //                    else
        //                    {
        //                        if (trans.TransactionAmount > 0)
        //                            AmountBalance = AmountBalance - Convert.ToDecimal(trans.TransactionAmount);
        //                    }

        //                    if (trans.IfaupFrontFee > 0 && AmountBalance > 0)
        //                        //IFAInitialFee = (AmountBalance * IFAInitialFee) / 100;
        //                        IFAInitialFee = IFAInitialFee + ((AmountBalance * Convert.ToDecimal(trans.IfaupFrontFee)) / 100);
        //                    if (trans.IfaAnnualFee > 0 && AmountBalance > 0)
        //                    {
        //                        decimal IFAnualFeePercentage = (AmountBalance * Convert.ToDecimal(trans.IfaAnnualFee)) / 100;
        //                        if (IFAnualFeePercentage > 0)
        //                            //IFAAnnualFee = (AmountBalance * (IFAAnnualFee / 100)) / CurrentYearTotalDays;
        //                            IFAAnnualFee = IFAAnnualFee + (IFAnualFeePercentage / CurrentYearTotalDays);
        //                    }
        //                }

        //                if (UnitType.ToLower() == "unit a")
        //                {
        //                    if (FundDetail.ManagementFeeA > 0)
        //                        FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeA);
        //                    if (FundDetail.PerformanceFeeA > 0)
        //                        FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeA);
        //                }
        //                else if (UnitType.ToLower() == "unit b")
        //                {
        //                    if (FundDetail.ManagementFeeB > 0)
        //                        FundManagementFees = FundManagementFees + Convert.ToDecimal(FundDetail.ManagementFeeB);
        //                    if (FundDetail.PerformanceFeeB > 0)
        //                        FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(FundDetail.PerformanceFeeB);
        //                }
        //                else
        //                {
        //                    foreach (var item in dynamicUnitsFieldsList)
        //                    {
        //                        if (Convert.ToDecimal(item.Value) > 0)
        //                        {
        //                            if (item.Label.Contains("management fee") && item.Label == UnitType.ToLower())
        //                            {
        //                                FundManagementFees = FundManagementFees + Convert.ToDecimal(item.Value);
        //                            }

        //                            if (item.Label.Contains("performance fee") && item.Label == UnitType.ToLower())
        //                            {
        //                                if (Convert.ToDecimal(item.Value) > 0)
        //                                    FundPerformanceFees = FundPerformanceFees + Convert.ToDecimal(item.Value);
        //                            }
        //                        }
        //                    }
        //                }

        //                if (AmountBalance > 0 && FundManagementFees > 0)
        //                    //formula : Total* Management fee of fund percentage / 365
        //                    ManagementFees = ((AmountBalance * FundManagementFees) / 100) / CurrentYearTotalDays; // 

        //                AuditFee = Convert.ToDecimal(FundDetail.AuditFee);
        //                ComplianceFees = Convert.ToDecimal(FundDetail.ComplianceFee);

        //                if (ComplianceFees > 0)
        //                    ComplianceFees = ComplianceFees / CurrentYearTotalDays;
        //                if (AuditFee > 0)
        //                    AuditFee = AuditFee / CurrentYearTotalDays;

        //                //Formula : IFAFees = Upfront IFA Fees + Anual IFA Fees 
        //                IFAFees = IFAInitialFee + IFAAnnualFee;

        //                if (!HasFundTransaction)
        //                {
        //                    //First Transaction

        //                    if (ManagementFees > 0 && FundPerformanceFees > 0)
        //                    {
        //                        // formula : performance fee * Management fee / 365
        //                        PerformanceFees = ((ManagementFees * FundPerformanceFees) / 100) / CurrentYearTotalDays;
        //                    }

        //                    TotalFees = ManagementFees + PerformanceFees + AuditFee + ComplianceFees + IFAInitialFee + IFAAnnualFee;

        //                    if (FundDetail.UnitStartingPrice > 0)
        //                    {
        //                        CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                    }

        //                    if (AmountBalance > 0 && CurrentUnitPrice > 0)
        //                    {
        //                        UnitBalance = AmountBalance / CurrentUnitPrice;
        //                    }

        //                    if (AmountBalance > 0 && AmountBalance > TotalFees)
        //                        HWM = AmountBalance - TotalFees;
        //                }
        //                else
        //                {
        //                    //Second Transactions--------------------------------------------------------

        //                    if (PrevCurrentBalanceDetail != null && FundPerformanceFees > 0)
        //                    //if (PrevCurrentBalanceDetail != null && FundPerformanceFees > 0)
        //                    {
        //                        if (PrevUnitPrice > CurrentUnitPrice)
        //                        {
        //                            CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                        }

        //                        if (PrevUnitPrice < CurrentUnitPrice)
        //                        {
        //                            // formula : ((Total Balance - Previous HWM) * Fund Performance Fee) / 100 
        //                            PerformanceFees = ((AmountBalance - PrevHWM) * FundPerformanceFees) / 100;
        //                        }
        //                        else if (PrevCurrentBalanceDetail != null && PrevUnitPrice > CurrentUnitPrice)
        //                        {
        //                            PerformanceFees = 0;
        //                        }
        //                        else if (ManagementFees > 0 && FundPerformanceFees > 0)
        //                        {
        //                            PerformanceFees = ((ManagementFees * FundPerformanceFees) / 100) / CurrentYearTotalDays;
        //                        }
        //                    }
        //                    else if (PrevCurrentBalanceDetail != null && PrevUnitPrice > CurrentUnitPrice)
        //                    {
        //                        PerformanceFees = 0;
        //                    }
        //                    else if (ManagementFees > 0 && FundPerformanceFees > 0)
        //                    {
        //                        PerformanceFees = ((ManagementFees * FundPerformanceFees) / 100) / CurrentYearTotalDays;
        //                    }

        //                    TotalFees = ManagementFees + PerformanceFees + AuditFee + ComplianceFees + IFAInitialFee + IFAAnnualFee;

        //                    if (AmountBalance > 0 && PrevCurrentBalanceDetail != null && PrevUnitBalance > 0 && AmountBalance > TotalFees)
        //                    {
        //                        CurrentUnitPrice = (AmountBalance - TotalFees) / PrevUnitBalance;
        //                    }
        //                    else if (FundDetail.UnitStartingPrice > 0)
        //                    {
        //                        CurrentUnitPrice = Convert.ToDecimal(FundDetail.UnitStartingPrice);
        //                    }

        //                    if (AmountBalance > 0 && CurrentUnitPrice > 0)
        //                    {
        //                        UnitBalance = AmountBalance / CurrentUnitPrice;
        //                    }

        //                    if (PrevCurrentBalanceDetail != null && PrevUnitPrice < CurrentUnitPrice)
        //                    {
        //                        if (AmountBalance > 0)
        //                            // Formula : HWM = Total Amount - Total Fees
        //                            HWM = AmountBalance - TotalFees;
        //                    }
        //                    else if (PrevCurrentBalanceDetail != null && PrevUnitPrice > CurrentUnitPrice && PrevHWM > 0)
        //                    {
        //                        HWM = PrevHWM - TotalFees;
        //                    }
        //                    else if (AmountBalance > 0 && TotalFees > 0)
        //                    {
        //                        HWM = AmountBalance - TotalFees;
        //                    }
        //                }

        //                // Adding ClientTransactionDetail Table Using latest Values

        //                ClientTransactionDetail clientTransactionDetail = new ClientTransactionDetail();

        //                clientTransactionDetail.TransactionId = TransactionMstDetail.Id;
        //                clientTransactionDetail.UnitBalance = UnitBalance;
        //                clientTransactionDetail.AmountBalance = AmountBalance;
        //                clientTransactionDetail.UnitPrice = CurrentUnitPrice;
        //                clientTransactionDetail.IfaupFrontFee = IFAInitialFee;
        //                clientTransactionDetail.IfaannualFee = IFAAnnualFee;
        //                clientTransactionDetail.Ifafees = IFAFees;
        //                clientTransactionDetail.ManFees = ManagementFees;
        //                clientTransactionDetail.PerfFees = PerformanceFees;
        //                clientTransactionDetail.TotalFees = TotalFees;
        //                clientTransactionDetail.Hwm = HWM;
        //                clientTransactionDetail.UnitBalanceWithFee = 0;
        //                clientTransactionDetail.AmountBalanceWithFee = 0;
        //                clientTransactionDetail.UnitPriceWithFee = 0;
        //                clientTransactionDetail.UnitPriceNav = 0;
        //                clientTransactionDetail.IsActive = true;
        //                clientTransactionDetail.IsDeleted = false;
        //                clientTransactionDetail.CreatedBy = TransactionMstDetail.CreatedBy;
        //                clientTransactionDetail.UpdatedBy = TransactionMstDetail.UpdatedBy;
        //                clientTransactionDetail.CreatedDate = _commonHelper.GetCurrentDateTime();
        //                clientTransactionDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();

        //                _dbContext.ClientTransactionDetails.Add(clientTransactionDetail);
        //                _dbContext.SaveChanges();

        //                commonResponse.Status = true;
        //                commonResponse.StatusCode = HttpStatusCode.OK;
        //                commonResponse.Message = "Client Transaction Details Added Successfully.";
        //                commonResponse.Data = clientTransactionDetail;
        //            }
        //            else
        //            {
        //                commonResponse.StatusCode = HttpStatusCode.NotFound;
        //                commonResponse.Message = "Fund Id Not Found.";
        //            }
        //        }
        //        else
        //        {
        //            commonResponse.StatusCode = HttpStatusCode.NotFound;
        //            commonResponse.Message = "Transaction Id Not Found.";
        //        }
        //    }
        //    catch (Exception ex) { throw; }
        //    return commonResponse;
        //}

    }
}
