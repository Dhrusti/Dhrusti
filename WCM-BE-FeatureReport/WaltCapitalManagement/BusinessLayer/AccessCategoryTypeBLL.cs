using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using System.Net;

namespace BusinessLayer
{
    public class AccessCategoryTypeBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        public AccessCategoryTypeBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse AddAccessCategoryType(AccessCategoryTypeReqDTO accessCategoryTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AccessCategoryTypeResDTO accessCategoryTypeResDTO = new AccessCategoryTypeResDTO();
            try
            {
                var accessCategory = _commonRepo.accessCategoryTypeList().Where(x => x.AccessCategoryType.ToLower() == accessCategoryTypeReqDTO.AccessCategoryType.ToLower()).ToList();
                if (accessCategory.Count == 0)
                {
                    AccessCategoryTypeMst accessCategoryTypeMst = new AccessCategoryTypeMst();
                    accessCategoryTypeMst.AccessCategoryType = accessCategoryTypeReqDTO.AccessCategoryType;
                    accessCategoryTypeMst.CreatedBy = accessCategoryTypeReqDTO.CreatedBy;
                    accessCategoryTypeMst.UpdatedBy = accessCategoryTypeReqDTO.CreatedBy;
                    accessCategoryTypeMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                    accessCategoryTypeMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                    accessCategoryTypeMst.IsActive = true;
                    accessCategoryTypeMst.IsDeleted = false;

                    _dBContext.AccessCategoryTypeMsts.Add(accessCategoryTypeMst);
                    _dBContext.SaveChanges();

                    accessCategoryTypeResDTO.Id = accessCategoryTypeMst.Id;
                    accessCategoryTypeResDTO.AccessCategoryType = accessCategoryTypeMst.AccessCategoryType;

                    commonResponse.Message = "Data added Successfully...!!!";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = accessCategoryTypeResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not add Data...!!!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse DeleteAccessCategoryType(DeleteAccessCategoryTypeReqDTO deleteAccessCategoryTypeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteAccessCategoryTypeResDTO deleteAccessCategoryTypeResDTO = new DeleteAccessCategoryTypeResDTO();
            try
            {
                var AccessCategory = _commonRepo.accessCategoryTypeList().FirstOrDefault(x => x.Id == deleteAccessCategoryTypeReqDTO.Id);
                if (AccessCategory != null)
                {
                    // AccessCategoryTypeMst AccessCategory = new AccessCategoryTypeMst();
                    AccessCategory.Id = deleteAccessCategoryTypeReqDTO.Id;
                    AccessCategory.UpdatedBy = deleteAccessCategoryTypeReqDTO.Id;
                    AccessCategory.IsDeleted = true;
                    AccessCategory.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dBContext.Entry(AccessCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dBContext.SaveChanges();

                    deleteAccessCategoryTypeResDTO.Id = AccessCategory.Id;

                    commonResponse.Data = deleteAccessCategoryTypeResDTO;
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
