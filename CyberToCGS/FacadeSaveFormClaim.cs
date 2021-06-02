using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberToCGS.SaveFormClaim;
using CyberToCGS.Database;
namespace CyberToCGS
{
  public  class FacadeSaveFormClaim
    {
        protected SaveFormClaimRoot saveFormClaim = new SaveFormClaimRoot();
       SqlDataReader rec;
       public string lgNo;
       public string bankId;
       public Database.Database db;
     public FacadeSaveFormClaim(string lgNo)
        {
            // loadJson l = new loadJson();

            // l.ReadAppConfig();
            // fromDate = l.GetFormatFromdate();
            // toDate = l.GetFormatTodate();
            this.lgNo = lgNo;

             db = Database.Database.GetInstance("DB_CLAIM_ONLINE");
           
            rec = db.GetTw01_Claim_Online(lgNo); //62036859

        }
    public SaveFormClaimRoot Operation()
        {
            // List<ClaimCollateral> claimCollaterals = new List<ClaimCollateral>();
            char pad = '0';
            Utils utils = new Utils();

            
            while (rec.Read())
            {
                //TODO Get BANK -ID FROM api 
                db = Database.Database.GetInstance("DB_CGSAPI_MASTER");
                
                this.bankId = db.GetBankId(rec["T01Bank_Code"].ToString().PadLeft(3, pad));// rec["T01Bank_Code"].ToString().PadLeft(3, pad); //"4"; //rec["T01Bank_Code"].ToString();
                saveFormClaim.lgId = 1;// getLG_ID LG for Convert.ToInt32(rec["T01LG_No"]);
                saveFormClaim.requestClaim = string.IsNullOrEmpty(rec["T01Claim_Amount"].ToString()) ? (int?)null : Convert.ToInt32(rec["T01Claim_Amount"]);//10000;

                ClaimCollateral cr = new ClaimCollateral();
                cr.collateralId = 1; //  TODO add collateralId
                cr.sueStatus = "Y";  // TODO add sue status
                cr.remark = "Remark"; //TODO add remark status
                saveFormClaim.claimCollaterals.Add(cr);

                ClaimLoan cl = new ClaimLoan();
                cl.loanId = 1;  //TODO loanId
                cl.detailType = "1";
                cl.sueDtAct =  utils.ConvertYear(rec["T01Accuse_Date"].ToString());  //TOTO suedate //วันฟ้อง
                cl.courtName = "ศาลแพ่ง กทม.ใต้"; //TODO
                cl.undecideCaseNo = "1/2563"; //TODO
                cl.judgmentDt = DateTime.Now; //TODO judementDt
                cl.escortDt = DateTime.Now;//TODO escortDT
                cl.finalCaseDt = DateTime.Now;// TODO finalCaseDt
                cl.auctionDt = DateTime.Now; //TODO auctionDt
                cl.filingDtObgAmount = 100000; //TODO filingDtObgAmount
                cl.requestDtObgAmount = 100000; //TODO requestDtObgAmount
                cl.defaultDt = DateTime.Now;//TODO defaultDt
                cl.loanPage = 1;//TODO loanPage
                saveFormClaim.claimLoans.Add(cl);

            }
            return saveFormClaim;
        }
    }
}
