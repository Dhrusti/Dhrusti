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
using static DTO.ResDTO.GetAllNotificationResDTO;
using static DTO.ResDTO.GetAllReceptionistNotificationResDTO;

namespace BussinessLayer
{
    public class NotificationBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;
        private readonly ClientBLL _clientBLL;
        private readonly DoctorBLL _doctorBLL;

        public NotificationBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, IConfiguration configuration, AuthRepo authRepo, CommonHelper commonHelper, ClientBLL clientBLL, DoctorBLL doctorBLL)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _iConfiguration = configuration;
            _authRepo = authRepo;
            _clientBLL = clientBLL;
            _doctorBLL = doctorBLL;
        }

        public CommonResponse SendNotification(SendNotificationReqDTO sendNotificationReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                SendNotificationResDTO sendNotificationResDTO = new SendNotificationResDTO();
                var loginDetail = _dbContext.UserMsts.FirstOrDefault(x => x.Id == sendNotificationReqDTO.SenderId);
                var IsPatientExist = _commonRepo.getAllAppointment().OrderBy(x =>x.Id).LastOrDefault(x => x.Id == sendNotificationReqDTO.CreatedBy);
                var notificationexist = _commonRepo.getAllNotification().OrderBy(x => x.Id).LastOrDefault(x => x.CreatedBy == sendNotificationReqDTO.CreatedBy );
                

                if (notificationexist != null)
                {
                    //var notification12 = notificationexist.ApprovalStatus == "Pending" || notificationexist.ApprovalStatus == "Reject"? true : false;

                    var notification12 = notificationexist.ApprovalStatus == "Pending" ? true : false;

                    if (notification12 == true)
                    {
                        commonResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        commonResponse.Status = false;
                        commonResponse.Message = "Notification has been already sent";

                    }
                    else
                    {


                        if (IsPatientExist != null)
                        {

                            var SenderExist = _dbContext.UserMsts.FirstOrDefault(x => x.Id == sendNotificationReqDTO.SenderId);
                            NotificationMst notification = new NotificationMst();
                            notification.SenderId = sendNotificationReqDTO.SenderId;
                            notification.ReceiverId = sendNotificationReqDTO.ReceiverId;
                            notification.Description = "Send Request Successfully for Editing Appointment Details";
                            notification.AdminDescription = "You have received notification from the" + " " + loginDetail.FirstName + loginDetail.LastName + " " + "for Editing Appointment Details for " + IsPatientExist.PatientFirstName;
                            notification.DescriptionTitle = "Appointment Status Notification";
                            notification.AdminDescriptionTitle = "You have Received request Notification";
                            notification.IsNotificationRead = false;
                            notification.CreatedBy = sendNotificationReqDTO.CreatedBy;
                            notification.UpdatedBy = sendNotificationReqDTO.CreatedBy;
                            notification.CreatedDate = DateTime.Now;
                            notification.UpdatedDate = DateTime.Now;
                            notification.ApprovalStatus = "Pending";
                            _dbContext.NotificationMsts.Add(notification);
                            _dbContext.SaveChanges();

                            sendNotificationResDTO.SenderId = sendNotificationReqDTO.SenderId;
                            sendNotificationResDTO.ReceiverId = sendNotificationReqDTO.ReceiverId;
                            sendNotificationResDTO.Description = notification.Description;

                            commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                            commonResponse.Status = true;
                            commonResponse.Message = "Request to Edit Appointment";
                            commonResponse.Data = sendNotificationResDTO;

                        }
                        else
                        {
                            commonResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                            commonResponse.Status = false;
                            commonResponse.Message = "Fail";

                        }
                        //if (IsPatientExist != null)
                        //{

                        //    var SenderExist = _dbContext.UserMsts.FirstOrDefault(x => x.Id == sendNotificationReqDTO.SenderId);
                        //    NotificationMst notification = new NotificationMst();
                        //    notification.SenderId = sendNotificationReqDTO.SenderId;
                        //    notification.ReceiverId = sendNotificationReqDTO.ReceiverId;
                        //    notification.Description = "Send Request Successfully for Editing Appointment Details";
                        //    notification.AdminDescription = "You have received notification from the" + " " + loginDetail.FirstName + loginDetail.LastName + " " + "for Editing Appointment Details for " + IsPatientExist.PatientFirstName;
                        //    notification.AdminDescriptionTitle = "You have Received request Notification";
                        //    notification.DescriptionTitle = "Appointment Status Notification";
                        //    notification.IsNotificationRead = false;
                        //    notification.CreatedBy = sendNotificationReqDTO.CreatedBy;
                        //    notification.UpdatedBy = sendNotificationReqDTO.CreatedBy;
                        //    notification.CreatedDate = DateTime.Now;
                        //    notification.UpdatedDate = DateTime.Now;
                        //    notification.ApprovalStatus = "Pending";
                        //    _dbContext.NotificationMsts.Add(notification);
                        //    _dbContext.SaveChanges();

                        //    sendNotificationResDTO.SenderId = sendNotificationReqDTO.SenderId;
                        //    sendNotificationResDTO.ReceiverId = sendNotificationReqDTO.ReceiverId;
                        //    sendNotificationResDTO.Description = notification.Description;

                        //    commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                        //    commonResponse.Status = true;
                        //    commonResponse.Message = "Request to Edit Appointment";
                        //    commonResponse.Data = sendNotificationResDTO;

                        //}
                        //else
                        //{
                        //    commonResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                        //    commonResponse.Status = false;
                        //    commonResponse.Message = "Fail";

                        //}
                    }
                }
                else
                {
                    if (IsPatientExist != null)
                    {

                        var SenderExist = _dbContext.UserMsts.FirstOrDefault(x => x.Id == sendNotificationReqDTO.SenderId);
                        NotificationMst notification = new NotificationMst();
                        notification.SenderId = sendNotificationReqDTO.SenderId;
                        notification.ReceiverId = sendNotificationReqDTO.ReceiverId;
                        notification.Description = "Send Request Successfully for Editing Appointment Details";
                        notification.AdminDescription = "You have received notification from the" + " " + loginDetail.FirstName + loginDetail.LastName + " " + "for Editing Appointment Details for " + IsPatientExist.PatientFirstName;
                        notification.DescriptionTitle = "Appointment Status Notification";
                        notification.AdminDescriptionTitle = "You have Received request Notification";
                        notification.IsNotificationRead = false;
                        notification.CreatedBy = sendNotificationReqDTO.CreatedBy;
                        notification.UpdatedBy = sendNotificationReqDTO.CreatedBy;
                        notification.CreatedDate = DateTime.Now;
                        notification.UpdatedDate = DateTime.Now;
                        notification.ApprovalStatus = "Pending";
                        _dbContext.NotificationMsts.Add(notification);
                        _dbContext.SaveChanges();

                        sendNotificationResDTO.SenderId = sendNotificationReqDTO.SenderId;
                        sendNotificationResDTO.ReceiverId = sendNotificationReqDTO.ReceiverId;
                        sendNotificationResDTO.Description = notification.Description;

                        commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                        commonResponse.Status = true;
                        commonResponse.Message = "Request to Edit Appointment";
                        commonResponse.Data = sendNotificationResDTO;

                    }
                    else
                    {
                        commonResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                        commonResponse.Status = false;
                        commonResponse.Message = "Fail";

                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllNotification(GetAllNotificationReqDTO getAllNotificationReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();

            try
            {
                var notificationlist = _dbContext.NotificationMsts.Where(x => x.ReceiverId == getAllNotificationReqDTO.UserId).ToList();
                GetAllNotificationResDTO getAllNotificationResDTO = new GetAllNotificationResDTO();
                var user = _commonRepo.getUserList_Login().FirstOrDefault(x => x.Id == getAllNotificationReqDTO.UserId);
                var role = _dbContext.RoleMsts.FirstOrDefault(x => x.Id == user.Role);
                if (role.RoleName == "Admin")
                {
                    if (notificationlist != null)
                    {
                        var notification = _commonRepo.getAllNotification().FirstOrDefault(x => x.ReceiverId == getAllNotificationReqDTO.UserId);

                        List<AllNotificationList> allNotificationLists = (from e in _commonRepo.getAllNotification().Where(x => x.ReceiverId == getAllNotificationReqDTO.UserId && x.AdminDescription != null)
                                                                          join i in _commonRepo.getAllUsers()
                                                                          on e.ReceiverId equals i.Id
                                                                          select new { e, i }).
                                                     Select(x => new AllNotificationList
                                                     {
                                                         NotificationId = x.e.Id,
                                                         Description = x.e.AdminDescription,
                                                         IsRead = x.e.IsNotificationRead,
                                                         Title = x.e.AdminDescriptionTitle
                                                     }).ToList();

                        allNotificationLists = allNotificationLists
                               .OrderByDescending(x => x.NotificationId)
                               .ToList();

                        getAllNotificationResDTO.allNotificationList = allNotificationLists;
                        getAllNotificationResDTO.TotalCount = allNotificationLists.Where(x => x.IsRead == false).Count();

                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = getAllNotificationResDTO;
                        commonResponse.Message = "GetAll Notification Successfully";
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Fail";
                    }
                }
                else
                {
                    if (notificationlist != null)
                    {
                        var notification = _commonRepo.getAllNotification().FirstOrDefault(x => x.ReceiverId == getAllNotificationReqDTO.UserId);

                        List<AllNotificationList> allNotificationLists = (from e in _commonRepo.getAllNotification().Where(x => x.ReceiverId == getAllNotificationReqDTO.UserId && x.Description != null)
                                                                          join i in _commonRepo.getAllUsers()
                                                                          on e.ReceiverId equals i.Id
                                                                          select new { e, i }).
                                                     Select(x => new AllNotificationList
                                                     {
                                                         NotificationId = x.e.Id,
                                                         Description = x.e.Description,
                                                         IsRead = x.e.IsNotificationRead,
                                                         Title = x.e.DescriptionTitle
                                                     }).ToList();

                        allNotificationLists = allNotificationLists
                               .OrderByDescending(x => x.NotificationId)
                               .ToList();


                        getAllNotificationResDTO.allNotificationList = allNotificationLists;
                        getAllNotificationResDTO.TotalCount = allNotificationLists.Where(x => x.IsRead == false).Count();

                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = getAllNotificationResDTO;
                        commonResponse.Message = "GetAll Notification Successfully";

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Fail";

                    }

                }


            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateNotification(UpdateNotificationReqDTO updateNotificationReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateNotificationResDTO updateNotificationResDTO = new UpdateNotificationResDTO();
            try
            {
                if (updateNotificationReqDTO != null)
                {
                    var notificationList = _commonRepo.getAllNotification().FirstOrDefault(x => x.Id == updateNotificationReqDTO.NotificationId);
                    if (notificationList != null)
                    {
                        NotificationMst notificationMst = new NotificationMst();
                        notificationList.IsNotificationRead = true;
                        notificationList.UpdatedDate = DateTime.Now;
                        notificationList.UpdatedBy = updateNotificationReqDTO.UserId;

                        _dbContext.NotificationMsts.Update(notificationList);
                        _dbContext.SaveChanges();

                        updateNotificationResDTO.NotificationId = updateNotificationReqDTO.NotificationId;
                        updateNotificationResDTO.IsReadable = notificationList.IsNotificationRead;

                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = updateNotificationResDTO;
                        commonResponse.Message = "Updated Notification Successfully";

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Data = updateNotificationResDTO;
                        commonResponse.Message = "Fail";

                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Data = updateNotificationResDTO;
                    commonResponse.Message = "Please Enter Valid Data";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse GetAllReceptionistNotification(GetAllReceptionistNotificationReqDTO getAllReceptionistNotificationReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var notificationlist = _dbContext.NotificationMsts.Where(x => x.ReceiverId == getAllReceptionistNotificationReqDTO.UserId).ToList();
                GetAllReceptionistNotificationResDTO getAllReceptionistNotificationResDTO = new GetAllReceptionistNotificationResDTO();
                if (notificationlist != null)
                {
                    var notification = _commonRepo.getAllNotification().FirstOrDefault(x => x.ReceiverId == getAllReceptionistNotificationReqDTO.UserId);

                    List<ReceptionistNotificationList> allNotificationLists = (from e in _commonRepo.getAllNotification().Where(x => x.ReceiverId == getAllReceptionistNotificationReqDTO.UserId)
                                                                               join i in _commonRepo.getAllUsers()
                                                                               on e.ReceiverId equals i.Id
                                                                               select new { e, i }).
                                                 Select(x => new ReceptionistNotificationList
                                                 {
                                                     NotificationId = x.e.Id,
                                                     Description = x.e.Description,
                                                     IsRead = x.e.IsNotificationRead,
                                                     Title = x.e.DescriptionTitle

                                                 }).ToList();

                    allNotificationLists = allNotificationLists
                                            .OrderByDescending(x => x.NotificationId)
                                            .ToList();

                    getAllReceptionistNotificationResDTO.receptionistNotificationList = allNotificationLists;
                    getAllReceptionistNotificationResDTO.TotalCount = allNotificationLists.Count();

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getAllReceptionistNotificationResDTO;
                    commonResponse.Message = "GetAll Notification Successfully";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Fail";

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
