using CharacterGen.Selectors;
using CharacterGen.Selectors.Domain;
using Moq;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Selectors
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
            mockInnerSelector.Setup(s => s.SelectFrom("table name")).Returns(Boolean.TrueString);
            var value = selector.SelectFrom("table name");
            Assert.That(value, Is.True);
        }

        [Test]
        public void SelectFalseValue()
        {
            mockInnerSelector.Setup(s => s.SelectFrom("table name")).Returns(Boolean.FalseString);
            var value = selector.SelectFrom("table name");
            Assert.That(value, Is.False);
        }
    }
}