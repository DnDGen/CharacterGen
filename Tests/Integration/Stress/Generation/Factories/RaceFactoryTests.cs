using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Factories
{
    [TestFixture]
    public class RaceFactoryTests : StressTest
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

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void RaceFactoryReturnsRace()
        {
            while (TestShouldKeepRunning())
            {
                var race = RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race, Is.Not.Null);
            }
        }

        [Test]
        public void RaceFactoryReturnsRaceWithBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var race = RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race.BaseRace, Is.Not.Null);
                Assert.That(race.BaseRace, Is.Not.Empty);
            }
        }

        [Test]
        public void RaceFactoryReturnsRaceWithMetarace()
        {
            while (TestShouldKeepRunning())
            {
                var race = RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race.Metarace, Is.Not.Null);
            }
        }
    }
}