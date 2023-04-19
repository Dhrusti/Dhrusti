using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ModelPortfolioBLL
    {
        private readonly WaltCapitalDBContext _dBContext;
        private readonly CommonRepo _commonRepo;

        public ModelPortfolioBLL(WaltCapitalDBContext dBContext, CommonRepo commonRepo)
        {
            _dBContext = dBContext;
            _commonRepo = commonRepo;
        }

        public CommonResponse GetModelPortfolio(ModelPortfolioReqDTO modelPortfolioReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                ModelPortfolioResDTO modelPortfolioResDTO = new ModelPortfolioResDTO();

                List<HeaderModel> headerModelList = new List<HeaderModel>();
                headerModelList.Add(new HeaderModel { Value = "name", Label = "Name" });
                headerModelList.Add(new HeaderModel { Value = "portfolio", Label = "Portfolio" });
                headerModelList.Add(new HeaderModel { Value = "s&p", Label = "S&P" });
             
                modelPortfolioResDTO.HeaderList = headerModelList;

                modelPortfolioResDTO.TableDataList = new  List<Dictionary<string, string>>();

                Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "SinceInception(1 Feb 2022)");
                keyValuePair.Add("portfolio", "51.4"+ "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Latest3Month(90 Days)");
                keyValuePair.Add("portfolio", "21.4" + "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Latest6Month(180 Days)");
                keyValuePair.Add("portfolio", "51.4" + "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "LatestMonth(30 Days)");
                keyValuePair.Add("portfolio", "11.4" + "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "LatestYear(12 Months)");
                keyValuePair.Add("portfolio", "11.4" + "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Year To Date");
                keyValuePair.Add("portfolio", "11.4" + "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Annualized");
                keyValuePair.Add("portfolio", "11.4" + "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                modelPortfolioResDTO.TableDataList.Add(keyValuePair);

                commonResponse.Message = "Model Portfolio List";
                commonResponse.Status = true;
                commonResponse.StatusCode = HttpStatusCode.OK;
                commonResponse.Data = modelPortfolioResDTO;

            }
            catch (Exception ex)
            {
                throw;
            }
            return commonResponse;
        }
        public CommonResponse GetRiskStatistics(GetRiskStatisticsReqDTO getRiskStatisticsReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GetRiskStatisticsResDTO getRiskStatisticsResDTO = new GetRiskStatisticsResDTO();

                List<HeaderValueModel> headerList = new List<HeaderValueModel>();
                headerList.Add(new HeaderValueModel { Value = "name", Label = "Name" });
                headerList.Add(new HeaderValueModel { Value = "portfolio", Label = "Portfolio" });
                headerList.Add(new HeaderValueModel { Value = "s&p", Label = "S&P" });


                getRiskStatisticsResDTO.HeaderValueList = headerList;
                getRiskStatisticsResDTO.TableDataList = new List<Dictionary<string, string>>();

                
                Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Standard Deviation");
                keyValuePair.Add("portfolio", "11.4" + "%");
                keyValuePair.Add("s&p", "48.5" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Best Months");
                keyValuePair.Add("portfolio", "51.4" + "%");
                keyValuePair.Add("s&p", "44.5" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Worst Month");
                keyValuePair.Add("portfolio", "11.4" + "%");
                keyValuePair.Add("s&p", "47.5" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "Max Drawn Down");
                keyValuePair.Add("portfolio", "11.4" + "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);

                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "% of Positive Months");
                keyValuePair.Add("portfolio", "11.4" + "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);


                keyValuePair = new Dictionary<string, string>();
                keyValuePair.Add("name", "% of Negative Months");
                keyValuePair.Add("portfolio", "11.4" + "%");
                keyValuePair.Add("s&p", "4.5" + "%");
                getRiskStatisticsResDTO.TableDataList.Add(keyValuePair);

                
                commonResponse.Status = true;
                commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                commonResponse.Data = getRiskStatisticsResDTO;
                commonResponse.Message = "Risk Statistics List";
            }
            catch (Exception ex)
            {
                throw;
            }
            return commonResponse;
        }

        public CommonResponse GetTopHoldings(GetTopHoldingsListReqDTO getTopHoldingsListReqDTO)
        { 
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<GetTopHoldingsListResDTO> model = new List<GetTopHoldingsListResDTO>();
                model.Add(new GetTopHoldingsListResDTO { Name ="Stock",Value = "20" + "%" });
                model.Add(new GetTopHoldingsListResDTO { Name ="Silver",Value = "30" + "%" });
                model.Add(new GetTopHoldingsListResDTO { Name ="Bitcoin",Value = "10" + "%" });
                model.Add(new GetTopHoldingsListResDTO { Name ="Cash",Value = "40" + "%" });

                commonResponse.Status = true;
                commonResponse.StatusCode = System.Net.HttpStatusCode.OK;
                commonResponse.Data = model;
                commonResponse.Message = "TopHoldings List";

            }
            catch (Exception ex)
            {
                throw;
            }
            return commonResponse;
        }

    }
}
