using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using System.Data.Entity.Validation;
using System.Net;
using System.Transactions;

namespace BusinessLayer
{
    public class WatchBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;

        public WatchBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
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


        public CommonResponse GetAllPrivileges(GetAllPrivilegesReqDTO getAllPrivilegesReqDTO)//3
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                int layer = 1;
                List<AccessCategoryPermissionMst> accessCategoryPermissionMstList = new List<AccessCategoryPermissionMst>();
                if (getAllPrivilegesReqDTO.GroupId != 0)
                {
                    accessCategoryPermissionMstList = _dbContext.AccessCategoryPermissionMsts.Where(x => x.GroupId == getAllPrivilegesReqDTO.GroupId).ToList();
                }

                List<GetAllPrivilegesResDTO> level1 = _commonRepo.accessCategoryList().Where(x => x.TypeId == getAllPrivilegesReqDTO.TypeId && x.ParentId == 0).ToList().Adapt<List<GetAllPrivilegesResDTO>>();
                foreach (var item in level1)
                {
                    foreach (var permission in accessCategoryPermissionMstList)
                    {
                        item.IsSelected = item.IsSelected ? true : item.Id == permission.AccessableCategoryId ? true : false;
                    }
                    item.AllPrivileges = GetAccessCategoryChild(item, accessCategoryPermissionMstList, item.Id, layer + 1);
                    item.ParentId = item.Id;
                    item.Layer = layer;
                }

