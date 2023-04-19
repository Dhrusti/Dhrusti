using DataLayer.Entities;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class DoctorBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;

        public DoctorBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, IConfiguration configuration, AuthRepo authRepo, CommonHelper commonHelper)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _iConfiguration = configuration;
            _authRepo = authRepo;

        }

        public CommonResponse GetDoctorList()
        {
            CommonResponse commonResponse = new CommonResponse();
            List<GetAllDoctorResDTO> doctorList = new List<GetAllDoctorResDTO>();
            try
            {
                doctorList = _commonRepo.getAllPhysician().Select(x => new GetAllDoctorResDTO
                {
                    DoctorName = x.DoctorFirstName +" "+ x.DoctorLastName,
                    Degree = x.DoctorDegreeName1 +" ,"+x.DoctorDegreeName2+" ,"+x.DoctorDegreeName3,
                    secretary = x.SecretaryFirstName +" "+x.SecretaryLastName,
                }).ToList();

                if (doctorList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = doctorList;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Not Found";
                    commonResponse.Data = null;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetApptDoctorList()
        {
            CommonResponse commonResponse = new CommonResponse();
            List<GetAllApptDoctorResDTO> doctorList = new List<GetAllApptDoctorResDTO>();
            try
            {
                doctorList = _commonRepo.getApptDoctor().Select(x => new GetAllApptDoctorResDTO
                {
                    Id = x.Id,
                    PhysicianName = x.DoctorFirstName +" "+x.DoctorLastName
                }).ToList();

                if (doctorList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = doctorList;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Not Found";
                    commonResponse.Data = null;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
    }
}
