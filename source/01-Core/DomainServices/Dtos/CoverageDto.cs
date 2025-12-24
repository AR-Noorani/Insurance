namespace DomainServices.Dtos
{
    public sealed class CoverageDto
    {
        public long Id { get; init; }
        public string Title { get; init; }
        public decimal MinimumAllowedInvestment { get; init; }
        public decimal MaximumAllowedInvestment { get; init; }
        public decimal NetPercent { get; init; }
    }
}
