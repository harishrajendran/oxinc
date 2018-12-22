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
        public string Get(string fileName)
        {
            return string.Empty;
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]dynamic file)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            var fileContent = Convert.ToString(file);

            if (Convert.ToString(file).IsNullOrWhiteSpace())
                response = Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Error sending data. Please contact the administrator");

            ExcelParser.PersistExcelData(fileContent);
            
            if (ValidationResult.Validations.Any())
            {
                string validationMessage = "";
                foreach (var validation in ValidationResult.Validations)
                {
                    validationMessage += $"{validation.Key.ToString()} - {validation.Value}";
                    validationMessage += "\n";
                }

                response = Request.CreateResponse(HttpStatusCode.ExpectationFailed, validationMessage);
            }

            return response;
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
