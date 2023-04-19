using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper;
using System.Net;

namespace BusinessLayer
{
    public class FundAdministrationDashBoardBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        public FundAdministrationDashBoardBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
            _dbContext = dbContext;
            _commonRepo = commonRepo;
        }

        public CommonResponse GetFundAdministrationDashBoardByFundId(GetFundAdministrationDashBoardByFundIdReqDTO getFundAdministrationDashBoardByFundIdReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                GetFundAdministrationDashBoardByFundIdResDTO getFundAdministrationDashBoard = new GetFundAdministrationDashBoardByFundIdResDTO();

                var funddetails = _commonRepo.fundList().FirstOrDefault(x => x.Id == getFundAdministrationDashBoardByFundIdReqDTO.FundId);
                if (funddetails != null)
                {
                    List<GetFundAdministrationFundReturns1> getFundAdministrationFundReturns1 = new List<GetFundAdministrationFundReturns1>();

                    getFundAdministrationFundReturns1.Add(new GetFundAdministrationFundReturns1 { Title = "Month To Date", values = "0 %" });
                    getFundAdministrationFundReturns1.Add(new GetFundAdministrationFundReturns1 { Title = "Since Inception", values = "0 %" });
                    getFundAdministrationFundReturns1.Add(new GetFundAdministrationFundReturns1 { Title = "Since Inception(Annualized)", values = "0.9 %" });
                    getFundAdministrationFundReturns1.Add(new GetFundAdministrationFundReturns1 { Title = "12 Month Rolling", values = "-12.84 %" });
                    getFundAdministrationFundReturns1.Add(new GetFundAdministrationFundReturns1 { Title = "3years Rolling", values = "NaN" });
                    getFundAdministrationFundReturns1.Add(new GetFundAdministrationFundReturns1 { Title = "5years Rolling ", values = "NaN" });

                    List<GetFundAdministrationFundReturns2> getFundAdministrationFundReturns2 = new List<GetFundAdministrationFundReturns2>();

                    getFundAdministrationFundReturns2.Add(new GetFundAdministrationFundReturns2 { Title = "Inception date", values = Convert.ToString(_commonHelper.TodaysConvertDate(funddetails.InceptionDate.ToString()))});
                    getFundAdministrationFundReturns2.Add(new GetFundAdministrationFundReturns2 { Title = " Fund size", values = "R246,569.63" });
                    getFundAdministrationFundReturns2.Add(new GetFundAdministrationFundReturns2 { Title = " Units Issued", values = "252,998.40" });
                    getFundAdministrationFundReturns2.Add(new GetFundAdministrationFundReturns2 { Title = " Unit price", values = "R0.974" });

                    List<GetFundAdminDarshboardGraphData> getFundAdminDarshboardGraphData  = new List<GetFundAdminDarshboardGraphData>();
                    List<GetFundAdministrationCommentoryHeader> getFundAdministrationCommentoryHeaders = new List<GetFundAdministrationCommentoryHeader>();
                    List<GetFundAdministrationCommentoryValue> getFundAdministrationCommentoryValues = new List<GetFundAdministrationCommentoryValue>();

                    getFundAdminDarshboardGraphData.Add(new GetFundAdminDarshboardGraphData { Title = " Fund size", value = "R246,569.63" });
                    getFundAdministrationCommentoryHeaders.Add(new GetFundAdministrationCommentoryHeader { Title = " Fund size", values = "R246,569.63" });
                    getFundAdministrationCommentoryValues.Add(new GetFundAdministrationCommentoryValue { Title = " Fund size", values = "R246,569.63" });


                    getFundAdministrationDashBoard.getFundAdministrationFundReturns1 = getFundAdministrationFundReturns1;
                    getFundAdministrationDashBoard.getFundAdministrationFundReturns2 = getFundAdministrationFundReturns2; 

                    if (getFundAdministrationDashBoard != null)
                    {
                        commonResponse.Message = "Success";
                        commonResponse.Status = true;
                        commonResponse.StatusCode = HttpStatusCode.OK;
                        commonResponse.Data = getFundAdministrationDashBoard;
                    }
                    else
                    {
                        commonResponse.Message = "Data Not Found.";
                        commonResponse.StatusCode = HttpStatusCode.NotFound;
                        commonResponse.Data = getFundAdministrationDashBoard;
                    }
                }
                else
                {
                    commonResponse.Message = "Data Not Found.";
                    commonResponse.StatusCode = HttpStatusCode.NotFound;
                    commonResponse.Data = getFundAdministrationDashBoard;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commonResponse;
        }


    }
}
