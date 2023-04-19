using AutoMapper;
using BookManagement.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace BookManagement.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfile _userprofile;
        private readonly IMapper _imapper;
        public UserProfileController(IUserProfile iuserprofile, IMapper imapper, IHttpContextAccessor httpContext)
        {
            this._userprofile = iuserprofile;
            this._imapper = imapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserProfile(int userid)
        {
            UserMstModel userMstModel = new UserMstModel();
            if (userid > 0)
            {
                //Edit mode
                var response = this._userprofile.GetUserProfileById(userid);
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
    }
}
