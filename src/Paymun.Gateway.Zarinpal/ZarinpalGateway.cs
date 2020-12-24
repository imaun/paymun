using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;

namespace Paymun.Gateway.Zarinpal {

    public class ZarinpalGateway {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public ZarinpalGateway(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory) {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            if (httpContextAccessor == null)
                throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpContextAccessor = httpContextAccessor;
        }


    }
}
