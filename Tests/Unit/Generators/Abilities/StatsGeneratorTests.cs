using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Abilities;
using CharacterGen.Generators.Domain.Abilities;
using CharacterGen.Generators.Randomizers.Stats;
using CharacterGen.Selectors;
using CharacterGen.Selectors.Objects;
using CharacterGen.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class StatsGeneratorTests
    {
        private const Int32 baseStat = 10;

        private Mock<IStatPrioritySelector> mockStatPrioritySelector;
        private Mock<IStatAdjustmentsSelector> mockStatAdjustmentsSelector;
        private Mock<IBooleanPercentileSelector> mockBooleanPercentileSelector;
        private IStatsGenerator statsGenerator;

        private Mock<IStatsRandomizer> mockStatRandomizer;
        private Dictionary<String, Stat> randomizedStats;
        private Dictionary<String, Int32> adjustments;
        private Race race;
        private CharacterClass characterClass;
        private StatPrioritySelection statPrioritySelection;

        [SetUp]
        public void Setup()
        {
            statPrioritySelection = new StatPrioritySelection();
            mockStatPrioritySelector = new Mock<IStatPrioritySelector>();
            race = new Race();
            adjustments = new Dictionary<String, Int32>();
            mockStatAdjustmentsSelector = new Mock<IStatAdjustmentsSelector>();
            mockBooleanPercentileSelector = new Mock<IBooleanPercentileSelector>();
            statsGenerator = new StatsGenerator(mockBooleanPercentileSelector.Object, mockStatPrioritySelector.Object,
                mockStatAdjustmentsSelector.Object);
            randomizedStats = new Dictionary<String, Stat>();
            mockStatRandomizer = new Mock<IStatsRandomizer>();
            characterClass = new CharacterClass();

            statPrioritySelection.First = "first priority";
            statPrioritySelection.Second = "second priority";
            mockStatPrioritySelector.Setup(p => p.SelectFor(It.IsAny<String>())).Returns(statPrioritySelection);

            race.BaseRace = "base race";
            race.Metarace = String.Empty;
            adjustments.Add(statPrioritySelection.First, 0);
            adjustments.Add(statPrioritySelection.Second, 0);
            adjustments.Add("other stat", 0);
            mockStatAdjustmentsSelector.Setup(p => p.SelectFor(race)).Returns(adjustments);

            randomizedStats.Add(statPrioritySelection.First, new Stat { Value = baseStat });
            randomizedStats.Add(statPrioritySelection.Second, new Stat { Value = baseStat });
            randomizedStats.Add("other stat", new Stat { Value = baseStat });
            mockStatRandomizer.Setup(r => r.Randomize()).Returns(randomizedStats);

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
            mockStatPrioritySelector.Verify(p => p.SelectFor(characterClass.ClassName), Times.Once);
        }

        [Test]
        public void PrioritizesStatsByClass()
        {
            randomizedStats["other stat"].Value = 18;
            randomizedStats[statPrioritySelection.First].Value = 16;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(18));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(16));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void AdjustsStatsByBaseRace()
        {
            adjustments["other stat"] = 1;
            adjustments[statPrioritySelection.First] = -1;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat - 1));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat + 1));
        }

        [Test]
        public void AdjustStatsAfterPrioritizing()
        {
            randomizedStats["other stat"].Value = 18;
            randomizedStats[statPrioritySelection.First].Value = 16;
            adjustments["other stat"] = 9266;
            adjustments[statPrioritySelection.First] = -10;
            adjustments[statPrioritySelection.Second] = -7;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(8));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(9));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat + 9266));
        }

        [Test]
        public void IncreaseFirstPriorityStat()
        {
            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void IncreaseSecondPriorityStat()
        {
            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(false);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void IncreasingIsAfterPrioritizationAndAdjustments()
        {
            randomizedStats["other stat"].Value = 18;
            randomizedStats[statPrioritySelection.First].Value = 16;
            adjustments["other stat"] = 9266;
            adjustments[statPrioritySelection.First] = -10;
            adjustments[statPrioritySelection.Second] = -7;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);
            characterClass.Level = 4;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(9));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(9));
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
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat + increase));
        }

        [Test]
        public void DetermineWhichStatToIncreasePerLevel()
        {
            characterClass.Level = 12;
            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat))
                .Returns(true).Returns(false).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat + 2));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void IncreasesIgnorePrioritization()
        {
            characterClass.Level = 12;
            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat))
                .Returns(true).Returns(false).Returns(false);

            foreach (var stat in randomizedStats.Values)
                stat.Value = baseStat;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat + 2));
            Assert.That(stats["other stat"].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void CannotHaveStatLessThan3()
        {
            adjustments[statPrioritySelection.First] = -9266;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(3));
        }
    }
}