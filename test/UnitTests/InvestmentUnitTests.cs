using DomainModels;
using DomainModels.CoverageAggregate.Entities;
using DomainModels.InsuranceAggregate.Entities;
using DomainModels.Utilities;
using Moq;
using Xunit;

namespace UnitTests
{
    public class InvestmentUnitTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Coverage_WhenIdNotSet_ThrowException(long id)
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Investment.Create(id, It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<Coverage[]>()));
            Assert.Equal(Messages.IdIsRequired, ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Coverage_WhenTitleSetWithNullOrEmptyOrWhitespace_ThrowException(string title)
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Investment.Create(1, title, It.IsAny<decimal>(), It.IsAny<Coverage[]>()));
            Assert.Equal(Messages.TitleIsRequired, ex.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Coverage_WhenValueIsInvalid_ThrowException(decimal value)
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Investment.Create(1, "Test", value, It.IsAny<Coverage[]>()));
            Assert.Equal(Messages.ValueIsInvalid, ex.Message);
        }

        [Fact]
        public void Coverage_WhenNotSpecifiedAtleastOneCoverage_ThrowException()
        {
            var ex = Assert.Throws<MyApplicationException>(() =>
                Investment.Create(1, "Test", 1, It.IsAny<Coverage[]>()));
            Assert.Equal(Messages.AtleastOneCoverageNeeded, ex.Message);
        }

        [Theory]
        [InlineData(1000, 5000, 100)]
        [InlineData(1000, 5000, 5100)]
        public void Coverage_WhenValueIsBasedOnCoverageMinimumAndMaximumInvalid_ThrowException(decimal coverageMin, decimal coverageMax, decimal value)
        {
            var coverage = Coverage.Load(It.IsAny<long>(), It.IsAny<string>(), coverageMin, coverageMax, It.IsAny<decimal>());
            var ex = Assert.Throws<MyApplicationException>(() =>
                Investment.Create(1, "Test", value, new[] { coverage }));
            Assert.Equal(Messages.BasedOnCoverageMinimumAndMaximumValueIsInvalid, ex.Message);
        }
    }
}
