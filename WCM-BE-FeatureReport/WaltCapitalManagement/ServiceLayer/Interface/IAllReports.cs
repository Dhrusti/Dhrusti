using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IAllReports
    {
        public CommonResponse GetTradeStationClientList(GetTradeStationClientListReqDTO getTradeStationClientListReqDTO);

        public CommonResponse InteractiveBrokersClientList(GetInteractiveBrokersClientListReqDTO getInteractiveBrokersClientListReqDTO);

        public CommonResponse AllenGrayClientList(GetAllenGrayClientListReqDTO getAllenGrayClientListReqDTO);
    }
}
