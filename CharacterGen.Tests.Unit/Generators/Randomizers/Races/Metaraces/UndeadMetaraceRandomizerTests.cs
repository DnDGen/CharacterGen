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
    public class UndeadMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        protected override IEnumerable<string> metaraceNames
        {
            get
            {
                return new[]
                {
                    "undead metarace",
                    RaceConstants.Metaraces.None
                };
            }
        }

        private Mock<ICollectionsSelector> mockCollectionsSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            randomizer = new UndeadMetaraceRandomizer(mockPercentileResultSelector.Object, mockAdjustmentsSelector.Object, mockCollectionsSelector.Object, generator);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead))
                .Returns(new[] { "undead metarace" });
        }

        [TestCase("undead metarace")]
        [TestCase(RaceConstants.Metaraces.None)]
        public void Allowed(string metarace)
        {
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Contains.Item(metarace));
        }

        [TestCase("genetic metarace")]
        public void NotAllowed(string metarace)
        {
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.All.Not.EqualTo(metarace));
        }
    }
}
