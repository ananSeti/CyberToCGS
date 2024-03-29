﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CyberToCGS.Database;
using CyberToCGS;
using System.Data;
using System.Data.SqlClient;

namespace DbOnlinetest
{
    [TestClass]
    public class DbConnectTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void TestConnectDatabase()
        {
            Database db = Database.GetInstance("localDB");
            db.GetUser();
            db.GetUser(2);

            db.SetConnection("DB_ONLINE_CG");
            //assert
           
        }
        [TestMethod]
        public void TestInsertLog()
        {
            Database db = Database.GetInstance("localDB");
            logData log = new logData();
            log.lgNo = "LG123456";
            log.logDate = DateTime.Now;
            log.method = "GET service1";
            db.LogData(log);

        }
        [TestMethod]
        public void testDB_online_CG()
        {
            string fromDate;
            string toDate;

            loadJson l = new loadJson();
            l.ReadAppConfig();
            fromDate = l.GetFormatFromdate();
            toDate = l.GetFormatTodate();
            Database db = Database.GetInstance("DB_ONLINE_CG");
            SqlDataReader rec= db.GetT01_Request_Online(fromDate, toDate);
             while (rec.Read())
            Console.WriteLine( rec.GetValue(1));

          

        }
        [TestMethod]
        public void testDB_online_CG_Request()
        {
            string fromDate;
            string toDate;

            loadJson l = new loadJson();
            l.ReadAppConfig();
            fromDate = l.GetFormatFromdate();
            toDate = l.GetFormatTodate();
            Database db = Database.GetInstance("DB_ONLINE_CG");
            //  db.GetT01_Request_Online(fromDate, toDate);
            //request no =5703591
          SqlDataReader rec =  db.GetT01_Request_Online("5703591");

            while (rec.Read())
                Console.WriteLine(rec.GetValue(0) + "-"+ rec.GetValue(2));

        }


        [TestMethod]
        public void testTime()
        {
            Utils u = new Utils();
            u.GetSystemDate();
            u.GetSystemDateThai();
        }
        [TestMethod]
       public void getNewClaimLg()
        {
            Database db = Database.GetInstance("DB_ONLINE_CG");
            SqlDataReader rec = db.GetT01_Request_online_lgNo();
            while (rec.Read())
                Console.WriteLine(rec.GetValue(0));

        }
        [TestMethod]
        public void UpdateLGStatus()
        {
            Database db = Database.GetInstance("DB_ONLINE_CG");
            int a = db.UpdateT01_request_Online("400", "5858691");
            Console.WriteLine("update lgno : {0}  and Status {1} " ,"5858691","800"  );
        }
    }
}
