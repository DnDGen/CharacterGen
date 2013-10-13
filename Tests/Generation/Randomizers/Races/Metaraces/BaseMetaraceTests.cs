using Moq;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class BaseMetaraceTests
    {
        private TestMetaraceRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("metarace");

            randomizer = new TestMetaraceRandomizer(mockPercentileResultProvider.Object);
            randomizer.SwitchAllowed = false;
            randomizer.MetaraceAllowed = true;
        }

        [Test]
        public void LoopUntilMetaraceIsAllowed()
        {
            randomizer.MetaraceAllowed = false;
            randomizer.SwitchAllowed = true;

            randomizer.Randomize(String.Empty, String.Empty);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void ReturnMetaraceFromPercentileResultProvider()
        {
            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        [Test]
        public void IfAllowNoMetaraceIsTrueThenEmptyMetaraceIsAllowed()
        {
            randomizer.AllowNoMetarace = true;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns(String.Empty);

            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void IfAllowNoMetaraceIsTrueThenNonEmptyMetaraceIsAllowed()
        {
            randomizer.AllowNoMetarace = true;

            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        [Test]
        public void IfAllowNoMetaraceIsFalseThenEmptyMetaraceIsNotAllowed()
        {
            randomizer.AllowNoMetarace = false;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns(String.Empty).Returns("metarace");

            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        [Test]
        public void IfAllowNoMetaraceIsFalseThenNonEmptyMetaraceIsAllowed()
        {
            randomizer.AllowNoMetarace = false;

            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        [Test]
        public void AccessesTableAlignmentGoodnessClassNameMetaraces()
        {
            var result = randomizer.Randomize("goodness", "className");

            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("goodnessclassNameMetaraces"),
                Times.Once());
        }

        private class TestMetaraceRandomizer : BaseMetarace
        {
            public Boolean MetaraceAllowed { get; set; }
            public Boolean SwitchAllowed { get; set; }

            public TestMetaraceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

            protected override Boolean MetaraceIsAllowed(String metarace)
            {
                var toReturn = MetaraceAllowed;

                if (SwitchAllowed)
                    MetaraceAllowed = !MetaraceAllowed;

                return toReturn;
            }
        }
    }
}