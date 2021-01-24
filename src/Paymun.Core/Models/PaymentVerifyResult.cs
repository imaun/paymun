using Paymun.Core.Models.Enum;

namespace Paymun.Core.Models {
    public class PaymentVerifyResult {
        public PaymentVerifiyStatus Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ReferenceId { get; set; }
        public bool Succeeded => Status == PaymentVerifiyStatus.Succeeded;
    }
}
