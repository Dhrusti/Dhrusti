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
    public class PatientPDFController : ControllerBase
    {
        private readonly IPatientPDF _patientPDF;

        public PatientPDFController(IPatientPDF patientPDF)
        {
            _patientPDF = patientPDF;
        }
       
        [HttpPost("GeneratePatientPDF")]
        public CommonResponse GeneratePatientPDF(GeneratePatientPDFReqViewModel generatePDFReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse =  _patientPDF.GeneratePatientPDF(generatePDFReqViewModel.Adapt<GeneratePatientPDFReqDTO>());
                //GeneratePatientPDFResDTO model = commonResponse.Data;
                //commonResponse.Data = model.Adapt<GeneratePatientPDFResViewModel>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }


    }
}
