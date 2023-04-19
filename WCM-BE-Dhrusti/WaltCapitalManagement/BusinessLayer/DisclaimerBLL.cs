using DataLayer.Entities;
using DTO.ReqDTO;
using Helper;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace BusinessLayer
{
    public class DisclaimerBLL
    {
        private readonly WaltCapitalDBContext _dbContext;
        private readonly CommonRepo _commonRepo;
        private readonly CommonHelper _commonHelper;
        private readonly IConfiguration _iConfiguration;

        public DisclaimerBLL(WaltCapitalDBContext dbContext, CommonRepo commonRepo, CommonHelper commonHelper, IConfiguration iConfiguration)
        {
            _dbContext = dbContext;
            _commonRepo = commonRepo;
            _commonHelper = commonHelper;
            _iConfiguration = iConfiguration;
        }

        public CommonResponse AddDisclaimer(AddDisclaimerReqDTO addDisclaimerReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (addDisclaimerReqDTO != null)
                {
                    int validtilldays = Convert.ToInt32(_iConfiguration.GetSection("DisclaimerDay").Value);
                    DisclaimerMst disclaimerMst = new DisclaimerMst();
                    disclaimerMst.UserId = addDisclaimerReqDTO.UserId;
                    disclaimerMst.Disclaimer = addDisclaimerReqDTO.Disclaimer;
                    disclaimerMst.CreatedOn = _commonHelper.GetCurrentDateTime();
                    disclaimerMst.ValidTill = _commonHelper.GetCurrentDateTime().AddDays(validtilldays);

                    _dbContext.DisclaimerMsts.Add(disclaimerMst);
                    _dbContext.SaveChanges();

                    commonResponse.Message = "Disclaimer Added Successfully!";
                    commonResponse.Status = true;
                    commonResponse.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    commonResponse.Status = false;
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = "";
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
