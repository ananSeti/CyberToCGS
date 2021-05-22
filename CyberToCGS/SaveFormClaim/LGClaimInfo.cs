using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberToCGS.SaveFormClaim
{
    // LGCliamInfo myDeserializedClass = JsonConvert.DeserializeObject<LGCliamInfo>(myJsonResponse); 
    public class Content
    {
        public object postRequestId { get; set; }
        public object refPostRequestId { get; set; }
        public object statusReq { get; set; }
        public object considerDetail { get; set; }
        public int lgId { get; set; }
        public DateTime lgDate { get; set; }
        public int lgAmount { get; set; }
        public int lgObgAmount { get; set; }
        public object lgGuaObgIdSeq { get; set; }
        public object lgDueDt { get; set; }
        public string lgNo { get; set; }
        public object guaType { get; set; }
        public string lgName { get; set; }
        public object lgStatus { get; set; }
        public object customerId { get; set; }
        public object customerInfId { get; set; }
        public object cifNo { get; set; }
        public string titleAbbrTh { get; set; }
        public string customerNameTh { get; set; }
        public string customerSurnameTh { get; set; }
        public string titleSuffixTh { get; set; }
        public object registerDt { get; set; }
        public string identificationId { get; set; }
        public string customerType { get; set; }
        public int productId { get; set; }
        public object productGroupId { get; set; }
        public object productSubGroupId { get; set; }
        public string prodGrpName { get; set; }
        public string productName { get; set; }
        public object productCode { get; set; }
        public int bankId { get; set; }
        public object bankNameTh { get; set; }
        public object bankNameAbbr { get; set; }
        public object branchId { get; set; }
        public object branchNameTh { get; set; }
        public object createDt { get; set; }
        public object postReqSendDt { get; set; }
        public object postReqStatus { get; set; }
        public object postReqStatusDesc { get; set; }
        public object debtorDesc { get; set; }
        public object documentTypeCode { get; set; }
        public object createBy { get; set; }
        public object updateBy { get; set; }
        public object preRequestId { get; set; }
        public object workflowCd { get; set; }
        public object workflowStatus { get; set; }
        public object assignee { get; set; }
        public object assigneeGroup { get; set; }
        public object assigner { get; set; }
        public object assignerGroup { get; set; }
        public int workFlowSelecedStateId { get; set; }
        public object assigneeId { get; set; }
        public object postReturnBankReason { get; set; }
        public List<object> button { get; set; }
        public object rejectRemark { get; set; }
        public object cancelRemark { get; set; }
        public object docList { get; set; }
        public object docTcg { get; set; }
        public object docBank { get; set; }
        public object applicationDocument { get; set; }
        public object adjustLgList { get; set; }
        public object paymentDto { get; set; }
        public object guaPostReq { get; set; }
        public object postReqHistoryList { get; set; }
        public object collateralList { get; set; }
        public object loanList { get; set; }
        public object considerHistoryList { get; set; }
        public object reduceReturnFeeDto { get; set; }
        public object reduceReturnFeeAdvDto { get; set; }
        public object newLoanObgAmount { get; set; }
        public object refundGuaObgDto { get; set; }
        public object productSubgroupName { get; set; }
        public object payConditionType { get; set; }
        public object remark { get; set; }
        public object rangeOverDueDate { get; set; }
        public object billPaymentFlg { get; set; }
        public object isRefundFee { get; set; }
        public object customerName { get; set; }
        public object debtcardId { get; set; }
        public object caseId { get; set; }
        public object postRequestIdSeq { get; set; }
        public object batchFileNumber { get; set; }
        public object batchFlg { get; set; }
        public object effectiveDt { get; set; }
        public object expireDt { get; set; }
        public object usersIdMaker { get; set; }
        public object adjustGuaLoanList { get; set; }
    }

    public class Sort
    {
        public bool sorted { get; set; }
        public bool unsorted { get; set; }
        public bool empty { get; set; }
    }

    public class Pageable
    {
        public Sort sort { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public int offset { get; set; }
        public bool paged { get; set; }
        public bool unpaged { get; set; }
    }

    public class LGClaimInfo
    {
        public List<Content> content { get; set; }
        public Pageable pageable { get; set; }
        public int totalElements { get; set; }
        public bool last { get; set; }
        public int totalPages { get; set; }
        public bool first { get; set; }
        public Sort sort { get; set; }
        public int numberOfElements { get; set; }
        public int size { get; set; }
        public int number { get; set; }
        public bool empty { get; set; }
    }


}
