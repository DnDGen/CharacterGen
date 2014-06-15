using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTests
    {
        protected Mock<IPercentileSelector> mockPercentileResultProvider;
        protected Mock<ILevelAdjustmentsSelector> mockLevelAdjustmentsProvider;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileSelector>();
            mockLevelAdjustmentsProvider = new Mock<ILevelAdjustmentsSelector>();
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