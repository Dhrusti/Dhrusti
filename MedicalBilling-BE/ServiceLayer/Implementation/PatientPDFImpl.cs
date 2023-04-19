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
    public class PatientPDFImpl : IPatientPDF
    {
        private readonly PatientPDFBLL _patientPDFBLL;

        public PatientPDFImpl(PatientPDFBLL patientPDFBLL)
        {
            _patientPDFBLL = patientPDFBLL;
        }

        public CommonResponse GeneratePatientPDF(GeneratePatientPDFReqDTO generatePatientPDFReqDTO)
        {
            CommonResponse response = new CommonResponse();
            response = _patientPDFBLL.GeneratePatientPDF(generatePatientPDFReqDTO);
            return response;

        }

    }
}
