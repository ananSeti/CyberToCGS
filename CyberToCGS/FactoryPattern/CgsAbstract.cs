using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.FactoryPattern
{
   public abstract class CgsAbstract
    {
        public string json;
        public CgsAbstract()
        {
            string fileName = @"D:\tcg\Cyber\CyberToCGS\CyberToCGS\Json\request.json";
            using (System.IO.StreamReader r = new System.IO.StreamReader(fileName))
            {
                json = r.ReadToEnd();
            }
           // return json;
        }
    }
}
