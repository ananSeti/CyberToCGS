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
        SqlDataReader recMapp;
        
        OdbcDataReader odbcRec;
       public string lgNo;
       public string bankId;
       public Database.Database db;
       public Database.Database db2;
       public Database.Database dbCI;
       public Database.Database dbInterface;
       public string dbInstance;
       public string dbClaimOnline;
       public string dbLocal;
     public FacadeSaveFormClaim(string lgNo,string DbInstance,string DbCliamOnline,string dbLocal)
        {
            // loadJson l = new loadJson();

            // l.ReadAppConfig();
            // fromDate = l.GetFormatFromdate();
            // toDate = l.GetFormatTodate();
            this.lgNo = lgNo;
            this.dbInstance = DbInstance;
            this.dbClaimOnline = DbCliamOnline;
            this.dbLocal = dbLocal;

            // db = Database.Database.GetInstance("DB_CLAIM_ONLINE");
            //  rec = db.GetTw01_Claim_Online(lgNo); //62036859

            //  db = Database.Database.GetInstance("SIT1");
            //db = Database.Database.GetInstance("PROD");
            db = Database.Database.GetInstance(dbInstance); ///PROD
            odbcRec = db.GetLGInfo(lgNo);

            db2 = Database.Database.GetInstance(dbClaimOnline);
            rec = db2.GetTw01_Claim_Online(lgNo);

        }
    public SaveFormClaimRoot Operation()
        {
            // List<ClaimCollateral> claimCollaterals = new List<ClaimCollateral>();
            char pad = '0';
            Utils utils = new Utils();

            
           // while (odbcRec.Read())
            if(odbcRec.HasRows)
            {
                //TODO Get BANK -ID FROM api 
                // db = Database.Database.GetInstance("DB_CGSAPI_MASTER");
                odbcRec.Read();
                
                db = Database.Database.GetInstance(dbInstance); //PROD
                //this.bankId = "11";//db.GetBankId(rec["T01Bank_Code"].ToString().PadLeft(3, pad));// rec["T01Bank_Code"].ToString().PadLeft(3, pad); //"4"; //rec["T01Bank_Code"].ToString();
                saveFormClaim.bankId = Convert.ToInt32(odbcRec["BANK_ID"].ToString()); //11;
                saveFormClaim.lgId = Convert.ToInt32(odbcRec["LG_ID"].ToString()) ;//222910; // getLG_ID LG for Convert.ToInt32(rec["T01LG_No"]);
                if (this.rec.HasRows)
                {
                    this.rec.Read();
                    saveFormClaim.requestClaim = string.IsNullOrEmpty(this.rec["T01Claim_Amount"].ToString()) ? (int?)null : Convert.ToInt32(this.rec["T01Claim_Amount"]);//10000;
                    // เพิม Create T01Create_date
                    string d = this.rec["T01Create_Date"].ToString();
                    string t = this.rec["T01Create_Time"].ToString();
                    //เพิ่ม Send Date Time
                    //T01Send_Date
                    //T01Send_Time
                    string dS = this.rec["T01Send_Date"].ToString();
                    string tS = this.rec["T01Send_Time"].ToString();

                    saveFormClaim.guaPostCreateDt = utils.GetT01DateTime(d,t);  // create_Date + Create time
                    saveFormClaim.guaPostSendDt = utils.GetT01DateTime(dS,tS); // send date
                    dbCI = Database.Database.GetInstance("DB_ONLINE_CI");
                    string userCode = dbCI.GetLGOnwer(this.rec["T01Claim_ID"].ToString());
                    
                    dbInterface = Database.Database.GetInstance("DB_CGS_INTERFACE");
                    string userAssign = dbInterface.GetAssignUser(userCode);
                    saveFormClaim.assignName = userAssign;  //  เช่น 'Jirattha'
                     
                    //จนท ผู้ดูแลผู้ให้ขอ้มูล/ผู้ดูแล
                    saveFormClaim.guaCareBy = string.IsNullOrEmpty(this.rec["T01Contract_Name"].ToString()) ? "" : this.rec["T01Contract_Name"].ToString();
                    saveFormClaim.guaCarePostion = string.IsNullOrEmpty(this.rec["T01Contract_Position"].ToString()) ? "" : this.rec["T01Contract_Position"].ToString();
                    saveFormClaim.guaCarePhone = string.IsNullOrEmpty(this.rec["T01Contract_Telephone"].ToString()) ? "" : this.rec["T01Contract_Telephone"].ToString();
                    saveFormClaim.guaCareMobile = string.IsNullOrEmpty(this.rec["T01Contract_Mobile"].ToString()) ? "" : this.rec["T01Contract_Mobile"].ToString();
                    saveFormClaim.guaCareFaxNo = string.IsNullOrEmpty(this.rec["T01Contract_Fax"].ToString()) ? "" : this.rec["T01Contract_Fax"].ToString();
                    saveFormClaim.guaCareEmail = string.IsNullOrEmpty(this.rec["T01Contract_Email"].ToString()) ? "" : this.rec["T01Contract_Email"].ToString();
                    saveFormClaim.authorizedBy = ""; //TODO
                    saveFormClaim.authorizedPosition = ""; //TODO
                    saveFormClaim.loanContact = "0";

                    //PDF
                    string folderPDF = this.rec["T01Claim_ID"].ToString().Substring(1,2);
                    ///เอกสารเพิ่มเติมประกอบการพิจารณา
                     string aa = this.rec["T01File_1"].ToString();
                    if (! string.IsNullOrEmpty( this.rec["T01File_1"].ToString())) {
                        byte[] t1;
                        

                        PDFLoad.PdfLoader p = new PDFLoad.PdfLoader(folderPDF, this.rec["T01Claim_ID"].ToString(), this.rec["T01File_1"].ToString());
                        //string filename = this.rec["T01File_1"].ToString();
                        //string ext = filename.Substring(filename.IndexOf(".")).Replace(".","");
                        t1 =   p.getPdf();
                        ApplicationDocument a = new ApplicationDocument();
                        string base64String = Convert.ToBase64String(t1);
                        File f = new File();
                        f.fileName = p.getFileName(this.rec["T01File_1"].ToString());
                        f.fileType = p.getExtension(this.rec["T01File_1"].ToString());
                        f.base64 = base64String;

                        a.documentTypeInfId = 1112; //สำเนาคำฟ้องและเอกสารแนบท้ายฟ้องที่เจ้าหน้าที่ศาลรับรองความถูกต้อง
                        a.file.Add(f);
                        saveFormClaim.applicationDocuments.Add(a);
                      
                    }
                    if (!string.IsNullOrEmpty(this.rec["T01File_2"].ToString()))
                    {
                        byte[] t1;
                        PDFLoad.PdfLoader p = new PDFLoad.PdfLoader(folderPDF, this.rec["T01Claim_ID"].ToString(), this.rec["T01File_2"].ToString());
                        t1 = p.getPdf();
                        ApplicationDocument a = new ApplicationDocument();
                        string base64String = Convert.ToBase64String(t1);
                        File f = new File();
                        f.fileName = p.getFileName(this.rec["T01File_2"].ToString());
                        f.fileType = p.getExtension(this.rec["T01File_2"].ToString());
                        f.base64 = base64String;

                        a.documentTypeInfId = 1113; //สำเนาคำพิพากษาที่เจ้าหน้าที่ศาลรับรองความถูกต้อง
                        a.file.Add(f);
                        saveFormClaim.applicationDocuments.Add(a);


                    }
                    if (!string.IsNullOrEmpty(this.rec["T01File_3"].ToString()))
                    {
                        byte[] t1;
                        PDFLoad.PdfLoader p = new PDFLoad.PdfLoader(folderPDF, this.rec["T01Claim_ID"].ToString(), this.rec["T01File_3"].ToString());
                        t1 = p.getPdf();
                        ApplicationDocument a = new ApplicationDocument();
                        string base64String = Convert.ToBase64String(t1);
                        File f = new File();
                        f.fileName = p.getFileName(this.rec["T01File_3"].ToString());
                        f.fileType = p.getExtension(this.rec["T01File_3"].ToString());
                        f.base64 = base64String;

                        a.documentTypeInfId = 1132; //หนังสือบอกกล่าวทวงถาม
                        a.file.Add(f);
                        saveFormClaim.applicationDocuments.Add(a);
                    }
                    if (!string.IsNullOrEmpty(this.rec["T01File_4"].ToString()))
                    {
                        byte[] t1;
                        PDFLoad.PdfLoader p = new PDFLoad.PdfLoader(folderPDF, this.rec["T01Claim_ID"].ToString(), this.rec["T01File_4"].ToString());
                        t1 = p.getPdf();
                        ApplicationDocument a = new ApplicationDocument();
                        string base64String = Convert.ToBase64String(t1);
                        File f = new File();
                        f.fileName = p.getFileName(this.rec["T01File_4"].ToString());
                        f.fileType = p.getExtension(this.rec["T01File_4"].ToString());
                        f.base64 = base64String;

                        a.documentTypeInfId = 1133; //ใบตอบรับจากไปรษณีย์
                        a.file.Add(f);
                        saveFormClaim.applicationDocuments.Add(a);
                    }
                    if (!string.IsNullOrEmpty(this.rec["T01File_5"].ToString()))
                    {
                        byte[] t1;
                        PDFLoad.PdfLoader p = new PDFLoad.PdfLoader(folderPDF, this.rec["T01Claim_ID"].ToString(), this.rec["T01File_5"].ToString());
                        t1 = p.getPdf();
                        ApplicationDocument a = new ApplicationDocument();
                        string base64String = Convert.ToBase64String(t1);
                        File f = new File();
                        f.fileName = p.getFileName(this.rec["T01File_5"].ToString());
                        f.fileType = p.getExtension(this.rec["T01File_5"].ToString());
                        f.base64 = base64String;

                        a.documentTypeInfId = 1125; //สำเนาสัญญาสินเชื่อ
                        a.file.Add(f);
                        saveFormClaim.applicationDocuments.Add(a);

                    }
                    if (!string.IsNullOrEmpty(this.rec["T01File_6"].ToString()))
                    {
                        byte[] t1;
                        PDFLoad.PdfLoader p = new PDFLoad.PdfLoader(folderPDF, this.rec["T01Claim_ID"].ToString(), this.rec["T01File_6"].ToString());
                        t1 = p.getPdf();
                        ApplicationDocument a = new ApplicationDocument();
                        string base64String = Convert.ToBase64String(t1);
                        File f = new File();
                        f.fileName = p.getFileName(this.rec["T01File_6"].ToString());
                        f.fileType = p.getExtension(this.rec["T01File_6"].ToString());
                        f.base64 = base64String;

                        a.documentTypeInfId = 1116; //Bank Statement หรือหลักฐานการชำระหนี้ ตั้งแต่วันที่ทำสัญญาจนถึงจนถึงวันยื่นคำขอรับเงินค่าประกันชดเชย
                        a.file.Add(f);
                        saveFormClaim.applicationDocuments.Add(a);

                    }
                    if (!string.IsNullOrEmpty(this.rec["T01File_Merge"].ToString()))
                    {
                        byte[] t1;
                        PDFLoad.PdfLoader p = new PDFLoad.PdfLoader(folderPDF, this.rec["T01Claim_ID"].ToString(), this.rec["T01File_Merge"].ToString());
                        t1 = p.getPdf();
                        ApplicationDocument a = new ApplicationDocument();
                        string base64String = Convert.ToBase64String(t1);
                        File f = new File();
                        f.fileName = p.getFileName(this.rec["T01File_Merge"].ToString());
                        f.fileType = p.getExtension(this.rec["T01File_Merge"].ToString());
                        f.base64 = base64String;

                        a.documentTypeInfId = 53; //เอกสารเพิ่มเติมประกอบการพิจารณา
                        a.file.Add(f);
                        saveFormClaim.applicationDocuments.Add(a);

                    }

                    //consider
                    PostConsider postConsider;
                    db2 = Database.Database.GetInstance(dbLocal);
                    // string a = this.rec["T01Management_1"].ToString();
                    if (this.rec["T01Management_1"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Management_1");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Management_2"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Management_2");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Management_3"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Management_3");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Management_4"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Management_4");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Management_5"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Management_5");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    //T01Finance_1
                    if (this.rec["T01Finance_1"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Finance_1");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Finance_2"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Finance_2");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Finance_3"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Finance_3");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Finance_4"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Finance_4");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Finance_5"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Finance_5");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    //T01Market_1
                    if (this.rec["T01Market_1"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Market_1");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Market_2"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Market_2");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Market_3"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Market_3");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Market_4"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Market_4");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                    if (this.rec["T01Market_5"].ToString() == "True")
                    {
                        //get Mapping Data
                        recMapp = db2.GetCGSMapping("T01Market_5");
                        if (recMapp.HasRows)
                        {
                            postConsider = new PostConsider();
                            recMapp.Read();
                            postConsider.considerId = Convert.ToInt32(recMapp["Consider_Id"]);
                            postConsider.considerInfId = Convert.ToInt32(recMapp["Consider_Inf_Id"]);
                            postConsider.remark = null;
                            saveFormClaim.postConsider.Add(postConsider);
                        }

                    }
                }
                else {
                   // จนท.ผู้ดูแลระบบ
                    saveFormClaim.loanContact = "0";
                }


                saveFormClaim.productGroupId = Convert.ToInt32(odbcRec["product_group_id"]);
                saveFormClaim.productId = Convert.ToInt32(odbcRec["p"]);
                saveFormClaim.portNo = Convert.ToInt32(odbcRec["port_no"]);
                saveFormClaim.refuseFlag = "N";
                ///Check if PGS or Package
                ///
                db = Database.Database.GetInstance(dbInstance);
                if ( db.GetProductGroup(saveFormClaim.productGroupId) ==1 )
                {
                    /// PGS
                    saveFormClaim.payConditionType = string.IsNullOrEmpty(odbcRec["pay_condition_type"].ToString()) ? null : odbcRec["pay_condition_type"].ToString();// "B";
                    saveFormClaim.claimPgsModelId = string.IsNullOrEmpty(odbcRec["claim_pgs_model_id"].ToString()) ? (int?)null : Convert.ToInt32(odbcRec["claim_pgs_model_id"]);
                    saveFormClaim.maxClaimModelId = string.IsNullOrEmpty(odbcRec["max_claim_model_id"].ToString()) ? (int?)null : Convert.ToInt32(odbcRec["max_claim_model_id"]);
                   
                }
                if (db.GetProductGroup(saveFormClaim.productGroupId) == 3)
                { /// Package
                   
                   OdbcDataReader r= db.GetLginfoPackage(lgNo);
                    if (r.HasRows)
                    {
                        r.Read();
                        saveFormClaim.payConditionType = string.IsNullOrEmpty(r["PAY_CONDITION_TYPE"].ToString()) ?null : r["PAY_CONDITION_TYPE"].ToString();
                        saveFormClaim.claimPgsModelId = null; //r[""].ToString()
                        saveFormClaim.maxClaimModelId = null;
                    }
                }
                //get AVG_YEAR

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
                cr.remark = "Remark"; //TODO add remark statuszz
                saveFormClaim.claimCollaterals.Add(cr);

                //Get Court Date Time Info from CGS
                db = Database.Database.GetInstance(dbInstance); //PROD
                OdbcDataReader rec= db.GetCourtDateInfo(saveFormClaim.lgId);
                if (rec.HasRows)
                {
                    while (rec.Read())
                    {
                        ClaimLoan cl = new ClaimLoan();
                        cl.loanId = Convert.ToInt32(rec["LG_LOAN_ID"]);  //TODO loanId
                        cl.detailType = "1";
                        cl.sueDtAct = utils.ConvertYear(this.rec["T01Accuse_Date"].ToString()); // utils.ConvertYear(rec["SUE_DATE"].ToString()); ;// utils.ConvertYear(rec["T01Accuse_Date"].ToString());  //TOTO suedate //วันฟ้อง
                        cl.courtName = "";//"ศาลแพ่ง กทม.ใต้"; //TODO
                        cl.undecideCaseNo = string.IsNullOrEmpty(rec["UNDECIDE_CASE_NO"].ToString()) ? null : rec["UNDECIDE_CASE_NO"].ToString();//"1/2563"; //TODO เลขที่คดีดำ
                        cl.judgmentDt = utils.ConvertYear(rec["JUDGMENT_DT"].ToString()); // null;//DateTime.Now; //TODO judementDt วันที่พิพากษา
                        cl.escortDt = utils.ConvertYear(rec["ESCORT_DT"].ToString()); //null;// DateTime.Now;//TODO escortDT //วันที่พิทักษ์ทรัพย์
                        cl.finalCaseDt = utils.ConvertYear(rec["FINAL_CASE_DT"].ToString()); //null;//DateTime.Now;// TODO finalCaseDt //วันที่คดีถึงที่สุด
                        cl.auctionDt = utils.ConvertYear(rec["AUCTION_SALE_DT"].ToString()); //null;//DateTime.Now; //TODO auctionDt  //วันที่ขายทอดตลาด
                        cl.filingDtObgAmount = null;//1217477.377; //TODO filingDtObgAmount ภาระหนี้ ณ วันฟ้อง
                        cl.requestDtObgAmount = 0; //TODO requestDtObgAmount ภาระหนี้ ณ วันที่ยื่นคำขอรับเงินค่าประกันชดเชย
                        cl.defaultDt = utils.ConvertYear(this.rec["T01Default_Date"].ToString()); ;// DateTime.ParseExact("2020-11-20", "yyyy-mm-dd", CultureInfo.InvariantCulture);//DateTime.Now;//TODO defaultDt วันที่ผิดนัด / ชำระหนี้ครั้งสุดท้าย
                        cl.loanPage = null;//1;//TODO loanPage
                        cl.loanObgAmount = db.GetLoanObgAmount(saveFormClaim.lgId);  // null;//TODO 

                        saveFormClaim.claimLoans.Add(cl);
                    }

                }
                /**
                // Get Court Data Time Info from Claim
                //if (this.rec.HasRows)
                //{
                //    ClaimLoan cl = new ClaimLoan();
                //   cl.loanId = Convert.ToInt32(rec["LG_LOAN_ID"]);  //TODO loanId
                //   cl.detailType = "1";
                //   cl.sueDtAct = utils.ConvertYear(this.rec["T01Accuse_Date"].ToString());  //TOTO suedate //วันฟ้อง
                //   cl.courtName = "";//"ศาลแพ่ง กทม.ใต้"; //TODO
                //   cl.undecideCaseNo = string.IsNullOrEmpty(rec["UNDECIDE_CASE_NO"].ToString()) ? null : rec["UNDECIDE_CASE_NO"].ToString();//"1/2563"; //TODO เลขที่คดีดำ
                //   cl.judgmentDt = utils.ConvertYear(rec["JUDGMENT_DT"].ToString()); // null;//DateTime.Now; //TODO judementDt วันที่พิพากษา
                //   cl.escortDt = utils.ConvertYear(rec["ESCORT_DT"].ToString()); //null;// DateTime.Now;//TODO escortDT //วันที่พิทักษ์ทรัพย์
                //   cl.finalCaseDt = utils.ConvertYear(rec["FINAL_CASE_DT"].ToString()); //null;//DateTime.Now;// TODO finalCaseDt //วันที่คดีถึงที่สุด
                //   cl.auctionDt = utils.ConvertYear(rec["AUCTION_SALE_DT"].ToString()); ;//null;//DateTime.Now; //TODO auctionDt  //วันที่ขายทอดตลาด
                //   cl.filingDtObgAmount = null;//1217477.377; //TODO filingDtObgAmount ภาระหนี้ ณ วันฟ้อง
                //   cl.requestDtObgAmount = 0; //TODO requestDtObgAmount ภาระหนี้ ณ วันที่ยื่นคำขอรับเงินค่าประกันชดเชย
                //   cl.defaultDt = utils.ConvertYear(this.rec["T01Default_Date"].ToString());// DateTime.ParseExact("2020-11-20", "yyyy-mm-dd", CultureInfo.InvariantCulture);//DateTime.Now;//TODO defaultDt วันที่ผิดนัด / ชำระหนี้ครั้งสุดท้าย
                //   cl.loanPage = null;//1;//TODO loanPage
                //   cl.loanObgAmount = db.GetLoanObgAmount(saveFormClaim.lgId);  // null;//TODO 

                //    saveFormClaim.claimLoans.Add(cl);
                //}
                **/

            }
            return saveFormClaim;
        }
    }
}
