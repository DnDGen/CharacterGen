using CharacterGen.Domain.Selectors.Percentiles;
using Moq;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Selectors.Percentiles
{
    [TestFixture]
    public class BooleanPercentileSelectorTests
    {
        private IBooleanPercentileSelector selector;
        private Mock<IPercentileSelector> mockInnerSelector;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<IPercentileSelector>();
            selector = new BooleanPercentileSelector(mockInnerSelector.Object);
        }

        [Test]
        public void SelectTrueValue()
        {
            mockInnerSelector.Setup(s => s.SelectFrom("table name")).Returns(bool.TrueString);
            var value = selector.SelectFrom("table name");
            Assert.That(value, Is.True);
        }

        [Test]
        public void SelectFalseValue()
        {
            mockInnerSelector.Setup(s => s.SelectFrom("table name")).Returns(bool.FalseString);
            var value = selector.SelectFrom("table name");
            Assert.That(value, Is.False);
        }
    }
}