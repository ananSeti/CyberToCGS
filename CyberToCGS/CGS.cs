using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using CyberToCGS.FactoryPattern;

namespace CyberToCGS
{
    class CGS
    {
        //private string webUser = "webapp";
        //private string password = "password";

        // private string cyberweb = "web_portal";
        // private string cyberpass = "password";
         private string cyberweb = "JeTwLJALsikYUPXYhQtXag==";
         private string cyberpass ="3UjGoHL3x0kCJ2+Bu0n0Yg==";

        private string grant_type = "password";
        //private string bodyusername ="crm_system";
        //private string bodypassword = "P@ssw0rd";

        //private string cyberbodyUser = "TCG_SYSTEM";
        //private string cyberbodypassword = "'P@ssw0rd";

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
            // restClient.Authenticator = new HttpBasicAuthenticator(cyberweb, cyberpass);
            //restClient.Authenticator = new HttpBasicAuthenticator(webUser, password);
            restClient.BaseUrl = new Uri(url);
                       restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyError) => true;

            RestRequest restRequest = new RestRequest(serviceReq, Method.POST);
                         restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                         restRequest.AddHeader("Authorization", "Basic eSrTcpfOZ1O6ZmkkN4YbWlSg1X9JYpFexMZSAprl7gM=");
                         restRequest.AddParameter("grant_type", grant_type, ParameterType.GetOrPost);
            //  restRequest.AddParameter("grant_type", "password", ParameterType.GetOrPost);
            // restRequest.AddParameter("username", "TCG_SYSTEM", ParameterType.GetOrPost);  
            // restRequest.AddParameter("password", "P@ssw0rd", ParameterType.GetOrPost);
                         restRequest.AddParameter("username", cyberweb, ParameterType.GetOrPost);  
                         restRequest.AddParameter("password", cyberpass, ParameterType.GetOrPost);

            try
            {
                IRestResponse restResponse = restClient.Execute(restRequest);
               // Console.WriteLine("Status code: " + restResponse.StatusCode);
               // Console.WriteLine("Status message " + restResponse.Content);
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
            IndirectRequest indirectRequest = new IndirectRequest();
           
            /* 
             * Product
             * 
             */
            Product p = new Product() {
            preReqStatus= "07",
            productId= 215,
            roundId= 1,
            guaAmount= "20000000",
            prdPayFeeType= 1,
            prdReduGuaType = null,
            refNo1= null,
            refNo2 = null,
            refNo3 = null,
            advFeeYearId = null
            };     
            Bank b = new Bank() {

             bankId = 4,
             bankBrnUseLimit= 1687,
             bankBrnSendOper = 1687,
             guaCareName= "ม้า",
             guaCareMobile= "0336569898",
             guaCarePhone= "0336569898",
             guaCareEmail= "nn@zz.com",
             guaApproveEmail= "nn@zz.com",
             guaRemark= ""
            };
            var cust = new List<Customer>()
            {
                new Customer()
                {
                    identification= "4081299709357",
                    identificationType= "C",
                    customerStatus =null,
                    customerType= "02",
                    customerGrade= null,
                    customerScore= null,
                    raceId = null,
                    raceStr = null,
                    nationalityId = null,
                    nationalityStr = null,
                    refReqNumber = null,
                    customerId = 3375,
                    borrowerType = "01",
                    titleId = 10,
                    cusNameTh = "สุนิติ",
                    cusSurnameTh = "ฟามาระ",
                    cusNameEn = null,
                    cusSurnameEn = null,
                    gender= "M",
                    marriedStatus= "01",
                    birthDate = Convert.ToDateTime("1965-09-13"),
                    educationLevel= "7"
                }
            };


            /* TEST Data Factory   */
            DataFactory dataFactory = new DataFactory();
            IcgsData product = dataFactory.getData("Product");
            product.deserial();
           // string a = product.getData();


            IcgsData customer = dataFactory.getData("Customer");
            customer.deserial();

            indirectRequest.product = p;
            indirectRequest.bank = b;
            indirectRequest.customer = cust;
                                          
                          
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(indirectRequest);
             restRequest.AddParameter("application / json; charset = utf - 8", json, ParameterType.RequestBody);
             restRequest.RequestFormat = DataFormat.Json;

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
