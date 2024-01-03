﻿using DnDGen.CharacterGen.Generators.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class HeroicAbilitiesRandomizerTests
    {
        private const int min = 16;
        private const int max = 18;
        private const int middle = (max + min) / 2;

        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            mockDice.Setup(d => d.Roll(3).d(6).AsIndividualRolls()).Returns(new[] { 1, 1, 1 });

            randomizer = new HeroicAbilitiesRandomizer(mockDice.Object);
        }

        [Test]
        public void HeroicAbilitiesCalls3d6OnceTimesPerAbility()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsIndividualRolls(), Times.Exactly(stats.Count));
        }

        [Test]
        public void HeroicAbilityRollsTreatsOnesAsSixes()
        {
            var sequence = mockDice.SetupSequence(d => d.Roll(3).d(6).AsIndividualRolls());
            for (var i = 0; i < 6; i++)
                sequence = sequence.Returns(new[] { 1, 5, 6 });

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(17));
        }

        [Test]
        public void AllowIfAbilityAverageIsInRange()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange(min, max));
        }

        [Test]
        public void RerollIfAbilityAverageIsLessThanSixteen()
        {
            var sequence = mockDice.SetupSequence(d => d.Roll(3).d(6).AsIndividualRolls());
            for (var i = 0; i < 6; i++)
                sequence = sequence.Returns(new[] { 5, 5, 5 }); //invalid average

            for (var i = 0; i < 6; i++)
                sequence = sequence.Returns(new[] { 1, 6, 5 }); //valid average

            var stats = randomizer.Randomize();
            var average = stats.Average(s => s.Value.Value);
            Assert.That(average, Is.EqualTo(middle));
        }

        [Test]
        public void DefaultValueIs16()
        {
            mockDice.Setup(d => d.Roll(3).d(6).AsIndividualRolls()).Returns(new[] { 2, 2, 2 });

            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(16));
        }
    }
}