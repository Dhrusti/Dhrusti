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
    public class WaltCapConsultantController : ControllerBase
    {
        private readonly IWaltCapConsultant _iwaltCapConsultant;

        public WaltCapConsultantController(IWaltCapConsultant iwaltCapConsultant)
        {
            _iwaltCapConsultant = iwaltCapConsultant;
        }

        [HttpPost("GetAllWaltCapConsultant")]
        public CommonResponse GetAllWaltCapConsultant()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iwaltCapConsultant.GetAllWaltCapConsultant();
                List<GetWaltCapConsultantResDTO> getWaltCapConsultantResDTO = commonResponse.Data ?? new List<GetWaltCapConsultantResDTO>();
                commonResponse.Data = getWaltCapConsultantResDTO.Adapt<List<GetWaltCapConsultantResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("GetWaltCapConsultantDetailById")]
        public CommonResponse GetWaltCapConsultantDetailById(GetWaltCapConsultantReqViewModel getWaltCapConsultantReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iwaltCapConsultant.GetWaltCapConsultantDetailById(getWaltCapConsultantReqViewModel.Adapt<GetWaltCapConsultantReqDTO>());
                var List = commonResponse.Data;
                commonResponse.Data = List;
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("AddWaltCapConsultant")]
        public CommonResponse AddWaltCapConsultant(AddWaltCapConsultantReqViewModel addWaltCapConsultantReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iwaltCapConsultant.AddWaltCapConsultant(addWaltCapConsultantReqViewModel.Adapt<AddWaltCapConsultantReqDTO>());
                AddWaltCapConsultantResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddWaltCapConsultantResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("UpdateWaltCapConsultant")]
        public CommonResponse UpdateWaltCapConsultant(UpdateWaltCapConsultantReqViewModel updateWaltCapConsultantReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iwaltCapConsultant.UpdateWaltCapConsultant(updateWaltCapConsultantReqViewModel.Adapt<UpdateWaltCapConsultantReqDTO>());
                UpdateWaltCapConsultantResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateWaltCapConsultantResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("DeleteWaltCapConsultant")]
        public CommonResponse DeleteWaltCapConsultant(DeleteWaltCapConsultantReqViewModel deleteWaltCapConsultantReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iwaltCapConsultant.DeleteWaltCapConsultant(deleteWaltCapConsultantReqViewModel.Adapt<DeleteWaltCapConsultantReqDTO>());
                DeleteWaltCapConsultantResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteWaltCapConsultantResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
