using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Core.Data.Stats;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Randomizers.Stats.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
{
    [TestFixture]
    public class StatsFactoryTests
    {
        private Mock<IStatsRandomizer> mockStatRandomizer;
        private Dictionary<String, Stat> expectedStats;

        [SetUp]
        public void Setup()
        {
            mockStatRandomizer = new Mock<IStatsRandomizer>();
            expectedStats = new Dictionary<String, Stat>();
            mockStatRandomizer.Setup(r => r.Randomize()).Returns(expectedStats);
        }

        [Test]
        public void RandomizesStatValues()
        {
            var stats = StatsFactory.CreateUsing(mockStatRandomizer.Object);
            Assert.That(stats, Is.EqualTo(expectedStats));
        }
    }
}