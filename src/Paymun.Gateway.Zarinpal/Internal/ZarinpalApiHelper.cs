using System;
using System.Collections.Generic;
using System.Text;

namespace Paymun.Gateway.Zarinpal.Internal
{
    class ZarinpalApiHelper
    {
        public ZarinpalApiHelper(bool sandbox) {
            Sandbox = sandbox;
        }

        #region Constants
        private const string _PAYMENT_REQUEST_URL = "https://{0}.zarinpal.com/pg/rest/WebGate/PaymentRequest{1}.json";
        private const string _PAYMENT_VERIFY_URL = "https://{0}.zarinpal.com/pg/rest/WebGate/PaymentVerification{1}.json";
        private const string _PAYMENT_GATEWAY_URL = "https://{0}.zarinpal.com/pg/StartPay/{1}/ZarinGate";
        private const string _SANDBOX = "sandbox";
        private const string _WWW = "www";
        private const string _WITH_EXTRA = "WithExtra";
        #endregion

        #region Properties
        public bool Sandbox { get; set; }
        #endregion

        #region Methods
        public string GetPaymentRequestUrl(bool withExtra) =>
            string.Format(
                _PAYMENT_REQUEST_URL,
                (Sandbox ? _SANDBOX : _WWW),
                (withExtra ? _WITH_EXTRA : string.Empty)
            );

        public string GetVerifyRequestUrl(bool withExtra) =>
            string.Format(
                _PAYMENT_VERIFY_URL,
                (Sandbox ? _SANDBOX : _WWW),
                (withExtra ? _WITH_EXTRA : string.Empty)
            );

        public string GetPaymentGatewayPageUrl(string authority) => string.Format(
                _PAYMENT_GATEWAY_URL,
                (Sandbox ? _SANDBOX : _WWW),
                authority
            );


        #endregion

    }
}
