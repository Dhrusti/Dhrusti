using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class PortfolioImpl : IPortfolio
    {
        private readonly PortfolioBLL _portfolioBLL;
        public PortfolioImpl(PortfolioBLL portfolioBLL)
        {
            _portfolioBLL = portfolioBLL;
        }

        public CommonResponse GetPortfolioData(PortFolioDataReqDTO portFolioDataReqDTO)
        {
            return _portfolioBLL.GetPortfolioData(portFolioDataReqDTO);
        }
        public CommonResponse GetPortfolioCSVData(GetPortfolioCSVDataReqDTO getPortfolioCSVDataReqDTO)
        {
            return _portfolioBLL.GetPortfolioCSVData(getPortfolioCSVDataReqDTO);
        }

        public CommonResponse GetPortfolioClientData(GetPortfolioClientDataReqDTO getPortfolioClientDataReqDTO)
        {
            return _portfolioBLL.GetPortfolioClientData(getPortfolioClientDataReqDTO);
        }
        public CommonResponse GetPortfolioClientList(GetPortfolioClientListReqDTO getPortfolioClientListReqDTO)
        {
            return _portfolioBLL.GetPortfolioClientList(getPortfolioClientListReqDTO);
        }
    }
}
