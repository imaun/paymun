using System.Collections.Generic;
using Newtonsoft.Json;

namespace Paymun.Gateway.Zarinpal.Models {
    internal class ZarinpalPaymentRequest {

        [JsonProperty("MerchantID")]
        public string MerchantId { get; set; }

        public long TrackingNumber { get; set; }

        [JsonProperty("CallbackURL")]
        public string CallbackUrl {
            get; set;
        }

        public long Amount { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Description { get; set; }

        public IDictionary<string, object> AdditionalData { get; set; }
    }
}
