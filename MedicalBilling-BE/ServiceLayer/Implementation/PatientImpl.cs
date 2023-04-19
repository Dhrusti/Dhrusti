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
    public class PatientImpl : IPatient
    {
        private readonly PatientBLL _patientBLL;
        public PatientImpl(PatientBLL patientBLL)
        {
            _patientBLL = patientBLL;
        }

        public CommonResponse GetAllAppointment(GetAllAppointmentReqDTO getAllAppointmentReqDTO)
        {
            return _patientBLL.GetAllAppointment(getAllAppointmentReqDTO);
        }

        public async Task<CommonResponse> GenerateAppointmentNumber()
        {
            return await _patientBLL.GenerateAppointmentNumber();
        }

        public CommonResponse AddAppoitment(AddAppoitmentReqDTO addAppoitmentReqDTO)
        {
            return _patientBLL.AddAppoitment(addAppoitmentReqDTO);
        }
        public CommonResponse AddRemark(AddRemarkReqDTO addRemarkReqDTO)
        {
            return _patientBLL.AddRemark(addRemarkReqDTO);
        }


        public async Task<CommonResponse> GetRemarksByAppointmentNo(RemarkReqDTO remarkReqDTO)
        {
            return await _patientBLL.GetRemarksByAppointmentNo(remarkReqDTO);
        }

        //public CommonResponse AddRemarkStatus(AddRemarkStatusReqDTO addRemarkStatusReqDTO)
        //{
        //    return _patientBLL.AddRemarkStatus(addRemarkStatusReqDTO);
        // }
        public CommonResponse GetAllAppointmentbyId(GetAllAppointmentbyIdReqDTO getAllAppointmentReqDTO)
        {
            return _patientBLL.GetAllAppointmentbyId(getAllAppointmentReqDTO);
        }

        public CommonResponse EditAppointment(EditAppointmentReqDTO editAppointmentReqDTO)
        {
            return _patientBLL.EditAppointment(editAppointmentReqDTO);
        }

        public CommonResponse GetAllCallTypeCount()
        {
            return _patientBLL.GetAllCallTypeCount();
        }
        public CommonResponse GetAllAppointmentByLocalSearch(GetAllAppointmentByLocalSearchReqDTO getAllAppointmentByLocalSearchReqDTO)
        {
            return _patientBLL.GetAllAppointmentByLocalSearch(getAllAppointmentByLocalSearchReqDTO);
        }
        public CommonResponse UpdateRemarkStatus(UpdateRemarkStatusReqDTO updateRemarkStatusReqDTO)
        { 
            return _patientBLL.UpdateRemarkStatus(updateRemarkStatusReqDTO);
        }

    }
}

