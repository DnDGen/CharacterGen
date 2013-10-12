using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Randomizers.Races.Metaraces;
using NUnit.Framework;
using System;

namespace NPCGen.Tests.Generation.Randomizers.Races.Metaraces
{
    [TestFixture]
    public class AllowedEvilMetaraceRandomizerTests : MetaraceRandomizerTests
    {
        [SetUp]
        public void Setup()
        {
            randomizer = new AllowedEvilMetaraceRandomizer(mockPercentileResultProvider.Object);
            controlCase = RaceConstants.Metaraces.HalfDragon;
        }

        [Test]
        public void NoMetaraceIsAllowed()
        {
            AssertRaceIsAllowed(String.Empty);
        }
    }
}