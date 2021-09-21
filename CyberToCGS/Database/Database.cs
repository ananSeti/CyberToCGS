using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace CyberToCGS.Database
{
   public class Database
    {
        string db_online = @"server = 192.168.0.83; database = DB_ONLINE_CG; user = sa; password = ABC123abc$; ";
        string db_apiMaster = @"server = 192.168.0.83; database = DB_CGSAPI_MASTER; user = sa; password = ABC123abc$; ";
        string db_cgs_interface = @"server = 192.168.0.83; database = DB_CGS_INTERFACE; user = sa; password = ABC123abc$; ";
        string db_claim_online = @"server = 192.168.0.83; database = DB_CLAIM_ONLINE; user = sa; password = ABC123abc$; ";
        string db_claim_online_Prod = @"server = 192.168.10.17; database = DB_CLAIM_ONLINE; user = sa; password = sicgcadmin; ";
        string db_claim_online_CI = @"server = 192.168.0.17; database = DB_CI; user = sa; password = sicgcadmin; ";
        string localDb = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=testDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string SIT1 = @"DSN=SIT1;uid=CGS;Pwd=tibero";
        string PROD = @"DSN=PROD;uid=cgs;Pwd=tcg2019;";
        //string PROD = @"Driver={Tibero 6 ODBC Driver};server=192.168.12.13;port=8629;uid=cgs;pwd=tibero2019;DB=tac;TYPE=ODBC";
        string connectionString = null;

        SqlConnection connection;
        OdbcCommand odbcCommand;
        OdbcConnection odbcConnection;
        SqlCommand command;
        SqlDataReader dataReader;
        OdbcDataReader odbcDataReader;

       

        String Sql;
        public Database(string value) {
            if (value == "localDB")
            {
                connectionString = localDb;
            }
            if(value == "DB_ONLINE_CG")
            {
                connectionString =  db_online;
            }
            if (value == "DB_CGSAPI_MASTER")
            {
                connectionString = db_apiMaster;
            }
            if(value  == "DB_CLAIM_ONLINE")
            {
                connectionString = db_claim_online;
            }
            if(value == "SIT1")
            {
                connectionString = SIT1;
            }
            if(value == "PROD")
            {
                connectionString = PROD;
            }
            if(value == "DB_ONLINE_CG_PROD")
            {
                connectionString = db_claim_online_Prod;
            }
            if (value == "DB_ONLINE_CI")
            {
                connectionString = db_claim_online_CI;
            }
            if (value == "DB_CGS_INTERFACE")
            {
                connectionString = db_cgs_interface;
            }
        }


        private static Database _database;
        private static readonly object _lock = new object();
        public string Value { get; set; }
        public static Database GetInstance(string value)
        {
            if (_database == null)
            {
                lock (_lock)
                {
                    if (_database == null)
                    {
                        _database = new Database(value);
                        _database.Value = value;

                    }
                }

            }
            else
            {
                _database.Value = value;
                if (value == "localDB")
                {
                    _database.connectionString = _database.localDb;
                }
                if (value == "DB_ONLINE_CG")
                {
                   _database.connectionString = _database.db_online;
                }
                if (value == "DB_CGSAPI_MASTER")
                {
                   _database.connectionString =_database. db_apiMaster;
                }
                if (value == "DB_CLAIM_ONLINE")
                {
                    _database.connectionString = _database.db_claim_online;
                }
                if (value == "SIT1")
                {
                    _database.connectionString = _database.SIT1;
                }
                if (value == "PROD")
                {
                    _database.connectionString = _database.PROD;
                }
                if (value == "DB_ONLINE_CG_PROD")
                {
                    _database.connectionString = _database.db_claim_online_Prod;
                }
                if (value == "DB_ONLINE_CI")
                {
                    _database.connectionString = _database.db_claim_online_CI;
                }
                if (value == "DB_CGS_INTERFACE")
                {
                    _database.connectionString = _database.db_cgs_interface;
                }
            }
            
            return _database;
        }

        public SqlDataReader GetCGSMapping(string ClaimOnlineColumn)
        {
            Sql = " select * from [dbo].[MappingConsider_inf_id] "
                + " where ClaimOnlineColumn =@ClName ";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                command.Parameters.AddWithValue("@ClName", ClaimOnlineColumn);
                dataReader = command.ExecuteReader();
               // connection.Close();
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get CGS Mapping data Error  ");
            }
            return dataReader;
        }

        public bool LogData(logData log)
        {
            Sql = "insert into [dbo].[claimLog](LgNo,logDate,Method,Status,JsonPost) values(@lgNo,@logDate,@method,@Status,@JsonPost)";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                //Parameter
                command.Parameters.AddWithValue("@LgNo", log.lgNo);
                command.Parameters.AddWithValue("@logDate", log.logDate);
                command.Parameters.AddWithValue("@method", log.method);
                command.Parameters.AddWithValue("@Status",log.status);
                command.Parameters.AddWithValue("@JsonPost",log.JsonPost);


                
               
                int result = command.ExecuteNonQuery();
                if (result < 0)
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("------- log Data : " + log.lgNo);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("insert log  error" + ex.Message.ToString());
                return false;
            }
        }

        public SqlDataReader GetT01_Request_online_lgNo()
        {
            Sql = " SELECT T01LG_No,T01Claim_ID  FROM DB_CLAIM_ONLINE.dbo.TW01_Claim_Online "
                  + " where t01last_status in ( '100') and T01Active ='1'  order by T01send_Date,T01Send_Time  ";   ///  010 ใหม่    100 Assign แล้ว
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                dataReader = command.ExecuteReader();
                //close new
                //ห้าม Close
                //connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Get DB_CLAIM_ONLINE.dbo.TW01_Claim_Online  ");
            }
            return dataReader;
        }
        public bool isExisting(string lgNO) {
           // connection.Close();
            Sql = " select  lgno From [DB_CGS_INTERFACE].[dbo].[claimLog] where lgNo = @lgno  group by LgNo ";   ///  010 ใหม่    100 Assign แล้ว
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                command.Parameters.AddWithValue("@lgno",lgNO);
               dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    //close new
                    connection.Close();
                    return true;

                }
                else {
                    //close new
                    connection.Close();
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("....Get  [DB_CGS_INTERFACE].[dbo].[claimLog] ");
                return false;
            }
           
        }

        public void GetUser()
        {
           
             Sql = "select id,name from [dbo].[user]";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql,connection);
                dataReader = command.ExecuteReader();
                Console.WriteLine("-------Get All User ----------- ");
                while (dataReader.Read())
                {
                   
                    Console.WriteLine(dataReader.GetValue(1));
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("getUser error" + ex.Message.ToString());
            }
        }
        public void GetUser(int id)
        {

             Sql = "SELECT id,name FROM  [dbo].[user] "
                         +" WHERE id = @id;";
            connection = new SqlConnection(connectionString);

            Console.WriteLine("---------Get User By Parameter :" + id.ToString() + "-------");
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@ID"].Value = id;

                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    
                    Console.WriteLine(dataReader.GetValue(1));
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("getUser error" + ex.Message.ToString());
            }
        }

        internal SqlDataReader GetViewCgsapiCustomer(string T01OnlineID)
        {
            Sql = "SELECT T01Online_ID, identification, identificationType, customerStatus, customerType, "
                +" T0X, T01Customer_Type, titleName, customerGrade, customerScore, raceId, raceStr, nationalityId, "
                + " nationalityStr, refReqNumber, customerId, borrowerType, titleId, cusNameTh, cusSurnameTh, "
                + " cusNameEn, cusSurnameEn, gender, marriedStatus, birthDate, educationLevel, career, telephoneNo, "
                + " mobilePhoneNo, faxNo, email, depLevelId, proveDate, businessExp, registerDate, registerCapital, "
                + " certificateDate, customerAlive, amountCol, kycResult, kycDate, guarantorRelationCode, "
                + " guarantorRelationStr, seq, s_identification, s_identificationType, s_titleId, s_cusNameTh, "
                + " s_cusSurnameTh, s_cusNameEn, s_cusSurnameEn, s_birthDate, s_telephoneNo, s_mobilePhoneNo,"
                + " s_faxNo, s_email, s_registerCapital, s_active, s_addressType, s_addressNo, s_addressMoo, "
                + " s_addressAlley, s_addressRoad, s_subDistrictId, s_districtId, s_provinceId, s_countryId, " 
                + " s_addressOversea "
                + " FROM DB_CGSAPI_MASTER.dbo.View_cgsapi_customer "
                + " Where T01Online_ID = @T01OnlineID "
                + " order by identification DESC ";

            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);

                command.Parameters.Add("@T01OnlineID", SqlDbType.NVarChar);
                command.Parameters["@T01OnlineID"].Value = T01OnlineID;


                dataReader = command.ExecuteReader();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Get  DB_CGSAPI_MASTER.dbo.View_cgsapi_customer error" + ex.Message.ToString());
            }
            return dataReader;
        }

        public void SetConnection(string value)
        {
            if(value == "DB_ONLINE_CG")
            {
                connectionString = db_online;
            }
        }
        public void GetT01_Request_Online()
        {
            Sql = "select T01Online_ID, T01Send_Date, T01Name_Thai, T01Surname_Thai, T01Last_Status "
                + " from[dbo].[T01_Request_Online] "
                + " where t01last_status >= '010' "
                + " and t01last_status <= '110' " ;
            /*
             *
             *and T01Send_Date >= '{0}'
             *and T01Send_Date <= '{1}
             *order by t01online_id desc", strFrom, strTo); 
             */
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                dataReader = command.ExecuteReader();
                Console.WriteLine("------- GetT01_Request_Online() ----------- ");
                while (dataReader.Read())
                {

                    Console.WriteLine(dataReader.GetValue(1) + "-"  + dataReader.GetValue(2) );
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("getUser error" + ex.Message.ToString());
            }
        }
    
        public SqlDataReader GetT01_Request_Online(string fromDate,string toDate)
        {
            Sql = "select T01Online_ID,T01Send_Date, T01Name_Thai, T01Surname_Thai, T01Last_Status , T01Request_No"
                + " from[dbo].[T01_Request_Online] "
                + " where t01last_status >= '010' "
                + " and t01last_status <= '110' "
                + " and T01Send_Date >= @fromDate"
                + " and T01Send_Date <= @toDate"
                + " order by t01online_id desc";
            /*
             *
             *and T01Send_Date >= '{0}'
             *and T01Send_Date <= '{1}
             *order by t01online_id desc", strFrom, strTo); 
             */
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                
                command.Parameters.Add("@fromDate",SqlDbType.NVarChar);
                command.Parameters["@fromDate"].Value = fromDate;
                command.Parameters.Add("@toDate", SqlDbType.NVarChar);
                command.Parameters["@toDate"].Value = toDate;

                dataReader = command.ExecuteReader();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get T01_Request_Online error" + ex.Message.ToString());
            }
           
            return dataReader;
        }
        public SqlDataReader GetT01_Request_Online(string request_no)
        {
            Sql = " select  T01Online_ID, T01Send_Date, T01Ref_1, T01Ref_2, T01Ref_3,T01Sub_Project_Type, T01Bank_Code, T01Branch_Code, "
              + "  T01Brn_Cnt_Person, T01Bank_Cnt_Telephone, T01Bank_Cnt_Mobile, T01Bank_Cnt_Email, T89Enterprise_No, T89Enterprise_Soi, "
              + " T89Enterprise_Road, T89Enterprise_Distinct_ID, T89Enterprise_Ampure_ID, T89Enterprise_Province, T89Enterprise_Zip_Code, "
              + " T01Title_Name_Thai, T01Name_Thai, T01Surname_Thai, T01Marital_Status, T01Customer_Type, T01Card_Type, T01Card_ID1, T01House_Num, "
              + " T01House_Soi, T01House_Road, T01House_Distinct_ID, T01House_Ampure_ID, T01House_Province, T01House_Zip_Code, T01House_Phone, T01House_Fax, "
              + " T01House_Mobile, T01House_Email, T01Title_Name_Thai2, T01Name_Thai2, T01Surname_Thai2, T01Card_ID2, T01House_Num2, T01House_Soi2, "
              + " T01House_Road2, T01House_Distinct2, T01House_Ampure2, T01House_Province2, T01House_Zip_Code2, T01House_Phone2, T01House_Fax2, "
              + " T01House_Mobile2, T01House_Email2, T02Title_Name_Thai, T02Name_Thai, T02Surname_Thai, T02Marital_Status, T02Card_Type, T02Card_ID1, "
              + " T02House_Num, T02House_Soi, T02House_Road, T02House_Distinct_ID, T02House_Ampure_ID, T02House_Province, T02House_Zip_Code, "
              + " T02House_Phone, T02House_Fax, T02House_Mobile, T02House_Email, T02Title_Name_Thai2, T02Name_Thai2, T02Surname_Thai2, T02Card_ID2, " 
              + " T02House_Num2, T02House_Soi2, T02House_Road2, T02House_Distinct2, T02House_Ampure2, T02House_Province2, T02House_Zip_Code2, "
              + " T02House_Phone2, T02House_Fax2, T02House_Mobile2, T02House_Email2, T03Title_Name_Thai, T03Name_Thai, T03Surname_Thai, T03Marital_Status, "
              + " T03Card_Type, T03Card_ID1, T03House_Num, T03House_Soi, T03House_Road, T03House_Distinct_ID, T03House_Ampure_ID, T03House_Province, " 
              + " T03House_Zip_Code, T03House_Phone, T03House_Fax, T03House_Mobile, T03House_Email, T03Title_Name_Thai2, T03Name_Thai2, "
              + " T03Surname_Thai2, T03Card_ID2, T03House_Num2, T03House_Soi2, T03House_Road2, T03House_Distinct2, T03House_Ampure2, T03House_Province2, "
              + " T03House_Zip_Code2, T03House_Phone2, T03House_Fax2, T03House_Mobile2, T03House_Email2, T04Title_Name_Thai, T04Name_Thai, T04Surname_Thai, "
              + " T04Marital_Status, T04Card_Type, T04Card_ID1, T04House_Num, T04House_Soi, T04House_Road, T04House_Distinct_ID, T04House_Ampure_ID, " 
              + " T04House_Province, T04House_Zip_Code, T04House_Phone, T04House_Fax, T04House_Mobile, T04House_Email, T04Title_Name_Thai2, T04Name_Thai2, "
              + " T04Surname_Thai2, T04Card_ID2, T04House_Num2, T04House_Soi2, T04House_Road2, T04House_Distinct2, T04House_Ampure2, T04House_Province2, " 
              + " T04House_Zip_Code2, T04House_Phone2, T04House_Fax2, T04House_Mobile2, T04House_Email2, T05Title_Name_Thai, T05Name_Thai, T05Surname_Thai,"
              + " T05Marital_Status, T05Card_Type, T05Card_ID1, T05House_Num, T05House_Soi, T05House_Road, T05House_Distinct_ID, T05House_Ampure_ID, " 
              + " T05House_Province, T05House_Zip_Code, T05House_Phone, T05House_Fax, T05House_Mobile, T05House_Email, T05Title_Name_Thai2, T05Name_Thai2, " 
              + " T05Surname_Thai2, T05Card_ID2, T05House_Num2, T05House_Soi2, T05House_Road2, T05House_Distinct2, T05House_Ampure2, T05House_Province2, "
              + " T05House_Zip_Code2, T05House_Phone2, T05House_Fax2, T05House_Mobile2, T05House_Email2, T01Industry_Name, T01Staff_Amount, T01Staff_Amount_Inc,"
              + " T01Asset_Money, T01Loan_Subject_1, T01Loan_Amount_1, T01Request_Amount_1, T01Loan_Type_1, T01Loan_Subject_2, T01Loan_Amount_2, T01Request_Amount_2,"
              + " T01Loan_Type_2, T01Loan_Subject_3, T01Loan_Amount_3, T01Request_Amount_3, T01Loan_Type_3, T01Loan_Subject_4, T01Loan_Amount_4, T01Request_Amount_4, "
              + " T01Loan_Type_4, T01Loan_Amount, T01Request_Amount, T01File_1, T01File_2, T01File_3, T01File_4, T01File_5, T01File_6, T01File_7, T01File_8, T01File_9, "
              + " T01Investment_Objective_1, T01Debt_Year_1, T01Debt_Define_1, T01ContractNo_1, T01Investment_Objective_2, T01Debt_Year_2, T01Debt_Define_2, T01ContractNo_2 ,"
              + " T01Investment_Objective_3, T01Debt_Year_3, T01Debt_Define_3, T01ContractNo_3, T01Investment_Objective_4, T01Debt_Year_4, T01Debt_Define_4, T01ContractNo_4,"
              + " T01ISIC_Code, T01Education, T02Education, T03Education, T04Education, T05Education, T01Not_Reduce, T01Census_Num, T01Census_Soi, T01Census_Road, "
              + " T01Census_Distinct, T01Census_Ampure, T01Census_Province, T01Census_Zip_Code, T02Census_Num, T02Census_Soi, T02Census_Road, T02Census_Distinct,"
              + " T02Census_Ampure, T02Census_Province, T02Census_Zip_Code, T03Census_Num, T03Census_Soi, T03Census_Road, T03Census_Distinct, T03Census_Ampure, " 
              + " T03Census_Province, T03Census_Zip_Code, T04Census_Num, T04Census_Soi, T04Census_Road, T04Census_Distinct, T04Census_Ampure, T04Census_Province," 
              + " T04Census_Zip_Code, T05Census_Num, T05Census_Soi, T05Census_Road, T05Census_Distinct, T05Census_Ampure, T05Census_Province, T05Census_Zip_Code, "
              + " T01Birth_Date, T02Birth_Date, T03Birth_Date, T04Birth_Date, T05Birth_Date, T01Asset_Money_Building, T01Asset_Money_Machine, T01Project_Character,"
              + " T01DSCR, T01Experience_Direct, T01Start_Date_Business, T01Request_No "
              + " from dbo.t01_request_online "
              + " where T01Request_No = @request_No;" ;
           
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);

                command.Parameters.Add("@request_No", SqlDbType.NVarChar);
                command.Parameters["@request_No"].Value = request_no;
                

                dataReader = command.ExecuteReader();
                               
                //close new
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get T01_Request_Online error" + ex.Message.ToString());
            }
            return dataReader;
        }
        public SqlDataReader GetViewCgsapiProduct(string T01OnlineID)
        {
            Sql = "  SELECT T01Online_ID, preReqStatus, ProductCode, roundId, guaAmount, prdPayFeeType, prdReduGuaType, "
                + " refNo1, refNo2, refNo3, advFeeYearId, productId "
                + " FROM DB_CGSAPI_MASTER.dbo.View_cgsapi_product"               
                + " where T01Online_ID = @T01OnlineID;";

                connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    command = new SqlCommand(Sql, connection);

                    command.Parameters.Add("@T01OnlineID", SqlDbType.NVarChar);
                    command.Parameters["@T01OnlineID"].Value = T01OnlineID;


                    dataReader = command.ExecuteReader();
                // //close new
                connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Get  DB_CGSAPI_MASTER.dbo.View_cgsapi_product error" + ex.Message.ToString());
                }
                return dataReader;
            }

        internal SqlDataReader GetViewCgsapiBank(string T01OnlineID)
        {
            Sql = " SELECT T01Online_ID, bankId, bankBrnUseLimit, bankBrnSendOper, guaCareName,"
                + " guaCareMobile, guaCarePhone, guaCareEmail, guaApproveEmail, guaRemark "
                + " FROM DB_CGSAPI_MASTER.dbo.View_cgsapi_bank  "
                + " Where T01Online_ID = @T01OnlineID ";

            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);

                command.Parameters.Add("@T01OnlineID", SqlDbType.NVarChar);
                command.Parameters["@T01OnlineID"].Value = T01OnlineID;


                dataReader = command.ExecuteReader();
                //close new
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Get  DB_CGSAPI_MASTER.dbo.View_cgsapi_bank error" + ex.Message.ToString());
            }
            return dataReader;
        }
        public SqlDataReader GetTw01_Claim_Online(string T01lg_no) {
            Sql = " SELECT T01Claim_ID, T01LG_No, T01LG_Name, T01LG_Date, T01End_Date_LG, T01Last_Status,"
                + " T01Bank_Code, T01Project_Type, T01Sub_Project_Type, T01Contract_Name, T01Contract_Position, "
                + " T01Contract_Province, T01Contract_Branch, T01Contract_Telephone, T01Contract_Fax, T01Contract_Mobile, "
                + " T01Contract_Email, T01Create_Date, T01Create_Time, T01Create_User, T01Total_Amount_Duty, T01Active,"
                + " T01Default_Date, T01Accuse_Date, T01Cancel_Date_1, T01Cancel_Date_2, T01Description, T01Update_Date, "
                + " T01Update_Time, T01Update_User, T01Send_Date, T01Send_Time, T01Send_User, T01Receive_Date, T01Receive_Time, "
                + " T01Receive_User, T01Success_Document, T01Success_Document_Time, T01File_1, T01File_2, T01File_3, T01File_4, "
                + " T01File_5, T01File_6, T01File_Merge, T01Year_Port, T01Message, T01Order_No, T01Branch_Code, T01Internal_Message, "
                + " T01Business_Running, T01Management_1, T01Management_2, T01Management_3, T01Management_4, T01Management_5, "
                + " T01Other_Management, T01Finance_1, T01Finance_2, T01Finance_3, T01Finance_4, T01Finance_5, T01Other_Finance, "
                + " T01Market_1, T01Market_2, T01Market_3, T01Market_4, T01Market_5, T01Other_Market, T01Capital_Asset, T01Judgement,"
                + " T01Collectral, T01Collectral_Desc, T01IsResend,T01Claim_Amount   "
                + " FROM DB_CLAIM_ONLINE.dbo.TW01_Claim_Online  where 1=1  and  T01Active ='1' "     //change status from 010 to 100 //2.
                + " and T01LG_No = @T01lg_no ; " ;


            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);

                command.Parameters.Add("@T01lg_no", SqlDbType.NVarChar);
                command.Parameters["@T01lg_no"].Value = T01lg_no;


                dataReader = command.ExecuteReader();
                // //close new
                //connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get  DB_CLAIM_ONLINE.dbo.TW01_Claim_Online  Error" + ex.Message.ToString());
            }
            return dataReader;
        }
        public OdbcDataReader GetLGInfo(string lgNo)
        {
            //dataReader.Close();
           // odbcConnection.Close();
           /*
            Sql = "SELECT a.lg_id,a.LG_NO , a.BANK_ID ,a.PRODUCT_ID AS p, b.product_id ,b.claim_pgs_model_id,b.max_claim_model_id, "
                + " c.product_group_id,c.product_name ,b.pay_condition_type  ,d.port_no "
                + " FROM tbl_rd_lg a "
                + " LEFT outer JOIN  TBL_AS_PRODUCT_CLAIM_PGS b ON a.PRODUCT_ID = b.product_id "
                + " LEFT outer JOIN TBL_MD_PRODUCT  c ON a.PRODUCT_ID = c.product_id "
                + " LEFT OUTER JOIN TBL_RD_PRODUCT_ROUND_INF d ON a.product_id = d.product_id "
                + " WHERE a.status='A' AND  a.lg_no = ? ;";//'" + lgNo +"'"; //'5910612';"; //= @lgNo ;" ;  //5910612
           */
            Sql = " SELECT a.lg_id,a.LG_NO,a.BANK_ID,a.PRODUCT_ID AS p,b.product_id,b.claim_pgs_model_id,b.max_claim_model_id,c.product_group_id, "
             + "  c.product_name, b.pay_condition_type,d.port_no FROM tbl_rd_lg a "
             + "   JOIN TBL_AS_PRODUCT_CLAIM_PGS b ON a.PRODUCT_ID = b.product_id "
             + "  LEFT outer JOIN TBL_MD_PRODUCT c ON a.PRODUCT_ID = c.product_id "
             + " LEFT OUTER JOIN TBL_RD_PRODUCT_ROUND_INF d ON a.product_id = d.product_id and a.PRODUCT_ROUND_INF_ID = d.PRODUCT_ROUND_INF_ID "
             + " WHERE a.status = 'A' "
             + " AND a.lg_no = ? ";

            // connection = new SqlConnection(connectionString);
            odbcConnection = new OdbcConnection(connectionString);
            try {
                odbcConnection.Open();
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                // odbcCommand.Parameters.Add("@lgNo",OdbcType.NVarChar);
                // odbcCommand.Parameters["@lgNo"].Value = lgNo;
                // odbcCommand.Parameters.AddWithValue("@lgno", lgNo);
                odbcCommand.Parameters.Add("@lg_no", OdbcType.NVarChar).Value =lgNo;
                odbcDataReader = odbcCommand.ExecuteReader();

                //odbcConnection.Close();
            }
            catch (Exception ex) {
                Console.WriteLine("Get SIT1 or Prod  PGS error: "+ ex.Message.ToString());
            }
            return odbcDataReader;
        }
        public OdbcDataReader GetLginfoPackage(string lgNo)
        {
            Sql = " SELECT a.lg_id,a.LG_NO , a.BANK_ID , a.product_id ,b.* "
                 +" FROM tbl_rd_lg a "
                 +" LEFT  JOIN TBL_AS_PRODUCT_CLAIM_PACKAGE b ON a.PRODUCT_ID = b.product_id "
                 +" WHERE b.PRODUCT_CLAIM_PACKAGE_ID is not NULL "
                 +" AND a.LG_NO = ?"
                + " ORDER BY PRODUCT_CLAIM_PACKAGE_ID DESC; "; //'60034524'


            // connection = new SqlConnection(connectionString);
            odbcConnection = new OdbcConnection(connectionString);
            try
            {
                odbcConnection.Open();
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcCommand.Parameters.Add("@lg_no", OdbcType.NVarChar).Value = lgNo;
                odbcDataReader = odbcCommand.ExecuteReader();
                //close new
               // odbcConnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Get SIT1 or Prod  PGS error: " + ex.Message.ToString());
            }
            return odbcDataReader;
        }
        public double GetmaxClaimBal(int productid,int bankid,int productGroupid)
        {
            double maxClaimBal=0;
            //ให้ใช้ตัวแรก 
            Sql = " SELECT MAX_CLAIM_BAL FROM TBL_RD_CLAIM_ACCUM "
                  + " WHERE PRODUCT_ID = ? "
                  + " AND BANK_ID = ?"
                  + " AND PRODUCT_GROUP_ID = ? "
                  + " ORDER BY ACCUM_ORDER DESC;";
            odbcConnection = new OdbcConnection(connectionString);
            try {
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcConnection.Open();
                odbcCommand.Parameters.AddWithValue("@prodcutid",productid);
                odbcCommand.Parameters.AddWithValue("@bankid",bankid);
                odbcCommand.Parameters.AddWithValue("@productGroupid",productGroupid);

                odbcDataReader = odbcCommand.ExecuteReader();
                if (odbcDataReader.HasRows)
                {
                   odbcDataReader.Read();
                    maxClaimBal= Convert.ToDouble(odbcDataReader["MAX_CLAIM_BAL"]);
                    odbcConnection.Close();
                }

            }
            catch (Exception ex) {
                Console.WriteLine(" Get SIT1 or Prod Max ClaimBal error: " + ex.Message.ToLower());
            }
            return maxClaimBal;
        }
        public string GetLGOnwer(string Claim_ID)
        {
            string lgOwner = "";
            // select T13User_ID from[dbo].[T13_Assign_Table] where T13Claim_ID = 'C63006877'
             Sql = "select T13User_ID from [dbo].[T13_Assign_Table] where T13Claim_ID = @ClaimID";
           // connection.Close();      
            connection = new SqlConnection(connectionString);
            try
            {
                command = new SqlCommand(Sql, connection);
                connection.Open();
                command.Parameters.AddWithValue("@ClaimID", Claim_ID);

                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                   
                    dataReader.Read();
                    lgOwner = dataReader["T13User_ID"].ToString();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" GET CI LG Owner error: " + ex.Message.ToString());
            }
            return lgOwner;
        }
        public SqlDataReader GetLGOnwerRec(string Claim_ID) {
           // string lgOwner="";
            // select T13User_ID from[dbo].[T13_Assign_Table] where T13Claim_ID = 'C63006877'
           // Sql = "select T13User_ID from [dbo].[T13_Assign_Table] where T13Claim_ID = @ClaimID";

           Sql =" SELECT TOP(10) [T01_Claim_ID] ,[T01_Order_No],[T01_Subject],[T01_LG_No],[T01_Port_No] "
      + ",[T01_Port_Year],[T01_Project_Type],[T01_Sub_Project_Type],[T01_Bank_Code],[T01_Bank_Name],[T01_Branch_Name] "
      + ",[T01_Customer_ID],[T01_Customer_Name],[T01_Project_Name],[T01_Project_address],[T01_Approve_Date] "
      + " ,[T01_LG_Date],[T01_Out_Standing],[T01_Claim_Date],[T01_Request_Date],[T01_Success_Document],[T01_App_Date] "
      + ",[T01_Process_Day],[T01_Avg_Out],[T01_Claim_Partial],[T01_Max_Claim],[T01_Claim_Max_Part_1],[T01_Claim_Max_Part_2] "
      + ",[T01_Claim_Percent_Max_Part_1],[T01_Claim_Percent_Max_Part_2],[T01_Real_Claim],[T01_Wait_Claim],[T01_Cancel_Claim] "
      + ",[T01_Remain_Claim],[T01_Claim_Part_1],[T01_Claim_Part_2],[T01_Claim_Percent_Part_1],[T01_Claim_Percent_Part_2] "
      + " ,[T01_Total_Claim],[T01_Max_Claim_Remain],[T01_Date_Create],[T01_Time_Create],[T01_User_Create]"
      + ",[T01_Date_Update],[T01_Time_Update],[T01_User_Update],[T01_Active],[T01_File_Name],[T01_File_Template],[T01_Default_Date] "
      + ",[T01_Cancel_Date_1],[T01_Cancel_Date_2],[T01_Claim_Online],[T01_Sum_Total_Claim],[T01_Sum_Total_Amount],[T01Business_Running] "
      +",[T01Management_1],[T01Management_2],[T01Management_3],[T01Management_4],[T01Management_5],[T01Other_Management],[T01Finance_1] "
      +",[T01Finance_2],[T01Finance_3],[T01Finance_4],[T01Finance_5],[T01Other_Finance],[T01Market_1],[T01Market_2],[T01Market_3] "
      + ",[T01Market_4],[T01Market_5],[T01Other_Market],[T01Capital_Asset],[T01Judgement],[T01Collectral],"
      +" [T01Collectral_Desc], [T01Additional_Document] "
      +" ,[T01Seq_Project] ,[T01Pay_Seq],[T01Description],[T01Assign_RequestNo] "
      + " FROM[DB_CI].[dbo].[T01_Claim_Insurance] where T01_Claim_Online = @ClaimID" ;

            connection = new SqlConnection (connectionString);
            try {
                command = new SqlCommand(Sql, connection);
                connection.Open();
                command.Parameters.AddWithValue("@ClaimID",Claim_ID);
                
                dataReader = command.ExecuteReader();
                //connection.Close();
            }
            catch (Exception ex) {
                Console.WriteLine(" GET CI LG owner error: " + ex.Message.ToString());
            }


            return dataReader;    
        }
        public OdbcDataReader GetCourtDateInfo(int lg_id)
        {
            Sql = "  SELECT lg_id ,LG_LOAN_ID, SUE_DATE, UNDECIDE_CASE_NO, DECIDE_CASE_NO, "
                + " JUDGMENT_DT, ESCORT_DT, FINAL_CASE_DT, AUCTION_SALE_DT "
                + " FROM CGS.TBL_RD_LG_LOAN WHERE lg_id =? and STATUS ='A' ";
            odbcConnection = new OdbcConnection(connectionString);
            try
            {
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcConnection.Open();
                odbcCommand.Parameters.AddWithValue("@lg_id", lg_id);
                
                odbcDataReader = odbcCommand.ExecuteReader();
                //odbcConnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(" Get SIT1 or Prod Max Court date info error: " + ex.Message.ToString());
            }
            return odbcDataReader;
        }
        public int GetavgYear(int productid,int prodno,int bankid) {

            int avgYear = 0;
            Sql = " SELECT AVG_YEAR FROM TBL_MD_AVG_OUT_CNF WHERE PRODUCT_ID = ?"
                + " AND PORT_NO = ? AND BANK_ID = ? AND STATUS = 'A'";
            odbcConnection = new OdbcConnection(connectionString);
            try
            {
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcConnection.Open();
                odbcCommand.Parameters.AddWithValue("@prodcutid", productid);
                odbcCommand.Parameters.AddWithValue("@portNo", prodno);
                odbcCommand.Parameters.AddWithValue("@bankid", bankid);
                

                odbcDataReader = odbcCommand.ExecuteReader();
                if (odbcDataReader.HasRows) {
                    odbcDataReader.Read();
                    avgYear = Convert.ToInt32(odbcDataReader["AVG_YEAR"]);
                    odbcConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Get everage year SIT1 or Prod error: " + ex.Message.ToLower());
            }
            return avgYear;

        }
        public double GetMaxClaim(int productid,int bankid,int productGroupid) {
            //ให้ใช้ตัวแรก 
            double maxClaim =0;
            Sql = " SELECT MAX_CLAIM FROM TBL_RD_CLAIM_ACCUM "
                + " WHERE PRODUCT_ID =? "
                + " AND BANK_ID = ? "
                + " AND PRODUCT_GROUP_ID = ? "
                + " ORDER BY ACCUM_ORDER DESC ";
            odbcConnection = new OdbcConnection(connectionString);
            try
            {
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcConnection.Open();
                odbcCommand.Parameters.AddWithValue("@prodcutid", productid);
                odbcCommand.Parameters.AddWithValue("@bankid", bankid);
                odbcCommand.Parameters.AddWithValue("@productGroupid", productGroupid);

                odbcDataReader = odbcCommand.ExecuteReader();
                if (odbcDataReader.HasRows) {
                    odbcDataReader.Read();
                    maxClaim = string.IsNullOrEmpty(odbcDataReader["MAX_CLAIM"].ToString())? 0: Convert.ToDouble(odbcDataReader["MAX_CLAIM"]);
                    //odbcConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Get everage year SIT1 or Prod  Get Max Claim error: " + ex.Message.ToLower());
            }
            return maxClaim;
        }
        public double GetAdjustClaimAmtAccum(int productid,int bankid,int productGroupid) {
            //ให้ใช้ตัวแรก 
            double adjClaimAccum = 0;
            Sql = " SELECT ADJUST_CLAIM_AMT_ACCUM FROM TBL_RD_CLAIM_ACCUM "
                 +" WHERE PRODUCT_ID = ? "
                 +" AND BANK_ID =? "
                 +" AND PRODUCT_GROUP_ID =? "
                 +" ORDER BY ACCUM_ORDER DESC; ";
            odbcConnection = new OdbcConnection(connectionString);
            try
            {
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcConnection.Open();
                odbcCommand.Parameters.AddWithValue("@prodcutid", productid);
                odbcCommand.Parameters.AddWithValue("@bankid", bankid);
                odbcCommand.Parameters.AddWithValue("@productGroupid", productGroupid);
                

                odbcDataReader = odbcCommand.ExecuteReader();
                if (odbcDataReader.HasRows)
                {
                    odbcDataReader.Read();
                    adjClaimAccum =  string.IsNullOrEmpty(odbcDataReader["ADJUST_CLAIM_AMT_ACCUM"].ToString())?0 : Convert.ToDouble(odbcDataReader["ADJUST_CLAIM_AMT_ACCUM"]);
                   // odbcConnection.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(" Get everage year SIT1 or Prod error: " + ex.Message.ToLower());
            }
            return adjClaimAccum;
        }
        public double GetfilingdtobgAmountAccumul(int productid,int bankid,int productGroupid) {
            double obgAmountAccummul = 0;
            //ให้ใช้ตัวแรก 
            Sql = " SELECT FILINGDTOBG_AMOUNT_ACCUMUL FROM TBL_RD_CLAIM_ACCUM "
              + " WHERE PRODUCT_ID = ? "
              + " AND BANK_ID = ? "
              + " AND PRODUCT_GROUP_ID = ? "
              + " ORDER BY ACCUM_ORDER DESC; ";    //'5910612';"; //= @lgNo ;" ;  //5910612

            // connection = new SqlConnection(connectionString);
            odbcConnection = new OdbcConnection(connectionString);
            try
            {

                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcCommand.Parameters.AddWithValue("@productid",productid);
                odbcCommand.Parameters.AddWithValue("@bankid",bankid);
                odbcCommand.Parameters.AddWithValue("@productGroupid", productGroupid);
                odbcConnection.Open();
                odbcDataReader = odbcCommand.ExecuteReader();
                if (odbcDataReader.HasRows) {
                    odbcDataReader.Read();
                    obgAmountAccummul = string.IsNullOrEmpty(odbcDataReader["FILINGDTOBG_AMOUNT_ACCUMUL"].ToString())? 0 : Convert.ToDouble(odbcDataReader["FILINGDTOBG_AMOUNT_ACCUMUL"]);
                    //odbcConnection.Close();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Get FILINGDTOBG_AMOUNT_ACCUMUL  error: " + ex.Message.ToString());
            }
            return obgAmountAccummul;
        }
        public double GetClaimAmtAccum(int productid,int bankid,int productGroupid) {

            //ให้ใช้ตัวแรก 
            double claimAmtAccum = 0;
            Sql =  " SELECT CLAIM_AMT_ACCUM FROM TBL_RD_CLAIM_ACCUM "
                 + " WHERE PRODUCT_ID = ? "
                 + " AND BANK_ID = ? "
                 + " AND PRODUCT_GROUP_ID = ? "
                 + " ORDER BY ACCUM_ORDER DESC; ";
            odbcConnection = new OdbcConnection(connectionString);
            try
            {
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcConnection.Open();
                odbcCommand.Parameters.AddWithValue("@prodcutid", productid);
                odbcCommand.Parameters.AddWithValue("@bankid", bankid);
                odbcCommand.Parameters.AddWithValue("@productGroupid", productGroupid);

                odbcDataReader = odbcCommand.ExecuteReader();
                if (odbcDataReader.HasRows) {
                    odbcDataReader.Read();
                    claimAmtAccum = string.IsNullOrEmpty(odbcDataReader["CLAIM_AMT_ACCUM"].ToString())? 0 : Convert.ToDouble(odbcDataReader["CLAIM_AMT_ACCUM"]);
                    //odbcConnection.Close();
                }
                return claimAmtAccum;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Get SIT1 or Prod  claimAmtAccum error: " + ex.Message.ToLower());
            }
            return claimAmtAccum;
        }

        public double   GetPreviousNpgAccumul(int productid,int bankid ,int productGroupid) {

            //ห้ใช้ตัวแรก
            double previousNpgAccumul = 0;
            Sql = " SELECT NPG FROM TBL_RD_CLAIM_ACCUM "
                + " WHERE PRODUCT_ID = ? "
                + " AND BANK_ID = ? "
                + " AND PRODUCT_GROUP_ID = ? "
                + " ORDER BY ACCUM_ORDER DESC; ";
            odbcConnection = new OdbcConnection(connectionString);
            try
            {
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcConnection.Open();
                odbcCommand.Parameters.AddWithValue("@prodcutid", productid);
                odbcCommand.Parameters.AddWithValue("@bankid", bankid);
                odbcCommand.Parameters.AddWithValue("@productGroupid", productGroupid);

                odbcDataReader = odbcCommand.ExecuteReader();
                if (odbcDataReader.HasRows) {
                    odbcDataReader.Read();
                    previousNpgAccumul = string.IsNullOrEmpty(odbcDataReader["NPG"].ToString())? 0 : Convert.ToDouble(odbcDataReader["NPG"]);
                    //odbcConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Get everage year SIT1 or Prod previousNpgAccumul error: " + ex.Message.ToLower());
            }
            return previousNpgAccumul;
        }
        public double? GetLoanObgAmount(int lgId ) {
            double loanObgAmount = 0;
            Sql = "SELECT G.LOAN_OBG_AMOUNT FROM TBL_RD_LG_LOAN  L "
                + " INNER JOIN TBL_RD_LG_GUA_OBG G ON L.LG_LOAN_ID = G.LG_LOAN_ID "
                + " WHERE G.LG_ID =?; ";
            odbcConnection = new OdbcConnection(connectionString);
            try
            {
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcConnection.Open();
                odbcCommand.Parameters.AddWithValue("@lgId", lgId);
                //odbcCommand.Parameters.AddWithValue("@bankid", bankid);
                //odbcCommand.Parameters.AddWithValue("@productGroupid", productGroupid);

                odbcDataReader = odbcCommand.ExecuteReader();
                if (odbcDataReader.HasRows)
                {
                    odbcDataReader.Read();
                    loanObgAmount =  string.IsNullOrEmpty(odbcDataReader["LOAN_OBG_AMOUNT"].ToString())? 0: Convert.ToDouble(odbcDataReader["LOAN_OBG_AMOUNT"]);
                    //odbcConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Get everage year SIT1 or Prod loanObgAmount error: " + ex.Message.ToLower());
            }
            return loanObgAmount;
        }
        public int UpdateT01_request_Online(string lastStatus,string lgNo)
        {
            int ret=0;
            Sql = " Update DB_CLAIM_ONLINE.dbo.TW01_Claim_Online set T01Last_Status = @lastStatus " //100
                 + " where T01LG_No  = @T01lg_no  and  T01Last_Status ='010' ;";
            connection = new SqlConnection(connectionString);
            try {
                connection.Open();
                command = new SqlCommand(Sql,connection);
                command.Parameters.AddWithValue("@lastStatus",lastStatus);
                command.Parameters.AddWithValue("@T01lg_no", lgNo);
               
                ret = command.ExecuteNonQuery();
            }
            catch( Exception ex )
            {
                Console.WriteLine( "...can not update TW01_Claim_Online...." );
            }
            //connection.Close();
            return ret; 
        }

        public int InsertTW03_Status(string claimId,string statusCode) {
            int ret = 0;
            Sql = "   INSERT INTO[dbo].[TW03_Status] "
              + "  ([T03Claim_ID] "
             + ",[T03Status_Code] "
             + ",[T03Status_Date] "
            + " ,[T03Status_Time] "
            + " ,[T03Status_User] "
            + " ,[T03Status_Comment1] "
            + "  ) "
            + " VALUES "
             + " ( @T03Claim_ID "
             + " , @T03Status_Code  "
             + " , @T03Status_Date "
             + " , @T03Status_Time  "
             + " , @T03Status_User  "
             + " ,@T03Status_Comment1"
             + ") ";
           // Sql = " Update DB_CLAIM_ONLINE.dbo.TW01_Claim_Online set T01Last_Status = @lastStatus " //100
           //      + " where T01LG_No  = @T01lg_no and  T01Last_Status ='010' ;";
            
            connection = new SqlConnection(connectionString);
            try
            {
                DateTime dd = CheckBCYear(DateTime.Now);
                string d = dd.ToString("yyyyMMdd");
                string t = dd.ToString("HHmmss");
                connection.Open();
                 command = new SqlCommand(Sql, connection);
                command.Parameters.AddWithValue("@T03Claim_ID", claimId);
                command.Parameters.AddWithValue("@T03Status_Code", statusCode);
                command.Parameters.AddWithValue("@T03Status_Date", d);
                command.Parameters.AddWithValue("@T03Status_Time", t);
                command.Parameters.AddWithValue("@T03Status_User", "99999");
                command.Parameters.AddWithValue("@T03Status_Comment1", "TCGSYSTEM");

                ret = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("...can not update TW01_Claim_Online....");
            }
            //connection.Close();
            return ret;
        }
        public string GetAssignUser(string userName) {
            string ret=null;
            Sql = "SELECT [USER_USERNAME],[USER_FIRST_NAME],[USER_LAST_NAME],[USER_TITLE],[USER_EMAIL] "
              + ", substring( [USER_EMAIL],1 ,charindex('@',[USER_EMAIL]) - 1 )as 'AssignName' "
              + " ,[USER_DEPARTMENT] FROM[DB_Pre_Migration].[dbo].[TCG_TBL_MD_USERS] "
              + " where user_username = @UserName ";
            connection = new SqlConnection(connectionString);
            try {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                command.Parameters.AddWithValue("@UserName",userName);
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    ret = string.IsNullOrEmpty(dataReader["AssignName"].ToString()) ? null : dataReader["AssignName"].ToString();
                    // ret = dataReader.GetString(5);
                    //connection.Close();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(" can not select TCG_TBL_MD_USER");
            }
            return ret;
        }
        private DateTime CheckBCYear(DateTime date)
        {
            DateTime returndate = DateTime.Now;

            if (date.Year < 2500 && date.Year > 1950)
            {
                returndate = date.AddYears(543);
            }

            return returndate;
        }
        public string GetBankId(string bankCode)
        {
            string ret = "";
            Sql = "SELECT BANK_ID ,bank_code  FROM DB_CGSAPI_MASTER.dbo.TBL_MD_BANK "
                   + " WHERE BANK_CODE = @bankcode ;";
            connection = new SqlConnection(connectionString);
            try {
                connection.Open();
                command = new SqlCommand(Sql, connection);
                command.Parameters.AddWithValue("@bankcode", bankCode);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                 ret=   dataReader.GetValue(0).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("... get bank id error... " + ex.Message.ToString());
            }
            //connection.Close();
            return ret;
        }
        public int GetProductGroup(int product_id) {
            int ret = 0;
            Sql = " SELECT product_group_id,prod_grp_name "
                + " FROM tbl_md_product_group "
                + " WHERE status = 'A'  and product_group_id =? ";

            odbcConnection = new  OdbcConnection(connectionString);
            try
            {
                odbcConnection.Open();
                odbcCommand = new OdbcCommand(Sql, odbcConnection);
                odbcCommand.Parameters.AddWithValue("@product_group_id", product_id);
                odbcDataReader = odbcCommand.ExecuteReader();
                while (odbcDataReader.Read())
                {
                    ret = Convert.ToInt32(odbcDataReader.GetValue(0));
                   // odbcConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("... get GetProductGroup  error... " + ex.Message.ToString());
            }

            return ret;

        }
    }
}
