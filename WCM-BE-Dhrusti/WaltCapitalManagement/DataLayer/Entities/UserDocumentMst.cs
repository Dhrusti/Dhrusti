﻿using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public partial class UserDocumentMst
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentPath { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
