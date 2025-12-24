using DomainModels.CoverageAggregate.Entities;
using DomainServices.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Logics
{
    public interface ICoverageService
    {
        Task<IEnumerable<CoverageDto>> GetCoverages(CancellationToken ct = default);
        Task AddCoverage(Coverage coverage, CancellationToken ct = default);
        Task<bool> CoveragesExist(long[] coverageIds, CancellationToken ct = default);
        Task<IEnumerable<CoverageDto>> GetCoverages(long[] coverageIds, CancellationToken ct = default);
        Task<bool> IsThereAnyCoverage(CancellationToken ct = default);
    }
}
