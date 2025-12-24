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
    public class CoverageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CoverageController> _logger;

        public CoverageController(IMediator mediator, ILogger<CoverageController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCoverages(CancellationToken ct = default)
        {
            var query = new GetAllCoveragesQuery();
            var result = await _mediator.Send(query, ct);
            return result.ToActionResult();
        }
    }
}
