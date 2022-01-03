using System;
using System.Threading.Tasks;
using MellatBankPaymentService;
using Paymun.Core.Models;
using Paymun.Gateway.Core;

namespace Paymun.Gateway.Mellat {


    public class MellatGateway : Paymun.Gateway.Core.IPaymentGateway
    {

        private readonly PaymentGatewayClient _client = new PaymentGatewayClient();


        public MellatGateway() {

        }

        #region Properties
        public string MerchantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Username { get; set; }

        public string Password { get; set; }

        public long TerminalId => Convert.ToInt64(MerchantId);

        #endregion

        public async Task<PaymentRequestResult> CreatePaymentAsync(PaymentRequest request) {
            //var response = await _client.bpInquiryRequestAsync(
            //    Convert.ToInt64(MerchantId), 
            //    Username, 
            //    Password, 
            //    request.TrackingNumber, 
            //    request.OrderId, 
            //    0);

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

        }

        public async Task<PaymentVerifyResult> VerifyPaymentAsync(PaymentVerifyRequest request) {
            throw new NotImplementedException();
        }


        private string getLocalDate()
            => DateTime.Now.ToShortDateString();

        private string getLocalTime()
            => DateTime.Now.ToShortTimeString();

    }
}
