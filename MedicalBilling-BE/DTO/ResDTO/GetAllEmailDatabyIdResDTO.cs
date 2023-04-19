﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllEmailDatabyIdResDTO
    {
        public string PatientName { get; set; }

        public string Subject { get; set; }
        public string PatientEmail { get; set; }
        public  int UserId { get; set; }

        public string AppointmentType { get; set; }

        public List<SendEmailList> sendEmailLists { get; set; }

        public class SendEmailList
        {
            public int SR { get; set; }
            public string SendTo { get; set; }
            public string SendOn { get; set; }
            public string SendBy { get; set; }
            public DateTime UpdatedDate { get; set; }
        }
    }
}
