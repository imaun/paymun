using System;
using Paymun.Gateway.Mellat;

namespace Microsoft.Extensions.DependencyInjection {

    public static class MellatBuilderExtensions {

        /// <summary>
        /// Adds Payment services for Mellat Bank
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddMellatPaymentGateway(
            this IServiceCollection services,
            Action<MellatGatewayOptions> options) {

            services.Configure(options);
            services.AddTransient<IMellatGateway, MellatGateway>();

            return services;
        }


        public static IServiceCollection AddMellatPaymentGateway(
            this IServiceCollection services,
            MellatGatewayOptions options) {

            services.AddTransient<IMellatGateway>(_=> new MellatGateway(options));

            return services;
        }

    }
}
