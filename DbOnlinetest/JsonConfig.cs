using CyberToCGS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DbOnlinetest
{
    [TestClass]
    public class JsonConfig
    {
        [TestMethod]
        public void LoadAppConfig()
        {
            loadJson l = new loadJson();
            l.ReadAppConfig();  //1.
                                //  l.GetFromDate();    //2.
                                //  l.SetformatDateTime(); //3.
            Console.WriteLine( "From Date format :" + l.GetFormatFromdate());//4.
            Console.WriteLine( "To Date format : " + l.GetFormatTodate());//. 5

        }
    }
}
