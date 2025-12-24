using DomainPractice.Investment;
using System.Collections.Generic;

namespace DomainPractice.Coverage
{
    internal sealed class CoverageData
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal MinimumAllowedInvestment { get; set; }
        public decimal MaximumAllowedInvestment { get; set; }
        public decimal NetPercent { get; set; }

        public ICollection<InvestmentData> Investments { get; set; } = new List<InvestmentData>();
    }
}
