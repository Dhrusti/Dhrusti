using DTO.ResDTO;
using Helper.Models;
using Mapster;
using MedicalBillingManagementWebAPI.ViewModels.ResViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;

namespace MedicalBillingManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor _doctor;

        public DoctorController(IDoctor doctor)
        {
            _doctor = doctor;
        }

        [HttpGet("GetAllDoctor")]
        public CommonResponse GetDoctorList()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _doctor.GetDoctorList();
                List<GetAllDoctorResDTO> model = commonResponse.Data;
                commonResponse.Data = model.Adapt<List<GetAllDoctorResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }
        
        [HttpGet("GetAllApptDoctor")]
        public CommonResponse GetApptDoctorList()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _doctor.GetApptDoctorList();
                List<GetAllApptDoctorResDTO> model = commonResponse.Data;
                commonResponse.Data = model.Adapt<List<GetAllApptDoctorResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }
    }
}
