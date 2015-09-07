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
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private IStatsGenerator statsGenerator;

        private Mock<IStatsRandomizer> mockStatRandomizer;
        private Dictionary<String, Stat> randomizedStats;
        private Dictionary<String, Int32> adjustments;
        private Race race;
        private CharacterClass characterClass;
        private StatPrioritySelection statPrioritySelection;
        private Dictionary<String, Int32> ageRolls;

        [SetUp]
        public void Setup()
        {
            mockStatPrioritySelector = new Mock<IStatPrioritySelector>();
            mockStatAdjustmentsSelector = new Mock<IStatAdjustmentsSelector>();
            mockBooleanPercentileSelector = new Mock<IBooleanPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            statsGenerator = new StatsGenerator(mockBooleanPercentileSelector.Object, mockStatPrioritySelector.Object,
                mockStatAdjustmentsSelector.Object, mockAdjustmentsSelector.Object);

            mockStatRandomizer = new Mock<IStatsRandomizer>();

            randomizedStats = new Dictionary<String, Stat>();
            race = new Race();
            adjustments = new Dictionary<String, Int32>();
            statPrioritySelection = new StatPrioritySelection();
            characterClass = new CharacterClass();
            ageRolls = new Dictionary<String, Int32>();

            characterClass.ClassName = "class name";
            randomizedStats[StatConstants.Charisma] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Constitution] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Dexterity] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Intelligence] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Strength] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Wisdom] = new Stat { Value = baseStat };
            statPrioritySelection.First = StatConstants.Strength;
            statPrioritySelection.Second = StatConstants.Wisdom;
            race.BaseRace = "base race";
            race.Metarace = String.Empty;
            adjustments[StatConstants.Charisma] = 0;
            adjustments[StatConstants.Constitution] = 0;
            adjustments[StatConstants.Dexterity] = 0;
            adjustments[StatConstants.Intelligence] = 0;
            adjustments[StatConstants.Strength] = 0;
            adjustments[StatConstants.Wisdom] = 0;
            ageRolls[AdjustmentConstants.MiddleAge] = 42;
            ageRolls[AdjustmentConstants.Old] = 600;
            ageRolls[AdjustmentConstants.Venerable] = 90210;

            var tableName = String.Format(TableNameConstants.Formattable.Adjustments.AGEGROUPRACEAges, GroupConstants.SelfTaught, race.BaseRace);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName)).Returns(ageRolls);

            mockStatPrioritySelector.Setup(p => p.SelectFor(It.IsAny<String>())).Returns(statPrioritySelection);
            mockStatAdjustmentsSelector.Setup(p => p.SelectFor(race)).Returns(adjustments);
            mockStatRandomizer.Setup(r => r.Randomize()).Returns(randomizedStats);
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
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[statPrioritySelection.First].Value = 16;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(18));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void AdjustsStatsByBaseRace()
        {
            adjustments[StatConstants.Dexterity] = 1;
            adjustments[statPrioritySelection.First] = -1;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat - 1));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat + 1));
        }

        [Test]
        public void AdjustStatsAfterPrioritizing()
        {
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[statPrioritySelection.First].Value = 16;
            adjustments[StatConstants.Dexterity] = 9266;
            adjustments[statPrioritySelection.First] = -10;
            adjustments[statPrioritySelection.Second] = -7;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(8));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat + 9266));
        }

        [Test]
        public void IncreaseFirstPriorityStat()
        {
            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void IncreaseSecondPriorityStat()
        {
            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(false);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void IncreasingIsAfterPrioritizationAndAdjustments()
        {
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[statPrioritySelection.First].Value = 16;
            adjustments[StatConstants.Dexterity] = 9266;
            adjustments[statPrioritySelection.First] = -10;
            adjustments[statPrioritySelection.Second] = -7;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);
            characterClass.Level = 4;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(9));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat + 9266));
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
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void IncreasesIgnorePrioritization()
        {
            characterClass.Level = 12;
            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat))
                .Returns(true).Returns(false).Returns(false);

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat + 2));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat));
        }

        [TestCase(41, 10, 10, 10, 10, 10, 10)]
        [TestCase(42, 9, 9, 9, 11, 11, 11)]
        [TestCase(43, 9, 9, 9, 11, 11, 11)]
        [TestCase(599, 9, 9, 9, 11, 11, 11)]
        [TestCase(600, 7, 7, 7, 12, 12, 12)]
        [TestCase(601, 7, 7, 7, 12, 12, 12)]
        [TestCase(90209, 7, 7, 7, 12, 12, 12)]
        [TestCase(90210, 4, 4, 4, 13, 13, 13)]
        [TestCase(90211, 4, 4, 4, 13, 13, 13)]
        public void ApplyAgeModifierToStats(Int32 age, Int32 strength, Int32 constitution, Int32 dexterity, Int32 intelligence, Int32 wisdom, Int32 charisma)
        {
            race.AgeInYears = age;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(charisma));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(constitution));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(dexterity));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(intelligence));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(strength));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(wisdom));
        }

        [Test]
        public void CannotHaveStatLessThan1()
        {
            adjustments[StatConstants.Strength] = -9266;
            randomizedStats[StatConstants.Strength].Value = 3;
            race.AgeInYears = 90211;

            var stats = statsGenerator.GenerateWith(mockStatRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(1));
        }
    }
}