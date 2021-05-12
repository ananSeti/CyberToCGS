using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.Database
{
   public class Database
    {
        string db_online = @"server = 192.168.0.83; database = DB_ONLINE_CG; user = sa; password = ABC123abc$; ";
        string db_apiMaster = @"server = 192.168.0.83; database = DB_CGSAPI_MASTER; user = sa; password = ABC123abc$; ";
        string db_claim_online = @"server = 192.168.0.83; database = DB_CLAIM_ONLINE; user = sa; password = ABC123abc$; ";
        string localDb = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=testDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string connectionString = null;

        SqlConnection connection;
        SqlCommand command;
        SqlDataReader dataReader;
     
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
        }


        private static Database _database;
        private static readonly object _lock = new object();
        public string Value { get; set; }
        public static Database GetInstance(string value)
        {
            if(_database == null)
            {
                lock (_lock)
                {
                    if(_database == null)
                    {
                        _database = new Database(value);
                        _database.Value = value;

                    }
                }
               
            }
           
            return _database;
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
                //Console.WriteLine("------- GetT01_Request_Online() ----------- ");
                //Console.WriteLine("Param request no :" + request_no );
               // while (dataReader.Read())
               // {
                    //var bb = dataReader["T01Sub_Project_Type"];
                   // Console.WriteLine(dataReader.GetValue(0) + "-" + dataReader.GetValue(1) + "-" + dataReader.GetValue(2) + "-" + dataReader.GetValue(3) + "-" + dataReader.GetValue(4) + "-" + dataReader.GetValue(5));
               //     rec = (IDataRecord)dataReader;
               // }
               // dataReader.Close();
               // command.Dispose();
               // connection.Close();
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
                + " T01Collectral, T01Collectral_Desc, T01IsResend "
                + " FROM DB_CLAIM_ONLINE.dbo.TW01_Claim_Online  where T01LG_No = @T01lg_no ; " ;


            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(Sql, connection);

                command.Parameters.Add("@T01lg_no", SqlDbType.NVarChar);
                command.Parameters["@T01lg_no"].Value = T01lg_no;


                dataReader = command.ExecuteReader();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Get  DB_CLAIM_ONLINE.dbo.TW01_Claim_Online  Error" + ex.Message.ToString());
            }
            return dataReader;
        }
    }
}
