using App.RestApi.ApiResponse;
using ApplicationServices.Commands;
using ApplicationServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace App.RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InvestmentController> _logger;

        public InvestmentController(IMediator mediator, ILogger<InvestmentController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvestments(CancellationToken ct = default)
        {
            var query = new GetAllInvestmentsQuery();
            var result = await _mediator.Send(query, ct);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterInvestment(InvestmentInputModel input, CancellationToken ct = default)
        {
            var command = new RegisterInvestmentCommand
            {
                Title = input.Title,
                Value = input.Value,
                CoverageIds = input.CoverageIds,
            };
            var result = await _mediator.Send(command, ct);
            return result.ToActionResult();
        }
    }
}
