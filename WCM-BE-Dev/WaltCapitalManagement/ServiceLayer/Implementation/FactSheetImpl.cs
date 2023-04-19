using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class FactSheetImpl : IFactSheet
    {
        private readonly FactSheetBLL _iFactSheetBLL;
        public FactSheetImpl(FactSheetBLL iFactSheetBLL)
        {
            _iFactSheetBLL = iFactSheetBLL;
        }

        public CommonResponse GetFactSheetById(GetFactSheetByIdReqDTO getFactSheetByIdReqDTO)
        {
            return _iFactSheetBLL.GetFactSheetById(getFactSheetByIdReqDTO);
        }

        public CommonResponse GetFactSheetByFundId(GetFactSheetByFundReqDTO getFactSheetByFundReqDTO)
        {
            return _iFactSheetBLL.GetFactSheetByFundId(getFactSheetByFundReqDTO);
        }
        public CommonResponse AddFactSheetDetails(AddFactSheetReqDTO addFactSheetReqDTO)
        {
            return _iFactSheetBLL.AddFactSheetDetails(addFactSheetReqDTO);
        }

        public CommonResponse UpdateFactSheet(UpdateFactSheetReqDTO updateFactSheetReqDTO)
        {
            return _iFactSheetBLL.UpdateFactSheet(updateFactSheetReqDTO);
        }
        public CommonResponse GetModelPortfolio(ModelPortfolioReqDTO modelPortfolioReqDTO)
        {
            return _iFactSheetBLL.GetModelPortfolio(modelPortfolioReqDTO);
        }
        public CommonResponse GetRiskStatistics(GetRiskStatisticsReqDTO getRiskStatisticsReqDTO)
        {
            return _iFactSheetBLL.GetRiskStatistics(getRiskStatisticsReqDTO);
        }
        public CommonResponse GetTopHoldings(GetTopHoldingsListReqDTO getTopHoldingsListReqDTO)
        {
            return _iFactSheetBLL.GetTopHoldings(getTopHoldingsListReqDTO);
        }

        public CommonResponse GetMonthlyPerformance(GetMonthlyPerformanceReqDTO getMonthlyPerformanceReqDTO)
        {
            return _iFactSheetBLL.GetMonthlyPerformance(getMonthlyPerformanceReqDTO);
        }
        public CommonResponse GetPortfolioPerformance(GetPortfolioPerformanceReqDTO getPortfolioPerformanceReqDTO)
        {
            return _iFactSheetBLL.GetPortfolioPerformance(getPortfolioPerformanceReqDTO);
        }

        public CommonResponse GetFactSheetFieldsFromUnit(GetFactSheetByUnitReqDTO getFactSheetByUnitReqDTO)
        {
            return _iFactSheetBLL.GetFactSheetFieldsFromUnit(getFactSheetByUnitReqDTO);
        }

        public CommonResponse GetUnitTypeByFundId(GetUnitTypeByFundIdReqDTO getUnitTypeByFundIdReqDTO)
        {
            return _iFactSheetBLL.GetUnitTypeByFundId(getUnitTypeByFundIdReqDTO);
        }
    }
}
