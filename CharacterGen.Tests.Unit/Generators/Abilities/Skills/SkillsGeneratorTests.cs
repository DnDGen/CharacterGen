using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Generators.Abilities;
using CharacterGen.Domain.Selectors.Collections;
using CharacterGen.Domain.Selectors.Percentiles;
using CharacterGen.Domain.Selectors.Selections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Abilities.Skills
{
    [TestFixture]
    public class SkillsGeneratorTests
    {
        private ISkillsGenerator skillsGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private Mock<IBooleanPercentileSelector> mockBooleanPercentileSelector;
        private CharacterClass characterClass;
        private Dictionary<string, Stat> stats;
        private List<string> classSkills;
        private List<string> monsterClassSkills;
        private List<string> crossClassSkills;
        private Mock<ISkillSelector> mockSkillSelector;
        private int classSkillPoints;
        private int racialSkillPoints;
        private int monsterHitDice;
        private Race race;
        private List<string> specialistSkills;
        private List<string> allSkills;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockSkillSelector = new Mock<ISkillSelector>();
            mockBooleanPercentileSelector = new Mock<IBooleanPercentileSelector>();
            skillsGenerator = new SkillsGenerator(mockSkillSelector.Object, mockCollectionsSelector.Object, mockAdjustmentsSelector.Object,
                mockBooleanPercentileSelector.Object);
            characterClass = new CharacterClass();
            stats = new Dictionary<string, Stat>();
            classSkills = new List<string>();
            crossClassSkills = new List<string>();
            stats[StatConstants.Intelligence] = new Stat(StatConstants.Intelligence);
            race = new Race();
            specialistSkills = new List<string>();
            allSkills = new List<string>();
            monsterClassSkills = new List<string>();

            characterClass.Name = "class name";
            characterClass.Level = 5;
            characterClass.SpecialistFields = new[] { "specialist field" };
            race.BaseRace = "base race";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "class name")).Returns(classSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CrossClassSkills, "class name")).Returns(crossClassSkills);

            var emptyAdjustments = new Dictionary<string, int>();
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(It.IsAny<string>())).Returns(emptyAdjustments);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, characterClass.Name)).Returns(() => classSkillPoints);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, race.BaseRace)).Returns(() => racialSkillPoints);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice, race.BaseRace)).Returns(() => monsterHitDice);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "specialist field")).Returns(specialistSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills)).Returns(allSkills);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.First());
            mockSkillSelector.Setup(s => s.SelectFor(It.IsAny<string>())).Returns((string skill) => new SkillSelection { SkillName = skill, BaseStatName = StatConstants.Intelligence });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace)).Returns(monsterClassSkills);

        }

        [TestCase(1, 4)]
        [TestCase(2, 5)]
        [TestCase(3, 6)]
        [TestCase(4, 7)]
        [TestCase(5, 8)]
        [TestCase(6, 9)]
        [TestCase(7, 10)]
        [TestCase(8, 11)]
        [TestCase(9, 12)]
        [TestCase(10, 13)]
        [TestCase(11, 14)]
        [TestCase(12, 15)]
        [TestCase(13, 16)]
        [TestCase(14, 17)]
        [TestCase(15, 18)]
        [TestCase(16, 19)]
        [TestCase(17, 20)]
        [TestCase(18, 21)]
        [TestCase(19, 22)]
        [TestCase(20, 23)]
        public void GetSkillPointsPerLevel(int level, int points)
        {
            characterClass.Level = level;
            classSkillPoints = 1;
            AddClassSkills(3);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var totalRanks = skills.Sum(kvp => kvp.Value.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        private void AddClassSkills(int quantity)
        {
            while (quantity > 0)
            {
                classSkills.Add($"class skill {quantity--}");
            }
        }

        [TestCase(1, 4)]
        [TestCase(2, 8)]
        [TestCase(3, 12)]
        [TestCase(4, 16)]
        [TestCase(5, 20)]
        [TestCase(6, 24)]
        [TestCase(7, 28)]
        [TestCase(8, 32)]
        [TestCase(9, 36)]
        [TestCase(10, 40)]
        public void GetSkillPointsForClass(int classPoints, int points)
        {
            characterClass.Level = 1;
            classSkillPoints = classPoints;
            AddClassSkills(classPoints + 1);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var totalRanks = skills.Sum(kvp => kvp.Value.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        [TestCase(1, 8)]
        [TestCase(2, 10)]
        [TestCase(3, 12)]
        [TestCase(4, 14)]
        [TestCase(5, 16)]
        [TestCase(6, 18)]
        [TestCase(7, 20)]
        [TestCase(8, 22)]
        [TestCase(9, 24)]
        [TestCase(10, 26)]
        [TestCase(11, 28)]
        [TestCase(12, 30)]
        [TestCase(13, 32)]
        [TestCase(14, 34)]
        [TestCase(15, 36)]
        [TestCase(16, 38)]
        [TestCase(17, 40)]
        [TestCase(18, 42)]
        [TestCase(19, 44)]
        [TestCase(20, 46)]
        public void AddIntelligenceBonusToSkillPointsPerLevel(int level, int points)
        {
            characterClass.Level = level;
            classSkillPoints = 1;
            AddClassSkills(3);
            stats[StatConstants.Intelligence].Value = 12;

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var totalRanks = skills.Sum(kvp => kvp.Value.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        [Test]
        public void GetClassSkills()
        {
            AddClassSkills(4);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills.Keys, Is.EquivalentTo(classSkills));
        }

        [Test]
        public void GetCrossClassSkills()
        {
            AddCrossClassSkills(4);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills.Keys, Is.EquivalentTo(crossClassSkills));
        }

        private void AddCrossClassSkills(int quantity)
        {
            while (quantity > 0)
            {
                crossClassSkills.Add($"cross-class skill {quantity--}");
            }
        }

        [Test]
        public void GetSkillsFromSpecialistFields()
        {
            AddSpecialistSkills(2);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills.Keys, Is.EquivalentTo(specialistSkills));
        }

        private void AddSpecialistSkills(int quantity)
        {
            while (quantity > 0)
            {
                specialistSkills.Add($"specialist skill {quantity--}");
            }
        }

        [Test]
        public void DoNotGetDuplicateSkillsFromSpecialistFields()
        {
            AddClassSkills(1);
            specialistSkills.Add(classSkills[0]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills.Keys, Contains.Item("class skill 1"));
            Assert.That(skills.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllSkills()
        {
            AddClassSkills(2);
            AddCrossClassSkills(2);
            AddSpecialistSkills(1);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(classSkills, Is.SubsetOf(skills.Keys));
            Assert.That(crossClassSkills, Is.SubsetOf(skills.Keys));
            Assert.That(specialistSkills, Is.SubsetOf(skills.Keys));
            Assert.That(skills.Count, Is.EqualTo(5));
        }

        [Test]
        public void AssignStatsToSkills()
        {
            AddClassSkills(1);
            AddCrossClassSkills(1);
            AddSpecialistSkills(1);

            var classSkillSelection = new SkillSelection();
            classSkillSelection.BaseStatName = "stat 1";

            var crossClassSkillSelection = new SkillSelection();
            crossClassSkillSelection.BaseStatName = "stat 2";

            var specialistSkillSelection = new SkillSelection();
            specialistSkillSelection.BaseStatName = "stat 3";

            mockSkillSelector.Setup(s => s.SelectFor(classSkills[0])).Returns(classSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor(crossClassSkills[0])).Returns(crossClassSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor(specialistSkills[0])).Returns(specialistSkillSelection);

            stats["stat 1"] = new Stat("stat 1");
            stats["stat 2"] = new Stat("stat 2");
            stats["stat 3"] = new Stat("stat 3");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["class skill 1"].BaseStat, Is.EqualTo(stats["stat 1"]));
            Assert.That(skills["cross-class skill 1"].BaseStat, Is.EqualTo(stats["stat 2"]));
            Assert.That(skills["specialist skill 1"].BaseStat, Is.EqualTo(stats["stat 3"]));
        }

        [Test]
        public void SetClassSkill()
        {
            AddClassSkills(2);
            AddCrossClassSkills(2);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["class skill 1"].ClassSkill, Is.True);
            Assert.That(skills["class skill 2"].ClassSkill, Is.True);
            Assert.That(skills["cross-class skill 1"].ClassSkill, Is.False);
            Assert.That(skills["cross-class skill 2"].ClassSkill, Is.False);
        }

        [Test]
        public void SpecialistSkillsAreClassSkills()
        {
            AddSpecialistSkills(1);
            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["specialist skill 1"].ClassSkill, Is.True);
        }

        [Test]
        public void SpecialistSkillOverwritesCrossClassSkill()
        {
            AddCrossClassSkills(1);
            specialistSkills.Add(crossClassSkills[0]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["cross-class skill 1"].ClassSkill, Is.True);
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongClassSkills()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;
            AddClassSkills(2);

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(false);
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 2)))
                .Returns(classSkills[1])
                .Returns(classSkills[0])
                .Returns(classSkills[0])
                .Returns(classSkills[1])
                .Returns(classSkills[1]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["class skill 1"].Ranks, Is.EqualTo(3));
            Assert.That(skills["class skill 2"].Ranks, Is.EqualTo(2));
            Assert.That(skills["class skill 1"].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills["class skill 2"].EffectiveRanks, Is.EqualTo(2));
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongCrossClassSkills()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;
            AddCrossClassSkills(2);

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 2)))
                .Returns(crossClassSkills[1])
                .Returns(crossClassSkills[0])
                .Returns(crossClassSkills[0])
                .Returns(crossClassSkills[1])
                .Returns(crossClassSkills[1]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["cross-class skill 1"].Ranks, Is.EqualTo(3));
            Assert.That(skills["cross-class skill 2"].Ranks, Is.EqualTo(2));
            Assert.That(skills["cross-class skill 1"].EffectiveRanks, Is.EqualTo(1.5));
            Assert.That(skills["cross-class skill 2"].EffectiveRanks, Is.EqualTo(1));
        }

        [Test]
        public void AssignPointsToClassSkills()
        {
            classSkillPoints = 1;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(false);

            classSkills.Add("class skill");
            crossClassSkills.Add("cross-class skill");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["class skill"].Ranks, Is.EqualTo(8));
            Assert.That(skills["cross-class skill"].Ranks, Is.EqualTo(0));
        }

        [Test]
        public void AssignPointsToCrossClassSkills()
        {
            classSkillPoints = 1;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            classSkills.Add("class skill");
            crossClassSkills.Add("cross-class skill");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["class skill"].Ranks, Is.EqualTo(0));
            Assert.That(skills["cross-class skill"].Ranks, Is.EqualTo(8));
        }

        [Test]
        public void AssignPointsRandomlyBetweenClassAndCrossClassSkills()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;

            mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true).Returns(false).Returns(false).Returns(true).Returns(false);

            classSkills.Add("class skill");
            crossClassSkills.Add("cross-class skill");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["class skill"].Ranks, Is.EqualTo(3));
            Assert.That(skills["cross-class skill"].Ranks, Is.EqualTo(2));
        }

        [Test]
        public void CannotAssignMoreThanRankCapPerClassSkill()
        {
            classSkillPoints = 2;
            AddClassSkills(3);

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(false);

            var sequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 3)));
            for (var count = 6; count > 0; count--)
                sequence = sequence.Returns(classSkills[0])
                    .Returns(classSkills[1])
                    .Returns(classSkills[0])
                    .Returns(classSkills[2])
                    .Returns(classSkills[0]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills[classSkills[0]].Ranks, Is.EqualTo(8));
            Assert.That(skills[classSkills[0]].RankCap, Is.EqualTo(8));
            Assert.That(skills[classSkills[1]].Ranks, Is.EqualTo(6));
            Assert.That(skills[classSkills[1]].RankCap, Is.EqualTo(8));
            Assert.That(skills[classSkills[2]].Ranks, Is.EqualTo(2));
            Assert.That(skills[classSkills[2]].RankCap, Is.EqualTo(8));
        }

        [Test]
        public void CannotAssignMoreThanLevelPlusThreePointsPerCrossClassSkill()
        {
            classSkillPoints = 2;
            AddCrossClassSkills(3);

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var sequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 3)));
            for (var count = 6; count > 0; count--)
                sequence = sequence.Returns(crossClassSkills[0])
                    .Returns(crossClassSkills[1])
                    .Returns(crossClassSkills[0])
                    .Returns(crossClassSkills[2])
                    .Returns(crossClassSkills[0]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills[crossClassSkills[0]].Ranks, Is.EqualTo(8));
            Assert.That(skills[crossClassSkills[0]].RankCap, Is.EqualTo(8));
            Assert.That(skills[crossClassSkills[1]].Ranks, Is.EqualTo(6));
            Assert.That(skills[crossClassSkills[1]].RankCap, Is.EqualTo(8));
            Assert.That(skills[crossClassSkills[2]].Ranks, Is.EqualTo(2));
            Assert.That(skills[crossClassSkills[2]].RankCap, Is.EqualTo(8));
        }

        [Test]
        public void AllSkillsMaxedOut()
        {
            classSkillPoints = 3;
            crossClassSkills.Add("skill 1");
            classSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(characterClass.Level + 3));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(characterClass.Level + 3));
        }

        [Test]
        public void ApplySkillSynergyIfSufficientRanks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1", "synergy 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 3" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 3")).Returns(new[] { "synergy 4" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 4")).Returns(new[] { "synergy 5" });

            classSkillPoints = 2;
            characterClass.Level = 13;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 2");
            classSkills.Add("synergy 3");
            classSkills.Add("synergy 4");
            classSkills.Add("synergy 5");
            classSkills.Add("synergy 6");

            var sequence = mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill));
            for (var count = 0; count < 11; count++)
                sequence = sequence.Returns(false);
            for (var count = 0; count < 21; count++)
                sequence = sequence.Returns(true);

            var randomSequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 8)))
                .Returns(classSkills[0]);

            for (var index = 0; index < 10; index++)
                randomSequence = randomSequence.Returns(classSkills[index % 2]);

            randomSequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 2)))
                .Returns(crossClassSkills[0]);

            for (var index = 0; index < 20; index++)
                randomSequence = randomSequence.Returns(crossClassSkills[index % 2]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(6));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(5));
            Assert.That(skills["skill 3"].Ranks, Is.EqualTo(11));
            Assert.That(skills["skill 4"].Ranks, Is.EqualTo(10));
            Assert.That(skills["synergy 1"].Ranks, Is.EqualTo(0));
            Assert.That(skills["synergy 2"].Ranks, Is.EqualTo(0));
            Assert.That(skills["synergy 3"].Ranks, Is.EqualTo(0));
            Assert.That(skills["synergy 4"].Ranks, Is.EqualTo(0));
            Assert.That(skills["skill 1"].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills["skill 2"].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills["skill 3"].EffectiveRanks, Is.EqualTo(5.5));
            Assert.That(skills["skill 4"].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills["synergy 1"].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills["synergy 2"].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills["synergy 3"].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills["synergy 4"].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills["skill 1"].Bonus, Is.EqualTo(0));
            Assert.That(skills["skill 2"].Bonus, Is.EqualTo(0));
            Assert.That(skills["skill 3"].Bonus, Is.EqualTo(0));
            Assert.That(skills["skill 4"].Bonus, Is.EqualTo(0));
            Assert.That(skills["synergy 1"].Bonus, Is.EqualTo(2));
            Assert.That(skills["synergy 2"].Bonus, Is.EqualTo(2));
            Assert.That(skills["synergy 3"].Bonus, Is.EqualTo(2));
            Assert.That(skills["synergy 4"].Bonus, Is.EqualTo(2));
            Assert.That(skills["synergy 5"].Bonus, Is.EqualTo(2));
            Assert.That(skills["synergy 6"].Bonus, Is.EqualTo(0));
        }

        [Test]
        public void SkillSynergyStacks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1", "synergy 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 1" });

            classSkillPoints = 2;
            characterClass.Level = 2;
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 2");

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(false);

            var randomSequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 4)));

            for (var index = 0; index < 5; index++)
                randomSequence = randomSequence.Returns(classSkills[1]).Returns(classSkills[0]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(5));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(5));
            Assert.That(skills["synergy 1"].Ranks, Is.EqualTo(0));
            Assert.That(skills["synergy 2"].Ranks, Is.EqualTo(0));
            Assert.That(skills["skill 1"].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills["skill 2"].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills["synergy 1"].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills["synergy 2"].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills["skill 1"].Bonus, Is.EqualTo(0));
            Assert.That(skills["skill 2"].Bonus, Is.EqualTo(0));
            Assert.That(skills["synergy 1"].Bonus, Is.EqualTo(4));
            Assert.That(skills["synergy 2"].Bonus, Is.EqualTo(2));
        }

        [Test]
        public void DoNotApplySkillSynergyIfThereIsNoSynergisticSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(Enumerable.Empty<string>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 1" });

            classSkillPoints = 2;
            characterClass.Level = 2;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("synergy 2");

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(false);

            var randomSequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 3)));

            for (var index = 0; index < 10; index++)
                randomSequence = randomSequence.Returns(classSkills[index % 2]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(5));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(5));
            Assert.That(skills["synergy 2"].Ranks, Is.EqualTo(0));
            Assert.That(skills["skill 1"].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills["skill 2"].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills["synergy 2"].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills["skill 1"].Bonus, Is.EqualTo(0));
            Assert.That(skills["skill 2"].Bonus, Is.EqualTo(0));
            Assert.That(skills["synergy 2"].Bonus, Is.EqualTo(0));
            Assert.That(skills.Keys, Is.All.Not.EqualTo("synergy 1"));
        }

        [Test]
        public void DoNotApplySkillSynergyIfInsufficientRanks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 3")).Returns(new[] { "synergy 3" });

            classSkillPoints = 3;
            characterClass.Level = 6;

            classSkills.Add("skill 1");
            crossClassSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            classSkills.Add("synergy 1");
            classSkills.Add("synergy 2");
            classSkills.Add("synergy 3");

            var sequence = mockBooleanPercentileSelector.SetupSequence(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill));
            for (var count = 0; count < 10; count++)
                sequence = sequence.Returns(false);
            for (var count = 0; count < 17; count++)
                sequence = sequence.Returns(true);

            var randomSequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 4)));

            for (var index = 0; index < 4; index++)
                randomSequence = randomSequence.Returns(classSkills[0]);
            for (var index = 0; index < 6; index++)
                randomSequence = randomSequence.Returns(classSkills[1]);

            randomSequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 2)));

            for (var index = 0; index < 17; index++)
                randomSequence = randomSequence.Returns(crossClassSkills[index % 2]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(4));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(9));
            Assert.That(skills["skill 3"].Ranks, Is.EqualTo(8));
            Assert.That(skills["synergy 1"].Ranks, Is.EqualTo(6));
            Assert.That(skills["synergy 2"].Ranks, Is.EqualTo(0));
            Assert.That(skills["synergy 3"].Ranks, Is.EqualTo(0));
            Assert.That(skills["skill 1"].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills["skill 2"].EffectiveRanks, Is.EqualTo(4.5));
            Assert.That(skills["skill 3"].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills["synergy 1"].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills["synergy 2"].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills["synergy 3"].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills["skill 1"].Bonus, Is.EqualTo(0));
            Assert.That(skills["skill 2"].Bonus, Is.EqualTo(0));
            Assert.That(skills["skill 3"].Bonus, Is.EqualTo(0));
            Assert.That(skills["synergy 1"].Bonus, Is.EqualTo(0));
            Assert.That(skills["synergy 2"].Bonus, Is.EqualTo(0));
            Assert.That(skills["synergy 3"].Bonus, Is.EqualTo(0));
        }

        [TestCase(1, 8)]
        [TestCase(2, 10)]
        [TestCase(3, 12)]
        [TestCase(4, 14)]
        [TestCase(5, 16)]
        [TestCase(6, 18)]
        [TestCase(7, 20)]
        [TestCase(8, 22)]
        [TestCase(9, 24)]
        [TestCase(10, 26)]
        [TestCase(11, 28)]
        [TestCase(12, 30)]
        [TestCase(13, 32)]
        [TestCase(14, 34)]
        [TestCase(15, 36)]
        [TestCase(16, 38)]
        [TestCase(17, 40)]
        [TestCase(18, 42)]
        [TestCase(19, 44)]
        [TestCase(20, 46)]
        public void HumansGetExtraSkillPointsPerLevel(int level, int points)
        {
            characterClass.Level = level;
            classSkillPoints = 1;
            AddClassSkills(3);
            race.BaseRace = RaceConstants.BaseRaces.Human;

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var totalRanks = skills.Sum(kvp => kvp.Value.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        [TestCase(1, 16)]
        [TestCase(2, 20)]
        [TestCase(3, 24)]
        [TestCase(4, 28)]
        [TestCase(5, 32)]
        [TestCase(6, 36)]
        [TestCase(7, 40)]
        [TestCase(8, 44)]
        [TestCase(9, 48)]
        [TestCase(10, 52)]
        [TestCase(11, 56)]
        [TestCase(12, 60)]
        [TestCase(13, 64)]
        [TestCase(14, 68)]
        [TestCase(15, 72)]
        [TestCase(16, 76)]
        [TestCase(17, 80)]
        [TestCase(18, 84)]
        [TestCase(19, 88)]
        [TestCase(20, 92)]
        public void AllPerLevelBonusesStack(int level, int points)
        {
            characterClass.Level = level;
            classSkillPoints = 2;
            stats[StatConstants.Intelligence].Value = 12;
            race.BaseRace = RaceConstants.BaseRaces.Human;
            AddClassSkills(5);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var totalRanks = skills.Sum(kvp => kvp.Value.Ranks);
            Assert.That(totalRanks, Is.EqualTo(points));
        }

        [TestCase(1, 1, 4)]
        [TestCase(2, 1, 5)]
        [TestCase(3, 1, 6)]
        [TestCase(4, 1, 7)]
        [TestCase(5, 1, 8)]
        [TestCase(6, 1, 9)]
        [TestCase(7, 1, 10)]
        [TestCase(8, 1, 11)]
        [TestCase(9, 1, 12)]
        [TestCase(10, 1, 13)]
        [TestCase(11, 1, 14)]
        [TestCase(12, 1, 15)]
        [TestCase(13, 1, 16)]
        [TestCase(14, 1, 17)]
        [TestCase(15, 1, 18)]
        [TestCase(16, 1, 19)]
        [TestCase(17, 1, 20)]
        [TestCase(18, 1, 21)]
        [TestCase(19, 1, 22)]
        [TestCase(20, 1, 23)]
        [TestCase(1, 2, 8)]
        [TestCase(2, 2, 10)]
        [TestCase(3, 2, 12)]
        [TestCase(4, 2, 14)]
        [TestCase(5, 2, 16)]
        [TestCase(6, 2, 18)]
        [TestCase(7, 2, 20)]
        [TestCase(8, 2, 22)]
        [TestCase(9, 2, 24)]
        [TestCase(10, 2, 26)]
        [TestCase(11, 2, 28)]
        [TestCase(12, 2, 30)]
        [TestCase(13, 2, 32)]
        [TestCase(14, 2, 34)]
        [TestCase(15, 2, 36)]
        [TestCase(16, 2, 38)]
        [TestCase(17, 2, 40)]
        [TestCase(18, 2, 42)]
        [TestCase(19, 2, 44)]
        [TestCase(20, 2, 46)]
        public void MonstersGetMoreSkillPointsByMonsterHitDice(int hitDie, int monsterSkillPoints, int monsterPoints)
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 1;
            classSkillPoints = 1;
            racialSkillPoints = monsterSkillPoints;
            stats[StatConstants.Intelligence].Value = 10;
            monsterHitDice = hitDie;
            AddMonsterSkills(hitDie + 3);
            AddClassSkills(2);
            classSkills.Add(monsterClassSkills[0]);
            crossClassSkills.Add(monsterClassSkills[1]);

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var totalRanks = skills.Sum(kvp => kvp.Value.Ranks);

            //INFO: Adding 8 to account for class skill points
            Assert.That(totalRanks, Is.EqualTo(monsterPoints + 4));
            Assert.That(skills[monsterClassSkills[0]].RankCap, Is.EqualTo(4 + hitDie));
            Assert.That(skills[monsterClassSkills[1]].RankCap, Is.EqualTo(4 + hitDie));
            Assert.That(skills["class skill 1"].RankCap, Is.EqualTo(4));
            Assert.That(skills["class skill 2"].RankCap, Is.EqualTo(4));

            foreach (var monsterSkill in monsterClassSkills.Skip(2))
                Assert.That(skills[monsterSkill].RankCap, Is.EqualTo(hitDie + 3));
        }

        private void AddMonsterSkills(int quantity)
        {
            while (quantity > 0)
            {
                monsterClassSkills.Add($"monster skill {quantity--}");
            }
        }

        [Test]
        public void OldMonsterSkillsHaveCapIncreased()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            stats[StatConstants.Intelligence].Value = 10;
            monsterHitDice = 1;
            AddClassSkills(1);
            AddCrossClassSkills(1);
            monsterClassSkills.AddRange(classSkills);
            monsterClassSkills.AddRange(crossClassSkills);

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["class skill 1"].Ranks, Is.EqualTo(8));
            Assert.That(skills["class skill 1"].RankCap, Is.EqualTo(24));
            Assert.That(skills["cross-class skill 1"].Ranks, Is.EqualTo(20));
            Assert.That(skills["cross-class skill 1"].RankCap, Is.EqualTo(24));
        }

        [TestCase(1, 12)]
        [TestCase(2, 15)]
        [TestCase(3, 18)]
        [TestCase(4, 21)]
        [TestCase(5, 24)]
        [TestCase(6, 27)]
        [TestCase(7, 30)]
        [TestCase(8, 33)]
        [TestCase(9, 36)]
        [TestCase(10, 39)]
        [TestCase(11, 42)]
        [TestCase(12, 45)]
        [TestCase(13, 48)]
        [TestCase(14, 51)]
        [TestCase(15, 54)]
        [TestCase(16, 57)]
        [TestCase(17, 60)]
        [TestCase(18, 63)]
        [TestCase(19, 66)]
        [TestCase(20, 69)]
        public void MonstersApplyIntelligenceBonusToMonsterSkillPoints(int hitDie, int monsterPoints)
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 1;
            classSkillPoints = 1;
            racialSkillPoints = 2;
            stats[StatConstants.Intelligence].Value = 12;
            monsterHitDice = hitDie;
            AddMonsterSkills(hitDie + 3);
            AddClassSkills(2);
            classSkills.Add(monsterClassSkills[0]);
            crossClassSkills.Add(monsterClassSkills[1]);

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var totalRanks = skills.Sum(kvp => kvp.Value.Ranks);

            //INFO: Adding 8 to account for class skill points
            Assert.That(totalRanks, Is.EqualTo(monsterPoints + 8));
            Assert.That(skills[monsterClassSkills[0]].RankCap, Is.EqualTo(4 + hitDie));
            Assert.That(skills[monsterClassSkills[1]].RankCap, Is.EqualTo(4 + hitDie));
            Assert.That(skills["class skill 1"].RankCap, Is.EqualTo(4));
            Assert.That(skills["class skill 2"].RankCap, Is.EqualTo(4));

            foreach (var monsterSkill in monsterClassSkills.Skip(2))
                Assert.That(skills[monsterSkill].RankCap, Is.EqualTo(hitDie + 3));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void MonstersCannotHaveFewerThan1SkillPointPerMonsterHitDie(int hitDie)
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 1;
            classSkillPoints = 1;
            racialSkillPoints = 2;
            stats[StatConstants.Intelligence].Value = -600;
            AddMonsterSkills(hitDie + 2);
            monsterHitDice = hitDie;

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var totalRanks = skills.Values.Sum(v => v.Ranks);

            //INFO: Don't need to worry about class skill points, because we do not specify any class skills
            Assert.That(totalRanks, Is.EqualTo(hitDie));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void CannotHaveFewerThan1SkillPointPerLevel(int level)
        {
            stats[StatConstants.Intelligence].Value = -9266;
            characterClass.Level = level;
            classSkillPoints = 1;
            AddClassSkills(2);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var totalRanks = skills.Values.Sum(v => v.Ranks);
            Assert.That(totalRanks, Is.EqualTo(level));
        }

        [Test]
        public void ExpertGets10RandomSkillsAsClassSkills()
        {
            characterClass.Name = CharacterClassConstants.Expert;
            classSkillPoints = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, CharacterClassConstants.Expert)).Returns(classSkillPoints);

            for (var i = 11; i > 0; i--)
                allSkills.Add("skill " + i.ToString());

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(allSkills)).Returns(allSkills[0]).Returns(allSkills[0]).Returns(allSkills[10])
                .Returns(allSkills[9]).Returns(allSkills[8]).Returns(allSkills[7]).Returns(allSkills[6]).Returns(allSkills[5]).Returns(allSkills[4])
                .Returns(allSkills[3]).Returns(allSkills[1]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].ClassSkill, Is.True);
            Assert.That(skills["skill 2"].ClassSkill, Is.True);
            Assert.That(skills["skill 3"].ClassSkill, Is.True);
            Assert.That(skills["skill 4"].ClassSkill, Is.True);
            Assert.That(skills["skill 5"].ClassSkill, Is.True);
            Assert.That(skills["skill 6"].ClassSkill, Is.True);
            Assert.That(skills["skill 7"].ClassSkill, Is.True);
            Assert.That(skills["skill 8"].ClassSkill, Is.True);
            Assert.That(skills["skill 10"].ClassSkill, Is.True);
            Assert.That(skills["skill 11"].ClassSkill, Is.True);
            Assert.That(skills.Count, Is.EqualTo(10));
        }

        [Test]
        public void ExpertCrossClassSkillsDoNotIncludeRandomClassSkills()
        {
            characterClass.Name = CharacterClassConstants.Expert;
            classSkillPoints = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, CharacterClassConstants.Expert)).Returns(classSkillPoints);

            for (var i = 11; i > 0; i--)
                allSkills.Add("skill " + i.ToString());

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CrossClassSkills, CharacterClassConstants.Expert)).Returns(crossClassSkills);
            crossClassSkills.Add("other skill");
            crossClassSkills.Add("different skill");
            crossClassSkills.Add("skill 3");

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(allSkills)).Returns(allSkills[0]).Returns(allSkills[0]).Returns(allSkills[10])
                .Returns(allSkills[9]).Returns(allSkills[8]).Returns(allSkills[7]).Returns(allSkills[6]).Returns(allSkills[5]).Returns(allSkills[4])
                .Returns(allSkills[3]).Returns(allSkills[1]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].ClassSkill, Is.True);
            Assert.That(skills["skill 2"].ClassSkill, Is.True);
            Assert.That(skills["skill 3"].ClassSkill, Is.True);
            Assert.That(skills["skill 4"].ClassSkill, Is.True);
            Assert.That(skills["skill 5"].ClassSkill, Is.True);
            Assert.That(skills["skill 6"].ClassSkill, Is.True);
            Assert.That(skills["skill 7"].ClassSkill, Is.True);
            Assert.That(skills["skill 8"].ClassSkill, Is.True);
            Assert.That(skills["skill 10"].ClassSkill, Is.True);
            Assert.That(skills["skill 11"].ClassSkill, Is.True);
            Assert.That(skills["other skill"].ClassSkill, Is.False);
            Assert.That(skills["different skill"].ClassSkill, Is.False);
            Assert.That(skills.Count, Is.EqualTo(12));
        }

        [Test]
        public void IfCharacterHasBaseStat_GetSkill()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");
            specialistSkills.Add("skill 5");
            specialistSkills.Add("skill 6");

            stats[StatConstants.Constitution] = new Stat(StatConstants.Constitution);

            var constitutionSelection = new SkillSelection { BaseStatName = StatConstants.Constitution, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(constitutionSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 4")).Returns(constitutionSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 6")).Returns(constitutionSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills.Keys, Contains.Item("skill 1"));
            Assert.That(skills.Keys, Contains.Item("skill 3"));
            Assert.That(skills.Keys, Contains.Item("skill 5"));
            Assert.That(skills.Keys, Contains.Item("skill 2"));
            Assert.That(skills.Keys, Contains.Item("skill 4"));
            Assert.That(skills.Keys, Contains.Item("skill 6"));
            Assert.That(skills.Count, Is.EqualTo(6));
        }

        [Test]
        public void IfCharacterDoesNotHaveBaseStat_CannotGetSkill()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");
            specialistSkills.Add("skill 5");
            specialistSkills.Add("skill 6");

            var constitutionSelection = new SkillSelection { BaseStatName = StatConstants.Constitution, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(constitutionSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 4")).Returns(constitutionSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 6")).Returns(constitutionSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills.Keys, Contains.Item("skill 1"));
            Assert.That(skills.Keys, Contains.Item("skill 3"));
            Assert.That(skills.Keys, Contains.Item("skill 5"));
            Assert.That(skills.Keys, Is.All.Not.EqualTo("skill 2"));
            Assert.That(skills.Keys, Is.All.Not.EqualTo("skill 4"));
            Assert.That(skills.Keys, Is.All.Not.EqualTo("skill 6"));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void DoNotAssignSkillPointsToClassSkillsThatTheCharacterDoesNotHaveDueToNotHavingTheBaseStat()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            specialistSkills.Add("skill 5");
            specialistSkills.Add("skill 6");

            var constitutionSelection = new SkillSelection { BaseStatName = StatConstants.Constitution, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(constitutionSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 6")).Returns(constitutionSelection);

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(false);
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 2)))
                .Returns("skill 5")
                .Returns("skill 1")
                .Returns("skill 1")
                .Returns("skill 5")
                .Returns("skill 5");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(2));
            Assert.That(skills["skill 5"].Ranks, Is.EqualTo(3));
            Assert.That(skills["skill 1"].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills["skill 5"].EffectiveRanks, Is.EqualTo(3));
        }

        [Test]
        public void DoNotAssignSkillPointsToCrossClassSkillsThatTheCharacterDoesNotHaveDueToNotHavingTheBaseStat()
        {
            classSkillPoints = 1;
            characterClass.Level = 2;

            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");

            var constitutionSelection = new SkillSelection { BaseStatName = StatConstants.Constitution, SkillName = "skill 4" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 4")).Returns(constitutionSelection);

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 1)))
                .Returns("skill 3")
                .Returns("skill 3")
                .Returns("skill 3")
                .Returns("skill 3")
                .Returns("skill 3");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 3"].Ranks, Is.EqualTo(5));
            Assert.That(skills["skill 3"].EffectiveRanks, Is.EqualTo(2.5));
        }

        [Test]
        public void DoNotAssignMonsterSkillPointsToMonsterSkillsIfCharacterDoesNotHaveRequiredBaseStat()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            stats[StatConstants.Intelligence].Value = 10;
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var constitutionSelection = new SkillSelection { BaseStatName = StatConstants.Constitution, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 1")).Returns(constitutionSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(5));
            Assert.That(skills["skill 2"].RanksMaxedOut, Is.True);
            Assert.That(skills.Keys, Is.All.Not.EqualTo("skill 1"));
            Assert.That(skills.Count, Is.EqualTo(1));
        }

        [Test]
        public void SelectRandomFocusForMonsterSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            stats[StatConstants.Intelligence].Value = 10;
            stats[StatConstants.Charisma] = new Stat(StatConstants.Charisma);
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var randomSelection = new SkillSelection { BaseStatName = StatConstants.Charisma, RandomFociQuantity = 1, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["random"].BaseStat, Is.EqualTo(stats[StatConstants.Charisma]));
            Assert.That(skills["skill 1"].BaseStat, Is.EqualTo(stats[StatConstants.Intelligence]));
            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void SelectMultipleRandomFociForMonsterSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            stats[StatConstants.Intelligence].Value = 10;
            stats[StatConstants.Charisma] = new Stat(StatConstants.Charisma);
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var randomSelection = new SkillSelection { BaseStatName = StatConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["random"].BaseStat, Is.EqualTo(stats[StatConstants.Charisma]));
            Assert.That(skills["other random"].BaseStat, Is.EqualTo(stats[StatConstants.Charisma]));
            Assert.That(skills["skill 1"].BaseStat, Is.EqualTo(stats[StatConstants.Intelligence]));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void DoNotSelectRepeatedRandomFociForMonsterSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            stats[StatConstants.Intelligence].Value = 10;
            stats[StatConstants.Charisma] = new Stat(StatConstants.Charisma);
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var randomSelection = new SkillSelection { BaseStatName = StatConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ / 2 % ss.Count()));

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["random"].BaseStat, Is.EqualTo(stats[StatConstants.Charisma]));
            Assert.That(skills["other random"].BaseStat, Is.EqualTo(stats[StatConstants.Charisma]));
            Assert.That(skills["skill 1"].BaseStat, Is.EqualTo(stats[StatConstants.Intelligence]));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void SelectAllRandomFociForMonsterSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { race.BaseRace, "other base race" });

            characterClass.Level = 20;
            classSkillPoints = 0;
            racialSkillPoints = 2;
            stats[StatConstants.Intelligence].Value = 10;
            stats[StatConstants.Charisma] = new Stat(StatConstants.Charisma);
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            monsterHitDice = 2;

            var randomSelection = new SkillSelection { BaseStatName = StatConstants.Charisma, RandomFociQuantity = 3, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["random"].BaseStat, Is.EqualTo(stats[StatConstants.Charisma]));
            Assert.That(skills["other random"].BaseStat, Is.EqualTo(stats[StatConstants.Charisma]));
            Assert.That(skills["skill 1"].BaseStat, Is.EqualTo(stats[StatConstants.Intelligence]));
            Assert.That(skills.Count, Is.EqualTo(3));
        }
    }
}