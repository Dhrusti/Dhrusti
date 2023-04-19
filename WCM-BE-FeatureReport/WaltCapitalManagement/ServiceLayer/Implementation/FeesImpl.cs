using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class FeesImpl : IFees
    {
        private readonly FeesBLL _feesBLL;

        public FeesImpl(FeesBLL feesBLL)
        {
            _feesBLL = feesBLL;
        }

        public CommonResponse GetIFAFeeBreakdown(GetIFAFeeBreakdownReqDTO getIFAFeeBreakdownReqDTO)
        {
            return _feesBLL.GetIFAFeeBreakdown(getIFAFeeBreakdownReqDTO);
        }

        public CommonResponse GetClientDetailsByIfaId(GetClientDetailsByIfaIdReqDTO getClientDetailsByIfaIdReqDTO)
        {
            return _feesBLL.GetClientDetailsByIfaId(getClientDetailsByIfaIdReqDTO);
        }
        public CommonResponse GetIfaFeeReport(GetIfaFeeReportReqDTO getIfaFeeReportReqDTO)
        {
            return _feesBLL.GetIfaFeeReport(getIfaFeeReportReqDTO);
        }
    }
}
