using Azure.Core;

namespace Helper
{
    public class CommonConstant
    {
        #region SP_Modes

        public const int Read = 0;
        public const int Create = 1;
        public const int Update = 2;
        public const int Delete = 3;
        public const int ReadWithFilter = 4;

        #endregion

        #region LogType
        public const string Activity_log = "ActivityLog";
        public const string Exception_log = "ExceptionLog";
        #endregion

        #region File Extensions
        public const string Json = "json";
        public const string Jpeg = "jpeg";
		#endregion

		#region ConstantIds
        public const int CallTypeId = 6;
		#endregion

		#region Keys
		public const string Approve = "Approve";
		public const string Reject = "Reject";
		public const string Done = "Done";
		public const string Pending = "Pending";
		public const string Admin = "Admin";
		#endregion

		#region ResponseMessage
		public const string Data_Not_Found = "Data not found!";
		public const string Data_Found_Successfully = "Data found successfully!";
		public const string Something_Went_Wrong = "Something went wrong! Please try again.";
		public const string Request_To_Edit_Appointment = "Request to edit appointment.";
		public const string Request_To_Edit_Appointment_Fail = "Request to edit appointment fail.";
		public const string Send_Request_Successfully_For_Editing_Appointment_Details = "Send request successfully for editing appointment details.";
		public const string You_Have_Received_Request_Notification = "You have received request notification.";
		public const string Appointment_Status_Notification = "Appointment status notification.";
		public const string Notification_Has_Been_Already_Sent = "Notification has been already sent.";
		public const string Updated_Notification_Successfully = "Updated notification successfully.";
		public const string Please_Enter_Valid_Data = "Please enter valid data.";
		#endregion
	}

}
