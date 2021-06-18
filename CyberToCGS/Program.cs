using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberToCGS.FactoryPattern;
using CyberToCGS.Database;
using System.Data.SqlClient;

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
            //read config
           // loadJson l = new loadJson();
           // l.ReadAppConfig();
            //string js = l.ReadJson();
            //     l.DeserialProduct();
            //    l.DeserialCutomerArray();
            /*---------------------*/
            string tokenSME = "";
            string tokenTCG = "";
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
            var urlSBCG = "https://cgs.sbcg.or.th";
            var localSME = "http://localhost:8080";
            // /requestservice/api/external/request
            //authentication-service/oauth/token
            loadJson l = new loadJson();
            var cgs = new CGS();
          // cgs.AuthenticationBasics(ref tokenTCG, urlTCG);
            cgs.AuthenticationBasics(ref tokenSME,urlSME);

            Database.Database db= Database.Database.GetInstance("DB_ONLINE_CG");
            SqlDataReader rec= db.GetT01_Request_online_lgNo();

            if (string.IsNullOrEmpty(tokenSME ))
            {
                Console.WriteLine("-------- Can not get SME token ------------");
            }
            else
            {
                /* ---------------------------------
                 * เริ่ม Call API ตรงนี้
                 * htt://<HOST_NAME>/request-service/api/external/request
                 *6. บันทึกคำขอแบบ indirect
                 *----------------------------------
                */
                
                l.ReadAppConfig();
                if ( l.isUrlSME())
                {
                    //cgs.IndirectPost(token, urlSME);

                    while (rec.Read())
                    {
                        //localSME
                        //LG test :222910
                        // cgs.SaveRequestClaimPGSPackage("5910612 ", tokenSME, localSME);
                        // cgs.SaveRequestClaimPGSPackage("5910612", tokenSME, urlSME);
                        // cgs.SaveRequestClaimPGSPackage("60080702", tokenSME, urlSME);
                        // 5619990
                        var testLg = new List<string> { "5619990" };  //{ "5910612", "60080702" };
                        testingLg(cgs, testLg, tokenSME, urlSME);
                        //  cgs.SaveRequestClaimPGSPackage(rec.GetValue(0).ToString(), tokenSME, localSME);
                        //  cgs.SaveRequestClaimPGSPackage(rec.GetValue(0).ToString(), tokenSME, urlSME);
                        // cgs.SaveRequestClaimPGSPackage("222910", tokenSME, urlSME);
                    }
                }
                
            }

            if (string.IsNullOrEmpty(tokenTCG)){
                Console.WriteLine("======= Can not get TCG token");
            }
            else
            {
               
                l.ReadAppConfig();
                if (l.isUrlTCG())
                {

                    //P300
                    //get 47.	รายละเอียดคำขอลดวงเงินค้ำประกัน
                    // cgs.GetAdjustGuaLoanByLgId(token, urlTCG);
                    while(rec.Read())
                    //cgs.SaveRequestClaimPGSPackage(rec.GetValue(0).ToString(),tokenTCG, urlTCG);
                    cgs.SaveRequestClaimPGSPackage("60080702", tokenTCG, urlTCG);
                    // cgs.SaveRequestClaimPGSPackage(token, urlSME);
                }
            }


        }

        private static void testingLg(CGS cgs, List<string> testLg, string tokenSME, string urlSME)
        {
            foreach (string lg in testLg)
            {
                cgs.SaveRequestClaimPGSPackage(lg, tokenSME, urlSME);
                //Branch ANan
            }
        }
    }
}
