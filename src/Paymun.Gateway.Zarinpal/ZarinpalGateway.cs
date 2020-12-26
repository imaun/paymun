using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Paymun.Core;
using Paymun.Core.Models;
using Paymun.Gateway.Zarinpal.Internal;

namespace Paymun.Gateway.Zarinpal {

    public class ZarinpalGateway {

        //private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpRestClient _httpClient;
        private ZarinpalApiHelper _apiHelper;

        public string MerchantId { get; set; }

        public ZarinpalGateway(
            //IHttpContextAccessor httpContextAccessor,
            HttpRestClient httpClient,
            string merchantId) {
            //_httpContextAccessor = httpContextAccessor 
                //?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            MerchantId = merchantId;
        }

        public async Task<PaymentRequestResult> CreatePaymentRequestAsync(
            PaymentRequest request, 
            bool sandbox = false) 
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            request.MerchantId = MerchantId;

            _apiHelper = new ZarinpalApiHelper(sandbox);

            var result = await _httpClient.PostAsync<PaymentRequest, PaymentRequestResult>(
                request,
                _apiHelper.GetPaymentRequestUrl(withExtra: false));

            result.PaymentPageUrl = _apiHelper.GetPaymentGatewayPageUrl(result.Authority);

            return await Task.FromResult(result);
        }
    }
}
