﻿using System;
using System.Linq;
using D20Dice;
using Moq;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Generators.Randomizers.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class GoodStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        private const Int32 min = 13;
        private const Int32 max = 15;
        private Int32 middle;

        [SetUp]
        public void Setup()
        {
            middle = (max + min) / 2;

            mockDice = new Mock<IDice>();
            mockDice.SetupSequence(d => d.Roll(3).d6()).Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle);

            randomizer = new GoodStatsRandomizer(mockDice.Object);
        }

        [Test]
        public void GoodCalls3d6PerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d6(), Times.Exactly(stats.Count));
        }

        [Test]
        public void AllowIfStatAverageIsInRange()
        {
            var stats = randomizer.Randomize();
            var average = stats.Values.Average(s => s.Value);
            Assert.That(average, Is.InRange<Double>(min, max));
        }

        [Test]
        public void RerollIfStatAverageIsGreaterThanFifteen()
        {
            mockDice.SetupSequence(d => d.Roll(3).d6())
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(9000) //invalid average
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d6(), Times.Exactly(stats.Count * 2));
        }

        [Test]
        public void RerollIfStatAverageIsLessThanThirteen()
        {
            mockDice.SetupSequence(d => d.Roll(3).d6())
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(3) //invalid average
                .Returns(min).Returns(max).Returns(middle).Returns(min - 1).Returns(max + 1).Returns(middle); //valid average

            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(3).d6(), Times.Exactly(stats.Count * 2));
        }
    }
}