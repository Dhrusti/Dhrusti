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
    public class ClientTypeController : ControllerBase
    {
        private readonly IClientType _iclientType;

        public ClientTypeController(IClientType clientType)
        {
            this._iclientType = clientType;
        }
        [HttpPost("GetAllClientType")]
        public CommonResponse GetAllClientType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iclientType.GetAllClientType();
                List<GetAllClientTypeResDTO> clientTypeResDTO = commonResponse.Data ?? new List<GetAllClientTypeResDTO>();
                commonResponse.Data = clientTypeResDTO.Adapt<List<GetAllClientTypeResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }


        [HttpPost("GetClientTypeById")]
        public CommonResponse GetByClientTypeId(GetClientTypeByIdReqViewModel getClientTypeByIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iclientType.GetByClientTypeId(getClientTypeByIdReqViewModel.Adapt<GetClientTypeByIdReqDTO>());
                GetClientTypeByIdResDTO ClientTypebyIdResDTO = commonResponse.Data ?? new GetClientTypeByIdResDTO();
                commonResponse.Data = ClientTypebyIdResDTO.Adapt<GetClientTypeByIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
        [HttpPost("AddClientType")]
        public CommonResponse AddClientType(AddClientTypeReqViewModel addClientTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iclientType.AddClientType(addClientTypeReqViewModel.Adapt<AddClientTypeReqDTO>());
                AddClientTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddClientTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateClientType")]
        public CommonResponse UpdateClientType(UpdateClientTypeReqViewModel updateClientTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iclientType.UpdateClientType(updateClientTypeReqViewModel.Adapt<UpdateClientTypeReqDTO>());
                UpdateClientTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateClientTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteClientType")]
        public CommonResponse DeleteClientType(DeleteClientTypeReqViewModel deleteClientTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iclientType.DeleteClientType(deleteClientTypeReqViewModel.Adapt<DeleteClientTypeReqDTO>());
                DeleteClientTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteClientTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
