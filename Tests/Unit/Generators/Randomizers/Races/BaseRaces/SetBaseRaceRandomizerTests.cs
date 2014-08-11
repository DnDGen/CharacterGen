using System;
using System.Linq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests
    {
        private ISetBaseRaceRandomizer randomizer;
        private CharacterClass characterclass;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetBaseRaceRandomizer();
            characterclass = new CharacterClass();
        }

        [Test]
        public void SetBaseRaceRandomizerIsABaseRaceRandomizer()
        {
            Assert.That(randomizer, Is.InstanceOf<IBaseRaceRandomizer>());
        }

        [Test]
        public void ReturnSetBaseRace()
        {
            randomizer.SetBaseRace = "base race";
            var baseRace = randomizer.Randomize(String.Empty, characterclass);
            Assert.That(baseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void ReturnJustSetBaseRace()
        {
            randomizer.SetBaseRace = "base race";

            var baseRaces = randomizer.GetAllPossibleResults(String.Empty, characterclass);
            Assert.That(baseRaces, Contains.Item("base race"));
            Assert.That(baseRaces.Count(), Is.EqualTo(1));
        }
    }
}