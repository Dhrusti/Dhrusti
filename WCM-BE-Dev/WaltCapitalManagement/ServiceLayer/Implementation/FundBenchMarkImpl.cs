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
    public class FundBenchMarkImpl : IFundBenchMark
    {
        private readonly FundBenchMarkBLL _fundBenchMarkBLL;

        public FundBenchMarkImpl(FundBenchMarkBLL fundBenchMarkBLL)
        { 
            _fundBenchMarkBLL = fundBenchMarkBLL;
        }
        public CommonResponse GetAllFundBenchMark(GetFundBenchMarkReqDTO getFundBenchMarkReqDTO)
        {
            return _fundBenchMarkBLL.GetAllFundBenchMark(getFundBenchMarkReqDTO);
          
        }  public CommonResponse GetAllUpdateFundBenchMark(GetAllUpdateFundBenchMarkReqDTO getAllUpdateFundBenchMarkReqDTO)
        {
            return _fundBenchMarkBLL.GetAllUpdateFundBenchMark(getAllUpdateFundBenchMarkReqDTO);
          
        }
        public CommonResponse AddFundBenchMark(AddFundBenchMarkReqDTO addFundBenchMarkReqDTO)
        {
            return _fundBenchMarkBLL.AddFundBenchMark(addFundBenchMarkReqDTO);
        }
        public CommonResponse UpdateFundBenchMark(UpdateFundBenchMarkReqDTO updateFundBenchMarkReqDTO)
        {
            return _fundBenchMarkBLL.UpdateFundBenchMark(updateFundBenchMarkReqDTO);
        }

        public CommonResponse UpdateAddStatusFundBenchMark(UpdateStatusFundBenchMarkReqDTO updateStatusFundBenchMarkReqDTO)
        {
            return _fundBenchMarkBLL.UpdateAddStatusFundBenchMark(updateStatusFundBenchMarkReqDTO);
        }    
        public CommonResponse UpdateRemoveStatusFundBenchMark(UpdateRemoveStatusFundBenchMarkReqDTO updateRemoveStatusFundBenchMarkReqDTO)
        {
            return _fundBenchMarkBLL.UpdateRemoveStatusFundBenchMark(updateRemoveStatusFundBenchMarkReqDTO);
        }
        public CommonResponse GetAllDashboarFundBenchMark(GetAllDashboardFundBenchMarkReqDTO getAllDashboardFundBenchMarkReqDTO)
        {
            return _fundBenchMarkBLL.GetAllDashboarFundBenchMark(getAllDashboardFundBenchMarkReqDTO);
        }
    }
}
