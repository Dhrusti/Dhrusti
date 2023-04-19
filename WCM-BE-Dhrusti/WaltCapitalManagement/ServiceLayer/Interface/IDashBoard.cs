using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IDashBoard
    {
        public CommonResponse GetDashBoardWaltValuation();
        public CommonResponse GetDashBoardOffice();
        public CommonResponse MobileGetDashboard(MobileGetDashboardReqDTO mobileGetDashboardReqDTO);
    }
}
