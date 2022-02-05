using System;
using Paymun.Core.Models;
using Paymun.Core.Models.Enum;
using Paymun.Core.Extensions;

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
        public static MellatPaymentResult GetMellatPaymentRequestResult(this string result) {
            result.CheckIsNullOrEmpty();
            
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static MellatVerifyPaymentResult GetMellatVerifyPaymentResult(this string result) {
            result.CheckIsNullOrEmpty();

            bool success = result == _OK_Code;
            string msg = "";
            if(!success) {
                msg = MellatGatewayTranslator.Translate(result);
            }

            return new MellatVerifyPaymentResult {
                Message = msg,
                ResCode = result
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static PaymentRequestResult ToPaymentResult(this MellatPaymentResult input) {
            var result = new PaymentRequestResult {
                PaymentPageUrl = PaymentPageUrl,
                Message = input.Message,
                Token = input.RefId
            };

            if (input.Success)
                result.Status = PaymentRequestStatus.Succeeded;

            return result;
        }


        public static PaymentVerifyResult ToPaymentVerifyResult(this MellatVerifyPaymentResult input) {
            input.CheckArgumentIsNull(nameof(input));

            var result = new PaymentVerifyResult();
            if (input.Success)
                result.Status = PaymentVerifiyStatus.Succeeded;

            if(input.Message.IsNotNull())
                result.Message = input.Message;

            return result;
        }

    }
}
