using Moq;
using NPCGen.Core.Generation.Providers.Interfaces;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTests
    {
        protected Mock<IPercentileResultProvider> mockPercentileResultProvider;
        protected String controlCase;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
        }

        protected void AssertControlIsAllowed(String secondaryControl)
        {
            var storedControlCase = controlCase;
            controlCase = secondaryControl;

            AssertRaceIsAllowed(storedControlCase);

            controlCase = storedControlCase;
        }

        protected void AssertRaceIsAllowed(String race)
        {
            var result = GetResult(race, controlCase);
            Assert.That(result, Is.EqualTo(race));
        }

        protected void AssertRaceIsNotAllowed(String race)
        {
            var result = GetResult(race, controlCase);
            Assert.That(result, Is.EqualTo(controlCase));
        }

        protected abstract String GetResult(String race, String controlCase);
    }
}