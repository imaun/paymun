using System;

namespace Paymun.Core.Exceptions {
    public class CallbackUrlNotValidException: Exception { 

        public CallbackUrlNotValidException(string url):
            base(message: $"The url : '{url}' is not valid.") {

        }
    }
}
