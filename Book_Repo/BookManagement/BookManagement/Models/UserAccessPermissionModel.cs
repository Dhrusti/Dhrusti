namespace BookManagement.Models
{
	public class UserAccessPermissionModel
	{
		public UserMstModel userMstModel { get; set; }
        public List<UserPermissionModel> userPermissionModels { get; set; }
        public List<AccessModel> accessModel { get; set; }
		public List<PermissionModel> permissionModel { get; set; }
	}
}
