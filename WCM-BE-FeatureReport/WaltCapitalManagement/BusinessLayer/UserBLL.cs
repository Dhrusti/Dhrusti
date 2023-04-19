using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Transactions;

namespace BusinessLayer
{
    public class UserBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _configaration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configaration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public CommonResponse AddClientPhase1(AddClientPhase1ReqDTO addClientPhase1ReqDTO)
        {
            int validtillMonthsDueDiligence = Convert.ToInt32(_configaration.GetSection("DueDiligenceMonths").Value);
            int validtillMonthsAML = Convert.ToInt32(_configaration.GetSection("AMLMonths").Value);
            CommonResponse commonResponse = new CommonResponse();
            AddClientPhase1ResDTO addClientResDTO = new AddClientPhase1ResDTO();
            try
            {
                if (addClientPhase1ReqDTO != null)
                {
                    using (var scope = new TransactionScope())
                    {
                         /*List<UserMst> IsExistclient = new List<UserMst>();
                         if (!string.IsNullOrEmpty(addClientPhase1ReqDTO.MobileNo))
                         {
                             IsExistclient = _dbContext.UserMsts.Where(x => (x.MobileNo.ToLower() == addClientPhase1ReqDTO.MobileNo.ToLower() || x.Email.ToLower() == addClientPhase1ReqDTO.Email.ToLower() || x.ClientAccNo == addClientPhase1ReqDTO.ClientAccNo) && x.AccessCategoryId == 2).ToList();
                         }
                         else
                         {
                             IsExistclient = _dbContext.UserMsts.Where(x => (x.Email.ToLower() == addClientPhase1ReqDTO.Email.ToLower() || x.ClientAccNo == addClientPhase1ReqDTO.ClientAccNo) && x.AccessCategoryId == 2).ToList();
                         }

                         if (IsExistclient.Count == 0)
                         {*/
                        UserMst clientMst = new UserMst();
                        var CRP = _commonHelper.CreateRandomPassword();
                        var passwordEncrtpt = _commonHelper.EncryptString(CRP);
                        clientMst.ProfilePhoto = addClientPhase1ReqDTO.ProfilePhoto;
                        clientMst.Office = addClientPhase1ReqDTO.Office;
                        clientMst.AccessCategoryId = addClientPhase1ReqDTO.AccessCategoryId;
                        clientMst.ClientAccNo = addClientPhase1ReqDTO.ClientAccNo;
                        clientMst.Password = passwordEncrtpt;
                        clientMst.ResponsiblePerson = addClientPhase1ReqDTO.ResponsiblePerson;
                        clientMst.FirstName = addClientPhase1ReqDTO.FirstName;
                        clientMst.LastName = addClientPhase1ReqDTO.LastName;
                        clientMst.PositionHeld = addClientPhase1ReqDTO.PositionHeld;
                        clientMst.Dob = addClientPhase1ReqDTO.Dob;
                        clientMst.TrustRegNo = addClientPhase1ReqDTO.TrustRegNo;
                        clientMst.MobileNo = addClientPhase1ReqDTO.MobileNo;
                        clientMst.WorkNo = addClientPhase1ReqDTO.WorkNo;
                        clientMst.Email = addClientPhase1ReqDTO.Email;
                        clientMst.SarstaxNo = addClientPhase1ReqDTO.SarstaxNo;
                        clientMst.Country = addClientPhase1ReqDTO.Country;
                        clientMst.StreetNo = addClientPhase1ReqDTO.StreetNo;
                        clientMst.HomeName = addClientPhase1ReqDTO.HomeName;
                        clientMst.StreetName = addClientPhase1ReqDTO.StreetName;
                        clientMst.Suburb = addClientPhase1ReqDTO.Suburb;
                        clientMst.City = addClientPhase1ReqDTO.City;
                        clientMst.Province = addClientPhase1ReqDTO.Province;
                        clientMst.PostalCode = addClientPhase1ReqDTO.PostalCode;
                        clientMst.AccountHolder = addClientPhase1ReqDTO.AccountHolder;
                        clientMst.Bank = addClientPhase1ReqDTO.Bank;
                        clientMst.AccountType = addClientPhase1ReqDTO.AccountType;
                        clientMst.AccountNo = addClientPhase1ReqDTO.AccountNo;
                        clientMst.BranchCode = addClientPhase1ReqDTO.BranchCode;
                        clientMst.SwiftCode = addClientPhase1ReqDTO.SwiftCode;
                        clientMst.ClientType = addClientPhase1ReqDTO.ClientType;
                        clientMst.PersonalityType = addClientPhase1ReqDTO.PersonalityType;
                        clientMst.WaltCapConsultant = addClientPhase1ReqDTO.WaltCapConsultant;
                        clientMst.Ifa = addClientPhase1ReqDTO.Ifa;
                        clientMst.MaritalStatus = addClientPhase1ReqDTO.MaritalStatus;
                        clientMst.SoftwareAccessGroup = addClientPhase1ReqDTO.SoftwareAccessGroup;
                        clientMst.SpouseName = addClientPhase1ReqDTO.SpouseName;
                        clientMst.SpouseDob = addClientPhase1ReqDTO.SpouseDob;
                        clientMst.NickName = addClientPhase1ReqDTO.NickName;
                        clientMst.Faserial = addClientPhase1ReqDTO.Faserial;
                        clientMst.Notes = addClientPhase1ReqDTO.Notes;
                        clientMst.CreatedBy = addClientPhase1ReqDTO.CreatedBy;
                        clientMst.UpdatedBy = addClientPhase1ReqDTO.CreatedBy;
                        //  clientMst.Salutation = addClientPhase1ReqDTO.Salutation;
                        clientMst.MiddleName = addClientPhase1ReqDTO.MiddleName;
                        clientMst.IsProminentPolitical = addClientPhase1ReqDTO.IsProminentPolitical;
                        clientMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                        clientMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        clientMst.IsActive = false;
                        clientMst.IsDeleted = false;
                        clientMst.Idnumber = addClientPhase1ReqDTO.Idnumber;
                        clientMst.Vatno = addClientPhase1ReqDTO.Vatno;

                        if (addClientPhase1ReqDTO.IsAml == true)
                        {
                            clientMst.IsAml = addClientPhase1ReqDTO.IsAml;
                            clientMst.AmlupdatedDate = _commonHelper.GetCurrentDateTime();
                            Amlmst amlmst = new Amlmst();
                            amlmst.UserId = clientMst.Id;
                            amlmst.CreatedOn = _commonHelper.GetCurrentDateTime();
                            amlmst.ValidTill = _commonHelper.GetCurrentDateTime().AddMonths(validtillMonthsAML);
                            _dbContext.Amlmsts.Add(amlmst);
                            _dbContext.SaveChanges();
                        }

                        if (addClientPhase1ReqDTO.IsDueDiligence == true)
                        {
                            clientMst.IsDueDiligence = addClientPhase1ReqDTO.IsDueDiligence;
                            clientMst.DueDiligenceUpdatedDate = _commonHelper.GetCurrentDateTime();
                            DueDiligenceMst dueDiligenceMst = new DueDiligenceMst();
                            dueDiligenceMst.UserId = clientMst.Id;
                            dueDiligenceMst.CreatedOn = _commonHelper.GetCurrentDateTime();
                            dueDiligenceMst.ValidTill = _commonHelper.GetCurrentDateTime().AddMonths(validtillMonthsDueDiligence);
                            _dbContext.DueDiligenceMsts.Add(dueDiligenceMst);
                            _dbContext.SaveChanges();
                        }

                        _dbContext.UserMsts.Add(clientMst);
                        _dbContext.SaveChanges();


                        bool IsUploadSuccess = true;
                        string CurrentFileName = "";

                        if (addClientPhase1ReqDTO.ClientDocuments != null)
                        {
                            foreach (var item in addClientPhase1ReqDTO.ClientDocuments)
                            {
                                var FileUploadResponse = _commonHelper.UploadBase64File(item.FileName, item.File, addClientPhase1ReqDTO.ClientAccNo, addClientPhase1ReqDTO.AccessCategoryId);
                                if (FileUploadResponse.StatusCode == HttpStatusCode.OK)
                                {
                                    _dbContext.UserDocumentMsts.Add(new UserDocumentMst { UserId = clientMst.Id, DocumentTypeId = 1, DocumentPath = FileUploadResponse.Data, IsActive = true, IsDeleted = false, CreatedBy = addClientPhase1ReqDTO.CreatedBy, UpdatedBy = addClientPhase1ReqDTO.CreatedBy, CreatedDate = _commonHelper.GetCurrentDateTime(), UpdatedDate = _commonHelper.GetCurrentDateTime() });
                                    _dbContext.SaveChanges();
                                }
                                else
                                {
                                    CurrentFileName = item.FileName;
                                    IsUploadSuccess = false;
                                }
                            }
                        }

                        if (IsUploadSuccess)
                        {
                            scope.Complete();

                            addClientResDTO.UserId = clientMst.Id;

                            commonResponse.Message = "User added successfully!";
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Data = addClientResDTO;
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

        public CommonResponse AddClientPhase2(AddClientPhase2ReqDTO addClientPhase2ReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddClientPhase2ResDTO addClientPhase2ResDTO = new AddClientPhase2ResDTO();
            try
            {
                if (addClientPhase2ReqDTO != null)
                {
                    var IsExistclient = _dbContext.UserMsts.Where(x => x.Id == addClientPhase2ReqDTO.Id).FirstOrDefault();
                    if (IsExistclient != null)
                    {
                        IsExistclient.Equity = addClientPhase2ReqDTO.Equity;
                        IsExistclient.Tfsa = addClientPhase2ReqDTO.Tfsa;
                        IsExistclient.WcfundAdministration = addClientPhase2ReqDTO.WCFundAdministration;
                        IsExistclient.Dcs = addClientPhase2ReqDTO.Dcs;
                        IsExistclient.Mcs = addClientPhase2ReqDTO.Mcs;
                        IsExistclient.InitialFee = addClientPhase2ReqDTO.InitialFee;
                        IsExistclient.AnnualManagementFee = addClientPhase2ReqDTO.AnnualManagementFee;
                        IsExistclient.PerformanceFee = addClientPhase2ReqDTO.PerformanceFee;
                        IsExistclient.BrokerageRate = addClientPhase2ReqDTO.BrokerageRate;
                        IsExistclient.FlatBrokerageRate = addClientPhase2ReqDTO.FlatBrokerageRate;
                        IsExistclient.AdminMonthlyFee = addClientPhase2ReqDTO.AdminMonthlyFee;
                        IsExistclient.Other = addClientPhase2ReqDTO.Other;
                        IsExistclient.IsVatapplicable = addClientPhase2ReqDTO.IsVatapplicable;
                        IsExistclient.LoadWithoutFee = addClientPhase2ReqDTO.LoadWithoutFee;
                        IsExistclient.UpdatedBy = addClientPhase2ReqDTO.CreatedBy;
                        IsExistclient.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        IsExistclient.IsActive = addClientPhase2ReqDTO.IsActive;
                        IsExistclient.IsDeleted = false;

                        _dbContext.Entry(IsExistclient).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        addClientPhase2ResDTO.UserId = IsExistclient.Id;

                        if (IsExistclient.WelcomeMailSent == null || IsExistclient.WelcomeMailSent == false)
                        {
                            var passwordDecrpt = _commonHelper.DecryptString(IsExistclient.Password);
                            var ImagePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "EmailTemplate", "logo.png");
                            var emailTemplatePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "EmailTemplate", "RegistrationEmailTemplate.html");
                            StreamReader str = new StreamReader(emailTemplatePath);
                            string MailText = str.ReadToEnd();
                            str.Close();

                            var htmlBody = MailText.Replace("[your Account created successfully]", IsExistclient.FirstName).Replace("[Username]", IsExistclient.FirstName + " " + IsExistclient.LastName).Replace("[Account No.]", IsExistclient.ClientAccNo).Replace("[Password]", passwordDecrpt);
                            htmlBody = htmlBody.Replace("logo.png", ImagePath);
                            SendEmailRequestModel sendEmailRequestModel = new SendEmailRequestModel();
                            sendEmailRequestModel.ToEmail = IsExistclient.Email;
                            sendEmailRequestModel.Body = htmlBody;
                            sendEmailRequestModel.Subject = "Welcome To Walt Capital";

                            var EmailSend = _commonHelper.SendEmail(sendEmailRequestModel);

                            IsExistclient.WelcomeMailSent = true;

                            _dbContext.Entry(IsExistclient).State = EntityState.Modified;
                            _dbContext.SaveChanges();

                            commonResponse.Message = "User added successfully";
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Data = addClientPhase2ResDTO;
                        }
                        else
                        {
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "User updated successfully!";
                            commonResponse.Data = addClientPhase2ResDTO;
                        }
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Invalid Data";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllClient(GetAllClientReqDTO getClientReqDTO)
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


            int number = getClientReqDTO.PageNumber == 0 ? (pagecount) : getClientReqDTO.PageNumber;
            int size = getClientReqDTO.PageSize == 0 ? (pageSize) : getClientReqDTO.PageSize;
            bool orderby = getClientReqDTO.Orderby == true ? (order) : getClientReqDTO.Orderby;

            try
            {
                GetAllClientResDTO getAllClientResDTO = new GetAllClientResDTO();
                List<ClientDetails> clientList = new List<ClientDetails>();

                clientList = _commonRepo.ALLUserList().Where(x => !x.IsDeleted && x.AccessCategoryId == 2).Select(x => new ClientDetails //2 Clients
                {
                    UserId = x.Id,
                    ClientAccNo = x.ClientAccNo,
                    CreatedDate = x.CreatedDate,
                    Dob = x.Dob,
                    Email = x.Email,
                    InvestmentValue = "0",
                    MobileNo = x.MobileNo,
                    Name = x.FirstName + " " + x.LastName,
                    Status = x.IsActive == true ? "Active" : "InActive",
                }).ToList();

                getAllClientResDTO.TotalCount = clientList.Count();

                if (getClientReqDTO.Alphabet != null && !string.IsNullOrEmpty(getClientReqDTO.Alphabet))
                {
                    clientList = clientList.Where(x => x.Name.ToLower().StartsWith(getClientReqDTO.Alphabet.ToLower())).ToList();

                    getAllClientResDTO.TotalCount = clientList.Count();
                }
                if (getClientReqDTO.SearchString != null && !string.IsNullOrEmpty(getClientReqDTO.SearchString))
                {
                    clientList = clientList.Where(x => x.Name.ToLower().Contains(getClientReqDTO.SearchString.ToLower()) || x.ClientAccNo.ToLower().Contains(getClientReqDTO.SearchString.ToLower()) || x.Email.ToLower().Contains(getClientReqDTO.SearchString.ToLower()) || x.MobileNo.Contains(getClientReqDTO.SearchString.ToLower())).ToList();

                    getAllClientResDTO.TotalCount = clientList.Count();
                }

                if (orderby)
                {
                    if (clientList.Count <= size)
                    {
                        clientList = clientList.OrderBy(x => x.CreatedDate).ToList();
                    }
                    else
                    {
                        clientList = clientList.Skip((number - 1) * size)
                                .Take(size)
                                .OrderBy(x => x.CreatedDate)
                                .ToList();
                    }
                }
                else
                {
                    if (clientList.Count <= size)
                    {
                        clientList = clientList.OrderByDescending(x => x.CreatedDate).ToList();
                    }
                    else
                    {
                        clientList = clientList.OrderByDescending(x => x.CreatedDate).Skip((number - 1) * size)
                            .Take(size)
                            .ToList();
                    }
                }

                getAllClientResDTO.ClientList = clientList;

                if (getAllClientResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getAllClientResDTO;
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

        public CommonResponse GetByClientId(GetByClientIdReqDTO getByClientIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            GetClientByIdResDTO getClientByIdResDTO = new GetClientByIdResDTO();
            try
            {
                var getalluser = _commonRepo.ALLUserList().ToList();
                if (getalluser != null)
                {
                    var getClientById = _commonRepo.ALLUserList().Where(x => x.Id == getByClientIdReqDTO.UserId).FirstOrDefault();
                    var expirydateDDM = _commonRepo.dueDiligenceList().Where(x => x.UserId == getClientById.Id).OrderByDescending(x => x.Id).FirstOrDefault();

                    if (expirydateDDM != null)
                    {
                        if (expirydateDDM.ValidTill <= _commonHelper.GetCurrentDateTime())
                        {
                            getClientById.IsDueDiligence = false;
                            _dbContext.Entry(getClientById).State = EntityState.Modified;
                            _dbContext.SaveChanges();
                        }
                    }

                    var expirydateAMl = _commonRepo.amlList().Where(x => x.UserId == getClientById.Id).OrderByDescending(x => x.Id).FirstOrDefault();

                    if (expirydateAMl != null)
                    {
                        if (expirydateAMl.ValidTill <= _commonHelper.GetCurrentDateTime())
                        {
                            getClientById.IsAml = false;
                            _dbContext.Entry(getClientById).State = EntityState.Modified;
                            _dbContext.SaveChanges();
                        }
                    }
                    getClientByIdResDTO = getClientById.Adapt<GetClientByIdResDTO>();
                    var createdName = getalluser.Where(x => x.Id == getClientById.CreatedBy).FirstOrDefault();
                    string createdByName = createdName != null ? createdName.FirstName + " " + createdName.LastName : string.Empty;

                    var updateName = getalluser.Where(x => x.Id == getClientById.UpdatedBy).FirstOrDefault();
                    string updatedByName = updateName != null ? updateName.FirstName + " " + updateName.LastName : string.Empty;

                    getClientByIdResDTO.CreatedByName = createdByName;
                    getClientByIdResDTO.UpdatedByName = updatedByName;
                    getClientByIdResDTO.IsAmlName = updatedByName;
                    getClientByIdResDTO.IsDueDiligenceName = updatedByName;
                    getClientByIdResDTO.IsProminentPoliticalName = updatedByName;
                }


                string FileBaseURL = Convert.ToString(_configaration.GetSection("FileBaseURL").Value);

                var UserDocumentMst = _commonRepo.getUserDocumentList().Where(x => x.UserId == getClientByIdResDTO.Id).ToList();

                getClientByIdResDTO.ClientDocuments = new List<DTO.ResDTO.ListDocuments>();
                foreach (var item in UserDocumentMst)
                {
                    string documentName = item.DocumentPath.Split('\\').Last();
                    getClientByIdResDTO.ClientDocuments.Add(new DTO.ResDTO.ListDocuments { File = _commonHelper.GetBase64FromFile(FileBaseURL + item.DocumentPath, documentName), FileName = documentName });
                }

                if (getClientByIdResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getClientByIdResDTO;
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

        public CommonResponse UpdateClientPhase1(UpdateClientPhase1ReqDTO updateClientPhase1ReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateClientPhase1ResDTO updateClientPhase1ResDTO = new UpdateClientPhase1ResDTO();
            try
            {
                if (updateClientPhase1ReqDTO != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        var IsExistclientdetails = _commonRepo.ALLUserList().Where(x => (x.IsActive == true && !x.IsDeleted && x.Id != updateClientPhase1ReqDTO.Id) && (x.MobileNo.ToLower() == updateClientPhase1ReqDTO.MobileNo.ToLower() || x.Email.ToLower() == updateClientPhase1ReqDTO.Email.ToLower() || x.ClientAccNo == updateClientPhase1ReqDTO.ClientAccNo) && x.AccessCategoryId == 2).ToList() ?? new List<UserMst>();

                        if (IsExistclientdetails.Count == 0)
                        {
                            var IsExistclient = _commonRepo.ALLUserList().Where(x => !x.IsDeleted && x.Id == updateClientPhase1ReqDTO.Id).FirstOrDefault();
                            if (IsExistclient != null)
                            {
                                var IsAML = IsExistclient.IsAml;
                                var IsDueDelegence = IsExistclient.IsDueDiligence;

                                IsExistclient.ProfilePhoto = updateClientPhase1ReqDTO.ProfilePhoto;
                                IsExistclient.Office = updateClientPhase1ReqDTO.Office;
                                IsExistclient.ClientAccNo = updateClientPhase1ReqDTO.ClientAccNo;
                                IsExistclient.ResponsiblePerson = updateClientPhase1ReqDTO.ResponsiblePerson;
                                IsExistclient.FirstName = updateClientPhase1ReqDTO.FirstName;
                                IsExistclient.LastName = updateClientPhase1ReqDTO.LastName;
                                IsExistclient.PositionHeld = updateClientPhase1ReqDTO.PositionHeld;
                                IsExistclient.Dob = updateClientPhase1ReqDTO.Dob;
                                IsExistclient.TrustRegNo = updateClientPhase1ReqDTO.TrustRegNo;
                                IsExistclient.MobileNo = updateClientPhase1ReqDTO.MobileNo;
                                IsExistclient.WorkNo = updateClientPhase1ReqDTO.WorkNo;
                                IsExistclient.Email = updateClientPhase1ReqDTO.Email;
                                IsExistclient.SarstaxNo = updateClientPhase1ReqDTO.SarstaxNo;
                                IsExistclient.Country = updateClientPhase1ReqDTO.Country;
                                IsExistclient.StreetNo = updateClientPhase1ReqDTO.StreetNo;
                                IsExistclient.HomeName = updateClientPhase1ReqDTO.HomeName;
                                IsExistclient.StreetName = updateClientPhase1ReqDTO.StreetName;
                                IsExistclient.Suburb = updateClientPhase1ReqDTO.Suburb;
                                IsExistclient.City = updateClientPhase1ReqDTO.City;
                                IsExistclient.Province = updateClientPhase1ReqDTO.Province;
                                IsExistclient.PostalCode = updateClientPhase1ReqDTO.PostalCode;
                                IsExistclient.AccountHolder = updateClientPhase1ReqDTO.AccountHolder;
                                IsExistclient.Bank = updateClientPhase1ReqDTO.Bank;
                                IsExistclient.AccountType = updateClientPhase1ReqDTO.AccountType;
                                IsExistclient.AccountNo = updateClientPhase1ReqDTO.AccountNo;
                                IsExistclient.BranchCode = updateClientPhase1ReqDTO.BranchCode;
                                IsExistclient.SwiftCode = updateClientPhase1ReqDTO.SwiftCode;
                                IsExistclient.ClientType = updateClientPhase1ReqDTO.ClientType;
                                IsExistclient.PersonalityType = updateClientPhase1ReqDTO.PersonalityType;
                                IsExistclient.WaltCapConsultant = updateClientPhase1ReqDTO.WaltCapConsultant;
                                IsExistclient.Ifa = updateClientPhase1ReqDTO.Ifa;
                                IsExistclient.MaritalStatus = updateClientPhase1ReqDTO.MaritalStatus;
                                IsExistclient.SoftwareAccessGroup = updateClientPhase1ReqDTO.SoftwareAccessGroup;
                                IsExistclient.SpouseName = updateClientPhase1ReqDTO.SpouseName;
                                IsExistclient.SpouseDob = updateClientPhase1ReqDTO.SpouseDob;
                                IsExistclient.NickName = updateClientPhase1ReqDTO.NickName;
                                IsExistclient.Faserial = updateClientPhase1ReqDTO.Faserial;
                                IsExistclient.Notes = updateClientPhase1ReqDTO.Notes;
                                //  IsExistclient.Salutation = updateClientPhase1ReqDTO.Salutation;
                                IsExistclient.MiddleName = updateClientPhase1ReqDTO.MiddleName;
                                IsExistclient.IsAml = updateClientPhase1ReqDTO.IsAml;
                                IsExistclient.IsDueDiligence = updateClientPhase1ReqDTO.IsDueDiligence;
                                IsExistclient.IsProminentPolitical = updateClientPhase1ReqDTO.IsProminentPolitical;
                                IsExistclient.UpdatedBy = updateClientPhase1ReqDTO.UpdatedBy;
                                IsExistclient.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                IsExistclient.Idnumber = updateClientPhase1ReqDTO.Idnumber;
                                IsExistclient.Vatno = updateClientPhase1ReqDTO.Vatno;

                                _dbContext.Entry(IsExistclient).State = EntityState.Modified;
                                _dbContext.Entry(IsExistclient).Property(x => x.Country).IsModified = false;
                                _dbContext.Entry(IsExistclient).Property(x => x.Province).IsModified = false;
                                _dbContext.Entry(IsExistclient).Property(x => x.City).IsModified = false;
                                _dbContext.Entry(IsExistclient).Property(x => x.Office).IsModified = false;
                                _dbContext.Entry(IsExistclient).Property(x => x.ClientAccNo).IsModified = false;
                                _dbContext.Entry(IsExistclient).Property(x => x.Password).IsModified = false;
                                //_dbContext.Entry(IsExistclient).Property(x => x.Email).IsModified = false;

                                _dbContext.SaveChanges();

                                if (IsAML != updateClientPhase1ReqDTO.IsAml)
                                {
                                    int validtillMonthsAML = Convert.ToInt32(_configaration.GetSection("AMLMonths").Value);

                                    IsExistclient.IsAml = updateClientPhase1ReqDTO.IsAml;
                                    IsExistclient.AmlupdatedDate = _commonHelper.GetCurrentDateTime();
                                    if (updateClientPhase1ReqDTO.IsAml == true)
                                    {
                                        Amlmst amlmst = new Amlmst();
                                        amlmst.UserId = IsExistclient.Id;
                                        amlmst.CreatedOn = _commonHelper.GetCurrentDateTime();
                                        amlmst.ValidTill = _commonHelper.GetCurrentDateTime().AddMonths(validtillMonthsAML);
                                        _dbContext.Amlmsts.Add(amlmst);
                                        _dbContext.SaveChanges();
                                    }
                                }

                                if (IsDueDelegence != updateClientPhase1ReqDTO.IsDueDiligence)
                                {
                                    int validtillMonthsDueDiligence = Convert.ToInt32(_configaration.GetSection("DueDiligenceMonths").Value);
                                    IsExistclient.IsDueDiligence = updateClientPhase1ReqDTO.IsDueDiligence;
                                    IsExistclient.DueDiligenceUpdatedDate = _commonHelper.GetCurrentDateTime();
                                    if (updateClientPhase1ReqDTO.IsDueDiligence == true)
                                    {
                                        DueDiligenceMst dueDiligenceMst = new DueDiligenceMst();
                                        dueDiligenceMst.UserId = IsExistclient.Id;
                                        dueDiligenceMst.CreatedOn = _commonHelper.GetCurrentDateTime();
                                        dueDiligenceMst.ValidTill = _commonHelper.GetCurrentDateTime().AddMonths(validtillMonthsDueDiligence);
                                        _dbContext.DueDiligenceMsts.Add(dueDiligenceMst);
                                        _dbContext.SaveChanges();
                                    }
                                }

                                var userDocumentMst = _commonRepo.getUserDocumentList().Where(x => x.UserId == IsExistclient.Id).ToList();
                                if (userDocumentMst != null)
                                {
                                    _dbContext.UserDocumentMsts.RemoveRange(userDocumentMst);
                                    _dbContext.SaveChanges();
                                }

                                bool IsUploadSuccess = true;
                                string CurrentFileName = "";

                                if (updateClientPhase1ReqDTO.ClientDocuments != null)
                                {
                                    foreach (var item in updateClientPhase1ReqDTO.ClientDocuments)
                                    {
                                        var FileUploadResponse = _commonHelper.UploadBase64File(item.FileName, item.File, updateClientPhase1ReqDTO.ClientAccNo, 2);
                                        if (FileUploadResponse.StatusCode == HttpStatusCode.OK)
                                        {
                                            _dbContext.UserDocumentMsts.Add(new UserDocumentMst { UserId = IsExistclient.Id, DocumentTypeId = 1, DocumentPath = FileUploadResponse.Data, IsActive = true, IsDeleted = false, CreatedBy = updateClientPhase1ReqDTO.UpdatedBy, UpdatedBy = updateClientPhase1ReqDTO.UpdatedBy, CreatedDate = _commonHelper.GetCurrentDateTime(), UpdatedDate = _commonHelper.GetCurrentDateTime() });
                                            _dbContext.SaveChanges();
                                        }
                                        else
                                        {
                                            CurrentFileName = item.FileName;
                                            IsUploadSuccess = false;
                                        }
                                    }
                                }
                                if (IsUploadSuccess)
                                {
                                    scope.Complete();
                                    updateClientPhase1ResDTO.UserId = IsExistclient.ClientAccNo;

                                    commonResponse.Message = "User update successfully!";
                                    commonResponse.Status = true;
                                    commonResponse.StatusCode = HttpStatusCode.OK;
                                    commonResponse.Data = updateClientPhase1ResDTO;
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
                                commonResponse.Message = "Can not find user!";
                            }
                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "Mobile No., Email, Client Account No is already exist!";
                        }
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

        public CommonResponse UploadDocument(UploadDocumentReqDTO uploadDocumentReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (uploadDocumentReqDTO.UserId > 0)
                {
                    var documentDetail = _commonRepo.ALLUserList().FirstOrDefault(x => x.Id == uploadDocumentReqDTO.UserId);
                    if (documentDetail != null)
                    {
                        var CurrentDate = _commonHelper.GetCurrentDateTime();
                        dynamic FileCount = uploadDocumentReqDTO.Files;
                        int ErrorStatusCount = 0;
                        UploadDocumentResDTO uploadDocumentResDTO = new UploadDocumentResDTO();
                        if (FileCount != null)
                        {
                            var file = uploadDocumentReqDTO.Files;

                            _commonHelper.AddLog("AddProfilePhoto :: FileName :: " + file.FileName);
                            FileInfo fileInfo = new FileInfo(file.FileName);
                            string FileExtension = fileInfo.Extension.ToLower();
                            string mimeType = _commonHelper.GetMimeType(file.FileName);

                            if (FileExtension == ".jfif")
                            {
                                FileExtension = ".jpg";
                            }
                            bool validateFileSize = false;
                            bool validateFileExtension = false;
                            bool validateFileMIMEType = false;
                            string[] allowedFileExtensions = { ".jpg", ".jpeg", ".png" };  //.jpg , .jpeg , .png
                            string[] allowedFileMIMETypes = { "image/jpeg", "image/png" };
                            int allowedFileSize = 2000000;                                              // 2MB

                            validateFileSize = file.Length <= allowedFileSize ? true : false;
                            validateFileExtension = allowedFileExtensions.Contains(FileExtension) ? true : false;
                            validateFileMIMEType = allowedFileMIMETypes.Contains(mimeType) ? true : false;

                            if (validateFileSize && validateFileExtension && validateFileMIMEType)
                            {
                                string SubDirectoryPath = "";
                                if (fileInfo.Extension.ToLower() == ".pdf")
                                {
                                    SubDirectoryPath = Path.Combine(SubDirectoryPath, "PDF");
                                }

                                if (FileExtension == ".jpg" || FileExtension == ".jpeg" || FileExtension == ".png")
                                {
                                    SubDirectoryPath = Path.Combine(SubDirectoryPath, "Images");
                                }

                                string FileName = CurrentDate.ToString("yy") + CurrentDate.ToString("MM") + CurrentDate.ToString("dd") + CurrentDate.ToString("HH") + CurrentDate.ToString("mm") + CurrentDate.ToString("ss") + CurrentDate.ToString("fffffff") + uploadDocumentReqDTO.UserId.ToString() + FileExtension;

                                var response = _commonHelper.UploadFile(file, SubDirectoryPath, FileName);
                                if (response.Status)
                                {
                                    UserDocumentMst userDocumentMst = new UserDocumentMst();
                                    userDocumentMst.UserId = uploadDocumentReqDTO.UserId;
                                    userDocumentMst.DocumentTypeId = CommonConstant.ProfilePhoto;
                                    userDocumentMst.DocumentPath = response.Data;
                                    userDocumentMst.CreatedBy = uploadDocumentReqDTO.UserId;
                                    userDocumentMst.UpdatedBy = uploadDocumentReqDTO.UserId;
                                    userDocumentMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                    userDocumentMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                    userDocumentMst.IsActive = true;
                                    userDocumentMst.IsDeleted = false;

                                    _dbContext.UserDocumentMsts.Add(userDocumentMst);
                                    _dbContext.SaveChanges();
                                    string FullPath = Path.Combine(_commonHelper.GetRootPath(), userDocumentMst.DocumentPath);
                                    uploadDocumentResDTO.DocumentPath = FullPath;
                                }
                                else
                                {
                                    commonResponse.Data = response.Data;
                                    ErrorStatusCount++;
                                }
                            }
                            else
                            {
                                _commonHelper.AddLog("PhotoUpload :: FileSize : " + file.Length + " , FileExtension : " + FileExtension);
                                commonResponse.Message = "Invalid File.";
                                ErrorStatusCount++;
                            }
                            if (ErrorStatusCount == 0)
                            {
                                commonResponse.Status = true;
                                commonResponse.StatusCode = HttpStatusCode.OK;
                                commonResponse.Message = "Success.";
                                commonResponse.Data = uploadDocumentResDTO;
                            }
                        }
                    }
                    else
                    {
                        commonResponse.Message = "Invalid UserId.";
                    }
                }
                else
                {
                    commonResponse.Message = "Invalid UserId.";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GenerateAccountNo(GenerateAccountNoReqDTO generateAccountNoReqDTO)
        {

            CommonResponse commonResponse = new CommonResponse();
            GenerateAccountNoResDTO generateAccountNoResDTO = new GenerateAccountNoResDTO();
            string accountNo = string.Empty;
            try
            {
                var countryName = _configaration.GetSection("BaseCountryName").Value;
                var countryMst = _commonRepo.countryCustomList().Where(x => x.CountryId == generateAccountNoReqDTO.CountryId && x.CountryName.ToLower() == countryName.ToLower()).FirstOrDefault();

                if (countryMst != null)
                {
                    var clientDetails = _dbContext.UserMsts.Where(x => x.AccessCategoryId == 2 && x.Country == generateAccountNoReqDTO.CountryId && x.Office == generateAccountNoReqDTO.OfficeId).OrderByDescending(x => x.Id).ToList();

                    accountNo = Convert.ToString(clientDetails == null ? 1 : clientDetails.Count() + 1);

                    string ConvertaccNo = string.Format("{0:D5}", Convert.ToInt32(accountNo));

                    accountNo = generateAccountNoReqDTO.OfficeId + ConvertaccNo;

                    generateAccountNoResDTO.GenerateAccountNo = accountNo;

                    if (generateAccountNoResDTO != null)
                    {
                        commonResponse.Message = "Success";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = generateAccountNoResDTO;
                    }
                    else
                    {
                        commonResponse.Message = "Data Not Found.";
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    var clientDetails = _dbContext.UserMsts.Where(x => x.AccessCategoryId == 2 && x.Country == generateAccountNoReqDTO.CountryId).OrderByDescending(x => x.Id).ToList();

                    accountNo = Convert.ToString(clientDetails == null ? 1 : clientDetails.Count() + 1);

                    string ConvertaccNo = string.Format("{0:D5}", Convert.ToInt32(accountNo));

                    //var countrydata = _dbContext.CountryMsts.Where(x => x.CountryId == generateAccountNoReqDTO.CountryId).FirstOrDefault();
                    var countrydata = (from countryList in _commonRepo.countryCustomList()
                                       where countryList.CountryId == generateAccountNoReqDTO.CountryId
                                       join countries in _dbContext.CountryMsts on countryList.CountryName.ToLower() equals countries.CountryName.ToLower() into country
                                       from all in country.DefaultIfEmpty()
                                       select new CountryMst
                                       {
                                           CountryId = countryList.CountryId,
                                           CountryName = countryList.CountryName,
                                           Iso2 = all.Iso2
                                       }).FirstOrDefault();

                    accountNo = countrydata.Iso2 + ConvertaccNo;
                    generateAccountNoResDTO.GenerateAccountNo = accountNo;


                    if (generateAccountNoResDTO != null)
                    {
                        commonResponse.Message = "Success";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = generateAccountNoResDTO;
                    }
                    else
                    {
                        commonResponse.Message = "Data Not Found.";
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllClientList()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetAllClientListResDTO> getAllClientListRes = new List<GetAllClientListResDTO>();

                getAllClientListRes = _commonRepo.getUserList().Where(x => !x.IsDeleted && x.AccessCategoryId == 2).Select(x => new GetAllClientListResDTO //2 Clients
                {
                    UserId = x.Id,
                    ClientAccNo = x.ClientAccNo,
                    Name = x.FirstName + " " + x.LastName,

                }).ToList();

                if (getAllClientListRes != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getAllClientListRes;
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