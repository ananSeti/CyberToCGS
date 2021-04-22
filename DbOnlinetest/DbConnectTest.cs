using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CyberToCGS.Database;
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
        public void testDB_online_CG()
        {
            Database db = Database.GetInstance("DB_ONLINE_CG");
            db.GetT01_Request_Online();
        }

        [TestMethod]
        public void testTime()
        {
            Utils u = new Utils();
            u.GetSystemDate();
            u.GetSystemDateThai();
        }
    }
}
