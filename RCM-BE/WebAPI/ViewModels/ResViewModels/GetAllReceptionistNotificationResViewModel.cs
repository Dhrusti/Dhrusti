namespace WebAPI.ViewModels.ResViewModels
{
	public class GetAllReceptionistNotificationResViewModel
	{
		public int TotalCount { get; set; }

		public List<ReceptionistNotificationList> receptionistNotificationList { get; set; }

		public class ReceptionistNotificationList
		{
			public int NotificationId { get; set; }
			public string Description { get; set; }
			public string Title { get; set; }
			public bool IsRead { get; set; }
		}
	}
}
