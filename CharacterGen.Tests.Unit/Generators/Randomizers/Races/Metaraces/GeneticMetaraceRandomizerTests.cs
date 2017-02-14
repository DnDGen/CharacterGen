using CharacterGen.Domain.Generators.Randomizers.Races.Metaraces;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Races;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class GeneticMetaraceRandomizerTests : MetaraceRandomizerTestBase
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
            randomizer = new GeneticMetaraceRandomizer(mockPercentileSelector.Object, mockAdjustmentsSelector.Object, mockCollectionSelector.Object, generator);

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Genetic))
                .Returns(new[] { "genetic metarace" });
        }

        [Test]
        public void GeneticMetaracesAllowed()
        {
            (randomizer as IForcableMetaraceRandomizer).ForceMetarace = false;
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.EquivalentTo(new[] { "genetic metarace", RaceConstants.Metaraces.None }));
        }

        [Test]
        public void OnlyGeneticMetaracesAllowed()
        {
            (randomizer as IForcableMetaraceRandomizer).ForceMetarace = true;
            var metaraces = randomizer.GetAllPossible(alignment, characterClass);
            Assert.That(metaraces, Is.EquivalentTo(new[] { "genetic metarace" }));
        }
    }
}