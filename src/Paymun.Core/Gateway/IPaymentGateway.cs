using System.Threading.Tasks;
using Paymun.Core.Models;

namespace Paymun.Gateway.Core {
    public interface IPaymentGateway {
        string MerchantId { get; set; }
        Task<PaymentRequestResult> CreatePaymentAsync(PaymentRequest request);
        Task<PaymentVerifyResult> VerifyPaymentAsync(PaymentVerifyRequest request);
    }
}
