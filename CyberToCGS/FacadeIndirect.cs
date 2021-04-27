using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS
{
  public  class FacadeIndirect
    {
        protected IndirectRequest indirectRequest = new IndirectRequest();
        protected Product _product;
        protected Bank _bank;
        protected  Customer _customer;
        
        SqlDataReader rec;

        protected string T01OnlineID;

        public FacadeIndirect(Product product,Bank bank,Customer customer)
        {
           
            this._product = product;
            this._bank = bank;
            this._customer = customer;

         

            string fromDate;
            string toDate;

            loadJson l = new loadJson();

            l.ReadAppConfig();
            fromDate = l.GetFormatFromdate();
            toDate = l.GetFormatTodate();
            Database.Database db = Database.Database.GetInstance("DB_ONLINE_CG");
            
            rec = db.GetT01_Request_Online("5703591");
            //T01Online_ID =O5700002

            //  db.GetT01_Request_Online(fromDate, toDate);
            //request no =5703591

            //if (rec.HasRows)
            //{
            //    _product.rec = rec;
            //    _bank.rec = rec;
            //    _customer.rec = rec;
            //}
            //while (rec.Read())
            //    Console.WriteLine(rec.GetValue(0) + "-" + rec.GetValue(2));
        }
        public IndirectRequest Operation()
        {
            List<Customer> cust = new List<Customer>();
            while (rec.Read())
            {
                T01OnlineID = rec["T01Online_ID"].ToString();

            }


            this._product.operation(T01OnlineID);
            this._bank.operation(T01OnlineID);
            cust = this._customer.operation(T01OnlineID);

            indirectRequest.product = _product;
            indirectRequest.bank = _bank;
            indirectRequest.customer = cust.ToList();

            return indirectRequest;
        }
    }
}
