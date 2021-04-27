using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS
{
    public class Bank
    {
        public int bankId { get; set; }
        public int bankBrnUseLimit { get; set; }
        public int bankBrnSendOper { get; set; }
        public string guaCareName { get; set; }
        public string guaCareMobile { get; set; }
        public string guaCarePhone { get; set; }
        public string guaCareEmail { get; set; }
        public string guaApproveEmail { get; set; }
        public string guaRemark { get; set; }
        //public SqlDataReader rec { get; set; }
        protected string T01OnlineID;
        public void operation(string T01OnlineID)
        {
            SqlDataReader rec;
            this.T01OnlineID = T01OnlineID;
            Database.Database db2 = Database.Database.GetInstance("DB_CGSAPI_MASTER");
            rec = db2.GetViewCgsapiBank(T01OnlineID);

           while(rec.Read())
            {
                bankId = Convert.ToInt32( rec["bankId"]);// 4;
                bankBrnUseLimit = Convert.ToInt32(rec["bankBrnUseLimit"]);//1687;
                bankBrnSendOper = Convert.ToInt32(rec["bankBrnSendOper"]);//1687;
                guaCareName = rec["guaCareName"].ToString();//"ม้า";
                guaCareMobile = rec["guaCareMobile"].ToString();//"0336569898";
                guaCarePhone = rec["guaCarePhone"].ToString();//"0336569898";
                guaCareEmail = rec["guaCareEmail"].ToString();// "nn@zz.com";
                guaApproveEmail = rec["guaApproveEmail"].ToString(); // "nn@zz.com";
                guaRemark = rec["guaRemark"].ToString();//"";
            }


        }
    }
}
