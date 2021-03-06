﻿using Newtonsoft.Json;
using Paymun.Gateway.Zarinpal.Internal;

namespace Paymun.Gateway.Zarinpal.Models {

    public class ZarinpalPaymentVerifyResult {

        [JsonProperty("RefID")]
        public long? ReferenceId { get; set; }
        public int Status { get; set; }
        public bool Succeded => Status == ZarinpalApiHelper._Success_Status_Code;
        public ZarinpalPaymentVerifyExtraDetail ExtraDetail { get; set; }
    }

    public class ZarinpalPaymentVerifyExtraDetail { 
        public ZarinpalPaymentVerifyTransaction Transaction { get; set; }
    }

    public class ZarinpalPaymentVerifyTransaction { 
        public string CardPanHash { get; set; }
        public string CardPanMask { get; set; }
    }
}
