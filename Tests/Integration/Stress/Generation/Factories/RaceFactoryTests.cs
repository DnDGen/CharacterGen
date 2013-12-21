using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Factories
{
    [TestFixture]
    public class RaceFactoryTests : StressTest
    {
        [Inject]
        public IRaceFactory RaceFactory { get; set; }
        [Inject]
        public IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public IMetaraceRandomizer MetaraceRandomizer { get; set; }

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
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = GetNewInstanceOf<CharacterClassPrototype>();
                var race = RaceFactory.CreateWith(alignment.Goodness, prototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void RaceFactoryReturnsRaceWithBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = GetNewInstanceOf<CharacterClassPrototype>();
                var race = RaceFactory.CreateWith(alignment.Goodness, prototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race.BaseRace, Is.Not.Null);
                Assert.That(race.BaseRace, Is.Not.Empty);
            }

            AssertIterations();
        }

        [Test]
        public void RaceFactoryReturnsRaceWithMetarace()
        {
            while (TestShouldKeepRunning())
            {
                var alignment = GetNewInstanceOf<Alignment>();
                var prototype = GetNewInstanceOf<CharacterClassPrototype>();
                var race = RaceFactory.CreateWith(alignment.Goodness, prototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race.Metarace, Is.Not.Null);
            }

            AssertIterations();
        }
    }
}