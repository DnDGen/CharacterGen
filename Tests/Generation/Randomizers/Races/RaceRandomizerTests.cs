using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races
{
    [TestFixture]
    public abstract class RaceRandomizerTests
    {
        protected Mock<IPercentileResultProvider> mockPercentileResultProvider;
        protected String controlCase;
        protected Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            alignment = new Alignment();
        }

        protected void AssertRaceIsAlwaysAllowed(String race)
        {
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };

            foreach (var goodness in goodnesses)
            {
                alignment.Goodness = goodness;
                AssertRaceIsAllowed(race);
            }
        }

        protected void AssertRaceIsAllowedOnlyForAlignment(String race, Int32 allowedGoodness)
        {
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };

            foreach (var goodness in goodnesses)
            {
                alignment.Goodness = goodness;

                if (goodness == allowedGoodness)
                    AssertRaceIsAllowed(race);
                else
                    AssertRaceIsNotAllowed(race);
            }
        }

        protected void AssertRaceIsNeverAllowed(String race)
        {
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };

            foreach (var goodness in goodnesses)
            {
                alignment.Goodness = goodness;
                AssertRaceIsNotAllowed(race);
            }
        }

        protected void AssertControlIsAlwaysAllowed(String secondaryControl)
        {
            var storedControlCase = controlCase;
            controlCase = secondaryControl;

            AssertRaceIsAlwaysAllowed(storedControlCase);

            controlCase = storedControlCase;
        }

        private void AssertRaceIsAllowed(String race)
        {
            var result = GetResult(race, controlCase);
            Assert.That(result, Is.EqualTo(race), alignment.ToString());
        }

        private void AssertRaceIsNotAllowed(String race)
        {
            var result = GetResult(race, controlCase);
            Assert.That(result, Is.EqualTo(controlCase), alignment.ToString());
        }

        protected abstract String GetResult(String race, String controlCase);
    }
}