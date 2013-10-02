using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class StatsFactoryTests
    {
        private IStatsFactory statsFactory;
        private Mock<IStatsRandomizer> mockStatRandomizer;
        private Dictionary<String, Stat> expectedStats;

        [SetUp]
        public void Setup()
        {
            mockStatRandomizer = new Mock<IStatsRandomizer>();
            expectedStats = new Dictionary<String, Stat>();
            mockStatRandomizer.Setup(r => r.Randomize()).Returns(expectedStats);

            statsFactory = new StatsFactory(mockStatRandomizer.Object);
        }

        [Test]
        public void RandomizerSetProperly()
        {
            Assert.That(statsFactory.StatsRandomizer, Is.EqualTo(mockStatRandomizer.Object));
        }

        [Test]
        public void RandomizesStatValues()
        {
            var stats = statsFactory.Generate();
            Assert.That(stats, Is.EqualTo(expectedStats));
        }

        [Test]
        public void ChangeStatRandomizer()
        {
            var differentRandomizer = new Mock<IStatsRandomizer>();
            var newStats = new Dictionary<String, Stat>();
            differentRandomizer.Setup(r => r.Randomize()).Returns(newStats);

            statsFactory.StatsRandomizer = differentRandomizer.Object;

            var stats = statsFactory.Generate();
            Assert.That(stats, Is.EqualTo(newStats));
        }
    }
}