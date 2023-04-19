using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net;
using static DTO.ResDTO.GetAllAdminAppointmentResDTO;
using static DTO.ResDTO.GetAllAppointmentByLocalSearchResDTO;
using static DTO.ResDTO.GetAllAppointmentResDTO;
using static DTO.ResDTO.RemarksResDTO;

namespace BussinessLayer
{
    public class PatientBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;

        public PatientBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, IConfiguration configuration, AuthRepo authRepo, CommonHelper commonHelper)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _iConfiguration = configuration;
            _authRepo = authRepo;
        }

        public CommonResponse GetAllAppointment(GetAllAppointmentReqDTO getAllAppointmentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();

            GetAllAppointmentResDTO getAllAppointmentResDTO = new GetAllAppointmentResDTO();
            AppointmentList appointmentList = new AppointmentList();
            List<AppointmentList> appoitmentlist = new List<AppointmentList>();

            int number = Convert.ToInt32(_iConfiguration.GetSection("ByDefaultPagination:Page").Value);
            int size = Convert.ToInt32(_iConfiguration.GetSection("ByDefaultPagination:PageSize").Value);

            number = getAllAppointmentReqDTO.PageNumber == 0 ? number : getAllAppointmentReqDTO.PageNumber;
            size = getAllAppointmentReqDTO.PageSize == 0 ? size : getAllAppointmentReqDTO.PageSize;

            var NewSchedulingCondition = _dbContext.AppointmentMsts.Where(x => x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId || x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId).ToList() != null ? true : false;

            var user = _commonRepo.getAllUsers().Where(x => x.Id == getAllAppointmentReqDTO.ReceptionistId).FirstOrDefault();
            var role = _dbContext.RoleMsts.FirstOrDefault(x => x.Id == user.Role);

            var NewSchedulingCount = 0;
            var ReSchedulingCount = 0;
            var DoneCount = 0;
            var OtherCount = 0;


            if (role.RoleName == "Admin")
            {
                NewSchedulingCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 1).Count();
                ReSchedulingCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 5).Count();
                DoneCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 6).Count();
                OtherCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 7).Count();

            }
            else
            {
                NewSchedulingCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 1 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count();
                ReSchedulingCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 5 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count();
                DoneCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 6 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count();
                OtherCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 7 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count();

            }
            var AppointmentType = !string.IsNullOrWhiteSpace(getAllAppointmentReqDTO.AppointmentType) ? getAllAppointmentReqDTO.AppointmentType : "New_Scheduling";
           
            try
            {
                if (role.RoleName == "Admin")
                {
                    appoitmentlist = (from u in _dbContext.AppointmentMsts.Where(x => x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId || x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId)
                                      join l in _dbContext.CallTypeMsts.Where(x => x.CallTypeName == AppointmentType)
                                     on u.CallTypeId equals l.Id
                                      join E in _commonRepo.getAllPhysician()
                                      on u.AppDoctorId equals E.Id
                                      select new { u, l, E }).ToList().Select((x, index) => new AppointmentList
                                      {
                                          SR = index + 1,
                                          AccountNo = x.u.AccountNo,
                                          PatientName = x.u.PatientFirstName,
                                          PrimaryInsuranceName = x.u.PrimaryInsuranceName,
                                          SecondaryInsuranceName = x.u.SecondaryInsuranceName,
                                          Status = x.u.Status,
                                          ApptTime = x.u.ActualAppoitmentDate?.ToString("MM/dd/yyyy hh:mm tt"),
                                          EntryTime = x.u.CreatedDate.ToString("MM/dd/yyyy hh:mm tt"),
                                          DoName = x.E.DoctorFirstName + " " + x.E.DoctorLastName,
                                          Notes = x.u.Notes,
                                          Email = _dbContext.PatientEmailMsts.FirstOrDefault(y => y.SenderId == x.u.Id) != null ? true : false,
                                          Remark = _dbContext.RemarkMsts.FirstOrDefault(i => i.AppointmentId == x.u.Id) != null ? true : false,
                                          UserId = x.u.Id,
                                          DOB = x.u.PatientDob,
                                          CallType = AppointmentType,
                                          //IsEditable = x.u.CallTypeId == 6 && _dbContext.NotificationMsts.FirstOrDefault(y => y.CreatedBy == x.u.Id &&     y.ApprovalStatus == "Approve") == null ? false : true,
                                          IsEditable = x.u.IsEditable,
                                          PatientEmail = x.u.PatientEmail,
                                          ReceptionistId = x.u.CreatedBy,
                                          AdminId = 1
                                      }).ToList();

                }
                else
                {
                    appoitmentlist = (from u in _dbContext.AppointmentMsts.Where(x => x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId)
                                      join l in _dbContext.CallTypeMsts.Where(x => x.CallTypeName == AppointmentType)
                                     on u.CallTypeId equals l.Id
                                      join E in _commonRepo.getAllPhysician()
                                      on u.AppDoctorId equals E.Id
                                      select new { u, l, E }).ToList().Select((x, index) => new AppointmentList
                                      {
                                          SR = index + 1,
                                          AccountNo = x.u.AccountNo,
                                          PatientName = x.u.PatientFirstName,
                                          PrimaryInsuranceName = x.u.PrimaryInsuranceName,
                                          SecondaryInsuranceName = x.u.SecondaryInsuranceName,
                                          Status = x.u.Status,
                                          ApptTime = x.u.ActualAppoitmentDate?.ToString("MM/dd/yyyy hh:mm tt"),
                                          EntryTime = x.u.CreatedDate.ToString("MM/dd/yyyy hh:mm tt"),
                                          DoName = x.E.DoctorFirstName + " " + x.E.DoctorLastName,
                                          Notes = x.u.Notes,
                                          DOB = x.u.PatientDob,
                                          Email = _dbContext.PatientEmailMsts.FirstOrDefault(y => y.SenderId == x.u.Id) != null ? true : false,
                                          Remark = _dbContext.RemarkMsts.FirstOrDefault(i => i.AppointmentId == x.u.Id) != null ? true : false,
                                          UserId = x.u.Id,
                                          CallType = AppointmentType,
                                          //IsEditable = x.u.CallTypeId == 6 && _dbContext.NotificationMsts.FirstOrDefault(y => y.CreatedBy == x.u.Id && y.ApprovalStatus == "Approve") == null ? x.u.IsEditable : true,
                                           IsEditable = x.u.IsEditable,
                                          PatientEmail = x.u.PatientEmail,
                                          ReceptionistId = getAllAppointmentReqDTO.ReceptionistId,
                                          AdminId = 1
                                      }).ToList();

                }

                appoitmentlist = appoitmentlist
                                .OrderByDescending(x => x.UserId)
                                .ToList();

                getAllAppointmentResDTO.appointmentList = appoitmentlist;
                getAllAppointmentResDTO.TotalCount = appoitmentlist.Count;
                getAllAppointmentResDTO.NewSchedulingCount = NewSchedulingCount;
                getAllAppointmentResDTO.ReschedulingCount = ReSchedulingCount;
                getAllAppointmentResDTO.DoneCount = DoneCount;
                getAllAppointmentResDTO.OtherCount = OtherCount;
                getAllAppointmentResDTO.AppointmentType = AppointmentType;



                if (getAllAppointmentReqDTO.GlobalSearch != null && !string.IsNullOrWhiteSpace(getAllAppointmentReqDTO.GlobalSearch))
                {
                    var NewSchednulinglist = 0;
                    var ReSchedulingList = 0;
                    var Donelist = 0;
                    var Otherlist = 0;

                    string date = getAllAppointmentReqDTO.GlobalSearch;


                    if (DateTime.TryParse(date, out DateTime temp) == true)
                    {
                        DateTime dt = DateTime.Parse(date);

                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.PatientDob == dt && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count() > 0)
                        {

                            NewSchednulinglist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId && x.PatientDob == dt).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.DOB == dt).ToList();

                        }

                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5 && x.PatientDob == dt && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count() > 0)
                        {
                            ReSchedulingList = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5 && x.PatientDob == dt && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.DOB == dt).ToList();
                        }

                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6 && x.PatientDob == dt && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count() > 0)
                        {
                            Donelist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6 && x.PatientDob == dt && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.DOB == dt).ToList();
                        }

                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.PatientDob == dt && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count() > 0)
                        {
                            Otherlist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.PatientDob == dt && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.DOB == dt).ToList();
                        }

                    }

                    if (role.RoleName == "Admin")
                    {
                        string date1 = getAllAppointmentReqDTO.GlobalSearch;

                        if (DateTime.TryParse(date1, out DateTime temp1) == true)
                        {
                            DateTime dt = DateTime.Parse(date);

                            if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.PatientDob == dt).Count() > 0)
                            {

                                NewSchednulinglist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.PatientDob == dt).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.DOB == dt).ToList();

                            }
                        
                            if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5 && x.PatientDob == dt).Count() > 0)
                            {
                                ReSchedulingList = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5 && x.PatientDob == dt).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.DOB == dt).ToList();
                            }

                            if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6 && x.PatientDob == dt).Count() > 0)
                            {
                                Donelist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6 && x.PatientDob == dt).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.DOB == dt).ToList();
                            }

                            if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.PatientDob == dt).Count() > 0)
                            {
                                Otherlist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.PatientDob == dt).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.DOB == dt).ToList();
                            }
                        }
                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1).Count() > 0)
                        {
                            if (_commonRepo.getAllAppointment().Where(x => x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                            {
                                NewSchednulinglist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.PatientName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                            }

                            if (_commonRepo.getAllAppointment().Where(x => x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                            {
                                NewSchednulinglist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                            }
                        }
                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5).Count() > 0)
                        {
                            if (_commonRepo.getAllAppointment().Where(x => x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                            {
                                ReSchedulingList = _commonRepo.getAllAppointment().Where(x => x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.PatientName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                            }
                            if (_commonRepo.getAllAppointment().Where(x => x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                            {
                                ReSchedulingList = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5 && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                            }

                        }
                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6).Count() > 0)
                        {
                            if (_commonRepo.getAllAppointment().Where(x => x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                            {
                                Donelist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6 && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.PatientName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                            }
                            if (_commonRepo.getAllAppointment().Where(x => x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                            {
                                Donelist = _commonRepo.getAllAppointment().Where(x => x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                            }

                        }
                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7).Count() > 0)
                        {
                            if (_commonRepo.getAllAppointment().Where(x => x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId && x.PatientFirstName.ToLower().StartsWith(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                            {
                                Otherlist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.PatientName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                            }
                            if (_commonRepo.getAllAppointment().Where(x => x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                            {
                                Otherlist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                                appoitmentlist = appoitmentlist.Where(x => x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                            }
                        }

                    }
                    else
                    {
                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                        {
                            NewSchednulinglist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.PatientName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                        }

                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                        {
                            NewSchednulinglist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 1 && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower()) || x.CreatedBy != getAllAppointmentReqDTO.ReceptionistId && x.CreatedBy == 1).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                        }

                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId && x.PatientFirstName.ToLower().StartsWith(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                        {
                            ReSchedulingList = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5 && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.PatientName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                        }
                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                        {
                            ReSchedulingList = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 5 && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                        }


                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                        {
                            Donelist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6 && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.PatientName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                        }
                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                        {
                            Donelist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6 && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                        }


                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId && x.PatientFirstName.ToLower().StartsWith(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                        {
                            Otherlist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.PatientFirstName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.PatientName.ToLower().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                        }
                        if (_commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.CreatedBy == getAllAppointmentReqDTO.ReceptionistId && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count() > 0)
                        {
                            Otherlist = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 7 && x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).Count();
                            appoitmentlist = appoitmentlist.Where(x => x.AccountNo.ToString().Contains(getAllAppointmentReqDTO.GlobalSearch.ToLower())).ToList();
                        }

                    }

                    NewSchedulingCount = NewSchednulinglist;
                    ReSchedulingCount = ReSchedulingList;
                    DoneCount = Donelist;
                    OtherCount = Otherlist;

                    if (NewSchedulingCount == 0 && ReSchedulingCount == 0 && DoneCount == 0 && OtherCount == 0)
                    {
                        appoitmentlist = appoitmentlist.Where(x => x.AccountNo == 00000).ToList();
                    }

                    getAllAppointmentResDTO.TotalCount = appoitmentlist.Count();
                    getAllAppointmentResDTO.NewSchedulingCount = NewSchedulingCount;
                    getAllAppointmentResDTO.ReschedulingCount = ReSchedulingCount;
                    getAllAppointmentResDTO.DoneCount = DoneCount;
                    getAllAppointmentResDTO.OtherCount = OtherCount;
                    getAllAppointmentResDTO.AppointmentType = AppointmentType;
                    getAllAppointmentResDTO.appointmentList = appoitmentlist;
                }

                if (appoitmentlist.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "All Appointment List";
                    commonResponse.Data = getAllAppointmentResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found";
                    commonResponse.Data = getAllAppointmentResDTO;

                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public async Task<CommonResponse> GenerateAppointmentNumber()
        {
            CommonResponse response = new CommonResponse();
            GenerateNumberResDTO generateNumberResDTO = new GenerateNumberResDTO();
            decimal? AppointmentNumber = 0;

            var LastDurationEntry = _dbContext.DurationMsts.OrderByDescending(x => x.AppointmentId).FirstOrDefault();

            if (LastDurationEntry == null)
            {
                AppointmentNumber = 1;
            }
            else
            {
                if (LastDurationEntry.AppointmentId != null)
                {
                    AppointmentNumber = Convert.ToInt16(LastDurationEntry.AppointmentId) + 1;
                }
                else
                {
                    AppointmentNumber = 1;
                }
            }
            generateNumberResDTO.AppointmentNumber = (decimal)AppointmentNumber;
            response.Status = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Message = "Appointment number generated successfully.";
            response.Data = generateNumberResDTO;
            return response;

        }

        public CommonResponse AddAppoitment(AddAppoitmentReqDTO addAppoitmentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddAppoitmentResDTO addAppoitmentResDTO = new AddAppoitmentResDTO();

            try
            {
                var checkAppointment = _dbContext.AppointmentMsts.Where(x => x.AccountNo == addAppoitmentReqDTO.AppointmentNumber).FirstOrDefault();
                var checkDuplicateAppointment = _dbContext.AppointmentMsts.Where(x => x.AccountNo == addAppoitmentReqDTO.AppointmentNumber && x.ActualAppoitmentDate == Convert.ToDateTime(addAppoitmentReqDTO.ActualAppoitmentDate)).FirstOrDefault();
                AppointmentMst appoitment = new AppointmentMst();
                appoitment.CallTypeId = addAppoitmentReqDTO.CallTypeId;
                appoitment.AccountNo = addAppoitmentReqDTO.AppointmentNumber;
                appoitment.Date = addAppoitmentReqDTO.Date.Date;
                appoitment.AppoitmentLastDate = addAppoitmentReqDTO.IsAvailableLastDate;
                appoitment.ExtensionId = addAppoitmentReqDTO.ExtensionId;
                string date = addAppoitmentReqDTO.ActualAppoitmentDate;
                DateTime dt = DateTime.Parse(date);
                appoitment.ActualAppoitmentDate = dt;
                appoitment.TaxId = addAppoitmentReqDTO.TaxId;
                appoitment.PatientFirstName = addAppoitmentReqDTO.PatientName;
                appoitment.PatientLastName = addAppoitmentReqDTO.PatientName;
                appoitment.LastAppoitmentDate = addAppoitmentReqDTO.LastAppoitmentDate;
                appoitment.PatientMobileNo = addAppoitmentReqDTO.PatientMobileNo;
                appoitment.PatientDob = addAppoitmentReqDTO.PatientDob.Date;
                appoitment.AppDoctorId = addAppoitmentReqDTO.AppDoctorId;
                appoitment.DoctorGender = addAppoitmentReqDTO.DoctorGender;
                appoitment.Pcp = addAppoitmentReqDTO.Pcp;
                appoitment.PcpmobileNo = addAppoitmentReqDTO.PcpmobileNo;
                appoitment.ReferingMd = addAppoitmentReqDTO.ReferingMd;
                appoitment.ReferingMobileNo = addAppoitmentReqDTO.ReferingMobileNo;
                appoitment.PrimaryInsuranceName = addAppoitmentReqDTO.PrimaryInsuranceName;
                appoitment.PrimaryInsuranceId = addAppoitmentReqDTO.PrimaryInsuranceId;
                appoitment.SecondaryInsuranceName = addAppoitmentReqDTO.SecondaryInsuranceName;
                appoitment.SecondaryInsuranceId = addAppoitmentReqDTO.SecondaryInsuranceId;
                appoitment.Notes = addAppoitmentReqDTO.Notes;
                appoitment.Reason = addAppoitmentReqDTO.Reason;
                appoitment.IsAppoitmentVehicleOrworkInjury = addAppoitmentReqDTO.IsAppoitmentVehicleOrworkInjury;
                appoitment.PatientEmail = addAppoitmentReqDTO.PatientEmail;
                appoitment.IsCovidPossitive = addAppoitmentReqDTO.IsCovidPossitive;
                appoitment.IsVaccinated = addAppoitmentReqDTO.IsVaccinated;
                appoitment.IsIdCurrentOrExpired = addAppoitmentReqDTO.IsIdCurrentOrExpired;
                appoitment.CallTypeId = addAppoitmentReqDTO.CallTypeId;
                appoitment.IsMatchInsurance = addAppoitmentReqDTO.IsMatchInsurance;
                appoitment.IdExpirationDate = addAppoitmentReqDTO.IdExpirationDate.Date;
                appoitment.IsActive = true;
                appoitment.IsDeleted = false;
                appoitment.CreatedBy = addAppoitmentReqDTO.CreatedBy;
                appoitment.UpdatedBy = addAppoitmentReqDTO.CreatedBy;
                appoitment.Status = addAppoitmentReqDTO.Status;
                appoitment.IsEditable = true;
                appoitment.CreatedDate = _commonHelper.GetCurrentDateTime();
                appoitment.UpdatedDate = _commonHelper.GetCurrentDateTime();


                if (checkAppointment == null)
                {
                    var appoitmentlist = _dbContext.AppointmentMsts.Add(appoitment);
                    _dbContext.SaveChanges();

                    addAppoitmentResDTO.Id = appoitment.Id;
                    addAppoitmentResDTO.AccountNo = appoitment.AccountNo;
                    addAppoitmentResDTO.PatientName = appoitment.PatientFirstName;
                    addAppoitmentResDTO.Status = appoitment.Status;

                    if (appoitmentlist != null)
                    {
                        var Duration = _dbContext.DurationMsts.OrderBy(x => x.AppointmentId).Last();
                        Duration.EndDate = DateTime.Now;
                        Duration.AppointmentId = appoitment.AccountNo;

                        _dbContext.DurationMsts.Update(Duration);
                        _dbContext.SaveChanges();

                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Status = true;
                        commonResponse.Message = appoitment.Status == "Hold" ? "Appointment form is on hold  successfully " : "Appointment Added Successfully";
                        commonResponse.Data = addAppoitmentResDTO;

                    }
                    else
                    {
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Status = false;
                        commonResponse.Message = "Appointment Not Added!";

                    }
                }
                else
                {
                    if (checkDuplicateAppointment == null)
                    {
                        var appoitmentlist = _dbContext.AppointmentMsts.Add(appoitment);
                        _dbContext.SaveChanges();

                        addAppoitmentResDTO.Id = appoitment.Id;
                        addAppoitmentResDTO.AccountNo = appoitment.AccountNo;
                        addAppoitmentResDTO.PatientName = appoitment.PatientFirstName;
                        addAppoitmentResDTO.Status = appoitment.Status;

                        if (appoitmentlist != null)
                        {
                            var Duration = _dbContext.DurationMsts.OrderBy(x => x.AppointmentId).Last();
                            Duration.EndDate = DateTime.Now;
                            Duration.AppointmentId = appoitment.AccountNo;

                            _dbContext.DurationMsts.Update(Duration);
                            _dbContext.SaveChanges();

                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Status = true;
                            commonResponse.Message = "Duplicate Appointment Added Successfully";
                            commonResponse.Data = addAppoitmentResDTO;

                        }
                        else
                        {
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Status = false;
                            commonResponse.Message = "Appointment Not Added!";

                        }
                    }
                    else
                    {
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Status = true;
                        commonResponse.Message = "Appointment is Already Exist on Same date and time with same Appointment Number";
                    }


                }
                addAppoitmentResDTO.Id = appoitment.Id;
                addAppoitmentResDTO.AccountNo = appoitment.AccountNo;
                addAppoitmentResDTO.PatientName = appoitment.PatientFirstName;
                addAppoitmentResDTO.Status = appoitment.Status;

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddRemark(AddRemarkReqDTO addRemarkReqDTO)
        {

            CommonResponse response = new CommonResponse();
            AddRemarkResDTO remarkResDTO = new AddRemarkResDTO();
            var user = _commonRepo.getUserList_Login().FirstOrDefault(x => x.Id == addRemarkReqDTO.LoginUserId);
            var role = _dbContext.RoleMsts.FirstOrDefault(x => x.Id == user.Role);
            if ((addRemarkReqDTO != null) && (addRemarkReqDTO.AppointmentNumber > 0) && (addRemarkReqDTO.Remarks != null))
            {
                RemarkMst remarkMst = new RemarkMst();
                AddRemarkResDTO addRemarkResDTO = new AddRemarkResDTO();
                remarkMst.AppointmentId = addRemarkReqDTO.UserId;
                remarkMst.Remark = addRemarkReqDTO.Remarks;
                remarkMst.Details = addRemarkReqDTO.Remarks;
                remarkMst.Status = 1;
                remarkMst.IsActive = true;
                remarkMst.IsDeleted = false;
                remarkMst.CreatedBy = addRemarkReqDTO.LoginUserId;
                remarkMst.CreatedDate = DateTime.Now;
                remarkMst.UpdatedBy = addRemarkReqDTO.LoginUserId;
                remarkMst.UpdatedDate = DateTime.Now;

                _dbContext.RemarkMsts.Add(remarkMst);
                _dbContext.SaveChanges();

                var user1 = _commonRepo.getAllAppointment().FirstOrDefault(x => x.Id == remarkMst.AppointmentId);
                if (role.RoleName == "Admin")
                {
                    NotificationMst notification = new NotificationMst();
                    notification.SenderId = addRemarkReqDTO.LoginUserId;
                    notification.ReceiverId = addRemarkReqDTO.ReceiverId;
                    notification.AdminDescription = " ";
                    notification.AdminDescriptionTitle = " ";
                    notification.DescriptionTitle = "Remarks Notification";
                    notification.Description = "You have received Remarks from" + " " + "Admin for" +" "+user1.PatientFirstName ;
                    notification.IsNotificationRead = false;
                    notification.CreatedBy = addRemarkReqDTO.LoginUserId;
                    notification.UpdatedBy = addRemarkReqDTO.LoginUserId;
                    notification.CreatedDate = DateTime.Now;
                    notification.UpdatedDate = DateTime.Now;

                    _dbContext.NotificationMsts.Add(notification);
                    _dbContext.SaveChanges();
                }
                else
                {
                    NotificationMst notification = new NotificationMst();
                    notification.SenderId = addRemarkReqDTO.LoginUserId;
                    notification.ReceiverId = addRemarkReqDTO.ReceiverId;
                    notification.AdminDescription = "You have received Remarks from Receptionist for" + " " + user1.PatientFirstName;
                    notification.AdminDescriptionTitle = "Remarks Notification";
                    notification.DescriptionTitle = " ";
                    notification.Description = " ";
                    notification.IsNotificationRead = false;
                    notification.CreatedBy = addRemarkReqDTO.LoginUserId;
              
                    notification.UpdatedBy = addRemarkReqDTO.LoginUserId;
                    notification.CreatedDate = DateTime.Now;
                    notification.UpdatedDate = DateTime.Now;

                    _dbContext.NotificationMsts.Add(notification);
                    _dbContext.SaveChanges();
                }

                remarkResDTO.AppointmentNumber = remarkMst.AppointmentId;
                remarkResDTO.Remarks = remarkMst.Remark;
                remarkResDTO.ClickType = 1;
                remarkResDTO.RemarkId = remarkMst.Id;

                response.StatusCode = HttpStatusCode.OK;
                response.Message = "Remark Added Successfully";
                response.Status = true;
                response.Data = remarkResDTO;
            }
            else
            {
                response.Message = "Enter Appointment Number and Remarks!";
            }


            return response;
        }

        public async Task<CommonResponse> GetRemarksByAppointmentNo(RemarkReqDTO remarkReqDTO)
        {
            CommonResponse response = new CommonResponse();
            RemarksResDTO remakrResList = new RemarksResDTO();

            List<RemarkList> remarks = (from c in _dbContext.RemarkMsts
                                        where c.AppointmentId == remarkReqDTO.appointmentNo
                                        join
                                        d in _dbContext.UserMsts on c.CreatedBy equals d.Id
                                        select new { c, d }
                       ).ToList().Select((x, index) => new RemarkList
                       {
                           SR = index + 1,
                           RemarkTime = x.c.CreatedDate.ToString("MM-dd-yyyy hh:mm tt"),
                           Remarks = x.c.Remark,
                           EnterBy = x.d.FirstName + " " + x.d.LastName,
                           Status = x.c.Status,
                           RemarkId = x.c.Id,
                           UpdatedDate = x.c.UpdatedDate,
                           AdminId = 1
                       }).ToList();

            remarks = remarks
                              .OrderByDescending(x => x.UpdatedDate)
                              .ToList();


            remakrResList.AppointmentType = remarkReqDTO.AppointmentType;
            remakrResList.AppointmentNo = remarkReqDTO.appointmentNo;

            remakrResList.remarkLists = remarks;

            if (remarks.Count > 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Status = true;
                response.Message = "Remarks found";
                response.Data = remakrResList;
            }
            else
            {
                response.Message = "Remarks not found";
            }
            return response;
        }


        public CommonResponse GetAllAppointmentbyId(GetAllAppointmentbyIdReqDTO getAllAppointmentbyIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            GetAllAppointmentbyIdResDTO getAllAppointmentResDTO = new GetAllAppointmentbyIdResDTO();

            try
            {
                var appointmentList = _commonRepo.getAllAppointment().FirstOrDefault(x => x.Id == getAllAppointmentbyIdReqDTO.Id);
                if (appointmentList != null)
                {
                    getAllAppointmentResDTO.CallTypeId = appointmentList.CallTypeId;
                    getAllAppointmentResDTO.AppointmentNumber = appointmentList.AccountNo;
                    getAllAppointmentResDTO.Date = appointmentList.Date?.ToString("MM/dd/yyyy");
                    getAllAppointmentResDTO.IsAvailableLastDate = appointmentList.AppoitmentLastDate;
                    getAllAppointmentResDTO.ActualAppoitmentDate = appointmentList.ActualAppoitmentDate?.ToString("MM-dd-yyyy HH:mm");
                    getAllAppointmentResDTO.LastAppoitmentDate = appointmentList.LastAppoitmentDate?.ToString("MM/dd/yyyy");
                    getAllAppointmentResDTO.ExtensionId = appointmentList.ExtensionId;
                    getAllAppointmentResDTO.TaxId = appointmentList.TaxId;
                    getAllAppointmentResDTO.PatientName = appointmentList.PatientFirstName;
                    getAllAppointmentResDTO.PatientEmail = appointmentList.PatientEmail;
                    getAllAppointmentResDTO.PatientMobileNo = appointmentList.PatientMobileNo;
                    getAllAppointmentResDTO.PatientDob = appointmentList.PatientDob.ToString("MM/dd/yyyy");
                    getAllAppointmentResDTO.AppDoctorId = appointmentList.AppDoctorId;
                    getAllAppointmentResDTO.DoctorGender = appointmentList.DoctorGender;
                    getAllAppointmentResDTO.Pcp = appointmentList.Pcp;
                    getAllAppointmentResDTO.PcpmobileNo = appointmentList.PcpmobileNo;
                    getAllAppointmentResDTO.ReferingMd = appointmentList.ReferingMd;
                    getAllAppointmentResDTO.ReferingMobileNo = appointmentList.ReferingMobileNo;
                    getAllAppointmentResDTO.PrimaryInsuranceId = appointmentList.PrimaryInsuranceId;
                    getAllAppointmentResDTO.PrimaryInsuranceName = appointmentList.PrimaryInsuranceName;
                    getAllAppointmentResDTO.SecondaryInsuranceId = appointmentList.SecondaryInsuranceId;
                    getAllAppointmentResDTO.SecondaryInsuranceName = appointmentList.SecondaryInsuranceName;
                    getAllAppointmentResDTO.Notes = appointmentList.Notes;
                    getAllAppointmentResDTO.Reason = appointmentList.Reason;
                    getAllAppointmentResDTO.IsAppoitmentVehicleOrworkInjury = appointmentList.IsAppoitmentVehicleOrworkInjury;
                    getAllAppointmentResDTO.IsCovidPossitive = appointmentList.IsCovidPossitive;
                    getAllAppointmentResDTO.IsIdCurrentOrExpired = appointmentList.IsIdCurrentOrExpired;
                    getAllAppointmentResDTO.IsVaccinated = appointmentList.IsVaccinated;
                    getAllAppointmentResDTO.IsMatchInsurance = appointmentList.IsMatchInsurance;
                    getAllAppointmentResDTO.IdExpirationDate = appointmentList.IdExpirationDate.ToString("MM/dd/yyyy");
                    getAllAppointmentResDTO.Status = appointmentList.Status;


                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "All Appointment List";
                    commonResponse.Data = getAllAppointmentResDTO;

                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse EditAppointment(EditAppointmentReqDTO editAppointmentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();

            EditAppointmentResDTO editAppointmentResDTO = new EditAppointmentResDTO();
            try
            {
                var appointmentList = _commonRepo.getAllAppointment().FirstOrDefault(x => x.Id == editAppointmentReqDTO.UserId);
                var checkDuplicateAppointment = _dbContext.AppointmentMsts.Where(x => x.AccountNo == editAppointmentReqDTO.AppointmentNumber && x.ActualAppoitmentDate == Convert.ToDateTime(editAppointmentReqDTO.ActualAppoitmentDate) && x.Id != editAppointmentReqDTO.UserId).FirstOrDefault();
                if (appointmentList != null)
                {
                  
                    appointmentList.CallTypeId = editAppointmentReqDTO.CallTypeId;
                    appointmentList.AccountNo = editAppointmentReqDTO.AppointmentNumber;
                    appointmentList.Date = editAppointmentReqDTO.Date;
                    string date = editAppointmentReqDTO.ActualAppoitmentDate;
                    DateTime dt = DateTime.Parse(date);
                    appointmentList.ActualAppoitmentDate = dt;
                    appointmentList.AppoitmentLastDate = editAppointmentReqDTO.IsAvailableLastDate;
                    appointmentList.LastAppoitmentDate = editAppointmentReqDTO.LastAppoitmentDate;
                    appointmentList.ExtensionId = editAppointmentReqDTO.ExtensionId;
                    appointmentList.TaxId = editAppointmentReqDTO.TaxId;
                    appointmentList.PatientFirstName = editAppointmentReqDTO.PatientName;
                    appointmentList.PatientEmail = editAppointmentReqDTO.PatientEmail;
                    appointmentList.PatientMobileNo = editAppointmentReqDTO.PatientMobileNo;
                    appointmentList.PatientDob = editAppointmentReqDTO.PatientDob;
                    appointmentList.AppDoctorId = editAppointmentReqDTO.AppDoctorId;
                    appointmentList.DoctorGender = editAppointmentReqDTO.DoctorGender;
                    appointmentList.Pcp = editAppointmentReqDTO.Pcp;
                    appointmentList.PcpmobileNo = editAppointmentReqDTO.PcpmobileNo;
                    appointmentList.ReferingMd = editAppointmentReqDTO.ReferingMd;
                    appointmentList.ReferingMobileNo = editAppointmentReqDTO.ReferingMobileNo;  
                    appointmentList.PrimaryInsuranceId = editAppointmentReqDTO.PrimaryInsuranceId;
                    appointmentList.PrimaryInsuranceName = editAppointmentReqDTO.PrimaryInsuranceName;
                    appointmentList.SecondaryInsuranceId = editAppointmentReqDTO.SecondaryInsuranceId;
                    appointmentList.SecondaryInsuranceName = editAppointmentReqDTO.SecondaryInsuranceName;
                    appointmentList.Notes = editAppointmentReqDTO.Notes;
                    appointmentList.Reason = editAppointmentReqDTO.Reason;
                    appointmentList.IsAppoitmentVehicleOrworkInjury = editAppointmentReqDTO.IsAppoitmentVehicleOrworkInjury;
                    appointmentList.IsCovidPossitive = editAppointmentReqDTO.IsCovidPossitive;
                    appointmentList.IsIdCurrentOrExpired = editAppointmentReqDTO.IsIdCurrentOrExpired;
                    appointmentList.IsVaccinated = editAppointmentReqDTO.IsVaccinated;
                    appointmentList.Status = editAppointmentReqDTO.Status;
                    appointmentList.UpdatedDate = DateTime.Now;


                    if (checkDuplicateAppointment == null)
                    {
                        var appoitmentlist = _dbContext.AppointmentMsts.Update(appointmentList);
                        _dbContext.SaveChanges();

                        editAppointmentResDTO.AccountNo = appointmentList.AccountNo;
                        editAppointmentResDTO.Status = appointmentList.Status;

                        if (appoitmentlist != null)
                        {
                            var Duration = _dbContext.DurationMsts.OrderBy(x => x.AppointmentId).Last();
                            Duration.EndDate = DateTime.Now;
                            Duration.AppointmentId = appointmentList.AccountNo;

                            _dbContext.DurationMsts.Update(Duration);
                            _dbContext.SaveChanges();

                            var notification = _dbContext.NotificationMsts.Where(x => x.CreatedBy == appointmentList.Id && x.ApprovalStatus == "Approve").ToList();
                            if (notification.Count > 0)
                            {
                                appointmentList.IsEditable = false;
                                appointmentList.CallTypeId = 6;

                                _dbContext.AppointmentMsts.Update(appointmentList);
                                _dbContext.SaveChanges();
                            }

                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Status = true;
                            commonResponse.Message = appointmentList.Status == "Hold" ? "Appointment form is on hold  successfully " : "Appointment Edited Successfully";
                            commonResponse.Data = editAppointmentResDTO;

                            editAppointmentResDTO.AccountNo = appointmentList.AccountNo;
                            editAppointmentResDTO.Status = appointmentList.Status;


                        }
                        else
                        {
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Status = false;
                            commonResponse.Message = "Appointment Not Added!";

                        }
                    }
                    else
                    {
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Status = true;
                            commonResponse.Message = "Appointment is Already Exist on Same date and time with same Appointment Number";
                    }

                    //var result = _dbContext.AppointmentMsts.Update(appointmentList);
                    //_dbContext.SaveChanges();

                    //editAppointmentResDTO.AccountNo = appointmentList.AccountNo;
                    //editAppointmentResDTO.Status = appointmentList.Status;

                    //var notification = _dbContext.NotificationMsts.Where(x => x.CreatedBy == appointmentList.Id && x.ApprovalStatus == "Approve");
                    //if (notification != null)
                    //{
                    //    appointmentList.IsEditable = false;
                    //    appointmentList.CallTypeId = 6;

                    //    _dbContext.AppointmentMsts.Update(appointmentList);
                    //    _dbContext.SaveChanges();
                    //}

                    //if (result != null)
                    //{
                    //    commonResponse.Status = true;
                    //    commonResponse.StatusCode = HttpStatusCode.OK;
                    //    commonResponse.Message = editAppointmentResDTO.Status == "Hold" ? "Appointment form is on hold successfully " : "Appointment Data Updated Successfully";
                    //    commonResponse.Data = editAppointmentResDTO;
                    //}
                    //else
                    //{
                    //    commonResponse.Status = false;
                    //    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    //    commonResponse.Message = "Fail";
                    //}
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Please Enter Valid Data";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllCallTypeCount()
        {
            CommonResponse commonResponse = new CommonResponse();
            List<GetAllCallTypeCountResDTO> getAllDoctorEmailResDTO = new List<GetAllCallTypeCountResDTO>();
            try
            {

                getAllDoctorEmailResDTO = (from u in _commonRepo.getAllAppointment()

                                           join i in _dbContext.CallTypeMsts
                                           on u.CallTypeId equals i.Id
                                           select new { u, i }).ToList().Select(x => new GetAllCallTypeCountResDTO
                                           {
                                               Id = x.i.Id,
                                               Name = x.i.CallTypeName,
                                               Count = x.u.CallTypeId
                                           }).ToList();



                if (getAllDoctorEmailResDTO.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = getAllDoctorEmailResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Not Found";
                    commonResponse.Data = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse GetAllAppointmentByLocalSearch(GetAllAppointmentByLocalSearchReqDTO getAllAppointmentByLocalSearchReqDTO)
        {

            CommonResponse commonResponse = new CommonResponse();

            GetAllAppointmentByLocalSearchResDTO getAllAppointmentResDTO = new GetAllAppointmentByLocalSearchResDTO();
            AppointmentListByLocalSearch appointmentList = new AppointmentListByLocalSearch();
            List<AppointmentListByLocalSearch> appoitmentlist = new List<AppointmentListByLocalSearch>();

            int number = Convert.ToInt32(_iConfiguration.GetSection("ByDefaultPagination:Page").Value);
            int size = Convert.ToInt32(_iConfiguration.GetSection("ByDefaultPagination:PageSize").Value);


            number = getAllAppointmentByLocalSearchReqDTO.PageNumber == 0 ? number : getAllAppointmentByLocalSearchReqDTO.PageNumber;
            size = getAllAppointmentByLocalSearchReqDTO.PageSize == 0 ? size : getAllAppointmentByLocalSearchReqDTO.PageSize;

            var user = _commonRepo.getAllUsers().Where(x => x.Id == getAllAppointmentByLocalSearchReqDTO.ReceptionistId).FirstOrDefault();
            var role = _dbContext.RoleMsts.FirstOrDefault(x => x.Id == user.Role);

            var NewSchedulingCount = 0;
            var ReSchedulingCount = 0;
            var DoneCount = 0;
            var OtherCount = 0;

            if (role.RoleName == "Admin")
            {
                NewSchedulingCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 1).Count();
                ReSchedulingCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 5).Count();
                DoneCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 6).Count();
                OtherCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 7).Count();

            }
            else
            {
                NewSchedulingCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 1 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId).Count();
                ReSchedulingCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 5 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId).Count();
                DoneCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 6 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId).Count();
                OtherCount = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 7 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId).Count();

            }
            var AppointmentType = !string.IsNullOrWhiteSpace(getAllAppointmentByLocalSearchReqDTO.AppointmentType) ? getAllAppointmentByLocalSearchReqDTO.AppointmentType : "New_Scheduling";


            try
            {
                if (role.RoleName == "Admin")
                {
                    appoitmentlist = (from u in _commonRepo.getAllAppointmentList().Where(x => x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId || x.CreatedBy != getAllAppointmentByLocalSearchReqDTO.ReceptionistId)
                                      join l in _dbContext.CallTypeMsts.Where(x => x.CallTypeName == AppointmentType)
                                     on u.CallTypeId equals l.Id
                                      join E in _commonRepo.getAllPhysician()
                                      on u.AppDoctorId equals E.Id
                                      select new { u, E, l }).ToList().Select((x, index) => new AppointmentListByLocalSearch
                                      {
                                          SR = index + 1,
                                          AccountNo = x.u.AccountNo,
                                          PatientName = x.u.PatientFirstName,
                                          PrimaryInsuranceName = x.u.PrimaryInsuranceName,
                                          SecondaryInsuranceName = x.u.SecondaryInsuranceName,
                                          Status = x.u.Status,
                                          ApptTime = x.u.ActualAppoitmentDate?.ToString("MM/dd/yyyy hh:mm tt"),
                                          EntryTime = x.u.CreatedDate.ToString("MM/dd/yyyy hh:mm tt"),
                                          DoName = x.E.DoctorFirstName + " " + x.E.DoctorLastName,
                                          Notes = x.u.Notes,
                                          Email = _dbContext.PatientEmailMsts.FirstOrDefault(y => y.SenderId == x.u.Id) != null ? true : false,
                                          Remark = _dbContext.RemarkMsts.FirstOrDefault(i => i.AppointmentId == x.u.Id) != null ? true : false,
                                          UserId = x.u.Id,
                                          CallType = x.l.CallTypeName,
                                          //IsEditable = x.u.CallTypeId == 6 && _dbContext.NotificationMsts.FirstOrDefault(y => y.CreatedBy == x.u.Id && y.ApprovalStatus == "Approve") == null ? false : true,
                                          IsEditable = x.u.IsEditable,
                                          PatientEmail = x.u.PatientEmail,
                                          ReceptionistId = x.u.CreatedBy,
                                          AdminId = 1
                                      }).ToList();

                }
                else
                {
                    appoitmentlist = (from u in _commonRepo.getAllAppointmentList().Where(x => x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId)
                                      join l in _dbContext.CallTypeMsts.Where(x => x.CallTypeName == AppointmentType)
                                     on u.CallTypeId equals l.Id
                                      join E in _commonRepo.getAllPhysician()
                                      on u.AppDoctorId equals E.Id
                                      select new { u, E, l }).ToList().Select((x, index) => new AppointmentListByLocalSearch
                                      {
                                          SR = index + 1,
                                          AccountNo = x.u.AccountNo,
                                          PatientName = x.u.PatientFirstName,
                                          PrimaryInsuranceName = x.u.PrimaryInsuranceName,
                                          SecondaryInsuranceName = x.u.SecondaryInsuranceName,
                                          Status = x.u.Status,
                                          ApptTime = x.u.ActualAppoitmentDate?.ToString("MM/dd/yyyy hh:mm tt"),
                                          EntryTime = x.u.CreatedDate.ToString("MM/dd/yyyy hh:mm tt"),
                                          DoName = x.E.DoctorFirstName + " " + x.E.DoctorLastName,
                                          Notes = x.u.Notes,
                                          Email = _dbContext.PatientEmailMsts.FirstOrDefault(y => y.SenderId == x.u.Id) != null ? true : false,
                                          Remark = _dbContext.RemarkMsts.FirstOrDefault(i => i.AppointmentId == x.u.Id) != null ? true : false,
                                          UserId = x.u.Id,
                                          CallType = x.l.CallTypeName,
                                          //IsEditable = x.u.CallTypeId == 6 && _dbContext.NotificationMsts.FirstOrDefault(y => y.CreatedBy == x.u.Id && y.ApprovalStatus == "Approve") == null ? false : true,
                                          IsEditable = x.u.IsEditable,
                                          PatientEmail = x.u.PatientEmail,
                                          ReceptionistId = getAllAppointmentByLocalSearchReqDTO.ReceptionistId,
                                          AdminId = 1
                                      }).ToList();
                }

                appoitmentlist = appoitmentlist
                                .OrderByDescending(x => x.UserId)
                                .ToList();

                getAllAppointmentResDTO.appointmentList = appoitmentlist;
                getAllAppointmentResDTO.TotalCount = appoitmentlist.Count;
                getAllAppointmentResDTO.NewSchedulingCount = NewSchedulingCount;
                getAllAppointmentResDTO.ReschedulingCount = ReSchedulingCount;
                getAllAppointmentResDTO.DoneCount = DoneCount;
                getAllAppointmentResDTO.OtherCount = OtherCount;

                getAllAppointmentResDTO.AppointmentType = AppointmentType;


                if (getAllAppointmentByLocalSearchReqDTO.PageSearch != null && !string.IsNullOrEmpty(getAllAppointmentByLocalSearchReqDTO.PageSearch))
                {
                    appoitmentlist = appoitmentlist.Where(x => x.PatientName.ToLower().Contains(getAllAppointmentByLocalSearchReqDTO.PageSearch.ToLower())).ToList();

                    var NewSchednulinglist = 0;
                    var ReSchedulingList = 0;
                    var Donelist = 0;
                    var Otherlist = 0;

                    if (role.RoleName == "Admin")
                    {
                        if (getAllAppointmentByLocalSearchReqDTO.AppointmentType == "New_Scheduling" || string.IsNullOrWhiteSpace(getAllAppointmentByLocalSearchReqDTO.AppointmentType))
                        {
                            NewSchednulinglist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 1 && x.PatientFirstName.ToLower().Contains(getAllAppointmentByLocalSearchReqDTO.PageSearch.ToLower())).Count();
                        }
                        else
                        {
                            NewSchednulinglist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 1).Count();

                        }

                        if (getAllAppointmentByLocalSearchReqDTO.AppointmentType == "Re-Scheduling")
                        {
                            ReSchedulingList = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 5 && x.PatientFirstName.ToLower().Contains(getAllAppointmentByLocalSearchReqDTO.PageSearch.ToLower())).Count();
                        }
                        else
                        {
                            ReSchedulingList = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 5).Count();

                        }

                        if (getAllAppointmentByLocalSearchReqDTO.AppointmentType == "Other")
                        {
                            Otherlist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 7 && x.PatientFirstName.ToLower().Contains(getAllAppointmentByLocalSearchReqDTO.PageSearch.ToLower())).Count();
                        }
                        else
                        {
                            Otherlist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 7).Count();

                        }

                        if (getAllAppointmentByLocalSearchReqDTO.AppointmentType == "Done")
                        {
                            Donelist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 6 && x.PatientFirstName.ToLower().Contains(getAllAppointmentByLocalSearchReqDTO.PageSearch.ToLower())).Count();
                        }
                        else
                        {
                            Donelist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 6).Count();

                        }

                    }
                    else
                    {

                        if (getAllAppointmentByLocalSearchReqDTO.AppointmentType == "New_Scheduling" || string.IsNullOrWhiteSpace(getAllAppointmentByLocalSearchReqDTO.AppointmentType))
                        {
                            NewSchednulinglist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 1 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId && x.PatientFirstName.ToLower().Contains(getAllAppointmentByLocalSearchReqDTO.PageSearch.ToLower())).Count();
                        }
                        else
                        {
                            NewSchednulinglist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 1 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId).Count();

                        }

                        if (getAllAppointmentByLocalSearchReqDTO.AppointmentType == "Re-Scheduling")
                        {
                            ReSchedulingList = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 5 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId && x.PatientFirstName.ToLower().Contains(getAllAppointmentByLocalSearchReqDTO.PageSearch.ToLower())).Count();
                        }
                        else
                        {
                            ReSchedulingList = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 5 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId).Count();

                        }

                        if (getAllAppointmentByLocalSearchReqDTO.AppointmentType == "Other")
                        {
                            Otherlist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 7 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId && x.PatientFirstName.ToLower().Contains(getAllAppointmentByLocalSearchReqDTO.PageSearch.ToLower())).Count();
                        }
                        else
                        {
                            Otherlist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 7 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId).Count();

                        }

                        if (getAllAppointmentByLocalSearchReqDTO.AppointmentType == "Done")
                        {
                            Donelist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 6 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId && x.PatientFirstName.ToLower().Contains(getAllAppointmentByLocalSearchReqDTO.PageSearch.ToLower())).Count();
                        }
                        else
                        {
                            Donelist = _dbContext.AppointmentMsts.Where(x => x.CallTypeId == 6 && x.CreatedBy == getAllAppointmentByLocalSearchReqDTO.ReceptionistId).Count();

                        }
                    }
                    getAllAppointmentResDTO.TotalCount = appoitmentlist.Count();
                    getAllAppointmentResDTO.appointmentList = appoitmentlist;
                    getAllAppointmentResDTO.TotalCount = appoitmentlist.Count;

                    getAllAppointmentResDTO.NewSchedulingCount = NewSchednulinglist;
                    getAllAppointmentResDTO.ReschedulingCount = ReSchedulingList;
                    getAllAppointmentResDTO.DoneCount = Donelist;
                    getAllAppointmentResDTO.OtherCount = Otherlist;
                    getAllAppointmentResDTO.AppointmentType = AppointmentType;

                }

                getAllAppointmentResDTO.appointmentList = appoitmentlist;


                if (appoitmentlist.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "All Appointment List";
                    commonResponse.Data = getAllAppointmentResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found";
                    commonResponse.Data = getAllAppointmentResDTO;

                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;


        }

        public CommonResponse UpdateRemarkStatus(UpdateRemarkStatusReqDTO updateRemarkStatusReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateRemarkStatusResDTO updateRemarkStatusResDTO = new UpdateRemarkStatusResDTO();
            try
            {
                var remarkStatus = _dbContext.RemarkMsts.FirstOrDefault(x => x.Id == updateRemarkStatusReqDTO.RemarkId);
                if (remarkStatus != null || updateRemarkStatusReqDTO.Status == 1 || updateRemarkStatusReqDTO.Status == 2)
                {
                    if (updateRemarkStatusReqDTO.Status == 1)
                    {
                        RemarkMst remarkMst = new RemarkMst();
                        remarkStatus.Status = 2;

                        _dbContext.RemarkMsts.Update(remarkStatus);
                        _dbContext.SaveChanges();

                        updateRemarkStatusResDTO.RemarkId = remarkStatus.Id;
                        updateRemarkStatusResDTO.Status = 2;
                    }
                    if (updateRemarkStatusReqDTO.Status == 2)
                    {
                        RemarkMst remarkMst = new RemarkMst();
                        remarkStatus.Status = 3;

                        _dbContext.RemarkMsts.Update(remarkStatus);
                        _dbContext.SaveChanges();

                        updateRemarkStatusResDTO.RemarkId = remarkStatus.Id;
                        updateRemarkStatusResDTO.Status = 3;
                    }
                    else
                    {

                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Status = true;
                        commonResponse.Data = updateRemarkStatusResDTO;
                        commonResponse.Message = "Please Enter Valid Status";
                    }

                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Status = true;
                    commonResponse.Data = updateRemarkStatusResDTO;
                    commonResponse.Message = "Remark Status Updated Successfully";

                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Status = false;
                    commonResponse.Data = updateRemarkStatusResDTO;
                    commonResponse.Message = "Remarks Not Found";

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
