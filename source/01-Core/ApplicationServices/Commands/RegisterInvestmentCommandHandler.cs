using DomainModels.CoverageAggregate.Entities;
using DomainModels.InsuranceAggregate.Entities;
using DomainModels.Utilities;
using DomainServices.Logics;
using DomainServices.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Commands
{
    internal sealed class RegisterInvestmentCommandHandler : IRequestHandler<RegisterInvestmentCommand, long>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISnowflakeService _snowflakeService;
        private readonly IInvestmentService _investmentService;
        private readonly ICoverageService _coverageService;
        private readonly ILogger<RegisterInvestmentCommand> _logger;

        public RegisterInvestmentCommandHandler(IUnitOfWork unitOfWork, ISnowflakeService snowflakeService, IInvestmentService investmentService, ICoverageService coverageService, ILogger<RegisterInvestmentCommand> logger)
        {
            _unitOfWork = unitOfWork;
            _snowflakeService = snowflakeService;
            _investmentService = investmentService;
            _coverageService = coverageService;
            _logger = logger;
        }

        async Task<long> IRequestHandler<RegisterInvestmentCommand, long>
            .Handle(RegisterInvestmentCommand rq, CancellationToken ct)
        {
            var stopwatch = Stopwatch.StartNew();

            if (!await _coverageService.CoveragesExist(rq.CoverageIds, ct))
            {
                throw new MyApplicationException("پوشش انتخاب شده در سیستم تعریف نشده است", ExceptionStatus.InvalidArgument);
            }

            var coverages = await GetCoverages(rq, ct);
            var investmentId = _snowflakeService.CreateId();
            var investment = Investment.Create(investmentId, rq.Title, rq.Value, coverages);
            await _investmentService.RegisterInvestment(investment, ct);

            await _unitOfWork.SaveChanges(ct);

            _logger.LogInformation("Investment Registered. {InvestmentId} {ElapsedTime}", investmentId, TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds));

            return investmentId;
        }

        private async Task<System.Collections.Generic.IEnumerable<Coverage>> GetCoverages(RegisterInvestmentCommand rq, CancellationToken ct)
        {
            var coverageDtos = await _coverageService.GetCoverages(rq.CoverageIds, ct);
            var coverages = coverageDtos.Select(c => Coverage.Load(c.Id, c.Title, c.MinimumAllowedInvestment, c.MaximumAllowedInvestment, c.NetPercent));
            return coverages;
        }
    }
}