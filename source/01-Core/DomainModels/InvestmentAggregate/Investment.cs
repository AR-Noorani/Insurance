using DomainModels.CoverageAggregate.Entities;
using DomainModels.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace DomainModels.InsuranceAggregate.Entities
{
    public sealed class Investment
    {
        private Investment()
        { }

        public static Investment Create(long id, string title, decimal value, IEnumerable<Coverage> coverages)
        {
            var investment = Load(id, title, value, coverages);
            investment.ValidateInvariants();
            return investment;
        }

        public static Investment Load(long id, string title, decimal value, IEnumerable<Coverage> coverages)
        {
            var investment = new Investment
            {
                Id = id,
                Title = title,
                Value = value,
                Coverages = coverages?.ToHashSet() ?? new HashSet<Coverage>(),
            };

            return investment;
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public decimal Value { get; private set; }
        public HashSet<Coverage> Coverages { get; private set; }

        public decimal NetValue => Coverages.Sum(x => x.NetPercent * Value);

        private void ValidateInvariants()
        {
            if (Id <= 0)
            {
                throw new MyApplicationException(Messages.IdIsRequired, ExceptionStatus.Internal);
            }

            if (string.IsNullOrWhiteSpace(Title))
            {
                throw new MyApplicationException(Messages.TitleIsRequired, ExceptionStatus.InvalidArgument);
            }

            if (Value <= 0)
            {
                throw new MyApplicationException(Messages.ValueIsInvalid, ExceptionStatus.InvalidArgument);
            }

            if (!Coverages.Any())
            {
                throw new MyApplicationException(Messages.AtleastOneCoverageNeeded, ExceptionStatus.InvalidArgument);
            }

            var minimumAllowedInvestment = Coverages.Min(x => x.MinimumAllowedInvestment);
            var maximumAllowedInvestment = Coverages.Max(x => x.MaximumAllowedInvestment);
            if (Value < minimumAllowedInvestment || Value > maximumAllowedInvestment)
            {
                throw new MyApplicationException(Messages.BasedOnCoverageMinimumAndMaximumValueIsInvalid, ExceptionStatus.InvalidArgument);
            }
        }
    }
}
