using System;
using Moq;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
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