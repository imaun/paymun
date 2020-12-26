using Paymun.Gateway.Zarinpal.Internal;

namespace Paymun.Gateway.Zarinpal.Models {
    internal class ZarinpalPaymentRequestResult {
        public int Status { get; set; }
        public string Message { get; set; }
        public string PaymentPageUrl { get; set; }
        public string Authority { get; set; }
        public bool Succeded => Status == ZarinpalApiHelper._Success_Status_Code;
    }
}
