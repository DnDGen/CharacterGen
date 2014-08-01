using System;
using System.Collections.Generic;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Generators.Abilities;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Generators.Interfaces.Randomizers.Stats;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class StatsGeneratorTests
    {
        private const Int32 baseStat = 10;

        private Mock<IDice> mockDice;
        private Mock<IStatPrioritySelector> mockStatPrioritySelector;
        private Mock<IStatAdjustmentsSelector> mockStatAdjustmentsSelector;
        private IStatsGenerator statsGenerator;

        private Mock<IStatsRandomizer> mockStatRandomizer;
        private Dictionary<String, Stat> randomizedStats;
        private Dictionary<String, Int32> adjustments;
        private Race race;
        private CharacterClass characterClass;
        private StatPriority statPriority;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<IDice>();

            statPriority = new StatPriority();
            statPriority.FirstPriority = "first priority";
            statPriority.SecondPriority = "second priority";
            mockStatPrioritySelector = new Mock<IStatPrioritySelector>();
            mockStatPrioritySelector.Setup(p => p.SelectStatPrioritiesFor(It.IsAny<String>())).Returns(statPriority);

            race = new Race();
            race.BaseRace = "base race";
            race.Metarace = String.Empty;
            adjustments = new Dictionary<String, Int32>();
            adjustments.Add(statPriority.FirstPriority, 0);
            adjustments.Add(statPriority.SecondPriority, 0);
            adjustments.Add("other stat", 0);
            mockStatAdjustmentsSelector = new Mock<IStatAdjustmentsSelector>();
            mockStatAdjustmentsSelector.Setup(p => p.SelectAdjustmentsFor(race)).Returns(adjustments);

            statsGenerator = new StatsGenerator(mockDice.Object, mockStatPrioritySelector.Object,
                mockStatAdjustmentsSelector.Object);

            randomizedStats = new Dictionary<String, Stat>();
            randomizedStats.Add(statPriority.FirstPriority, new Stat { Value = baseStat });
            randomizedStats.Add(statPriority.SecondPriority, new Stat { Value = baseStat });
            randomizedStats.Add("other stat", new Stat { Value = baseStat });
            mockStatRandomizer = new Mock<IStatsRandomizer>();
            mockStatRandomizer.Setup(r => r.Randomize()).Returns(randomizedStats);

            characterClass = new CharacterClass();
            characterClass.ClassName = "class name";
        }

        [Test]
        public void RandomizesStatValues()
        {
            statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            mockStatRandomizer.Verify(r => r.Randomize(), Times.Once);
        }

        [Test]
        public void GetPrioritiesByClassName()
        {
            statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            mockStatPrioritySelector.Verify(p => p.SelectStatPrioritiesFor(characterClass.ClassName), Times.Once);
        }

        [Test]
        public void PrioritizesStatsByClass()
        {
            randomizedStats["other stat"].Value = 18;
            randomizedStats[statPriority.FirstPriority].Value = 16;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(18));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(16));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void AdjustsStatsByBaseRace()
        {
            adjustments["other stat"] = 1;
            adjustments[statPriority.FirstPriority] = -1;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat - 1));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(baseStat));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat + 1));
        }

        [Test]
        public void AdjustStatsAfterPrioritizing()
        {
            randomizedStats["other stat"].Value = 18;
            randomizedStats[statPriority.FirstPriority].Value = 16;
            adjustments["other stat"] = 9266;
            adjustments[statPriority.FirstPriority] = -10;
            adjustments[statPriority.SecondPriority] = -7;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(8));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(9));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat + 9266));
        }

        [Test]
        public void LowOnD2IncreasesFirstPriorityStat()
        {
            characterClass.Level = 4;
            mockDice.Setup(d => d.d2(1)).Returns(1);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(baseStat));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void HighOnD2IncreasesSecondPriorityStat()
        {
            characterClass.Level = 4;
            mockDice.Setup(d => d.d2(1)).Returns(2);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void IncreasingIsAfterPrioritizaionAndAdjustments()
        {
            randomizedStats["other stat"].Value = 18;
            randomizedStats[statPriority.FirstPriority].Value = 16;
            adjustments["other stat"] = 9266;
            adjustments[statPriority.FirstPriority] = -10;
            adjustments[statPriority.SecondPriority] = -7;
            mockDice.Setup(d => d.d2(1)).Returns(1);
            characterClass.Level = 4;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(9));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(9));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat + 9266));
        }

        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 1)]
        [TestCase(5, 1)]
        [TestCase(6, 1)]
        [TestCase(7, 1)]
        [TestCase(8, 2)]
        [TestCase(9, 2)]
        [TestCase(10, 2)]
        [TestCase(11, 2)]
        [TestCase(12, 3)]
        [TestCase(13, 3)]
        [TestCase(14, 3)]
        [TestCase(15, 3)]
        [TestCase(16, 4)]
        [TestCase(17, 4)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        public void IncreaseStat(Int32 level, Int32 increase)
        {
            characterClass.Level = level;
            mockDice.Setup(d => d.d2(1)).Returns(1);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + increase));
        }

        [Test]
        public void RollWhichStatToIncreasePerLevel()
        {
            characterClass.Level = 12;
            mockDice.SetupSequence(d => d.d2(1)).Returns(1).Returns(2).Returns(1);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + 2));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void IncreasesIgnorePrioritization()
        {
            characterClass.Level = 12;
            mockDice.SetupSequence(d => d.d2(1)).Returns(1).Returns(2).Returns(2);

            foreach (var stat in randomizedStats.Values)
                stat.Value = baseStat;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats[statPriority.SecondPriority].Value, Is.EqualTo(baseStat + 2));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void CannotHaveStatLessThanOne()
        {
            adjustments[statPriority.FirstPriority] = -9266;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPriority.FirstPriority].Value, Is.EqualTo(1));
        }
    }
}