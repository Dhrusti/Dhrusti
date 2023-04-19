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
    public class EmailImpl : IEmail
    {
        private readonly EmailBLL _emailBLL;

        public EmailImpl(EmailBLL emailBLL)
        { 
            _emailBLL = emailBLL;
        }

        public CommonResponse SendMail(SendEmailReqDTO sendEmailReqDTO)
        {
            return _emailBLL.SendEmail(sendEmailReqDTO);
        }

        public CommonResponse GetAllEmailDatatbyId(GetAllEmailbyIdReqDTO getAllEmailbyIdReqDTO)
        {
            return _emailBLL.GetAllEmailDatatbyId(getAllEmailbyIdReqDTO);   
        }
        public CommonResponse GetAllDoctorEmail()
        {
            return _emailBLL.GetAllDoctorEmail();
        }
    }
}
