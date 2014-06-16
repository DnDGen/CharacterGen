using System;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
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
            var baseRace = randomizer.Randomize(String.Empty, new CharacterClassPrototype());
            Assert.That(baseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void GetAllPossibleResultsReturnsSetBaseRace()
        {
            var baseRaces = randomizer.GetAllPossibleResults(String.Empty, new CharacterClassPrototype());
            Assert.That(baseRaces.First(), Is.EqualTo("base race"));
            Assert.That(baseRaces.Count(), Is.EqualTo(1));
        }
    }
}