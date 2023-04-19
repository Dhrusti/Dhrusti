using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IFactSheet
    {
        public CommonResponse GetFactSheetById(GetFactSheetByIdReqDTO getFactSheetByIdReqDTO);
        public CommonResponse GetFactSheetByFundId(GetFactSheetByFundReqDTO getFactSheetByFundReqDTO);
        public CommonResponse AddFactSheetDetails(AddFactSheetReqDTO addFactSheetReqDTO);
        public CommonResponse UpdateFactSheet(UpdateFactSheetReqDTO updateFactSheetReqDTO);
        public CommonResponse GetModelPortfolio(ModelPortfolioReqDTO modelPortfolioReqDTO);
        public CommonResponse GetRiskStatistics(GetRiskStatisticsReqDTO getRiskStatisticsReqDTO);
        public CommonResponse GetTopHoldings(GetTopHoldingsListReqDTO getTopHoldingsListReqDTO);
        public CommonResponse GetMonthlyPerformance(GetMonthlyPerformanceReqDTO getMonthlyPerformanceReqDTO);

        public CommonResponse GetPortfolioPerformance(GetPortfolioPerformanceReqDTO getPortfolioPerformanceReqDTO);
        public CommonResponse GetFactSheetFieldsFromUnit(GetFactSheetByUnitReqDTO getFactSheetByUnitReqDTO);
        public CommonResponse GetUnitTypeByFundId(GetUnitTypeByFundIdReqDTO getUnitTypeByFundIdReqDTO);
    }
}
