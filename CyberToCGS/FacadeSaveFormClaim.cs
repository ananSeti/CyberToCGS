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
           // List<ClaimCollateral> claimCollaterals = new List<ClaimCollateral>();
           
           
            

            while(rec.Read())
            {
                saveFormClaim.lgId = Convert.ToInt32(rec["T01LG_No"]);
                saveFormClaim.requestClaim = 10000;

                ClaimCollateral cr = new ClaimCollateral();
                cr.collateralId = 1; //  TODO add collateralId
                cr.sueStatus = "Y";  // TODO add sue status
                cr.remark = "Remark"; //TODO add remark status
                saveFormClaim.claimCollaterals.Add(cr);

                ClaimLoan cl = new ClaimLoan();
                cl.loanId = 1;  //TODO loanId
                cl.detailType = "1";
                cl.sueDtAct = DateTime.Now; //TOTO suedate
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
