using LeaveApprovalSystem.Models;
using System.Collections.Generic;

namespace LeaveApprovalSystem.Helpers
{
    public class LeaveApproval
    {
        public void LeaveRequestSubmission(Models.LeaveApproval value)
        {
            int TotalDaysLeave = value.FromDate.Day - value.ToDate.Day;
            bool IsRequireFurtherApproval=false;
            if (TotalDaysLeave > 10)
                IsRequireFurtherApproval = true;
            Repository.executeINsertOrUpdateSP("LeaveRequestMarking", new { studentid = value.StudentId, FromDate = value.FromDate,ToDate=value.ToDate,ApprovalStatus= Models.LeaveApprovalStatus.Pending, IsRequireFurtherApproval= IsRequireFurtherApproval, LeaveReason=value.LeaveReason });
        }
        public void LeaveApproving(Models.LeaveApproval value)
        {
            Models.LeaveApprovalStatus ApprovalStatus = Models.LeaveApprovalStatus.Approved;
            if (value.IsRequireFurtherApproval == true)
            {
                ApprovalStatus = Models.LeaveApprovalStatus.PartiallyApproved;
                Repository.executeINsertOrUpdateSP("UpdateLeaveRequest", new { id = value.id, ApprovalStatus = ApprovalStatus, userid = value.TeacherId, RejectionReason= value.RejectionReason });
            }
            Repository.executeINsertOrUpdateSP("UpdateLeaveRequestForPrincipal", new { id = value.id, ApprovalStatus = ApprovalStatus, userid = value.TeacherId, RejectionReason = value.RejectionReason });
        }
        public List<BrowseModel> BrowseLeaveRequest(filtercriteria value)
        {
            return Repository.executeLeaveRequsetBrowsequery(value);
        }
        public LeaveApprovalEdit LeaveRequestEdit(int value)
        {
            return Repository.executeLeaveRequsetEditquery(value);
        }
    }
}