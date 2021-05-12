using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS
{
   public class FinanceS
    {
        public string amtType { get; set; }
        public string yearPast1 { get; set; }
        public string yearPast2 { get; set; }
        public string yearPast3 { get; set; }
        public string yearPast4 { get; set; }
        public string yearPast5 { get; set; }
        public string yearCurrent { get; set; }
        public string yearEstimate1 { get; set; }
        public string yearEstimate2 { get; set; }
        public string yearEstimate3 { get; set; }
        public string yearEstimate4 { get; set; }
        public string yearEstimate5 { get; set; }
        public object isicId { get; set; }
        public object isicCodeNameTh { get; set; }
        public object operation { get; set; }
        public DateTime startDate { get; set; }
        public int employeeAmount { get; set; }
        public object employeeAdd { get; set; }
        public List<Manager> manager { get; set; }
        public List<Asset> asset { get; set; }
        public List<Finance2> finance { get; set; }
        public Address address { get; set; }
        public string ebidta { get; set; }
        public string amtCreditOwn { get; set; }
        public int tcgBusinessId { get; set; }
        public string amtCredit { get; set; }
        public string dscr { get; set; }
        public object typeEstablishment { get; set; }
        public object ownerBusinessLocation { get; set; }

        protected string T01OnlineID;
    }
}
