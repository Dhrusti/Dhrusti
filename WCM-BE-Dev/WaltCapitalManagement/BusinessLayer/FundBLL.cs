using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Transactions;

namespace BusinessLayer
{
    public class FundBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly FundDynamicInputPriceBLL _fundDynamicInput;
        private readonly FundPricingBLL _fundPricingBLL;
        private readonly RunFeesBLL _runFeesBLL;
        public FundBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration iConfiguration, FundDynamicInputPriceBLL fundDynamicInput, FundPricingBLL fundPricingBLL, FundCurrentBalanceBLL fundCurrentBalanceBLL, RunFeesBLL runFeesBLL)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _fundDynamicInput = fundDynamicInput;
            _fundPricingBLL = fundPricingBLL;
            _runFeesBLL = runFeesBLL;
        }

        public CommonResponse GetAllFundList()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var fund = _commonRepo.fundList().Where(x => x.IsActive == true || x.IsDeleted == true).ToList();
                if (fund.Count > 0)
                {
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

                TypeAdapterConfig<FundMst, GetFundResDTO>.NewConfig()
                .Map(d => d.FundId, s => s.Id)
                .IgnoreNullValues(true);

                commonResponse.Data = fund.Adapt<List<GetFundResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetFundList(GetFundListReqDTO getFundListByStatusReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<FundMst> fund = new List<FundMst>();
                if (getFundListByStatusReqDTO.IsActive == true)
                {
                    fund = _commonRepo.fundList().Where(x => x.IsActive == getFundListByStatusReqDTO.IsActive).OrderBy(x => x.CreatedDate).ToList();
                }
                else
                {
                    fund = _commonRepo.fundList().ToList();
                }

                if (fund != null && fund.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.Message = "Success.";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Data not Found";
                }

                TypeAdapterConfig<FundMst, GetFundListResDTO>.NewConfig()
                .Map(d => d.FundId, s => s.Id)
                .IgnoreNullValues(true);

                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Data = fund.Adapt<List<GetFundListResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddFund(AddFundReqDTO addFundReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddFundResDTO addFundResDTO = new AddFundResDTO();
            try
            {
                using (var scope = new TransactionScope())
                {
                    var funds = _commonRepo.fundList().Where(x => x.FundName.ToLower() == addFundReqDTO.FundName.ToLower()).FirstOrDefault();
                    if (funds == null)
                    {
                        FundMst fundMst = new FundMst();
                        fundMst.FundName = addFundReqDTO.FundName;
                        fundMst.FundRiskRating = addFundReqDTO.FundRiskRating;
                        fundMst.IsVatapplicable = addFundReqDTO.IsVatapplicable;
                        fundMst.Vat = addFundReqDTO.Vat ?? 0;
                        fundMst.FundPhilosophy = addFundReqDTO.FundPhilosophy;

                        var pricingInput = Convert.ToString(addFundReqDTO.PricingInputs).Split(',');
                        string finalPricingInput = "";
                        foreach (var item in pricingInput)
                        {
                            finalPricingInput += string.IsNullOrEmpty(finalPricingInput) ? item.Trim() : "," + item.Trim();
                        }

                        fundMst.PricingInputs = finalPricingInput;
                        fundMst.InceptionDate = addFundReqDTO.InceptionDate;
                        fundMst.UnitStartingPrice = addFundReqDTO.UnitStartingPrice;
                        fundMst.ManagementFeeA = addFundReqDTO.ManagementFeeA;
                        fundMst.ManagementFeeB = addFundReqDTO.ManagementFeeB;
                        fundMst.PerformanceFeeA = addFundReqDTO.PerformanceFeeA;
                        fundMst.PerformanceFeeB = addFundReqDTO.PerformanceFeeB;
                        fundMst.AuditFee = addFundReqDTO.AuditFee;
                        fundMst.Currency = addFundReqDTO.Currency;
                        fundMst.ComplianceFee = addFundReqDTO.ComplianceFee;
                        fundMst.TrusteesFee = addFundReqDTO.TrusteesFee;
                        fundMst.IsFactSheetCreated = false;
                        fundMst.CreatedBy = addFundReqDTO.CreatedBy;
                        fundMst.UpdatedBy = addFundReqDTO.CreatedBy;
                        fundMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                        fundMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        fundMst.IsActive = false;
                        fundMst.IsDeleted = false;

                        _dbContext.FundMsts.Add(fundMst);
                        _dbContext.SaveChanges();

                        List<FundDynamicFieldMst> fundDynamicFieldMsts = new List<FundDynamicFieldMst>();
                        FundDynamicFieldMst fundDynamicFieldMst;
                        //Preyansi Patel 23 - 11 - 2022

                        bool isScopeDis = true;
                        foreach (var item in addFundReqDTO.FundDynamicField)
                        {
                            bool IsValidFee = true;
                            if (item.Label.ToLower().Contains("management fee") || item.Label.ToLower().Contains("performance fee"))
                            {
                                string lable = string.Empty;
                                string lable1 = string.Empty;
                                if (item.Label.ToLower().Contains("management fee"))
                                {
                                    lable = Regex.Replace(item.Label, "management fee", string.Empty, RegexOptions.IgnoreCase).Trim();
                                    if (string.IsNullOrEmpty(lable))
                                    {
                                        IsValidFee = false;
                                    }
                                }
                                if (item.Label.ToLower().Contains("performance fee"))
                                {
                                    lable1 = Regex.Replace(item.Label, "performance fee", string.Empty, RegexOptions.IgnoreCase).Trim();
                                    if (string.IsNullOrEmpty(lable1))
                                    {
                                        IsValidFee = false;
                                    }
                                }
                            }

                            if (IsValidFee)
                            {
                                fundDynamicFieldMst = new FundDynamicFieldMst();
                                fundDynamicFieldMst.Label = item.Label.Trim();
                                fundDynamicFieldMst.Value = item.Value.Trim();
                                fundDynamicFieldMst.RowId = item.RowId;
                                fundDynamicFieldMst.FundId = fundMst.Id;
                                fundDynamicFieldMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                fundDynamicFieldMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                fundDynamicFieldMst.CreatedBy = fundMst.CreatedBy;
                                fundDynamicFieldMst.UpdatedBy = fundMst.CreatedBy;
                                fundDynamicFieldMst.IsActive = true;
                                fundDynamicFieldMst.IsDeleted = false;

                                fundDynamicFieldMsts.Add(fundDynamicFieldMst);
                            }
                        }
                        _dbContext.FundDynamicFieldMsts.AddRange(fundDynamicFieldMsts);
                        _dbContext.SaveChanges();

                        if (isScopeDis)
                        {
                            AddFundHistory(fundMst);
                            AddFundDynamicFieldHistory(fundDynamicFieldMsts);
                            _fundDynamicInput.AddFundDynamicInputPriceWithBlankData(fundMst.Id, fundMst.PricingInputs, fundMst.CreatedBy, false);
                            _fundPricingBLL.AddPricingWithBlankData(fundMst.Id);
                            _runFeesBLL.AddRunFeesWithBlankData(fundMst.Id);

                            scope.Complete();
                            addFundResDTO.FundId = fundMst.Id;
                            addFundResDTO.FundName = fundMst.FundName;
                            addFundResDTO.IsActive = fundMst.IsActive;
                            addFundResDTO.IsFactSheetCreated = fundMst.IsFactSheetCreated;

                            commonResponse.Message = "Fund added successfully!";
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Data = addFundResDTO;
                        }
                        else
                        {
                            commonResponse.Message = "Enter Proper Dynamic Name";
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        }
                        //Preyansi Patel 23 - 11 - 2022
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Fund is already exists!";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeleteFund(DeleteFundReqDTO deleteFundReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteFundResDTO deleteFundResDTO = new DeleteFundResDTO();
            try
            {
                var pwddecrpt = _commonHelper.EncryptString(deleteFundReqDTO.Pwd);
                var Ispwdexists = _commonRepo.permissionCredentialList().Where(x => x.Pwd == pwddecrpt).FirstOrDefault();

                if (Ispwdexists != null)
                {
                    var deleteFund = _commonRepo.fundList().FirstOrDefault(x => x.Id == deleteFundReqDTO.FundId);
                    var deleteFundBenchMark = _commonRepo.FundBenchMarkList().Where(x => x.FundId == deleteFundReqDTO.FundId).ToList();
                    if (deleteFund != null)
                    {
                        using (var scope = new TransactionScope())
                        {
                            FundMst fundMst = deleteFund;
                            fundMst.IsDeleted = true;

                            _dbContext.Entry(fundMst).State = EntityState.Modified;
                            _dbContext.SaveChanges();

                            foreach (var deletefund in deleteFundBenchMark)
                            {
                                FundBenchMarkMst fundBenchMarkMst = deletefund;

                                deletefund.IsDeleted = true;
                                _dbContext.Entry(fundBenchMarkMst).State = EntityState.Modified;
                                _dbContext.SaveChanges();
                            }

                            DeleteFundHistory(fundMst.Id);
                            DeleteFundDynamicFieldHistory(fundMst.Id);

                            scope.Complete();
                        }

                        deleteFundResDTO.FundId = deleteFund.Id;

                        commonResponse.Data = deleteFundResDTO;
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Fund deleted successfully!";

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Fund not exists";
                    }
                }

                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Fail to delete fund, Enter valid password";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetFundById(GetFundByIdReqDTO getFundByIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                FundMst fundMsts = _commonRepo.fundList().Where(x => x.Id == getFundByIdReqDTO.FundId).FirstOrDefault() ?? new FundMst();
                var fundMst1 = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == getFundByIdReqDTO.FundId).ToList();

                bool IsTransactionExists = _commonRepo.clientTransactionList().Where(x => x.Fund == getFundByIdReqDTO.FundId).Any();
                GetFundByIdResDTO fundByIdResDTO = new GetFundByIdResDTO();

                if (fundMsts != null)
                {
                    fundByIdResDTO.FundId = fundMsts.Id;
                    fundByIdResDTO.FundRiskRating = fundMsts.FundRiskRating;
                    fundByIdResDTO.IsVatapplicable = fundMsts.IsVatapplicable;
                    fundByIdResDTO.Vat = fundMsts.Vat;
                    fundByIdResDTO.FundName = fundMsts.FundName;
                    fundByIdResDTO.FundPhilosophy = fundMsts.FundPhilosophy;
                    fundByIdResDTO.PricingInputs = fundMsts.PricingInputs;
                    fundByIdResDTO.InceptionDate = fundMsts.InceptionDate;
                    fundByIdResDTO.UnitStartingPrice = fundMsts.UnitStartingPrice;
                    fundByIdResDTO.ManagementFeeA = fundMsts.ManagementFeeA;
                    fundByIdResDTO.ManagementFeeB = fundMsts.ManagementFeeB;
                    fundByIdResDTO.PerformanceFeeA = fundMsts.PerformanceFeeA;
                    fundByIdResDTO.PerformanceFeeB = fundMsts.PerformanceFeeB;
                    fundByIdResDTO.AuditFee = fundMsts.AuditFee;
                    fundByIdResDTO.Currency = fundMsts.Currency;
                    fundByIdResDTO.ComplianceFee = fundMsts.ComplianceFee;
                    fundByIdResDTO.TrusteesFee = fundMsts.TrusteesFee;
                    fundByIdResDTO.IsFeesEditable = true; //!IsTransactionExists;

                    fundByIdResDTO.FundDynamicField = new List<FundDynamicField>();

                    FundDynamicField fundDynamicField;
                    foreach (var item in fundMst1)
                    {
                        fundByIdResDTO.FundDynamicField.Add(new FundDynamicField { RowId = Convert.ToInt32(item.RowId), Label = item.Label, Value = item.Value });
                    }

                    commonResponse.Data = fundByIdResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Data Found Successfully!";
                }

                else
                {
                    commonResponse.Data = null;
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Fund Details Not Found!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateFundStatus(UpdateFundStatusReqDTO updateFundReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateFundStatusResDTO updateFundResDTO = new UpdateFundStatusResDTO();
            try
            {
                var fundStatus = _commonRepo.fundList().FirstOrDefault(x => x.Id == updateFundReqDTO.FundId);
                if (fundStatus != null)
                {
                    FundMst fundMst = fundStatus;
                    fundMst.IsActive = updateFundReqDTO.IsActive;

                    _dbContext.Entry(fundMst).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    updateFundResDTO.IsActive = fundMst.IsActive;

                    commonResponse.Data = updateFundResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Successfully Updated";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not update the data";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateFund(UpdateFundReqDTO updateFundReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateFundResDTO updateFundResDTO = new UpdateFundResDTO();
            try
            {
                using (var scope = new TransactionScope())
                {
                    var funds = _commonRepo.fundList().Where(x => (x.IsActive == true && !x.IsDeleted && x.Id != updateFundReqDTO.FundId) && (x.FundName.ToLower() == updateFundReqDTO.FundName.ToLower())).ToList();
                    if (funds.Count == 0)
                    {
                        var IsExistFund = _commonRepo.fundList().Where(x => x.Id == updateFundReqDTO.FundId).FirstOrDefault();
                        if (IsExistFund != null)
                        {
                            IsExistFund.FundName = updateFundReqDTO.FundName;
                            IsExistFund.FundRiskRating = updateFundReqDTO.FundRiskRating;
                            IsExistFund.IsVatapplicable = updateFundReqDTO.IsVatapplicable;
                            IsExistFund.Vat = updateFundReqDTO.Vat ?? 0;
                            IsExistFund.FundPhilosophy = updateFundReqDTO.FundPhilosophy;
                            IsExistFund.ManagementFeeA = updateFundReqDTO.ManagementFeeA;
                            IsExistFund.ManagementFeeB = updateFundReqDTO.ManagementFeeB;
                            IsExistFund.PerformanceFeeA = updateFundReqDTO.PerformanceFeeA;
                            IsExistFund.PerformanceFeeB = updateFundReqDTO.PerformanceFeeB;
                            IsExistFund.AuditFee = updateFundReqDTO.AuditFee;
                            IsExistFund.ComplianceFee = updateFundReqDTO.ComplianceFee;
                            IsExistFund.TrusteesFee = updateFundReqDTO.TrusteesFee;
                            IsExistFund.UpdatedBy = updateFundReqDTO.UpdatedBy;
                            IsExistFund.UpdatedDate = _commonHelper.GetCurrentDateTime();

                            _dbContext.Entry(IsExistFund).State = EntityState.Modified;
                            _dbContext.SaveChanges();

                            var isExistFundDynamicField = _commonRepo.fundDynamicFieldList().Where(x => x.FundId == updateFundReqDTO.FundId).ToList() ?? new List<FundDynamicFieldMst>();
                            bool isScopeDis = true;

                            List<FundDynamicFieldMst> fundDynamicFieldMsts = new List<FundDynamicFieldMst>();
                            if (isExistFundDynamicField.Count != 0)
                            {
                                foreach (var item1 in isExistFundDynamicField)
                                {
                                    _dbContext.FundDynamicFieldMsts.Remove(item1);
                                    _dbContext.SaveChanges();
                                }

                                FundDynamicFieldMst fundDynamicFieldMst;

                                foreach (var item in updateFundReqDTO.FundDynamicField)
                                {
                                    bool IsValidFee = true;
                                    if (item.Label.ToLower().Contains("management fee") || item.Label.ToLower().Contains("performance fee"))
                                    {
                                        string lable = string.Empty;
                                        string lable1 = string.Empty;
                                        if (item.Label.ToLower().Contains("management fee"))
                                        {
                                            lable = Regex.Replace(item.Label, "management fee", string.Empty, RegexOptions.IgnoreCase).Trim();
                                            if (string.IsNullOrEmpty(lable))
                                            {
                                                IsValidFee = false;
                                            }
                                        }
                                        if (item.Label.ToLower().Contains("performance fee"))
                                        {
                                            lable1 = Regex.Replace(item.Label, "performance fee", string.Empty, RegexOptions.IgnoreCase).Trim();
                                            if (string.IsNullOrEmpty(lable1))
                                            {
                                                IsValidFee = false;
                                            }
                                        }
                                    }

                                    if (IsValidFee)
                                    {
                                        fundDynamicFieldMst = new FundDynamicFieldMst();
                                        fundDynamicFieldMst.Label = item.Label;
                                        fundDynamicFieldMst.Value = item.Value;
                                        fundDynamicFieldMst.RowId = item.RowId;
                                        fundDynamicFieldMst.FundId = IsExistFund.Id;
                                        fundDynamicFieldMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                        fundDynamicFieldMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                        fundDynamicFieldMst.CreatedBy = IsExistFund.CreatedBy;
                                        fundDynamicFieldMst.UpdatedBy = IsExistFund.CreatedBy;
                                        fundDynamicFieldMst.IsActive = true;
                                        fundDynamicFieldMst.IsDeleted = false;

                                        fundDynamicFieldMsts.Add(fundDynamicFieldMst);
                                    }
                                }
                            }
                            else
                            {
                                FundDynamicFieldMst fundDynamicFieldMst;

                                foreach (var item in updateFundReqDTO.FundDynamicField)
                                {
                                    bool IsValidFee = true;
                                    if (item.Label.ToLower().Contains("management fee") || item.Label.ToLower().Contains("performance fee"))
                                    {
                                        string lable = string.Empty;
                                        string lable1 = string.Empty;
                                        if (item.Label.ToLower().Contains("management fee"))
                                        {
                                            lable = Regex.Replace(item.Label, "management fee", string.Empty, RegexOptions.IgnoreCase).Trim();
                                            if (string.IsNullOrEmpty(lable))
                                            {
                                                IsValidFee = false;
                                            }
                                        }
                                        if (item.Label.ToLower().Contains("performance fee"))
                                        {
                                            lable1 = Regex.Replace(item.Label, "performance fee", string.Empty, RegexOptions.IgnoreCase).Trim();
                                            if (string.IsNullOrEmpty(lable1))
                                            {
                                                IsValidFee = false;
                                            }
                                        }
                                    }
                                    if (IsValidFee)
                                    {

                                        fundDynamicFieldMst = new FundDynamicFieldMst();
                                        fundDynamicFieldMst.Label = item.Label;
                                        fundDynamicFieldMst.Value = item.Value;
                                        fundDynamicFieldMst.RowId = item.RowId;
                                        fundDynamicFieldMst.FundId = IsExistFund.Id;
                                        fundDynamicFieldMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                        fundDynamicFieldMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                        fundDynamicFieldMst.CreatedBy = IsExistFund.CreatedBy;
                                        fundDynamicFieldMst.UpdatedBy = IsExistFund.CreatedBy;
                                        fundDynamicFieldMst.IsActive = true;
                                        fundDynamicFieldMst.IsDeleted = false;

                                        fundDynamicFieldMsts.Add(fundDynamicFieldMst);
                                    }
                                    else
                                    {
                                        scope.Dispose();
                                        isScopeDis = false;
                                    }
                                }
                            }
                            if (isScopeDis)
                            {

                                _dbContext.FundDynamicFieldMsts.AddRange(fundDynamicFieldMsts);
                                _dbContext.SaveChanges();

                                AddFundHistory(IsExistFund);
                                AddFundDynamicFieldHistory(fundDynamicFieldMsts);

                                scope.Complete();
                                updateFundResDTO.FundId = IsExistFund.Id;
                                updateFundResDTO.FundName = IsExistFund.FundName;
                                updateFundResDTO.IsActive = IsExistFund.IsActive;
                                updateFundResDTO.IsFactSheetCreated = IsExistFund.IsFactSheetCreated;

                                commonResponse.Message = "Fund updated successfully!";
                                commonResponse.Status = true;
                                commonResponse.StatusCode = HttpStatusCode.OK;
                                commonResponse.Data = updateFundResDTO;

                            }
                            else
                            {
                                commonResponse.Message = "Enter Proper Dynamic Name";
                                commonResponse.Status = false;
                                commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            }
                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "Can Not Find Fund!";
                        }
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Fund is already exists!";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddFundHistory(FundMst fundMst)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                FundHistoryMst fundHistoryMst = new FundHistoryMst();
                fundHistoryMst.FundId = fundMst.Id;
                fundHistoryMst.FundName = fundMst.FundName;
                fundHistoryMst.FundRiskRating = fundMst.FundRiskRating;
                fundHistoryMst.IsVatapplicable = fundMst.IsVatapplicable;
                fundHistoryMst.Vat = fundMst.Vat;
                fundHistoryMst.FundPhilosophy = fundMst.FundPhilosophy;
                fundHistoryMst.PricingInputs = fundMst.PricingInputs;
                fundHistoryMst.InceptionDate = fundMst.InceptionDate;
                fundHistoryMst.UnitStartingPrice = fundMst.UnitStartingPrice;
                fundHistoryMst.ManagementFeeA = fundMst.ManagementFeeA;
                fundHistoryMst.ManagementFeeB = fundMst.ManagementFeeB;
                fundHistoryMst.PerformanceFeeA = fundMst.PerformanceFeeA;
                fundHistoryMst.PerformanceFeeB = fundMst.PerformanceFeeB;
                fundHistoryMst.AuditFee = fundMst.AuditFee;
                fundHistoryMst.Currency = fundMst.Currency;
                fundHistoryMst.ComplianceFee = fundMst.ComplianceFee;
                fundHistoryMst.TrusteesFee = fundMst.TrusteesFee;
                fundHistoryMst.IsFactSheetCreated = false;
                fundHistoryMst.CreatedBy = fundMst.CreatedBy;
                fundHistoryMst.UpdatedBy = fundMst.UpdatedBy;
                fundHistoryMst.CreatedDate = fundMst.CreatedDate;
                fundHistoryMst.UpdatedDate = fundMst.UpdatedDate;
                fundHistoryMst.IsActive = fundMst.IsActive;
                fundHistoryMst.IsDeleted = fundMst.IsDeleted;

                _dbContext.FundHistoryMsts.Add(fundHistoryMst);
                _dbContext.SaveChanges();
            }
            catch { throw; }
            return commonResponse;
        }

        public CommonResponse DeleteFundHistory(int fundId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<FundHistoryMst> fundHistoryMsts = _dbContext.FundHistoryMsts.Where(x => x.FundId == fundId).ToList();
                foreach (var item in fundHistoryMsts)
                {
                    item.IsDeleted = true;
                }
                _dbContext.FundHistoryMsts.UpdateRange(fundHistoryMsts);
                _dbContext.SaveChanges();

            }
            catch { throw; }
            return commonResponse;
        }

        public CommonResponse AddFundDynamicFieldHistory(List<FundDynamicFieldMst> fundDynamicFieldMsts)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<FundDynamicFieldHistoryMst> fundDynamicFieldHistoryMsts = new List<FundDynamicFieldHistoryMst>();
                FundDynamicFieldHistoryMst fundDynamicFieldHistoryMst;
                foreach (var item in fundDynamicFieldMsts)
                {
                    fundDynamicFieldHistoryMst = new FundDynamicFieldHistoryMst();
                    fundDynamicFieldHistoryMst.DynamicFieldId = item.Id;
                    fundDynamicFieldHistoryMst.FundId = item.FundId;
                    fundDynamicFieldHistoryMst.Label = item.Label;
                    fundDynamicFieldHistoryMst.Value = item.Value;
                    fundDynamicFieldHistoryMst.IsActive = item.IsActive;
                    fundDynamicFieldHistoryMst.IsDeleted = item.IsDeleted;
                    fundDynamicFieldHistoryMst.CreatedBy = item.CreatedBy;
                    fundDynamicFieldHistoryMst.UpdatedBy = item.UpdatedBy;
                    fundDynamicFieldHistoryMst.CreatedDate = item.CreatedDate;
                    fundDynamicFieldHistoryMst.UpdatedDate = item.UpdatedDate;
                    fundDynamicFieldHistoryMst.RowId = item.RowId;

                    fundDynamicFieldHistoryMsts.Add(fundDynamicFieldHistoryMst);
                }

                _dbContext.FundDynamicFieldHistoryMsts.AddRange(fundDynamicFieldHistoryMsts);
                _dbContext.SaveChanges();
            }
            catch { throw; }
            return commonResponse;
        }

        public CommonResponse DeleteFundDynamicFieldHistory(int fundId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<FundDynamicFieldHistoryMst> fundDynamicFieldHistoryMsts = _dbContext.FundDynamicFieldHistoryMsts.Where(x => x.FundId == fundId).ToList();
                foreach (var item in fundDynamicFieldHistoryMsts)
                {
                    item.IsDeleted = true;
                }
                _dbContext.FundDynamicFieldHistoryMsts.UpdateRange(fundDynamicFieldHistoryMsts);
                _dbContext.SaveChanges();
            }
            catch { throw; }
            return commonResponse;
        }
    }
}