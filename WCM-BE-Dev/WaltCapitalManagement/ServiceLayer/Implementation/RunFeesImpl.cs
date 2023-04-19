using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class RunFeesImpl : IRunFees
    {
        private readonly RunFeesBLL _runFeesBLL;

        public RunFeesImpl(RunFeesBLL runFeesBLL)
        {
            _runFeesBLL = runFeesBLL;
        }

        public CommonResponse GetRunFees(GetRunFeesReqDTO getRunFeesReqDTO)
        {
            return _runFeesBLL.GetRunFees(getRunFeesReqDTO);
        }
        public CommonResponse CalculateRunFees(CalculateRunFeesReqDTO calculateRunFeesReqDTO)
        {
            return _runFeesBLL.CalculateRunFees(calculateRunFeesReqDTO);

        }

    }
}
