using DataLayer.Entities;
using Helper;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class FundDynamicInputPriceBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly FundDynamicFieldBLL _fundDynamicFieldBLL;

        public FundDynamicInputPriceBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper, FundDynamicFieldBLL fundDynamicFieldBLL)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _fundDynamicFieldBLL = fundDynamicFieldBLL;
        }

        public CommonResponse AddFundDynamicInputPriceWithBlankData(int FundId, string InputPricing, int CreatedBy, bool IsAddedFromPricing)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var FundDetail = _commonRepo.fundList().FirstOrDefault(x => x.Id == FundId);
                if (FundDetail != null && !string.IsNullOrWhiteSpace(InputPricing))
                {
                    List<string> PricingInputs = FundDetail != null && !string.IsNullOrWhiteSpace(FundDetail.PricingInputs) ? FundDetail.PricingInputs.Trim().Split(',').ToList() : new List<string>();
                    var unitTypeList = _fundDynamicFieldBLL.GetUnitTypeList(FundDetail.Id);
                    List<FundDynamicInputPriceMst> FundDynamicInputPriceMstList = new List<FundDynamicInputPriceMst>();
                    foreach (var input in PricingInputs)
                    {
                        if (!string.IsNullOrWhiteSpace(input))
                        {

                            foreach (var item in unitTypeList)
                            {
                                FundDynamicInputPriceMst fundDynamicInputPriceMst = new FundDynamicInputPriceMst();
                                fundDynamicInputPriceMst.FundId = FundId;
                                fundDynamicInputPriceMst.Label = input;
                                fundDynamicInputPriceMst.Value = 0;
                                fundDynamicInputPriceMst.UnitType = item;
                                fundDynamicInputPriceMst.BalanceDate = FundDetail.InceptionDate;
                                fundDynamicInputPriceMst.IsActive = true;
                                fundDynamicInputPriceMst.IsDeleted = false;
                                fundDynamicInputPriceMst.IsAddedFromPricing = IsAddedFromPricing;
                                fundDynamicInputPriceMst.CreatedBy = CreatedBy;
                                fundDynamicInputPriceMst.UpdatedBy = CreatedBy;
                                fundDynamicInputPriceMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                fundDynamicInputPriceMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                                FundDynamicInputPriceMstList.Add(fundDynamicInputPriceMst);
                            }
                        }
                    }

                    _dbContext.FundDynamicInputPriceMsts.AddRange(FundDynamicInputPriceMstList);
                    _dbContext.SaveChanges();

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Fund Dynamic Input Pricing Added Sucessfully.";
                    commonResponse.Data = FundDynamicInputPriceMstList;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Fund Id Or InputPricing Not Found.";
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        public CommonResponse AddUpdateFundDynamicInputPrice(int FundId, string UnitType, string Label, decimal Value, DateTime BalanceDate, int UpdatedBy, bool IsAddedFromPricing)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                //var FundInputPricingList = _commonRepo.GetFundDynamicInputPriceList(FundId).Where(x => x.Label.ToLower() == Label.ToLower()).ToList();
                var FundInputPricingDetail = _commonRepo.GetFundDynamicInputPriceList(FundId).FirstOrDefault(x => x.BalanceDate.Date == BalanceDate.Date && x.Label.ToLower() == Label.ToLower() && x.UnitType.ToLower() == UnitType.ToLower());
                bool IsEditMode = FundInputPricingDetail != null ? true : false;
                IsEditMode = IsAddedFromPricing ? false : IsEditMode;

                FundDynamicInputPriceMst fundDynamicInputPriceMst = new FundDynamicInputPriceMst();
                if (IsEditMode)
                {
                    //Edit Mode
                    fundDynamicInputPriceMst = FundInputPricingDetail;
                    //fundDynamicInputPriceMst.Value = fundDynamicInputPriceMst.Value + Value;
                    fundDynamicInputPriceMst.Value = Value;
                    fundDynamicInputPriceMst.UnitType = UnitType;
                    fundDynamicInputPriceMst.BalanceDate = BalanceDate;
                    fundDynamicInputPriceMst.UpdatedBy = UpdatedBy;
                    fundDynamicInputPriceMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    fundDynamicInputPriceMst.IsAddedFromPricing = IsAddedFromPricing;

                    _dbContext.Entry(fundDynamicInputPriceMst).State = EntityState.Modified;
                    commonResponse.Message = "Fund Dynamic Input Pricing Updated Sucessfully.";
                }
                else
                {
                    //Add Mode
                    fundDynamicInputPriceMst.FundId = FundId;
                    fundDynamicInputPriceMst.UnitType = UnitType;
                    fundDynamicInputPriceMst.Label = Label;
                    fundDynamicInputPriceMst.Value = Value;
                    fundDynamicInputPriceMst.BalanceDate = BalanceDate;
                    fundDynamicInputPriceMst.IsAddedFromPricing = IsAddedFromPricing;
                    fundDynamicInputPriceMst.IsActive = true;
                    fundDynamicInputPriceMst.IsDeleted = false;
                    fundDynamicInputPriceMst.CreatedBy = UpdatedBy;
                    fundDynamicInputPriceMst.UpdatedBy = UpdatedBy;
                    fundDynamicInputPriceMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    fundDynamicInputPriceMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.FundDynamicInputPriceMsts.Add(fundDynamicInputPriceMst);
                    commonResponse.Message = "Fund Dynamic Input Pricing Added Sucessfully.";
                }
                _dbContext.SaveChanges();
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Data = fundDynamicInputPriceMst;
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
