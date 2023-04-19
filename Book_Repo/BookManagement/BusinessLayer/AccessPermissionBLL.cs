using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTOs;

namespace BusinessLayer
{
    public class AccessPermissionBLL
    {
        private readonly BookMgtDBContext _context;
        public AccessPermissionBLL(BookMgtDBContext context)
        {
            this._context = context;
        }
        public ResponseDTO GetUserByIdBLL(int id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                UserMstDTO userMstDTO = new UserMstDTO();
                var res = _context.TblUserMsts.FirstOrDefault(x => x.UserId == id);

                if (res != null)
                {
                    userMstDTO.UserId = res.UserId;
                    userMstDTO.UserName = res.UserName;
                    //userPermission.Email = res.Email;
                    //userPermission.Add = res.Add == 1 ? true : false;
                    //userPermission.Edit = res.Edit == 1 ? true : false;
                    //userPermission.Delete = res.Delete == 1 ? true : false;
                    //userPermission.View = res.View == 1 ? true : false;

                }
                response.Data = userMstDTO;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.Data = ex;
            }
            return response;
        }
        public ResponseDTO SaveUserPermissionBLL(List<UserPermissionDTO> userPermissionDTO)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                if (userPermissionDTO.Count > 0)
                {
                    var userid = userPermissionDTO.Select(x => x.UserId).FirstOrDefault();
                    if (userid > 0)
                    {
                        var userpermission = _context.TblUserAccessPermissions.Where(x => x.UserId == userid).ToList();
                        if (userpermission.Count > 0)
                        {
                            _context.TblUserAccessPermissions.RemoveRange(userpermission);
                            _context.SaveChanges();
                        }
                        List<TblUserAccessPermission> tblUserAccessPermissionList = new List<TblUserAccessPermission>();
                        foreach (var item in userPermissionDTO)
                        {
                            TblUserAccessPermission tblUserAccessPermission = new TblUserAccessPermission();
                            tblUserAccessPermission.UserId = userid;
                            tblUserAccessPermission.Permissionid = item.Permissionid;
                            if(tblUserAccessPermission.AccessId == 4)
                            {
                                tblUserAccessPermission.AccessId = 1;
                            }
                            else
                            {
                                tblUserAccessPermission.AccessId = item.AccessId;
                            }
                            //tblUserAccessPermission.AccessId = item.AccessId;
                            tblUserAccessPermission.IsActive = true;
                            tblUserAccessPermission.IsDeleted = false;
                            tblUserAccessPermissionList.Add(tblUserAccessPermission);
                        }
                        _context.TblUserAccessPermissions.AddRange(tblUserAccessPermissionList);
                        _context.SaveChanges();
                    }
                }
                response.Data = _context.TblUserAccessPermissions;
                response.Status = true;
                response.Message = "Saved Successfully...!!!";
            }
            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetPermissionBLL()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                List<TblPermissionMst> PermissionList = new List<TblPermissionMst>();
                PermissionList = _context.TblPermissionMsts.Where(x => x.IsDeleted == false).ToList();
                List<PermissionDTO> dtoList = PermissionList.Select(d => new PermissionDTO
                {
                    Pid = d.Pid,
                    PermissionName = d.PermissionName,
                }).OrderBy(x => x.Pid).ToList();

                response.Data = dtoList;
                response.Message = "Success";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
            }
            return response;
        }
        public ResponseDTO UserAccessPermissionbyIdBLL(int userid)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {


                UserAccessPermissionDTO userAccessPermissionDTO = new UserAccessPermissionDTO();
                UserMstDTO userMstDTO = new UserMstDTO();
                userMstDTO.UserId = userid;
                userAccessPermissionDTO.userMstDTO = userMstDTO;



                List<TblAccessMst> accesslist = new List<TblAccessMst>();
                accesslist = _context.TblAccessMsts.Where(x => x.IsDeleted == false).ToList();
                List<AccessDTO> accessDTO = accesslist.Select(a => new AccessDTO
                {
                    AccessId = a.AccessId,
                    AccessName = a.AccessName,
                }).ToList();
                userAccessPermissionDTO.accessDTO = accessDTO;


                List<TblPermissionMst> permissionMst = new List<TblPermissionMst>();
                permissionMst = _context.TblPermissionMsts.Where(x => x.IsDeleted == false).ToList();
                List<PermissionDTO> permissionDTO = permissionMst.Select(p => new PermissionDTO
                {
                    Pid = p.Pid,
                    PermissionName = p.PermissionName,
                }).ToList();
                userAccessPermissionDTO.permissionDTO = permissionDTO;

                List<TblUserAccessPermission> useraccesspermissionlist = new List<TblUserAccessPermission>();
                useraccesspermissionlist = _context.TblUserAccessPermissions.Where(x => x.IsDeleted == false).ToList();
                List<UserPermissionDTO> userpermissionDTO = useraccesspermissionlist.Select(d => new UserPermissionDTO
                {
                    Id = d.Id,
                    UserId = d.UserId,
                    Permissionid = d.Permissionid,
                    AccessId = d.AccessId


                }).ToList();
                userAccessPermissionDTO.userPermissionDTO = userpermissionDTO;

                response.Data = userAccessPermissionDTO;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.Data = ex;
            }
            return response;
        }
        public ResponseDTO AccessPermissionbyIdBLL(int userid)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                List<TblUserAccessPermission> tblUserAccessPermissions = new List<TblUserAccessPermission>();
                tblUserAccessPermissions = _context.TblUserAccessPermissions.Where(x => x.UserId == userid).ToList();
                List<UserPermissionDTO> userdto = tblUserAccessPermissions.Select(d => new UserPermissionDTO
                {
                    UserId = d.UserId,
                    AccessId = d.AccessId,
                    Permissionid = d.Permissionid,
                }).ToList();

                //if(res != null)
                //{
                //userAccessPermissionDTOs.UserId = userid;
                //userAccessPermissionDTOs.AccessId = res.AccessId;
                //userAccessPermissionDTOs.Permissionid = res.Permissionid;
                //}
                //List<UserMstDTO> userDTOs = res.Select(p => new UserMstDTO
                //{
                //    UserId = p.UserId,
                //    FullName = p.FullName,
                //    Email = p.Email,
                //    UserName = p.UserName,
                //    Password = p.Password,
                //    Address = p.Address,
                //    ContactNumber = p.ContactNumber,
                //}).OrderByDescending(x => x.UserId).ToList();

                response.Data = userdto;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.Data = ex;
            }
            return response;
        }
    }
}
