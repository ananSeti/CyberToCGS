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
            public object customerStatus { get; set; }
            public string customerType { get; set; }
            public object customerGrade { get; set; }
            public object customerScore { get; set; }
            public object raceId { get; set; }
            public object raceStr { get; set; }
            public object nationalityId { get; set; }
            public object nationalityStr { get; set; }
            public object refReqNumber { get; set; }
            public int customerId { get; set; }
            public string borrowerType { get; set; }
            public int titleId { get; set; }
            public string cusNameTh { get; set; }
            public string cusSurnameTh { get; set; }
            public object cusNameEn { get; set; }
            public object cusSurnameEn { get; set; }
            public string gender { get; set; }
            public string marriedStatus { get; set; }
            public DateTime birthDate { get; set; }
            public string educationLevel { get; set; }
            public List<int> career { get; set; }
            public string telephoneNo { get; set; }
            public string mobilePhoneNo { get; set; }
            public object faxNo { get; set; }
            public object email { get; set; }
            public int depLevelId { get; set; }
            public DateTime proveDate { get; set; }
            public string businessExp { get; set; }
            public object registerDate { get; set; }
            public string registerCapital { get; set; }
            public object certificateDate { get; set; }
            public string customerAlive { get; set; }
            public object amountCol { get; set; }
            public object kycResult { get; set; }
            public object kycDate { get; set; }
            public object guarantorRelationCode { get; set; }
            public object guarantorRelationStr { get; set; }
            public object seq { get; set; }
            public List<Address> address { get; set; }
            public List<object> relation { get; set; }
            public Spouse spouse { get; set; }
        public SqlDataReader rec { get; internal set; }
        protected string T01OnlineID;

        public List<Customer> operation(string T01Online_ID)
        {
            this.T01OnlineID = T01Online_ID;
            Database.Database db2 = Database.Database.GetInstance("DB_CGSAPI_MASTER");
            rec = db2.GetViewCgsapiCustomer(T01OnlineID);
            var cust = new List<Customer>();

           // Customer c= new Customer();
            while (rec.Read())
            {
              Customer  c = new Customer() {
                    identification = rec["identification"].ToString(),
                    identificationType = rec["identificationType"].ToString(),
                    customerStatus = rec["customerStatus"].ToString(),
                    customerType =rec["customerType"].ToString(),

                };

                cust.Add(c);
            }
           
            //var cust = new List<Customer>()
            //{
            //    new Customer()
            //    {
            //        identification= "4081299709357",
            //        identificationType= "C",
            //        customerStatus =null,
            //        customerType= "02",
            //        customerGrade= null,
            //        customerScore= null,
            //        raceId = null,
            //        raceStr = null,
            //        nationalityId = null,
            //        nationalityStr = null,
            //        refReqNumber = null,
            //        customerId = 3375,
            //        borrowerType = "01",
            //        titleId = 10,
            //        cusNameTh = "สุนิติ",
            //        cusSurnameTh = "ฟามาระ",
            //        cusNameEn = null,
            //        cusSurnameEn = null,
            //        gender= "M",
            //        marriedStatus= "01",
            //        birthDate = Convert.ToDateTime("1965-09-13"),
            //        educationLevel= "7"
            //    }
            //};
            return cust;
        }


    }
}
