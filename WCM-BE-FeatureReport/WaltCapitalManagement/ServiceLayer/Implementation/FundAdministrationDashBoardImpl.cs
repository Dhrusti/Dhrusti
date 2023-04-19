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
    public class FundAdministrationDashBoardImpl : IFundAdministrationDashBoard
    {
        private readonly FundAdministrationDashBoardBLL _fundAdministrationDashBoardBLL;
        public FundAdministrationDashBoardImpl(FundAdministrationDashBoardBLL fundAdministrationDashBoardBLL)
        {
            _fundAdministrationDashBoardBLL = fundAdministrationDashBoardBLL;

        }

        public CommonResponse GetFundAdministrationDashBoardByFundId(GetFundAdministrationDashBoardByFundIdReqDTO getFundAdministrationDashBoardByFundIdReqDTO)
        {
            return _fundAdministrationDashBoardBLL.GetFundAdministrationDashBoardByFundId(getFundAdministrationDashBoardByFundIdReqDTO);
        }
    }
}
