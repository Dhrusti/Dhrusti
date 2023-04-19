using BusinessLayer;
using Helper;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
