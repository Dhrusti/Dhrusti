using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer
{
    public class MeetingBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _configuration;

        public MeetingBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configuration = configuration;
        }

        public CommonResponse AddMeetings(AddMeetingsReqDTO addMeetingsReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddMeetingsResDTO addMeetingsResDTO = new AddMeetingsResDTO();
            try
            {
                var meeting = _commonRepo.meetingList().Where(x => x.ReminderTime == addMeetingsReqDTO.ReminderTime).FirstOrDefault();
                if (meeting == null)
                {
                    MeetingMst meetingMst = new MeetingMst();
                    meetingMst.ReminderDate = addMeetingsReqDTO.ReminderDate;
                    meetingMst.ReminderTime = addMeetingsReqDTO.ReminderTime;
                    meetingMst.Venue = addMeetingsReqDTO.Venue;
                    meetingMst.Attendees = addMeetingsReqDTO.Attendees;
                    meetingMst.ClientAction = addMeetingsReqDTO.ClientAction;
                    meetingMst.WaltCapitalActions = addMeetingsReqDTO.WaltCapitalActions;
                    meetingMst.Discussion = addMeetingsReqDTO.Discussion;
                    meetingMst.CreatedDate = DateTime.Now;
                    meetingMst.UpdatedDate = DateTime.Now;
                    meetingMst.IsActive = true;
                    meetingMst.IsDeleted = false;

                    _dBContext.MeetingMsts.Add(meetingMst);
                    _dBContext.SaveChanges();

                    addMeetingsResDTO.Id = meetingMst.Id;
                    addMeetingsResDTO.ReminderDate = meetingMst.ReminderDate;
                    addMeetingsResDTO.ReminderTime = meetingMst.ReminderTime;
                    addMeetingsResDTO.Venue = meetingMst.Venue;

                    commonResponse.Message = "Task/Meeting scheduled Successfully!";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addMeetingsResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Another Meeting is already scheduled at this time!";
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        public CommonResponse GetAllMeetings(GetAllMeetingsReqDTO getMeetingReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            List<GetAllMeetingsResDTO> getMeetingsList = new List<GetAllMeetingsResDTO>();
            try
            {

                int Page = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:Page").Value);
                int PageSize = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:PageSize").Value);
                bool OrderBy = Convert.ToBoolean(_configuration.GetSection("ByDefaultPagination:OrderBy").Value);

                Page = getMeetingReqDTO.PageNumber == 0 ? Page : getMeetingReqDTO.PageNumber;
                PageSize = getMeetingReqDTO.PageSize == 0 ? PageSize : getMeetingReqDTO.PageSize;
                if (getMeetingReqDTO.Orderby != null)
                    OrderBy = getMeetingReqDTO.Orderby;



                if (getMeetingReqDTO != null)
                {
                    DateTime dtNow = _commonHelper.GetCurrentDateTime();

                    int dayOfWeek = (int)dtNow.DayOfWeek;
                    DateTime nextSunday = dtNow.AddDays(7 - dayOfWeek).Date;

                    List<MeetingMst> meetings = new List<MeetingMst>();
                    if (getMeetingReqDTO.MeetingBy == 0)  // 0 = Today's Meeting List
                    {
                        meetings = _commonRepo.meetingList().Where(x => x.ReminderTime.Day == dtNow.Day && x.ReminderTime.Month == dtNow.Month && x.ReminderTime.Year == dtNow.Year).ToList();
                    }
                    else if (getMeetingReqDTO.MeetingBy == 1) // 1 = All Meetings
                    {
                        meetings = _commonRepo.meetingList().ToList();
                    }



                    

                    foreach (var item in meetings)
                    {
                        getMeetingsList.Add(new GetAllMeetingsResDTO
                        {
                            ReminderDate = item.ReminderDate,
                            ReminderTime = item.ReminderTime,
                            Venue = item.Venue,
                            Attendees = item.Attendees,
                            ClientAction = item.ClientAction,
                            WaltCapitalActions = item.WaltCapitalActions,
                            Discussion = item.Discussion
                        });
                    }
                    commonResponse.Status = true;
                    commonResponse.Data = getMeetingsList;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Data Found.";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                }
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
