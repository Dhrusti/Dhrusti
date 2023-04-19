using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using System.Net;

namespace BusinessLayer
{
    public class CurrencyBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonHelper _commonHelper;
        private readonly CommonRepo _iCommonRepo;

        public CurrencyBLL(CommonHelper iCommonHelper, CommonRepo iCommonRepo, WaltCapitalDBContext dBContext)
        {
            _commonHelper = iCommonHelper;
            _iCommonRepo = iCommonRepo;
            _dBContext = dBContext;
        }

        public CommonResponse GetAllCurrency()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var currency = _iCommonRepo.currencyList().ToList();
                if (currency.Count > 0)
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
                commonResponse.Data = currency.Adapt<List<GetCurrencyResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddCurrency(AddCurrencyReqDTO addCurrencyReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddCurrencyResDTO addCurrencyResDTO = new AddCurrencyResDTO();
            try
            {
                var currency = _iCommonRepo.currencyList().Where(x => x.CurrencyName.ToLower() == addCurrencyReqDTO.CurrencyName.ToLower()).FirstOrDefault();
                if (currency == null)
                {
                    CurrencyMst currencyMst = new CurrencyMst();
                    currencyMst.CurrencyName = addCurrencyReqDTO.CurrencyName;
                    currencyMst.Symbol = addCurrencyReqDTO.Symbol;
                    currencyMst.BaseValue = addCurrencyReqDTO.BaseValue;
                    currencyMst.CreatedBy = addCurrencyReqDTO.UserId;
                    currencyMst.UpdatedBy = addCurrencyReqDTO.UserId;
                    currencyMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    currencyMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    currencyMst.IsActive = true;
                    currencyMst.IsDeleted = false;

                    _dBContext.CurrencyMsts.Add(currencyMst);
                    _dBContext.SaveChanges();

                    addCurrencyResDTO.Id = currencyMst.Id;
                    addCurrencyResDTO.CurrencyName = currencyMst.CurrencyName;
                    addCurrencyResDTO.Symbol = currencyMst.Symbol;
                    addCurrencyResDTO.BaseValue = currencyMst.BaseValue;

                    commonResponse.Message = "Currency added Successfully!";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addCurrencyResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Currency Already Exist!";
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
