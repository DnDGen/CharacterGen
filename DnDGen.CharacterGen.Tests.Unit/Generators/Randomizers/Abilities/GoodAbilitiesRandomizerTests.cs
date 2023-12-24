﻿using DnDGen.CharacterGen.Generators.Randomizers.Abilities;
using DnDGen.CharacterGen.Randomizers.Abilities;
using Moq;
using NUnit.Framework;
using DnDGen.RollGen;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Unit.Generators.Randomizers.Abilities
{
    [TestFixture]
    public class GoodAbilitiesRandomizerTests
    {
        private const int min = 13;
        private const int max = 15;
        private const int middle = (max + min) / 2;

        private IAbilitiesRandomizer randomizer;
        private Mock<Dice> mockDice;


        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(2);
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(middle);

            randomizer = new GoodAbilitiesRandomizer(mockDice.Object, generator);
        }

        [Test]
        public void GoodCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count));
        }

        [Test]
        public void AllowIfStatAverageIsInRange()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange(min, max));
        }

        [Test]
        public void RerollIfStatAverageIsGreaterThanFifteen()
        {
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(9000) //invalid average
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void RerollIfStatAverageIsLessThanThirteen()
        {
            mockDice.SetupSequence(d => d.Roll(3).d(6).AsSum())
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(3) //invalid average
                .Returns(min).Returns(max).Returns(middle)
                .Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d(6).AsSum(), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void DefaultValueIs13()
        {
            mockDice.Setup(d => d.Roll(3).d(6).AsSum()).Returns(9);

            var stats = randomizer.Randomize();

            foreach (var stat in stats.Values)
                Assert.That(stat.Value, Is.EqualTo(13));
        }
    }
}