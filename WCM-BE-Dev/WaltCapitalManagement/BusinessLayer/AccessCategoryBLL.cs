using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using System.Net;
using System.Transactions;

namespace BusinessLayer
{
    public class AccessCategoryBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        public AccessCategoryBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
        }

        public CommonResponse AddAccessCategory(AccessCategoryReqDTO accessCategoryReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AccessCategoryResDTO accessCategoryResDTO = new AccessCategoryResDTO();
            try
            {
                if (accessCategoryReqDTO.TypeId > 0)
                {
                    var accessDetail = _commonRepo.accessCategoryTypeList().FirstOrDefault(x => x.Id == accessCategoryReqDTO.TypeId);
                    if (accessDetail != null)
                    {
                        var accessCategory = _commonRepo.accessCategoryList().Where(x => x.AccessCategory.ToLower() == accessCategoryReqDTO.AccessCategory.ToLower() && x.TypeId == accessCategoryReqDTO.TypeId).ToList();
                        if (accessCategory.Count == 0)
                        {
                            using (var scope = new TransactionScope())
                            {
                                AccessCategoryMst accessCategoryMst = new AccessCategoryMst();
                                accessCategoryMst.AccessCategory = accessCategoryReqDTO.AccessCategory;
                                accessCategoryMst.ParentId = accessCategoryReqDTO.ParentId;
                                accessCategoryMst.TypeId = accessCategoryReqDTO.TypeId;
                                accessCategoryMst.CreatedBy = accessCategoryReqDTO.CreatedBy;
                                accessCategoryMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                accessCategoryMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                                accessCategoryMst.IsActive = true;
                                accessCategoryMst.IsDeleted = false;

                                _dBContext.AccessCategoryMsts.Add(accessCategoryMst);
                                _dBContext.SaveChanges();

                                AccessCategoryPermissionMst accessCategoryPermissionMst = new AccessCategoryPermissionMst();
                                accessCategoryPermissionMst.GroupId = accessCategoryMst.Id;
                                accessCategoryPermissionMst.AccessableCategoryId = CommonConstant.Dashboard; // Minmum Permission for a Group
                                accessCategoryPermissionMst.IsActive = true;
                                accessCategoryPermissionMst.IsDeleted = false;
                                accessCategoryPermissionMst.CreatedBy = accessCategoryReqDTO.CreatedBy;
                                accessCategoryPermissionMst.UpdatedBy = accessCategoryReqDTO.CreatedBy;
                                accessCategoryPermissionMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                accessCategoryPermissionMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                                _dBContext.AccessCategoryPermissionMsts.Add(accessCategoryPermissionMst);
                                _dBContext.SaveChanges();

                                scope.Complete();
                                accessCategoryResDTO.Id = accessCategoryMst.Id;
                                accessCategoryResDTO.TypeId = accessCategoryMst.TypeId;
                                accessCategoryResDTO.ParentId = accessCategoryMst.ParentId;
                                accessCategoryResDTO.AccessCategory = accessCategoryMst.AccessCategory;

                                commonResponse.Message = "Data added Successfully!";
                                commonResponse.Status = true;
                                commonResponse.StatusCode = HttpStatusCode.OK;
                                commonResponse.Data = accessCategoryResDTO;
                            }
                        }
                        else
                        {
                            commonResponse.Status = false;
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "Already Data Available!";
                        }

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Invalid Type!";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse DeleteAccessCategory(DeleteAccessCategoryReqDTO deleteAccessCategoryReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            DeleteAccessCategoryResDTO deleteAccessCategoryResDTO = new DeleteAccessCategoryResDTO();
            try
            {
                var AccessCategory = _commonRepo.accessCategoryList().FirstOrDefault(x => x.Id == deleteAccessCategoryReqDTO.Id);
                if (AccessCategory != null)
                {
                    using (var scope = new TransactionScope())
                    {
                        AccessCategory.IsDeleted = true;
                        AccessCategory.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        _dBContext.Entry(AccessCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _dBContext.SaveChanges();

                        var accessCategoryPermissionList = _commonRepo.accessCategoryPermissionMsts().Where(x => x.GroupId == deleteAccessCategoryReqDTO.Id).ToList();

                        accessCategoryPermissionList.ForEach(x => x.IsDeleted = true);
                        _dBContext.AccessCategoryPermissionMsts.UpdateRange(accessCategoryPermissionList);
                        _dBContext.SaveChanges();

                        scope.Complete();
                        deleteAccessCategoryResDTO.Id = AccessCategory.Id;

                        commonResponse.Data = deleteAccessCategoryResDTO;
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Deleted Successfully!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Access Category Id Not Found!!!";
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
