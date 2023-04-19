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
    public class OfficeController : ControllerBase
    {

        private readonly IOffice _iOffice;
        public OfficeController(IOffice iOffice)
        {
            _iOffice = iOffice;
        }

        [HttpPost("GetAllOffice")]
        public CommonResponse GetAllOffice()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iOffice.GetAllOffice();
                List<GetOfficeResDTO> officeResDTO = commonResponse.Data ?? new List<GetOfficeResDTO>();
                commonResponse.Data = officeResDTO.Adapt<List<GetOfficeResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetOfficeByCityId")]
        public CommonResponse GetOfficeByCityId(GetOfficeByCityIdReqViewModel getOfficeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iOffice.GetOfficeByCityId(getOfficeReqViewModel.Adapt<GetOfficeByCityIdReqDTO>());
                GetOfficeByCityIdResDTO officeResDTO = commonResponse.Data ?? new GetOfficeByCityIdResDTO();
                commonResponse.Data = officeResDTO.Adapt<GetOfficeByCityIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetOfficeDetailById")]
        public CommonResponse GetOfficeDetailById(GetOfficeReqViewModel getOfficeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iOffice.GetOfficeDetailById(getOfficeReqViewModel.Adapt<GetOfficeReqDTO>());
                GetOfficeResDTO officeResDTO = commonResponse.Data ?? new GetOfficeResDTO();
                commonResponse.Data = officeResDTO.Adapt<GetOfficeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddOffice")]
        public CommonResponse AddOffice(AddOfficeReqViewModel addOfficeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iOffice.AddOffice(addOfficeReqViewModel.Adapt<AddOfficeReqDTO>());
                AddOfficeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddOfficeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateOffice")]
        public CommonResponse UpdateOffice(UpdateOfficeReqViewModel updateOfficeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iOffice.UpdateOffice(updateOfficeReqViewModel.Adapt<UpdateOfficeReqDTO>());
                UpdateOfficeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateOfficeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteOffice")]
        public CommonResponse DeleteOffice(DeleteOfficeReqViewModel deleteOfficeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iOffice.DeleteOffice(deleteOfficeReqViewModel.Adapt<DeleteOfficeReqDTO>());
                DeleteOfficeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteOfficeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
