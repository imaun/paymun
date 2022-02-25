using System;
using System.Linq;
using System.Threading.Tasks;
using MellatBankPaymentService;
using Paymun.Core.Models;
using Paymun.Core.Extensions;
using Paymun.Gateway.Mellat.Internal;
using Microsoft.AspNetCore.Http;

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

        #region Bank Mellat related constants

        private const string ALREADY_VERIFIED_CODE = "43";
        private const string OK_STATUS = "0";

        private const string FIELD_ResCode = "ResCode";
        private const string FIELD_RefId = "RefId";
        private const string FIELD_SaleOrderId = "SaleOrderId";
        private const string FIELD_SaleReferenceId = "SaleReferenceId";
        private const string FIELD_RRN = "RRN";
        private const string FIELD_CID = "CID";
        private const string FIELD_TRACENO = "TRACENO";
        private const string FIELD_SecurePAN = "CardHolderPan";

        #endregion

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
                orderId: request.OrderId, 
                amount: request.Amount, 
                localDate: getLocalDate(),
                localTime: getLocalTime(), 
                additionalData: string.Empty, 
                callBackUrl: request.CallbackUrl, 
                payerId: "0", 
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

        public GatewayCallbackResult GetCallbackResult(IFormCollection collection) 
        {
            return new GatewayCallbackResult
            {
                BankReferenceId = collection.GetValue(FIELD_SaleReferenceId),
                BankToken = collection.GetValue(FIELD_RefId),
                CID = collection.GetValue(FIELD_CID),
                OrderId = long.TryParse(collection.GetValue(FIELD_SaleOrderId), out var orderId) ? orderId : 0,
                RRN = collection.GetValue(FIELD_RRN),
                SecurePAN = collection.GetValue(FIELD_SecurePAN),
                StatusCode = collection.GetValue(FIELD_ResCode),
                TraceNo = collection.GetValue(FIELD_TRACENO)
            };
        }

        private string getLocalDate() => $"{DateTime.Now:yyyyMMdd}";

        private string getLocalTime() => $"{DateTime.Now:HHmmss}";

    }
}
