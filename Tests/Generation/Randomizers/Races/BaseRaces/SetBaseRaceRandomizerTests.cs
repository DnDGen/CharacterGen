using System;
using NPCGen.Core.Data.Alignments;
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

            var baseRace = randomizer.Randomize(new Alignment(), String.Empty);
            Assert.That(baseRace, Is.EqualTo("base race"));
        }
    }
}