using LeaveApprovalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LeaveApprovalSystem.Controllers
{
    public class EditController : ApiController
    {
        public LeaveApprovalEdit Get(int id)
        {
            Helpers.LeaveApproval leaveapproval = new Helpers.LeaveApproval();
            return leaveapproval.LeaveRequestEdit(id);
        }
    }
}
