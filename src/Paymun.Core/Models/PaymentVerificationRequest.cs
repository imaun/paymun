using System.Text.Json.Serialization;

namespace Paymun.Core.Models {

    public class PaymentVerificationRequest {

        public PaymentVerificationRequest() { }

        public long Amount { get; set; }
        public string MerchantId { get; set; }
    }
}
