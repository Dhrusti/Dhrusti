using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IDoctor
    {
        public CommonResponse GetDoctorList();
        public CommonResponse GetApptDoctorList();
    }
}
