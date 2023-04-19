using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class DurationImpl : IDuration
    {
        private readonly DurationBLL _durationBLL;

        public DurationImpl(DurationBLL durationBLL)
        {
            _durationBLL = durationBLL;
        }
        public CommonResponse AddDuration(AddDurationReqDTO addDurationReqDTO)
        {
            return _durationBLL.AddDuration(addDurationReqDTO);
        }
    }
}
