using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.FactoryPattern
{
    public class DataFactory
    {
        public  IcgsData getData(String dataType)
        {
            if(dataType== null)
            {
                return null;
            }
            if (dataType.Equals("Product"))
            {
                return new ProductFactory();
            }
            if(dataType.Equals("Customer"))
            {
                return new CustomerFactory();
            }
                return null;
        }
    }
}
