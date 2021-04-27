using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS
{
    public class Product
    {

        public string preReqStatus { get; set; }
        public int productId { get; set; }
        public int roundId { get; set; }
        public string guaAmount { get; set; }
        public int? prdPayFeeType { get; set; }
        public string prdReduGuaType { get; set; }
        public string refNo1 { get; set; }
        public string refNo2 { get; set; }
        public string refNo3 { get; set; }
        public int? advFeeYearId { get; set; }
        public string reduce { get; set; }
        public string preReqSendDt { get; set; }
      //  public SqlDataReader rec { get; set; }

        protected string T01OnlineID;
       
        public void operation(string T01Online_ID)
        {
            this.T01OnlineID = T01Online_ID;

            //while (rec.Read())
            //{
            //    T01OnlineID = rec["T01Online_ID"].ToString();

            //}
            SqlDataReader rec;
            Database.Database db2 = Database.Database.GetInstance("DB_CGSAPI_MASTER");
            rec = db2.GetViewCgsapiProduct(T01OnlineID);
            while (rec.Read())
            {
                preReqStatus = rec["preReqStatus"].ToString();
                productId = Convert.ToInt32(rec["productId"]);// 215;
                roundId = string.IsNullOrEmpty(rec["roundId"].ToString())? 1 : Convert.ToInt32(rec["roundId"]); // 1;
                guaAmount = rec["guaAmount"].ToString(); //"20000000";
                prdPayFeeType =  string.IsNullOrEmpty(rec["prdPayFeeType"].ToString())? (int?)null : Convert.ToInt32(rec["prdPayFeeType"]);//1;
                prdReduGuaType = rec["prdReduGuaType"].ToString();// null;
                refNo1 =  rec["refNo1"].ToString();
                refNo2 =  rec["refNo2"].ToString();
                refNo3 =  rec["refNo3"].ToString();
                advFeeYearId = string.IsNullOrEmpty(rec["advFeeYearId"].ToString()) ? (int?)null : Convert.ToInt32(rec["advFeeYearId"]);

               // preReqSendDt = rec["T01Send_Date"].ToString();
            }
        }
    }
}
