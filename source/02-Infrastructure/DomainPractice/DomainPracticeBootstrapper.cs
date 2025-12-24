using DomainPractice.Configs;
using DomainPractice.DomainServices;
using DomainPractice.Utilities;
using DomainServices.Logics;
using DomainServices.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace DomainPractice
{
    public static class DomainPracticeBootstrapper
    {
        public static IServiceCollection AddDomainPracticeServices(this IServiceCollection services, Action<DomainPracticeConfig> config)
        {
            var newConfig = new DomainPracticeConfig();
            config.Invoke(newConfig);

            services.AddDbContextPool<AppDbContext>(builder =>
            {
                builder.UseSqlServer(newConfig.ConnectionString, options =>
                {
                    options.MigrationsHistoryTable("EfMigrationHistory", "dbo");
                    options.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
            });

            services.TryAddScoped<IUnitOfWork, EfUnitOfWork<AppDbContext>>();

            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<ICoverageService, CoverageService>();

            services.AddSnowflakeService(cfg =>
            {
                cfg.GeneratorId = newConfig.SnowflakeGeneratorId;
                cfg.Epoch = new DateTime(2022, 9, 18, 0, 0, 0, DateTimeKind.Utc);
                cfg.IdStructure = (43, 6, 14);
            });

            return services;
        }

        private static void AddSnowflakeService(this IServiceCollection services, Action<SnowflakeOptions> config = default!)
        {
            var options = new SnowflakeOptions();
            config?.Invoke(options);
            services.TryAddSingleton<ISnowflakeService>(p => new SnowflakeService(options));
        }

        public static IApplicationBuilder UseDomainPractice(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            try
            {
                using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<IStartup>>();
                logger.LogWarning(ex, ex.Message);
            }
            return app;
        }
    }
}
