using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Microsoft.EntityFrameworkCore;
using static DTO.ResDTO.GetAllNotificationResDTO;
using static DTO.ResDTO.GetAllReceptionistNotificationResDTO;

namespace BussinessLayer
{
	public class NotificationBLL
	{
		private readonly CommonRepo _commonRepo;
		private readonly RevenueCycleDbContext _dbContext;
		private readonly CommonHelper _commonHelper;
		public NotificationBLL(CommonRepo commonRepo, RevenueCycleDbContext dbContext, CommonHelper commonHelper)
		{
			_commonRepo = commonRepo;
			_dbContext = dbContext;
			_commonHelper = commonHelper;
		}
		public async Task<CommonResponse> SendNotificationAsync(SendNotificationReqDTO sendNotificationReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				SendNotificationResDTO sendNotificationResDTO = new SendNotificationResDTO();
				var loginUsers = await _commonRepo.UserMstList().FirstOrDefaultAsync(x => x.Id == sendNotificationReqDTO.SenderId);
				var IsPatientExist = await _commonRepo.AppointmentMstList().OrderBy(x => x.Id).LastOrDefaultAsync(x => x.Id == sendNotificationReqDTO.CreatedBy);
				var notificationExist = await _commonRepo.NotificationMstList().OrderBy(x => x.Id).LastOrDefaultAsync(x => x.CreatedBy == sendNotificationReqDTO.CreatedBy);
				bool IsABC = true;

				if (notificationExist != null)
				{
					var notification12 = notificationExist.ApprovalStatus == CommonConstant.Pending ? true : false;
					if (notification12 == true)
					{
						IsABC = false;
						commonResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
						commonResponse.Status = false;
						commonResponse.Message = CommonConstant.Notification_Has_Been_Already_Sent;
					}
				}
				if (IsABC)
				{
					if (IsPatientExist != null)
					{
						var SenderExist = await _commonRepo.UserMstList().FirstOrDefaultAsync(x => x.Id == sendNotificationReqDTO.SenderId);
						NotificationMst notification = new NotificationMst();
						notification.SenderId = sendNotificationReqDTO.SenderId;
						notification.ReceiverId = sendNotificationReqDTO.ReceiverId;
						notification.Description = CommonConstant.Send_Request_Successfully_For_Editing_Appointment_Details;
						notification.AdminDescription = "You have received notification from the" + " " + loginUsers?.FirstName + loginUsers?.LastName + " " + "for Editing Appointment Details for " + IsPatientExist.PatientFirstName;
						notification.DescriptionTitle = CommonConstant.Appointment_Status_Notification;
						notification.AdminDescriptionTitle = CommonConstant.You_Have_Received_Request_Notification;
						notification.IsNotificationRead = false;
						notification.CreatedBy = sendNotificationReqDTO.CreatedBy;
						notification.UpdatedBy = sendNotificationReqDTO.CreatedBy;
						notification.CreatedDate = DateTime.Now;
						notification.UpdatedDate = DateTime.Now;
						notification.ApprovalStatus = CommonConstant.Pending;
						_dbContext.NotificationMsts.Add(notification);
						_dbContext.SaveChanges();

						sendNotificationResDTO.SenderId = sendNotificationReqDTO.SenderId;
						sendNotificationResDTO.ReceiverId = sendNotificationReqDTO.ReceiverId;
						sendNotificationResDTO.Description = notification.Description;

						commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
						commonResponse.Status = true;
						commonResponse.Message = CommonConstant.Request_To_Edit_Appointment;
						commonResponse.Data = sendNotificationResDTO;
					}
					else
					{
						commonResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
						commonResponse.Status = false;
						commonResponse.Message = CommonConstant.Request_To_Edit_Appointment_Fail;
					}
				}
			}
			catch (Exception) { throw; }
			return commonResponse;
		}

