using Ninject;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Stats
{
    [TestFixture]
    public class RawStatsRandomizerTests : DurationTest
    {
        [Inject]
        public RawStatsRandomizer StatsRandomizer { get; set; }

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
        public void RawStatsRandomization()
        {
            StatsRandomizer.Randomize();
            AssertDuration();
        }
    }
}