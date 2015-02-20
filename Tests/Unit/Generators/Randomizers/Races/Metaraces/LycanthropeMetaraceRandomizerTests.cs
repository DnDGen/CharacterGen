using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Common.Races;
using NPCGen.Generators.Randomizers.Races.Metaraces;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class LycanthropeMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        protected override IEnumerable<String> metaraces
        {
            get
            {
                return new[]
                {
                    "lycanthrope metarace",
                    RaceConstants.Metaraces.None
                };
            }
        }

        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new LycanthropeMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object,
                mockCollectionsSelector.Object);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, TableNameConstants.Set.Collection.Groups.Lycanthrope))
                .Returns(new[] { "lycanthrope metarace" });
        }

        [TestCase("lycanthrope metarace")]
        [TestCase(RaceConstants.Metaraces.None)]
        public void Allowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(metaraces, Contains.Item(metarace));
        }

        [TestCase("genetic metarace")]
        public void NotAllowed(String metarace)
        {
            var metaraces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(metaraces, Is.Not.Contains(metarace));
        }
    }
}