using ApplicationServices.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace App.RestApi
{
    internal static class SeedData
    {
        public static async Task EnsureSeedData(this IApplicationBuilder app, CancellationToken ct = default)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await CreateCoverages(mediator, ct);
        }

        private static async Task CreateCoverages(IMediator mediator, CancellationToken ct)
        {
            var command = new SeedCoverageCommand();
            await mediator.Send(command, ct);
        }
    }

}
