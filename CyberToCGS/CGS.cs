using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json.Linq;
using CyberToCGS.FactoryPattern;
using RestSharp.Serialization.Json;
using CyberToCGS.SaveFormClaim;
using System.Globalization;
using CyberToCGS.Database;
namespace CyberToCGS
{
    public class CGS
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


        //private string servicesToken = "/authentication-service/oauth/token";
        private string serviceReq = "/bank/authentication-service/oauth/token";
        private string serviceIndirecPost = "/bank/request-service/api/external/request";
        private string serviceSaveFormClaim = "/bank/guarantee-service/api/external/saveFormClaim";
        private string serviceGetAdjustGuaLonnByLgId = "/bank/guarantee-post-service/api/external/adjust-gua-loan-by-lg-id";
        private string serviceGetLgInfo = "/bank/guarantee-post-service/api/guarantee-post";

        public void AuthenticationBasics(ref string token,string url)
        {
            RestClient restClient = new RestClient();
            
            restClient.BaseUrl = new Uri(url);
                       restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyError) => true;

            RestRequest restRequest = new RestRequest(serviceReq, Method.POST);
                         restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                         restRequest.AddHeader("Authorization", "Basic eSrTcpfOZ1O6ZmkkN4YbWlSg1X9JYpFexMZSAprl7gM=");
                         restRequest.AddParameter("grant_type", grant_type, ParameterType.GetOrPost);
            
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
            restRequest.AddHeader("Authorization","Bearer" + token);
            restRequest.AddHeader("Content-Type", "application/json");

            IndirectRequest indirectRequest = new IndirectRequest();


            Product product = new Product();

            Bank bank = new Bank();

            Customer customer = new Customer();

            FacadeIndirect facade = new FacadeIndirect(product,bank,customer);
            indirectRequest= ClientFacadeIndirect.ClientCode(facade);


                                          
                          
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(indirectRequest);
             restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            

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
        public void IndirectPostTestJson(string Token, string url)
        {
            string token = Token;
            var restClient = new RestSharp.RestClient(url);
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyError) => true;
            restClient.Timeout = -1;
            RestRequest restRequest = new RestRequest(serviceIndirecPost, Method.POST);
            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Content-Type", "application/json");
           

            loadJson l = new loadJson();
            string indirectRequest = l.ReadJson();


           //  var json = Newtonsoft.Json.JsonConvert.SerializeObject(indirectRequest);
           
             restRequest.AddParameter("application/json", indirectRequest, ParameterType.RequestBody);
          

