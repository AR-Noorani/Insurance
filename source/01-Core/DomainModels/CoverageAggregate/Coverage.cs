using DomainModels.Utilities;

namespace DomainModels.CoverageAggregate.Entities
{
    public sealed class Coverage
    {
        private Coverage()
        { }

        public static Coverage Create(long id, string title, decimal minimumAllowedInvestment, decimal maximumAllowedInvestment, decimal netPercent)
        {
            var coverage = Load(id, title, minimumAllowedInvestment, maximumAllowedInvestment, netPercent);
            coverage.ValidateInvariants();
            return coverage;
        }

        public static Coverage Load(long id, string title, decimal minimumAllowedInvestment, decimal maximumAllowedInvestment, decimal netPercent)
        {
            var coverage = new Coverage
            {
                Id = id,
                Title = title,
                MinimumAllowedInvestment = minimumAllowedInvestment,
                MaximumAllowedInvestment = maximumAllowedInvestment,
                NetPercent = netPercent,
            };

            return coverage;
        }

        public long Id { get; private set; }
        public string Title { get; private set; }
        public decimal MinimumAllowedInvestment { get; private set; }
        public decimal MaximumAllowedInvestment { get; private set; }
        public decimal NetPercent { get; private set; }

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

            if (MinimumAllowedInvestment <= 0)
            {
                throw new MyApplicationException(Messages.MinimumAllowedInvestmentIsInvalid, ExceptionStatus.InvalidArgument);
            }

            if (MaximumAllowedInvestment <= 0)
            {
                throw new MyApplicationException(Messages.MaximumAllowedInvestmentIsInvalid, ExceptionStatus.InvalidArgument);
            }

            if (NetPercent <= 0)
            {
                throw new MyApplicationException(Messages.NetPercentIsInvalid, ExceptionStatus.InvalidArgument);
            }

            if (MinimumAllowedInvestment > MaximumAllowedInvestment)
            {
                throw new MyApplicationException(Messages.MinimumAllowedInvestmentAndMaximumAllowedInvestmentAreInvalid, ExceptionStatus.InvalidArgument);
            }
        }
    }
}
