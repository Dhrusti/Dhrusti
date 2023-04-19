using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IFees
    {
        public CommonResponse GetIFAFeeBreakdown(GetIFAFeeBreakdownReqDTO getIFAFeeBreakdownReqDTO);
        public CommonResponse GetClientDetailsByIfaId(GetClientDetailsByIfaIdReqDTO getClientDetailsByIfaIdReqDTO);
        public CommonResponse GetIfaFeeReport(GetIfaFeeReportReqDTO getIfaFeeReportReqDTO);
    }
}
