using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IAdminDashboard
    {
        public CommonResponse GetAllAdminAppointment(GetAllAdminAppointmentReqDTO getAllAdminAppointmentReqDTO);
        public CommonResponse UpdateApprovalStatus(UpdateApprovalStatusReqDTO updateApprovalStatusReqDTO);
    }
}
