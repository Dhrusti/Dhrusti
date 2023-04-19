﻿namespace MedicalBillingManagementWebAPI.ViewModels.ReqViewModel
{
    public class GetAllAppointmentByLocalSearchReqViewModel
    {
        public string AppointmentType { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; }
        public string PageSearch { get; set; }
        public int ReceptionistId { get; set; }
    }
}
