using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaveApprovalSystem.Models
{
    public enum LeaveApprovalStatus
    {
        Pending,
        PartiallyApproved,
        Approved,
        Cancelled
    }
    public class LeaveApproval
    {
        internal long? id { get; set; }
        internal long? StudentId { get; set; }
        internal long? TeacherId { get; set; }
        internal DateTime FromDate { get { return DateTime.Now; } }
        internal DateTime ToDate { get { return DateTime.Now; } }
        internal bool? IsRequireFurtherApproval { get; set; }
        internal LeaveApprovalStatus? ApprovalStatus { get; set; }
        internal string RejectionReason { get; set; }
        internal string LeaveReason { get; set; }
    }
    public class Data
    {
        internal LeaveApproval gettingleaveapproval { get; set; }
    }
    public class BrowseModel
    {
        internal long id;
        internal DateTime FromDate;
        internal DateTime ToDate;
        internal LeaveApprovalStatus ApprovalStatus;
        internal string LeaveReason;

    }
    public class filtercriteria
    {
        internal long? userid;
        internal long? studentid;
        internal LeaveApprovalStatus ApprovalStatus;
    }
    public class LeaveApprovalEdit
    {
        internal long id;
        internal long StudentId;
        internal DateTime FromDate;
        internal DateTime ToDate;
        internal bool IsRequireFurtherApproval;
        internal LeaveApprovalStatus ApprovalStatus;
        internal string RejectionReason;
        internal string LeaveReason;
        internal long createdbyid;
        internal DateTime createdbytime;
        internal long? updatedbyid;
        internal DateTime? updatedbytime;
    }
}