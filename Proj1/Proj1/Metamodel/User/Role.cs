using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proj1.Metamodel.User
{
    public class Role
    {
        public string Name { get; set; }
        public string SystemDefinedName { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemDefined { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DateTime? ActivationDate { get; set; }
        public int CreatedById { get { return 1; } }
        public DateTime? CreatedByTime { get { return DateTime.Now; } }
        public int UpdatedById { get { return 1; } }
        public DateTime? UpdatedByTime { get { return DateTime.Now; } }
    }
}