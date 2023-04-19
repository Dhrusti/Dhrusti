using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IPatient
    {
        public CommonResponse GetAllAppointment(GetAllAppointmentReqDTO getAllAppointmentReqDTO);
        public Task<CommonResponse> GenerateAppointmentNumber();
        public CommonResponse AddAppoitment(AddAppoitmentReqDTO addAppoitmentReqDTO);
        public CommonResponse AddRemark(AddRemarkReqDTO addRemarkReqDTO);
        public Task<CommonResponse> GetRemarksByAppointmentNo(RemarkReqDTO remarkReqDTO);

        //public CommonResponse AddRemarkStatus(AddRemarkStatusReqDTO addRemarkStatusReqDTO);
        public CommonResponse GetAllAppointmentbyId(GetAllAppointmentbyIdReqDTO getAllAppointmentReqDTO);
        public CommonResponse EditAppointment(EditAppointmentReqDTO editAppointmentReqDTO);

        public CommonResponse GetAllCallTypeCount();
        public CommonResponse GetAllAppointmentByLocalSearch(GetAllAppointmentByLocalSearchReqDTO getAllAppointmentByLocalSearchReqdto);

        public CommonResponse UpdateRemarkStatus(UpdateRemarkStatusReqDTO updateRemarkStatusReqDTO);

    }
}