                if (level1 != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = level1;
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

        public CommonResponse GetAllGroups(GetAllGroupsReqDTO getAllGroupsReqDTO)//2
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var accessCategoryList = _commonRepo.accessCategoryList().Where(x => x.TypeId == getAllGroupsReqDTO.TypeId).ToList();

                if (accessCategoryList != null && accessCategoryList.Count > 0)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = accessCategoryList;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                }
                commonResponse.Data = accessCategoryList.Adapt<List<GetAllGroupsResDTO>>();
            }
            catch (Exception ex)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdatePrivileges(UpdatePrivilegesReqDTO updatePrivilegesReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (updatePrivilegesReqDTO != null && updatePrivilegesReqDTO.Privileges != null)
                {
                    var accessCategoryList = _commonRepo.accessCategoryList().Where(x => x.Id == updatePrivilegesReqDTO.GroupId && x.TypeId == 2).ToList();//2 "Group"
                    if (accessCategoryList != null && accessCategoryList.Count > 0)
                    {
                        using (var scope = new TransactionScope())
                        {
                            var accessCategoryPermissionList = _commonRepo.accessCategoryPermissionMsts().Where(x => x.GroupId == updatePrivilegesReqDTO.GroupId && x.AccessableCategoryId != CommonConstant.Dashboard).ToList();

                            _dbContext.AccessCategoryPermissionMsts.RemoveRange(accessCategoryPermissionList);
                            _dbContext.SaveChanges();

                            updatePrivilegesReqDTO.Privileges.Remove(CommonConstant.Dashboard);

                            List<AccessCategoryPermissionMst> accessCategoryPermissionMsts = new List<AccessCategoryPermissionMst>();
                            foreach (var accessCategory in updatePrivilegesReqDTO.Privileges)
                            {
                                AccessCategoryPermissionMst accessCategoryPermissionMst = new AccessCategoryPermissionMst();
                                accessCategoryPermissionMst.GroupId = updatePrivilegesReqDTO.GroupId;
                                accessCategoryPermissionMst.AccessableCategoryId = accessCategory;
                                accessCategoryPermissionMst.IsActive = true;
                                accessCategoryPermissionMst.IsDeleted = false;
                                accessCategoryPermissionMst.CreatedBy = updatePrivilegesReqDTO.UpdatedBy;
                                accessCategoryPermissionMst.UpdatedBy = updatePrivilegesReqDTO.UpdatedBy;
                                accessCategoryPermissionMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                                accessCategoryPermissionMst.UpdatedDate = _commonHelper.GetCurrentDateTime();

                                accessCategoryPermissionMsts.Add(accessCategoryPermissionMst);
                            }

                            _dbContext.AccessCategoryPermissionMsts.AddRange(accessCategoryPermissionMsts);
                            _dbContext.SaveChanges();

                            scope.Complete();

                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Permissions Updated Successfully!";
                        }

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Group Id Not Found!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Request Data Are Invalid!";
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddPrivilege(AddPrivilegeReqDTO addPrivilegeReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (addPrivilegeReqDTO != null && addPrivilegeReqDTO.GroupId != 0)
                {
                    var accessCategory = _commonRepo.accessCategoryList().Where(x => x.Id == addPrivilegeReqDTO.GroupId && x.TypeId == 2).FirstOrDefault();//2 "Group"
                    if (accessCategory != null)
                    {
                        var accessCategoryPermission = _commonRepo.accessCategoryPermissionMsts().Where(x => x.GroupId == addPrivilegeReqDTO.GroupId && x.AccessableCategoryId == addPrivilegeReqDTO.AccessCategoryId).FirstOrDefault();
                        if (accessCategoryPermission != null)
                        {
                            //remove child
                            var accessCategoryMstList = _commonRepo.accessCategoryList().Where(x => x.ParentId == addPrivilegeReqDTO.AccessCategoryId).ToList();

                            foreach (var item in accessCategoryMstList)
                            {
                                var accessCategoryPermission2 = _commonRepo.accessCategoryPermissionMsts().Where(x => x.AccessableCategoryId == item.Id).FirstOrDefault() ?? new AccessCategoryPermissionMst();
                                _dbContext.AccessCategoryPermissionMsts.Remove(accessCategoryPermission2);
                                _dbContext.SaveChanges();
                            }

                            _dbContext.AccessCategoryPermissionMsts.Remove(accessCategoryPermission);
                            _dbContext.SaveChanges();

                            //remove parent
                            var accessCategoryPermissionMst = (from ac in _commonRepo.accessCategoryList()
                                                               where (ac.Id == addPrivilegeReqDTO.AccessCategoryId)
                                                               join acpm in _commonRepo.accessCategoryPermissionMsts() on ac.ParentId equals acpm.AccessableCategoryId into acpmst
                                                               from all in acpmst.DefaultIfEmpty()
                                                               select all).FirstOrDefault();


                            if (accessCategoryPermissionMst != null)
                            {
                                _dbContext.AccessCategoryPermissionMsts.Remove(accessCategoryPermissionMst);
                                _dbContext.SaveChanges();
                            }



                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Permissions Removed Successfully!";
                        }
                        else
                        {
                            //add
                            AccessCategoryPermissionMst accessCategoryPermissionMst;
                            var accessCategoryMstList = _commonRepo.accessCategoryList().ToList();

                            List<AccessCategoryMst> level2 = _commonRepo.accessCategoryList().Where(x => x.Id == addPrivilegeReqDTO.AccessCategoryId).ToList().Adapt<List<AccessCategoryMst>>();
                            foreach (var subitem in level2)
                            {
                                List<AccessCategoryMst> accessCategoryPermissionMsts = _commonRepo.accessCategoryList().Where(x => x.ParentId == subitem.Id).ToList();

                                foreach (var permission in accessCategoryPermissionMsts)
                                {
                                    var res = _commonRepo.accessCategoryList().Where(ac => ac.ParentId == permission.Id).ToList();


                                }

                            }
                            commonResponse.Status = true;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Message = "Permissions Saved Successfully!";
                        }

                        commonResponse.Data = GetAllPrivileges(new GetAllPrivilegesReqDTO { TypeId = 3, GroupId = addPrivilegeReqDTO.GroupId }).Data;

                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Message = "Group Id Not Found!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Request Data Are Invalid!";
                }
            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetAllSelectedPrivileges(GetAllSelectedPrivilegesReqDTO getAllSelectedPrivilegeReqDTO)//3
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetAllSelectedPrivilegesResDTO getAllSelectedPrivilegeResDTO = new GetAllSelectedPrivilegesResDTO();
                List<AccessCategoryPermissionMst> accessCategoryPermissionMstList = new List<AccessCategoryPermissionMst>();
                if (getAllSelectedPrivilegeReqDTO.GroupId != 0)
                {
                    accessCategoryPermissionMstList = _commonRepo.accessCategoryPermissionMsts().Where(x => x.GroupId == getAllSelectedPrivilegeReqDTO.GroupId).ToList();
                }
                foreach (var item in accessCategoryPermissionMstList)
                {
                    AccessCategoryMst accessCategoryDetails = _commonRepo.accessCategoryList().Where(x => x.Id == item.AccessableCategoryId).FirstOrDefault() ?? new AccessCategoryMst();

                    if (accessCategoryDetails != null && accessCategoryDetails.ParentId == 0)
                    {
                        getAllSelectedPrivilegeResDTO.Id = accessCategoryDetails.Id;
                        getAllSelectedPrivilegeResDTO.AccessCategory = accessCategoryDetails.AccessCategory;
                        getAllSelectedPrivilegeResDTO.Layer = 1;
                    }
                    else if (accessCategoryDetails != null && accessCategoryDetails.ParentId != 0)
                    {
                        List<AccessCategoryPermissionMst> accessCategoryPermissionMstList2 = _commonRepo.accessCategoryPermissionMsts().Where(x => x.AccessableCategoryId == accessCategoryDetails.ParentId).ToList();

                        getAllSelectedPrivilegeResDTO.Id = accessCategoryDetails.Id;
                        getAllSelectedPrivilegeResDTO.AccessCategory = accessCategoryDetails.AccessCategory;
                        getAllSelectedPrivilegeResDTO.Layer = 1;

                        List<GetAllSelectedPrivilegesResDTO> getAllPrivilegesResDTOs = new List<GetAllSelectedPrivilegesResDTO>();
                        GetAllSelectedPrivilegesResDTO layer1;
                        foreach (var subItem in accessCategoryPermissionMstList2)
                        {
                            layer1 = new GetAllSelectedPrivilegesResDTO();
                            layer1.Id = subItem.Id;
                            layer1.ParentId = accessCategoryDetails.ParentId;
                            layer1.AccessCategory = Convert.ToString(subItem.AccessableCategoryId);
                            layer1.Layer = 1;
                            getAllPrivilegesResDTOs.Add(layer1);
                        }
                        getAllSelectedPrivilegeResDTO.AllPrivileges = getAllPrivilegesResDTOs;
                    }

                }

                if (getAllSelectedPrivilegeResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getAllSelectedPrivilegeResDTO;
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