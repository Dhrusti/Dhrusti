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
    public class ReportPortfolioImpl : IReportPortfolio
    {
        private readonly ReportPortfolioBLL _reportPortfolioBLL;
        public ReportPortfolioImpl(ReportPortfolioBLL reportPortfolioBLL)
        {
            _reportPortfolioBLL = reportPortfolioBLL;
        }
        public CommonResponse GetPortfolioManagerFee(GetPortfolioManagerFeeReqDTO getPortfolioManagerFeeReqDTO)
        {
            return _reportPortfolioBLL.GetPortfolioManagerFee(getPortfolioManagerFeeReqDTO);
        } 
        public CommonResponse GetPPMClientList(GetPPMClientListReqDTO getPPMClientListReqDTO)
        {
            return _reportPortfolioBLL.GetPPMClientList(getPPMClientListReqDTO);
        }
          public CommonResponse GetPPMClientListTFSA(GetPPMClientListTFSAReqDTO getPPMClientListTFSAReqDTO)
        {
            return _reportPortfolioBLL.GetPPMClientListTFSA(getPPMClientListTFSAReqDTO);
        }

    }
}
