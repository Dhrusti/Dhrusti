using DTO.ReqDTO;
using Helper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface INotification
    {
        public CommonResponse SendNotification(SendNotificationReqDTO sendNotificationSendReqDTO);

        public CommonResponse GetAllNotification(GetAllNotificationReqDTO getAllNotificationReqDTO);

        public CommonResponse UpdateNotification(UpdateNotificationReqDTO updateNotificationReqDTO);

        public CommonResponse GetAllReceptionistNotification(GetAllReceptionistNotificationReqDTO getAllReceptionistNotificationReqDTO);
    }
}
