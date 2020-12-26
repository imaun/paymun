using System;
using System.Collections.Generic;
using System.Text;
using Paymun.Core.Models;
using Paymun.Core.Models.Enum;
using Paymun.Gateway.Zarinpal.Internal;
using Paymun.Gateway.Zarinpal.Models;

namespace Paymun.Gateway.Zarinpal.Internal {
    internal static class Extensions {

        public static ZarinpalPaymentRequest ToZarinpalRequest(this PaymentRequest model) =>
            new ZarinpalPaymentRequest {
                AdditionalData = model.AdditionalData,
                Amount = model.Amount,
                CallbackUrl = model.CallbackUrl,
                Description = model.Description,
                Email = model.Email,
                MerchantId = model.MerchantId,
                Mobile = model.Mobile,
                TrackingNumber = model.TrackingNumber
            };

        public static PaymentRequestResult ToResult(this ZarinpalPaymentRequestResult model, bool sandbox = false) =>
            new PaymentRequestResult {
                Message = model.Message,
                PaymentPageUrl = ZarinpalApiHelper.GetPaymentGatewayPageUrl(sandbox, model.Authority),
                StatusCode = model.Status,
                Status = model.Succeded 
                    ? PaymentRequestStatus.Succeeded 
                    : PaymentRequestStatus.Failed
            };

        
    }
}
