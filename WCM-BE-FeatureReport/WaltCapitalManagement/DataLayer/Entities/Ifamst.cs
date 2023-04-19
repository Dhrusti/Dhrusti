using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class Ifamst
    {
        public int Id { get; set; }
        public string Ifa { get; set; } = null!;
        public string FscaregistrationNo { get; set; } = null!;
        public string IfapractiseNo { get; set; } = null!;
        public string ResponsiblePersonTitle { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string PositionHeld { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string CompanyName { get; set; } = null!;
        public string CompRegNumber { get; set; } = null!;
        public string SarstaxNumber { get; set; } = null!;
        public string Vatnumber { get; set; } = null!;
        public string BuildingName { get; set; } = null!;
        public string FloorOfficeNumber { get; set; } = null!;
        public string StreetName { get; set; } = null!;
        public string Suburb { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string Postalcode { get; set; } = null!;
        public bool IsFscaregistration { get; set; }
        public string LastDate { get; set; } = null!;
        public string PersonChecked { get; set; } = null!;
        public string Consultant { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string WorkNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Notepad { get; set; } = null!;
        public string ProfilePictutePath { get; set; } = null!;
        public string DocumentPath { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
