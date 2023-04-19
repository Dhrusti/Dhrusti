using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class AccountTypeBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        public AccountTypeBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse GetAllAccountType()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var accountTypeList = _commonRepo.accountTypeList().ToList();
                if (accountTypeList.Count > 0)
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
                commonResponse.Data = accountTypeList.Adapt<List<GetAccountTypeResDTO>>();
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAccountTypeById(GetAccountTypeReqDTO getAccountTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetAccountTypeResDTO getAccountTypeResDTO = new GetAccountTypeResDTO();
                getAccountTypeResDTO = _commonRepo.accountTypeList().Where(x => x.Id == getAccountTypeReqDTO.Id).First().Adapt<GetAccountTypeResDTO>();
                if (getAccountTypeResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getAccountTypeResDTO;
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

        public CommonResponse AddAccountType(AddAccountTypeReqDTO addAccountTypeReq)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddAccountTypeResDTO addAccountTypeResDTO = new AddAccountTypeResDTO();
            try
            {
                var accountType = _commonRepo.accountTypeList().Where(x => x.AccountType.ToLower() == addAccountTypeReq.AccountType.ToLower()).ToList();
                if (accountType.Count == 0)
                {
                    AccountTypeMst accountTypeMst = new AccountTypeMst();
                    accountTypeMst.AccountType = addAccountTypeReq.AccountType;
                    accountTypeMst.CreatedBy = addAccountTypeReq.UserId;
                    accountTypeMst.UpdatedBy = addAccountTypeReq.UserId;
                    accountTypeMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    accountTypeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    accountTypeMst.IsActive = true;
                    accountTypeMst.IsDeleted = false;

                    _dbContext.AccountTypeMsts.Add(accountTypeMst);
                    _dbContext.SaveChanges();

                    addAccountTypeResDTO.Id = accountTypeMst.Id;
                    addAccountTypeResDTO.AccountType = accountTypeMst.AccountType;

                    commonResponse.Message = "Data added successfully";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = addAccountTypeResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not add data";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateAccountType(UpdateAccountTypeReqDTO updateAccountTypeReq)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateAccountTypeResDTO updateAccountTypeResDTO = new UpdateAccountTypeResDTO();
            try
            {
                var accountDetail = _commonRepo.accountTypeList().FirstOrDefault(x => x.Id == updateAccountTypeReq.Id);
                if (accountDetail != null)
                {
                    AccountTypeMst accountTypeMst = accountDetail;
                    accountTypeMst.AccountType = updateAccountTypeReq.AccountType;
                    accountTypeMst.UpdatedBy = updateAccountTypeReq.UserId;
                    accountTypeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(accountTypeMst).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    updateAccountTypeResDTO.AccountType = accountTypeMst.AccountType;

                    commonResponse.Data = updateAccountTypeResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Successfully Updated...!!!";
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not update the data...!!!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeleteAccountType(DeleteAccountTypeReqDTO deleteAccountTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteAccountTypeResDTO deleteAccountTypeResDTO = new DeleteAccountTypeResDTO();
            try
            {
                var account = _commonRepo.accountTypeList().FirstOrDefault(x => x.Id == deleteAccountTypeReqDTO.Id);
                if (account != null)
                {
                    AccountTypeMst accountTypeMst = account;
                    accountTypeMst.Id = deleteAccountTypeReqDTO.Id;
                    accountTypeMst.UpdatedBy = deleteAccountTypeReqDTO.UserId;
                    accountTypeMst.IsDeleted = true;
                    accountTypeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dbContext.Entry(accountTypeMst).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dbContext.SaveChanges();

                    deleteAccountTypeResDTO.Id = accountTypeMst.Id;

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
