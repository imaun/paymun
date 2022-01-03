using Paymun.Core.Models;

namespace Paymun.Gateway.Mellat {

    /// <summary>
    /// Options for Mellat payment gateway services
    /// </summary>
    public class MellatGatewayOptions : GatewayOptions {

        public long TerminalId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Determines that the Requests will sent to test url of Mellat service or not.
        /// </summary>
        public bool TestTerminal { get; set; }
    }

}
