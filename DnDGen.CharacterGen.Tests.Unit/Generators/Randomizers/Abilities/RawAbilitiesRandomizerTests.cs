﻿using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Generators.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using Moq;
using NUnit.Framework;
using DnDGen.RollGen;

namespace DnDGen.CharacterGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class RawAbilitiesRandomizerTests
    {
        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            randomizer = new RawAbilitiesRandomizer(mockDice.Object, generator);

            mockDice.Setup(d => d.Roll(It.IsAny<int>()).d(It.IsAny<int>()).AsSum()).Returns(1);
        }

        [Test]
        public void RawAbilitiesCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count));
        }

        [Test]
        public void RawAbilitiesReturnsUnmodified3d6PerStat()
        {
            mockDice.Setup(d => d.Roll(3).d(6).AsSum()).Returns(12);
            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(12));
        }

        [Test]
        public void RolledAbilitiesAreAlwaysAllowed()
        {
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(9266).Returns(-42).Returns(int.MaxValue)
                .Returns(int.MinValue).Returns(0).Returns(1337);

            var stats = randomizer.Randomize();

            Assert.That(stats[AbilityConstants.Charisma].Value, Is.EqualTo(9266));
            Assert.That(stats[AbilityConstants.Constitution].Value, Is.EqualTo(-42));
            Assert.That(stats[AbilityConstants.Dexterity].Value, Is.EqualTo(int.MaxValue));
            Assert.That(stats[AbilityConstants.Intelligence].Value, Is.EqualTo(int.MinValue));
            Assert.That(stats[AbilityConstants.Strength].Value, Is.EqualTo(0));
            Assert.That(stats[AbilityConstants.Wisdom].Value, Is.EqualTo(1337));
        }
    }
}