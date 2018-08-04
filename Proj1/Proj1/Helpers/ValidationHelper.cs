using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Proj1.Metamodel;
using Proj1.Metamodel.User;

namespace Proj1.Helpers
{
    public class ValidationHelper
    {
        const string emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        const string loginNameRegex = @"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$";
        const string mobileNumberRegex = @"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)";
        const string invalidEmailFormatMessage = "Please enter a valid email address";
        const string invalidLoginNameFormatMessage = "Login Name must be alphanumeric and can contain periods, underscores and hypens. It must start and end with an alphabet.";
        const string invalidMobileNumberMessage = "Please enter a valid mobile number";

        public static void ValidateEntities(IEnumerable<User> users)
        {
            ValidateUser(users);
        }

        public static void ValidateUser(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                if (!Regex.IsMatch(user.Email, emailRegex, RegexOptions.IgnoreCase))
                    ValidationResult.AddValidation(user.Row, invalidEmailFormatMessage);
                if (!Regex.IsMatch(user.LoginName, loginNameRegex))
                    ValidationResult.AddValidation(user.Row, invalidLoginNameFormatMessage);
                if (!Regex.IsMatch(user.MobileNumber.ToString(), mobileNumberRegex))
                    ValidationResult.AddValidation(user.Row, invalidMobileNumberMessage);
            }
        }

        public static void ValidateRole(IEnumerable<Role> roles)
        {

        }
    }
}