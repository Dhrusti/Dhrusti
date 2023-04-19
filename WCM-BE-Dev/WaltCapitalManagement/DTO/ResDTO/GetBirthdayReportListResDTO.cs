using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetBirthdayReportListResDTO
    {

        public List<BirthdayReport> BirthdayReports { get; set; }
        public int TotalCount { get; set; }

    }

    public class BirthdayReport
    {
        public int Id { get; set; }
        public string FirstChar { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
