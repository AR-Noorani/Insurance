using DomainModels;
using DomainModels.CoverageAggregate.Entities;
using DomainModels.Utilities;
using Moq;
using Xunit;

namespace UnitTests
{
    public class CoverageUnitTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Coverage_WhenIdNotSet_ThrowException(long id)
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Coverage.Create(id, It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()));
            Assert.Equal(Messages.IdIsRequired, ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Coverage_WhenTitleSetWithNullOrEmptyOrWhitespace_ThrowException(string title)
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Coverage.Create(1, title, It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>()));
            Assert.Equal(Messages.TitleIsRequired, ex.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Coverage_WhenMinimumAllowedInvestmentIsInvalid_ThrowException(decimal minimumAllowedInvestment)
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Coverage.Create(1, "Test", minimumAllowedInvestment, It.IsAny<decimal>(), It.IsAny<decimal>()));
            Assert.Equal(Messages.MinimumAllowedInvestmentIsInvalid, ex.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Coverage_WhenMaximumAllowedInvestmentIsInvalid_ThrowException(decimal maximumAllowedInvestment)
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Coverage.Create(1, "Test", 1, maximumAllowedInvestment, It.IsAny<decimal>()));
            Assert.Equal(Messages.MaximumAllowedInvestmentIsInvalid, ex.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Coverage_WhenNetPercentIsInvalid_ThrowException(decimal netPercent)
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Coverage.Create(1, "Test", 1, 1, netPercent));
            Assert.Equal(Messages.NetPercentIsInvalid, ex.Message);
        }

        [Theory]
        [InlineData(2, 1)]
        public void Coverage_WhenMinimumAllowedInvestmentIsGreaterThanMaximumAllowedInvestment_ThrowException(decimal minimumAllowedInvestment, decimal maximumAllowedInvestment)
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Coverage.Create(1, "Test", minimumAllowedInvestment, maximumAllowedInvestment, 1));
            Assert.Equal(Messages.MinimumAllowedInvestmentAndMaximumAllowedInvestmentAreInvalid, ex.Message);
        }
    }
}
