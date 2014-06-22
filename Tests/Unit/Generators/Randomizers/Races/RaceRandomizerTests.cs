using System;
using System.Collections.Generic;
using Moq;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTests
    {
        protected Mock<IPercentileSelector> mockPercentileResultSelector;
        protected Mock<ILevelAdjustmentsSelector> mockLevelAdjustmentsSelector;

        [SetUp]
        public void RaceRandomizerTestsSetup()
        {
            mockPercentileResultSelector = new Mock<IPercentileSelector>();
            mockLevelAdjustmentsSelector = new Mock<ILevelAdjustmentsSelector>();
        }

        protected void AssertRaceIsAllowed(String race)
        {
            var results = GetResults();
            Assert.That(results, Contains.Item(race));
        }

        protected void AssertRaceIsNotAllowed(String race)
        {
            var results = GetResults();
            Assert.That(results, Is.Not.Contains(race));
        }

        protected abstract IEnumerable<String> GetResults();
    }
}