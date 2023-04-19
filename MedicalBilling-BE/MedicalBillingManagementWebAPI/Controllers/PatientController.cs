using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ReqViewModel;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatient _patient;

        public PatientController(IPatient patient)
        {
            _patient = patient;
        }


        [HttpPost("GetAllAppointment")]
        public CommonResponse GetAllAppointment(GetAllAppointmentReqViewModel getAllAppointmentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _patient.GetAllAppointment(getAllAppointmentReqDTO.Adapt<GetAllAppointmentReqDTO>());
                GetAllAppointmentResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllAppointmentResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }

        [HttpGet("GenerateAppointmentNumber")]
        public async Task<CommonResponse> GenerateAppointmentNumber()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await _patient.GenerateAppointmentNumber();
                GenerateNumberResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<GenerateNumberResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        [HttpPost("AddAppoitment")]
        public CommonResponse AddAppoitment(AddAppoitmentReqViewModel addAppoitmentReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _patient.AddAppoitment(addAppoitmentReqViewModel.Adapt<AddAppoitmentReqDTO>());
                AddAppoitmentResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<AddAppoitmentResViewModel>();
            }
            catch (Exception) {     throw; }
            return commonResponse;


        }
        [HttpPost("AddRemark")]
        public CommonResponse AddRemark(AddRemarkReqViewModel addRemarkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _patient.AddRemark(addRemarkReqViewModel.Adapt<AddRemarkReqDTO>());
                AddRemarkResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<AddRemarkResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        [HttpPost("GetRemarksByAppointmentId")]
        public async Task<CommonResponse> GetRemarksByAppointmentId(RemarkReqViewModel remarkReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await _patient.GetRemarksByAppointmentNo(remarkReqViewModel.Adapt<RemarkReqDTO>());
                RemarksResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<RemarksResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }


        [HttpPost("GetAllAppointmentbyId")]
        public CommonResponse GetAllAppointmentbyId(GetAllAppointmentbyIdReqViewModel getAllAppointmentbyIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _patient.GetAllAppointmentbyId(getAllAppointmentbyIdReqDTO.Adapt<GetAllAppointmentbyIdReqDTO>());
                GetAllAppointmentbyIdResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllAppointmentbyIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("EditAppointment")]
        public CommonResponse EditAppointment(EditAppointmentReqViewModel editAppointmentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _patient.EditAppointment(editAppointmentReqDTO.Adapt<EditAppointmentReqDTO>());
                EditAppointmentResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<EditAppointmentResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }

    
        [HttpPost("GetAllCallTypeCount")]
        public CommonResponse GetAllCallTypeCount()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _patient.GetAllCallTypeCount();
                List<GetAllCallTypeCountResDTO> model = commonResponse.Data;
                commonResponse.Data = model.Adapt<List<GetAllCallTypeCountResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }

        [HttpPost("GetAllAppointmentByLocalSearch")]
        public CommonResponse GetAllAppointmentByLocalSearch(GetAllAppointmentByLocalSearchReqViewModel getAllAppointmentByLocalSearchReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _patient.GetAllAppointmentByLocalSearch(getAllAppointmentByLocalSearchReqViewModel.Adapt<GetAllAppointmentByLocalSearchReqDTO>());
                GetAllAppointmentByLocalSearchResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllAppointmentByLocalSearchResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }


        [HttpPost("UpdateRemarkStatus")]
        public CommonResponse UpdateRemarkStatus(UpdateRemarkStatusReqViewModel updateRemarkStatusReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse =  _patient.UpdateRemarkStatus(updateRemarkStatusReqViewModel.Adapt<UpdateRemarkStatusReqDTO>());
                UpdateRemarkStatusResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateRemarkStatusResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
   

    }
}
