using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace BusinessLayer
{
    public class BirthdayReportBLL
    {
        private readonly WaltCapitalDBContext _dbContextt;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _hostingEnvironment { get; }


        public BirthdayReportBLL(WaltCapitalDBContext dbContextt, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _dbContextt = dbContextt;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public CommonResponse GetBirthdayReport()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                #region
                GetBirthdayReportResDTO getBirthdayReportRes = new GetBirthdayReportResDTO();

                int dayOfWeek = (int)_commonHelper.GetCurrentDateTime().DayOfWeek;
                DateTime nextSunday = _commonHelper.GetCurrentDateTime().AddDays(7 - dayOfWeek).Date;

                List<TodaysBirthday> todaysBirthdays = _commonRepo.getUserList().Where(x => x.Dob.Day == _commonHelper.GetCurrentDateTime().Day && x.Dob.Month == _commonHelper.GetCurrentDateTime().Month).ToList().Select(x => new TodaysBirthday
                {
                    Name = x.FirstName + " " + x.LastName + " Turns " + _commonHelper.TodaysBirthDayList(x.Dob) + " Today",
                    Birthday = _commonHelper.TodaysConvertDate(x.Dob.ToString()),
                }).ToList();

                #region SP
                /*SqlParameter[] sqlParameters = new SqlParameter[]{
                    new SqlParameter("@StartDate", _commonHelper.GetCurrentDateTime().AddDays(1)),
                    new SqlParameter("@EndDate", _commonHelper.GetCurrentDateTime().AddDays(7-dayOfWeek))
                };
                var getUserData = _commonHelper.ExecuteCRUDStoreProcedure("[Get_DateInformation]", sqlParameters, false);
                List<GetDateInformationResDTO> model = new List<GetDateInformationResDTO>();
                var jsondata = JsonConvert.SerializeObject(getUserData.Data);
                model = JsonConvert.DeserializeObject<List<GetDateInformationResDTO>>(jsondata);*/
                #endregion sp

                DateTime startDate = _commonHelper.GetCurrentDateTime().AddDays(1);
                DateTime endDate = _commonHelper.GetCurrentDateTime().AddDays(7 - dayOfWeek);
                /*var a = _commonRepo.getUserList().Where(x => (x.Dob.Month == startDate.Month && x.Dob.Day >= startDate.Day) 
                ? (x.Dob.Day <= endDate.Day) || (x.Dob.Month != endDate.Month) 
                : (x.Dob.Month == endDate.Month && x.Dob.Day <= endDate.Day)).ToList();*/

                var a = _commonRepo.getUserList().Where(x => x.Dob.Month == startDate.Month && x.Dob.Day >= startDate.Day).ToList();
                a = a.Where(x=>x.Dob.Month == endDate.Month && x.Dob.Day <= endDate.Day).ToList();

                List<UpcommingBirthday> upcommingBirthdays = new List<UpcommingBirthday>();
                foreach (var item in a)
                {
                    var upcommingUserList = new UpcommingBirthday
                    {
                        Name = (item.FirstName + " " + item.LastName) + " Turns " + _commonHelper.TodaysBirthDayList(item.Dob) + " Today",
                        Birthday = _commonHelper.TodaysConvertDate(item.Dob.ToString())
                    };
                    if (upcommingUserList != null)
                        upcommingBirthdays.Add(upcommingUserList);
                }

                List<TaskMeeting> taskMeetings = new List<TaskMeeting>();
                //No Static Data
                //List<TaskMeeting> taskMeetings = _commonRepo.getUserList().Select(x => new TaskMeeting
                //{
                //    MeetingDate = _commonHelper.GetCurrentDateTime(),
                //    MeetingTime = _commonHelper.GetCurrentDateTime().ToString("hh:mm tt"),
                //    MeetingType = "Zoom Meeting",
                //    MeetingWith = "Trade Station"
                //}).ToList();

                List<DueDiligence> dueDiligences = new List<DueDiligence>();
                var DueDiligenceDetail = _commonRepo.dueDiligenceList().OrderByDescending(x => x.UserId).ToList();
                var dueDiligencesdata = DueDiligenceDetail.GroupBy(x => x.UserId).ToList();
                if (dueDiligencesdata != null)
                {
                    foreach (var group in dueDiligencesdata)
                    {
                        var user = group.FirstOrDefault();
                        if (user.ValidTill.Date == _commonHelper.GetCurrentDateTime().Date)
                        {
                            var DueDiligenceUserList = _commonRepo.getUserList().Where(x => x.Id == user.UserId).Select(x => new DueDiligence
                            {
                                Name = x.FirstName + " " + x.LastName
                            }).FirstOrDefault();
                            if (DueDiligenceUserList != null)
                                dueDiligences.Add(DueDiligenceUserList);
                        }
                    }
                }

                List<OverDueDiligence> overDueDiligences = new List<OverDueDiligence>();
                var overDueDiligencesdata = DueDiligenceDetail.Where(x => x.ValidTill <= _commonHelper.GetCurrentDateTime().Date).GroupBy(x => x.UserId).ToList();
                if (overDueDiligencesdata != null)
                {
                    foreach (var group in overDueDiligencesdata)
                    {
                        var user = group.FirstOrDefault();
                        var overDueDiligenceUserList = _commonRepo.getUserList().Where(x => x.Id == user.UserId).Select(x => new OverDueDiligence
                        {
                            Name = x.FirstName + " " + x.LastName
                        }).FirstOrDefault();
                        if (overDueDiligenceUserList != null)
                            overDueDiligences.Add(overDueDiligenceUserList);
                    }
                }

                List<AML> amlList = new List<AML>();
                var AmlmstsDetail = _commonRepo.amlList().Where(x => x.ValidTill <= _commonHelper.GetCurrentDateTime().Date).OrderByDescending(x => x.Id).ToList();
                var amlListData = AmlmstsDetail.GroupBy(x => x.UserId).ToList();
                if (amlListData != null)
                {
                    foreach (var group in amlListData)
                    {
                        var user = group.FirstOrDefault();
                        var AmlUserList = _commonRepo.getUserList().Where(x => x.Id == user.UserId).Select(x => new AML
                        {
                            Name = x.FirstName + " " + x.LastName
                        }).FirstOrDefault();
                        if (AmlUserList != null)
                            amlList.Add(AmlUserList);

                    }
                }

                getBirthdayReportRes.TodaysBirthdayList = todaysBirthdays;
                getBirthdayReportRes.UpcommingBirthdayList = upcommingBirthdays;
                getBirthdayReportRes.TaskMeetingList = taskMeetings;
                getBirthdayReportRes.dueDiligencesLists = dueDiligences;
                getBirthdayReportRes.overDueDiligencesLists = overDueDiligences;
                getBirthdayReportRes.aMLLists = amlList;
                #endregion

                if (getBirthdayReportRes != null)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getBirthdayReportRes.Adapt<GetBirthdayReportResDTO>();
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetBirthdayReportList(GetBirthdayReportListReqDTO getBirthdayReportListReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            GetBirthdayReportListResDTO getBirthdayReportListResDTO = new GetBirthdayReportListResDTO();
            try
            {
                int Page = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:Page").Value);
                int PageSize = Convert.ToInt32(_configuration.GetSection("ByDefaultPagination:PageSize").Value);
                bool OrderBy = Convert.ToBoolean(_configuration.GetSection("ByDefaultPagination:OrderBy").Value);

                Page = getBirthdayReportListReqDTO.PageNumber == 0 ? Page : getBirthdayReportListReqDTO.PageNumber;
                PageSize = getBirthdayReportListReqDTO.PageSize == 0 ? PageSize : getBirthdayReportListReqDTO.PageSize;
                if (getBirthdayReportListReqDTO.Orderby != null)
                    OrderBy = getBirthdayReportListReqDTO.Orderby;


                if (getBirthdayReportListReqDTO != null)
                {
                    DateTime dtNow = _commonHelper.GetCurrentDateTime();

                    int dayOfWeek = (int)dtNow.DayOfWeek;
                    DateTime nextSunday = dtNow.AddDays(7 - dayOfWeek).Date;

                    List<UserMst> users = new List<UserMst>();
                    if (getBirthdayReportListReqDTO.BirthdayBy == 0)  // 0 = Today's Birthday
                    {
                        users = _commonRepo.getUserList().Where(x => x.Dob.Day == dtNow.Day && x.Dob.Month == dtNow.Month).ToList();
                    }
                    else if (getBirthdayReportListReqDTO.BirthdayBy == 1) // 1 = Upcoming Birthday
                    {

                        DateTime startDate = dtNow.AddDays(1);
                        DateTime endDate = dtNow.AddDays(7 - dayOfWeek);
                        users = _commonRepo.getUserList().Where(x => (x.Dob.Month == startDate.Month && x.Dob.Day >= startDate.Day) ? (x.Dob.Day <= endDate.Day) || (x.Dob.Month != endDate.Month) : (x.Dob.Month == endDate.Month && x.Dob.Day >= endDate.Day)).ToList();
                    }

                    //if (getBirthdayReportListReqDTO.Alphabet != null && !string.IsNullOrEmpty(getBirthdayReportListReqDTO.Alphabet))
                    //{
                    //    users = users.Where(x => x.LastName.ToLower().StartsWith(getBirthdayReportListReqDTO.Alphabet.ToLower())).ToList();
                    //    getBirthdayReportListResDTO.TotalCount = users.Count();
                    //}
                    if (getBirthdayReportListReqDTO.SearchString != null && !string.IsNullOrEmpty(getBirthdayReportListReqDTO.SearchString))
                    {
                        users = users.Where(x => x.FirstName.ToLower().Contains(getBirthdayReportListReqDTO.SearchString.ToLower()) || x.LastName.ToLower().Contains(getBirthdayReportListReqDTO.SearchString.ToLower()) || x.AccountNo.ToLower().Contains(getBirthdayReportListReqDTO.SearchString.ToLower())).ToList();
                        getBirthdayReportListResDTO.TotalCount = users.Count();
                    }

                    getBirthdayReportListResDTO.TotalCount = users.Count();

                    if (OrderBy)
                    {
                        if (users.Count <= PageSize)
                        {
                            users = users.OrderBy(x => x.CreatedDate).ToList();
                        }
                        else
                        {
                            users = users.Skip((Page - 1) * PageSize)
                                    .Take(PageSize)
                                    .OrderBy(x => x.CreatedDate)
                                    .ToList();
                        }
                    }
                    else
                    {
                        if (users.Count <= PageSize)
                        {
                            users = users.OrderByDescending(x => x.CreatedDate).ToList();
                        }
                        else
                        {
                            users = users.OrderByDescending(x => x.CreatedDate).Skip((Page - 1) * PageSize)
                                .Take(PageSize)
                                .ToList();
                        }
                    }

                    List<BirthdayReport> list = new List<BirthdayReport>();
                    foreach (var item in users)
                    {
                        list.Add(new BirthdayReport
                        {
                            FullName = item.FirstName + " " + item.LastName,
                            BirthDate = item.Dob,
                            Age = "Turns "+ _commonHelper.TodaysBirthDayList(item.Dob),
                            FirstChar = item.FirstName.Substring(0, 1),
                            Id = item.Id
                        });
                    }
                    getBirthdayReportListResDTO.BirthdayReports = list;

                    commonResponse.Status = true;
                    commonResponse.Data = getBirthdayReportListResDTO;
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
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse SendEmailToCRM(DailyEventEmailReqDTO dailyEventEmailReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DailyEventEmailResDTO dailyEventEmailResDTO = new DailyEventEmailResDTO();
            try
            {
                var ISExistmail = _commonRepo.getUserList().Where(x => x.Email == dailyEventEmailReqDTO.Email).FirstOrDefault();
                if (ISExistmail != null)
                {
                    var templateName = dailyEventEmailReqDTO.TemplateName.Split('.');
                    var fileExtension = templateName[templateName.Length - 1];

                    //var uploadTemplate = _commonHelper.UploadFile(dailyEventEmailReqDTO.UploadTemplate, ISExistmail.ClientAccNo, _commonHelper.GetCurrentDateTime().ToString("ddMMyyyyHHmmss") + "." + fileExtension);

                    var uploadTemplate = _commonHelper.UploadFile(dailyEventEmailReqDTO.UploadTemplate, ISExistmail.ClientAccNo, dailyEventEmailReqDTO.TemplateName);
                    DailyEventEmailMst dailyEvent = new DailyEventEmailMst();
                    dailyEvent.EmailFor = "Birthday";
                    dailyEvent.Gender = dailyEventEmailReqDTO.Gender;
                    dailyEvent.Subject = dailyEventEmailReqDTO.Subject;
                    dailyEvent.Message = dailyEventEmailReqDTO.Message;
                    dailyEvent.CreatedBy = dailyEventEmailReqDTO.SenderId;
                    dailyEvent.CreatedDate = _commonHelper.GetCurrentDateTime();
                    dailyEvent.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    dailyEvent.IsActive = true;
                    dailyEvent.IsDeleted = false;

                    var FileUploadResponse = _commonHelper.UploadBase64File(dailyEventEmailReqDTO.TemplateName, dailyEventEmailReqDTO.UploadTemplate, ISExistmail.ClientAccNo, ISExistmail.AccessCategoryId);
                    if (FileUploadResponse.StatusCode == HttpStatusCode.OK)
                    {
                        _dbContextt.DailyEventEmailMsts.Add(new DailyEventEmailMst { SenderId = 1, ReceiverId = 1, Gender = dailyEvent.Gender, EmailFor = "Birthday", UploadTemplate = FileUploadResponse.Data, Message = dailyEvent.Message, Subject = dailyEvent.Subject, IsActive = true, IsDeleted = false, CreatedBy = dailyEvent.CreatedBy, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
                        _dbContextt.SaveChanges();
                    }
                    else
                    {
                        _dbContextt.DailyEventEmailMsts.Add(new DailyEventEmailMst { SenderId = 1, ReceiverId = 1, Gender = dailyEvent.Gender, EmailFor = "Birthday", UploadTemplate = "", Message = dailyEvent.Message, Subject = dailyEvent.Subject, IsActive = true, IsDeleted = false, CreatedBy = dailyEvent.CreatedBy, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
                        _dbContextt.SaveChanges();
                    }

                    SendEmailRequestModel sendEmailRequestModel = new SendEmailRequestModel();

                    if (FileUploadResponse.StatusCode == HttpStatusCode.OK)
                    {
                        string FullPath = Path.Combine("wwwroot", uploadTemplate.Data);
                        sendEmailRequestModel.ToEmail = dailyEventEmailReqDTO.Email;
                        sendEmailRequestModel.Body = dailyEventEmailReqDTO.Message;
                        sendEmailRequestModel.Subject = dailyEventEmailReqDTO.Subject;
                        sendEmailRequestModel.Attachment = FullPath;
                    }
                    else
                    {
                        sendEmailRequestModel.ToEmail = dailyEventEmailReqDTO.Email;
                        sendEmailRequestModel.Body = dailyEventEmailReqDTO.Message;
                        sendEmailRequestModel.Subject = dailyEventEmailReqDTO.Subject;
                    }
                    var EmailSend = _commonHelper.SendEmail(sendEmailRequestModel);
                    commonResponse.Data = dailyEventEmailResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Email Send Sucessfully.";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can Not Send Mail...!!!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}

