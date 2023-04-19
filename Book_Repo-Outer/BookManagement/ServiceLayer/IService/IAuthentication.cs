using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace ServiceLayer.IService
{
    public interface IAuthentication
    {
        public ResponseDTO GetAllUsers();

        public ResponseDTO GetUsersById(int id);

        public ResponseDTO SaveUserReg(UserMstDTO userMstDTO);

        public ResponseDTO DeleteUserDetail(int id);
        
        public ResponseDTO UpdateUser(UserMstDTO userMst);

        public ResponseDTO Login(LoginDTO loginDTO);

        public ResponseDTO ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);

        public ResponseDTO ChangePassword(ChangePasswordDTO changePasswordDTO);
        public ResponseDTO ChangePassword(string email, string resetcode);

        //public ResponseDTO UpdateValue(string TblUserMsts, string Password);

    }
}
