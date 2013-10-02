using System;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests
    {
        [Test]
        public void ReturnSetBaseRace()
        {
            var randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = "base race";

            var baseRace = randomizer.Randomize(String.Empty, String.Empty);
            Assert.That(baseRace, Is.EqualTo("base race"));
        }
    }
}