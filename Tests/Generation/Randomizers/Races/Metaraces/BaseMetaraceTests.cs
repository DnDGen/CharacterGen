using System;
using Moq;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

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
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("result");

            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo("result"));
        }

        [Test]
        public void IfNotForcedThenEmptyMetaraceIsAllowed()
        {
            randomizer.ForcedMetarace = false;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns(String.Empty);

            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void IfNotForcedThenNonEmptyMetaraceIsAllowed()
        {
            randomizer.ForcedMetarace = false;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("metarace");

            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        [Test]
        public void IfForcedThenEmptyMetaraceIsNotAllowed()
        {
            randomizer.ForcedMetarace = true;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns(String.Empty).Returns("metarace");

            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        [Test]
        public void IfForcedThenNonEmptyMetaraceIsAllowed()
        {
            randomizer.ForcedMetarace = true;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("metarace");

            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        private class TestMetaraceRandomizer : BaseMetarace
        {
            public Boolean MetaraceAllowed { get; set; }
            public Boolean SwitchAllowed { get; set; }
            public Boolean ForcedMetarace
            {
                get { return forcedMetarace; }
                set { forcedMetarace = value; }
            }

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