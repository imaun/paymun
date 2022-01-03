using Paymun.Core.Models.Enum;

namespace Paymun.Core.Models {

    public class PaymentVerifyRequest {

        public PaymentVerifyRequest() { }

        public long Amount { get; set; }
        public string MerchantId { get; set; }
        public long OrderId { get; set; }
        public long ReferenceId { get; set; }
        public long TrackingNumber { get; set; }
    }
}
