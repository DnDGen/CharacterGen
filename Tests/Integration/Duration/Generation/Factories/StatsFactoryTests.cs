using Ninject;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class StatsFactoryTests : DurationTest
    {
        [Inject]
        public IStatsFactory StatsFactory { get; set; }
        [Inject]
        public RawStatsRandomizer StatsRandomizer { get; set; }
        [Inject]
        public CharacterClass CharacterClass { get; set; }
        [Inject]
        public Race Race { get; set; }

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
        public void StatsGeneration()
        {
            StatsFactory.CreateWith(StatsRandomizer, CharacterClass, Race);
            AssertDuration();
        }
    }
}