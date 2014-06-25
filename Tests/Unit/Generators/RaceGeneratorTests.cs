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
        private CharacterClassPrototype prototype;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            raceGenerator = new RaceGenerator(mockDice.Object);

            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            prototype = new CharacterClassPrototype();
        }

        [Test]
        public void RandomizeBaseRace()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize("goodness", prototype)).Returns("base race");

            var race = raceGenerator.GenerateWith("goodness", prototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void RandomizeMetarace()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize("goodness", prototype)).Returns("metarace");

            var race = raceGenerator.GenerateWith("goodness", prototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void ReturnMaleOnLowRoll()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);

            var race = raceGenerator.GenerateWith(String.Empty, prototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void ReturnFemaleOnHighRoll()
        {
            mockDice.Setup(d => d.d2(1)).Returns(2);

            var race = raceGenerator.GenerateWith(String.Empty, prototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void RaceGeneratorReturnsMaleForDrowWizard()
        {
            prototype.ClassName = CharacterClassConstants.Wizard;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(String.Empty, prototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockDice.Verify(d => d.d2(1), Times.Never);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void RaceGeneratorReturnsFemaleForDrowCleric()
        {
            prototype.ClassName = CharacterClassConstants.Cleric;
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(String.Empty, prototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            mockDice.Verify(d => d.d2(1), Times.Never);
            Assert.That(race.Male, Is.False);
        }
    }
}