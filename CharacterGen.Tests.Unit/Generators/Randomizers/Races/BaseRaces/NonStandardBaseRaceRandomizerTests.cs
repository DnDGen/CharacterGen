using CharacterGen.Domain.Generators.Randomizers.Races.BaseRaces;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class NonStandardBaseRaceRandomizerTests : BaseRaceRandomizerTestBase
    {
        protected override IEnumerable<string> baseRaces
        {
            get
            {
                return new[]
                {
                    "base race",
                    "standard base race",
                    "non-standard base race",
                    "monster base race",
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new NonStandardBaseRaceRandomizer(mockPercentileSelector.Object, mockAdjustmentsSelector.Object, mockCollectionSelector.Object, generator);

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Standard))
                .Returns(new[] { "standard base race", "other base race" });
        }

        [Test]
        public void OnlyNonStandardBaseRacesAllowed()
        {
            var baseRaces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(baseRaces, Is.EquivalentTo(new[] { "non-standard base race", "base race", "monster base race" }));
        }
    }
}