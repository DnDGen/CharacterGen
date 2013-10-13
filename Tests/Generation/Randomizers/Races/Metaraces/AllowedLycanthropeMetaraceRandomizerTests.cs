using NPCGen.Core.Data.Races;
using NPCGen.Tests.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;
using System;

namespace NPCGen.Core.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AllowedLycanthropeMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new AllowedLycanthropeMetaraceRandomizer(mockPercentileResultProvider.Object);
            controlCase = RaceConstants.Metaraces.Werebear;
        }

        [Test]
        public void NoMetaraceIsAllowed()
        {
            AssertRaceIsAllowed(String.Empty);
        }
    }
}