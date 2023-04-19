using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class DashBoardImpl: IDashBoard
    {
        private readonly DashBoardBLL _dashBoardBLLL;
        public DashBoardImpl(DashBoardBLL dashBoardBLL)
        {
          _dashBoardBLLL = dashBoardBLL;
        }

        public CommonResponse GetDashBoardWaltValuation()
        {
            return _dashBoardBLLL.GetDashBoardWaltValuation();
        } 
        public CommonResponse GetDashBoardOffice()
        {
            return _dashBoardBLLL.GetDashBoardOffice();
        }
        public CommonResponse MobileGetDashboard(MobileGetDashboardReqDTO mobileGetDashboardReqDTO)
        {
            return _dashBoardBLLL.MobileGetDashboard(mobileGetDashboardReqDTO);
        }
    }
}
