﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class RemarksResDTO
    {
        public string AppointmentType { get; set; }
        public int AppointmentNo { get; set; }
        public List<RemarkList> remarkLists { get; set; }

        public class RemarkList
        {
            public int SR { get; set; }
            public string RemarkTime { get; set; }
            public string Remarks { get; set; }
            public string EnterBy { get; set; }
            public int? Status { get; set; }
            public int RemarkId { get; set; }
            public DateTime UpdatedDate { get; set; }
            public int AdminId { get; set; }
        }
    }
  
}
