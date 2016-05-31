using CharacterGen.Domain.Generators.Randomizers.Races.Metaraces;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class GeneticMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        protected override IEnumerable<string> metaraceNames
        {
            get
            {
                return new[]
                {
                    "genetic metarace",
                    RaceConstants.Metaraces.None
                };
            }
        }

        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new GeneticMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object,
                mockCollectionsSelector.Object, generator);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Genetic))
                .Returns(new[] { "genetic metarace" });
        }

        [TestCase("genetic metarace")]
        [TestCase(RaceConstants.Metaraces.None)]
        public void Allowed(string metarace)
        {
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Contains.Item(metarace));
        }

        [TestCase("lycanthrope metarace")]
        public void NotAllowed(string metarace)
        {
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.All.Not.EqualTo(metarace));
        }
    }
}