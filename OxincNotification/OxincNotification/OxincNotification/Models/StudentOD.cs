using System;
using System.Collections.Generic;
using System.Text;

namespace OxincNotification.Models
{
    public class StudentOD
    {
        public long? StudentId { get; set; }

        public string StudentRollNumber { get; set; }

        public string LeaveDays
        {
            get
            {
                var fromDate = FromDate.ToString("dd MMM yyyy");
                var toDate = ToDate.ToString("dd MMM yyyy");
                if (string.IsNullOrWhiteSpace(toDate))
                    return fromDate;
                else
                    return $"{fromDate} - {toDate}";
            }
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Reason { get; set; }

        public bool IsConsiderLeave { get; set; }

        public bool IsActive { get; set; }
    }
}
