using Newtonsoft.Json;

namespace Paymun.Gateway.Zarinpal.Models {
    internal class ZarinpalPaymentVerifyRequest {
        public long Amount { get; set; }

        [JsonProperty("MerchantID")]
        public string MerchantId { get; set; }
        public string Authority { get; set; }
    }
}
