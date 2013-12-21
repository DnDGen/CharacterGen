using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Factories.Interfaces;
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
                var race = RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, GetBaseRaceRandomizer(kernel), GetMetaraceRandomizer(kernel));
                Assert.That(race, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void RaceFactoryReturnsRaceWithBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var race = RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, GetBaseRaceRandomizer(kernel), GetMetaraceRandomizer(kernel));
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
                var race = RaceFactory.CreateWith(Alignment.Goodness, CharacterClassPrototype, GetBaseRaceRandomizer(kernel), GetMetaraceRandomizer(kernel));
                Assert.That(race.Metarace, Is.Not.Null);
            }

            AssertIterations();
        }
    }
}