            try
            {
                IRestResponse restResponse = restClient.Execute(restRequest);
                if (restResponse.IsSuccessful)
                {
                    JObject obj = JObject.Parse(restResponse.Content);
                }
                else
                {
                    Console.WriteLine("Response code:" + restResponse.StatusCode );
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex.InnerException.Message.ToString();
                }
            }


        }
       
        public void SaveRequestClaimPGSPackage(string LGNo, string Token,string url)
        {
            string token = Token;
            var restClient = new RestSharp.RestClient(url);
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyError) => true;

            RestRequest restRequest = new RestRequest(serviceSaveFormClaim, Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer" + token);

            //this lg not found 63092355
            //this lg found  63071729
            string lgno = LGNo; // "63092355";// "63071729";  //not found in Claim online //"62036859"; //63060917
            Utils databaseUtil = new Utils();
            Database.Database db = Database.Database.GetInstance("DB_ONLINE_CG");
            int a = db.UpdateT01_request_Online("400", "5858691");
            string json= null;
            loadJson l = new loadJson();
            l.ReadAppConfig();
            if (l.isLoadTestFile())
            {
                string saveformClaim;
                saveformClaim = l.ReadsaveFormClaim();
               // json = Newtonsoft.Json.JsonConvert.SerializeObject(saveformClaim);
            }
            else
            {
                SaveFormClaimRoot sCR = new SaveFormClaimRoot();
                FacadeSaveFormClaim facade = new FacadeSaveFormClaim(lgno);
                sCR = ClientFacadeSaveFormClaim.ClientCode(facade);
                bool found;
                SaveFormClaim.Content lgInfo = GetLgByBank(facade.bankId, facade.lgNo, token, url, out found);
                //update lgID get from CGS 
                if (found)
                {
                    sCR.lgId = lgInfo.lgId;
                }


                json = Newtonsoft.Json.JsonConvert.SerializeObject(sCR);
            }

                      
               restRequest.AddParameter("application/json", json, ParameterType.RequestBody);
            

           // restRequest.RequestFormat = DataFormat.Json;

            try
            {
                IRestResponse restResponse = restClient.Execute(restRequest);
                JObject obj = JObject.Parse(restResponse.Content);
                // update log 
                databaseUtil.log(lgno,"postclaim","S");
                //update status
               // db.UpdateT01_request_Online("400", lgno);

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex.InnerException.Message.ToString();
                }
            }

        }
        //รายละเอียดคำขอลดวงเงิน
        public void GetAdjustGuaLoanByLgId(string Token,string url)
        {
            string token = Token;
            var restClient = new RestSharp.RestClient(url);
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyError) => true;

            RestRequest restRequest = new RestRequest(serviceGetAdjustGuaLonnByLgId, Method.GET);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer" + token);
            string testdate = "25620930";
            string format = "YYYY-mm-dd";
            //Create param for Service
            AdjustGuaLoanByLgID param = new AdjustGuaLoanByLgID();
            param.P_LG_ID = 736;
            param.P_POST_REQUEST_ID = 245;
            param.P_POST_REQ_CHANNELL = "01";
            param.P_APPROVE_DT = DateTime.Parse(testdate) ; //2020-11-18T10:58:55+07:00
            param.P_POST_REQ_SEND_DT = Convert.ToDateTime( "2021-05-14");//
            param.P_DOCUMENT_TYPE_CODE = "POST14";

            restRequest.AddParameter("P_LG_ID",param.P_LG_ID,ParameterType.GetOrPost);
            restRequest.AddParameter("P_POST_REQUEST_ID",param.P_POST_REQUEST_ID,ParameterType.GetOrPost);
            restRequest.AddParameter("P_POST_REQ_CHANNELL",param.P_POST_REQ_CHANNELL,ParameterType.GetOrPost);
            restRequest.AddParameter("P_APPROVE_DT", param.P_APPROVE_DT, ParameterType.GetOrPost);
            restRequest.AddParameter("P_POST_REQ_SEND_DT",param.P_POST_REQ_SEND_DT,ParameterType.GetOrPost);
            restRequest.AddParameter("P_DOCUMENT_TYPE_CODE",param.P_DOCUMENT_TYPE_CODE,ParameterType.GetOrPost);



            try {
                IRestResponse restResponse = restClient.Execute(restRequest);
                JObject tk = JObject.Parse(restResponse.Content);
           
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex.InnerException.Message.ToString();
                }
            }
        }
        public SaveFormClaim.Content GetLgByBank(string bank, string lgNo, string Token, string url,out bool found)
        {
            found = false;
            SaveFormClaim.Content lgInfo = new Content();
            string token = Token;
            var restClient = new RestSharp.RestClient(url);
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyError) => true;

            RestRequest restRequest = new RestRequest(serviceGetLgInfo, Method.GET);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Authorization", "Bearer" + token);

            LGInformation param = new LGInformation();
            param.bankId = bank;
            param.lgNo = lgNo;

            restRequest.AddParameter("page", param.page, ParameterType.GetOrPost);
            restRequest.AddParameter("perPage", param.perPage, ParameterType.GetOrPost);
            restRequest.AddParameter("documentTypeCode", param.documentTypeCode, ParameterType.GetOrPost);
            restRequest.AddParameter("bakId", param.bankId, ParameterType.GetOrPost);
            restRequest.AddParameter("lgNo", param.lgNo, ParameterType.GetOrPost);

            
            try
            {
                IRestResponse restResponse = restClient.Execute(restRequest);
                LGClaimInfo lgCliaminfo = Newtonsoft.Json.JsonConvert.DeserializeObject<LGClaimInfo>(restResponse.Content);
                if (lgCliaminfo != null)
                {
                    foreach (SaveFormClaim.Content c in lgCliaminfo.content)
                    {
                        //if (c != null)
                        //{
                        //    if (SearchLgInfo(c, lgNo))
                        //    {
                        //        lgInfo = c;
                        //    }
                        //}
                        lgInfo = c;
                        found = true;
                    }

                }
                
            }
             catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    ex.InnerException.Message.ToString();
                }
            }
          

            return lgInfo;
        }

        private bool SearchLgInfo(Content c,string lgNo)
        {
            
            if (c.lgNo ==lgNo)
            {
               // Console.WriteLine("Lg No: " +c.lgNo) ;
                
                return true;
            }
            return false;
        }
    }
}
