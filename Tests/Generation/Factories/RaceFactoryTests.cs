using System;
using D20Dice.Dice;
using Moq;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Data.Races;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Randomizers.Races.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Factories
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
        public void FactoryReturnsRandomizedBaseRace()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<String>())).Returns("base race");

            var race = RaceFactory.CreateUsing(String.Empty, String.Empty, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object,
                mockDice.Object);
            Assert.That(race.BaseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void FactoryReturnsRandomizedMetarace()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<String>())).Returns("metarace");

            var race = RaceFactory.CreateUsing(String.Empty, String.Empty, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object,
                mockDice.Object);
            Assert.That(race.Metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void FactoryReturnsMaleOnLowRoll()
        {
            mockDice.Setup(d => d.d2(1, 0)).Returns(1);

            var race = RaceFactory.CreateUsing(String.Empty, String.Empty, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object, 
                mockDice.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void FactoryReturnsFemaleOnHighRoll()
        {
            mockDice.Setup(d => d.d2(1, 0)).Returns(2);

            var race = RaceFactory.CreateUsing(String.Empty, String.Empty, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object,
                mockDice.Object);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void FactoryReturnsMaleForDrowWizard()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<String>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = RaceFactory.CreateUsing(String.Empty, CharacterClassConstants.Wizard, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object,
                mockDice.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void FactoryReturnsFemaleForDrowCleric()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<String>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = RaceFactory.CreateUsing(String.Empty, CharacterClassConstants.Cleric, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object,
                mockDice.Object);
            Assert.That(race.Male, Is.False);
        }
    }
}