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
    public class PersonalityTypeController : ControllerBase
    {
        private readonly IPersonalityType _personalityType;

        public PersonalityTypeController(IPersonalityType personalityType)
        {
            _personalityType = personalityType;
        }

        [HttpPost("GetAllPersonalityType")]
        public CommonResponse GetAllPersonalityType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _personalityType.GetAllPersonalityType();
                List<GetPersonalityTypeResDTO> Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<List<GetPersonalityTypeResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("GetPersonalityTypeById")]
        public CommonResponse GetPersonalityTypeById(GetByIdPersonalityTypeReqViewModel getByIdPersonalityTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _personalityType.GetPersonalityTypeById(getByIdPersonalityTypeReqViewModel.Adapt<GetByIdPersonalityTypeReqDTO>());
                GetPersonalityTypeResDTO Model = commonResponse.Data ?? new GetPersonalityTypeResDTO();
                commonResponse.Data = Model.Adapt<GetPersonalityTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("AddPersonalityType")]
        public CommonResponse AddPersonalityType(AddPersonalityTypeReqViewModel addPersonalityTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _personalityType.AddPersonalityType(addPersonalityTypeReqViewModel.Adapt<AddPersonalityTypeReqDTO>());
                AddPersonalityTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddPersonalityTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("UpdatePeronalityType")]
        public CommonResponse UpdatePersonalitytype(UpdatePersonalityTypeReqViewModel updatePersonalityTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _personalityType.UpdatePersonalityType(updatePersonalityTypeReqViewModel.Adapt<UpdatePersonalityTypeReqDTO>());
                UpdatePersonalityTypeResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdatePersonalityTypeResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [NonAction]
        [HttpPost("DeletePersonalityType")]
        public CommonResponse DeletePersonalityType(DeletePersonalityTypeReqViewModel deletePersonalityTypeReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _personalityType.DeletePersonalityType(deletePersonalityTypeReqViewModel.Adapt<DeletePersonalityTypeReqDTO>());

            }
            catch (Exception) { throw; }
            return commonResponse;
        }


    }
}
