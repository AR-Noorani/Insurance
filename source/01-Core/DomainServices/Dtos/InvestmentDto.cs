using System.Collections.Generic;

namespace DomainServices.Dtos
{
    public sealed class InvestmentDto
    {
        public long Id { get; init; }
        public string Title { get; init; }
        public decimal Value { get; init; }
        public decimal NetValue { get; init; }
        public IEnumerable<CoverageDto> Coverages { get; init; }
    }
}
