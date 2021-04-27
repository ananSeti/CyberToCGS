using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;


namespace DbOnlinetest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string url = "https://abc.example.com/";
            string jsonString = "{" +
                    "\"auth\": {" +
                        "\"type\" : \"basic\"," +
                        "\"password\": \"@P&p@y_10364\"," +
                        "\"username\": \"prop_apiuser\"" +
                    "}," +
                    "\"requestId\" : 15," +
                    "\"method\": {" +
                        "\"name\": \"getProperties\"," +
                        "\"params\": {" +
                            "\"showAllStatus\" : \"0\"" +
                        "}" +
                    "}" +
                "}";

            IRestClient client = new RestClient(url);
            IRestRequest request = new RestRequest("api/properties", Method.POST, DataFormat.Json);
            request.AddHeader("Content-Type", "application/json; CHARSET=UTF-8");
            request.AddJsonBody(jsonString);

            var response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}
