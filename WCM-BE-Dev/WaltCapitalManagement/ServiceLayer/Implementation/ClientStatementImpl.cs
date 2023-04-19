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
    public class ClientStatementImpl : IClientStatement
    {
        private readonly ClientStatementBLL _clientStatementBLL;

        public ClientStatementImpl(ClientStatementBLL clientStatementBLL)
        {
            this._clientStatementBLL = clientStatementBLL;

        }

        public CommonResponse GetReportType()
        {
            return _clientStatementBLL.GetReportType();
        }  
        public CommonResponse GetClientStatementByFundId(GetClientStatementByFundIdReqDTO getClientStatementByFundIdReqDTO)
        {
            return _clientStatementBLL.GetClientStatementByFundId(getClientStatementByFundIdReqDTO);
        }
        public CommonResponse GetClientStatementReport(GetClientStatementReportReqDTO getClientStatementReportReqDTO)
        {
            return _clientStatementBLL.GetClientStatementReport(getClientStatementReportReqDTO);
        }
    }
}
