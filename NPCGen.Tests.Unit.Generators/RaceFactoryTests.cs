﻿using System;
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
    public class RaceFactoryTests
    {
        private Mock<IBaseRaceRandomizer> mockBaseRaceRandomizer;
        private Mock<IMetaraceRandomizer> mockMetaraceRandomizer;
        private Mock<IDice> mockDice;
        private IRaceFactory raceFactory;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            raceFactory = new RaceFactory(mockDice.Object);

            mockBaseRaceRandomizer = new Mock<IBaseRaceRandomizer>();
            mockMetaraceRandomizer = new Mock<IMetaraceRandomizer>();
        }

        [Test]
        public void RaceFactoryReturnsRandomizedBaseRace()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns("base race");

            var race = raceFactory.CreateWith(String.Empty, new CharacterClassPrototype(), mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.BaseRace, Is.EqualTo("base race"));
        }

        [Test]
        public void RaceFactoryReturnsRandomizedMetarace()
        {
            mockMetaraceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns("metarace");

            var race = raceFactory.CreateWith(String.Empty, new CharacterClassPrototype(), mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Metarace, Is.EqualTo("metarace"));
        }

        [Test]
        public void RaceFactoryReturnsMaleOnLowRoll()
        {
            mockDice.Setup(d => d.d2(1)).Returns(1);

            var race = raceFactory.CreateWith(String.Empty, new CharacterClassPrototype(), mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void RaceFactoryReturnsFemaleOnHighRoll()
        {
            mockDice.Setup(d => d.d2(1)).Returns(2);

            var race = raceFactory.CreateWith(String.Empty, new CharacterClassPrototype(), mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.False);
        }

        [Test]
        public void RaceFactoryReturnsMaleForDrowWizard()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = raceFactory.CreateWith(String.Empty, new CharacterClassPrototype() { ClassName = CharacterClassConstants.Wizard }, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.True);
        }

        [Test]
        public void RaceFactoryReturnsFemaleForDrowCleric()
        {
            mockBaseRaceRandomizer.Setup(r => r.Randomize(It.IsAny<String>(), It.IsAny<CharacterClassPrototype>())).Returns(RaceConstants.BaseRaces.Drow);

            var race = raceFactory.CreateWith(String.Empty, new CharacterClassPrototype() { ClassName = CharacterClassConstants.Cleric }, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(race.Male, Is.False);
        }
    }
}