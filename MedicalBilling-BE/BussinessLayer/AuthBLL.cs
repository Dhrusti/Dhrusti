using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class AuthBLL
    {
        private readonly MedicalBillingDbContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;
        private IHostingEnvironment _hostingEnvironment { get; }

        public AuthBLL(MedicalBillingDbContext dbContext, CommonRepo commonRepo, AuthRepo authRepo, IConfiguration iConfiguration, CommonHelper commonHelper, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _authRepo = authRepo;
            _iConfiguration = iConfiguration;
            _commonHelper = commonHelper;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<CommonResponse> LoginAsync(LogInReqDTO logInReqDTO)
        {
            CommonResponse response = new CommonResponse();
            LogInResDTO logInResDTO = new LogInResDTO();

            var user = _dbContext.UserMsts.Where(x => x.UserName == logInReqDTO.UserName && x.Password == logInReqDTO.Password).FirstOrDefault();
        
            if (user != null)
            {
                var role = _dbContext.RoleMsts.FirstOrDefault(x => x.Id == user.Role);
                var UserDetails = new UserDetail
                {

                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    Role = role.RoleName
                };

                if (user.Password == logInReqDTO.Password)
                {
                    response = getSuccessLoginResponse(logInResDTO, UserDetails, false);

                }
                else
                {
                    response = getSuccessLoginResponse(logInResDTO, UserDetails, false);
                }
            }
            else
            {
                response.Status = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Please Enter valid UserName and Password!";
            }

            response.Data = logInResDTO;
            return response;
        }
        public CommonResponse GetRefreshToken(GetRefreshTokenReqDTO refreshTokenReqDTO)
        {
            CommonResponse response = new CommonResponse();
            response = _authRepo.ValidateToken(refreshTokenReqDTO.Adapt<TokenModel>());
            return response;
        }

        public string EncryptString(string plainText)
        {
            return _commonHelper.EncryptString(plainText);
        }

        public string DecryptString(string cipherText)
        {
            return _commonHelper.DecryptString(cipherText);
        }



        private CommonResponse getSuccessLoginResponse(LogInResDTO logInResDTO, UserDetail UserDetail, bool IsDisclaimer)
        {
            CommonResponse response = new CommonResponse();
            var token = _authRepo.CreateJWT(UserDetail.Id, false);
            _authRepo.CreateRefreshToken();

            if (token != null)
            {
                logInResDTO.UserDetail = UserDetail;
                logInResDTO.Token = token.Token;
                logInResDTO.RefreshToken = token.RefreshToken;

                response.Status = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Message = "LogIn Sucessfully.";
            }
            else
            {
                response.Message = "Token not Generated";
            }
            return response;
        }

        //private dynamic GetUserGroupAccess(int SoftwareAccessGroupId)
        //{
        //    var accessCategoryPermissionMst = (from acpm in _commonRepo.accessCategoryPermissionMsts()
        //                                       where acpm.GroupId == SoftwareAccessGroupId
        //                                       join acm in _commonRepo.accessCategoryList() on acpm.AccessableCategoryId equals acm.Id into acmst
        //                                       from all in acmst.DefaultIfEmpty()
        //                                       select new
        //                                       {
        //                                           all.Id,
        //                                           all.AccessCategory,
        //                                           all.CreatedDate,
        //                                           all.ParentId
        //                                       }).OrderBy(x => x.CreatedDate).ToList();

        //    var accessCategoryMst = _commonRepo.accessCategoryList().ToList();

        //    List<string> accessControl = new List<string>();
        //    foreach (var item in accessCategoryPermissionMst)
        //    {
        //        if (item.ParentId != 0)
        //        {
        //            string parentAccessControlName = accessCategoryMst.Where(x => x.Id == item.ParentId).Select(x => x.AccessCategory).FirstOrDefault() ?? "";

        //            if (!accessControl.Contains("/" + parentAccessControlName))
        //            { accessControl.Add("/" + parentAccessControlName); }

        //            string parentChildAccessControl = parentAccessControlName + "/" + item.AccessCategory;
        //            accessControl.Add("/" + parentChildAccessControl);
        //        }
        //        else
        //        {
        //            accessControl.Add("/" + item.AccessCategory);
        //        }
        //    }
        //    return accessControl;
        //}



    }
}
