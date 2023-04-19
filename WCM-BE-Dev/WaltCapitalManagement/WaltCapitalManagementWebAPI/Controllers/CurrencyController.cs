using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.ViewModels.ReqViewModels;
using WaltCapitalManagementWebAPI.ViewModels.ResViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrency _iCurrency;

        public CurrencyController(ICurrency iCurrency)
        {
            _iCurrency = iCurrency;

        }


        [HttpPost("GetAllCurrency")]
        public CommonResponse GetAllCurrency()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iCurrency.GetAllCurrency();
                List<GetCurrencyResDTO> getCurrencyResDTO = commonResponse.Data ?? new List<GetCurrencyResDTO>();
                commonResponse.Data = getCurrencyResDTO.Adapt<List<GetCurrencyResViewModel>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        [HttpPost("AddCurrency")]
        public CommonResponse AddCurrency(AddCurrencyReqViewModel addCurrencyReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iCurrency.AddCurrency(addCurrencyReqViewModel.Adapt<AddCurrencyReqDTO>());
                AddCurrencyResDTO addCurrencyResDTO = commonResponse.Data;
                commonResponse.Data = addCurrencyResDTO.Adapt<AddCurrencyResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

    }
}
