using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.SaveFormClaim
{
    public class AdjustGuaLoanByLgID
    {
        public int P_LG_ID { get; set; }
        public int P_POST_REQUEST_ID { get; set; }
        public string P_POST_REQ_CHANNELL { get; set; }
        public DateTime P_APPROVE_DT { get; set; }
        public DateTime P_POST_REQ_SEND_DT { get; set; }
        public string P_DOCUMENT_TYPE_CODE { get; set; }

    }
    public class LGInformation
    {
        public string page { get; set; }
        public string perPage { get; set; }
        public string documentTypeCode { get; set; }
        public string bankId { get; set; }
        public string lgNo { get; set; }

         public LGInformation()
         {
            this.page ="1";
            this.perPage = "10";
            this.documentTypeCode = "POST36";
         }
    }
}
        