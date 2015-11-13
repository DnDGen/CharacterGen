using CharacterGen.Common.Alignments;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Domain.Randomizers.Races.Metaraces;
using CharacterGen.Selectors;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NonNeutralMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        protected override IEnumerable<String> metaraceIds
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
            randomizer = new NonNeutralMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object,
                mockCollectionsSelector.Object);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Good)).Returns(new[] { "good metarace", "metarace", "not neutral metarace", "not evil metarace" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Evil)).Returns(new[] { "evil metarace", "metarace", "not good metarace", "not neutral metarace" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Neutral)).Returns(new[] { "neutral metarace", "metarace", "not evil metarace", "not good metarace" });
        }

        [TestCase("metarace")]
        [TestCase("good metarace")]
        [TestCase("evil metarace")]
        [TestCase("not neutral metarace")]
        [TestCase("not good metarace")]
        [TestCase("not evil metarace")]
        [TestCase(RaceConstants.Metaraces.None)]
        public void Allowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Contains.Item(metarace));
        }

        [TestCase("neutral metarace")]
        public void NotAllowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.All.Not.EqualTo(metarace));
        }
    }
}