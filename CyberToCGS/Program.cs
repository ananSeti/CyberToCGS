using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberToCGS.FactoryPattern;


namespace CyberToCGS
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //Database.Database db = Database.Database.GetInstance("localDB");
            //db.GetUser();
            //db.GetUser(2);

            //db.SetConnection("DB_ONLINE_CG");

            /*test load Json */
            /* ทดลองการ load Json file   */
            //loadJson l = new loadJson();
            //string js = l.ReadJson();
            //     l.DeserialProduct();
            //    l.DeserialCutomerArray();
            /*---------------------*/
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
            var urlTCG = "https://cgs.tcg.or.th";
            // /requestservice/api/external/request
            //authentication-service/oauth/token

            var cgs = new CGS();
            cgs.AuthenticationBasics(ref token, urlTCG);
          //  cgs.AuthenticationBasics(ref token,urlSME);

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("-------- Can not get token ------------");
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
