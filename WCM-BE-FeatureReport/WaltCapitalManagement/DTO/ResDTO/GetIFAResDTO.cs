namespace DTO.ResDTO
{
    public class GetIFAResDTO
    {
        public int Id { get; set; }
        public string? ProfilePhoto { get; set; }
        public List<ListDocuments> IFADocuments { get; set; }
        public int Country { get; set; }
        public int Province { get; set; }
        public string City { get; set; }
        public int Office { get; set; }
        public string FSCA { get; set; }
        public string IFAPractice { get; set; } = null!;
        public string? ResponsiblePerson { get; set; }
        public string? ResponsiblePersonTitle { get; set; }
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? PositionHeld { get; set; }
        public DateTime Dob { get; set; }
        public string? CompanyName { get; set; }
        public string CompRegNumber { get; set; }
        public string SarstaxNo { get; set; } = null!;
        public string Vatno { get; set; }
        public string? BuildingName { get; set; }
        public string? FloorandOfficeNo { get; set; }
        public string? StreetName { get; set; }
        public string? Suburb { get; set; }
        public string? PostalCode { get; set; }
        public bool? IsFscaactive { get; set; }
        public DateTime? LastDateChecked { get; set; }
        public string? PersonChecked { get; set; }
        public int WaltCapConsultant { get; set; }
        public int SoftwareAccessGroup { get; set; }
        public string MobileNo { get; set; } = null!;
        public string? WorkNo { get; set; }
        public string Email { get; set; } = null!;
        public string? Notes { get; set; }
        public bool? IsAml { get; set; }
        public bool? IsDueDiligence { get; set; }
        public string? InitialFee { get; set; }
        public string? AnnualAdvisorFees { get; set; }
        public string? PerformanceFee { get; set; }
        public string? Other { get; set; }
        public bool IsVatapplicable { get; set; }
        public DateTime? DueDiligenceUpdatedDate { get; set; }
        public DateTime? AmlupdatedDate { get; set; }
        public string UpdatedByName { get; set; }
        public string IsProminentPoliticalName { get; set; }
        public string IsDueDiligenceName { get; set; }
        public string IsAmlName { get; set; }
        public string CreatedByName { get; set; }
        public bool? IsActive { get; set; }
        public string? Role { get; set; }

    }

    public class ListDocuments
    {
        public string File { get; set; }
        public string FileName { get; set; }
    }
}
