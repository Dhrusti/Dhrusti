using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
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
    public class RunFeesBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public RunFeesBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse GetRunFees(GetRunFeesReqDTO getRunFeesReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetRunFeesResDTO getRunFeesResDTO = new GetRunFeesResDTO();
                getRunFeesResDTO.RunFeesList = new List<RunFeesResDetail>();
                var FeesList = _commonRepo.GetFundFeeList(0);
                var RunFeesList = _commonRepo.GetRunFeesDetailList(getRunFeesReqDTO.FundId).ToList();
                decimal TotalRunFees = 0;
                foreach (var item in FeesList)
                {
                    var feesDetail = RunFeesList.FirstOrDefault(x => x.Feesid == item.Id);
                    if (feesDetail != null)
                    {
                        RunFeesResDetail runFeesDetail = new RunFeesResDetail();
                        runFeesDetail.FeesId = item.Id;
                        runFeesDetail.FeesName = item.FeesName;
                        runFeesDetail.LastRunDate = feesDetail.LastRunDate;
                        runFeesDetail.LastAmountStr = _commonHelper.GetFormatedDecimal(feesDetail.LastAmount);
                        runFeesDetail.NextRunDate = feesDetail.NextRunDate.Value;
                        runFeesDetail.PendingAmountStr = _commonHelper.GetFormatedDecimal(feesDetail.PendingAmount);
                        runFeesDetail.TotalStr = _commonHelper.GetFormatedDecimal(feesDetail.Total);
                        runFeesDetail.VATStr = _commonHelper.GetFormatedDecimal(feesDetail.Vat);
                        runFeesDetail.TotalInclVATStr = _commonHelper.GetFormatedDecimal(feesDetail.TotalIncVat);

                        getRunFeesResDTO.RunFeesList.Add(runFeesDetail);
                        TotalRunFees = TotalRunFees + feesDetail.PendingAmount;
                    }
                }
                getRunFeesResDTO.TotalRunFeesStr = _commonHelper.GetFormatedDecimal(TotalRunFees);
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Message = "Success.";
                commonResponse.Data = getRunFeesResDTO;
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse CalculateRunFees(CalculateRunFeesReqDTO calculateRunFeesReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var FundDetails = _commonRepo.fundList().FirstOrDefault(x => x.Id == calculateRunFeesReqDTO.FundId);
                var FundFees = _commonRepo.GetRunFeesDetailList(calculateRunFeesReqDTO.FundId).FirstOrDefault(x => x.Feesid == calculateRunFeesReqDTO.FeesId);
                var PricingList = _commonRepo.GetPricingList(FundDetails.Id).ToList();
               
                int TotalNoOfDays = 0;
                if (FundFees != null && FundDetails != null)
                {
                    RunFeesDetail runFeesDetail = new RunFeesDetail();
                    runFeesDetail = FundFees;

                    TotalNoOfDays = Convert.ToInt32((calculateRunFeesReqDTO.NextRunDate.Date - runFeesDetail.LastRunDate.Date).TotalDays);
                    DateTime LastDateOfMonth = _commonHelper.GetLastDateOfMonth(calculateRunFeesReqDTO.NextRunDate.Year, calculateRunFeesReqDTO.NextRunDate.Month);
                    var CurrentYearTotalDays = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;
                    PricingList = PricingList.Where(x => x.TransactionDate.Value.Date >= runFeesDetail.LastRunDate.Date && x.TransactionDate.Value.Date <= calculateRunFeesReqDTO.NextRunDate.Date).ToList();

                    runFeesDetail.LastRunDate = calculateRunFeesReqDTO.NextRunDate;
                    runFeesDetail.LastAmount = runFeesDetail.PendingAmount;
                    runFeesDetail.NextRunDate = LastDateOfMonth;
                   
                    decimal CalculatedFee = 0;
                    if (PricingList.Count > 0)
                    {
                        if (FundFees.Feesid == 1) // Audit Fees
                        {
                            //decimal PerDayAuditFee = Convert.ToDecimal(FundDetails.AuditFee) / CurrentYearTotalDays;
                            //CalculatedFee = PerDayAuditFee * TotalNoOfDays;
                            CalculatedFee = PricingList.Select(x => x.AuditFees).Sum();
                        }
                        else if (FundFees.Feesid == 2) // Compliance Fee
                        {
                            //decimal PerDayComplianceFee = Convert.ToDecimal(FundDetails.ComplianceFee) / CurrentYearTotalDays;
                            //CalculatedFee = PerDayComplianceFee * TotalNoOfDays;
                            CalculatedFee = PricingList.Select(x => x.ComplianceFees).Sum();
                        }
                        else if (FundFees.Feesid == 3) // Management Fee
                        {
                            CalculatedFee = PricingList.Select(x => x.ManFees).Sum();
                        }
                        else if (FundFees.Feesid == 4) // Performance Fee
                        {
                            CalculatedFee = PricingList.Select(x => x.PerfFees).Sum();
                        }
                        else if (FundFees.Feesid == 5) // IFA Fee
                        {
                            CalculatedFee = PricingList.Select(x => x.Ifafees).Sum();
                        }
                    }

                    runFeesDetail.PendingAmount = CalculatedFee;
                    runFeesDetail.Total = runFeesDetail.PendingAmount;
                    runFeesDetail.Vat = FundDetails.IsVatapplicable ? ((runFeesDetail.Total * Convert.ToDecimal(FundDetails.Vat)) / 100) : 0;
                    runFeesDetail.TotalIncVat = runFeesDetail.Total + runFeesDetail.Vat;
                    runFeesDetail.UpdatedBy = calculateRunFeesReqDTO.UpdatedBy;
                    runFeesDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(runFeesDetail).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    GetRunFeesReqDTO getRunFeesReqDTO = new GetRunFeesReqDTO();
                    getRunFeesReqDTO.FundId = FundDetails.Id;
                    commonResponse = GetRunFees(getRunFeesReqDTO);

                    //commonResponse.Status = true;
                    //commonResponse.StatusCode = HttpStatusCode.OK;
                    //commonResponse.Message = "Fees Calculated Successfully!";
                    //commonResponse.Data = getRunFeesResDTO;
                }
                else
                {
                    commonResponse.Message = "Invalid Fund or Fees.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        #region Internal Methods
        public CommonResponse AddRunFeesWithBlankData(int FundId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<RunFeesDetail> runFeesDetailList = new List<RunFeesDetail>();
                var FundFees = _commonRepo.GetFundFeeList(0).ToList();
                var FundDetails = _commonRepo.fundList(false, false).FirstOrDefault(x => x.Id == FundId);
                if (FundDetails != null)
                {
                    foreach (var item in FundFees)
                    {
                        RunFeesDetail runFeesDetail = new RunFeesDetail();
                        runFeesDetail.FundId = FundId;
                        runFeesDetail.Feesid = item.Id;
                        runFeesDetail.LastRunDate = FundDetails.InceptionDate;
                        runFeesDetail.LastAmount = 0;

                        //int year = FundDetails.InceptionDate.Year;
                        //int month = FundDetails.InceptionDate.Month;
                        //int LastDayOfMonth = DateTime.DaysInMonth(year, month);
                        //DateTime LastDateOfMonth = new DateTime(year, month, LastDayOfMonth);
                        DateTime LastDateOfMonth = _commonHelper.GetLastDateOfMonth(FundDetails.InceptionDate.Year, FundDetails.InceptionDate.Month);

                        runFeesDetail.NextRunDate = LastDateOfMonth;
                        runFeesDetail.PendingAmount = 0;
                        runFeesDetail.Total = 0;
                        runFeesDetail.Vat = 0;
                        runFeesDetail.TotalIncVat = 0;
                        runFeesDetail.IsActive = false;
                        runFeesDetail.IsDeleted = false;
                        runFeesDetail.CreatedBy = FundDetails.CreatedBy;
                        runFeesDetail.UpdatedBy = FundDetails.CreatedBy;
                        runFeesDetail.CreatedDate = _commonHelper.GetCurrentDateTime();
                        runFeesDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        runFeesDetailList.Add(runFeesDetail);
                    }
                    _dbContext.RunFeesDetails.AddRange(runFeesDetailList);
                    _dbContext.SaveChanges();

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Run Fees Added Successfully!";
                    commonResponse.Data = runFeesDetailList;
                }
                else
                {
                    commonResponse.Message = "Fund Not Found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse ActivateRunFeesStatus(int FundId, bool IsActive)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var FundDetails = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId);
                if (FundDetails != null)
                {
                    var RunFeesList = _commonRepo.GetAllRunFeesDetailList(FundDetails.Id).Where(x => !x.IsDeleted).ToList();
                    foreach (var item in RunFeesList)
                    {
                        RunFeesDetail runFeesDetail = new RunFeesDetail();
                        runFeesDetail = item;
                        runFeesDetail.IsActive = IsActive;
                        _dbContext.Entry(runFeesDetail).State = EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Run Fees Status Updated.";

                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        #endregion

    }
}
