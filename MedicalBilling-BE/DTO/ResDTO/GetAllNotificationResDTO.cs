using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllNotificationResDTO
    {
        public int TotalCount { get; set; }
       
        public List<AllNotificationList> allNotificationList { get; set; }

        public class AllNotificationList
        {
            public int NotificationId { get; set; }
            public string Description { get; set; }
            public string Title { get; set; }
            public bool IsRead { get; set; }
        }
    }
 
}
