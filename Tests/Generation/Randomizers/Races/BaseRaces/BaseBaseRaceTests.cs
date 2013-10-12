using Moq;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseBaseRaceTests
    {
        private TestBaseRaceRandomizer randomizer;
        private Mock<IPercentileResultProvider> mockPercentileResultProvider;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult(It.IsAny<String>())).Returns("result");

            randomizer = new TestBaseRaceRandomizer(mockPercentileResultProvider.Object);
            randomizer.Allowed = true;
        }

        [Test]
        public void LoopUntilBaseRaceIsAllowed()
        {
            randomizer.Allowed = false;

            randomizer.Randomize(String.Empty, String.Empty);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void ReturnBaseRaceFromPercentileResultProvider()
        {
            var result = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(result, Is.EqualTo("result"));
        }

        [Test]
        public void NoBaseRaceIsNotAllowed()
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>())).Returns(String.Empty).Returns("result");

            randomizer.Randomize(String.Empty, String.Empty);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult(It.IsAny<String>()), Times.Exactly(2));
        }

        [Test]
        public void AccessesTableAlignmentGoodnessClassBaseRaces()
        {
            var result = randomizer.Randomize("goodness", "className");

            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("goodnessclassNameBaseRaces")
                , Times.Once());
        }

        private class TestBaseRaceRandomizer : BaseBaseRace
        {
            public Boolean Allowed { get; set; }

            public TestBaseRaceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

            protected override Boolean BaseRaceIsAllowed(String baseRace)
            {
                var toReturn = Allowed;
                Allowed = !Allowed;
                return toReturn;
            }
        }
    }
}