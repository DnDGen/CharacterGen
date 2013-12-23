using Ninject;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Stats
{
    [TestFixture]
    public class AverageStatsRandomizerTests : DurationTest
    {
        [Inject]
        public AverageStatsRandomizer StatsRandomizer { get; set; }

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
        public void AverageStatsRandomization()
        {
            StatsRandomizer.Randomize();
            AssertDuration();
        }
    }
}