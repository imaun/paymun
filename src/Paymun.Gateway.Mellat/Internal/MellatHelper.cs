using System;
using Paymun.Core.Models;
using Paymun.Core.Models.Enum;

namespace Paymun.Gateway.Mellat.Internal {

    internal static class MellatHelper {

        public const string PaymentPageUrl = @"https://bpm.shaparak.ir/pgwchannel/startpay.mellat";

        public const string _OK_Code = "0";
        public const string _Duplicate_OrderNumber_Code = "41";
        public const string _Settle_Success_Code = "45";
        public const string _Already_Verified_Code = "43";

        /// <summary>
        /// Generate PaymentResult from string returned by Mellat payment service.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static MellatPaymentResult GetMellatResult(this string result) {
            if(result == null || result.Length == 0) 
                throw new ArgumentNullException(nameof(result));
            
            var myResult = result.Split(',');
            var refId = myResult.Length > 1 ? myResult[1] : null;
            var resCode = myResult[0];
            var msg = MellatGatewayTranslator.Translate(resCode);

            return new MellatPaymentResult { 
                RefId = refId,
                ResCode = resCode,
                Message = msg
            };
        }


        public static PaymentRequestResult ToPaymentResult(this MellatPaymentResult input) {
            var result = new PaymentRequestResult {
                PaymentPageUrl = PaymentPageUrl,
                Message = input.Message
            };

            if (input.Success)
                result.Status = PaymentRequestStatus.Succeeded;

            return result;
        }
    }
}
