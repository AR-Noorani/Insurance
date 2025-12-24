using DomainPractice.Coverage;
using DomainServices.Dtos;
using DomainServices.Logics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainPractice.DomainServices
{
    internal sealed partial class CoverageService : ICoverageService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<CoverageService> _logger;

        public CoverageService(AppDbContext dbContext, ILogger<CoverageService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        async Task<IEnumerable<CoverageDto>> ICoverageService.GetCoverages(CancellationToken ct)
        {
            var data = await _dbContext.Coverages
                .AsNoTracking()
                .ToListAsync(ct);

            return data.Select(c => new CoverageDto
            {
                Id = c.Id,
                Title = c.Title,
                MinimumAllowedInvestment = c.MinimumAllowedInvestment,
                MaximumAllowedInvestment = c.MaximumAllowedInvestment,
                NetPercent = c.NetPercent,
            });
        }

        async Task ICoverageService.AddCoverage(DomainModels.CoverageAggregate.Entities.Coverage coverage, CancellationToken ct)
        {
            var coverageData = new CoverageData
            {
                Id = coverage.Id,
                Title = coverage.Title,
                MinimumAllowedInvestment = coverage.MinimumAllowedInvestment,
                MaximumAllowedInvestment = coverage.MaximumAllowedInvestment,
                NetPercent = coverage.NetPercent,
            };
            await _dbContext.Coverages.AddAsync(coverageData, ct);
        }

        async Task<bool> ICoverageService.CoveragesExist(long[] coverageIds, CancellationToken ct)
        {
            var count = await _dbContext.Coverages
                .AsNoTracking()
                .CountAsync(c => coverageIds.Distinct().Contains(c.Id), ct);
            return coverageIds.Length == count;
        }

        async Task<IEnumerable<CoverageDto>> ICoverageService.GetCoverages(long[] coverageIds, CancellationToken ct)
        {
            var data = await _dbContext.Coverages
                .Where(c => coverageIds.Contains(c.Id))
                .AsNoTracking()
                .ToListAsync(ct);

            return data.Select(c => new CoverageDto
            {
                Id = c.Id,
                Title = c.Title,
                MinimumAllowedInvestment = c.MinimumAllowedInvestment,
                MaximumAllowedInvestment = c.MaximumAllowedInvestment,
                NetPercent = c.NetPercent,
            });
        }

        async Task<bool> ICoverageService.IsThereAnyCoverage(CancellationToken ct)
        {
            return await _dbContext.Coverages.AnyAsync(ct);
        }
    }
}
