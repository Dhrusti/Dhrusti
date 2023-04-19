using BussinessLayer;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class ClientImpl : IClient
    {
        private readonly ClientBLL _clientBLL;

        public ClientImpl(ClientBLL clientBLL)
        {
            _clientBLL = clientBLL;
        }

        public CommonResponse GetAllClient()
        {
            return _clientBLL.GetAllClient();
        }
    }
}
