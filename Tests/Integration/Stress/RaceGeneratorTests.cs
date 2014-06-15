using Ninject;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class RaceGeneratorTests : StressTests
    {
        [Inject]
        public IRaceGenerator RaceFactory { get; set; }
        [Inject]
        public IBaseRaceRandomizer BaseRaceRandomizer { get; set; }
        [Inject]
        public IMetaraceRandomizer MetaraceRandomizer { get; set; }

        [Test]
        public void RaceFactoryReturnsRace()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var race = RaceFactory.CreateWith(data.Alignment.Goodness, data.CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race, Is.Not.Null);
            }

            AssertIterations();
        }

        [Test]
        public void RaceFactoryReturnsRaceWithBaseRace()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                var race = RaceFactory.CreateWith(data.Alignment.Goodness, data.CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
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
                var data = GetNewInstanceOf<DependentDataCollection>();
                var race = RaceFactory.CreateWith(data.Alignment.Goodness, data.CharacterClassPrototype, BaseRaceRandomizer, MetaraceRandomizer);
                Assert.That(race.Metarace, Is.Not.Null);
            }

            AssertIterations();
        }
    }
}