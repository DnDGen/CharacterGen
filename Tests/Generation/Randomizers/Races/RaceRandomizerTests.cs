using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Core.Generation.Providers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTests
    {
        protected Mock<IPercentileResultProvider> mockPercentileResultProvider;
        protected Mock<ILevelAdjustmentsProvider> mockLevelAdjustmentsProvider;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            mockLevelAdjustmentsProvider = new Mock<ILevelAdjustmentsProvider>();
        }

        protected void AssertRaceIsAllowed(String race)
        {
            var results = GetResults();
            Assert.That(results.Contains(race), Is.True);
        }

        protected void AssertRaceIsNotAllowed(String race)
        {
            var results = GetResults();
            Assert.That(results.Contains(race), Is.False);
        }

        protected abstract IEnumerable<String> GetResults();
    }
}