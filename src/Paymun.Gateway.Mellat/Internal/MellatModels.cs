using System;
using System.Collections.Generic;
using System.Text;

namespace Paymun.Gateway.Mellat.Internal
{
    internal class MellatPaymentResult
    {
        public string RefId { get; set; }

        public string ResCode { get; set; }

        public bool Success => ResCode == MellatHelper._OK_Code;

        public string Message { get; set; }
    }
}
