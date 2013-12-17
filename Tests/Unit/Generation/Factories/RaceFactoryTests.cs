using System;
using D20Dice;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Factories
{
    [TestFixture]
    public class RaceFactoryTests
    {
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
            mockDice = new Mock<IDice>();
        }

        [Test]
        public void RaceFactoryReturnsRandomizedBaseRace()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns("base race");

            var race = RaceFactory.CreateUsing(String.Empty, new CharacterClassPrototype(), mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object,
                mockDice.Object);
            Assert.That(race.BaseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void RaceFactoryReturnsRandomizedMetarace()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns("metarace");

            var race = RaceFactory.CreateUsing(String.Empty, new CharacterClassPrototype(), mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object,
                mockDice.Object);
            Assert.That(race.Metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void RaceFactoryReturnsMaleOnLowRoll()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);

            var race = RaceFactory.CreateUsing(String.Empty, new CharacterClassPrototype(), mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, 
                mockDice.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void RaceFactoryReturnsFemaleOnHighRoll()
        {
            mockDice.Setup(d => d.d2(1)).Returns(2);

            var race = RaceFactory.CreateUsing(String.Empty, new CharacterClassPrototype(), mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object,
                mockDice.Object);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void RaceFactoryReturnsMaleForDrowWizard()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = RaceFactory.CreateUsing(String.Empty, new CharacterClassPrototype() { ClassName = CharacterClassConstants.Wizard },
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockDice.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void RaceFactoryReturnsFemaleForDrowCleric()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = RaceFactory.CreateUsing(String.Empty, new CharacterClassPrototype() { ClassName = CharacterClassConstants.Cleric },
                mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, mockDice.Object);
            Assert.That(race.Male, Is.False);
        }
    }
}