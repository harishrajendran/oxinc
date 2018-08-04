using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proj1.Metamodel
{
    public static class ValidationResult
    {
        public static bool Success { get; set; }

        public static IDictionary<int, string> Validations = new Dictionary<int, string>();

        public static void AddValidation(int rowNumber, string validationMessage)
        {
            if (Validations.ContainsKey(rowNumber))
                Validations[rowNumber] += " | " + validationMessage;
            else
                Validations.Add(rowNumber, validationMessage);
        }
    }
}