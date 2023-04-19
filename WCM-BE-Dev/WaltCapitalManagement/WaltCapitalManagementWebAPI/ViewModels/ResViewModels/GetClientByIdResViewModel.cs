namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class GetClientByIdResViewModel
    {
        public int Id { get; set; }
        public string ProfilePhoto { get; set; }
        public List<ListDocuments>? ClientDocuments { get; set; }
        public int Office { get; set; }
        public string ClientAccNo { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int AccessCategoryId { get; set; }
        public string? ResponsiblePerson { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PositionHeld { get; set; }
        public DateTime Dob { get; set; }
        public string? TrustRegNo { get; set; }
        public string MobileNo { get; set; } = null!;
        public string? WorkNo { get; set; }
        public string Email { get; set; } = null!;
        public string SarstaxNo { get; set; } = null!;
        public int Country { get; set; }
        public string? StreetNo { get; set; }
        public string? HomeName { get; set; }
        public string? StreetName { get; set; }
        public string? Suburb { get; set; }
        public string? City { get; set; }
        public int Province { get; set; }
        public string? PostalCode { get; set; }
        public string? AccountHolder { get; set; }
        public string? Bank { get; set; }
        public int AccountType { get; set; }
        public string? AccountNo { get; set; }
        public string? BranchCode { get; set; }
        public string? SwiftCode { get; set; }
        public int ClientType { get; set; }
        public int PersonalityType { get; set; }
        public int WaltCapConsultant { get; set; }
        public int Ifa { get; set; }
        public string MaritalStatus { get; set; } = null!;
        public int SoftwareAccessGroup { get; set; }
        public string? SpouseName { get; set; }
        public DateTime? SpouseDob { get; set; }
        public string? NickName { get; set; }
        public string? Faserial { get; set; }
        public string? Notes { get; set; }
        public bool Equity { get; set; }
        public bool Tfsa { get; set; }
        public bool Dcs { get; set; }
        public bool Mcs { get; set; }
        public string? InitialFee { get; set; }
        public string? AnnualManagementFee { get; set; }
        public string? PerformanceFee { get; set; }
        public string? BrokerageRate { get; set; }
        public string? FlatBrokerageRate { get; set; }
        public string? AdminMonthlyFee { get; set; }
        public string? Other { get; set; }
        public bool IsVatapplicable { get; set; }
        public bool LoadWithoutFee { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string? Salutation { get; set; }
        public string? MiddleName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool? IsDueDiligence { get; set; }
        public bool? IsAml { get; set; }
        public DateTime? DueDiligenceUpdatedDate { get; set; }
        public DateTime? AmlupdatedDate { get; set; }
        public bool? IsProminentPolitical { get; set; }
        public bool? WelcomeMailSent { get; set; }
        public string UpdatedByName { get; set; }
        public string IsProminentPoliticalName { get; set; }
        public string IsDueDiligenceName { get; set; }
        public string IsAmlName { get; set; }
        public string CreatedByName { get; set; }
        public string? Idnumber { get; set; }
        public string? Vatno { get; set; }
        public bool? WcfundAdministration { get; set; }
    }
}
