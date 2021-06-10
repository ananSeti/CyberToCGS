using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyberToCGS.SaveFormClaim;
using CyberToCGS.Database;
using System.Globalization;
using System.Data.Odbc;

namespace CyberToCGS
{
  public  class FacadeSaveFormClaim
    {
        protected SaveFormClaimRoot saveFormClaim = new SaveFormClaimRoot();
       SqlDataReader rec;
        OdbcDataReader odbcRec;
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

            // db = Database.Database.GetInstance("DB_CLAIM_ONLINE");
            //  rec = db.GetTw01_Claim_Online(lgNo); //62036859

            //  db = Database.Database.GetInstance("SIT1");
            //db = Database.Database.GetInstance("PROD");
            db = Database.Database.GetInstance("PROD");
            odbcRec = db.GetLGInfo(lgNo);

        }
    public SaveFormClaimRoot Operation()
        {
            // List<ClaimCollateral> claimCollaterals = new List<ClaimCollateral>();
            char pad = '0';
            Utils utils = new Utils();

            
            while (odbcRec.Read())
            {
                //TODO Get BANK -ID FROM api 
               // db = Database.Database.GetInstance("DB_CGSAPI_MASTER");

                //this.bankId = "11";//db.GetBankId(rec["T01Bank_Code"].ToString().PadLeft(3, pad));// rec["T01Bank_Code"].ToString().PadLeft(3, pad); //"4"; //rec["T01Bank_Code"].ToString();
                saveFormClaim.bankId = Convert.ToInt32(odbcRec["BANK_ID"].ToString()); //11;
                saveFormClaim.lgId = Convert.ToInt32(odbcRec["LG_ID"].ToString()) ;//222910; // getLG_ID LG for Convert.ToInt32(rec["T01LG_No"]);
                saveFormClaim.requestClaim =null ;//string.IsNullOrEmpty(rec["T01Claim_Amount"].ToString()) ? (int?)null : Convert.ToInt32(rec["T01Claim_Amount"]);//10000;
                saveFormClaim.payConditionType = odbcRec["pay_condition_type"].ToString() ;// "B";
                saveFormClaim.productId = Convert.ToInt32(odbcRec["PRODUCT_ID"]);
                saveFormClaim.portNo = Convert.ToInt32(odbcRec["port_no"]);
                saveFormClaim.refuseFlag = "N";
                saveFormClaim.claimPgsModelId = Convert.ToInt32(odbcRec["claim_pgs_model_id"]);
                saveFormClaim.maxClaimModelId =Convert.ToInt32(odbcRec["max_claim_model_id"]);
                saveFormClaim.productGroupId = Convert.ToInt32(odbcRec["product_group_id"]);

                //get AVG_YEAR
                db = Database.Database.GetInstance("PROD");
                saveFormClaim.maxClaimBal= db.GetmaxClaimBal(saveFormClaim.productId, saveFormClaim.bankId, saveFormClaim.productGroupId);
                saveFormClaim.avgYear = db.GetavgYear(saveFormClaim.productId, saveFormClaim.portNo, saveFormClaim.bankId);
                saveFormClaim.maxClaim = db.GetMaxClaim(saveFormClaim.productId, saveFormClaim.bankId, saveFormClaim.productGroupId);
                saveFormClaim.adjustClaimAmtAccum = db.GetAdjustClaimAmtAccum(saveFormClaim.productId,saveFormClaim.bankId,saveFormClaim.productGroupId);
                saveFormClaim.filingdtobgAmountAccumul = db.GetfilingdtobgAmountAccumul(saveFormClaim.productId, saveFormClaim.bankId, saveFormClaim.productGroupId);
                saveFormClaim.claimAmtAccum = db.GetClaimAmtAccum(saveFormClaim.productId, saveFormClaim.bankId, saveFormClaim.productGroupId);
                saveFormClaim.previousNpgAccumul = db.GetPreviousNpgAccumul(saveFormClaim.productId,saveFormClaim.bankId,saveFormClaim.productGroupId);
                ClaimCollateral cr = new ClaimCollateral();
                cr.collateralId = 1; //  TODO add collateralId
                cr.sueStatus = "Y";  // TODO add sue status
                cr.remark = "Remark"; //TODO add remark status
                saveFormClaim.claimCollaterals.Add(cr);

                ClaimLoan cl = new ClaimLoan();
                cl.loanId = null;  //TODO loanId
                cl.detailType = "1";
                cl.sueDtAct = null;// utils.ConvertYear(rec["T01Accuse_Date"].ToString());  //TOTO suedate //วันฟ้อง
                cl.courtName = "";//"ศาลแพ่ง กทม.ใต้"; //TODO
                cl.undecideCaseNo = null;//"1/2563"; //TODO เลขที่คดีดำ
                cl.judgmentDt = null;//DateTime.Now; //TODO judementDt วันที่พิพากษา
                cl.escortDt = null;// DateTime.Now;//TODO escortDT //วันที่พิทักษ์ทรัพย์
                cl.finalCaseDt = null;//DateTime.Now;// TODO finalCaseDt //วันที่คดีถึงที่สุด
                cl.auctionDt = null;//DateTime.Now; //TODO auctionDt  //วันที่ขายทอดตลาด
                cl.filingDtObgAmount = 1217477.377; //TODO filingDtObgAmount ภาระหนี้ ณ วันฟ้อง
                cl.requestDtObgAmount = 0; //TODO requestDtObgAmount ภาระหนี้ ณ วันที่ยื่นคำขอรับเงินค่าประกันชดเชย
                cl.defaultDt =  DateTime.ParseExact( "2020-11-20","yyyy-mm-dd",CultureInfo.InvariantCulture );//DateTime.Now;//TODO defaultDt วันที่ผิดนัด / ชำระหนี้ครั้งสุดท้าย
                cl.loanPage = null;//1;//TODO loanPage


                saveFormClaim.claimLoans.Add(cl);

            }
            return saveFormClaim;
        }
    }
}
