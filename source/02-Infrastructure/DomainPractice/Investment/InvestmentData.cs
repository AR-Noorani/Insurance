using DomainPractice.Coverage;
using System.Collections.Generic;

namespace DomainPractice.Investment
{
    internal sealed class InvestmentData
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }

        public ICollection<CoverageData> Coverages { get; set; } = new List<CoverageData>();
    }
}
