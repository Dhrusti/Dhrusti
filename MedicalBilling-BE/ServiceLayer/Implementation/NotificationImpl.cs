using BussinessLayer;
using DTO.ReqDTO;
using Helper.Models;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class NotificationImpl : INotification
    {
        private readonly NotificationBLL _notificationBLL;

        public NotificationImpl(NotificationBLL notificationBLL)
        { 
            _notificationBLL = notificationBLL;
        }
        public CommonResponse SendNotification(SendNotificationReqDTO sendNotificationReqDTO)
        {
            return _notificationBLL.SendNotification(sendNotificationReqDTO);
        }
        public CommonResponse GetAllNotification(GetAllNotificationReqDTO getAllNotificationReqDTO)
        {
           return _notificationBLL.GetAllNotification(getAllNotificationReqDTO);
           
        }
        public CommonResponse UpdateNotification(UpdateNotificationReqDTO updateNotificationReqDTO)
        {
            return _notificationBLL.UpdateNotification(updateNotificationReqDTO);   
        }
        public CommonResponse GetAllReceptionistNotification(GetAllReceptionistNotificationReqDTO getAllReceptionistNotificationReqDTO)
        {
            return _notificationBLL.GetAllReceptionistNotification(getAllReceptionistNotificationReqDTO);
        }
    }
}
