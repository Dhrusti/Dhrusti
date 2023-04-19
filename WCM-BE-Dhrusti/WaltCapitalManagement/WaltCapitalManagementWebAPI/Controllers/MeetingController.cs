using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.ViewModels.ReqViewModels;
using WaltCapitalManagementWebAPI.ViewModels.ResViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeeting _iMeeting;
        public MeetingController(IMeeting iMeeting)
        {
            _iMeeting = iMeeting;
        }

        [HttpPost("AddMeetings")]
        public CommonResponse AddMeetings(AddMeetingsReqViewModel addMeetingsReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iMeeting.AddMeetings(addMeetingsReqViewModel.Adapt<AddMeetingsReqDTO>());
                AddMeetingsResDTO addMeetingsResDTO = commonResponse.Data;
                commonResponse.Data = addMeetingsResDTO.Adapt<AddMeetingsResViewModel>();
            }
            catch(Exception)
            {
                throw;
            }
            return commonResponse;
        }

        [HttpPost("GetAllMeetings")]
        public CommonResponse GetAllMeetings(GetAllMeetingsReqViewModel getMeetingReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iMeeting.GetAllMeetings(getMeetingReqViewModel.Adapt<GetAllMeetingsReqDTO>());
                List<GetAllMeetingsResDTO> getMeetingResDTO = commonResponse.Data ?? new List<GetAllMeetingsResDTO>();
                commonResponse.Data = getMeetingResDTO.Adapt<List<GetAllMeetingsResViewModel>>();
            }
            catch(Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
