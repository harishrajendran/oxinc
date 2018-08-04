using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oxinc.Models
{
    public class UserLogin
    {
        public long Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }

        [Required(ErrorMessage = "Login Name can not be blank")]
        public string loginname { get; set; }
        public long mobilenumber { get; set; }

        [Required(ErrorMessage = "Password can not be blank")]
        public string password { get; set; }
        public string email { get; set; }

        public string loginerrormessage { get; set; }
    }
    public class UserAuthorize
    {
        public long Id { get; set; }
        public string password { get; set; }
        public string loginname { get; set; }
    }
}