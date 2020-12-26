﻿using System.Text.Json.Serialization;

namespace Paymun.Gateway.Zarinpal.Models {
    internal class ZarinpalPaymentVerificationRequest {
        public long Amount { get; set; }

        [JsonPropertyName(name: "MerchantID")]
        public string MerchantId { get; set; }

        public string Authority { get; set; }
    }
}
