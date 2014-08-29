using System;
using D20Dice;
using Moq;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators
{
    [TestFixture]
    public class RaceGeneratorTests
    {
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IDice> mockDice;
        private IRaceGenerator raceGenerator;
        private CharacterClass characterClass;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            raceGenerator = new RaceGenerator(mockDice.Object);

            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            characterClass = new CharacterClass();

            mockDice.Setup(d => d.Roll(1).d2()).Returns(1);
        }

        [Test]
        public void RandomizeBaseRace()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize("goodness", characterClass)).Returns("base race");

            var race = raceGenerator.GenerateWith("goodness", characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void RandomizeMetarace()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize("goodness", characterClass)).Returns("metarace");

            var race = raceGenerator.GenerateWith("goodness", characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void ReturnMaleOnLowRoll()
        {
            mockDice.Setup(d => d.Roll(1).d2()).Returns(1);

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void ReturnFemaleOnHighRoll()
        {
            mockDice.Setup(d => d.Roll(1).d2()).Returns(2);

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void RaceGeneratorReturnsMaleForDrowWizard()
        {
            characterClass.ClassName = CharacterClassConstants.Wizard;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClass>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockDice.Verify(d => d.Roll(It.IsAny<Int32>()).d2(), Times.Never);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void RaceGeneratorReturnsFemaleForDrowCleric()
        {
            characterClass.ClassName = CharacterClassConstants.Cleric;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClass>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(String.Empty, characterClass, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockDice.Verify(d => d.Roll(It.IsAny<Int32>()).d2(), Times.Never);
            Assert.That(race.Male, Is.False);
        }
    }
}