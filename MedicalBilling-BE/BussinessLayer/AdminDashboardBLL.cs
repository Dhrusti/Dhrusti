using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static DTO.ResDTO.GetAllAdminAppointmentResDTO;

namespace BussinessLayer
{
    public class AdminDashboardBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;

        public AdminDashboardBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, IConfiguration configuration, AuthRepo authRepo, CommonHelper commonHelper)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _iConfiguration = configuration;
            _authRepo = authRepo;

        }

        public CommonResponse GetAllAdminAppointment(GetAllAdminAppointmentReqDTO getAllAdminAppointmentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            GetAllAdminAppointmentResDTO getAllAdminAppointmentResDTO = new GetAllAdminAppointmentResDTO();
            AdminAppointmentList adminappointmentList = new AdminAppointmentList();
            try
            {
                if (getAllAdminAppointmentReqDTO.UserId != null)
                {
                    var appointment = _commonRepo.getAllAppointment().Where(x => x.CallTypeId == 6).ToList();
                   // var approvalStatus = _commonRepo.getAllNotification().FirstOrDefault(x => x.CreatedBy == getAllAdminAppointmentReqDTO.UserId);
                    List<AdminAppointmentList> appoitmentlist = (from u in _dbContext.AppointmentMsts.Where(x =>x.CallTypeId == 6)
                                                                 join l in _dbContext.CallTypeMsts
                                                                on u.CallTypeId equals l.Id
                                                                 join E in _commonRepo.getAllPhysician()
                                                                 on u.AppDoctorId equals E.Id
                                                                 join f in _commonRepo.getAllNotification().Where(x => x.ApprovalStatus != null)
                                                               on u.Id equals f.CreatedBy
                                                                 select new { u, E, l,f }).ToList().Select((x, index) => new AdminAppointmentList
                                                                 {
                                                                     SR = x.u.Id,
                                                                     AccountNo = x.u.AccountNo,
                                                                     PatientName = x.u.PatientFirstName,
                                                                     PrimaryInsuranceName = x.u.PrimaryInsuranceName,
                                                                     SecondaryInsuranceName = x.u.SecondaryInsuranceName,
                                                                     Status = _dbContext.PatientEmailMsts.FirstOrDefault(y => y.SenderId == x.u.Id) != null ? true : false,
                                                                     ApptTime = x.u.ActualAppoitmentDate?.ToString("MM/dd/yyyy hh:mm tt"),
                                                                     EntryTime = x.u.CreatedDate.ToString("MM/dd/yyyy hh:mm tt"),
                                                                     DoName = x.E.DoctorFirstName + " " + x.E.DoctorLastName,
                                                                     Notes = x.u.Notes,
                                                                     Email = _dbContext.PatientEmailMsts.FirstOrDefault(y => y.SenderId == x.u.Id) != null ? true : false,
                                                                     Remark = _dbContext.RemarkMsts.FirstOrDefault(i => i.AppointmentId == x.u.Id) != null ? true : false,
                                                                     IsApprove = x.f.ApprovalStatus =="Approve" ? true : false,
                                                                     IsReject = x.f.ApprovalStatus == "Reject" ? true : false,
                                                                     ApprovalStatus = x.f.ApprovalStatus,
                                                                     ReceptionistId = x.u.CreatedBy,
                                                                     UserId = x.u.Id,
                                                                     CallType = "Done",
                                                                     UpdatedDate = x.u.UpdatedDate,
                                                                     PatientEmail = x.u.PatientEmail,
                                                                     AdminId = 1
                                                                 }).ToList();

                    appoitmentlist = appoitmentlist.OrderByDescending(x => x.UpdatedDate).ToList();

                    getAllAdminAppointmentResDTO.adminAppointmentList = appoitmentlist;
                    getAllAdminAppointmentResDTO.TotalCount = appointment.Count();

                //    var appointment1 = _dbContext.AppointmentMsts.FirstOrDefault(x =>x.Id == )

                    if (appoitmentlist.Count > 0)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "All Appointment List";
                        commonResponse.Data = getAllAdminAppointmentResDTO;
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Data Not Found";
                        commonResponse.Data = getAllAdminAppointmentResDTO;

                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found";
                }


            }
            catch (Exception ex)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateApprovalStatus(UpdateApprovalStatusReqDTO updateApprovalStatusReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateApprovalStatusResDTO  updateApprovalStatusResDTO = new UpdateApprovalStatusResDTO();
            try
            {
                if (updateApprovalStatusReqDTO != null)
                {
                    var notificationlist = _commonRepo.getAllNotification().OrderBy(x =>x.Id).LastOrDefault(x => x.CreatedBy == updateApprovalStatusReqDTO.PatientId);
                    notificationlist.ApprovalStatus = updateApprovalStatusReqDTO.Status;

                    NotificationMst notificationMst = new NotificationMst();
                    notificationlist.SenderId = updateApprovalStatusReqDTO.AdminId;
                    notificationlist.ReceiverId = updateApprovalStatusReqDTO.ReceptionistId;
                    notificationlist.ApprovalStatus = updateApprovalStatusReqDTO.Status;
                    notificationlist.Description = updateApprovalStatusReqDTO.Status == "Approve"? " Your Id"+" "+updateApprovalStatusReqDTO.PatientId +" "+" request has been approved" : "Your Id"+ " "+ updateApprovalStatusReqDTO.PatientId +" " +"request has been Rejected" ;
                    notificationlist.DescriptionTitle = updateApprovalStatusReqDTO.Status == "Approve" ? "Approve request Notification from Admin" : "Reject request Notification from Admin";
                    notificationlist.IsNotificationRead = false;
                    notificationlist.CreatedDate = DateTime.Now;
                    notificationlist.CreatedBy = updateApprovalStatusReqDTO.PatientId;
                    notificationlist.UpdatedDate = DateTime.Now;
                    notificationlist.UpdatedBy = updateApprovalStatusReqDTO.PatientId;
                  

                    _dbContext.NotificationMsts.Update(notificationlist);
                    var result = _dbContext.SaveChanges();
                    var notificationlist1 = _commonRepo.getAllNotification().OrderBy(x => x.Id).LastOrDefault(x => x.CreatedBy == updateApprovalStatusReqDTO.PatientId );
                    //&& x.ApprovalStatus == "Reject" 

                    var patient = _dbContext.AppointmentMsts.FirstOrDefault(x => x.Id == notificationlist1.CreatedBy);

                    if (notificationlist1.ApprovalStatus == "Reject")
                    { 
                        patient.IsEditable = false;

                        _dbContext.AppointmentMsts.Update(patient);
                        _dbContext.SaveChanges();
                    }

                    if (notificationlist1.ApprovalStatus == "Approve")
                    {
                        patient.IsEditable = true;

                        _dbContext.AppointmentMsts.Update(patient);
                        _dbContext.SaveChanges();
                    }

                    updateApprovalStatusResDTO.SenderId = updateApprovalStatusReqDTO.AdminId;
                    updateApprovalStatusResDTO.Status = updateApprovalStatusReqDTO.Status;
                    updateApprovalStatusResDTO.Description = "Id "+notificationlist.CreatedBy + notificationlist.Description +" "+"And Notification Send Successfully";

                    if (result != null)
                    {
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Appointment Status Changed Successfully";
                        commonResponse.Data = updateApprovalStatusResDTO;
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Fail";
                        commonResponse.Data = updateApprovalStatusResDTO;
                    }

                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data Not Found";
                    commonResponse.Data = updateApprovalStatusResDTO;
                }

            }
            catch (Exception ex)
            {
                commonResponse.Data = ex;
                commonResponse.Message = "Exception";
            }
            return commonResponse;
        }
    }
}
