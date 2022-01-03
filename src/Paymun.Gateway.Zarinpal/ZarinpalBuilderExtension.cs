using Paymun.Gateway.Zarinpal;
using Paymun.Core;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ZarinpalBuilderExtension {

        /// <summary>
        /// Add Zarinpal payment services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public static IServiceCollection AddZarinpalServices(
            this IServiceCollection services,
            string merchantId) {

            services.AddHttpClient();
            services.AddSingleton<IHttpRestClient, HttpRestClient>();
            var restClient = services.BuildServiceProvider()
                .GetRequiredService<IHttpRestClient>();
            services.AddTransient<IZarinpalGateway>
                (_=> new ZarinpalGateway(restClient, merchantId));

            return services;
        }
    }
}
