using DomainModels.CoverageAggregate.Entities;
using DomainServices.Logics;
using DomainServices.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Commands
{
    internal sealed class SeedCoverageCommandHandler : IRequestHandler<SeedCoverageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISnowflakeService _snowflakeService;
        private readonly ICoverageService _coverageService;
        private readonly ILogger<RegisterInvestmentCommand> _logger;

        public SeedCoverageCommandHandler(IUnitOfWork unitOfWork, ISnowflakeService snowflakeService, ICoverageService coverageService, ILogger<RegisterInvestmentCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _snowflakeService = snowflakeService;
            _coverageService = coverageService;
            _logger = logger;
        }

        async Task IRequestHandler<SeedCoverageCommand>
            .Handle(SeedCoverageCommand rq, CancellationToken ct)
        {
            var stopwatch = Stopwatch.StartNew();

            var isThereAnyCoverage = await _coverageService.IsThereAnyCoverage(ct);
            if (isThereAnyCoverage)
            {
                return;
            }

            var seedData = GetCoverageToSeed();
            foreach (var data in seedData)
            {
                await _coverageService.AddCoverage(data, ct);
            }

            await _unitOfWork.SaveChanges(ct);

            _logger.LogInformation("Coverages Seeded. {ElapsedTime}", TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds));
        }

        private List<Coverage> GetCoverageToSeed()
        {
            var newId = _snowflakeService.CreateId();
            return new List<Coverage>
            {
                 Coverage.Create(newId++, "پوشش جراحی", 5000, 500000000, 0.0052m),
                 Coverage.Create(newId++, "پوشش دندانپزشکی", 4000, 400000000, 0.0042m),
                 Coverage.Create(newId++, "پوشش بستری", 2000, 200000000, 0.005m),
            };
        }
    }
}