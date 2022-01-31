namespace Paymun.Core.Models
{

    public class GatewayCallbackResult
    {
        public long OrderId { get; set; }
        public string BankToken { get; set; }
        public string BankReferenceId { get; set; }
        public bool Success { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string RRN { get; set; }
        public string SecurePAN { get; set; }
        public string TraceNo { get; set; }
        public string CID { get; set; }
    }
}
