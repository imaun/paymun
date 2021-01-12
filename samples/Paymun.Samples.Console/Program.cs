using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paymun.Core.Models;
using Paymun.Gateway.Zarinpal;

namespace Paymun.Samples.Console
{
    class Program
    {
        const string _MERCHANT_ID = "YOUR_API_KEY";

        static Task Main(string[] args) {
            //Console.WriteLine("Hello World!");
            using IHost host = CreateHostBuilder(args).Build();

            var payment = getPaymentResult(host.Services);

            return host.RunAsync();
        }

        static PaymentRequestResult getPaymentResult(IServiceProvider services) {
            using IServiceScope scope = services.CreateScope();
            IServiceProvider provider = scope.ServiceProvider;

            IZarinpalGateway zarinpal = provider.GetRequiredService<IZarinpalGateway>();
            var request = new PaymentRequest("http://localhost:31474/Verify") {
                Amount = 2507000,
                Description = "Filan",
                Email = "myemail@gmail.com",
                Mobile = "+989121234567"
            };
            zarinpal.SetAuthority("varzeshafarinan.com");
            
            var result = zarinpal.CreatePaymentAsync(request).Result;

            return result;
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
                services.AddZarinpalServices(merchantId: _MERCHANT_ID));

    }
}
