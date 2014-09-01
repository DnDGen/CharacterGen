using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NeutralForcedMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        protected override IEnumerable<String> metaraces
        {
            get
            {
                return new[]
                {
                    "metarace",
                    "good metarace",
                    "neutral metarace",
                    "evil metarace",
                    "not good metarace",
                    "not neutral metarace",
                    "not evil metarace",
                    RaceConstants.Metaraces.None
                };
            }
        }

        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new NeutralForcedMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object,
                mockCollectionsSelector.Object);

            mockCollectionsSelector.Setup(s => s.SelectFrom("MetaraceGroups", AlignmentConstants.Good)).Returns(new[] { "good metarace", "metarace", "not neutral metarace", "not evil metarace" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("MetaraceGroups", AlignmentConstants.Evil)).Returns(new[] { "evil metarace", "metarace", "not good metarace", "not neutral metarace" });
            mockCollectionsSelector.Setup(s => s.SelectFrom("MetaraceGroups", AlignmentConstants.Neutral)).Returns(new[] { "neutral metarace", "metarace", "not evil metarace", "not good metarace" });
        }

        [TestCase("metarace")]
        [TestCase("neutral metarace")]
        [TestCase("not good metarace")]
        [TestCase("not evil metarace")]
        public void Allowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(metaraces, Contains.Item(metarace));
        }

        [TestCase("evil metarace")]
        [TestCase("good metarace")]
        [TestCase("not neutral metarace")]
        [TestCase(RaceConstants.Metaraces.None)]
        public void NotAllowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(metaraces, Is.Not.Contains(metarace));
        }
    }
}