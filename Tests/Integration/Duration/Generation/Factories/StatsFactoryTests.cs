using Ninject;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats;
using NPCGen.Tests.Integration.Common;
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
        public DependentDataCollection DependentData { get; set; }

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
            StatsFactory.CreateWith(StatsRandomizer, DependentData.CharacterClass, DependentData.Race);
            AssertDuration();
        }
    }
}