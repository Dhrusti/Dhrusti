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
    public class IFAController : ControllerBase
    {
        private readonly IIFA _iIFA;
        public IFAController(IIFA iIFA)
        {
            _iIFA = iIFA;
        }

        [HttpPost("GetAllIFA")]
        public CommonResponse GetAllIFA()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iIFA.GetAllIFA();
                List<GetAllIFAResDTO> getIFAResDTO = commonResponse.Data ?? new List<GetAllIFAResDTO>();
                commonResponse.Data = getIFAResDTO.Adapt<List<GetAllIFAResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetIFAById")]
        public CommonResponse GetIFAById(GetIFAReqViewModel getIFAReqView)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iIFA.GetIFAById(getIFAReqView.Adapt<GetIFAReqDTO>());
                GetIFAResDTO getIFAResDTO = commonResponse.Data ?? new GetIFAResDTO();
                commonResponse.Data = getIFAResDTO.Adapt<GetIFAResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddIFAPhase1")]
        public CommonResponse AddIFAPhase1(AddIFAReqViewModel addIFAReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iIFA.AddIFAPhase1(addIFAReqViewModel.Adapt<AddIFAReqDTO>());
                AddIFAResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddIFAResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddIFAPhase2")]
        public CommonResponse AddIFAPhase2(AddIFAPhase2ReqViewModel addIFAPhase2ReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iIFA.AddIFAPhase2(addIFAPhase2ReqViewModel.Adapt<AddIFAPhase2ReqDTO>());
                AddIFAPhase2ResDTO addIFAPhase2 = commonResponse.Data;
                commonResponse.Data = addIFAPhase2.Adapt<AddIFAPhase2ResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateIFA")]
        public CommonResponse UpdateIFA(UpdateIFAReqViewModel updateIFAReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iIFA.UpdateIFA(updateIFAReqViewModel.Adapt<UpdateIFAReqDTO>());
                UpdateIFAResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateIFAResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("DeleteIFA")]
        public CommonResponse DeleteIFA(DeleteIFAReqViewModel deleteIFAReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iIFA.DeleteIFA(deleteIFAReqViewModel.Adapt<DeleteIFAReqDTO>());
                DeleteIFAResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteIFAResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllIFAClient")]
        public CommonResponse GetAllIFAClient(GetIFAClientReqViewModel getIFAClientReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iIFA.GetAllIFAClient(getIFAClientReqViewModel.Adapt<GetIFAClientReqDTO>());
                GetIFAClientResDTO getIFAClientResDTO = commonResponse.Data ?? new GetIFAClientResDTO();
                commonResponse.Data = getIFAClientResDTO.Adapt<GetIFAClientResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllIFAList")]
        public CommonResponse GetAllIFAList(GetIFAAssetReqViewModel getIFAAssetReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iIFA.GetAllIFAList(getIFAAssetReqViewModel.Adapt<GetIFAAssetReqDTO>());
                GetIFAAssetResDTO getIFAAssetResDTO = commonResponse.Data ?? new GetIFAAssetResDTO();
                commonResponse.Data = getIFAAssetResDTO.Adapt<GetIFAAssetResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GenerateIFAAccountNo")]
        public CommonResponse GenerateIFAAccountNo()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iIFA.GenerateIFAAccountNo();
                GenerateIFAAccountNoResDTO generateIFAAccountNoResDTO = commonResponse.Data ?? new GenerateIFAAccountNoResDTO();
                commonResponse.Data = generateIFAAccountNoResDTO.Adapt<GenerateIFAAccountNoResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

    }
}
