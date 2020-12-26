using Paymun.Gateway.Zarinpal;
using Paymun.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ZarinpalBuilderExtension
    {

        public static IServiceCollection AddZarinpalServices(
            this IServiceCollection services,
            string merchantId) {

            services.AddHttpClient();
            services.AddSingleton<IHttpRestClient, HttpRestClient>();
            var restClient = services.BuildServiceProvider().GetRequiredService<IHttpRestClient>();
            services.AddTransient(_=> new ZarinpalGateway(restClient, merchantId));

            return services;
        }
    }
}
