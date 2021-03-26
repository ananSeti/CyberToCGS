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
        Product a;
        public void deserial()
        {
            JObject obj = JObject.Parse(json);

            if (obj["product"] != null)
            {
                string data = obj["product"].ToString();
                 a = JsonConvert.DeserializeObject<Product>(data);
                //foreach (var item in a)
                //{
               // JsonConvert.SerializeObject(a);
                Console.WriteLine("Product:" + JsonConvert.SerializeObject(a));
                //}
                
            }
        }

        T IcgsData.getData<T>()
        {
            throw new NotImplementedException();
        }
    }
}
