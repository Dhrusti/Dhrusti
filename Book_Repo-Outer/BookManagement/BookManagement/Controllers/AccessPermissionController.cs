using AutoMapper;
using BookManagement.Models;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.IService;

namespace BookManagement.Controllers
{
    public class AccessPermissionController : Controller
    {

        private readonly IAccessPermission _accessPermission;
        private readonly IMapper _imapper;

        public AccessPermissionController(IAccessPermission iaccesspermission, IMapper imapper, IHttpContextAccessor httpContext)
        {
            this._accessPermission = iaccesspermission;
            this._imapper = imapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserPermission(int userid)
        {
            var res = _accessPermission.UserAccessPermissionbyId(userid);
            //res.Data = this._imapper.Map<UserAccessPermissionModel>(res.Data);
            return View(res.Data);
            //UserAccessPermissionDTO userAccessPermissionDTO = new UserAccessPermissionDTO();

            //UserAccessPermissionModel umodel = new UserAccessPermissionModel();
            //umodel.accessModel = new List<AccessModel>();
            //umodel.permissionModel = new List<PermissionModel>();
            //umodel.userPermissionModels = new List<UserPermissionModel>();
            //umodel.userMstModel = new UserMstModel();
            
            //return View(umodel);
        }

        [HttpPost]
        public IActionResult SaveUserPermission([FromBody]List<UserPermissionModel> userPermissionModel)
        {
            var res = _accessPermission.SaveUserPermission(_imapper.Map<List<UserPermissionDTO>>(userPermissionModel));
            //var userid = userPermissionModel.Select(x => x.UserId).FirstOrDefault();
            //ViewBag.message = "Saved Succesfully...!!!";
            return Json(res);
        }

        [HttpGet]
        public IActionResult UserAccessPermissionbyId(int userid)
        {
            var res = _accessPermission.UserAccessPermissionbyId(userid);
            res.Data = this._imapper.Map<UserAccessPermissionModel>(res.Data);
           
            return View(res.Data);
        }

        public IActionResult AccessPermissionbyId(int userid)
        {
            var res = _accessPermission.AccessPermissionbyId(userid);
            res.Data = this._imapper.Map<UserPermissionModel>(res.Data);

            return View(res.Data);
        }

    }
}
