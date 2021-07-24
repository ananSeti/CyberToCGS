using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CyberToCGS.PDFLoad;
using System.Net;
using System.IO;
using System.Globalization;


  

namespace DbOnlinetest
{
   
    [TestClass]
    public static class UnitTest2
    {
        [TestMethod]
        public static void TestGetFile()
        {
            byte[] test;
            PdfLoader pdfLoader = new PdfLoader("64", "C64013032", "QzY0MDEzMDMyLTFfMQ==.pdf");
            test = pdfLoader.getPdf();
        }

        [TestMethod]
        public static void Testy() {

            DateTime ret;
            var mydate = "2016/31/05 13:33";
            string T01Create_Date = "25610208";
            string T01Create_Time = "142607";
            ret = DateTime.ParseExact("2021-07-23 14:30:52", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            ret = DateTime.Parse("2021-07-23 14:30:52");
           
            using (WebClient client = new WebClient())
            {

                // Download data.
                byte[] arr = client.DownloadData("https://www.tcgcyber.com/claim/documents/64/C64013032/QzY0MDEzMDMyLTFfMQ==.pdf");

              ///  File.WriteAllBytes(path_to_your_app_data_folder, arr)
   
    }
       
    }


       // [TestMethod]
        //public DateTime TestThaiDateTime()
        //{
        //    DateTime ret;
        //    string T01Create_Date = "25610208";
        //    string T01Create_Time = "142607";
        //    ret = DateTime.ParseExact("2021-07-23 14:30:52", "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        //    return ret;
        //}
        //public DateTime ToDateTime(this string s,
        //            string format, CultureInfo culture)
        //{
        //    try
        //    {
        //        var r = DateTime.ParseExact(s: s, format: format,
        //                                    provider: culture);
        //        return r;
        //    }
        //    catch (FormatException)
        //    {
        //        throw;
        //    }

        //}

        //public override string ToString()
        //{
        //    return base.ToString();
        //}
    }
}
