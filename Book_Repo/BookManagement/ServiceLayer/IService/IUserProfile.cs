using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace ServiceLayer.IService
{
    public interface IUserProfile
    {
        public ResponseDTO GetUserProfileById(int id);
    }
}
