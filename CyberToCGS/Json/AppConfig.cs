using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.Json
{
   public class AppConfigClass
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string urlSME { get; set; }
        public string urlTCG { get; set; }
        public string ReadTestFile { get; set; }
        public string fromYear { get; set; }
        public string fromMonth { get; set; }
        public string fromDay { get; set; }
        public string GetformatFromDate()
        {
            return fromYear + fromMonth + fromDay;
        }
        public string toYear { get; set; }
        public string toMonth { get; set; }
        public string toDay { get; set; }
        public string GetformatToDate()
        {
            return toYear + toMonth + toDay;
        }
    }
}
