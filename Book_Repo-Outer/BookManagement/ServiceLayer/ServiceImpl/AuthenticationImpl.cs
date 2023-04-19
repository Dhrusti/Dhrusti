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
    public class AuthenticationImpl : IAuthentication
    {
        private readonly AuthenticationBLL _authenticationsBLL;
        public AuthenticationImpl(AuthenticationBLL authenticationsBLL)
        {
            _authenticationsBLL = authenticationsBLL;
        }
        public ResponseDTO GetAllUsers()
        {
          return _authenticationsBLL.GetAllUsersBLL();
        }
        public ResponseDTO GetUsersById(int id)
        {
            return _authenticationsBLL.GetUsersByIdBLL(id);
        }
        public ResponseDTO SaveUserReg(UserMstDTO userMstDTO)
        {
            ResponseDTO response = new ResponseDTO();
            response = _authenticationsBLL.SaveUserRegBLL(userMstDTO);
            return response;
        }
        public ResponseDTO DeleteUserDetail(int id)
        {
            return _authenticationsBLL.DeleteUserDetailBLL(id);
        }

        public ResponseDTO UpdateUser(UserMstDTO userMst)
        {
            return _authenticationsBLL.UpdateUserBLL(userMst);
        }
        public ResponseDTO Login(LoginDTO loginDTO)
        {
            ResponseDTO response = new ResponseDTO();
            response = _authenticationsBLL.LoginBLL(loginDTO);
            return response;
        }

        public ResponseDTO ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            ResponseDTO response = new ResponseDTO();
            response = _authenticationsBLL.ForgotPasswordBLL(forgotPasswordDTO);
            return response;
        }

        public ResponseDTO ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            ResponseDTO response = new ResponseDTO();
            response = _authenticationsBLL.ChangePasswordBLL(changePasswordDTO);
            return response;
        }


        public ResponseDTO ChangePassword(string email, string resetcode)
        {
            ResponseDTO response = new ResponseDTO();
            response = _authenticationsBLL.ChangePasswordBLL(email,resetcode);
            return response;
        }

        //public ResponseDTO UpdateValue(string TblUserMsts, string Password)
        //{
        //    ResponseDTO response = new ResponseDTO();
        //    response = _authenticationsBLL.UpdateValueBLL(TblUserMsts,Password);
        //    return response;
        //}

    }
}
