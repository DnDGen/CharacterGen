using Ninject;
using NPCGen.Core.Generation.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Randomizers.Stats
{
    [TestFixture]
    public class OnesAsSixesStatsRandomizerTests : DurationTest
    {
        [Inject]
        public OnesAsSixesStatsRandomizer StatsRandomizer { get; set; }

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
        public void OnesAsSixesStatsRandomization()
        {
            StatsRandomizer.Randomize();
            AssertDuration();
        }
    }
}