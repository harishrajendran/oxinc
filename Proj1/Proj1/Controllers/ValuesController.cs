using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using Proj1.Helpers;
using Proj1.Metamodel;

namespace Proj1.Controllers
{
    
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            ExcelParser.PersistExcelData();

            if (ValidationResult.Validations.Any())
            {
                string validationMessage = "";
                foreach (var validation in ValidationResult.Validations)
                {
                    validationMessage += $"{validation.Key.ToString()} - {validation.Value}";
                    validationMessage += "\n";
                }
                return validationMessage;
            }
            return "Its done";
        }

        // POST api/values
        public void Post([FromBody]dynamic value)
        {

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
