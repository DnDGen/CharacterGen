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
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private IStatsGenerator statsGenerator;

        private Mock<IStatsRandomizer> mockStatsRandomizer;
        private Mock<ISetStatsRandomizer> mockSetStatsRandomizer;
        private Dictionary<String, Stat> randomizedStats;
        private Dictionary<String, Int32> adjustments;
        private Race race;
        private CharacterClass characterClass;
        private StatPrioritySelection statPrioritySelection;
        private List<String> undead;

        [SetUp]
        public void Setup()
        {
            mockStatPrioritySelector = new Mock<IStatPrioritySelector>();
            mockStatAdjustmentsSelector = new Mock<IStatAdjustmentsSelector>();
            mockBooleanPercentileSelector = new Mock<IBooleanPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            statsGenerator = new StatsGenerator(mockBooleanPercentileSelector.Object, mockStatPrioritySelector.Object,
                mockStatAdjustmentsSelector.Object, mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);

            mockStatsRandomizer = new Mock<IStatsRandomizer>();
            mockSetStatsRandomizer = new Mock<ISetStatsRandomizer>();

            randomizedStats = new Dictionary<String, Stat>();
            race = new Race();
            adjustments = new Dictionary<String, Int32>();
            statPrioritySelection = new StatPrioritySelection();
            characterClass = new CharacterClass();
            undead = new List<String>();

            characterClass.ClassName = "class name";
            characterClass.Level = 1;
            randomizedStats[StatConstants.Charisma] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Constitution] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Dexterity] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Intelligence] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Strength] = new Stat { Value = baseStat };
            randomizedStats[StatConstants.Wisdom] = new Stat { Value = baseStat };
            statPrioritySelection.First = StatConstants.Strength;
            statPrioritySelection.Second = StatConstants.Wisdom;
            race.BaseRace = "base race";
            race.Metarace = "metarace";
            adjustments[StatConstants.Charisma] = 0;
            adjustments[StatConstants.Constitution] = 0;
            adjustments[StatConstants.Dexterity] = 0;
            adjustments[StatConstants.Intelligence] = 0;
            adjustments[StatConstants.Strength] = 0;
            adjustments[StatConstants.Wisdom] = 0;

            mockSetStatsRandomizer.SetupAllProperties();

            mockStatPrioritySelector.Setup(p => p.SelectFor(It.IsAny<String>())).Returns(statPrioritySelection);
            mockStatAdjustmentsSelector.Setup(p => p.SelectFor(race)).Returns(adjustments);
            mockStatsRandomizer.Setup(r => r.Randomize()).Returns(randomizedStats);
            mockSetStatsRandomizer.Setup(r => r.Randomize()).Returns(randomizedStats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead))
                .Returns(undead);
        }

        [Test]
        public void RandomizesStatValues()
        {
            statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            mockStatsRandomizer.Verify(r => r.Randomize(), Times.Once);
        }

        [Test]
        public void GetPrioritiesByClassName()
        {
            statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            mockStatPrioritySelector.Verify(p => p.SelectFor(characterClass.ClassName), Times.Once);
        }

        [Test]
        public void PrioritizesStatsByClass()
        {
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[statPrioritySelection.First].Value = 16;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(18));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void AdjustsStatsByRace()
        {
            adjustments[StatConstants.Dexterity] = 1;
            adjustments[statPrioritySelection.First] = -1;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
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

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(8));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat + 9266));
        }

        [Test]
        public void IncreaseFirstPriorityStat()
        {
            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void IncreaseSecondPriorityStat()
        {
            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(false);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
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

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
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

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat + increase));
        }

        [Test]
        public void DetermineWhichStatToIncreasePerLevel()
        {
            characterClass.Level = 12;
            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat))
                .Returns(true).Returns(false).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
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

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[statPrioritySelection.First].Value, Is.EqualTo(baseStat + 1));
            Assert.That(stats[statPrioritySelection.Second].Value, Is.EqualTo(baseStat + 2));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void CannotHaveStatLessThan1()
        {
            adjustments[StatConstants.Strength] = -9266;
            randomizedStats[StatConstants.Strength].Value = 3;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(1));
        }

        [Test]
        public void UndeadHaveConstitution0()
        {
            undead.Add(race.Metarace);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(0));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(baseStat));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(baseStat));
        }

        [Test]
        public void AdjustSetStats()
        {
            randomizedStats[StatConstants.Charisma].Value = 10;
            randomizedStats[StatConstants.Constitution].Value = 11;
            randomizedStats[StatConstants.Dexterity].Value = 9;
            randomizedStats[StatConstants.Intelligence].Value = 12;
            randomizedStats[StatConstants.Strength].Value = 8;
            randomizedStats[StatConstants.Wisdom].Value = 13;

            adjustments[StatConstants.Charisma] = 1;
            adjustments[StatConstants.Constitution] = -2;
            adjustments[StatConstants.Dexterity] = 3;
            adjustments[StatConstants.Intelligence] = -4;
            adjustments[StatConstants.Strength] = 5;
            adjustments[StatConstants.Wisdom] = -6;

            characterClass.Level = 20;
            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat))
                .Returns(true).Returns(true).Returns(false).Returns(true).Returns(false);

            mockSetStatsRandomizer.Object.AllowAdjustments = true;

            var stats = statsGenerator.GenerateWith(mockSetStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(12));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(4));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(21));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(8));
        }

        [Test]
        public void DoNotAdjustSetStats()
        {
            randomizedStats[StatConstants.Charisma].Value = 10;
            randomizedStats[StatConstants.Constitution].Value = 11;
            randomizedStats[StatConstants.Dexterity].Value = 9;
            randomizedStats[StatConstants.Intelligence].Value = 12;
            randomizedStats[StatConstants.Strength].Value = 8;
            randomizedStats[StatConstants.Wisdom].Value = 13;

            adjustments[StatConstants.Charisma] = 1;
            adjustments[StatConstants.Constitution] = -2;
            adjustments[StatConstants.Dexterity] = 3;
            adjustments[StatConstants.Intelligence] = -4;
            adjustments[StatConstants.Strength] = 5;
            adjustments[StatConstants.Wisdom] = -6;

            characterClass.Level = 20;
            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat))
                .Returns(true).Returns(true).Returns(false).Returns(true).Returns(false);

            mockSetStatsRandomizer.Object.AllowAdjustments = false;

            var stats = statsGenerator.GenerateWith(mockSetStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(12));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(8));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(13));
        }

        [Test]
        public void CannotHaveSetStatBelow1IfAdjustingStats()
        {
            randomizedStats[StatConstants.Charisma].Value = -10;
            randomizedStats[StatConstants.Constitution].Value = -11;
            randomizedStats[StatConstants.Dexterity].Value = -9;
            randomizedStats[StatConstants.Intelligence].Value = -12;
            randomizedStats[StatConstants.Strength].Value = -8;
            randomizedStats[StatConstants.Wisdom].Value = -13;

            adjustments[StatConstants.Charisma] = -1;
            adjustments[StatConstants.Constitution] = -2;
            adjustments[StatConstants.Dexterity] = -3;
            adjustments[StatConstants.Intelligence] = -4;
            adjustments[StatConstants.Strength] = -5;
            adjustments[StatConstants.Wisdom] = -6;

            mockSetStatsRandomizer.Object.AllowAdjustments = true;

            var stats = statsGenerator.GenerateWith(mockSetStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(1));
        }

        [Test]
        public void CannotHaveSetStatBelow1IfNotAdjustingStats()
        {
            randomizedStats[StatConstants.Charisma].Value = -10;
            randomizedStats[StatConstants.Constitution].Value = -11;
            randomizedStats[StatConstants.Dexterity].Value = -9;
            randomizedStats[StatConstants.Intelligence].Value = -12;
            randomizedStats[StatConstants.Strength].Value = -8;
            randomizedStats[StatConstants.Wisdom].Value = -13;

            adjustments[StatConstants.Charisma] = -1;
            adjustments[StatConstants.Constitution] = -2;
            adjustments[StatConstants.Dexterity] = -3;
            adjustments[StatConstants.Intelligence] = -4;
            adjustments[StatConstants.Strength] = -5;
            adjustments[StatConstants.Wisdom] = -6;

            mockSetStatsRandomizer.Object.AllowAdjustments = false;

            var stats = statsGenerator.GenerateWith(mockSetStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(1));
        }

        [Test]
        public void UndeadStillHaveConstitution0WhenNotAdjustSetStats()
        {
            randomizedStats[StatConstants.Charisma].Value = 10;
            randomizedStats[StatConstants.Constitution].Value = 11;
            randomizedStats[StatConstants.Dexterity].Value = 9;
            randomizedStats[StatConstants.Intelligence].Value = 12;
            randomizedStats[StatConstants.Strength].Value = 8;
            randomizedStats[StatConstants.Wisdom].Value = 13;

            adjustments[StatConstants.Charisma] = 1;
            adjustments[StatConstants.Constitution] = -2;
            adjustments[StatConstants.Dexterity] = 3;
            adjustments[StatConstants.Intelligence] = -4;
            adjustments[StatConstants.Strength] = 5;
            adjustments[StatConstants.Wisdom] = -6;

            characterClass.Level = 20;
            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat))
                .Returns(true).Returns(true).Returns(false).Returns(true).Returns(false);

            mockSetStatsRandomizer.Object.AllowAdjustments = false;
            undead.Add(race.Metarace);

            var stats = statsGenerator.GenerateWith(mockSetStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(0));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(12));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(8));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(13));
        }

        [Test]
        public void UndeadStillHaveConstitution0WhenAdjustingSetStatsBelow1()
        {
            randomizedStats[StatConstants.Charisma].Value = -10;
            randomizedStats[StatConstants.Constitution].Value = -11;
            randomizedStats[StatConstants.Dexterity].Value = -9;
            randomizedStats[StatConstants.Intelligence].Value = -12;
            randomizedStats[StatConstants.Strength].Value = -8;
            randomizedStats[StatConstants.Wisdom].Value = -13;

            adjustments[StatConstants.Charisma] = -1;
            adjustments[StatConstants.Constitution] = -2;
            adjustments[StatConstants.Dexterity] = -3;
            adjustments[StatConstants.Intelligence] = -4;
            adjustments[StatConstants.Strength] = -5;
            adjustments[StatConstants.Wisdom] = -6;

            mockSetStatsRandomizer.Object.AllowAdjustments = false;

            mockSetStatsRandomizer.Object.AllowAdjustments = false;
            undead.Add(race.Metarace);

            var stats = statsGenerator.GenerateWith(mockSetStatsRandomizer.Object, characterClass, race);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(0));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(1));
        }
    }
}