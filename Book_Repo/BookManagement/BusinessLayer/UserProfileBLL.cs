using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DTOs;

namespace BusinessLayer
{
    public class UserProfileBLL
    {
        private readonly BookMgtDBContext _context;
        public UserProfileBLL(BookMgtDBContext context)
        {
            this._context = context;
        }

        public ResponseDTO GetUserProfileById(int id)
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
            ////}

        }
    }
}