		public async Task<CommonResponse> GetAllAdminNotificationAsync(GetAllNotificationReqDTO getAllNotificationReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var notificationlist = await _commonRepo.NotificationMstList().Where(x => x.ReceiverId == getAllNotificationReqDTO.UserId).ToListAsync();
				GetAllNotificationResDTO getAllNotificationResDTO = new GetAllNotificationResDTO();
				var user = await _commonRepo.UserMstList().FirstOrDefaultAsync(x => x.Id == getAllNotificationReqDTO.UserId) ?? new UserMst();
				var role = await _commonRepo.RoleMstList().FirstOrDefaultAsync(x => x.Id == user.Role) ?? new RoleMst();

				var allNotificationLists = _commonRepo.NotificationMstList().Where(x => x.ReceiverId == getAllNotificationReqDTO.UserId).AsQueryable();

				if (role.RoleName == CommonConstant.Admin)
				{
					allNotificationLists = allNotificationLists.Where(x => x.AdminDescription != null).AsQueryable();
				}
				else
				{
					allNotificationLists = allNotificationLists.Where(x => x.Description != null).AsQueryable();
				}

				if (notificationlist != null)
				{
					var notification = await _commonRepo.NotificationMstList().FirstOrDefaultAsync(x => x.ReceiverId == getAllNotificationReqDTO.UserId);

					List<AllNotificationList> allNotification = await (from e in allNotificationLists
																	   join i in _commonRepo.UserMstList()
																	   on e.ReceiverId equals i.Id
																	   select new AllNotificationList
																	   {
																		   NotificationId = e.Id,
																		   Description = role.RoleName == CommonConstant.Admin ? e.AdminDescription : e.Description,
																		   IsRead = e.IsNotificationRead,
																		   Title = role.RoleName == CommonConstant.Admin ? e.AdminDescriptionTitle : e.DescriptionTitle,
																	   }).ToListAsync();

					allNotification = allNotification
						   .OrderByDescending(x => x.NotificationId)
						   .ToList();

					getAllNotificationResDTO.allNotificationList = allNotification;
					getAllNotificationResDTO.TotalCount = allNotification.Where(x => x.IsRead == false).Count();

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = getAllNotificationResDTO;
					commonResponse.Message = CommonConstant.Data_Found_Successfully;
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = CommonConstant.Data_Not_Found;
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public async Task<CommonResponse> UpdateNotificationAsync(UpdateNotificationReqDTO updateNotificationReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			UpdateNotificationResDTO updateNotificationResDTO = new UpdateNotificationResDTO();
			try
			{
				if (updateNotificationReqDTO != null)
				{
					var notificationList = await _commonRepo.NotificationMstList().FirstOrDefaultAsync(x => x.Id == updateNotificationReqDTO.NotificationId);
					if (notificationList != null)
					{
						notificationList.IsNotificationRead = true;
						notificationList.UpdatedDate = _commonHelper.GetCurrentDateTime();
						notificationList.UpdatedBy = updateNotificationReqDTO.UserId;

						_dbContext.Entry(notificationList).State = EntityState.Modified;
						await _dbContext.SaveChangesAsync();

						updateNotificationResDTO.NotificationId = updateNotificationReqDTO.NotificationId;
						updateNotificationResDTO.IsReadable = notificationList.IsNotificationRead;

						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Data = updateNotificationResDTO;
						commonResponse.Message = CommonConstant.Updated_Notification_Successfully;

					}
					else
					{
						commonResponse.StatusCode = HttpStatusCode.NotFound;
						commonResponse.Message = CommonConstant.Data_Not_Found;
					}
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = CommonConstant.Please_Enter_Valid_Data;
				}
			}
			catch { throw; }
			return commonResponse;
		}

		public async Task<CommonResponse> GetAllReceptionistNotificationAsync(GetAllReceptionistNotificationReqDTO getAllReceptionistNotificationReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			try
			{
				var notificationlist = await _commonRepo.NotificationMstList().AnyAsync(x => x.ReceiverId == getAllReceptionistNotificationReqDTO.UserId);
				GetAllReceptionistNotificationResDTO getAllReceptionistNotificationResDTO = new GetAllReceptionistNotificationResDTO();
				if (notificationlist)
				{
					var notification = await _commonRepo.NotificationMstList().FirstOrDefaultAsync(x => x.ReceiverId == getAllReceptionistNotificationReqDTO.UserId);
					List<ReceptionistNotificationList> allNotificationLists = await (from e in _commonRepo.NotificationMstList().Where(x => x.ReceiverId == getAllReceptionistNotificationReqDTO.UserId)
																					 join i in _commonRepo.UserMstList()
																					 on e.ReceiverId equals i.Id
																					 select new ReceptionistNotificationList
																					 {
																						 NotificationId = e.Id,
																						 Description = e.Description,
																						 IsRead = e.IsNotificationRead,
																						 Title = e.DescriptionTitle
																					 }).ToListAsync();
					allNotificationLists = allNotificationLists.OrderByDescending(x => x.NotificationId).ToList();
					getAllReceptionistNotificationResDTO.receptionistNotificationList = allNotificationLists;
					getAllReceptionistNotificationResDTO.TotalCount = allNotificationLists.Count();

					commonResponse.Status = true;
					commonResponse.StatusCode = HttpStatusCode.OK;
					commonResponse.Data = getAllReceptionistNotificationResDTO;
					commonResponse.Message = CommonConstant.Data_Found_Successfully;
				}
				else
				{
					commonResponse.StatusCode = HttpStatusCode.NotFound;
					commonResponse.Message = CommonConstant.Data_Not_Found;
				}
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
