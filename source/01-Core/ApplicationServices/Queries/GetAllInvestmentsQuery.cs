using DomainServices.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ApplicationServices.Queries
{
    public sealed class GetAllInvestmentsQuery : IRequest<IEnumerable<InvestmentDto>>
    {
    }
}
