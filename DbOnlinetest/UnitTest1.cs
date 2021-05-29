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
        [TestMethod]
        public string CellPutValue(string Value, string Cube, string[,] Dimensions)
        {
            //create body header
            string body = @"{
                            ""Cells"":[
                                { ""Tuple@odata.bind"": [";
            for (int i = 0; i < Dimensions.GetLength(0); i++)
            {
                //check if array data is correct...
                if (Dimensions[i, 0] == "")
                    break;

                //for cleanness, used temp vars for reading out the array and build the body
                string dim = Dimensions[i, 0];
                string hierarchy = Dimensions[i, 1] == null ? Dimensions[i, 0] : Dimensions[i, 1];
                string dimEl = Dimensions[i, 2];

                //loop through the array and construct the body json
                if (i < Dimensions.GetLength(0) - 1)
                {
                    body = body + @"
                                       ""Dimensions('" + dim + @"')/Hierarchies('" + hierarchy + @"')/Elements('" + dimEl + @"')"",";
                }
                else
                {
                    body = body + @"
                                       ""Dimensions('" + dim + @"')/Hierarchies('" + hierarchy + @"')/Elements('" + dimEl + @"')""";
                }
            }
            //add body footer
            body = body + @"
                    ]
            }
            ],
            ""Value"":""" + Value + @"""
        }";

            var request = new RestRequest("Cubes('" + Cube + "')/tm1.Update", Method.POST);
           // request.AddCookie(sessionCookie.Name, sessionCookie.Value);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            //   IRestResponse response = restClient.Execute(request);
            //return response
            return "S"; //esponse.Content;
        }

    }
}
