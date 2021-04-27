using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS
{
    class ClientFacadeIndirect
    {
     
        
        public static IndirectRequest ClientCode(FacadeIndirect facade)
        {
          return  facade.Operation();
        }
    }
}
