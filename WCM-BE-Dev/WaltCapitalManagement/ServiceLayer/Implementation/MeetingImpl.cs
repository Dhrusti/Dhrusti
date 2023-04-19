using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class MeetingImpl : IMeeting
    {
        private readonly MeetingBLL _meetingBLL;
        public MeetingImpl(MeetingBLL meetingBLL)
        {
            _meetingBLL = meetingBLL;
        }

        public CommonResponse AddMeetings(AddMeetingsReqDTO addMeetingsReqDTO)
        {
            return _meetingBLL.AddMeetings(addMeetingsReqDTO);
        }

        public CommonResponse GetAllMeetings(GetAllMeetingsReqDTO getMeetingReqDTO)
        {
            return _meetingBLL.GetAllMeetings(getMeetingReqDTO);
        }
    }
}
