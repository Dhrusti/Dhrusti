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
    public class EmailController : ControllerBase
    {
        private readonly IEmail _email;

        public EmailController(IEmail email)
        { 
            _email = email;
        }
        [HttpPost("GetAllEmailDatatbyId")]
        public CommonResponse GetAllEmailDatatbyId(GetAllEmailDataIdReqViewModel getAllEmailDataIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _email.GetAllEmailDatatbyId(getAllEmailDataIdReqViewModel.Adapt<GetAllEmailbyIdReqDTO>());
                GetAllEmailDatabyIdResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetAllEmailDatabyIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }


        [HttpPost("SendEmail")]
        public CommonResponse SendEmail(SendEmailReqViewModel sendEmailReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _email.SendMail(sendEmailReqViewModel.Adapt<SendEmailReqDTO>());
                SendMailResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<SendMailResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;

        }
        [HttpGet("GetAllDoctorEmail")]
        public CommonResponse GetAllDoctorEmail()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _email.GetAllDoctorEmail();
                List<GetAllDoctorEmailResDTO> model = commonResponse.Data;
                commonResponse.Data = model.Adapt<List<GetAllDoctorEmailResViewModel>>();
            }
            catch (Exception) { throw; }
            return commonResponse;


        }
    }
}
