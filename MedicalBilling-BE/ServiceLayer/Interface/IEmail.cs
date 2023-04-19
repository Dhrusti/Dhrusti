using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IEmail
    {
        public CommonResponse SendMail(SendEmailReqDTO sendEmailReqDTO);
        public CommonResponse GetAllEmailDatatbyId(GetAllEmailbyIdReqDTO getAllEmailbyIdReqDTO);
        public CommonResponse GetAllDoctorEmail();
    }
}
