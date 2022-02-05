using Paymun.Core.Models.Enum;

namespace Paymun.Core.Models {

    public class PaymentRequestResult {

        public PaymentRequestResult() { }

        public PaymentRequestStatus Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string PaymentPageUrl { get; set; }
        public string Token { get; set; }
        public bool Succeeded => Status == PaymentRequestStatus.Succeeded;
    }
}
