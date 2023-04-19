using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class ExternalAccountBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public ExternalAccountBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse AddExternalAccount(AddExternalAccountReqDTO addExternalAccountReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddExternalAccountResDTO addExternalAccountResDTO = new AddExternalAccountResDTO();
            try
            {
                var ExternalAccountList = _commonRepo.externalAccountList().Where(x => x.ServiceProvider == addExternalAccountReqDTO.ServiceProvider && x.Type == addExternalAccountReqDTO.Type);
                var IsExistServiceProvider = ExternalAccountList.Where(x => x.ClientId == addExternalAccountReqDTO.ClientId).ToList();
                var IsExistAccountNumber = ExternalAccountList.Where(x => x.AccountCode == addExternalAccountReqDTO.AccountCode).ToList();
                if (IsExistServiceProvider.Count == 0 && IsExistAccountNumber.Count == 0)
                {
                    ExternalAccountDetail externalAccountDetail = new ExternalAccountDetail();
                    externalAccountDetail.ServiceProvider = addExternalAccountReqDTO.ServiceProvider;
                    externalAccountDetail.ClientId = addExternalAccountReqDTO.ClientId;
                    externalAccountDetail.Type = addExternalAccountReqDTO.Type;
                    externalAccountDetail.AccountCode = addExternalAccountReqDTO.AccountCode;
                    externalAccountDetail.CreatedBy = addExternalAccountReqDTO.CreatedBy;
                    externalAccountDetail.UpdatedBy = addExternalAccountReqDTO.CreatedBy;
                    externalAccountDetail.CreatedDate = _commonHelper.GetCurrentDateTime();
                    externalAccountDetail.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    externalAccountDetail.IsActive = true;
                    externalAccountDetail.IsDeleted = false;

                    _dbContext.ExternalAccountDetails.Add(externalAccountDetail);
                    _dbContext.SaveChanges();

                    addExternalAccountResDTO.AccountId = externalAccountDetail.Id;

                    commonResponse.Message = "Account Added Successfully!";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Account Is Already Linked!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllExternalAccountByUserId(GetAllExternalAccountReqDTO getAllExternalAccountReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetAllExternalAccountResDTO> getAllExternalAccountResDTO = new List<GetAllExternalAccountResDTO>();

                getAllExternalAccountResDTO = (from ea in _commonRepo.externalAccountList()
                                               where (ea.ClientId == getAllExternalAccountReqDTO.ClientId)
                                               join spm in _dbContext.ServiceProviderMsts on ea.ServiceProvider equals spm.Id
                                               join sptm in _dbContext.ServiceProviderTypeMsts on ea.Type equals sptm.Id
                                               select new GetAllExternalAccountResDTO
                                               {
                                                   Id = ea.Id,
                                                   AccountCode = ea.AccountCode != null ? ea.AccountCode : string.Empty,
                                                   ServiceProvider = (int?)ea.ServiceProvider ?? 0,
                                                   ServiceProviderName = spm.ServiceProvider != null ? spm.ServiceProvider : string.Empty,
                                                   ServiceProviderType = (int?)ea.Type ?? 0,
                                                   ServiceProviderTypeName = sptm.ServiceProviderType != null ? sptm.ServiceProviderType : string.Empty,
                                                   ClientId = (int?)ea.ClientId ?? 0
                                               }).ToList().Adapt<List<GetAllExternalAccountResDTO>>();

                if (getAllExternalAccountResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getAllExternalAccountResDTO;
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

        public CommonResponse DeleteExternalAccount(DeleteExternalAccountReqDTO deleteExternalAccountReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteExternalAccountResDTO deleteExternalAccountResDTO = new DeleteExternalAccountResDTO();
            try
            {
                var ExternalAccount = _dbContext.ExternalAccountDetails.FirstOrDefault(x => x.Id == deleteExternalAccountReqDTO.ExternalAccountId);
                if (ExternalAccount != null)
                {
                    ExternalAccountDetail externalAccountDetail = new ExternalAccountDetail();
                    ExternalAccount.UpdatedBy = deleteExternalAccountReqDTO.DeletedBy;
                    ExternalAccount.IsDeleted = true;
                    ExternalAccount.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(ExternalAccount).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    deleteExternalAccountResDTO.UserId = ExternalAccount.ClientId;

                    commonResponse.Data = deleteExternalAccountResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Deleted Successfully";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not delete the data";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateupdateExternalAccount(UpdateExternalAccountReqDTO updateExternalAccountReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateExternalAccountResDTO updateExternalAccountResDTO = new UpdateExternalAccountResDTO();
            try
            {
                var ExternalAccountList = _commonRepo.externalAccountList().Where(x => x.Id != updateExternalAccountReqDTO.Id);
                ExternalAccountDetail externalAccountDetails = _commonRepo.externalAccountList().FirstOrDefault(x => x.Id == updateExternalAccountReqDTO.Id);
                if (externalAccountDetails != null)
                {
                    var IsExistServiceProvider = ExternalAccountList.Where(x => x.ServiceProvider == updateExternalAccountReqDTO.ServiceProvider && x.Type == updateExternalAccountReqDTO.Type && x.ClientId == externalAccountDetails.ClientId).ToList();
                    var IsExistAccountNumber = ExternalAccountList.Where(x => x.ServiceProvider == updateExternalAccountReqDTO.ServiceProvider && x.Type == updateExternalAccountReqDTO.Type && x.AccountCode == updateExternalAccountReqDTO.AccountCode).ToList();
                    if (IsExistServiceProvider.Count == 0 && IsExistAccountNumber.Count == 0)
                    {
                        externalAccountDetails.ServiceProvider = updateExternalAccountReqDTO.ServiceProvider;
                        externalAccountDetails.AccountCode = updateExternalAccountReqDTO.AccountCode;
                        externalAccountDetails.Type = updateExternalAccountReqDTO.Type;
                        externalAccountDetails.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        externalAccountDetails.UpdatedBy = updateExternalAccountReqDTO.UpdatedBy;

                        _dbContext.Entry(externalAccountDetails).State = EntityState.Modified;
                        _dbContext.SaveChanges();

                        updateExternalAccountResDTO.Id = externalAccountDetails.Id;

                        commonResponse.Data = updateExternalAccountResDTO;
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Successfully Updated!";
                    }
                    else
                    {
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Account Is Already Linked!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Data Not Found!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllReportData(int ServiceProviderId, int ServiceProviderTypeId)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetAllServiceProviderReportsResDTO> getAllServiceProviderReportsList = new List<GetAllServiceProviderReportsResDTO>();

                var ExternalAccountList = _commonRepo.externalAccountList();
                var UserList = _commonRepo.getUserList();
                var CustomCountryList = _commonRepo.countryCustomList();
                var RoleList = _commonRepo.getRoleList();
                var csvLog = _commonRepo.getPPMCSVLogList();
                var csvData = _commonRepo.getPPMCSVDataList().Where(x => !string.IsNullOrWhiteSpace(x.AccountNo));

                int currentCSVId = 0;
                var currentCSV = csvLog.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                if (currentCSV != null)
                {
                    currentCSVId = currentCSV.Id;
                }
                var currentCSVDataList = csvData.Where(x => x.CsvfileId == currentCSVId).ToList();

                if (ServiceProviderId > 0)
                {
                    ExternalAccountList = ExternalAccountList.Where(x => x.ServiceProvider == ServiceProviderId);
                }
                if (ServiceProviderTypeId > 0)
                {
                    ExternalAccountList = ExternalAccountList.Where(x => x.Type == ServiceProviderTypeId);
                }

                var ExternalAccountFilteredList = ExternalAccountList.ToList();
                var ClientIdList = ExternalAccountFilteredList.Select(x => x.ClientId).Distinct().ToList();
                var ClientFilteredList = UserList.Where(x => ClientIdList.Contains(x.Id) && x.Role == null && x.AccessCategoryId == 2).ToList();
                var IFAIdList = ClientFilteredList.Select(x => x.Ifa).ToList();
                var IFAFilteredList = UserList.Where(x => IFAIdList.Contains(x.Id) && x.AccessCategoryId == 3).ToList(); //AccessCategoryId = 3 = IFA

                GetAllServiceProviderReportsResDTO reportsResDTO;
                foreach (var item in ClientFilteredList)
                {
                    var IFADetails = IFAFilteredList.FirstOrDefault(x => x.Id == item.Ifa);
                    var ExterAccountDetails = ExternalAccountFilteredList.FirstOrDefault(x => x.ClientId == item.Id);
                    var CountryDetail = CustomCountryList.FirstOrDefault(x => x.CountryName.ToLower() == "south africa");

                    reportsResDTO = new GetAllServiceProviderReportsResDTO();
                    reportsResDTO.Id = item.Id;
                    reportsResDTO.Salutation = item.Salutation;
                    reportsResDTO.FirstName = item.FirstName;
                    reportsResDTO.MiddleName = item.MiddleName;
                    reportsResDTO.LastName = item.LastName;
                    reportsResDTO.ExternalAccountNo = ExterAccountDetails != null ? ExterAccountDetails.AccountCode : "";
                    reportsResDTO.ClientAccountNo = item.ClientAccNo;
                    reportsResDTO.Email = item.Email;
                    reportsResDTO.Mobile = item.MobileNo;
                    reportsResDTO.CreatedDate = item.CreatedDate;
                    if (ExterAccountDetails != null)
                    {
                        var CSVDetail = currentCSVDataList.FirstOrDefault(x => x.AccountNo == ExterAccountDetails.AccountCode && x.Category == ServiceProviderCategoryConstant.Total_Holdings);
                        if (CSVDetail != null)
                        {
                            reportsResDTO.AccountValue = Convert.ToDecimal(CSVDetail.Value);
                            reportsResDTO.InvestmentValue = Convert.ToDecimal(CSVDetail.Value);
                            reportsResDTO.PortfolioValue = Convert.ToDecimal(CSVDetail.Value);
                        }
                    }

                    reportsResDTO.AccountValueStr = _commonHelper.GetFormatedDecimal(reportsResDTO.AccountValue);
                    reportsResDTO.InvestmentValueStr = _commonHelper.GetFormatedDecimal(reportsResDTO.InvestmentValue);
                    reportsResDTO.PortfolioValueStr = _commonHelper.GetFormatedDecimal(reportsResDTO.PortfolioValue);
                    if (CountryDetail != null)
                        reportsResDTO.Currency = CountryDetail.CountryId == item.Country ? "ZAR" : "USD";
                    if (IFADetails != null)
                    {
                        var RoleDetail = RoleList.FirstOrDefault(x => x.Id == Convert.ToInt32(IFADetails.Role));
                        if (RoleDetail != null && _commonHelper.GetEnumWiseFormattedRole(RoleDetail.RoleName) == Roles.Portfolio_Manager.ToString())
                        {
                            reportsResDTO.IFAId = item.Ifa;
                            reportsResDTO.IFAFirstName = IFADetails.FirstName;
                            reportsResDTO.IFAMiddleName = IFADetails != null ? IFADetails.MiddleName : "";
                            reportsResDTO.IFALastName = IFADetails.LastName;
                        }
                    }

                    getAllServiceProviderReportsList.Add(reportsResDTO);
                }

                if (getAllServiceProviderReportsList.Count > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getAllServiceProviderReportsList;
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
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
