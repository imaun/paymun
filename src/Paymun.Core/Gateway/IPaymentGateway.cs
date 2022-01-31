using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Paymun.Core.Models;

namespace Paymun.Gateway.Core {
    public interface IPaymentGateway {
        string MerchantId { get; set; }
        Task<PaymentRequestResult> CreatePaymentAsync(PaymentRequest request);
        Task<PaymentVerifyResult> VerifyPaymentAsync(PaymentVerifyRequest request);

        GatewayCallbackResult GetCallbackResult(IFormCollection collection);
    }
}
