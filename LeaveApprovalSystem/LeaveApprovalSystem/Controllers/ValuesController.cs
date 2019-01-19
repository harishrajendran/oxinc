using LeaveApprovalSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LeaveApprovalSystem.Helpers;

namespace LeaveApprovalSystem.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        public List<BrowseModel> Get([FromBody]filtercriteria value)
        {
            Helpers.LeaveApproval leaveapproval = new Helpers.LeaveApproval();
            return leaveapproval.BrowseLeaveRequest(value);
        }

        // POST api/values
        public void Post([FromBody]Data value)
        {
            Models.LeaveApproval leaveapprovalrequest = value.gettingleaveapproval;
            Helpers.LeaveApproval leaveapproval = new Helpers.LeaveApproval();
            leaveapproval.LeaveRequestSubmission(leaveapprovalrequest);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Data value)
        {
            Models.LeaveApproval leaveapprovalrequest = value.gettingleaveapproval;
            Helpers.LeaveApproval leaveapproval = new Helpers.LeaveApproval();
            leaveapproval.LeaveApproving(leaveapprovalrequest);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
