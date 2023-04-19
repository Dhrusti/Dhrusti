using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Microsoft.EntityFrameworkCore;
using static DTO.ResDTO.GetAllAdminAppointmentResDTO;

namespace BussinessLayer
{
	public class AdminDashboardBLL
	{
		private readonly CommonRepo _commonRepo;
		public AdminDashboardBLL(CommonRepo commonRepo)
		{
			_commonRepo = commonRepo;
		}
		public async Task<CommonResponse> GetAllAdminAppointmentAsync(GetAllAdminAppointmentReqDTO getAllAdminAppointmentReqDTO)
		{
			CommonResponse commonResponse = new CommonResponse();
			GetAllAdminAppointmentResDTO getAllAdminAppointmentResDTO = new GetAllAdminAppointmentResDTO();
			AdminAppointment adminAppointment = new AdminAppointment();
			try
			{
				if (getAllAdminAppointmentReqDTO.Id != 0)
				{
					//int appointmentCount = await _commonRepo.AppointmentMstList().Where(x => x.CallTypeId == CommonConstant.CallTypeId).CountAsync();
					var appointmentData = await _commonRepo.AppointmentMstList().Where(x => x.CallTypeId == CommonConstant.CallTypeId).ToListAsync();
					var physicianData = _commonRepo.PhysicianMstList();
					var notificationData = _commonRepo.NotificationMstList(); 
					var patientData = _commonRepo.PatientEmailMstList();
					var remarkData = _commonRepo.RemarkMstList();

					PhysicianMst physician = null;
					NotificationMst notification = null;
					bool patient = false;
					bool remark = false;

					getAllAdminAppointmentResDTO.AdminAppointmentList = new List<AdminAppointment>();
					foreach (var item in appointmentData)
					{
						physician = await physicianData.FirstOrDefaultAsync(x => x.Id == item.AppDoctorId) ?? new PhysicianMst();
						notification = await notificationData.FirstOrDefaultAsync(x => x.CreatedBy == item.Id) ?? new NotificationMst(); 
						patient = await patientData.AnyAsync(y => y.SenderId == item.Id);
						remark = await remarkData.AnyAsync(y => y.AppointmentId == item.Id);

						adminAppointment.SR = item.Id;
						adminAppointment.AccountNo = item.AccountNo;
						adminAppointment.PatientName = $"{item.PatientFirstName} {item.PatientLastName}";
						adminAppointment.PrimaryInsuranceName = item.PrimaryInsuranceName;
						adminAppointment.SecondaryInsuranceName = item.SecondaryInsuranceName;
						adminAppointment.Status = item.Status;
						adminAppointment.ApptTime = item.ActualAppoitmentDate?.ToString("MM/dd/yyyy hh:mm tt");
						adminAppointment.EntryTime = item.CreatedDate.ToString("MM/dd/yyyy hh:mm tt");
						adminAppointment.DoName = $"{physician.DoctorFirstName} {physician.DoctorLastName}";
						adminAppointment.Notes = item.Notes;
						adminAppointment.Email = patient;
						adminAppointment.Remark = remark;
						adminAppointment.DOB = item.PatientDob;
						adminAppointment.IsApprove = notification.ApprovalStatus == CommonConstant.Approve; 
						adminAppointment.IsReject = notification.ApprovalStatus == CommonConstant.Reject;
						adminAppointment.ApprovalStatus = notification.ApprovalStatus;
						adminAppointment.ReceptionistId = item.CreatedBy;
						adminAppointment.UserId = item.Id;
						adminAppointment.CallType = CommonConstant.Done;
						adminAppointment.UpdatedDate = item.UpdatedDate;
						adminAppointment.PatientEmail = item.PatientEmail;

						getAllAdminAppointmentResDTO.AdminAppointmentList.Add(adminAppointment);
					}

					appointmentData = appointmentData.OrderByDescending(x => x.UpdatedDate).ToList();
					getAllAdminAppointmentResDTO.TotalCount = appointmentData.Count;
					if (appointmentData.Count > 0)
					{
						commonResponse.Status = true;
						commonResponse.StatusCode = HttpStatusCode.OK;
						commonResponse.Message = CommonConstant.Data_Found_Successfully;
						commonResponse.Data = getAllAdminAppointmentResDTO;
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
					commonResponse.Message = CommonConstant.Data_Not_Found;
				}
			}
			catch { throw; }
			return commonResponse;
		}
	}
}
