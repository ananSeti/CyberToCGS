using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberToCGS.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CyberToCGS
{
    
    public class loadJson
    {
       public string json;
       public string AppConfig;
        AppConfigClass app;

        public string ReadsaveFormClaim()
        {
            // string filename = "success.json"; //"request.json";
            string filename = "saveFormClaim.json";// "request.json";
            filename = Path.Combine(Environment.CurrentDirectory, filename);
            // string fileName = @"D:\tcg\Cyber\CyberToCGS\CyberToCGS\Json\request.json";
            using (System.IO.StreamReader r = new System.IO.StreamReader(filename))
            {
                json = r.ReadToEnd();
            }
            return json;
        }
        public string ReadJson()
        {
            // string filename = "success.json"; //"request.json";
            string filename = "20210429_102335_Json4Request.json";// "request.json";
            filename = Path.Combine(Environment.CurrentDirectory, filename);
            // string fileName = @"D:\tcg\Cyber\CyberToCGS\CyberToCGS\Json\request.json";
            using (System.IO.StreamReader r = new System.IO.StreamReader(filename))
            {
                json = r.ReadToEnd();
            }
            return json;
         }
        public void DeserialProduct()
        {
            JObject obj = JObject.Parse(json);
           
            if (obj["product"] != null)
            {
                string data = obj["product"].ToString();
                Product a = JsonConvert.DeserializeObject<Product>(data);
                //foreach (var item in a)
                //{
                   Console.WriteLine(".................");
                //}
            }
        }
        public void DeserialCutomerArray()
        {
            JObject obj = JObject.Parse(json);

            if (obj["customer"] != null)
            {
                string data = obj["customer"].ToString();
               List<Customer> a = JsonConvert.DeserializeObject<List<Customer>>(data);
                //foreach (var item in a)
                //{
               // Console.WriteLine(".................");
                //}
            }
        }
        public string ReadAppConfig()
        {
            string appConfig = "Appconfig.json";
            appConfig = Path.Combine(Environment.CurrentDirectory, appConfig);
            using(System.IO.StreamReader r = new System.IO.StreamReader(appConfig))
            {
                AppConfig = r.ReadToEnd();
            }
            return AppConfig;
        }
        public void GetFromDate()
        {
            JObject obj = JObject.Parse(AppConfig);
            if(obj["selectDate"] != null)
            {
                string data = obj["selectDate"].ToString();
                 app = JsonConvert.DeserializeObject<AppConfigClass>(data);
                      
            }
        }
        public void SetformatFromDateTime()
        {
            string d;
            string m;
            string y;
            this.GetFromDate();
            string[] a = app.fromDate.ToString().Split('/');
             if(a[0].Length == 1)
            {
                d = "0" + a[0];
                
            }
            else
            {
                d = a[0];
            }
             if(a[1].Length == 1)
            {
                m = "0" + a[1];
            }
            else
            {
                m = a[1];
            }
             if(a[2].Substring(0,2) == "20")
            {
                y = (Convert.ToInt16(a[2]) + 543).ToString();
            }else
            {
                y = a[2];
            }

            app.fromDay = d;
            app.fromMonth = m;
            app.fromYear = y;

           //var test = app.GetformatFromDate();
        }
        public string GetFormatFromdate()
        {
            this.SetformatFromDateTime();
            return app.GetformatFromDate();
        }
        public void SetformatToDateTime()
        {
            string d;
            string m;
            string y;
            this.GetFromDate();
            string[] a = app.toDate.ToString().Split('/');
            if (a[0].Length == 1)
            {
                d = "0" + a[0];

            }
            else
            {
                d = a[0];
            }
            if (a[1].Length == 1)
            {
                m = "0" + a[1];
            }
            else
            {
                m = a[1];
            }
            if (a[2].Substring(0, 2) == "20")
            {
                y = (Convert.ToInt16(a[2]) + 543).ToString();
            }
            else
            {
                y = a[2];
            }

            app.toDay = d;
            app.toMonth = m;
            app.toYear = y;

            //var test = app.GetformatFromDate();
        }
        public string GetFormatTodate()
        {
            this.SetformatToDateTime();
            return app.GetformatToDate();
        }
     public bool isUrlSME()
        {
            this.GetFromDate();
            if (this.app.urlSME == "True")
            {
                return true;
            }
            else return false;
        }
        public bool isUrlTCG()
        {
            this.GetFromDate();
            if (this.app.urlTCG == "True")
            {
                return true;
            }
            else return false;
        }
        public bool isLoadTestFile()
        {
            this.GetFromDate();
            if (this.app.ReadTestFile == "True")
            {
                return true;
            }
            else return false;
        }
    }
}
