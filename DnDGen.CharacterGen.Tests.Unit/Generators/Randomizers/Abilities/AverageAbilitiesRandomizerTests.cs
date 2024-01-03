﻿using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Generators.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class AverageAbilitiesRandomizerTests
    {
        private const int min = 10;
        private const int max = 13;
        private const int middle = (max + min) / 2;

        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            mockDice.SetupSequence(d => d.Roll(3).d(2).Plus(7).AsSum<int>())
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min + 1).Returns(max - 1).Returns(middle);

            randomizer = new AverageAbilitiesRandomizer(mockDice.Object);
        }

        [Test]
        public void Randomize_CallsRollPerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(2).Plus(7).AsSum<int>(), Times.Exactly(stats.Count));
        }

        [Test]
        public void Randomize_ReturnRandomizedStats()
        {
            var stats = randomizer.Randomize();
            Assert.That(stats, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Charisma)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Wisdom));
            Assert.That(stats[AbilityConstants.Strength].Value, Is.EqualTo(12));
            Assert.That(stats[AbilityConstants.Constitution].Value, Is.EqualTo(13));
            Assert.That(stats[AbilityConstants.Dexterity].Value, Is.EqualTo(11));
            Assert.That(stats[AbilityConstants.Intelligence].Value, Is.EqualTo(11));
            Assert.That(stats[AbilityConstants.Wisdom].Value, Is.EqualTo(11));
            Assert.That(stats[AbilityConstants.Charisma].Value, Is.EqualTo(10));
        }
    }
}