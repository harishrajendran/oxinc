using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Proj1.Metamodel.User
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public long MobileNumber { get; set; }
        public string Password { get; set; }
        public string LoginName { get; set; }
        public int Row { get; set; }
        public string Email { get; set; }
        public DateTime? ActivationDate { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? LoginBlockedTime { get; set; }
        public bool ForcePasswordReset { get; set; }
        public StatusValues Status { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public int LoginFailureCounter { get; set; }
        public int CreatedById { get { return 1; } }
        public DateTime? CreatedByTime { get { return DateTime.Now; } }
        public int UpdatedById { get { return 1; } }
        public DateTime? UpdatedByTime { get { return DateTime.Now; } }
    }
}