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
    public class ExtensionImpl :IExtension
    {
        private readonly ExtensionBLL _extensionBLL;

        public ExtensionImpl(ExtensionBLL extensionBLL)
        {
            _extensionBLL = extensionBLL;
        }

        public CommonResponse GetExtensionList()
        {
            return _extensionBLL.GetExtensionList();
        }

    }
}
