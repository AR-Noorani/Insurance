using DomainServices.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ApplicationServices.Queries
{
    public sealed class GetAllCoveragesQuery : IRequest<IEnumerable<CoverageDto>>
    {
    }
}
