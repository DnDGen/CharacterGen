﻿using CharacterGen.Generators.Domain.Randomizers.Stats;
using CharacterGen.Generators.Randomizers.Stats;
using RollGen;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.Stats
{
    [TestFixture]
    public class BestOfFourStatsRandomizerTests
    {
        private IStatsRandomizer randomizer;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();
            randomizer = new BestOfFourStatsRandomizer(mockDice.Object);

            mockDice.Setup(d => d.Roll(1).d6()).Returns(1);
        }

        [Test]
        public void BestOfFourStatsCalls1d6FourTimesPerStat()
        {
            var stats = randomizer.Randomize();
            mockDice.Verify(d => d.Roll(1).d6(), Times.Exactly(stats.Count * 4));
        }

        [Test]
        public void BestOfFourIgnoresLowestRollPerStat()
        {
            mockDice.SetupSequence(d => d.Roll(1).d6()).Returns(2).Returns(1).Returns(3).Returns(4);

            var stats = randomizer.Randomize();
            var stat = stats.Values.First();
            Assert.That(stat.Value, Is.EqualTo(9));
        }
    }
}