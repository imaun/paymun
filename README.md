# Paymun

### Add **Payment** services to your **ASP.NET CORE** web apps.

"Paymun" (in farsi : پیمون) is an online payment library for Iranian IPG (PSP & Banks)  providers and payment gateways like Zarinpal.

This library is currently under development and only support **Zarinpal** gateway, because I needed it for one of my projects. But in the near feature I'm going to develop other IPG & payment gateway services.

### How to use Zarinpal payment service

- Download or clone the repo

- Build the soultion using VisualStudio or by running `dotnet build` command in VsCode or any terminal.

- Reference `Pyamun.Core` & `Paymun.Core.Zarinpal` in your project

- In your `Startup.cs` file, under `ConfigureServices` method add `services.AddZarinpalServices(merchantId: "your_merchant_id");` line. (Get MerchantID from Zarinpal.com)

Now you can use `ZarinpalGateway` class to Create or Verify payment requests with Zarinpal service.
