using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;
using System.Net;
using System.Transactions;

namespace BusinessLayer
{
    public class IFABLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonHelper _commonHelper;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _configaration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public IFABLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configaration, IHostingEnvironment hostingEnvironment)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configaration = configaration;
            _hostingEnvironment = hostingEnvironment;
        }

        public CommonResponse GetAllIFA()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetAllIFAResDTO> ifaList = new List<GetAllIFAResDTO>();
                ifaList = _commonRepo.getUserList().Where(x => x.AccessCategoryId == 3).Select(x => new GetAllIFAResDTO //3 AccessCategory
                {
                    Id = x.Id,
                    IFA = x.FirstName + " " + x.LastName,

                }).ToList();
                if (ifaList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                }
                commonResponse.Data = ifaList.Adapt<List<GetAllIFAResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetIFAById(GetIFAReqDTO getIFAReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                UserMst IFADetails = _commonRepo.ALLUserList().FirstOrDefault(x => x.Id == getIFAReqDTO.Id);

                if (IFADetails != null)
                {
                    GetIFAResDTO getIFAResDTO = new GetIFAResDTO();
                    getIFAResDTO.Id = IFADetails.Id;
                    getIFAResDTO.ProfilePhoto = IFADetails.ProfilePhoto;
                    getIFAResDTO.Country = IFADetails.Country;
                    getIFAResDTO.Province = IFADetails.Province;
                    getIFAResDTO.City = IFADetails.City;
                    getIFAResDTO.Office = IFADetails.Office;
                    getIFAResDTO.FSCA = IFADetails.Fsca;
                    getIFAResDTO.IFAPractice = IFADetails.ClientAccNo;
                    getIFAResDTO.ResponsiblePerson = IFADetails.ResponsiblePerson;
                    getIFAResDTO.ResponsiblePersonTitle = IFADetails.Salutation;
                    getIFAResDTO.FirstName = IFADetails.FirstName;
                    getIFAResDTO.Surname = IFADetails.LastName;
                    getIFAResDTO.PositionHeld = IFADetails.PositionHeld;
                    getIFAResDTO.Dob = IFADetails.Dob;
                    getIFAResDTO.CompanyName = IFADetails.CompanyName;
                    getIFAResDTO.CompRegNumber = IFADetails.CompRegNumber;
                    getIFAResDTO.SarstaxNo = IFADetails.SarstaxNo;
                    getIFAResDTO.Vatno = IFADetails.Vatno;
                    getIFAResDTO.BuildingName = IFADetails.HomeName;
                    getIFAResDTO.FloorandOfficeNo = IFADetails.FloorandOfficeNo;
                    getIFAResDTO.StreetName = IFADetails.StreetName;
                    getIFAResDTO.Suburb = IFADetails.Suburb;
                    getIFAResDTO.PostalCode = IFADetails.PostalCode;
                    getIFAResDTO.IsFscaactive = IFADetails.IsFscaactive;
                    getIFAResDTO.LastDateChecked = IFADetails.LastDateChecked;
                    getIFAResDTO.PersonChecked = IFADetails.PersonChecked;
                    getIFAResDTO.WaltCapConsultant = IFADetails.WaltCapConsultant;
                    getIFAResDTO.SoftwareAccessGroup = IFADetails.SoftwareAccessGroup;
                    getIFAResDTO.MobileNo = IFADetails.MobileNo;
                    getIFAResDTO.WorkNo = IFADetails.WorkNo;
                    getIFAResDTO.Email = IFADetails.Email;
                    getIFAResDTO.Notes = IFADetails.Notes;
                    getIFAResDTO.IsAml = IFADetails.IsAml;
                    getIFAResDTO.IsDueDiligence = IFADetails.IsDueDiligence;
                    getIFAResDTO.InitialFee = IFADetails.InitialFee;
                    getIFAResDTO.AnnualAdvisorFees = IFADetails.AnnualAdvisorFees;
                    getIFAResDTO.PerformanceFee = IFADetails.PerformanceFee;
                    getIFAResDTO.IsVatapplicable = IFADetails.IsVatapplicable;
                    getIFAResDTO.Other = IFADetails.Other;
                    getIFAResDTO.DueDiligenceUpdatedDate = IFADetails.DueDiligenceUpdatedDate;
                    getIFAResDTO.AmlupdatedDate = IFADetails.AmlupdatedDate;
                    getIFAResDTO.IsActive = IFADetails.IsActive;
                    getIFAResDTO.Role = IFADetails.Role;

                    var expirydateDDM = _commonRepo.dueDiligenceList().Where(x => x.UserId == getIFAReqDTO.Id).OrderByDescending(x => x.Id).FirstOrDefault();

                    if (expirydateDDM != null)
                    {
                        if (expirydateDDM.ValidTill <= _commonHelper.GetCurrentDateTime())
                        {
                            getIFAResDTO.IsDueDiligence = false;
                            _dBContext.Entry(getIFAReqDTO).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            _dBContext.SaveChanges();
                        }
                    }

                    var expirydateAMl = _commonRepo.amlList().Where(x => x.UserId == getIFAReqDTO.Id).OrderByDescending(x => x.Id).FirstOrDefault();

                    if (expirydateAMl != null)
                    {
                        if (expirydateAMl.ValidTill <= _commonHelper.GetCurrentDateTime())
                        {
                            getIFAResDTO.IsAml = false;
                            _dBContext.Entry(getIFAReqDTO).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            _dBContext.SaveChanges();
                        }
                    }

                    var getalluser = _commonRepo.ALLUserList().ToList();

                    var createdName = getalluser.Where(x => x.Id == IFADetails.CreatedBy).FirstOrDefault();
                    string createdByName = createdName != null ? createdName.FirstName + " " + createdName.LastName : string.Empty;

                    var updateName = getalluser.Where(x => x.Id == IFADetails.UpdatedBy).FirstOrDefault();
                    string updatedByName = updateName != null ? updateName.FirstName + " " + updateName.LastName : string.Empty;

                    getIFAResDTO.CreatedByName = createdByName;
                    getIFAResDTO.UpdatedByName = updatedByName;
                    getIFAResDTO.IsAmlName = updatedByName;
                    getIFAResDTO.IsDueDiligenceName = updatedByName;
                    getIFAResDTO.IsProminentPoliticalName = updatedByName;

                    string FileBaseURL = Convert.ToString(_configaration.GetSection("FileBaseURL").Value);

                    var UserDocumentMst = _commonRepo.getUserDocumentList().Where(x => x.UserId == IFADetails.Id).ToList();

                    getIFAResDTO.IFADocuments = new List<DTO.ResDTO.ListDocuments>();
                    foreach (var item in UserDocumentMst)
                    {
                        string documentName = item.DocumentPath.Split('\\').Last();
                        getIFAResDTO.IFADocuments.Add(new DTO.ResDTO.ListDocuments { File = _commonHelper.GetBase64FromFile(FileBaseURL + item.DocumentPath, documentName), FileName = documentName });
                    }

                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getIFAResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddIFAPhase1(AddIFAReqDTO addIFAReqDTO)
        {
            int validtillMonthsDueDiligence = Convert.ToInt32(_configaration.GetSection("DueDiligenceMonths").Value);
            int validtillMonthsAML = Convert.ToInt32(_configaration.GetSection("AMLMonths").Value);
            CommonResponse commonResponse = new CommonResponse();
            AddIFAResDTO addIFAResDTO = new AddIFAResDTO();
            try
            {
                if (addIFAReqDTO != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        /*Code commented as per discussion with Mitesh Sir
                        var IsExistIfa = _commonRepo.getUserList().Where(x => (x.ClientAccNo == addIFAReqDTO.IFAPractice || x.Email.ToLower() == addIFAReqDTO.Email.ToLower() || x.MobileNo.ToLower() == addIFAReqDTO.MobileNo.ToLower()) && x.AccessCategoryId == 3).ToList();
                        if (IsExistIfa.Count == 0)
                        {*/
                        UserMst userMst = new UserMst();
                        var CRP = _commonHelper.CreateRandomPassword();
                        var passwordEncrtpt = _commonHelper.EncryptString(CRP);
                        userMst.ProfilePhoto = addIFAReqDTO.ProfilePhoto;
                        userMst.Country = addIFAReqDTO.Country;
                        userMst.Province = addIFAReqDTO.Province;
                        userMst.City = Convert.ToString(addIFAReqDTO.City);
                        userMst.Office = addIFAReqDTO.Office;
                        userMst.Fsca = addIFAReqDTO.FSCA;
                        userMst.ClientAccNo = addIFAReqDTO.IFAPractice;
                        userMst.Password = passwordEncrtpt;
                        userMst.ResponsiblePerson = addIFAReqDTO.ResponsiblePerson;
                        userMst.Salutation = addIFAReqDTO.ResponsiblePersonTitle;
                        userMst.AccessCategoryId = 3; // IFA Id
                        userMst.FirstName = addIFAReqDTO.FirstName;
                        userMst.LastName = addIFAReqDTO.Surname;
                        userMst.PositionHeld = addIFAReqDTO.PositionHeld;
                        userMst.Dob = addIFAReqDTO.Dob;
                        userMst.CompanyName = addIFAReqDTO.CompanyName;
                        userMst.CompRegNumber = addIFAReqDTO.CompRegNumber;
                        userMst.SarstaxNo = addIFAReqDTO.SarstaxNo;
                        userMst.Vatno = addIFAReqDTO.Vatno;
                        userMst.HomeName = addIFAReqDTO.BuildingName;
                        userMst.FloorandOfficeNo = addIFAReqDTO.FloorandOfficeNo;
                        userMst.StreetName = addIFAReqDTO.StreetName;
                        userMst.Suburb = addIFAReqDTO.Suburb;
                        userMst.PostalCode = addIFAReqDTO.PostalCode;
                        userMst.IsFscaactive = addIFAReqDTO.IsFscaactive;
                        userMst.LastDateChecked = addIFAReqDTO.LastDateChecked;
                        userMst.PersonChecked = addIFAReqDTO.PersonChecked;
                        userMst.WaltCapConsultant = addIFAReqDTO.WaltCapConsultant;
                        userMst.SoftwareAccessGroup = addIFAReqDTO.SoftwareAccessGroup;
                        userMst.MobileNo = addIFAReqDTO.MobileNo;
                        userMst.WorkNo = addIFAReqDTO.WorkNo;
                        userMst.Email = addIFAReqDTO.Email;
                        userMst.Notes = addIFAReqDTO.Notes;
                        userMst.Role = addIFAReqDTO.Role;
                        userMst.CreatedBy = addIFAReqDTO.CreatedBy;
                        userMst.UpdatedBy = addIFAReqDTO.CreatedBy;
                        userMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                        userMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        userMst.IsActive = false;
                        userMst.IsDeleted = false;
                        userMst.MaritalStatus = "N/A";

                        if (addIFAReqDTO.IsAml == true)
                        {
                            userMst.IsAml = addIFAReqDTO.IsAml;
                            userMst.AmlupdatedDate = _commonHelper.GetCurrentDateTime();
                            Amlmst amlmst = new Amlmst();
                            amlmst.UserId = userMst.Id;
                            amlmst.CreatedOn = _commonHelper.GetCurrentDateTime();
                            amlmst.ValidTill = _commonHelper.GetCurrentDateTime().AddMonths(validtillMonthsAML);
                            _dBContext.Amlmsts.Add(amlmst);
                            _dBContext.SaveChanges();
                        }

                        if (addIFAReqDTO.IsDueDiligence == true)
                        {
                            userMst.IsDueDiligence = addIFAReqDTO.IsDueDiligence;
                            userMst.DueDiligenceUpdatedDate = _commonHelper.GetCurrentDateTime();
                            DueDiligenceMst dueDiligenceMst = new DueDiligenceMst();
                            dueDiligenceMst.UserId = userMst.Id;
                            dueDiligenceMst.CreatedOn = _commonHelper.GetCurrentDateTime();
                            dueDiligenceMst.ValidTill = _commonHelper.GetCurrentDateTime().AddMonths(validtillMonthsDueDiligence);
                            _dBContext.DueDiligenceMsts.Add(dueDiligenceMst);
                            _dBContext.SaveChanges();
                        }

                        _dBContext.UserMsts.Add(userMst);
                        _dBContext.SaveChanges();

                        bool IsUploadSuccess = true;
                        string CurrentFileName = "";
                        foreach (var item in addIFAReqDTO.IFADocuments)
                        {
                            var FileUploadResponse = _commonHelper.UploadBase64File(item.FileName, item.File, addIFAReqDTO.IFAPractice, 3);
                            if (FileUploadResponse.StatusCode == HttpStatusCode.OK)
                            {
                                _dBContext.UserDocumentMsts.Add(new UserDocumentMst { UserId = userMst.Id, DocumentTypeId = 1, DocumentPath = FileUploadResponse.Data, IsActive = true, IsDeleted = false, CreatedBy = addIFAReqDTO.CreatedBy, UpdatedBy = addIFAReqDTO.CreatedBy, CreatedDate = _commonHelper.GetCurrentDateTime(), UpdatedDate = _commonHelper.GetCurrentDateTime() });
                                _dBContext.SaveChanges();
                            }
                            else
                            {
                                CurrentFileName = item.FileName;
                                IsUploadSuccess = false;
                            }
                        }

                        if (IsUploadSuccess)
                        {
                            scope.Complete();

                            addIFAResDTO.Id = userMst.Id;

                            commonResponse.Message = "User added successfully!";
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Data = addIFAResDTO;
                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "Please recheck and upload file : " + CurrentFileName;
                        }
                        /*}
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "Email Id, Mobile No. Or User Account No is already taken!";
                        }*/
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Invalid Data!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddIFAPhase2(AddIFAPhase2ReqDTO addIFAPhase2ReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddIFAPhase2ResDTO addIFAPhase2ResDTO = new AddIFAPhase2ResDTO();
            try
            {
                int assignedClientCount = 0;
                var ifaPhase2 = _commonRepo.ALLUserList().Where(x => x.Id == addIFAPhase2ReqDTO.Id).FirstOrDefault();

                if (addIFAPhase2ReqDTO.IsActive == false)
                {
                    assignedClientCount = _commonRepo.getUserList().Where(x => x.Ifa == addIFAPhase2ReqDTO.Id).Count();
                }

                if ((addIFAPhase2ReqDTO.IsActive == false && assignedClientCount == 0) || (addIFAPhase2ReqDTO.IsActive == true))
                {
                    if (ifaPhase2 != null)
                    {
                        ifaPhase2.InitialFee = addIFAPhase2ReqDTO.InitialFee;
                        ifaPhase2.AnnualAdvisorFees = addIFAPhase2ReqDTO.AnnualAdvisorFees;
                        ifaPhase2.PerformanceFee = addIFAPhase2ReqDTO.PerformanceFee;
                        ifaPhase2.Other = addIFAPhase2ReqDTO.Other;
                        ifaPhase2.IsVatapplicable = addIFAPhase2ReqDTO.IsVatapplicable;
                        ifaPhase2.UpdatedBy = addIFAPhase2ReqDTO.CreatedBy;
                        ifaPhase2.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        ifaPhase2.IsActive = addIFAPhase2ReqDTO.IsActive;
                        ifaPhase2.IsDeleted = false;

                        _dBContext.Entry(ifaPhase2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _dBContext.SaveChanges();

                        addIFAPhase2ResDTO.InitialFee = ifaPhase2.InitialFee;
                        addIFAPhase2ResDTO.AnnualAdvisorFees = ifaPhase2.AnnualAdvisorFees;

                        if (ifaPhase2.WelcomeMailSent == null || ifaPhase2.WelcomeMailSent == false)
                        {
                            var passwordDecrpt = _commonHelper.DecryptString(ifaPhase2.Password);
                            var ImagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "EmailTemplate", "logo.png");
                            var emailTemplatePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "EmailTemplate", "RegistrationEmailTemplate.html");
                            StreamReader str = new StreamReader(emailTemplatePath);
                            string MailText = str.ReadToEnd();
                            str.Close();

                            var htmlBody = MailText.Replace("[your Account created successfully]", ifaPhase2.FirstName).Replace("[Username]", ifaPhase2.FirstName + " " + ifaPhase2.LastName).Replace("[Account No.]", ifaPhase2.ClientAccNo).Replace("[Password]", passwordDecrpt);
                            htmlBody = htmlBody.Replace("logo.png", ImagePath);
                            SendEmailRequestModel sendEmailRequestModel = new SendEmailRequestModel();
                            sendEmailRequestModel.ToEmail = ifaPhase2.Email;
                            sendEmailRequestModel.Body = htmlBody;
                            sendEmailRequestModel.Subject = "Welcome To Walt Capital";

                            var EmailSend = _commonHelper.SendEmail(sendEmailRequestModel);

                            ifaPhase2.WelcomeMailSent = true;

                            //_dBContext.Entry(_dBContext).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            _dBContext.Entry(ifaPhase2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                            _dBContext.SaveChanges();

                            commonResponse.Message = "IFA added Successfully!";
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Data = addIFAPhase2ResDTO;
                        }
                        else
                        {
                            commonResponse.Message = "IFA updated successfully!";
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Data = addIFAPhase2ResDTO;
                        }
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "IFA already exist!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "IFA is already having linked client(s), So can't make it In-Active!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateIFA(UpdateIFAReqDTO updateIFAReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateIFAResDTO updateIFAResDTO = new UpdateIFAResDTO();
            try
            {
                using (var scope = new TransactionScope())
                {
                    /* Code commented as per discussion with Mitesh Sir
                    var IsExistclientdetails = _commonRepo.ALLUserList().Where(x => (x.IsActive == true && !x.IsDeleted && x.Id != updateIFAReqDTO.Id) && (x.MobileNo.ToLower() == updateIFAReqDTO.MobileNo.ToLower() || x.Email.ToLower() == updateIFAReqDTO.Email.ToLower()) && x.AccessCategoryId == 3).ToList() ?? new List<UserMst>();
                    if (IsExistclientdetails.Count == 0)
                    {*/
                    var ifaDetail = _commonRepo.ALLUserList().FirstOrDefault(x => x.Id == updateIFAReqDTO.Id);
                    if (ifaDetail != null)
                    {
                        string IFAPracticeNo = ifaDetail.ClientAccNo;


                        var IsAML = ifaDetail.IsAml;
                        var IsDueDelegence = ifaDetail.IsDueDiligence;
                        UserMst userMst = ifaDetail;

                        userMst.ProfilePhoto = updateIFAReqDTO.ProfilePhoto;
                        userMst.Fsca = updateIFAReqDTO.FSCA;
                        userMst.ResponsiblePerson = updateIFAReqDTO.ResponsiblePerson;
                        userMst.Salutation = updateIFAReqDTO.ResponsiblePersonTitle;
                        userMst.FirstName = updateIFAReqDTO.FirstName;
                        userMst.LastName = updateIFAReqDTO.SurName;
                        userMst.PositionHeld = updateIFAReqDTO.PositionHeld;
                        userMst.Dob = updateIFAReqDTO.Dob;
                        userMst.CompanyName = updateIFAReqDTO.CompanyName;
                        userMst.CompRegNumber = updateIFAReqDTO.CompRegNumber;
                        userMst.SarstaxNo = updateIFAReqDTO.SarstaxNo;
                        userMst.Vatno = updateIFAReqDTO.Vatno;
                        userMst.HomeName = updateIFAReqDTO.BuildingName;
                        userMst.FloorandOfficeNo = updateIFAReqDTO.FloorandOfficeNo;
                        userMst.StreetName = updateIFAReqDTO.StreetName;
                        userMst.Suburb = updateIFAReqDTO.Suburb;
                        userMst.PostalCode = updateIFAReqDTO.PostalCode;
                        userMst.IsFscaactive = updateIFAReqDTO.IsFscaactive;
                        userMst.LastDateChecked = updateIFAReqDTO.LastDateChecked;
                        userMst.PersonChecked = updateIFAReqDTO.PersonChecked;
                        userMst.WaltCapConsultant = updateIFAReqDTO.WaltCapConsultant;
                        userMst.SoftwareAccessGroup = updateIFAReqDTO.SoftwareAccessGroup;
                        userMst.Email = updateIFAReqDTO.Email;
                        userMst.MobileNo = updateIFAReqDTO.MobileNo;
                        userMst.WorkNo = updateIFAReqDTO.WorkNo;
                        userMst.Notes = updateIFAReqDTO.Notes;
                        userMst.Role = updateIFAReqDTO.Role;
                        userMst.IsAml = updateIFAReqDTO.IsAml;
                        userMst.IsDueDiligence = updateIFAReqDTO.IsDueDiligence;
                        userMst.UpdatedBy = updateIFAReqDTO.UpdatedBy;
                        userMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        _dBContext.Entry(userMst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _dBContext.SaveChanges();

                        if (IsAML != updateIFAReqDTO.IsAml)
                        {
                            int validtillMonthsAML = Convert.ToInt32(_configaration.GetSection("AMLMonths").Value);

                            ifaDetail.IsAml = updateIFAReqDTO.IsAml;
                            ifaDetail.AmlupdatedDate = _commonHelper.GetCurrentDateTime();
                            if (updateIFAReqDTO.IsAml == true)
                            {
                                Amlmst amlmst = new Amlmst();
                                amlmst.UserId = ifaDetail.Id;
                                amlmst.CreatedOn = _commonHelper.GetCurrentDateTime();
                                amlmst.ValidTill = _commonHelper.GetCurrentDateTime().AddMonths(validtillMonthsAML);
                                _dBContext.Amlmsts.Add(amlmst);
                                _dBContext.SaveChanges();
                            }
                        }

                        if (IsDueDelegence != updateIFAReqDTO.IsDueDiligence)
                        {
                            int validtillMonthsDueDiligence = Convert.ToInt32(_configaration.GetSection("DueDiligenceMonths").Value);
                            ifaDetail.IsDueDiligence = updateIFAReqDTO.IsDueDiligence;
                            ifaDetail.DueDiligenceUpdatedDate = _commonHelper.GetCurrentDateTime();
                            if (updateIFAReqDTO.IsDueDiligence == true)
                            {
                                DueDiligenceMst dueDiligenceMst = new DueDiligenceMst();
                                dueDiligenceMst.UserId = ifaDetail.Id;
                                dueDiligenceMst.CreatedOn = _commonHelper.GetCurrentDateTime();
                                dueDiligenceMst.ValidTill = _commonHelper.GetCurrentDateTime().AddMonths(validtillMonthsDueDiligence);
                                _dBContext.DueDiligenceMsts.Add(dueDiligenceMst);
                                _dBContext.SaveChanges();
                            }
                        }

                        var userDocumentMst = _commonRepo.getUserDocumentList().Where(x => x.UserId == userMst.Id).ToList();
                        if (userDocumentMst != null)
                        {
                            _dBContext.UserDocumentMsts.RemoveRange(userDocumentMst);
                            _dBContext.SaveChanges();
                        }

                        bool IsUploadSuccess = true;
                        string CurrentFileName = "";
                        foreach (var item in updateIFAReqDTO.IFADocuments)
                        {
                            var FileUploadResponse = _commonHelper.UploadBase64File(item.FileName, item.File, IFAPracticeNo, 3);
                            if (FileUploadResponse.StatusCode == HttpStatusCode.OK)
                            {
                                _dBContext.UserDocumentMsts.Add(new UserDocumentMst { UserId = userMst.Id, DocumentTypeId = 1, DocumentPath = FileUploadResponse.Data, IsActive = true, IsDeleted = false, CreatedBy = updateIFAReqDTO.UpdatedBy, UpdatedBy = updateIFAReqDTO.UpdatedBy, CreatedDate = _commonHelper.GetCurrentDateTime(), UpdatedDate = _commonHelper.GetCurrentDateTime() });
                                _dBContext.SaveChanges();
                            }
                            else
                            {
                                CurrentFileName = item.FileName;
                                IsUploadSuccess = false;
                            }
                        }

                        if (IsUploadSuccess)
                        {
                            scope.Complete();

                            updateIFAResDTO.Id = userMst.Id;

                            commonResponse.Message = "IFA Updated successfully";
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Data = updateIFAResDTO;
                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "Please recheck and upload file : " + CurrentFileName;
                        }
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Can not find IFA!";
                    }
                    /*}
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Email OR Mobile No. Already Exist!";
                    }*/
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeleteIFA(DeleteIFAReqDTO deleteIFAReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteIFAResDTO deleteIFAResDTO = new DeleteIFAResDTO();
            try
            {
                //var ifa = _commonRepo.ifaList().FirstOrDefault(x => x.Id == deleteIFAReqDTO.Id);
                //if (ifa != null)
                //{
                //    Ifamst ifamst = ifa;
                //    ifamst.Id = deleteIFAReqDTO.Id;
                //    ifamst.UpdatedBy = deleteIFAReqDTO.UserId;
                //    ifamst.IsDeleted = true;
                //    ifamst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                //    _dBContext.Entry(ifamst).State = EntityState.Modified;
                //    _dBContext.SaveChanges();

                //    deleteIFAResDTO.Id = ifamst.Id;

                //    commonResponse.Data = deleteIFAResDTO;
                //    commonResponse.Status = true;
                //    commonResponse.StatusCode = HttpStatusCode.OK;
                //    commonResponse.Message = "Deleted Successfully...!!!";
                //}
                //else
                //{
                //    commonResponse.Status = false;
                //    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                //    commonResponse.Message = "Can not delete the data...!!!";
                //}
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllIFAClient(GetIFAClientReqDTO getIFAClientReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            var pageData = _configaration.GetSection("ByDefaultPagination:Page");
            string pages = pageData.Value.ToString();
            int pagecount = Int32.Parse(pages);

            var totalPage = _configaration.GetSection("ByDefaultPagination:PageSize");
            string Size = totalPage.Value.ToString();
            int pageSize = Int32.Parse(Size);

            var orderBy = _configaration.GetSection("ByDefaultPagination:OrderBy");
            string orders = orderBy.Value.ToString();
            bool order = Boolean.Parse(orders);


            int number = getIFAClientReqDTO.PageNumber == 0 ? (pagecount) : getIFAClientReqDTO.PageNumber;
            int size = getIFAClientReqDTO.PageSize == 0 ? (pageSize) : getIFAClientReqDTO.PageSize;
            bool orderby = getIFAClientReqDTO.Orderby == true ? (order) : getIFAClientReqDTO.Orderby;
            try
            {
                GetIFAClientResDTO getIFAClientResDTO = new GetIFAClientResDTO();
                List<IFAClientList> iFAClientList = new List<IFAClientList>();
                var ifaDetail = _commonRepo.getUserList().FirstOrDefault(x => x.Id == getIFAClientReqDTO.IFAId);

                iFAClientList = _commonRepo.getUserList().Where(x => x.Ifa == getIFAClientReqDTO.IFAId && x.AccessCategoryId == 2).Select(x => new IFAClientList
                {
                    ClientAccNo = ifaDetail != null ? ifaDetail.ClientAccNo : "",
                    IFAId = ifaDetail != null ? ifaDetail.Ifa : 0,
                    Name = x.FirstName,
                    Surname = x.LastName,
                    ClientId = x.ClientAccNo
                }).ToList();

                getIFAClientResDTO.TotalCount = iFAClientList.Count();

                if (orderby)
                {
                    if (iFAClientList.Count <= size)
                    {
                        iFAClientList = iFAClientList.OrderBy(x => x.IFAId).ToList();
                    }
                    else
                    {
                        iFAClientList = iFAClientList.Skip((number - 1) * size)
                                .Take(size)
                                .OrderBy(x => x.IFAId)
                                .ToList();
                    }
                }
                else
                {
                    if (iFAClientList.Count <= size)
                    {
                        iFAClientList = iFAClientList.OrderByDescending(x => x.IFAId).ToList();
                    }
                    else
                    {
                        iFAClientList = iFAClientList.OrderByDescending(x => x.IFAId).Skip((number - 1) * size)
                            .Take(size)
                            .ToList();
                    }
                }


                getIFAClientResDTO.ifaClientList = iFAClientList;

                if (iFAClientList.Count > 0)
                {

                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getIFAClientResDTO;
                    commonResponse.Message = "Success";

                }
                else
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Data not Found";
                    commonResponse.Data = getIFAClientResDTO;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllIFAList(GetIFAAssetReqDTO getIFAAssetReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            var pageData = _configaration.GetSection("ByDefaultPagination:Page");
            string pages = pageData.Value.ToString();
            int pagecount = Int32.Parse(pages);

            var totalPage = _configaration.GetSection("ByDefaultPagination:PageSize");
            string Size = totalPage.Value.ToString();
            int pageSize = Int32.Parse(Size);

            var orderBy = _configaration.GetSection("ByDefaultPagination:OrderBy");
            string orders = orderBy.Value.ToString();
            bool order = Boolean.Parse(orders);


            int number = getIFAAssetReqDTO.PageNumber == 0 ? (pagecount) : getIFAAssetReqDTO.PageNumber;
            int size = getIFAAssetReqDTO.PageSize == 0 ? (pageSize) : getIFAAssetReqDTO.PageSize;
            bool orderby = getIFAAssetReqDTO.Orderby == true ? (order) : getIFAAssetReqDTO.Orderby;


            try
            {
                GetIFAAssetResDTO getIFAAssetResDTO = new GetIFAAssetResDTO();
                List<IFAAsseList> iFAList = new List<IFAAsseList>();

                iFAList = _commonRepo.ALLUserList(true).Where(x => x.AccessCategoryId == 3).Select(x => new IFAAsseList
                {
                    Id = x.Id,
                    Name = x.FirstName,
                    SurName = x.LastName,
                    PhoneNo = x.MobileNo,
                    Email = x.Email,
                    AUM = new Random().Next(10000, 100000),
                    ClientAccNo = x.ClientAccNo,
                    Status = x.IsActive == true ? "Active" : "InActive",
                }).ToList();

                getIFAAssetResDTO.TotalCount = iFAList.Count();

                if (getIFAAssetReqDTO.Alphabet != null && !string.IsNullOrWhiteSpace(getIFAAssetReqDTO.Alphabet))
                {
                    iFAList = iFAList.Where(x => x.SurName.ToLower().StartsWith(getIFAAssetReqDTO.Alphabet.ToLower())).ToList();
                    getIFAAssetResDTO.TotalCount = iFAList.Count();

                }

                if (getIFAAssetReqDTO.SearchString != null && !string.IsNullOrEmpty(getIFAAssetReqDTO.SearchString))
                {
                    iFAList = iFAList.Where(x => x.Name.ToLower().Contains(getIFAAssetReqDTO.SearchString.ToLower()) || x.SurName.ToLower().Contains(getIFAAssetReqDTO.SearchString.ToLower())).ToList();

                    getIFAAssetResDTO.TotalCount = iFAList.Count();
                }

                if (orderby)
                {
                    if (iFAList.Count <= size)
                    {
                        iFAList = iFAList.OrderBy(x => x.Id).ToList();
                    }
                    else
                    {
                        iFAList = iFAList.Skip((number - 1) * size)
                                .Take(size)
                                .OrderBy(x => x.Id)
                                .ToList();
                    }
                }
                else
                {
                    if (iFAList.Count <= size)
                    {
                        iFAList = iFAList.OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        iFAList = iFAList.OrderByDescending(x => x.Id).Skip((number - 1) * size)
                            .Take(size)
                            .ToList();
                    }
                }

                getIFAAssetResDTO.TotalIFACount = iFAList.Sum(x => x.AUM);
                getIFAAssetResDTO.ifaAssetList = iFAList;

                if (getIFAAssetResDTO != null)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success";
                    commonResponse.Data = getIFAAssetResDTO;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Data not found";
                    commonResponse.Data = getIFAAssetResDTO;
                }
            }

            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GenerateIFAAccountNo()
        {

            CommonResponse commonResponse = new CommonResponse();
            GenerateIFAAccountNoResDTO generateIFAAccountNoResDTO = new GenerateIFAAccountNoResDTO();
            try
            {
                string accountNo = string.Empty;
                int totalIFAs = _commonRepo.ALLUserList(false).Where(x => x.AccessCategoryId == 3).Count();
                accountNo = "IFA" + string.Format("{0:D4}", totalIFAs + 1);
                generateIFAAccountNoResDTO.AccountNo = accountNo;

                if (generateIFAAccountNoResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = generateIFAAccountNoResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
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