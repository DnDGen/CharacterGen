using Ninject;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Stats
{
    [TestFixture]
    public class BestOfFourStatsRandomizerTests : DurationTest
    {
        [Inject]
        public BestOfFourStatsRandomizer StatsRandomizer { get; set; }

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
        public void BestOfFourStatsRandomization()
        {
            StatsRandomizer.Randomize();
            AssertDuration();
        }
    }
}