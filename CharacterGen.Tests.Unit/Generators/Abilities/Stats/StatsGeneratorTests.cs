using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using CharacterGen.Randomizers.Stats;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Abilities.Stats
{
    [TestFixture]
    public class StatsGeneratorTests
    {
        private Mock<IStatAdjustmentsSelector> mockStatAdjustmentsSelector;
        private Mock<IBooleanPercentileSelector> mockBooleanPercentileSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private IStatsGenerator statsGenerator;

        private Mock<IStatsRandomizer> mockStatsRandomizer;
        private Mock<ISetStatsRandomizer> mockSetStatsRandomizer;
        private Dictionary<string, Stat> randomizedStats;
        private Dictionary<string, int> adjustments;
        private Race race;
        private CharacterClass characterClass;
        private List<string> undead;
        private List<string> statPriorities;

        [SetUp]
        public void Setup()
        {
            mockStatAdjustmentsSelector = new Mock<IStatAdjustmentsSelector>();
            mockBooleanPercentileSelector = new Mock<IBooleanPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            statsGenerator = new StatsGenerator(mockBooleanPercentileSelector.Object, mockStatAdjustmentsSelector.Object, mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);

            mockStatsRandomizer = new Mock<IStatsRandomizer>();
            mockSetStatsRandomizer = new Mock<ISetStatsRandomizer>();

            randomizedStats = new Dictionary<string, Stat>();
            race = new Race();
            adjustments = new Dictionary<string, int>();
            characterClass = new CharacterClass();
            undead = new List<string>();
            statPriorities = new List<string>();

            characterClass.Name = "class name";
            characterClass.Level = 1;
            race.BaseRace = "base race";
            race.Metarace = "metarace";

            randomizedStats[StatConstants.Charisma] = new Stat(StatConstants.Charisma);
            randomizedStats[StatConstants.Constitution] = new Stat(StatConstants.Constitution);
            randomizedStats[StatConstants.Dexterity] = new Stat(StatConstants.Dexterity);
            randomizedStats[StatConstants.Intelligence] = new Stat(StatConstants.Intelligence);
            randomizedStats[StatConstants.Strength] = new Stat(StatConstants.Strength);
            randomizedStats[StatConstants.Wisdom] = new Stat(StatConstants.Wisdom);

            statPriorities.Add(StatConstants.Strength);
            statPriorities.Add(StatConstants.Wisdom);

            adjustments[StatConstants.Charisma] = 0;
            adjustments[StatConstants.Constitution] = 0;
            adjustments[StatConstants.Dexterity] = 0;
            adjustments[StatConstants.Intelligence] = 0;
            adjustments[StatConstants.Strength] = 0;
            adjustments[StatConstants.Wisdom] = 0;

            mockSetStatsRandomizer.SetupAllProperties();

            mockStatAdjustmentsSelector.Setup(p => p.SelectFor(race)).Returns(adjustments);
            mockStatsRandomizer.Setup(r => r.Randomize()).Returns(randomizedStats);
            mockSetStatsRandomizer.Setup(r => r.Randomize()).Returns(randomizedStats);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead)).Returns(undead);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.StatPriorities, characterClass.Name)).Returns(statPriorities);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());
        }

        [Test]
        public void RandomizesStatValues()
        {
            statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            mockStatsRandomizer.Verify(r => r.Randomize(), Times.Once);
        }

        [Test]
        public void PrioritizesStatsByClass()
        {
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[StatConstants.Strength].Value = 16;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(18));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
        }

        private void AssertStats(Dictionary<string, Stat> stats)
        {
            foreach (var statKVP in stats)
            {
                var stat = statKVP.Value;
                Assert.That(stat.Name, Is.EqualTo(statKVP.Key));
                Assert.That(stat.Value, Is.Positive);
            }
        }

        [Test]
        public void IfMultipleSecondPriorityStats_DoNotCompeteAmongstThemselves()
        {
            statPriorities.Add(StatConstants.Charisma);
            randomizedStats[StatConstants.Charisma].Value = 17;
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[StatConstants.Strength].Value = 16;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(18));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(17));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
        }

        [Test]
        public void IfMultipleSecondPriorityStats_StillHigherThanNonPriorityStats()
        {
            statPriorities.Add(StatConstants.Charisma);
            randomizedStats[StatConstants.Charisma].Value = 9;
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[StatConstants.Strength].Value = 16;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(18));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(9));
        }

        [Test]
        public void DoNotPrioritizeSecondPriorityStatMoreThanOnce()
        {
            statPriorities.Add(StatConstants.Charisma);
            randomizedStats[StatConstants.Strength].Value = 19;
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[StatConstants.Constitution].Value = 17;
            randomizedStats[StatConstants.Intelligence].Value = 16;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(19));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(18));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(17));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(16));
        }

        [Test]
        public void DoNotPrioritizeIfAllValuesTheSame()
        {
            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(10));
        }

        [Test]
        public void DoNotPrioritizeSecondaryPrioritiesIfTheyAreAllAlreadyGreaterThanNonPriorityStats()
        {
            statPriorities.Add(StatConstants.Charisma);
            randomizedStats[StatConstants.Strength].Value = 17;
            randomizedStats[StatConstants.Charisma].Value = 18;
            randomizedStats[StatConstants.Wisdom].Value = 19;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(19));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(17));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(18));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(10));
        }

        [Test]
        public void IfNoSecondPriorityStats_OnlyPrioritiesTheFirst()
        {
            statPriorities.Remove(StatConstants.Strength);
            randomizedStats[StatConstants.Strength].Value = 16;
            randomizedStats[StatConstants.Dexterity].Value = 12;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(12));
        }

        [Test]
        public void DoNotPrioritizeStatsIfNoPriorities()
        {
            statPriorities.Clear();
            randomizedStats[StatConstants.Charisma].Value = 17;
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[StatConstants.Strength].Value = 16;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(16));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(17));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(18));
        }

        [Test]
        public void AdjustsStatsByRace()
        {
            adjustments[StatConstants.Dexterity] = 1;
            adjustments[StatConstants.Strength] = -1;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(11));
        }

        [Test]
        public void AdjustStatsAfterPrioritizing()
        {
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[StatConstants.Strength].Value = 16;
            adjustments[StatConstants.Dexterity] = 9266;
            adjustments[StatConstants.Strength] = -10;
            adjustments[StatConstants.Wisdom] = -7;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(8));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(9276));
        }

        [Test]
        public void IncreaseFirstPriorityStat()
        {
            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
        }

        [Test]
        public void IncreaseSecondPriorityStat()
        {
            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(false);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
        }

        [Test]
        public void IncreaseRandomPriorityStatThatIsNotFirstPriority()
        {
            statPriorities.Add(StatConstants.Charisma);
            characterClass.Level = 4;

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(false);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
        }

        [Test]
        public void DoNotIncreaseSecondPriorityStatIfNone()
        {
            statPriorities.Remove(StatConstants.Strength);
            characterClass.Level = 4;

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(false);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
        }

        [Test]
        public void IncreaseRandomStatIfNoPriorities()
        {
            statPriorities.Clear();
            characterClass.Level = 4;

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(false);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(2));

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(11));
        }

        [Test]
        public void IncreasingIsAfterPrioritizationAndAdjustments()
        {
            randomizedStats[StatConstants.Dexterity].Value = 18;
            randomizedStats[StatConstants.Strength].Value = 16;
            adjustments[StatConstants.Dexterity] = 9266;
            adjustments[StatConstants.Strength] = -10;
            adjustments[StatConstants.Wisdom] = -7;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);
            characterClass.Level = 4;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(9276));
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
        public void IncreaseStat(int level, int increase)
        {
            characterClass.Level = level;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(10 + increase));
        }

        [Test]
        public void DetermineWhichStatToIncreasePerLevel()
        {
            characterClass.Level = 12;
            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat))
                .Returns(true).Returns(false).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(12));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
        }

        [Test]
        public void IncreasesIgnorePrioritization()
        {
            characterClass.Level = 12;
            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat))
                .Returns(true).Returns(false).Returns(false);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(12));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
        }

        [Test]
        public void CannotHaveStatLessThan1()
        {
            adjustments[StatConstants.Strength] = -9266;
            randomizedStats[StatConstants.Strength].Value = 3;

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(1));
        }

        [Test]
        public void SetMinimumStatBeforeIncreasingStats()
        {
            adjustments[StatConstants.Strength] = -9266;
            randomizedStats[StatConstants.Strength].Value = 3;

            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(2));
        }

        [Test]
        public void UndeadHaveNoConstitution()
        {
            undead.Add(race.Metarace);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(10));
            Assert.That(stats.Keys, Is.All.Not.EqualTo(StatConstants.Constitution));
            Assert.That(stats.Count, Is.EqualTo(5));
        }

        [Test]
        public void UndeadDoNotTryToIncreaseConstitution()
        {
            statPriorities.Clear();
            statPriorities.Add(StatConstants.Constitution);
            statPriorities.Add(StatConstants.Strength);

            undead.Add(race.Metarace);
            characterClass.Level = 4;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityStat)).Returns(true);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(10));
            Assert.That(stats.Keys, Is.All.Not.EqualTo(StatConstants.Constitution));
            Assert.That(stats.Count, Is.EqualTo(5));
        }

        [Test]
        public void UndeadDoNotTryToIncreaseConstitutionWhenNoPriorities()
        {
            statPriorities.Clear();

            undead.Add(race.Metarace);
            characterClass.Level = 4;

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns(StatConstants.Constitution)
                .Returns(StatConstants.Intelligence);

            var stats = statsGenerator.GenerateWith(mockStatsRandomizer.Object, characterClass, race);
            AssertStats(stats);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(11));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(10));
            Assert.That(stats.Keys, Is.All.Not.EqualTo(StatConstants.Constitution));
            Assert.That(stats.Count, Is.EqualTo(5));
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
            AssertStats(stats);
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
            AssertStats(stats);
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
            AssertStats(stats);
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
            AssertStats(stats);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Constitution].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(1));
        }

        [Test]
        public void UndeadStillHaveNoConstitutionWhenNotAdjustSetStats()
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
            AssertStats(stats);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(10));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(9));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(12));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(8));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(13));
            Assert.That(stats.Keys, Is.All.Not.EqualTo(StatConstants.Constitution));
            Assert.That(stats.Count, Is.EqualTo(5));
        }

        [Test]
        public void UndeadStillHaveNoConstitutionWhenAdjustingSetStatsBelow1()
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
            AssertStats(stats);
            Assert.That(stats[StatConstants.Charisma].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Dexterity].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Intelligence].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Strength].Value, Is.EqualTo(1));
            Assert.That(stats[StatConstants.Wisdom].Value, Is.EqualTo(1));
            Assert.That(stats.Keys, Is.All.Not.EqualTo(StatConstants.Constitution));
            Assert.That(stats.Count, Is.EqualTo(5));
        }
    }
}