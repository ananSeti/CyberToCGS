using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.FactoryPattern
{
    public class CustomerFactory : CgsAbstract, IcgsData
    {
        public void deserial()
        {
            Newtonsoft.Json.Linq.JObject obj = JObject.Parse(json);

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
