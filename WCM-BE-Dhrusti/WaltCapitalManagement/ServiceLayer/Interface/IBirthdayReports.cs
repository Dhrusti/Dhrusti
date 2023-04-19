using DTO.ReqDTO;
using Helper;


namespace ServiceLayer.Interface
{
    public interface IBirthdayReports
    {
        public CommonResponse GetBirthdayReport();
        public CommonResponse GetBirthdayReportList(GetBirthdayReportListReqDTO getBirthdayReportListReqDTO);
        public CommonResponse SendEmailToCRM(DailyEventEmailReqDTO dailyEventEmailReqDTO);
    }
}
