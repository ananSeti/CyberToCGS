using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "";
            /*-------------------------------
             *  เร่ิ่มต้นด้วย การขอ TOKEN
             *-------------------------------
             *1. Reading URL Config 
             *2. read SME URL  
             *https://sme-bank.tcg.or.th
             *-------------------------------
             */

            // var urlSME = "http://192.168.15.17:31380";
            var urlSME = "https://sme-bank.tcg.or.th";
            // /requestservice/api/external/request
            //authentication-service/oauth/token

            var cgs = new CGS();
            cgs.AuthenticationBasics(ref token,urlSME);

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("can not get token");
            }
            else
            {
                /* ---------------------------------
                 * เริ่ม Call API ตรงนี้
                 * htt://<HOST_NAME>/request-service/api/external/request
                 *6. บันทึกคำขอแบบ indirect
                 *----------------------------------
                */
                cgs.IndirectPost(token, urlSME);
            }




        }
    }
}
