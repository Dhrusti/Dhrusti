using DTO.ReqDTO;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IReportPortfolio
    {
        public CommonResponse GetPortfolioManagerFee(GetPortfolioManagerFeeReqDTO getPortfolioManagerFeeReqDTO);
        public CommonResponse GetPPMClientList(GetPPMClientListReqDTO getPPMClientListReqDTO);
        public CommonResponse GetPPMClientListTFSA(GetPPMClientListTFSAReqDTO getPPMClientListTFSAReqDTO);
    }
}
