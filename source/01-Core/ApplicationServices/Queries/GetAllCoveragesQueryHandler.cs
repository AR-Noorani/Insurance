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
    internal sealed class GetAllCoveragesQueryHandler : IRequestHandler<GetAllCoveragesQuery, IEnumerable<CoverageDto>>
    {
        private readonly ICoverageService _coverageService;
        private readonly ILogger<GetAllCoveragesQueryHandler> _logger;

        public GetAllCoveragesQueryHandler(ICoverageService coverageService, ILogger<GetAllCoveragesQueryHandler> logger)
        {
            _coverageService = coverageService;
            _logger = logger;
        }

        async Task<IEnumerable<CoverageDto>> IRequestHandler<GetAllCoveragesQuery, IEnumerable<CoverageDto>>
            .Handle(GetAllCoveragesQuery rq, CancellationToken ct)
        {
            var stopwatch = Stopwatch.StartNew();

            var coverages = await _coverageService.GetCoverages(ct);

            _logger.LogInformation("All Coverages Fetched. {ElapsedTime}", TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds));

            return coverages;
        }
    }
}