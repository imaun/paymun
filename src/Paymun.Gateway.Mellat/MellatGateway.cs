using System;
using System.Threading.Tasks;
using MellatBankPaymentService;
using Paymun.Core.Models;
using Paymun.Gateway.Core;
using Paymun.Core.Models.Enum;
using Paymun.Gateway.Mellat.Internal;
using Paymun.Core.Extensions;

namespace Paymun.Gateway.Mellat {
    
    public interface IMellatGateway : Paymun.Gateway.Core.IPaymentGateway
    {
        long TerminalId { get; }
        string Username { get; }
        string Password { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MellatGateway : IMellatGateway {

        private readonly PaymentGatewayClient _client = new PaymentGatewayClient();
        private readonly MellatGatewayOptions _gatewayOptions;

        public MellatGateway(
            long terminalId,
            string userName,
            string userPassword) {

            _gatewayOptions = new MellatGatewayOptions
            {
                TerminalId = terminalId,
                UserName = userName,
                Password = userPassword
            };
        }

        public MellatGateway(MellatGatewayOptions options) {
            _gatewayOptions = options ?? throw new ArgumentNullException(nameof(options));
        }

        #region Properties
        public string MerchantId { get; set; }

        public string Username => _gatewayOptions.UserName;

        public string Password => _gatewayOptions.Password;

        public long TerminalId {
            get {
                if (_gatewayOptions != null && _gatewayOptions.TerminalId > 0)
                    return _gatewayOptions.TerminalId;

                return Convert.ToInt64(MerchantId);
            }
        } 

        #endregion

        /// <inheritdoc/>
        public async Task<PaymentRequestResult> CreatePaymentAsync(PaymentRequest request) {
            request.CheckArgumentIsNull("[PaymentRequest] cannot be null.");

            var response = await _client.bpPayRequestAsync(
                terminalId: TerminalId, 
                userName: Username, 
                userPassword: Password, 
                orderId: request.TrackingNumber, 
                amount: request.Amount, 
                localDate: getLocalDate(),
                localTime: getLocalTime(), 
                additionalData: string.Empty, 
                callBackUrl: request.CallbackUrl, 
                payerId: string.Empty, 
                mobileNo: request.Mobile, 
                encPan: string.Empty, 
                panHiddenMode: string.Empty, 
                cartItem: string.Empty, 
                enc: string.Empty);


            var mellatResult = response.Body.@return.GetMellatPaymentRequestResult();

            return await Task.FromResult(
                mellatResult.ToPaymentResult()
            );
        }

        /// <inheritdoc/>
        public async Task<PaymentVerifyResult> VerifyPaymentAsync(PaymentVerifyRequest request) {
            request.CheckArgumentIsNull("[PaymentVerifyRequest] cannot be null.");

            var response = await _client.bpVerifyRequestAsync(
                terminalId: TerminalId,
                userName: Username,
                userPassword: Password,
                orderId: request.OrderId,
                saleOrderId: request.TrackingNumber,
                saleReferenceId: request.ReferenceId);

            var mellatResult = response.Body.@return.GetMellatVerifyPaymentResult();

            return await Task.FromResult(
                mellatResult.ToPaymentVerifyResult()
            );
        }


        private string getLocalDate()
            => DateTime.Now.ToShortDateString();

        private string getLocalTime()
            => DateTime.Now.ToShortTimeString();

    }
}
