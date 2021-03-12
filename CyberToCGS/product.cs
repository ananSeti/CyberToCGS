using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS
{
    
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Product
    {
        
        public string preReqStatus { get; set; }
        public int productId { get; set; }
        public int roundId { get; set; }
        public string guaAmount { get; set; }
        public int prdPayFeeType { get; set; }
        public object prdReduGuaType { get; set; }
        public object refNo1 { get; set; }
        public object refNo2 { get; set; }
        public object refNo3 { get; set; }
        public object advFeeYearId { get; set; }
    }

    public class Bank
    {
        public int bankId { get; set; }
        public int bankBrnUseLimit { get; set; }
        public int bankBrnSendOper { get; set; }
        public string guaCareName { get; set; }
        public string guaCareMobile { get; set; }
        public string guaCarePhone { get; set; }
        public string guaCareEmail { get; set; }
        public string guaApproveEmail { get; set; }
        public string guaRemark { get; set; }
    }

    public class Address
    {
        public bool active { get; set; }
        public string addressType { get; set; }
        public string addressNo { get; set; }
        public object addressMoo { get; set; }
        public object addressAlley { get; set; }
        public object addressRoad { get; set; }
        public int subDistrictId { get; set; }
        public int districtId { get; set; }
        public int provinceId { get; set; }
        public string postalCode { get; set; }
        public object countryId { get; set; }
        public object addressOversea { get; set; }
        public object nameType { get; set; }
    }

    public class Spouse
    {
        public string identification { get; set; }
        public string identificationType { get; set; }
        public int titleId { get; set; }
        public string cusNameTh { get; set; }
        public string cusSurnameTh { get; set; }
        public object cusNameEn { get; set; }
        public object cusSurnameEn { get; set; }
        public DateTime birthDate { get; set; }
        public string telephoneNo { get; set; }
        public string mobilePhoneNo { get; set; }
        public object faxNo { get; set; }
        public object email { get; set; }
        public string registerCapital { get; set; }
        public List<Address> address { get; set; }
    }

    public class Customer
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
    }

    public class Manager
    {
        public string idCard { get; set; }
        public int titleId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string identificationType { get; set; }
        public string exp { get; set; }
        public int customerId { get; set; }
        public string registerCapital { get; set; }
    }

    public class Asset
    {
        public string fixedAssetType { get; set; }
        public string amt { get; set; }
    }

    public class Finance2
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
    }

    public class Credit2
    {
        public object loanName { get; set; }
        public int contractId { get; set; }
        public string loanLimit { get; set; }
        public object loanBal { get; set; }
        public string loanInterest { get; set; }
        public string guaByTcg { get; set; }
        public int debtMonth { get; set; }
        public string rateType { get; set; }
        public object ratio { get; set; }
        public string contractName { get; set; }
        public string loanTypeName { get; set; }
        public string contractNo { get; set; }
        public string contractDate { get; set; }
        public string purposeCode { get; set; }
        public string debtPeriod { get; set; }
        public string debtDescription { get; set; }
        public string guaLimit { get; set; }
        public string contractNoDm { get; set; }
        public int contractDetailId { get; set; }
        public int debtorTypeId { get; set; }
        public string guaLoadPurpose { get; set; }
        public List<object> oldCredit { get; set; }
        public List<Credit2> credit { get; set; }
        public List<object> col { get; set; }
        public List<object> guarantorContract { get; set; }
        public List<object> guarantorTcg { get; set; }
    }

    public class Contract2
    {
        public int loanContractId { get; set; }
        public object loanName { get; set; }
        public string contractNo { get; set; }
        public string contractName { get; set; }
        public string contractDate { get; set; }
        public string purposeCode { get; set; }
        public string debtPeriod { get; set; }
        public string debtDescription { get; set; }
        public string loanLimit { get; set; }
        public string guaLimit { get; set; }
        public int debtMonth { get; set; }
        public string rateType { get; set; }
        public object ratio { get; set; }
        public string loanTypeName { get; set; }
        public string contractNoDm { get; set; }
        public object loanInterest { get; set; }
        public int contractDetailId { get; set; }
        public int debtorTypeId { get; set; }
        public string preRequestName { get; set; }
        public object issuedAs { get; set; }
        public object issuedAsName { get; set; }
        public List<Contract2> contract { get; set; }
    }

    public class Answer2
    {
        public int answerId { get; set; }
        public bool status { get; set; }
    }

    public class Answer
    {
        public int questionDetailId { get; set; }
        public List<Answer> answers { get; set; }
    }

    public class PayInSlip
    {
        public int documentTypeId { get; set; }
        public object lgId { get; set; }
        public object payinslipDt { get; set; }
        public object payinslipAmount { get; set; }
        public object bankAccountName { get; set; }
        public object bankAccountNo { get; set; }
        public object bankId { get; set; }
        public object bankName { get; set; }
        public object branchId { get; set; }
        public object branchName { get; set; }
        public object chequeName { get; set; }
        public string status { get; set; }
        public int createBy { get; set; }
        public string createDt { get; set; }
        public object updateBy { get; set; }
        public object updateDt { get; set; }
        public List<Answer> answer { get; set; }
        public object remark { get; set; }
        public List<object> preScreeningInitialNew { get; set; }
    }
    /*
     *6. บันทึกคำขอแบบ indirect 
     *htt://<HOST_NAME>/request-service/api/external/request
     *
     */
    public class IndirectRequest
    {
        public Product product { get; set; }
        public Bank bank { get; set; }
      
        public List<Customer> customer { get; set; }
        public Finance2 finance { get; set; }
        public Credit2 credit { get; set; }
        public Contract2 contract { get; set; }
        public PayInSlip payInSlip { get; set; }
    }


}
