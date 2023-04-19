using DTO.ReqDTO;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IRunFees
    {
        public CommonResponse GetRunFees(GetRunFeesReqDTO getRunFeesReqDTO);
        public CommonResponse CalculateRunFees(CalculateRunFeesReqDTO calculateRunFeesReqDTO);
    }
}
