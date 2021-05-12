using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CyberToCGS;

namespace DbOnlinetest
{
    [TestClass]
    public class UnitTestindirectPost
    {
        [TestMethod]
        public void IndirectPostTestJson()
        {
            string token = "";
            var urlSME = "https://sme-bank.tcg.or.th";
            var urlTCG = "https://cgs.tcg.or.th";
            var cgs = new CGS();
            //cgs.AuthenticationBasics(ref token, urlTCG);
            cgs.AuthenticationBasics(ref token, urlSME);
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
                cgs.IndirectPostTestJson(token, urlSME);
            }

        }
    }
}

/* TEST Data Factory   */
// DataFactory dataFactory = new DataFactory();
// IcgsData product = dataFactory.getData("Product");
// product.deserial();
// string a = product.getData();


//  IcgsData customer = dataFactory.getData("Customer");
//  customer.deserial();

//  indirectRequest.product = p
//  indirectRequest.bank = b;
//  indirectRequest.customer = cust;