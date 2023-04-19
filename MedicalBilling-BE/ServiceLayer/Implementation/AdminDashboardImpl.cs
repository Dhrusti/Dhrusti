using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class AdminDashboardImpl : IAdminDashboard
    {
        private readonly AdminDashboardBLL _adminDashboardBLL;

        public AdminDashboardImpl(AdminDashboardBLL adminDashboardBLL)
        {
            _adminDashboardBLL = adminDashboardBLL;
        }

        public CommonResponse GetAllAdminAppointment(GetAllAdminAppointmentReqDTO getAllAdminAppointmentReqDTO)
        {
            return _adminDashboardBLL.GetAllAdminAppointment(getAllAdminAppointmentReqDTO);
        }
        public CommonResponse UpdateApprovalStatus(UpdateApprovalStatusReqDTO updateApprovalStatusReqDTO)
        {
            return _adminDashboardBLL.UpdateApprovalStatus(updateApprovalStatusReqDTO);
        }
    }
}
