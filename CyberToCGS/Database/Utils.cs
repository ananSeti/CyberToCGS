using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.Database
{
  public  class Utils
    {
        private List<string> _localDate = new List<string>();

       public Utils()
        {

        }
        public void GetSystemDate()
        {
            DateTime localDate = DateTime.Now;
            String[] cultureNames = { "en-US", "th-TH" };
                           

            foreach (var cultureName in cultureNames)
            {
                var culture = new CultureInfo(cultureName);
               
                Console.WriteLine("{0}: {1}", cultureName,
                                        localDate.ToString(culture));
            }

        }
        public void GetSystemDateThai()
        {
            DateTime localDate = DateTime.Now;
            String[] cultureNames = { "th-TH" };


            foreach (var cultureName in cultureNames)
            {
                var culture = new CultureInfo(cultureName);
                Console.WriteLine("--- Get Thai ---");
                Console.WriteLine("{0}: {1}", cultureName,
                                        localDate.ToString(culture));
            }

        }
        public DateTime? ConvertYear(string bc)
        {
            string s = bc;
            const string V = "yyyy-MM-dd";

            if (bc != "")
            {
                string y = s.Substring(0, 4);
                int c = Convert.ToInt32(y) - 543;
                string mm = s.Substring(4, 2);
                string dd = s.Substring(6, 2);
               
                return Convert.ToDateTime(string.Format("{0}-{1:00}-{2:00} 00:00:00", c.ToString(), mm, dd, V, CultureInfo.InvariantCulture));
            }
            else
            {
                return null;
            }
           // string test = s.Replace(y, c.ToString());
           
        }
        public int ClaimAmountCheck(System.Data.SqlClient.SqlDataReader rec)
        {
            int ret = 0;
            if (rec["T01Claim_Amount"] != null)
            {
                ret = Convert.ToInt32(rec["T01Claim_Amount"]);
            }
                       
            return ret;
        }
        public void log(string lgno,string logtype,string status,string json)
        {
            Database db = Database.GetInstance("localDB");
            logData log = new logData();
            log.lgNo = lgno; // "LG123456";
            log.logDate = DateTime.Now;
            log.method = logtype;
            log.status = status;
            log.JsonPost = json;
            db.LogData(log);
        }
    }
}
