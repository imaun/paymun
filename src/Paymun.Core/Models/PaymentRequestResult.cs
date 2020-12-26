using System;
using System.Collections.Generic;
using System.Text;

namespace Paymun.Core.Models
{
    public class PaymentRequestResult
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string PaymentPageUrl { get; set; }
        public string Authority { get; set; }
    }
}
