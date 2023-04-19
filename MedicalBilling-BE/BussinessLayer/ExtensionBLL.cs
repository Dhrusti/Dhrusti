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
    public class ExtensionBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;

        public ExtensionBLL(MedicalBillingDbContext dbcontext, CommonRepo commonRepo, IConfiguration configuration, AuthRepo authRepo, CommonHelper commonHelper)
        {
            _dbContext = dbcontext;
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _iConfiguration = configuration;
            _authRepo = authRepo;

        }
        public CommonResponse GetExtensionList()
        {
            CommonResponse commonResponse = new CommonResponse();
            List<GetAllExtensionResDTO> extensionList = new List<GetAllExtensionResDTO>();
            try
            {
                extensionList = _commonRepo.getAllExtension().Select(x => new GetAllExtensionResDTO
                {
                    Id = x.Id,
                    ExtensionName = x.ExtensionName,

                }).ToList();

                if (extensionList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = extensionList;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Not Found";
                  
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
