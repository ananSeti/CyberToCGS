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
        public int? loanId { get; set; }
        public string detailType { get; set; }
        public DateTime? sueDtAct { get; set; }
        public string courtName { get; set; }
        public string undecideCaseNo { get; set; }
        public string decideCaseNo { get; set; }
        public DateTime? judgmentDt { get; set; }
        public DateTime? escortDt { get; set; }
        public DateTime? finalCaseDt { get; set; }
        public DateTime? auctionDt { get; set; }
        public double? filingDtObgAmount { get; set; }
        public double requestDtObgAmount { get; set; }
        public DateTime? defaultDt { get; set; }
        public int? loanPage { get; set; }
        public double? loanObgAmount { get; set; }
    }
    public class PostConsider
    {
        public int considerInfId { get; set; }
        public string remark { get; set; }
        public int considerId { get; set; }
    }
    public class SaveFormClaimRoot
    {
        public int lgId { get; set; }
        public int? requestClaim { get; set; }
        public string payConditionType { get; set; }
        public int? claimPgsModelId { get; set; }
        public int? maxClaimModelId { get; set; }
        public int productId { get; set; }
        public int productGroupId { get; set; }
        public int portNo { get; set; }
        public int bankId { get; set; }
        public double maxClaimBal { get; set; }
        public int avgYear { get; set; }
        public double maxClaim { get; set; }
      
        public double adjustClaimAmtAccum {get;set;}
        public double filingdtobgAmountAccumul { get; set; }
        public double claimAmtAccum { get; set; }
        public double previousNpgAccumul { get; set; }
        public string refuseFlag { get; set; }
        public string loanContact { get; set; }
        public string guarantorContact { get; set; }
        public string guaCareBy { get; set; }
        public string guaCarePostion { get; set; }
        public string guaCarePhone { get; set; }
        public string guaCareMobile { get; set; }
        public string guaCareFaxNo { get; set; }
        public string guaCareEmail { get; set; }
        public string authorizedBy { get; set; }
        public string authorizedPosition { get; set; }
        public string recieveFullAmountFlag { get; set; }

        public List<ClaimCollateral> claimCollaterals { get; set; }
        public List<ClaimLoan> claimLoans { get; set; }
        public List<PostConsider> postConsider { get; set; }
        public SaveFormClaimRoot()
        {
            this.claimCollaterals = new List<ClaimCollateral>();
            this.claimLoans = new List<ClaimLoan>();
            this.postConsider = new List<PostConsider>();
        }
    }


}
