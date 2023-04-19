using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IMeeting
    {
        public CommonResponse AddMeetings(AddMeetingsReqDTO addMeetingsReqDTO);
        public CommonResponse GetAllMeetings(GetAllMeetingsReqDTO getMeetingReqDTO);
    }
}
