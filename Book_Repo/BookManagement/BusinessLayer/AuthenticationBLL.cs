using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BookManagement.Models;
using DataLayer.Entities;
using DTOs;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer
{
    public class AuthenticationBLL
    {
        private readonly BookMgtDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticationBLL(BookMgtDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._context = context;
        }

        public ResponseDTO GetAllUsersBLL()
        {
            ResponseDTO response = new ResponseDTO();
            try
            {


                var res = _context.TblUserMsts.Where(x => x.IsDeleted == false).ToList();
                if (res != null)
                {
                    List<UserMstDTO> userDTOs = res.Select(p => new UserMstDTO
                    {
                        UserId = p.UserId,
                        RoleId = p.RoleId,
                        FullName = p.FullName,
                        Email = p.Email,
                        UserName = p.UserName,
                        Password = p.Password,
                        Address = p.Address,
                        ContactNumber = p.ContactNumber,
                    }).OrderByDescending(x => x.UserId).ToList();


                    if (userDTOs.Count > 0)
                    {
                        response.Message = "Record Found";
                        response.Status = true;
                        response.Data = userDTOs;
                    }
                    else
                    {
                        response.Message = "Record Not Found";
                        response.Status = false;
                        response.Data = null;
                    }
                }
            }

            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                response.Status = false;
                response.Data = ex;
            }
            return response;
        }

        public ResponseDTO GetUsersByIdBLL(int id)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                UserMstDTO userMstDTO = new UserMstDTO();
                var res = _context.TblUserMsts.FirstOrDefault(x => x.UserId == id);

                if (res != null)
                {
                    userMstDTO.UserId = res.UserId;
                    userMstDTO.FullName = res.FullName;
                    userMstDTO.Email = res.Email;
                    userMstDTO.UserName = res.UserName;
                    userMstDTO.Address = res.Address;
                    userMstDTO.ContactNumber = res.ContactNumber;
                    //userMstDTO.Add = res.Add;
                    //userMstDTO.Edit = res.Edit;
                    //userMstDTO.Delete = res.Delete;
                    //userMstDTO.View = res.View;
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

        public ResponseDTO SaveUserRegBLL(UserMstDTO userMstDTO)
        {
            ResponseDTO response = new ResponseDTO();



            try
            {
                var res = this._context.TblUserMsts.FirstOrDefault(x => x.Email == userMstDTO.Email || x.UserName == userMstDTO.UserName);
                TblUserMst userMst = new TblUserMst();

                if (res == null)
                {
                    userMst.UserId = userMstDTO.UserId;
                    userMst.FullName = userMstDTO.FullName;
                    userMst.Email = userMstDTO.Email;
                    userMst.UserName = userMstDTO.UserName;
                    userMst.Password = userMstDTO.Password;
                    userMst.RoleId = userMstDTO.RoleId;
                    userMst.Address = userMstDTO.Address;
                    userMst.ContactNumber = userMstDTO.ContactNumber;
                    userMst.RoleId = CommonConstant.User;
                    userMst.CreatedOn = DateTime.Now;
                    userMst.UpdateOn = DateTime.Now;
                    userMst.CreatedBy = userMstDTO.UpdateBy;
                    userMst.UpdateBy = userMstDTO.UpdateBy;
                    userMst.IsActive = true;
                    userMst.IsDeleted = false;

                    _context.TblUserMsts.Add(userMst);
                    _context.SaveChanges();

                    //response.Data = userMst;
                    response.Status = true;
                    response.Message = "Successfully Created...!!!";
                }
                else
                {
                    response.Status = false;
                    response.Message = "UserName Already Exist...!!!";
                }

            }

            catch (Exception ex)
            {
                response.Data = ex;
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public ResponseDTO DeleteUserDetailBLL(int id)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {
                TblUserMst userMst = new TblUserMst();
                var res = this._context.TblUserMsts.FirstOrDefault(x => x.UserId == id && x.RoleId != CommonConstant.Admin);

                if (res != null)
                {
                    if (res.RoleId != CommonConstant.Admin)
                    {
                        userMst = res;
                        userMst.IsActive = false;
                        userMst.IsDeleted = true;
                        userMst.UpdateOn = DateTime.Now;
                        _context.Entry(res).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();

                        response.Data = res;
                        response.Status = true;
                        response.Message = "Deleted Successfully...!!!";
                    }
                    else
                    {
                        response.Message = "This is Admin Record, You Can't update it....!!!";//"User Data Updated Successfully.";
                        response.Status = false;
                        response.Data = null;
                    }
                    
                }



            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.Data = null;

            }
            return response;
        }

        public ResponseDTO UpdateUserBLL(UserMstDTO userMst)
        {
            ResponseDTO response = new ResponseDTO();

            try
            {

                var res = _context.TblUserMsts.Where(x => x.UserId == userMst.UserId).FirstOrDefault();

                if (res != null)
                {
                    if (res.RoleId != CommonConstant.Admin)
                    {
                        res.FullName = userMst.FullName;
                        res.Email = userMst.Email;
                        res.UserName = userMst.UserName;
                        res.Address = userMst.Address;
                        res.ContactNumber = userMst.ContactNumber;
                        //res.UpdateBy=userMst.UpdateBy;
                        res.UpdateOn = DateTime.Now;

                        _context.Entry(res).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        response.Message = "Record Updated Successfully";//"User Data Updated Successfully.";
                        response.Status = true;
                        response.Data = res;
                    }
                    else
                    {
                        response.Message = "This is Admin Record, You Can't update it....!!!";//"User Data Updated Successfully.";
                        response.Status = false;
                        response.Data = null;
                    }
                    //AuthenticationBAL authenticationBAL = new AuthenticationBAL(_context, userMst.UserName, "User Updated");

                }
                else
                {
                    response.Message = "Record does not exists";
                    response.Status = false;
                    response.Data = null;

                    // return Request.CreateResponse(HttpStatusCode.OK, objResponseDTO);
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.Data = ex;
                //return Request.CreateResponse(HttpStatusCode.ExpectationFailed, objResponseDTO);
            }
            return response;
        }

        public ResponseDTO LoginBLL(LoginDTO loginDTO)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var result = this._context.TblUserMsts.FirstOrDefault(x => (x.Email == loginDTO.Email || x.UserName == loginDTO.UserName) && x.IsDeleted==false && x.Password == loginDTO.Password.ToString());
                if (result != null)
                {
                    UserMstDTO loginresponse = new UserMstDTO();
                    loginresponse.UserId = result.UserId;
                    loginresponse.RoleId = result.RoleId;
                    loginresponse.UserName = result.UserName;
                    loginresponse.FullName = result.FullName;
                    loginresponse.Email = result.Email;
                    loginresponse.Address = result.Address;
                    loginresponse.ContactNumber = result.ContactNumber;
                     
                    response.Data = loginresponse;
                    response.Message = "Logged In Successfully...!!!";
                    response.Status = true;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Incorrect Credentials...!!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
            }
            return response;

        }


        public ResponseDTO ForgotPasswordBLL(ForgotPasswordDTO forgotPasswordDTO)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                var res = this._context.TblUserMsts.Where(x => x.Email == forgotPasswordDTO.Email).FirstOrDefault();
                if (res != null)
                {
                    string ResetCode = Guid.NewGuid().ToString();
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("noreply@archesoftronix.com");
                    mail.To.Add(new MailAddress(forgotPasswordDTO.Email));
                    mail.Subject = "Hello...!!!";
                 
                    string value = _httpContextAccessor.HttpContext.Request.Host.Value;
                    var email = forgotPasswordDTO.Email;

                    mail.Body = "<b>Hey...!!! Forgot your Password ??? Click the link to reset the password </b><a href=http://" + value + "/Authentication/ChangePassword?ResetCode=" + ResetCode + "&Email=" + forgotPasswordDTO.Email + " target='_blank'>Click Me</a>";
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "mail1.archesoftronix.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential("noreply@archesoftronix.com", "N0123ply", "");
                    smtp.Send(mail);

                    response.Data = mail;
                    response.Message = "Email Sent Successfully...!!!";
                    response.Status = true;

                    res.ResetCode = ResetCode;
                    _context.Entry(res).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();

                }
                else
                {
                    response.Status = false;
                    response.Message = "Email Not Found...!!!";

                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
            }
            return response;
        }
        public ResponseDTO ChangePasswordBLL(ChangePasswordDTO changePasswordDTO)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
                
                var res = this._context.TblUserMsts.Where(x => x.ResetCode == changePasswordDTO.ResetCode).FirstOrDefault();
                if (res != null)
                {
                    res.Password = changePasswordDTO.NewPassword;
                    res.ResetCode = "0";
                    _context.SaveChangesAsync();
                    response.Status = true;
                    response.Message = "Password changed successfully";
                    return response;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Link Expired...!!!";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Data = ex;
            }
            return response;
        }

        public ResponseDTO ChangePasswordBLL(string email, string resetcode)
        {
            ResponseDTO response = new ResponseDTO();
            var message = "";
            var mail = _context.TblUserMsts.Where(x => x.Email == email).FirstOrDefault();
            if (mail != null)
            {
                ChangePasswordDTO model = new ChangePasswordDTO();
                model.ResetCode = resetcode;
                response.Data=model;
                response.Status=true;
                
            }
            else
            {
                response.Data = null;
                response.Status = false;
            }
            return response;
        }

        //public ResponseDTO UpdateValueBLL(string TblUserMsts, string Password)
        //{
        //    ResponseDTO response = new ResponseDTO();
        //    try
        //    {

        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //    return response;
        //}


    }
}
