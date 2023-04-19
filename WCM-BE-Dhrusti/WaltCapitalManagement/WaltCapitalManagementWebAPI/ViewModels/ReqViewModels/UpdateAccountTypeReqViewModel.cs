
namespace WaltCapitalManagementWebAPI.ViewModels.ReqViewModels
{
    public class UpdateAccountTypeReqViewModel
    {
        public int Id { get; set; }
        public string AccountType { get; set; } = null!;
        public int UserId { get; set; }
    }
}
