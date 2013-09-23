using System;
using Moq;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Providers.Interfaces;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class BaseRaceRandomizerTests
    {
        protected Mock<IPercentileResultProvider> mockPercentileResultProvider;
        protected IBaseRaceRandomizer randomizer;
        protected String controlCase;

        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockPercentileResultProvider = new Mock<IPercentileResultProvider>();
            alignment = new Alignment();
        }

        protected void AssertBaseRaceIsAlwaysAllowed(String baseRace)
        {
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };

            foreach (var goodness in goodnesses)
            {
                alignment.Goodness = goodness;
                AssertBaseRaceIsAllowed(baseRace);
            }
        }

        protected void AssertBaseRaceIsAllowedOnlyForAlignment(String baseRace, Int32 allowedGoodness)
        {
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };

            foreach (var goodness in goodnesses)
            {
                alignment.Goodness = goodness;

                if (goodness == allowedGoodness)
                    AssertBaseRaceIsAllowed(baseRace);
                else
                    AssertBaseRaceIsNotAllowed(baseRace);
            }
        }

        protected void AssertBaseRaceIsNeverAllowed(String baseRace)
        {
            var goodnesses = new[] { AlignmentConstants.Evil, AlignmentConstants.Neutral, AlignmentConstants.Good };

            foreach (var goodness in goodnesses)
            {
                alignment.Goodness = goodness;
                AssertBaseRaceIsNotAllowed(baseRace);
            }
        }

        protected void AssertControlIsAlwaysAllowed(String secondaryControl)
        {
            var storedControlCase = controlCase;
            controlCase = secondaryControl;

            AssertBaseRaceIsAlwaysAllowed(storedControlCase);

            controlCase = storedControlCase;
        }

        private void AssertBaseRaceIsAllowed(String baseRace)
        {
            var result = GetResult(baseRace, controlCase);
            Assert.That(result, Is.EqualTo(baseRace));
        }

        private void AssertBaseRaceIsNotAllowed(String baseRace)
        {
            var result = GetResult(baseRace, controlCase);
            Assert.That(result, Is.EqualTo(controlCase));
        }

        private void AssertControlIsAllowed(String secondaryControl)
        {
            var result = GetResult(controlCase, secondaryControl);
            Assert.That(result, Is.EqualTo(controlCase));
        }

        private String GetResult(String baseRace, String controlCase)
        {
            mockPercentileResultProvider.SetupSequence(p => p.GetPercentileResult(It.IsAny<String>()))
                .Returns(baseRace)
                .Returns(controlCase);

            return randomizer.Randomize(alignment, String.Empty);
        }
    }
}