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
    public class CallTypeImpl :ICallType
    {
        private readonly CallTypeBLL _callTypeBLL;

        public CallTypeImpl(CallTypeBLL callTypeBLL)
        { 
            _callTypeBLL = callTypeBLL;
        }

        public CommonResponse GetCallTypeList()
        {
            return _callTypeBLL.GetCallTypeList();
        }

     
    }
}
