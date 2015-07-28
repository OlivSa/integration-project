using System;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Xml.Linq;
using System.Collections.Generic;
using MVCApplication.Models;
using System.Web;

namespace MVCApplication.Services.Harvest
{
    public static class RequestHelper
    {
        public static bool Validator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public static HttpWebRequest  MakeRequest(string uri)
        {
            HttpWebRequest request;
            // 1. Set some variables specific to your account.
            string username = "olshakurova@yandex.ru";
            string password = "Hqrvwst";
            string usernamePassword = String.Format("{0}:{1}",
            username,
            password);
        
            ServicePointManager.ServerCertificateValidationCallback = Validator;
            request = WebRequest.Create(uri) as HttpWebRequest;
            request.MaximumAutomaticRedirections = 1;
            request.AllowAutoRedirect = true;

            // 2. It's important that both the Accept and ContentType headers are
            // set in order for this to be interpreted as an API request.
            request.Accept = "application/xml";
            request.ContentType = "application/xml";
            request.UserAgent = "harvest_api_sample.cs";
            // 3. Add the Basic Authentication header with username/password string.
            request.Headers.Add("Authorization", "Basic " + 
                Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));
            return request;
        }
        public static XDocument CreateXDocument(HttpWebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                return XDocument.Load(responseStream);
            }
        }

        public static void CatchWebException(WebException wex)
        {
            if (wex.Response != null)
            {
                using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                {
                    Console.WriteLine(
                    "The server returned '{0}' with the status code {1} ({2:d}).",
                    errorResponse.StatusDescription, errorResponse.StatusCode,
                    errorResponse.StatusCode);
                }
            }
            else
            {
                Console.WriteLine(wex);
            }
        }

        public static void CloseResponse(HttpWebResponse response)
        {
            if (response != null)
            {
                response.Close();
            }
        }
       
    }
}