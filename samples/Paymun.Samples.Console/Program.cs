﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paymun.Core.Models;
using Paymun.Gateway.Zarinpal;

namespace Paymun.Samples.Console
{
    class Program
    {
        const string _MERCHANT_ID = "ab6dc267-9904-4b9c-8fe0-a6fa78800756";

        static Task Main(string[] args) {
            //Console.WriteLine("Hello World!");
            using IHost host = CreateHostBuilder(args).Build();

            var payment = getPaymentResult(host.Services);

            System.Console.WriteLine($"Payment link created: {payment.PaymentPageUrl}");
            System.Console.WriteLine("Enter verify when you pay the link.");

            var command = System.Console.ReadLine();

            if(command.ToUpper() == "VERIFY") {
                System.Console.WriteLine("Enter Authority:");
                var authority = System.Console.ReadLine();
                var verify = getVerifyResult(host.Services, authority);
                System.Console.WriteLine($"RefId: {verify.ReferenceId}");
                System.Console.WriteLine($"Status: {verify.StatusCode}");
                System.Console.WriteLine($"Msg: {verify.Message}");
            }

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
                services.AddZarinpalServices(merchantId: _MERCHANT_ID));

    }
}
