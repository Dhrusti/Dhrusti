using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Microsoft.AspNetCore.Hosting;

namespace BusinessLayer
{
    public class UserDocumentBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonHelper _commonHelper;
        private IHostingEnvironment _hostingEnvironment;
        private readonly CommonRepo _commonRepo;
        public UserDocumentBLL(WaltCapitalDBContext dBContext, CommonHelper commonHelper, IHostingEnvironment hostingEnvironment, CommonRepo commonRepo)
        {
            _dBContext = dBContext;
            _commonHelper = commonHelper;
            _hostingEnvironment = hostingEnvironment;
            _commonRepo = commonRepo;
        }

        public CommonResponse AddUserDocument(UserDocumentReqDTO userDocumentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                //if (userDocumentReqDTO.UserId > 0)
                //{
                //    var userDetail = _commonRepo.ALLUserList().FirstOrDefault(x => x.Id == userDocumentReqDTO.UserId);
                //    if (userDetail != null)
                //    {
                //        var CurrentDate = _commonHelper.GetCurrentDateTime();
                //        int FileCount = userDocumentReqDTO.Files.Count;
                //        int ErrorStatusCount = 0;
                //        //List<string> FilePaths = new List<string>();
                //        UserDocumentResDTO userDocumentResDTO = new UserDocumentResDTO();
                //        userDocumentResDTO.Files = new List<string>();
                //        if (FileCount > 0)
                //        {
                //            foreach (var file in userDocumentReqDTO.Files)
                //            {
                //                FileInfo fileInfo = new FileInfo(file.FileName);
                //                bool validateFileSize = false;
                //                bool validateFileExtension = false;
                //                string[] allowedFileExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };       //.pdf , .jpg , .jpeg , .png
                //                int allowedFileSize = 2000000;                                              // 2MB

                //                validateFileSize = file.Length <= allowedFileSize ? true : false;
                //                validateFileExtension = allowedFileExtensions.Contains(fileInfo.Extension.ToLower()) ? true : false;

                //                if (validateFileSize && validateFileExtension)
                //                {
                //                    string SubDirectoryPath = "UserDocument";
                //                    if (fileInfo.Extension.ToLower() == ".pdf")
                //                    {
                //                        SubDirectoryPath = Path.Combine(SubDirectoryPath, "PDF");
                //                    }
                //                    else if (fileInfo.Extension.ToLower() == ".jpg" || fileInfo.Extension.ToLower() == ".jpeg" || fileInfo.Extension.ToLower() == ".png")
                //                    {
                //                        SubDirectoryPath = Path.Combine(SubDirectoryPath, "Images");
                //                    }

                //                    string FileName = CurrentDate.ToString("yy") + CurrentDate.ToString("MM") + CurrentDate.ToString("dd") + CurrentDate.ToString("HH") + CurrentDate.ToString("mm") + CurrentDate.ToString("ss") + CurrentDate.ToString("fffffff") + userDocumentReqDTO.UserId.ToString() + fileInfo.Extension;

                //                    var response = _commonHelper.UploadFile(file, SubDirectoryPath, FileName);
                //                    if (response.Status)
                //                    {
                //                        /* UserDocumentMst userDocumentMst = new UserDocumentMst();
                //                         userDocumentMst.UserId = userDocumentReqDTO.UserId;
                //                         userDocumentMst.DocumentTypeId = CommonConstant.ProfilePhoto;
                //                         userDocumentMst.DocumentPath = response.Data;
                //                         userDocumentMst.CreatedBy = userDocumentReqDTO.UserId;
                //                         userDocumentMst.UpdatedBy = userDocumentReqDTO.UserId;
                //                         userDocumentMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                //                         userDocumentMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                //                         userDocumentMst.IsActive = true;
                //                         userDocumentMst.IsDeleted = false;

                //                         //_dBContext.UserDocumentMsts.Add(userDocumentMst);
                //                         _dBContext.SaveChanges();
                //                         string FullPath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", userDocumentMst.DocumentPath);
                //                         userDocumentResDTO.Files.Add(FullPath);*/
                //                    }
                //                    else
                //                    {
                //                        commonResponse.Data = response.Data;
                //                        ErrorStatusCount++;
                //                    }
                //                }
                //                else
                //                {
                //                    commonResponse.Message = "Invalid File.";
                //                    ErrorStatusCount++;
                //                }
                //            }

                //            if (ErrorStatusCount == 0)
                //            {

                //                commonResponse.Status = true;
                //                commonResponse.StatusCode = HttpStatusCode.OK;
                //                commonResponse.Message = "Success.";
                //                commonResponse.Data = userDocumentResDTO;

                //            }
                //        }
                //    }
                //    else
                //    {
                //        commonResponse.Message = "Invalid UserId.";
                //    }

                //}
                //else
                //{
                //    commonResponse.Message = "Invalid UserId.";
                //}
            }
            catch (Exception ex)
            {
                commonResponse.Data = ex.ToString();
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }

        public CommonResponse RemoveUserDocument(DeleteAccountTypeReqDTO deleteAccountTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteAccountTypeResDTO deleteAccountTypeResDTO = new DeleteAccountTypeResDTO();
            try
            {
                /*var document = _commonRepo.userDocumentList().FirstOrDefault(x => x.Id == deleteAccountTypeReqDTO.Id);
                if (document != null)
                {
                    UserDocumentMst userDocumentMst = document;
                    userDocumentMst.Id = deleteAccountTypeReqDTO.Id;
                    userDocumentMst.UpdatedBy = deleteAccountTypeReqDTO.UserId;
                    userDocumentMst.IsDeleted = true;
                    userDocumentMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dBContext.Entry(userDocumentMst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dBContext.SaveChanges();

                    deleteAccountTypeResDTO.Id = userDocumentMst.Id;

                    commonResponse.Data = deleteAccountTypeResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Deleted Successfully...!!!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not delete the data...!!!";
                }*/
            }
            catch (Exception ex)
            {
                commonResponse.Data = ex.ToString();
                commonResponse.Message = ex.Message;
            }
            return commonResponse;
        }
    }
}
