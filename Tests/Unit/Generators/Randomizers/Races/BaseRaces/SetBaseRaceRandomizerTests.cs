using System;
using System.Linq;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NPCGen.Generators.Randomizers.Races.BaseRaces;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests
    {
        private ISetBaseRaceRandomizer randomizer;
        private CharacterClass characterclass;
        private Mock<INameSelector> mockNameSelector;

        [SetUp]
        public void Setup()
        {
            randomizer = new SetBaseRaceRandomizer(mockNameSelector.Object);
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
            randomizer.SetBaseRaceId = "baserace";
            mockNameSelector.Setup(s => s.Select("baserace")).Returns("base race");

            var baseRace = randomizer.Randomize(String.Empty, characterclass);
            Assert.That(baseRace.Id, Is.EqualTo("baserace"));
            Assert.That(baseRace.Name, Is.EqualTo("base race"));
        }

        [Test]
        public void ReturnJustSetBaseRace()
        {
            randomizer.SetBaseRaceId = "baserace";

            var baseRaces = randomizer.GetAllPossibleIds(String.Empty, characterclass);
            Assert.That(baseRaces, Contains.Item("baserace"));
            Assert.That(baseRaces.Count(), Is.EqualTo(1));
        }
    }
}