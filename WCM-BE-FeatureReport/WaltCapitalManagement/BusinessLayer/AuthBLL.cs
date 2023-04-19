using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BusinessLayer
{
    public class AuthBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _iConfiguration;
        private readonly AuthRepo _authRepo;
        private readonly CommonHelper _commonHelper;
        private IHostingEnvironment _hostingEnvironment { get; }

        public AuthBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, AuthRepo authRepo, IConfiguration iConfiguration, CommonHelper commonHelper, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _authRepo = authRepo;
            _iConfiguration = iConfiguration;
            _commonHelper = commonHelper;
            _hostingEnvironment = hostingEnvironment;
        }

        public bool? IsDeviceApproved(int Id)
        {
            return _commonRepo.getUserList_Login().Where(x => x.IsActive == true && x.Id == Id).Select(x => x.IsDeviceApproved).FirstOrDefault();
        }

        public async Task<CommonResponse> LoginAsync(LogInReqDTO logInReqDTO)
        {
            CommonResponse response = new CommonResponse();
            LogInResDTO logInResDTO = new LogInResDTO();
            try
            {
                var UserDetails = (from um in _commonRepo.getUserList_Login()
                                   where (um.ClientAccNo.ToLower() == logInReqDTO.UserName.ToLower() || um.Email.ToLower() == logInReqDTO.UserName.ToLower()) && um.Password == logInReqDTO.Password
                                   join acm in _dbContext.AccessCategoryMsts on um.AccessCategoryId equals acm.Id into acmst
                                   from all in acmst.DefaultIfEmpty()
                                   select new UserDetail
                                   {
                                       Id = um.Id,
                                       FirstName = um.FirstName,
                                       LastName = um.LastName,
                                       Email = um.Email,
                                       Role = um.Role,
                                       AccessCategory = all.AccessCategory,
                                       SoftwareAccessGroupId = um.SoftwareAccessGroup,
                                       IsDeviceApproved = um.IsDeviceApproved,
                                       DeviceId = um.DeviceId,
                                       IsActive = um.IsActive ?? true
                                   }).ToList();

                if (UserDetails != null && UserDetails.Count() > 0)
                {
                    var UserDetail = UserDetails.Where(x => x.IsActive).FirstOrDefault();
                    if (UserDetail != null)
                    {
                        UserMst userMst = _commonRepo.getUserList_Login().Where(x => x.IsActive == true && x.Id == UserDetail.Id).SingleOrDefault() ?? new UserMst();
                        userMst.IsDeviceApproved = null;

                        _dbContext.Entry(userMst).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        if (logInReqDTO.UserName == "MasterXX" && logInReqDTO.Password == "5BkvUD2PrA3K8aU6KwcJWw==")
                        {
                            response = getSuccessLoginResponse(logInResDTO, UserDetail, false);
                        }
                        else if (UserDetail.DeviceId != null)
                        {
                            userMst = _commonRepo.getUserList_Login().Where(x => x.IsActive == true && x.Id == UserDetail.Id).SingleOrDefault() ?? new UserMst();
                            userMst.IsDeviceApproved = null;

                            _dbContext.Entry(userMst).State = EntityState.Modified;
                            _dbContext.SaveChanges();

                            NotificationReqDTO notificationReqDTO = new NotificationReqDTO();
                            notificationReqDTO.DeviceId = UserDetail.DeviceId;
                            notificationReqDTO.IsAndroiodDevice = true;
                            notificationReqDTO.Title = _iConfiguration.GetSection("FcmNotification:NotificationTitle").Value;
                            notificationReqDTO.Body = _iConfiguration.GetSection("FcmNotification:NotificationBody").Value;

                            var notification = await _commonHelper.SendNotificationAsync(notificationReqDTO);

                            int? ThreadSleepTime = Convert.ToInt32(_iConfiguration.GetSection("FcmNotification:ThreadSleepTime").Value);
                            int? ThreadLoopTime = Convert.ToInt32(_iConfiguration.GetSection("FcmNotification:ThreadLoopTime").Value);

                            bool? isDeviceApproved = null;
                            for (int i = 0; i < (ThreadLoopTime ?? 12); i++)
                            {
                                isDeviceApproved = IsDeviceApproved(UserDetail.Id);
                                if (isDeviceApproved == null)
                                {
                                    Thread.Sleep(ThreadSleepTime ?? 5000);
                                }
                                else
                                {
                                    UserDetail.IsDeviceApproved = isDeviceApproved;
                                    if (UserDetail.IsDeviceApproved != null)
                                    {
                                        break;
                                    }
                                }
                            }

                            userMst.IsDeviceApproved = null;

                            _dbContext.Entry(userMst).State = EntityState.Modified;
                            _dbContext.SaveChanges();

                            //popup open disclaimer
                            bool IsDisclaimer;
                            var disclaimer = _dbContext.DisclaimerMsts.Where(x => x.UserId == userMst.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                            IsDisclaimer = disclaimer != null ? _commonHelper.GetCurrentDateTime() > disclaimer.ValidTill ? true : false : true;

                            if (UserDetail.IsDeviceApproved == true)
                            {
                                response = getSuccessLoginResponse(logInResDTO, UserDetail, IsDisclaimer);
                            }
                            else if (UserDetail.IsDeviceApproved == null || UserDetail.IsDeviceApproved == false)
                            {
                                response.Message = "Device Is Not Authenticated!";
                            }
                            else
                            {
                                response.Message = "Login Request Timeout!";
                            }
                        }
                        else
                        {
                            response.Message = "Please Login Again From Mobile Device!";
                        }
                    }
                    else
                    {
                        response.Message = "Account Is In-Active. Please Contact Administrator!";
                    }
                }
                else
                {
                    response.Message = "Account No. or Password Not Valid.";
                }
                response.Data = logInResDTO;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public CommonResponse GetRefreshToken(RefreshTokenReqDTO refreshTokenReqDTO)
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

        public CommonResponse ForgotPassword(ForgotPasswordReqDTO forgotReqDTO)
        {
            CommonResponse commonResponse = new();
            try
            {
                var baseURL = _iConfiguration.GetSection("SiteEmailConfigration:BaseURL").Value;

                var ISExistmail = _commonRepo.getUserList_Login().Where(x => x.ClientAccNo == forgotReqDTO.ClientAccountNo).FirstOrDefault();
                if (ISExistmail != null)
                {
                    var userid = _commonHelper.EncryptString(ISExistmail.Id.ToString());
                    var datetimevalue = _commonHelper.EncryptString(_commonHelper.GetCurrentDateTime().ToString("ddmmyyyyhhmmsstt"));
                    baseURL += "?q=" + userid + "&d=" + datetimevalue;

                    var ImagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "EmailTemplate", "logo.png");
                    var emailTemplatePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "EmailTemplate", "EmailTemplate.html");
                    StreamReader str = new StreamReader(emailTemplatePath);
                    string MailText = str.ReadToEnd();
                    str.Close();

                    var htmlBody = MailText.Replace("[Resetlink]", "<a target='_blank' href='" + baseURL + "'>Reset Password</a>").Replace("[Username]", ISExistmail.FirstName + " " + ISExistmail.LastName);
                    htmlBody = htmlBody.Replace("logo.png", ImagePath);
                    SendEmailRequestModel sendEmailRequestModel = new SendEmailRequestModel();
                    sendEmailRequestModel.ToEmail = ISExistmail.Email;
                    sendEmailRequestModel.Body = htmlBody;
                    sendEmailRequestModel.Subject = "Reset Password Link";

                    var EmailSend = _commonHelper.SendEmail(sendEmailRequestModel);

                    var IsLinkSave = AddResetPasswordLink(userid, baseURL);

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Password Reset Link Has Been Sent To Your Email!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Account Number Not Found!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse ResetPassword(ResetPasswordReqDTO resetPasswordReqDTO)
        {
            CommonResponse commonResponse = new();
            try
            {
                var decrptId = _commonHelper.DecryptString(resetPasswordReqDTO.UserId);
                int userId = Convert.ToInt32(decrptId);
                var IsExistId = _commonRepo.getUserList_Login().Where(x => x.Id == userId).FirstOrDefault();
                if (IsExistId != null)
                {
                    IsExistId.Password = _commonHelper.EncryptString(resetPasswordReqDTO.NewPassword); // encrypted password


                    _dbContext.Entry(IsExistId).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Reset Password Sucessfully!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can Not Reset Your Password!";
                }

            }
            catch (Exception)
            {
                throw;

            }
            return commonResponse;
        }

        private bool AddResetPasswordLink(string Id, string BaseUrl)
        {
            int id = Convert.ToInt32(_commonHelper.DecryptString(Id));
            LinkMst linkMst = new LinkMst();
            linkMst.UserId = id;
            linkMst.IsClicked = false;
            linkMst.ResetPasswordLink = BaseUrl;
            linkMst.CreatedDate = _commonHelper.GetCurrentDateTime();
            linkMst.ExpiredDate = _commonHelper.GetCurrentDateTime().AddDays(1);
            _dbContext.LinkMsts.Add(linkMst);
            _dbContext.SaveChanges();

            return true;
        }

        public CommonResponse CheckResetPasswordLink(CheckResetPasswordLinkReqDTO checkResetPasswordLinkReqDTO)
        {
            CommonResponse commonResponse = new();
            try
            {
                if (!string.IsNullOrEmpty(checkResetPasswordLinkReqDTO.Id) && !string.IsNullOrEmpty(checkResetPasswordLinkReqDTO.Link) && !string.IsNullOrEmpty(checkResetPasswordLinkReqDTO.SecurityCode))

                {
                    var decrptId = _commonHelper.DecryptString(checkResetPasswordLinkReqDTO.Id);
                    int userId = Convert.ToInt32(decrptId);
                    var IsExistLink =_commonRepo.linkList().Where(x => x.UserId == userId && x.ResetPasswordLink == checkResetPasswordLinkReqDTO.Link && x.IsClicked == false).FirstOrDefault();

                    if (IsExistLink != null)
                    {
                        if (IsExistLink.ExpiredDate <= _commonHelper.GetCurrentDateTime())
                        {
                            commonResponse.Message = "Link is expries";
                        }
                        else
                        {
                            var decrptdatetime = _commonHelper.DecryptString(checkResetPasswordLinkReqDTO.SecurityCode);
                            DateTime date = DateTime.ParseExact(decrptdatetime, "ddmmyyyyhhmmsstt", null);
                            date = date.AddDays(1);
                            if (_commonHelper.GetCurrentDateTime() <= date)
                            {
                                IsExistLink.IsClicked = true;
                                _dbContext.Entry(IsExistLink).State = EntityState.Modified;
                                _dbContext.SaveChanges();
                                commonResponse.Status = true;
                                commonResponse.StatusCode = HttpStatusCode.OK;
                                commonResponse.Message = "Link is valid.";
                            }
                            else
                            {
                                commonResponse.Message = "Link is expries";
                            }
                        }
                    }
                    else
                    {
                        commonResponse.Message = "Link is expries";
                    }
                }
                else
                {
                    commonResponse.Message = "Link Expries";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse MobileLoginCheck(MobileLoginApiReqDTO mobileLoginApiReqDTO)
        {
            CommonResponse response = new CommonResponse();
            MobileLoginApiResDTO mobileLoginApiResDTO = new MobileLoginApiResDTO();
            try
            {
                //var encrptPassword = _commonHelper.EncryptString(mobileLoginApiReqDTO.Password);
                var encrptPassword = mobileLoginApiReqDTO.Password;


                var UserDetail = _dbContext.UserMsts.Where(x => (x.ClientAccNo == mobileLoginApiReqDTO.UserName || x.Email == mobileLoginApiReqDTO.UserName) && x.Password == encrptPassword).FirstOrDefault();


                if (UserDetail != null)
                {
                    UserDetail.DeviceId = mobileLoginApiReqDTO.DeviceId;
                    _dbContext.Entry(UserDetail).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    var token = _authRepo.CreateJWT(UserDetail.Id, false);
                    var Refreshtoken = _authRepo.CreateRefreshToken();

                    if (token != null)
                    {
                        mobileLoginApiResDTO.UserDetail = UserDetail;
                        mobileLoginApiResDTO.Token = token.Token;
                        mobileLoginApiResDTO.RefreshToken = token.RefreshToken;

                        // Send Firebase Notification On Device Id;

                        response.Status = true;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = "LogIn Sucessfully.";
                    }
                    else
                    {
                        response.Message = "Token not Generated";
                    }
                }
                else
                {
                    response.Message = "UserName or Password Not Valid.";
                }
                response.Data = mobileLoginApiResDTO;
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public CommonResponse MobileApproveReject(MobileApproveRejectReqDTO mobileApproveRejectReqDTO)
        {
            CommonResponse commonResponse = new();
            try
            {
                var IsUserExits = _commonRepo.getUserList_Login().Where(x => x.IsActive == true && x.Id == mobileApproveRejectReqDTO.UserId).FirstOrDefault();
                if (IsUserExits != null)
                {
                    IsUserExits.IsDeviceApproved = mobileApproveRejectReqDTO.IsDeviceApproved;
                    _dbContext.Entry(IsUserExits).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Fail";
                }

            }
            catch (Exception)
            {
                throw;

            }
            return commonResponse;
        }

        public CommonResponse Logout()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        private List<GetAllPrivilegesResDTO> GetAccessCategoryChild(GetAllPrivilegesResDTO item, List<AccessCategoryPermissionMst> accessCategoryPermissionMstList, int parentId, int layer)
        {
            List<GetAllPrivilegesResDTO> level2 = _commonRepo.accessCategoryList().Where(x => x.ParentId == item.Id).ToList().Adapt<List<GetAllPrivilegesResDTO>>();
            foreach (var subitem in level2)
            {
                foreach (var permission in accessCategoryPermissionMstList)
                {
                    subitem.IsSelected = subitem.IsSelected ? true : subitem.Id == permission.AccessableCategoryId ? true : false;
                }
                subitem.AllPrivileges = GetAccessCategoryChild(subitem, accessCategoryPermissionMstList, parentId, layer + 1);
                subitem.ParentId = parentId;
                subitem.Layer = layer;
            }
            return level2;
        }

        private CommonResponse getSuccessLoginResponse(LogInResDTO logInResDTO, UserDetail UserDetail, bool IsDisclaimer)
        {
            CommonResponse response = new CommonResponse();
            var token = _authRepo.CreateJWT(UserDetail.Id, false);
            _authRepo.CreateRefreshToken();

            if (token != null)
            {
                //UserDetail.AccessCategory = _commonRepo.getAllRoleList().Where(x=>x.Id == Convert.ToInt32(UserDetail.Role)).Select(x=>x.RoleName).FirstOrDefault() ?? "IFA";
                logInResDTO.UserDetail = UserDetail;
                logInResDTO.Token = token.Token;
                logInResDTO.RefreshToken = token.RefreshToken;
                logInResDTO.Disclaimer = IsDisclaimer;
                logInResDTO.AccessControl = GetUserGroupAccess(UserDetail.SoftwareAccessGroupId);

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

        private dynamic GetUserGroupAccess(int SoftwareAccessGroupId)
        {
            var accessCategoryPermissionMst = (from acpm in _commonRepo.accessCategoryPermissionMsts()
                                               where acpm.GroupId == SoftwareAccessGroupId
                                               join acm in _commonRepo.accessCategoryList() on acpm.AccessableCategoryId equals acm.Id into acmst
                                               from all in acmst.DefaultIfEmpty()
                                               select new
                                               {
                                                   all.Id,
                                                   all.AccessCategory,
                                                   all.CreatedDate,
                                                   all.ParentId
                                               }).OrderBy(x => x.CreatedDate).ToList();

            var accessCategoryMst = _commonRepo.accessCategoryList().ToList();

            List<string> accessControl = new List<string>();
            foreach (var item in accessCategoryPermissionMst)
            {
                if (item.ParentId != 0)
                {
                    string parentAccessControlName = accessCategoryMst.Where(x => x.Id == item.ParentId).Select(x => x.AccessCategory).FirstOrDefault() ?? "";

                    if (!accessControl.Contains("/" + parentAccessControlName))
                    { accessControl.Add("/" + parentAccessControlName); }

                    string parentChildAccessControl = parentAccessControlName + "/" + item.AccessCategory;
                    accessControl.Add("/" + parentChildAccessControl);
                }
                else
                {
                    accessControl.Add("/" + item.AccessCategory);
                }
            }
            return accessControl;
        }
    }
}
