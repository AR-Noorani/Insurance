using DomainServices.Dtos;
using DomainServices.Logics;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Queries
{
    internal sealed class GetAllInvestmentsQueryHandler : IRequestHandler<GetAllInvestmentsQuery, IEnumerable<InvestmentDto>>
    {
        private readonly IInvestmentService _investmentService;
        private readonly ILogger<GetAllInvestmentsQueryHandler> _logger;

        public GetAllInvestmentsQueryHandler(IInvestmentService investmentService, ILogger<GetAllInvestmentsQueryHandler> logger)
        {
            _investmentService = investmentService;
            _logger = logger;
        }

        async Task<IEnumerable<InvestmentDto>> IRequestHandler<GetAllInvestmentsQuery, IEnumerable<InvestmentDto>>
            .Handle(GetAllInvestmentsQuery rq, CancellationToken ct)
        {
            var stopwatch = Stopwatch.StartNew();

            var investments = await _investmentService.GetInvestments(ct);

            _logger.LogInformation("All Investments Fetched. {ElapsedTime}", TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds));

            return investments;
        }
    }
}