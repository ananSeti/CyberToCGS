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
            var UAT = "http://192.168.15.17:31380"; // TCG
            // http://192.168.15.17:31380/authentication-service/oauth/token // UAT
            // /requestservice/api/external/request
            //authentication-service/oauth/token
            loadJson l = new loadJson();
            var cgs = new CGS();
          // cgs.AuthenticationBasics(ref tokenTCG, urlSME);
            cgs.AuthenticationBasics(ref tokenTCG, urlSBCG);
            // cgs.AuthenticationBasics(ref tokenSME,urlSME);
            //DB_CLAIM_ONLINE_PROD
           // TEST CI LG owner....
            //Database.Database db= Database.Database.GetInstance("DB_ONLINE_CG"); 

            Database.Database db = Database.Database.GetInstance("DB_ONLINE_CG_PROD");
           
           
            SqlDataReader rec = db.GetT01_Request_online_lgNo();
            //ข้อมูลทดสอบ  "60080702","5910612","60034524","62041574"
            var testLg = new List<string> { "60104765" };   //5619990 5910612 60034524

            if (string.IsNullOrEmpty(tokenSME ))
            {
                Console.WriteLine("-------- Can not get SME token ------------");
            }
            else
            {
                /* ---------------------------------
                 * เริ่ม Call API ตรงนี้
                 * htt://<HOST_NAME>/request-service/api/external/request
                 * 
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
                       // var testLg = new List<string> { "5619990" };  //{ "5910612", "60080702" };
                       // testingLg(cgs, testLg, tokenSME, urlSME);
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
                    while (rec.Read())
                    {
                        //// TEST CI LG owner....
                        ///DB_ONLINE_CG
                        ///DB_ONLINE_CI
                        Database.Database dbCI = Database.Database.GetInstance("DB_ONLINE_CI");
                       // dbCI = Database.Database.GetInstance("DB_ONLINE_CG");
                       string t01UserCode =  dbCI.GetLGOnwer(rec.GetValue(1).ToString());
                        ////
                        ///Check Duplicate


                        // string cl = rec.GetValue(1).ToString();
                        // testingLg(cgs, testLg, rec.GetValue(1).ToString(), tokenTCG, urlSBCG);
                        Database.Database dbInterface = Database.Database.GetInstance("DB_CGS_INTERFACE");
                        if (!dbInterface.isExisting(rec.GetValue(0).ToString()))
                        {
                            cgs.SaveRequestClaimPGSPackage(rec.GetValue(0).ToString(), rec.GetValue(1).ToString(), tokenTCG, urlSBCG);
                        }
                        // 60034524  
                        //var testLg = new List<string> { "5619990" };

                        // cgs.SaveRequestClaimPGSPackage("60034524", tokenTCG, urlTCG);  //60034524   //60080702
                        // cgs.SaveRequestClaimPGSPackage(token, urlSME);
                    }

                    }
            }


        }

        private static void testingLg(CGS cgs, List<string> testLg,string lgID, string tokenUsed, string urlUsed)
        {
            foreach (string lg in testLg)
            {
              cgs.SaveRequestClaimPGSPackage(lg,lgID, tokenUsed, urlUsed);
                
            }
        }
    }
}
