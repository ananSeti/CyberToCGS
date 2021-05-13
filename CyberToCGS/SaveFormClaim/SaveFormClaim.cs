using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.SaveFormClaim
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ClaimCollateral
    {
        public string sueStatus { get; set; }
        public string remark { get; set; }
        public int collateralId { get; set; }
    }

    public class ClaimLoan
    {
        public int loanId { get; set; }
        public string detailType { get; set; }
        public DateTime sueDtAct { get; set; }
        public string courtName { get; set; }
        public string undecideCaseNo { get; set; }
        public string decideCaseNo { get; set; }
        public DateTime judgmentDt { get; set; }
        public DateTime escortDt { get; set; }
        public DateTime finalCaseDt { get; set; }
        public DateTime auctionDt { get; set; }
        public int filingDtObgAmount { get; set; }
        public int requestDtObgAmount { get; set; }
        public DateTime defaultDt { get; set; }
        public int loanPage { get; set; }
    }

    public class SaveFormClaimRoot
    {
        public int lgId { get; set; }
        public int requestClaim { get; set; }
        public List<ClaimCollateral> claimCollaterals { get; set; }
        public List<ClaimLoan> claimLoans { get; set; }
    public SaveFormClaimRoot()
        {
            this.claimCollaterals = new List<ClaimCollateral>();
            this.claimLoans = new List<ClaimLoan>();
        }
    }


}
