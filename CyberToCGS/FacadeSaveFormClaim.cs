using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberToCGS.SaveFormClaim;

namespace CyberToCGS
{
  public  class FacadeSaveFormClaim
    {
        protected SaveFormClaimRoot saveFormClaim = new SaveFormClaimRoot();
        SqlDataReader rec;
     public FacadeSaveFormClaim()
        {
            // loadJson l = new loadJson();

            // l.ReadAppConfig();
            // fromDate = l.GetFormatFromdate();
            // toDate = l.GetFormatTodate();
            Database.Database db = Database.Database.GetInstance("DB_CLAIM_ONLINE");
            rec = db.GetTw01_Claim_Online("62036859");
        }
    public SaveFormClaimRoot Operation()
        {
            while(rec.Read())
            {
                saveFormClaim.lgId = Convert.ToInt32(rec["T01LG_No"]);
            }
            return saveFormClaim;
        }
    }
}
