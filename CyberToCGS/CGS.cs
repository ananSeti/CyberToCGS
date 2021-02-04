﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;

namespace CyberToCGS
{
    class CGS
    {
        private string webUser = "webapp";
        private string password = "password";

        private string cyberweb = "web_portal";
        private string cyberpass = "password";

       private string grant_type = "password";
        //private string bodyusername ="crm_system";
        //private string bodypassword = "P@ssw0rd";

        private string cyberbodyUser = "TCG_SYSTEM";
        private string cyberbodypassword = "'P@ssw0rd";

        /* 
         * username =TCG_SYSTEM
         * password: P@ssw0rd
         * 
         * web ac
         * **/


        private string servicesToken = "/authentication-service/oauth/token";
        private string serviceReq = "/bank/authentication-service/oauth/token";
        private string serviceIndirecPost = "/request-service/api/external/request";

        public void AuthenticationBasics(ref string token,string url)
        {
            RestClient restClient = new RestClient();
                       restClient.Authenticator = new HttpBasicAuthenticator(cyberweb, cyberpass);
                       restClient.BaseUrl = new Uri(url);
                       restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyError) => true;

            RestRequest restRequest = new RestRequest(serviceReq, Method.POST);
                        //restRequest.AddParameter("grant_type",grant_type, ParameterType.GetOrPost);
                        //restRequest.AddParameter("username",bodyusername, ParameterType.GetOrPost);  
                        //restRequest.AddParameter("password",bodypassword, ParameterType.GetOrPost);

                        restRequest.AddParameter("grant_type", "password", ParameterType.GetOrPost);
                        restRequest.AddParameter("username", "TCG_SYSTEM", ParameterType.GetOrPost);  
                        restRequest.AddParameter("password", "P@ssw0rd", ParameterType.GetOrPost);

            try
            {
                IRestResponse restResponse = restClient.Execute(restRequest);
                Console.WriteLine("Status code: " + restResponse.StatusCode);
                Console.WriteLine("Status message " + restResponse.Content);
                // Access token
                JObject tk = JObject.Parse(restResponse.Content);
                string t = (string)tk["access_token"];
                token = t;
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex.InnerException.Message.ToString();
                }
            }
        }
        /*
         * เซอร์วิสสำหรับบันทึกคำขอแบบ Indirect
         */
        public void IndirectPost(string Token,string url)
        {
            string token = Token;
            var restClient = new RestSharp.RestClient(url);
                 restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyError) => true;

            RestRequest restRequest = new RestRequest(serviceIndirecPost,Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Content-Type","application/json");
            restRequest.AddHeader("Authorization","Bearer" + token);

            /*
             *Parameter Here
             *
             */
           // restRequest.AddParameter(0);
            try {
                IRestResponse restResponse = restClient.Execute(restRequest);
                JObject obj = JObject.Parse(restResponse.Content);

            
            } catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex.InnerException.Message.ToString();
                }
            }


        }
    }
}
