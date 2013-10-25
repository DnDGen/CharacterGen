using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class StatsFactoryTests
    {
        private Mock<IStatsRandomizer> mockStatRandomizer;
        private Dictionary<String, Stat> expectedStats;
        private Mock<IStatAdjustmentsProvider> mockStatAdjustmentsProvider;
        private Race race;

        [SetUp]
        public void Setup()
        {
            mockStatRandomizer = new Mock<IStatsRandomizer>();
            expectedStats = new Dictionary<String, Stat>();
            mockStatRandomizer.Setup(r => r.Randomize()).Returns(expectedStats);

            mockStatAdjustmentsProvider = new Mock<IStatAdjustmentsProvider>();
            race = new Race();
        }

        [Test]
        public void StatsContainAllStats()
        {
            var stats = StatsFactory.CreateUsing(mockStatRandomizer.Object, mockStatAdjustmentsProvider.Object, race);

            foreach (var stat in StatConstants.GetStats())
                Assert.That(stats.ContainsKey(stat), Is.True);
        }

        [Test]
        public void RandomizesStatValues()
        {
            StatsFactory.CreateUsing(mockStatRandomizer.Object, mockStatAdjustmentsProvider.Object, race);
            mockStatRandomizer.Verify(r => r.Randomize(), Times.Once);
        }

        [Test]
        public void UsesStatAdjustmentProviderToAdjustStatsByRace()
        {
            StatsFactory.CreateUsing(mockStatRandomizer.Object, mockStatAdjustmentsProvider.Object, race);
            mockStatAdjustmentsProvider.Verify(p => p.GetAdjustments(It.IsAny<Race>()), Times.Once);
        }
    }
}