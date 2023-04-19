using DataLayer.Entities;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class ClientBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;

        public ClientBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, IConfiguration configuration, AuthRepo authRepo, CommonHelper commonHelper)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _iConfiguration = configuration;
            _authRepo = authRepo;

        }
        public CommonResponse GetAllClient()
        {
            CommonResponse commonResponse = new CommonResponse();
            List<GetAllClientResDTO> clientResDTO = new List<GetAllClientResDTO>();
            try
            {
                clientResDTO = _commonRepo.getCLientList().Select(x => new GetAllClientResDTO
                {
                    ClientName = x.FirstName +" "+x.LastName,
                    OfficeName = x.OfficeName,
                    Address = x.StreetNo +" "+x.HomeName +" "+x.StreetName+" "+x.City+" "+x.PostalCode ,
                    InfoEmail = x.InfoEmail,
                    AppoitmentEmail = x.AppoitmentEmail,
                    DoctorEmail = x.DoctorEmail,
                    MobileNo = x.MobileNo,
                    FaxNo = x.FaxNo,
                }).ToList();

                if (clientResDTO.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = clientResDTO;
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
