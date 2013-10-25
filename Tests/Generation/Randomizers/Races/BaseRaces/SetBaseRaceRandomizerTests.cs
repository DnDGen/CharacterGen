using System;
using System.Linq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests
    {
        private SetBaseRaceRandomizer randomizer;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetBaseRaceRandomizer();
            randomizer.BaseRace = "base race";
        }

        [Test]
        public void RandomizeReturnsSetBaseRace()
        {
            var baseRace = randomizer.Randomize(String.Empty, new CharacterClass());
            Assert.That(baseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void GetAllPossibleResultsReturnsSetBaseRace()
        {
            var baseRaces = randomizer.GetAllPossibleResults(String.Empty, new CharacterClass());
            Assert.That(baseRaces.First(), Is.EqualTo("base race"));
            Assert.That(baseRaces.Count(), Is.EqualTo(1));
        }
    }
}