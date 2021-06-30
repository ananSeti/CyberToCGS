using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace CyberToCGS.PDFLoad
{
  public  class PdfLoader
    {
        string url;
        string location = @"https://www.tcgcyber.com/claim/documents";
        string Year;
        string claimNo;
        string fileName;
        public PdfLoader(string year,string claimNo,string filename) {
            
            this.Year = year;
            this.claimNo = claimNo;
            this.fileName = filename;
            //https://www.tcgcyber.com/claim/documents/64/C64013032/QzY0MDEzMDMyLTFfMQ==.pdf
            url = location + "/" + year + "/" + claimNo + "/" + filename;
        }

        public byte[] getPdf() {
            // System.Diagnostics.Process.Start(https://www.tcgcyber.com/claim/documents/64/C64013032/QzY0MDEzMDMyLTFfMQ==.pdf);
            // byte[] ret;
            byte[] arr;
            using ( WebClient client = new WebClient()) {
                 arr = client.DownloadData(url);
                
              }

            return arr;
            }
          public void checkSysdate()
        {

        }
        public string getExtension(string f) {
            string filename = f;//this.rec["T01File_1"].ToString();
          return  filename.Substring(filename.LastIndexOf(".")).Replace(".", "");
          
        }
        public string getFileName(string f) {

           return f.Substring(0, f.LastIndexOf(".") - 1);

                  }
    }
}
