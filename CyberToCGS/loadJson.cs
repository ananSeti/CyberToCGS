using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CyberToCGS
{
    
    public class loadJson
    {
       public string json;
        public string ReadJson()
        {
           
            string fileName = @"D:\tcg\Cyber\CyberToCGS\CyberToCGS\Json\request.json";
            using (System.IO.StreamReader r = new System.IO.StreamReader(fileName))
            {
                json = r.ReadToEnd();
            }
            return json;
         }
        public void DeserialProduct()
        {
            JObject obj = JObject.Parse(json);
           
            if (obj["product"] != null)
            {
                string data = obj["product"].ToString();
                Product a = JsonConvert.DeserializeObject<Product>(data);
                //foreach (var item in a)
                //{
                   Console.WriteLine(".................");
                //}
            }
        }
        public void DeserialCutomerArray()
        {
            JObject obj = JObject.Parse(json);

            if (obj["customer"] != null)
            {
                string data = obj["customer"].ToString();
               List<Customer> a = JsonConvert.DeserializeObject<List<Customer>>(data);
                //foreach (var item in a)
                //{
               // Console.WriteLine(".................");
                //}
            }
        }
    }
}
