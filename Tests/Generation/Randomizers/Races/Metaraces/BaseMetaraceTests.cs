using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
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
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            alignment = new Alignment();
            alignment.Goodness = AlignmentConstants.Good;

            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            randomizer = new TestMetaraceRandomizer(mockPercentileResultProvider.Object);
        }

        [Test]
        public void LoopUntilMetaraceIsAllowed()
        {
            randomizer.MetaraceAllowed = false;
            randomizer.Randomize(alignment.GetGoodnessString(), CharacterClassConstants.Barbarian);
            mockPercentileResultProvider.Verify(p => p.GetPercentileResult("GoodBarbarianMetaraces"), Times.Exactly(2));
        }

        [Test]
        public void ReturnMetaraceFromPercentileResultProvider()
        {
            randomizer.MetaraceAllowed = true;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodBarbarianMetaraces")).Returns("result");

            var result = randomizer.Randomize(alignment.GetGoodnessString(), CharacterClassConstants.Barbarian);
            Assert.That(result, Is.EqualTo("result"));
        }

        [Test]
        public void IfNotForcedThenEmptyMetaraceIsAllowed()
        {
            randomizer.ForcedMetarace = false;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodBarbarianMetaraces")).Returns(String.Empty);

            var result = randomizer.Randomize(alignment.GetGoodnessString(), CharacterClassConstants.Barbarian);
            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void IfNotForcedThenMetaraceIsAllowed()
        {
            randomizer.ForcedMetarace = false;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodBarbarianMetaraces")).Returns("metarace");

            var result = randomizer.Randomize(alignment.GetGoodnessString(), CharacterClassConstants.Barbarian);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        [Test]
        public void IfForcedThenEmptyMetaraceIsNotAllowed()
        {
            randomizer.ForcedMetarace = true;
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult("GoodBarbarianMetaraces")).Returns(String.Empty)
                .Returns("metarace");

            var result = randomizer.Randomize(alignment.GetGoodnessString(), CharacterClassConstants.Barbarian);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        [Test]
        public void IfForcedThenMetaraceIsAllowed()
        {
            randomizer.ForcedMetarace = true;
            mockPercentileResultProvider.Setup(p => p.GetPercentileResult("GoodBarbarianMetaraces")).Returns("metarace");

            var result = randomizer.Randomize(alignment.GetGoodnessString(), CharacterClassConstants.Barbarian);
            Assert.That(result, Is.EqualTo("metarace"));
        }

        private class TestMetaraceRandomizer : BaseMetarace
        {
            public Boolean MetaraceAllowed { get; set; }
            public Boolean ForcedMetarace
            {
                get { return forcedMetarace; }
                set { forcedMetarace = value; }
            }

            public TestMetaraceRandomizer(IPercentileResultProvider percentileResultProvider) : base(percentileResultProvider) { }

            protected override Boolean MetaraceIsAllowed(String metarace)
            {
                var toReturn = MetaraceAllowed;
                MetaraceAllowed = !MetaraceAllowed;
                return toReturn;
            }
        }
    }
}