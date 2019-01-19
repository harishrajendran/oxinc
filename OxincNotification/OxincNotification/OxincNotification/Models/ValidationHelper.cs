using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace OxincNotification.Models
{
    class ValidationHelper
    {
        public string GetValidation(WebException ex)
        {
            using (HttpWebResponse webResp = (HttpWebResponse)ex.Response)
            {
                string message = GetResponseMessage(webResp);
                switch (webResp.StatusCode)
                {
                    case HttpStatusCode.ExpectationFailed:
                        {
                            return message;
                        }
                    default:
                        {
                            return "An unhandled error occured. Please contact the Admin.";
                        }
                }
            }
        }
        private string GetResponseMessage(HttpWebResponse webResp)
        {
            var message = string.Empty;

            using (var stream = webResp.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    message = reader.ReadToEnd();
                }
            }

            return message;
        }
    }
}
