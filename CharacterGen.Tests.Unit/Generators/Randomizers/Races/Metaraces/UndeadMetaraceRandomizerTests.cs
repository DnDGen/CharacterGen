using CharacterGen.Domain.Generators.Randomizers.Races.Metaraces;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class UndeadMetaraceRandomizerTests : MetaraceRandomizerTestBase
    {
        protected override IEnumerable<string> metaraces
        {
            get
            {
                return new[]
                {
                    "genetic metarace",
                    "lycanthrope metarace",
                    "undead metarace",
                    RaceConstants.Metaraces.None,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            randomizer = new UndeadMetaraceRandomizer(mockPercentileSelector.Object, mockAdjustmentsSelector.Object, mockCollectionSelector.Object, generator);

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead))
                .Returns(new[] { "undead metarace" });
        }

        [Test]
        public void UndeadMetaracesAllowed()
        {
            (randomizer as IForcableMetaraceRandomizer).ForceMetarace = false;
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.EquivalentTo(new[] { "undead metarace", RaceConstants.Metaraces.None }));
        }

        [Test]
        public void OnlyUndeadMetaracesAllowed()
        {
            (randomizer as IForcableMetaraceRandomizer).ForceMetarace = true;
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.EquivalentTo(new[] { "undead metarace" }));
        }
    }
}
