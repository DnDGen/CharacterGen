using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NPCGen.Selectors.Interfaces;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonStandardBaseRaceRandomizerTests : BaseRaceRandomizerTests
    {
        protected override IEnumerable<String> baseRaces
        {
            get
            {
                return new[]
                {
                    "nonstandard base race"
                };
            }
        }

        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new NonStandardBaseRaceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object,
                mockCollectionsSelector.Object);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, TableNameConstants.Set.Collection.Groups.Standard))
                .Returns(new[] { "standard base race" });
        }

        [TestCase("nonstandard base race")]
        public void Allowed(String baseRace)
        {
            var baseRaces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(baseRaces, Contains.Item(baseRace));
        }

        [TestCase("standard base race")]
        public void NotAllowed(String baseRace)
        {
            var baseRaces = randomizer.GetAllPossibleResults(String.Empty, characterClass);
            Assert.That(baseRaces, Is.Not.Contains(baseRace));
        }
    }
}