﻿namespace WaltCapitalManagementWebAPI.ViewModels.ResViewModels
{
    public class UserDocumentResViewModel
    {
        public int UserId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentPath { get; set; }
        public dynamic Files { get; set; }
    }
}
