using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberToCGS.SaveFormClaim;
namespace CyberToCGS
{
    class ClientFacadeIndirect
    {
     
        
        public static IndirectRequest ClientCode(FacadeIndirect facade)
        {
          return  facade.Operation();
        }
    }
    class ClientFacadeSaveFormClaim
    {
        public static SaveFormClaimRoot ClientCode(FacadeSaveFormClaim facade)
        {
            return facade.Operation();
        }
    }
}
