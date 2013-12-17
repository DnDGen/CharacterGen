using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Generation.Factories
{
    [TestFixture]
    public class RaceFactoryTests : IntegrationTest
    {
        [Inject]
        public IRaceFactory RaceFactory { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }
        [Inject]
        public CharacterClassPrototype CharacterClassPrototype { get; set; }
        [Inject]
        public AnyBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public AnyMetaraceRandomizer MetaraceRandomizer { get; set; }

        [Test]
        public void RaceFactoryReturnsRace()
        {
            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var race = RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race, Is.Not.Null);
            }
        }

        [Test]
        public void RaceFactoryReturnsRaceWithBaseRace()
        {
            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var race = RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race.BaseRace, Is.Not.Null);
                Assert.That(race.BaseRace, Is.Not.Empty);
            }
        }

        [Test]
        public void RaceFactoryReturnsRaceWithMetarace()
        {
            for (var i = 0; i < ConfidenceLevel; i++)
            {
                var race = RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race.Metarace, Is.Not.Null);
            }
        }
    }
}