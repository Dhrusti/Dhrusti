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
    public class RegionController : ControllerBase
    {
        private readonly IRegion _iregion;
        public RegionController(IRegion iregion)
        {
            _iregion = iregion;
        }

        [HttpPost("GetAllCountry")]
        public CommonResponse GetAllCountry()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.GetAllCountry();
                List<GetCountryResDTO> countryResDTO = commonResponse.Data ?? new List<GetCountryResDTO>();
                commonResponse.Data = countryResDTO.Adapt<List<GetCountryResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllCountryCustom")]
        public CommonResponse GetAllCountryCustom()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.GetAllCountryCustom();
                List<GetAllCountryCustomResDTO> getAllCountryCustomResDTO = commonResponse.Data ?? new List<GetAllCountryCustomResDTO>();
                commonResponse.Data = getAllCountryCustomResDTO.Adapt<List<GetAllCountryCustomResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetCountryByCountryId")]
        public CommonResponse GetCountryByCountryId(GetCountryByCountryIdReqViewModel getByCountryIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.GetCountryByCountryId(getByCountryIdReqViewModel.Adapt<GetCountryByCountryIdReqDTO>());
                List<GetCountryByCountryIdResDTO> Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<List<GetCountryByCountryIdResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddCountryCustom")]
        public CommonResponse AddCountryCustom(AddCountryCustomReqViewModel addCountryCustomReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.AddCountryCustom(addCountryCustomReqViewModel.Adapt<AddCountryCustomReqDTO>());
                AddCountryCustomResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddCountryCustomResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateCountryCustom")]
        public CommonResponse UpdateCountryCustom(UpdateCountryCustomReqViewModel updateCountryCustomReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.UpdateCountryCustom(updateCountryCustomReqViewModel.Adapt<UpdateCountryCustomReqDTO>());
                UpdateCountryCustomResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateCountryCustomResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteCountryCustom")]
        public CommonResponse DeleteCountryCustom(DeleteCountryCustomReqViewModel deleteCountryCustomReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.DeleteCountryCustom(deleteCountryCustomReqViewModel.Adapt<DeleteCountryCustomReqDTO>());
                DeleteCountryCustomResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteCountryCustomResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllStateByCountryID")]
        public CommonResponse GetAllStateByCountryID(GetStateReqViewModel getStateReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.GetAllStateByCountryID(getStateReqViewModel.Adapt<GetStateReqDTO>());
                List<GetStateResDTO> Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<List<GetStateResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllStateCustomByCountryId")]
        public CommonResponse GetAllStateCustomByCountryId(GetAllStateCustomReqViewModel getAllStateCustomReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.GetAllStateCustomByCountryId(getAllStateCustomReqViewModel.Adapt<GetAllStateCustomReqDTO>());
                GetAllStateCustomResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetAllStateCustomResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetStateByStateId")]
        public CommonResponse GetStateByStateId(GetStateByStateIdReqViewModel getByStateIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.GetStateByStateId(getByStateIdReqViewModel.Adapt<GetStateByStateIdReqDTO>());
                List<GetStateByStateIdResDTO> Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<List<GetStateByStateIdResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddStateCustom")]
        public CommonResponse AddStateCustom(AddStateCustomReqViewModel addStateCustomReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.AddStateCustom(addStateCustomReqViewModel.Adapt<AddStateCustomReqDTO>());
                AddStateCustomResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddStateCustomResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateStateCustom")]
        public CommonResponse UpdateStateCustom(UpdateStateCustomReqViewModel updateStateCustomReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.UpdateStateCustom(updateStateCustomReqViewModel.Adapt<UpdateStateCustomReqDTO>());
                UpdateStateCustomResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateStateCustomResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteStateCustom")]
        public CommonResponse DeleteStateCustom(DeleteStateCustomReqViewModel deleteStateCustomReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.DeleteStateCustom(deleteStateCustomReqViewModel.Adapt<DeleteStateCustomReqDTO>());
                DeleteStateCustomResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteStateCustomResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllCityByStateID")]
        public CommonResponse GetAllCityByStateID(GetCityReqViewModel getCityReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.GetAllCityByStateID(getCityReqViewModel.Adapt<GetCityReqDTO>());
                List<GetCityResDTO> Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<List<GetCityResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllCityCustomByStateId")]
        public CommonResponse GetAllCityCustomByStateId(GetAllCityCustomReqViewModel getAllCityCustomReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.GetAllCityCustomByStateId(getAllCityCustomReqViewModel.Adapt<GetAllCityCustomReqDTO>());
                GetAllCityCustomResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GetAllCityCustomResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetCityByCityId")]
        public CommonResponse GetCityByCityId(GetCityByCityIdReqViewModel getByCityIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.GetCityByCityId(getByCityIdReqViewModel.Adapt<GetCityByCityIdReqDTO>());
                List<GetCityByCityIdResDTO> Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<List<GetCityByCityIdResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddCityCustom")]
        public CommonResponse AddCityCustom(AddCustomCityReqViewModel addCustomCityReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.AddCustomCity(addCustomCityReqViewModel.Adapt<AddCustomCityReqDTO>());
                AddCustomCityResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddCustomCityResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateCityCustom")]
        public CommonResponse UpdateCityCustom(UpdateCustomCityReqViewModel updateCustomCityReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.UpdateCustomCity(updateCustomCityReqViewModel.Adapt<UpdateCustomCityReqDTO>());
                UpdateCustomCityResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateCustomCityResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteCityCustom")]
        public CommonResponse DeleteCityCustom(DeleteCustomCityReqViewModel deleteCustomCityReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iregion.DeleteCustomCity(deleteCustomCityReqViewModel.Adapt<DeleteCustomCityReqDTO>());
                DeleteCustomCityResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteCustomCityResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
