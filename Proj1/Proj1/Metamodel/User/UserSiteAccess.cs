using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proj1.Metamodel.User
{
    public class UserSiteAccess
    {
        public bool IsActive { get; set; }
        public int SiteConfigId { get; set; }
        public int UserId { get; set; }
        public int CreatedById { get { return 1; } }
        public DateTime? CreatedByTime { get { return DateTime.Now; } }
        public int UpdatedById { get { return 1; } }
        public DateTime? UpdatedByTime { get { return DateTime.Now; } }
    }
}