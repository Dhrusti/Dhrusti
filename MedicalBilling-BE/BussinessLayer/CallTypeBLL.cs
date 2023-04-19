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
    public class CallTypeBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;

        public CallTypeBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, IConfiguration configuration, AuthRepo authRepo, CommonHelper commonHelper)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _iConfiguration = configuration;
            _authRepo = authRepo;

        }
        public CommonResponse GetCallTypeList()
        {
            CommonResponse commonResponse = new CommonResponse();
            List<GetAllCallTypeResDTO> callTypeList = new List<GetAllCallTypeResDTO>();
            try
            {
                callTypeList = _commonRepo.getAllCallType().Select(x => new GetAllCallTypeResDTO
                {
                    Id = x.Id,
                    CallTypeName = x.CallTypeName,

                }).ToList();

                if (callTypeList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = callTypeList;
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
