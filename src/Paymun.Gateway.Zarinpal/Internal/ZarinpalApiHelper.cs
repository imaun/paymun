namespace Paymun.Gateway.Zarinpal.Internal {
    internal static class ZarinpalApiHelper {

        #region Constants
        private const string _PAYMENT_REQUEST__URL = "https://{0}.zarinpal.com/pg/rest/WebGate/PaymentRequest.json";
        private const string _PAYMENT_REQUEST_WithExtra_URL = "https://{0}.zarinpal.com/pg/rest/WebGate/PaymentRequest{1}.json";
        private const string _PAYMENT_VERIFY_URL = "https://{0}.zarinpal.com/pg/rest/WebGate/PaymentVerification{1}.json";
        private const string _PAYMENT_GATEWAY_URL = "https://{0}.zarinpal.com/pg/StartPay/{1}/ZarinGate";
        private const string _SANDBOX = "sandbox";
        private const string _WWW = "www";
        private const string _WITH_EXTRA = "WithExtra";
        public const int _Success_Status_Code = 100;
        public const int _ALREADY_OK_CODE = 101;
        public const string _SUCCESS_STR_CODE = "OK";
        #endregion

        #region Methods
        public static string GetPaymentRequestUrl(bool sandbox, bool withExtra) =>
            string.Format(
                _PAYMENT_REQUEST_WithExtra_URL,
                (sandbox ? _SANDBOX : _WWW),
                (withExtra ? _WITH_EXTRA : string.Empty)
            );

        public static string GetPaymentRequestUrl(bool sandbox = false) =>
            string.Format(
                _PAYMENT_REQUEST__URL,
                (sandbox ? _SANDBOX : _WWW));

        public static string GetVerifyRequestUrl(bool sandbox, bool withExtra) =>
            string.Format(
                _PAYMENT_VERIFY_URL,
                (sandbox ? _SANDBOX : _WWW),
                (withExtra ? _WITH_EXTRA : string.Empty)
            );

        public static string GetPaymentGatewayPageUrl(bool sandbox, string authority) => string.Format(
                _PAYMENT_GATEWAY_URL,
                (sandbox ? _SANDBOX : _WWW),
                authority
            );

        #endregion

    }
}
