using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DTOs;
using ServiceLayer.IService;

namespace ServiceLayer.ServiceImpl
{
    public class UserProfileImpl : IUserProfile
    {
        private readonly UserProfileBLL _userProfileBLL;
        public UserProfileImpl(UserProfileBLL userProfileBLL)
        {
            _userProfileBLL = userProfileBLL;
        }
        public ResponseDTO GetUserProfileById(int id)
        {
            return _userProfileBLL.GetUserProfileById(id);
        }
    }
}
