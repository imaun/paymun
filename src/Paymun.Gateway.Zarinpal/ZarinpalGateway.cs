using System;
using System.Threading.Tasks;
using Paymun.Core;
using Paymun.Gateway.Core;
using Paymun.Core.Models;
using Paymun.Core.Extensions;
using Paymun.Gateway.Zarinpal.Models;
using Paymun.Gateway.Zarinpal.Internal;
using Microsoft.AspNetCore.Http;

namespace Paymun.Gateway.Zarinpal {

    public interface IZarinpalGateway : IPaymentGateway {
        bool SandboxMode { get; }
        string Authority { get; }
        void EnableSandboxMode();
        void SetAuthority(string authority);
    }

    public class ZarinpalGateway: IZarinpalGateway {

        private readonly IHttpRestClient _httpClient;

        public ZarinpalGateway(
            IHttpRestClient httpClient,
            string merchantId) {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            MerchantId = merchantId;
        }

        #region Zarinpal gateway constants
        private const string FIELD_STATUS = "Status";
        private const string FIELD_AUTHORITY = "Authority";

        #endregion

        #region Properties
        public string MerchantId { get; set; }
        public bool SandboxMode { get; private set; }
        public string Authority { get; private set; }

        #endregion

        #region Interface Methods
        public async Task<PaymentRequestResult> CreatePaymentAsync(
            PaymentRequest request) 
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.MerchantId))
                request.MerchantId = this.MerchantId;
            var zarinpalRequest = request.ToZarinpalRequest();

            var result = await _httpClient
                .PostAsync<ZarinpalPaymentRequest, ZarinpalPaymentRequestResult>(
                    zarinpalRequest,
                    ZarinpalApiHelper
                        .GetPaymentRequestUrl(SandboxMode)
                    );

            return await Task.FromResult(result.ToResult());
        }

        public async Task<PaymentVerifyResult> VerifyPaymentAsync(
            PaymentVerifyRequest request) 
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.MerchantId))
                request.MerchantId = this.MerchantId;

            var zarinpalRequest = request.ToZarinpalRequest(Authority);
            
            var result = await _httpClient
                .PostAsync<ZarinpalPaymentVerifyRequest, ZarinpalPaymentVerifyResult>(
                    zarinpalRequest,
                    ZarinpalApiHelper
                        .GetVerifyRequestUrl(SandboxMode, false)
                    );

            return await Task.FromResult(result.ToResult());
        }

        #endregion

        public void EnableSandboxMode() => SandboxMode = true;

        public void DisableSandboxMode() => SandboxMode = false;

        public void SetAuthority(string authority) =>
            Authority = authority;

        public GatewayCallbackResult GetCallbackResult(IFormCollection collection) {
            var result = new GatewayCallbackResult {
                StatusCode = collection.GetValue(FIELD_STATUS),
                BankToken = collection.GetValue(FIELD_AUTHORITY),
            };

            result.Success = result.StatusCode.Equals(
                ZarinpalApiHelper._SUCCESS_STR_CODE, 
                StringComparison.InvariantCultureIgnoreCase);

            result.Message = result.Success
                ? $"Status : {ZarinpalApiHelper._SUCCESS_STR_CODE}"
                : $"Error with status : {result.StatusCode}";

            return result;
        }
    }
}
