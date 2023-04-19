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
    public class ClientTransactionController : ControllerBase
    {
        private readonly IClientTransaction _iClientTransaction;

        public ClientTransactionController(IClientTransaction iClientTransaction)
        {
            _iClientTransaction = iClientTransaction;
        }

        [HttpPost("GetAllClientTransactionByFundId")]
        public CommonResponse GetAllClientTransactionByFundId(GetAllClientTransactionByFundIdReqViewModel getAllClientTransactionByFundIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                commonResponse = _iClientTransaction.GetAllClientTransactionByFundId(getAllClientTransactionByFundIdReqViewModel.Adapt<GetAllClientTransactionByFundIdReqDTO>());

                GetAllClientTransactionResDTO clientTransactionResDTO = commonResponse.Data ?? new GetAllClientTransactionResDTO();
                commonResponse.Data = clientTransactionResDTO.Adapt<GetAllClientTransactionResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddClientTransaction")]
        public CommonResponse AddClientTransaction(AddClientTransactionReqViewModel addClientTransactionReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iClientTransaction.AddClientTransaction(addClientTransactionReqViewModel.Adapt<AddClientTransactionReqDTO>());
                AddClientTransactionResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddClientTransactionResViewModel>();

            }
            catch (Exception) { throw; }
            return commonResponse;
        }


        [HttpPost("UpdateClientTransaction")]
        public CommonResponse UpdateClientTransaction(UpdateClientTransactionReqViewModel updateClientTransactionReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iClientTransaction.UpdateClientTransaction(updateClientTransactionReqViewModel.Adapt<UpdateClientTransactionReqDTO>());
                UpdateClientTransactionResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateClientTransactionResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetFundForCTByFundId")]
        public CommonResponse GetFundForCTByFundId(GetFundForCTByFundIdReqViewModel getFundForCTByFundIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iClientTransaction.GetFundForCTByFundId(getFundForCTByFundIdReqViewModel.Adapt<GetFundForCTByFundIdReqDTO>());
                GetFundForCTByFundIdResDTO getFundForCTByFundIdResDTOs = commonResponse.Data ?? new GetFundForCTByFundIdResDTO();
                commonResponse.Data = getFundForCTByFundIdResDTOs.Adapt<GetFundForCTByFundIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetAllIFAbyClientId")]
        public CommonResponse GetAllIFAByClientId(GetAllIFAByClientIdReqViewModel getAllIFAByClientIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                commonResponse = _iClientTransaction.GetAllIFAbyClientId(getAllIFAByClientIdReqViewModel.Adapt<GetAllIFAByClientIdReqDTO>());
                List<GetAllIFAByClientIdResDTO> getAllIFAByClientIdRes = commonResponse.Data ?? new List<GetAllIFAByClientIdResDTO>();
                commonResponse.Data = getAllIFAByClientIdRes.Adapt<List<GetAllIFAByClientIdResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetByClientTransactionId")]
        public CommonResponse GetByClientTransactionId(GetByClientTransactionIdReqViewModel getByClientTransactionIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iClientTransaction.GetByClientTransactionId(getByClientTransactionIdReqViewModel.Adapt<GetByClientTransactionIdReqDTO>());
                GetByClientTransactionIdResDTO model = commonResponse.Data ?? new GetByClientTransactionIdResDTO();
                commonResponse.Data = model.Adapt<GetByClientTransactionIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("DeleteClientTransaction")]
        public CommonResponse DeleteClientTransaction(DeleteClientTransactionReqViewModel deleteClientTransactionReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iClientTransaction.DeleteClientTransaction(deleteClientTransactionReqViewModel.Adapt<DeleteClientTransactionReqDTO>());
                DeleteClientTransactionResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<DeleteClientTransactionResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetTranscationTypeByClientId")]
        public CommonResponse GetTranscationTypeByClientId(GetTranscationTypeByClientIdReqViewModel getTranscationTypeByClientIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iClientTransaction.GetTranscationTypeByClientId(getTranscationTypeByClientIdReqViewModel.Adapt<GetTranscationTypeByClientIdReqDTO>());
                GetTranscationTypeByClientIdResDTO model = commonResponse.Data ?? new GetTranscationTypeByClientIdResDTO();

                commonResponse.Data = model.Adapt<GetTranscationTypeByClientIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("GetTransactionUnitTypeByClientId")]
        public CommonResponse GetTransactionUnitTypeByClientId(GetTransactionUnitTypeByClientIdReqViewModel getTransactionUnitTypeByClientIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iClientTransaction.GetTranscationUnitTypeByClientId(getTransactionUnitTypeByClientIdReqViewModel.Adapt<GetTransactionUnitTypeByClientIdReqDTO>());
                GetTransactionUnitTypeByClientIdResDTO model = commonResponse.Data ?? new GetTransactionUnitTypeByClientIdResDTO();

                commonResponse.Data = model.Adapt<GetTransactionUnitTypeByClientIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
