using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Alignments;
using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class NeutralMetaraceRandomizerTests : MetaraceRandomizerTests
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
                    RaceConstants.Metaraces.NoneId
                };
            }
        }

        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new NeutralMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object, mockNameSelector.Object,
                mockCollectionsSelector.Object);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Good)).Returns(new[] { "good metarace", "metarace", "not neutral metarace", "not evil metarace" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Evil)).Returns(new[] { "evil metarace", "metarace", "not good metarace", "not neutral metarace" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, AlignmentConstants.Neutral)).Returns(new[] { "neutral metarace", "metarace", "not evil metarace", "not good metarace" });
        }

        [TestCase("metarace")]
        [TestCase("neutral metarace")]
        [TestCase("not good metarace")]
        [TestCase("not evil metarace")]
        [TestCase(RaceConstants.Metaraces.NoneId)]
        public void Allowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            Assert.That(metaraces, Contains.Item(metarace));
        }

        [TestCase("evil metarace")]
        [TestCase("good metarace")]
        [TestCase("not neutral metarace")]
        public void NotAllowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossibleIds(String.Empty, characterClass);
            Assert.That(metaraces, Is.Not.Contains(metarace));
        }
    }
}