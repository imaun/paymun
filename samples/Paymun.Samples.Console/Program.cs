using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paymun.Core.Models;
using Paymun.Gateway.Zarinpal;
using Paymun.Gateway.Mellat;
using System.IO;

namespace Paymun.Samples.Console
{
    class Program
    {
        const string _MERCHANT_ID = "ab6dc267-9904-4b9c-8fe0-a6fa78800756";

        static Task Main(string[] args) {
            //Console.WriteLine("Hello World!");
            using IHost host = CreateHostBuilder(args).Build();

            var mellatPay = getMellatPayment(host.Services);

            System.Console.WriteLine($"Mellat link created.");

            System.Console.WriteLine($"PageUrl : {mellatPay.PaymentPageUrl}");
            System.Console.WriteLine($"Status : {mellatPay.Status.ToString()}");
            System.Console.WriteLine($"Message : {mellatPay.Message}");

            using var writer = new StreamWriter("message.txt", true);
            writer.WriteLine(mellatPay.Message);
            writer.Flush();
            writer.Close();
            
            return host.RunAsync();
        }

        static PaymentRequestResult getPaymentResult(IServiceProvider services) {
            using IServiceScope scope = services.CreateScope();
            IServiceProvider provider = scope.ServiceProvider;

            IZarinpalGateway zarinpal = provider.GetRequiredService<IZarinpalGateway>();
            //zarinpal.EnableSandboxMode();
            var request = new PaymentRequest("http://localhost:31474/Verify") {
                Amount = 30000,
                Description = "Filan",
                Email = "myemail@gmail.com",
                Mobile = "+989121234567"
            };
            zarinpal.SetAuthority("varzeshafarinan.com");
            
            var result = zarinpal.CreatePaymentAsync(request).Result;
            
            return result;
        }

        static PaymentRequestResult getMellatPayment(IServiceProvider services) {
            using IServiceScope scope = services.CreateScope();
            IServiceProvider provider = scope.ServiceProvider;

            IMellatGateway mellat = provider.GetRequiredService<IMellatGateway>();
            var request = new PaymentRequest
            {
                Amount = 200000,
                CallbackUrl = "http://sample.com",
                Email = "dd@gmail.com",
                Mobile = "989121234567",
                OrderId = 123233,
                TrackingNumber = 1234232,
                Description = "Test"
            };

            var result = mellat.CreatePaymentAsync(request).GetAwaiter().GetResult();

            return result;
        }

        static PaymentVerifyResult getVerifyResult(IServiceProvider services, string authority) {
            using IServiceScope scope = services.CreateScope();
            IServiceProvider provider = scope.ServiceProvider;

            IZarinpalGateway zarinpal = provider.GetRequiredService<IZarinpalGateway>();
            var request = new PaymentVerifyRequest {
                Amount = 30000
            };
            //zarinpal.EnableSandboxMode();
            zarinpal.SetAuthority(authority);

            var result = zarinpal.VerifyPaymentAsync(request).Result;

            return result;
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
                services.AddZarinpalServices(merchantId: _MERCHANT_ID)
                        .AddMellatPaymentGateway(_=> {
                            _.Name = "Mellat";
                            _.UserName = "userName";
                            _.Password = "******";
                            _.TerminalId = 100000;
                        })
            );

    }
}
