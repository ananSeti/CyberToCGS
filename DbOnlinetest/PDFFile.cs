using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.IO;
using iText.Kernel.Pdf;

namespace DbOnlinetest
{
    [TestClass]
    public class ReadPDFFile
    {
        [TestMethod]
        public void ReadPDFFileEx()
         {
            byte[] bytes=null;
            string fileName = @"D:\(linuxCommand)TLCL-19.01.pdf";
            string outFile = @"D:\testOut.pdf";
           // StringBuilder text = new StringBuilder();
            if(File.Exists(fileName))
            {
               //PdfReader pdfReader = new PdfReader(fileName);
              using(FileStream stream = File.Open(fileName, FileMode.Open))
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        bytes = br.ReadBytes((int)stream.Length);
                    }
                }
            }

            var filestream = File.OpenWrite(outFile);
            BinaryWriter bw = new BinaryWriter(filestream);
            bw.Write(bytes, 0, (int)bytes.Length);
            bw.Close();
        }
    }
}
