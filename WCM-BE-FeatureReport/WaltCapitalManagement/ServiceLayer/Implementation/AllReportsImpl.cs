using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class AllReportsImpl : IAllReports
    {
        private readonly AllReportsBLL _allReportsBLL;
        public AllReportsImpl(AllReportsBLL allReportsBLL)
        {
            _allReportsBLL = allReportsBLL;
        }

        public CommonResponse GetTradeStationClientList(GetTradeStationClientListReqDTO getTradeStationClientListReqDTO)
        {
            return _allReportsBLL.GetTradeStationClientList(getTradeStationClientListReqDTO);
        }
        public CommonResponse InteractiveBrokersClientList(GetInteractiveBrokersClientListReqDTO getInteractiveBrokersClientListReqDTO)
        {
            return _allReportsBLL.InteractiveBrokersClientList(getInteractiveBrokersClientListReqDTO);
        }

        public CommonResponse AllenGrayClientList(GetAllenGrayClientListReqDTO getAllenGrayClientListReqDTO)
        {
            return _allReportsBLL.AllenGrayClientList(getAllenGrayClientListReqDTO);
        }

    }
}
