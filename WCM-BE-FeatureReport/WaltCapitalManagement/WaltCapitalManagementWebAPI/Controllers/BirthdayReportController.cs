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
    public class BirthdayReportController : ControllerBase
    {
        private readonly IBirthdayReports _birthdayReports;

        public BirthdayReportController(IBirthdayReports birthdayReports)
        {
            _birthdayReports = birthdayReports;
        }
        [HttpPost("GetBirthdayReports")]
        public CommonResponse GetBirthdayReports()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _birthdayReports.GetBirthdayReport();
                GetBirthdayReportResDTO birthdayReportsRes = commonResponse.Data ?? new GetBirthdayReportResDTO();
                commonResponse.Data = birthdayReportsRes.Adapt<GetBirthdayReportResViewModel>();
            }
            catch (Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("GetBirthdayReportList")]
        public CommonResponse GetBirthdayReportList(GetBirthdayReportListReqViewModel getBirthdayReportListReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {

                commonResponse = _birthdayReports.GetBirthdayReportList(getBirthdayReportListReqViewModel.Adapt<GetBirthdayReportListReqDTO>());
                GetBirthdayReportListResDTO getBirthdayReportsRes = commonResponse.Data ?? new GetBirthdayReportListResDTO();
                commonResponse.Data = getBirthdayReportsRes.Adapt<GetBirthdayReportListResViewModel>();
            }
            catch(Exception) { throw; }
            return commonResponse;
        }

        [HttpPost("SendEmailToCRM")]
        public CommonResponse SendEmailToCRM(DailyEventEmailReqViewModel dailyEventEmailReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = _birthdayReports.SendEmailToCRM(dailyEventEmailReqViewModel.Adapt<DailyEventEmailReqDTO>());
                DailyEventEmailResDTO dailyEventEmailResDTO  = commonResponse.Data ?? new DailyEventEmailResDTO();
                commonResponse.Data = dailyEventEmailResDTO.Adapt<DailyEventEmailResViewModel>();
            }
            catch(Exception)
            {
                throw;
            }
            return commonResponse;
        }

    }
}
