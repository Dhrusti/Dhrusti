using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IPortfolio
    {
        public CommonResponse GetPortfolioData(PortFolioDataReqDTO portFolioDataReqDTO);
        public CommonResponse GetPortfolioCSVData(GetPortfolioCSVDataReqDTO getPortfolioCSVDataReqDTO);
        public CommonResponse GetPortfolioClientData(GetPortfolioClientDataReqDTO getPortfolioClientDataReqDTO);
        public CommonResponse GetPortfolioClientList(GetPortfolioClientListReqDTO getPortfolioClientListReqDTO);
    }
}
