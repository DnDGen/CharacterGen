﻿using CharacterGen.Domain.Generators.Randomizers.CharacterClasses.Levels;
using Moq;
using NUnit.Framework;
using RollGen;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class MediumLevelRandomizerTests
    {
        [Test]
        public void Add5ToRoll()
        {
            var mockDice = new Mock<Dice>();
            mockDice.Setup(d => d.Roll(1).d(5).AsSum()).Returns(9266);
            var randomizer = new MediumLevelRandomizer(mockDice.Object);

            var level = randomizer.Randomize();
            Assert.That(level, Is.EqualTo(9271));
        }
    }
}