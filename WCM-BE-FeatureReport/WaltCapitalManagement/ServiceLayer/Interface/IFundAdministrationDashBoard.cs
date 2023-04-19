using DTO.ReqDTO;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IFundAdministrationDashBoard
    {
        public CommonResponse GetFundAdministrationDashBoardByFundId(GetFundAdministrationDashBoardByFundIdReqDTO getFundAdministrationDashBoardByFundIdReqDTO);
    }
}
