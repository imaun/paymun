namespace Paymun.Core.Models
{
    /// <summary>
    /// Class to keep Gateway options such as MerchantId, Username, Password, etc...
    /// </summary>
    public abstract class GatewayOptions
    {
        public string Name { get; set; }
    }
}
