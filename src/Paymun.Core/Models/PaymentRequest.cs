using System;
using Paymun.Core.Exceptions;
using System.Collections.Generic;

namespace Paymun.Core.Models {
    public class PaymentRequest {

        public PaymentRequest() {
            AdditionalData = new Dictionary<string, object>();
        }

        public PaymentRequest(string callbackUrl) {
            AdditionalData = new Dictionary<string, object>();
            CallbackUrl = callbackUrl;
        }

        #region Properties
        public string MerchantId { get; set; }

        public long TrackingNumber { get; set; }

        private string _callbackUrl;
        public string CallbackUrl {
            get => _callbackUrl;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(CallbackUrl));

                if (!Uri.TryCreate(value, UriKind.Absolute, out _))
                    throw new CallbackUrlNotValidException(value);

                _callbackUrl = value;
            }
        }

        public long Amount { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Description { get; set; }

        public IDictionary<string, object> AdditionalData { get; set; }

        #endregion
    }
}
