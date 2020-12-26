using System;
using System.Threading.Tasks;
using Paymun.Core;
using Paymun.Core.Models;
using Paymun.Gateway.Zarinpal.Models;
using Paymun.Gateway.Zarinpal.Internal;

namespace Paymun.Gateway.Zarinpal {

    public class ZarinpalGateway {

        private readonly IHttpRestClient _httpClient;

        public string MerchantId { get; set; }

        public ZarinpalGateway(
            IHttpRestClient httpClient,
            string merchantId) {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            MerchantId = merchantId;
        }

        public async Task<PaymentRequestResult> CreatePaymentRequestAsync(
            PaymentRequest request, 
            bool sandbox = false) 
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if(string.IsNullOrWhiteSpace(request.MerchantId))
                request.MerchantId = this.MerchantId;

            var zarinpalRequest = request.ToZarinpalRequest();

            var result = await _httpClient
                .PostAsync<ZarinpalPaymentRequest, ZarinpalPaymentRequestResult>(
                    zarinpalRequest,
                    ZarinpalApiHelper
                        .GetPaymentRequestUrl(sandbox, withExtra: false)
                    );

            return await Task.FromResult(result.ToResult());
        }
    }
}
