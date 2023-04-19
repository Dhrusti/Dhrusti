namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdatePrivilegesReqModel
    {
        public int GroupId { get; set; }
        public int UpdatedBy { get; set; }
        public List<int> Privileges { get; set; }
    }
}
