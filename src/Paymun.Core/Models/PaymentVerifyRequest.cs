using Paymun.Core.Models.Enum;

namespace Paymun.Core.Models {

    public class PaymentVerifyRequest {

        public PaymentVerifyRequest() { }

        public long Amount { get; set; }
        public string MerchantId { get; set; }
        public string ReferenceId { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public PaymentVerifiyStatus Status { get; set; }
        public bool Succeeded => Status == PaymentVerifiyStatus.Succeeded;
    }
}
