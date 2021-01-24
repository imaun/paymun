using Paymun.Core.Models.Enum;

namespace Paymun.Core.Models {

    public class PaymentVerifyRequest {

        public PaymentVerifyRequest() { }

        public long Amount { get; set; }
        public string MerchantId { get; set; }
    }
}
