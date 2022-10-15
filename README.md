# Paymun

### Add **Payment** services to your **ASP.NET CORE** web apps.

"Paymun" (in farsi : پیمون) is an online payment library for Iranian IPG (PSP & Banks)  providers and payment gateways like Zarinpal.

This library is currently under development and only support **Zarinpal** gateway and **Mellat (BehPardakht)** IPG, I needed them for one of my projects. I think I'm going to develop other IPG & payment gateway services as well :)

### How to use

- Download or clone the repo

- Build the soultion using VisualStudio or by running `dotnet build` command in VsCode or any terminal.

- Reference `Paymun.Core` in your project

#### How to use Zarinpal

- In your `Startup.cs` file, under `ConfigureServices` method add `services.AddZarinpalServices(merchantId: "your_merchant_id");` line. (Get MerchantID from Zarinpal.com)

Now you can use `ZarinpalGateway` class to Create or Verify payment requests with Zarinpal service.

#### How to use Mellat IPG

- In your `Startup.cs` file, under `ConfigureServices` method add :

 ``cs  
 services.AddMellatPaymentGateway(new MellatGatewayOptions
{
    Name = "Mellat",
    TerminalId = "TerminalId",
    Password = "password",
    TestTerminal = false,
    UserName = "username"
});``
