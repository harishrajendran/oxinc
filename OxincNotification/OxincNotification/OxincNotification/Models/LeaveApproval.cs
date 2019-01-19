using System;

namespace OxincNotification.Models
{
    public enum LeaveApprovalStatus
    {
        Pending,
        Approved,
        Cancelled
    }

    public class LeaveApproval : IBrowse
    {
        public long? Id { get; set; }
        public string StudentName { get; set; }
        public string RollNumber { get; set; }
        public long? StudentId { get; set; }
        public long? TeacherId { get; set; }
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
        public string DisplayName => $"{StudentName} - {RollNumber}";
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool RequireFurtherApproval { get; set; }
        public LeaveApprovalStatus ApprovalStatus { get; set; }
        public string RejectionReason { get; set; }
        public string LeaveReason { get; set; }
    }

    public interface IBrowse
    {
        long? Id { get; set; }
        string RollNumber { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        LeaveApprovalStatus ApprovalStatus { get; set; }
        string LeaveReason { get; set; }

    }

    public class FilterCriteria
    {
        internal long? UserId;
        internal long? StudentId;
        internal LeaveApprovalStatus ApprovalStatus;
    }
}
