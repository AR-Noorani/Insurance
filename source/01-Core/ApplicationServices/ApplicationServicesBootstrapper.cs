using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApplicationServices
{
    public static class ApplicationServicesBootstrapper
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            return services;
        }

        public static IApplicationBuilder UseApplicationServices(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
