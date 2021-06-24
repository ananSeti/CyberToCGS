using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CyberToCGS.PDFLoad;
using System.Net;
using System.IO;

namespace DbOnlinetest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestGetFile()
        {
            byte[] test;
            PdfLoader pdfLoader = new PdfLoader("64", "C64013032", "QzY0MDEzMDMyLTFfMQ==.pdf");
            test = pdfLoader.getPdf();
        }

        [TestMethod]
        public void Testy() {
            using (WebClient client = new WebClient())
            {

                // Download data.
                byte[] arr = client.DownloadData("https://www.tcgcyber.com/claim/documents/64/C64013032/QzY0MDEzMDMyLTFfMQ==.pdf");

              ///  File.WriteAllBytes(path_to_your_app_data_folder, arr)
   
    }
        
    }
    }
}
