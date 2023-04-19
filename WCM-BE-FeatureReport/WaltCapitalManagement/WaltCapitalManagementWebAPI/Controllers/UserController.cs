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
    public class UserController : ControllerBase
    {
        private readonly IUser _iUser;

        public UserController(IUser iUser)
        {
            _iUser = iUser;
        }

        [HttpPost("AddClientPhase1")]
        public CommonResponse AddClientPhase1(AddClientPhase1ReqViewModel addClientPhase1ReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUser.AddClientPhase1(addClientPhase1ReqViewModel.Adapt<AddClientPhase1ReqDTO>());
                AddClientPhase1ResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddClientPhase1ResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddClientPhase2")]
        public CommonResponse AddClientPhase2(AddClientPhase2ReqViewModel addClientPhase2ReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUser.AddClientPhase2(addClientPhase2ReqViewModel.Adapt<AddClientPhase2ReqDTO>());
                AddClientPhase2ResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddClientPhase2ResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllClient")]
        public CommonResponse GetAllClient(GetAllClientReqViewModel getClientReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUser.GetAllClient(getClientReqViewModel.Adapt<GetAllClientReqDTO>());
                GetAllClientResDTO ClientResDTO = commonResponse.Data ?? new GetAllClientResDTO();
                commonResponse.Data = ClientResDTO.Adapt<GetAllClientResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetByClientId")]
        public CommonResponse GetByClientId(GetByClientIdReqViewModel getByClientIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUser.GetByClientId(getByClientIdReqViewModel.Adapt<GetByClientIdReqDTO>());
                GetClientByIdResDTO ClientbyIdResDTO = commonResponse.Data ?? new GetClientByIdResDTO();
                commonResponse.Data = ClientbyIdResDTO.Adapt<GetClientByIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateClientPhase1")]
        public CommonResponse UpdateClientPhase1(UpdateClientPhase1ReqViewModel updateClientPhase1ReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUser.UpdateClientPhase1(updateClientPhase1ReqViewModel.Adapt<UpdateClientPhase1ReqDTO>());
                UpdateClientPhase1ResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateClientPhase1ResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UploadDocument")]
        public CommonResponse UploadDocument([FromForm] UploadDocumentReqViewModel uploadDocumentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUser.UploadDocument(uploadDocumentReqViewModel.Adapt<UploadDocumentReqDTO>());
                UploadDocumentResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UploadDocumentResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GenerateAccountNo")]
        public CommonResponse GenerateAccountNo(GenerateAccountNoReqViewModel generateAccountNoReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUser.GenerateAccountNo(generateAccountNoReqViewModel.Adapt<GenerateAccountNoReqDTO>());
                GenerateAccountNoResDTO generateAccountNoResNo = commonResponse.Data ?? new GenerateAccountNoResDTO();
                commonResponse.Data = generateAccountNoResNo.Adapt<GenerateAccountNoResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllClientList")]
        public CommonResponse GetAllClientList()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iUser.GetAllClientList();
                List<GetAllClientListResDTO> ClientResDTO = commonResponse.Data ?? new List<GetAllClientListResDTO>();
                commonResponse.Data = ClientResDTO.Adapt<List<GetAllClientListResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
