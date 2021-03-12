using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.FactoryPattern
{
    public class ProductFactory : CgsAbstract, IcgsData
    {
        public void deserial()
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
    }
}
