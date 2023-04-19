using DTO.ReqDTO;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IFundBenchMark
    {
        public CommonResponse GetAllFundBenchMark(GetFundBenchMarkReqDTO getFundBenchMarkReqDTO);
        public CommonResponse GetAllUpdateFundBenchMark(GetAllUpdateFundBenchMarkReqDTO getAllUpdateFundBenchMarkReqDTO);
        public CommonResponse AddFundBenchMark(AddFundBenchMarkReqDTO addFundBenchMarkReqDTO);
        public CommonResponse UpdateFundBenchMark(UpdateFundBenchMarkReqDTO updateFundBenchMarkReqDTO);
        public CommonResponse UpdateAddStatusFundBenchMark(UpdateStatusFundBenchMarkReqDTO updateStatusFundBenchMarkReqDTO);
        public CommonResponse UpdateRemoveStatusFundBenchMark(UpdateRemoveStatusFundBenchMarkReqDTO updateRemoveStatusFundBenchMarkReqDTO);
        public CommonResponse GetAllDashboarFundBenchMark(GetAllDashboardFundBenchMarkReqDTO getFundBenchMarkReqDTO);
    }
}
