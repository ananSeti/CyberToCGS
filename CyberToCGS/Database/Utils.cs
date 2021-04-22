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
    }
}
