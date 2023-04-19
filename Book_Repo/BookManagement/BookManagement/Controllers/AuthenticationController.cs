
using AutoMapper;
using BookManagement.Models;
using DataLayer.Entities;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace BookManagement.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthentication _authentication;
        private readonly IMapper _imapper;
        private readonly IAccessPermission _accesspermission;
        private readonly BookMgtDBContext _context;
        public AuthenticationController(IAuthentication iauthentication, IMapper imapper, IAccessPermission accesspermission, IHttpContextAccessor httpContext)
        {
            this._authentication = iauthentication;
            this._accesspermission = accesspermission;
            this._imapper = imapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel loginModel)
        {
            ResponseDTO response = new ResponseDTO();
            response = _authentication.Login(this._imapper.Map<LoginDTO>(loginModel));
            if (response.Status)
            {
                UserMstDTO userMstDTO = new UserMstDTO();
                userMstDTO = response.Data;


                //HttpContext.Session.SetString("UserId", userMstDTO.UserId.ToString());
                //HttpContext.Session.SetString("UserName", userMstDTO.UserName);
                //HttpContext.Session.SetString("RoleId", userMstDTO.RoleId.ToString());
                //HttpContext.Session.SetString("Email", userMstDTO.Email);

                var res = _accesspermission.AccessPermissionbyId(userMstDTO.UserId);
                List<UserPermissionModel> userRoleList = new List<UserPermissionModel>();

                userRoleList = this._imapper.Map<List<UserPermissionModel>>(res.Data);

                HttpContext.Session.SetObject("User", userMstDTO);
                HttpContext.Session.SetObject("UserRoleList", userRoleList);

                var user = HttpContext.Session.GetObject<UserMstModel>("User");
                return RedirectToAction("DashboardView", "Dashboard");
            }
            ViewBag.message = response.Message;
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            //return RedirectToAction("HomePage", "Home");
            return RedirectToAction("HomePage", "Home");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            var response = _authentication.ForgotPassword(_imapper.Map<ForgotPasswordDTO>(forgotPasswordModel));
            if (response.Status == true)
            {
                ViewBag.message = response.Message;
            }
            else
            {
                ViewBag.message = response.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword(string email , string resetcode)
        {
            var response = _authentication.ChangePassword(email,resetcode);
            ChangePasswordModel model = new ChangePasswordModel();
            if(response.Status == true)
            {
                model.Email = email;
                model.ResetCode=resetcode;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if(ModelState.IsValid)
            {
                var response = _authentication.ChangePassword(this._imapper.Map<ChangePasswordDTO>(changePasswordModel));
                if (response.Status == true)
                {
                    ViewBag.message = response.Message;
                }
                else
                {
                    ViewBag.message = response.Message;
                    return View(changePasswordModel);
                }
                return View("Login");
            }
            return View(changePasswordModel);
            
        }

        #endregion

        #region Registration

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {

            var response = this._authentication.GetAllUsers();
            response.Data = this._imapper.Map<List<UserMstModel>>(response.Data);

            return View(response.Data);
        }
        
        public IActionResult Registration(int userid)
        {

            UserMstModel userMstModel = new UserMstModel();
            if (userid > 0)
            {
                //Edit mode
                var response = this._authentication.GetUsersById(userid);
                //DTO to Model 
                response.Data = this._imapper.Map<UserMstModel>(response.Data);
                if (response.Status)
                {
                    userMstModel = response.Data;
                }
            }
            else
            {
                //Add Mode
            }
            return View(userMstModel);
        }

        [HttpPost]
        public IActionResult SaveUserReg(UserMstModel userMstModel)
        {
            if(ModelState.IsValid)
            {
                int userid = (int)Convert.ToInt64(HttpContext.Session.GetString("UserId"));
                userMstModel.UpdateBy = userid;
                var response = _authentication.SaveUserReg(_imapper.Map<UserMstDTO>(userMstModel));
                //var usernameresponse=_authentication.get
                //var permissionresponse=_accesspermission.AccessPermissionbyId(userid);

                if (response.Status == true)
                {
                    ViewBag.Message = response.Message;
                }
                else
                {
                    ViewBag.Message = response.Message;
                }
                //if (userMstModel.RoleId == 1 && response.Status == true)
                //{
                //    ViewBag.message = response.Message;
                //    return RedirectToAction("GetAllUsers");
                //    //return View("Registration");
                //}
                //else
                //{
                //    ViewBag.message = response.Message;
                //    ViewBag.message = "User Registred Succesfully...!!!";
                //    return RedirectToAction("GetAllUsers");
                //}
                return View("Registration",userMstModel);
            }
            else
            {
                return RedirectToAction("Registration");
            }

        }
        [HttpGet]
        public IActionResult DeleteUserDetail(int id)
        {
            var res = _authentication.DeleteUserDetail(id);

            return RedirectToAction("GetAllUsers");

        }

        [HttpPost]
        public IActionResult UpdateUser(UserMstModel userMstModel)
        {
            var response = _authentication.UpdateUser(this._imapper.Map<UserMstDTO>(userMstModel));
            return RedirectToAction("GetAllUsers");
        }

        #endregion

        public bool CheckAcessPermission(int Permissionid, int Accessid)
        {
            bool HasAccessPermission = false;
            var user = HttpContext.Session.GetObject<UserMstModel>("User");
            var userRoleList = HttpContext.Session.GetObject<List<UserPermissionModel>>("UserRoleList");

            if (user != null)
            {
                if (userRoleList != null)
                {
                    userRoleList = userRoleList.Where(x => x.UserId == user.UserId).ToList();
                    if (userRoleList.Count > 0)
                    {
                        userRoleList = userRoleList.Where(x => x.Permissionid == Permissionid).ToList();
                        if (userRoleList.Count > 0)
                        {
                            var AccessDetail = userRoleList.FirstOrDefault(x => x.AccessId == Accessid);
                            if (AccessDetail != null)
                            {
                                HasAccessPermission = true;
                            }
                        }
                    }
                }
            }
            return HasAccessPermission;
        }


        [HttpPost]
        public bool UpdateValue(string TblUserMsts , string Password)
        {
            bool updatevalue = false;
            var user = _context.TblUserMsts.FirstOrDefault();
            if(user != null)
            {
                updatevalue = true;
            }
           
            return updatevalue;

        }

    }
}
