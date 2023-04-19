using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WaltCapitalManagementWebAPI.ViewModels.ReqViewModels;
using WaltCapitalManagementWebAPI.ViewModels.ResViewModels;

namespace WaltCapitalManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactSheetController : ControllerBase
    {
        private readonly IFactSheet _iFactSheet;
        public FactSheetController(IFactSheet iFactSheet)
        {
            _iFactSheet = iFactSheet;
        }


        [HttpPost("GetFactSheetById")]
        public CommonResponse GetFactSheetById(GetFactSheetByIdReqViewModel getFactSheetByIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.GetFactSheetById(getFactSheetByIdReqViewModel.Adapt<GetFactSheetByIdReqDTO>());
                GetFactSheetByIdResDTO getFactSheetByIdResDTO = commonResponse.Data;
                commonResponse.Data = getFactSheetByIdResDTO.Adapt<GetFactSheetByIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetFactSheetByFundId")]
        public CommonResponse GetFactSheetByFundId(GetFactSheetByFundReqViewModel getFactSheetByFundReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.GetFactSheetByFundId(getFactSheetByFundReqViewModel.Adapt<GetFactSheetByFundReqDTO>());
                GetFactSheetByFundResDTO factSheetByFundResDTO = commonResponse.Data;
                commonResponse.Data = factSheetByFundResDTO.Adapt<GetFactSheetByFundResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("AddFactSheetDetails")]
        public CommonResponse AddFactSheetDetails(AddFactSheetReqViewModel addFactSheetReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.AddFactSheetDetails(addFactSheetReqViewModel.Adapt<AddFactSheetReqDTO>());
                AddFactSheetResDTO addFactSheetReqDTO = commonResponse.Data;
                commonResponse.Data = addFactSheetReqDTO.Adapt<AddFactSheetResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("UpdateFactSheet")]
        public CommonResponse UpdateFactSheet(UpdateFactSheetReqViewModel updateFactSheetReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.UpdateFactSheet(updateFactSheetReqViewModel.Adapt<UpdateFactSheetReqDTO>());
                UpdateFactSheetResDTO Model = commonResponse.Data;
                commonResponse.Data = Model.Adapt<UpdateFactSheetResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetModelPortfolioComparison")]
        public CommonResponse GetModelPortfolio(ModelPortfolioReqViewModel modelPortfolioReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.GetModelPortfolio(modelPortfolioReqViewModel.Adapt<ModelPortfolioReqDTO>());
                ModelPortfolioResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<ModelPortfolioResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetRiskStatistics")]
        public CommonResponse GetRiskStatistics(GetRiskStatisticsReqViewModel getRiskStatisticsReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.GetRiskStatistics(getRiskStatisticsReqViewModel.Adapt<GetRiskStatisticsReqDTO>());
                GetRiskStatisticsResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetRiskStatisticsResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetTopHoldings")]
        public CommonResponse GetTopHoldings(GetTopHoldingsListReqViewModel getTopHoldingsListReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.GetTopHoldings(getTopHoldingsListReqViewModel.Adapt<GetTopHoldingsListReqDTO>());
                GetTopHoldingsListResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetTopHoldingsListResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetMonthlyPerformance")]
        public CommonResponse GetMonthlyPerformance(GetMonthlyPerformanceReqViewModel getMonthlyPerformanceReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.GetMonthlyPerformance(getMonthlyPerformanceReqViewModel.Adapt<GetMonthlyPerformanceReqDTO>());
                GetMonthlyPerformanceResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetMonthlyPerformanceResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetPortfolioPerformance")]
        public CommonResponse GetPortfolioPerformance(GetPortfolioPerformanceReqViewModel getPortfolioPerformanceReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.GetPortfolioPerformance(getPortfolioPerformanceReqViewModel.Adapt<GetPortfolioPerformanceReqDTO>());
                GetPortfolioPerformanceResDTO model = commonResponse.Data;
                commonResponse.Data = model.Adapt<GetPortfolioPerformanceResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetFactSheetFieldsFromUnit")]
        public CommonResponse GetFactSheetFieldsFromUnit(GetFactSheetByUnitReqViewModel getFactSheetByUnitReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.GetFactSheetFieldsFromUnit(getFactSheetByUnitReqViewModel.Adapt<GetFactSheetByUnitReqDTO>());
                GetFactSheetByUnitResDTO getFactSheetByUnitResDTO = commonResponse.Data;
                commonResponse.Data = getFactSheetByUnitResDTO.Adapt<GetFactSheetByUnitResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetUnitTypeByFundId")]
        public CommonResponse GetUnitTypeByFundId(GetUnitTypeByFundIdReqViewModel getUnitTypeByFundIdReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _iFactSheet.GetUnitTypeByFundId(getUnitTypeByFundIdReqViewModel.Adapt<GetUnitTypeByFundIdReqDTO>());
                GetUnitTypeByFundIdResDTO getUnitTypeByFundIdResDTO = commonResponse.Data;
                commonResponse.Data = getUnitTypeByFundIdResDTO.Adapt<GetUnitTypeByFundIdResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }
    }
}
