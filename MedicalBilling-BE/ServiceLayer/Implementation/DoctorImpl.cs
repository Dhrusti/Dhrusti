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
    public class DoctorImpl : IDoctor
    {
        private readonly DoctorBLL _doctorBLL;

        public DoctorImpl(DoctorBLL doctorBLL)
        { 
             _doctorBLL = doctorBLL;
        }

        public CommonResponse GetDoctorList()
        {
            return _doctorBLL.GetDoctorList();
        }

        public CommonResponse GetApptDoctorList()
        {
            return _doctorBLL.GetApptDoctorList();
        }
    }
}
