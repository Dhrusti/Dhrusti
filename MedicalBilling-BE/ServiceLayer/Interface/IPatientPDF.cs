using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IPatientPDF
    {
        public  CommonResponse GeneratePatientPDF(GeneratePatientPDFReqDTO generatePatientPDFReqDTO);

        //public Byte[] GeneratePdfFromFragment(string htmlFragment);
    }
}
