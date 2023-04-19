namespace DTO.ReqDTO
{
    public class UpdateIFAReqDTO
    {
        public int Id { get; set; }
        public string? ProfilePhoto { get; set; }
        public string FSCA { get; set; }
        public List<ListDocuments> IFADocuments { get; set; }
        public string? ResponsiblePerson { get; set; }
        public string? ResponsiblePersonTitle { get; set; }
        public string FirstName { get; set; } = null!;
        public string SurName { get; set; } = null!;
        public string? PositionHeld { get; set; }
        public DateTime Dob { get; set; }
        public string? CompanyName { get; set; }
        public string? CompRegNumber { get; set; }
        public string Email { get; set; }
        public string SarstaxNo { get; set; } = null!;
        public string? Vatno { get; set; }
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
        public string? Notes { get; set; }
        public string? Role { get; set; }
        public int UpdatedBy { get; set; }
        public bool? IsDueDiligence { get; set; }
        public bool? IsAml { get; set; }
    }
}
