using DomainModels.InsuranceAggregate.Entities;
using DomainServices.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Logics
{
    public interface IInvestmentService
    {
        Task<IEnumerable<InvestmentDto>> GetInvestments(CancellationToken ct = default);
        Task RegisterInvestment(Investment investment, CancellationToken ct = default);
    }
}
