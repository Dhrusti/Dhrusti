using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Models
{
    public class HelperClass // : Controller
    {

        //private readonly IHttpContextAccessor _httpContext;

        //public HelperClass(IHttpContextAccessor httpContext)
        //{
        //    this._httpContext = httpContext;
        //}

        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ISession _session;
        //public HelperClass(IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _session = _httpContextAccessor.HttpContext.Session;
        //}
        //public bool CheckAcessPermission(int Permissionid, int Accessid)
        //{
        //    bool HasAccessPermission = false;
        //    var user = _httpContextAccessor.HttpContext.Session.GetObject<UserMstModel>("User");
        //    var userRoleList = _httpContextAccessor.HttpContext.Session.GetObject<List<UserPermissionModel>>("UserRoleList");

        //    if (user != null)
        //    {
        //        if (userRoleList != null)
        //        {
        //            userRoleList = userRoleList.Where(x => x.UserId == user.UserId).ToList();
        //            if (userRoleList.Count > 0)
        //            {
        //                userRoleList = userRoleList.Where(x => x.Permissionid == Permissionid).ToList();
        //                if (userRoleList.Count > 0)
        //                {
        //                    var AccessDetail = userRoleList.FirstOrDefault(x => x.AccessId == Accessid);
        //                    if (AccessDetail != null)
        //                    {                                       
        //                        HasAccessPermission = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return HasAccessPermission;
        //}

        //public bool HasPermission(int Permissionid, int Accessid)
        //{
        //    CheckAcessPermission checkAcessPermission = new CheckAcessPermission();
        //}



    }
}
