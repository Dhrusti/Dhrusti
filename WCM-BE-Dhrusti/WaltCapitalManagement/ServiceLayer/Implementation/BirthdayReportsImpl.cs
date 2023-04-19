using BusinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class BirthdayReportsImpl : IBirthdayReports
    {
        private readonly BirthdayReportBLL _birthdayReport;

        public BirthdayReportsImpl(BirthdayReportBLL birthdayReport)
        {
            _birthdayReport = birthdayReport;
        }

        public CommonResponse GetBirthdayReport()
        {
            return _birthdayReport.GetBirthdayReport();
        }

        public CommonResponse GetBirthdayReportList(GetBirthdayReportListReqDTO getBirthdayReportListReqDTO)
        {
            return _birthdayReport.GetBirthdayReportList(getBirthdayReportListReqDTO);
        }

        public CommonResponse SendEmailToCRM(DailyEventEmailReqDTO dailyEventEmailReqDTO)
        {
            return _birthdayReport.SendEmailToCRM(dailyEventEmailReqDTO);
        }
    }
}
