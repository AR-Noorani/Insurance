using DomainPractice.Investment;
using DomainServices.Dtos;
using DomainServices.Logics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainPractice.DomainServices
{
    internal sealed partial class InvestmentService : IInvestmentService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<InvestmentService> _logger;

        public InvestmentService(AppDbContext dbContext, ILogger<InvestmentService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        async Task<IEnumerable<InvestmentDto>> IInvestmentService.GetInvestments(CancellationToken ct)
        {
            var data = await _dbContext.Investments
                .Include(i => i.Coverages)
                .AsNoTracking()
                .ToListAsync(ct);

            var model = data.Select(i =>
            DomainModels.InsuranceAggregate.Entities.Investment.Load(i.Id, i.Title, i.Value,
                i.Coverages.Select(c =>
                    DomainModels.CoverageAggregate.Entities.Coverage.Load(c.Id, c.Title, c.MinimumAllowedInvestment, c.MaximumAllowedInvestment, c.NetPercent))));

            return model.Select(i => new InvestmentDto
            {
                Id = i.Id,
                Title = i.Title,
                Value = i.Value,
                NetValue = i.NetValue,
                Coverages = i.Coverages.Select(c => new CoverageDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    MinimumAllowedInvestment = c.MinimumAllowedInvestment,
                    MaximumAllowedInvestment = c.MaximumAllowedInvestment,
                    NetPercent = c.NetPercent,
                })
            });
        }

        async Task IInvestmentService.RegisterInvestment(DomainModels.InsuranceAggregate.Entities.Investment investment, CancellationToken ct)
        {
            var coverages = _dbContext.Coverages.Where(c => investment.Coverages.Select(c => c.Id).ToArray().Contains(c.Id)).ToList();
            var data = new InvestmentData
            {
                Id = investment.Id,
                Title = investment.Title,
                Value = investment.Value,
                Coverages = coverages,
            };
            await _dbContext.Investments.AddAsync(data, ct);
        }
    }
}
