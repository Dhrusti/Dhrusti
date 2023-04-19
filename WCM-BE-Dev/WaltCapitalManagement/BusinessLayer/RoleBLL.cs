using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Helper.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;


namespace BusinessLayer
{
    public class RoleBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonHelper _commonHelper;
        private readonly CommonRepo _commonRepo;
        private readonly IConfiguration _configaration;

        public RoleBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration configuration)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _configaration = configuration;
        }

        public CommonResponse GetAllRole(GetRoleReqDTO getRoleReqDTO)
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


            int number = getRoleReqDTO.PageNumber == 0 ? (pagecount) : getRoleReqDTO.PageNumber;
            int size = getRoleReqDTO.PageSize == 0 ? (pageSize) : getRoleReqDTO.PageSize;
            bool orderby = getRoleReqDTO.Orderby == true ? (order) : getRoleReqDTO.Orderby;

            try
            {
                GetAllRoleResDTO getRoleResDTO = new GetAllRoleResDTO();
                List<RoleList> roleList = new List<RoleList>();

                // IQueryable<RoleMst> roleList;
                if (getRoleReqDTO.Status == true)
                {

                    roleList = (from roleL in _commonRepo.getRoleList()
                                select new RoleList
                                {
                                    Id = roleL.Id,
                                    RoleName = roleL.RoleName,
                                    RoleDescription = roleL.RoleDescription,
                                    RoleStatus = roleL.IsActive == true ? "Active" : "InActive",
                                    IsRoleAssigned = _dBContext.UserMsts.FirstOrDefault(x => x.Role == Convert.ToString(roleL.Id)) != null ? true : false
                                }).ToList();
                }
                else
                {

                    roleList = (from roleL in _commonRepo.getAllRoleList()
                                select new RoleList
                                {
                                    Id = roleL.Id,
                                    RoleName = roleL.RoleName,
                                    RoleDescription = roleL.RoleDescription,
                                    RoleStatus = roleL.IsActive == true ? "Active" : "InActive",
                                    IsRoleAssigned = _dBContext.UserMsts.FirstOrDefault(x => x.Role == Convert.ToString(roleL.Id)) != null ? true : false
                                }).ToList();
                }

                getRoleResDTO.TotalCount = roleList.Count();

                if (orderby)
                {
                    if (roleList.Count() <= size)
                    {
                        roleList = roleList.OrderBy(x => x.Id).ToList();
                    }
                    else
                    {
                        roleList = roleList.Skip((number - 1) * size)
                                .Take(size)
                                .OrderBy(x => x.Id).ToList();
                    }
                }
                else
                {
                    if (roleList.Count() <= size)
                    {
                        roleList = roleList.OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        roleList = roleList.OrderByDescending(x => x.Id).Skip((number - 1) * size)
                            .Take(size).ToList();
                    }
                }

                getRoleResDTO.roleList = roleList;


                if (roleList.Count() > 0)
                {
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Success.";
                    commonResponse.Data = getRoleResDTO;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Message = "Data not Found.";
                    commonResponse.Data = getRoleResDTO;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetRoleById(GetRoleByIdReqDTO getRoleByIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetRoleByIdResDTO getRoleByIdResDTO = new GetRoleByIdResDTO();

                getRoleByIdResDTO = _commonRepo.getAllRoleList().FirstOrDefault(x => x.Id == getRoleByIdReqDTO.Id && x.IsDeleted == false).Adapt<GetRoleByIdResDTO>();


                if (getRoleByIdResDTO != null)
                {
                    commonResponse.Message = "Success";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Data = getRoleByIdResDTO;
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Data = null;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse AddRole(AddRoleReqDTO addRoleReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            AddRoleResDTO addRoleResDTO = new AddRoleResDTO();
            try
            {
                var roleName = _commonHelper.GetEnumWiseFormattedRole(addRoleReqDTO.RoleName);

                bool IsAbsoluteRole = Enum.IsDefined(typeof(Roles), roleName);
                if (IsAbsoluteRole)
                {
                    var role = _commonRepo.getAllRoleList().Where(x => x.RoleName.ToLower() == addRoleReqDTO.RoleName.ToLower()).ToList();
                    if (role.Count == 0)
                    {
                        RoleMst roleMst = new RoleMst();
                        roleMst.RoleName = addRoleReqDTO.RoleName;
                        roleMst.RoleDescription = addRoleReqDTO.RoleDescription;
                        roleMst.IsActive = addRoleReqDTO.RoleStatus;
                        roleMst.CreatedBy = addRoleReqDTO.CreatedBy;
                        roleMst.UpdatedBy = addRoleReqDTO.CreatedBy;
                        roleMst.CreatedDate = _commonHelper.GetCurrentDateTime();
                        roleMst.UpdatedDate = _commonHelper.GetCurrentDateTime();
                        roleMst.IsDeleted = false;

                        _dBContext.RoleMsts.Add(roleMst);
                        _dBContext.SaveChanges();

                        addRoleResDTO.Id = roleMst.Id;
                        addRoleResDTO.RoleName = roleMst.RoleName;
                        addRoleResDTO.RoleDescription = roleMst.RoleDescription;
                        addRoleResDTO.RoleStatus = roleMst.IsActive;


                        commonResponse.Message = "Role added successfully!";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = addRoleResDTO;
                    }
                    else
                    {
                        commonResponse.Status = false;
                        commonResponse.StatusCode = HttpStatusCode.BadRequest;
                        commonResponse.Message = "Role Name already Exists!";
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Please enter valid Role!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateRole(UpdateRoleReqDTO updateRoleReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateRoleResDTO updateRoleResDTO = new UpdateRoleResDTO();
            try
            {
                var roleName = string.Empty;
                foreach (var item in updateRoleReqDTO.RoleName.Split(" "))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        roleName += string.IsNullOrEmpty(roleName) ? char.ToUpper(item[0]) + item.Substring(1) : "_" + char.ToUpper(item[0]) + item.Substring(1);
                    }
                    else
                    {
                        break;
                    }
                }

                bool IsAbsoluteRole = Enum.IsDefined(typeof(Roles), roleName);
                if (IsAbsoluteRole)
                {
                    var roleList = _commonRepo.getAllRoleList().Where(x => x.IsDeleted == false);
                    var userList = _commonRepo.getUserList().Where(x => x.Role == Convert.ToString(updateRoleReqDTO.Id)).ToList();
                    if (userList.Count == 0)
                    {
                        var roleDetails = roleList.FirstOrDefault(x => x.Id == updateRoleReqDTO.Id);
                        bool isduplicate = false;

                        var duplicatecheck = roleList.FirstOrDefault(x => x.Id != updateRoleReqDTO.Id && x.RoleName.ToLower() == updateRoleReqDTO.RoleName.ToLower());

                        if (duplicatecheck != null)
                        {
                            isduplicate = true;
                        }

                        if (roleDetails != null)
                        {
                            if (!isduplicate)
                            {
                                roleDetails.RoleName = updateRoleReqDTO.RoleName;
                                roleDetails.RoleDescription = updateRoleReqDTO.RoleDescription;
                                roleDetails.IsActive = updateRoleReqDTO.RoleStatus;
                                roleDetails.UpdatedBy = updateRoleReqDTO.UpdatedBy;
                                roleDetails.UpdatedDate = _commonHelper.GetCurrentDateTime();

                                _dBContext.Entry(roleDetails).State = EntityState.Modified;
                                _dBContext.SaveChanges();

                                updateRoleResDTO.RoleName = roleDetails.RoleName;
                                updateRoleResDTO.RoleDescription = roleDetails.RoleDescription;
                                updateRoleResDTO.RoleStatus = roleDetails.IsActive;
                                updateRoleResDTO.Id = roleDetails.Id;

                                commonResponse.Data = updateRoleResDTO;
                                commonResponse.Status = true;
                                commonResponse.StatusCode = HttpStatusCode.OK;
                                commonResponse.Message = "Updated Successfully!";
                            }
                            else
                            {
                                commonResponse.StatusCode = HttpStatusCode.BadRequest;
                                commonResponse.Message = "Role Name Already Exist!";
                            }
                        }
                        else
                        {
                            commonResponse.StatusCode = HttpStatusCode.BadRequest;
                            commonResponse.Message = "Data Not Found.";
                        }
                    }
                    else
                    {
                        var roleDetails = _commonRepo.getAllRoleList().FirstOrDefault(x => x.Id == updateRoleReqDTO.Id);


                        roleDetails.RoleDescription = updateRoleReqDTO.RoleDescription;
                        roleDetails.UpdatedDate = _commonHelper.GetCurrentDateTime();

                        _dBContext.Entry(roleDetails).State = EntityState.Modified;
                        _dBContext.SaveChanges();

                        updateRoleResDTO.Id = roleDetails.Id;
                        updateRoleResDTO.RoleName = roleDetails.RoleName;
                        updateRoleResDTO.RoleDescription = roleDetails.RoleDescription;
                        updateRoleResDTO.RoleStatus = roleDetails.IsActive;

                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Message = "Role Description Updated Successfully!"; //Tell by Tanmay Sadamast
                        commonResponse.Data = updateRoleResDTO;
                    }
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Please enter valid Role!";

                }

            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse UpdateRoleStatus(UpdateRoleStatusReqDTO updateRoleStatusReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            UpdateRoleStatusResDTO updateRoleStatusResDTO = new UpdateRoleStatusResDTO();
            try
            {
                var roleDetails = _commonRepo.getAllRoleList().FirstOrDefault(x => x.Id == updateRoleStatusReqDTO.Id && x.IsDeleted == false);
                if (roleDetails != null)
                {
                    roleDetails.UpdatedBy = updateRoleStatusReqDTO.UpdatedBy;
                    roleDetails.IsActive = updateRoleStatusReqDTO.IsActive;
                    roleDetails.UpdatedDate = _commonHelper.GetCurrentDateTime();

                    _dBContext.Entry(roleDetails).State = EntityState.Modified;
                    _dBContext.SaveChanges();

                    updateRoleStatusResDTO.RoleName = roleDetails.RoleName;
                    updateRoleStatusResDTO.Id = roleDetails.Id;
                    updateRoleStatusResDTO.IsActive = roleDetails.IsActive;

                    commonResponse.Data = updateRoleStatusResDTO;
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                    commonResponse.Message = "Updated Successfully!";
                }
                else
                {
                    commonResponse.Data = updateRoleStatusResDTO;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "Can not update the data";
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
