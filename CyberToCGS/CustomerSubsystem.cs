using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS
{
  public  class Customer
    {
        
        
            public object customerUserType { get; set; }
            public string identification { get; set; }
            public string identificationType { get; set; }
            public string customerStatus { get; set; }
            public string customerType { get; set; }
            public string customerGrade { get; set; }
            public string customerScore { get; set; }
            public int? raceId { get; set; }
            public string raceStr { get; set; }
            public int? nationalityId { get; set; }
            public string nationalityStr { get; set; }
            public int? refReqNumber { get; set; }
            public int? customerId { get; set; }
            public string borrowerType { get; set; }
            public int? titleId { get; set; }
            public string cusNameTh { get; set; }
            public string cusSurnameTh { get; set; }
            public string cusNameEn { get; set; }
            public string cusSurnameEn { get; set; }
            public string gender { get; set; }
            public string marriedStatus { get; set; }
            public string birthDate { get; set; }
            public string educationLevel { get; set; }
            public List<int?> career { get; set; }
            public string telephoneNo { get; set; }
            public string mobilePhoneNo { get; set; }
            public string faxNo { get; set; }
            public string email { get; set; }
            public int? depLevelId { get; set; }
            public DateTime? proveDate { get; set; }
            public string businessExp { get; set; }
            public string registerDate { get; set; }
            public string registerCapital { get; set; }
            public string certificateDate { get; set; }
            public string customerAlive { get; set; }
            public string amountCol { get; set; }
            public string kycResult { get; set; }
            public string kycDate { get; set; }
            public string guarantorRelationCode { get; set; }
            public string guarantorRelationStr { get; set; }
            public int? seq { get; set; }
            public List<Address> address { get; set; }
            public List<object> relation { get; set; }
            public Spouse spouse { get; set; }
             //public SqlDataReader rec { get; internal set; }
             protected string T01OnlineID;
            

        public Customer()
        {
            this.career = new List<int?>();
        }
        public List<Customer> operation(string T01Online_ID)
        {
            SqlDataReader rec;
            this.T01OnlineID = T01Online_ID;
            Database.Database db2 = Database.Database.GetInstance("DB_CGSAPI_MASTER");
            rec = db2.GetViewCgsapiCustomer(T01OnlineID);
            var cust = new List<Customer>();
                      
            while (rec.Read())
            {
                List<int?> carr = new List<int?>();
                carr.Add( string.IsNullOrEmpty(rec["career"].ToString()) ? (int?)null : Convert.ToInt32(rec["career"]));
                List<Address> addr = new List<Address>();
                Spouse spo = new Spouse();

                Customer c = new Customer() {
                    identification = rec["identification"].ToString(),
                    identificationType = rec["identificationType"].ToString(),
                    customerStatus = rec["customerStatus"].ToString(),
                    customerType = rec["customerType"].ToString(),
                    customerGrade = rec["customerGrade"].ToString(),
                    customerScore = rec["customerScore"].ToString(),
                    raceId = string.IsNullOrEmpty(rec["raceId"].ToString()) ? (int?)null : Convert.ToInt32(rec["raceId"]),
                    raceStr = rec["raceStr"].ToString(),
                    nationalityId = string.IsNullOrEmpty(rec["nationalityId"].ToString()) ? (int?)null : Convert.ToInt32(rec["nationalityId"]),
                    nationalityStr = rec["nationalityStr"].ToString(),
                    refReqNumber = string.IsNullOrEmpty(rec["refReqNumber"].ToString()) ? (int?)null : Convert.ToInt32(rec["refReqNumber"]),
                    customerId = string.IsNullOrEmpty(rec["customerId"].ToString()) ? (int?)null : Convert.ToInt32(rec["customerId"]),
                    borrowerType = rec["borrowerType"].ToString(),
                    titleId = string.IsNullOrEmpty(rec["titleId"].ToString()) ? (int?)null : Convert.ToInt32(rec["titleId"]),
                    cusNameTh = rec["cusNameTh"].ToString(),
                    cusSurnameTh = rec["cusSurnameTh"].ToString(),
                    cusNameEn = rec["cusNameEn"].ToString(),
                    cusSurnameEn = rec["cusSurnameEn"].ToString(),
                    gender = rec["gender"].ToString(),
                    marriedStatus = rec["marriedStatus"].ToString(),
                    birthDate = rec["birthDate"].ToString(),
                    educationLevel = rec["educationLevel"].ToString(),
                    career = carr.ToList(),
                    telephoneNo = rec["telephoneNo"].ToString(),
                    mobilePhoneNo = rec["mobilePhoneNo"].ToString(),
                    faxNo = rec["faxNo"].ToString(),
                    email = rec["email"].ToString(),
                    depLevelId = string.IsNullOrEmpty(rec["depLevelId"].ToString()) ? (int?)null : Convert.ToInt32(rec["depLevelId"]),
                    proveDate = string.IsNullOrEmpty(rec["proveDate"].ToString()) ? (DateTime?)null : Convert.ToDateTime(rec["proveDate"]),
                    businessExp = rec["businessExp"].ToString(),
                    registerDate = rec["registerDate"].ToString(),
                    registerCapital = rec["registerCapital"].ToString(),
                    certificateDate = rec["certificateDate"].ToString(),
                    customerAlive = rec["customerAlive"].ToString(),
                    amountCol = rec["amountCol"].ToString(),
                    kycResult = rec["kycResult"].ToString(),
                    kycDate = rec["kycDate"].ToString(),
                    guarantorRelationCode = rec["guarantorRelationCode"].ToString(),
                    guarantorRelationStr = rec["guarantorRelationStr"].ToString(),
                    seq = string.IsNullOrEmpty(rec["seq"].ToString()) ? (int?)null : Convert.ToInt32(rec["seq"]),
                    //s_identification
                    //s_identificationType
                    address = addr.ToList(),
                    

                };

                cust.Add(c);
            }

           
            return cust;
        }


    }
}
