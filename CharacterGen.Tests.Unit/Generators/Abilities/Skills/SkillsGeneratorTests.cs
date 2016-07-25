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
using System;
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
        private List<string> crossClassSkills;
        private Mock<ISkillSelector> mockSkillSelector;
        private Dictionary<string, int> skillPoints;
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

            characterClass.Name = "class name";
            characterClass.Level = 5;
            characterClass.SpecialistFields = new[] { "specialist field" };

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, "class name")).Returns(classSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.CrossClassSkills, "class name")).Returns(crossClassSkills);
            var selection = new SkillSelection { BaseStatName = StatConstants.Intelligence };
            mockSkillSelector.Setup(s => s.SelectFor(It.IsAny<string>())).Returns(selection);

            var emptyAdjustments = new Dictionary<string, int>();
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(It.IsAny<string>())).Returns(emptyAdjustments);

            skillPoints = new Dictionary<string, int>();
            skillPoints[characterClass.Name] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPointsForClasses)).Returns(skillPoints);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "specialist field")).Returns(specialistSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.Skills)).Returns(allSkills);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.First());
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
            skillPoints[characterClass.Name] = 1;
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(points));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(0));
        }

        [Test]
        public void GetSkillPointsForClass()
        {
            skillPoints[characterClass.Name] = 2;
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("skill 3");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(8));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(8));
            Assert.That(skills["skill 3"].Ranks, Is.EqualTo(0));
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
        public void AddIntelligenceBonusToSkillPointsPerLevel(Int32 level, Int32 points)
        {
            characterClass.Level = level;
            skillPoints[characterClass.Name] = 0;
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            stats[StatConstants.Intelligence].Value = 12;

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(points));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(0));
        }

        [Test]
        public void GetClassSkills()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("skill 3");
            classSkills.Add("skill 4");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            foreach (var skill in classSkills)
                Assert.That(skills.Keys, Contains.Item(skill));

            Assert.That(skills.Count, Is.EqualTo(4));
        }

        [Test]
        public void GetCrossClassSkills()
        {
            crossClassSkills.Add("skill 1");
            crossClassSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            foreach (var skill in crossClassSkills)
                Assert.That(skills.Keys, Contains.Item(skill));

            Assert.That(skills.Count, Is.EqualTo(4));
        }

        [Test]
        public void GetSkillsFromSpecialistFields()
        {
            specialistSkills.Add("special skill");
            specialistSkills.Add("special skill 2");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            foreach (var skill in specialistSkills)
                Assert.That(skills.Keys, Contains.Item(skill));

            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetDuplicateSkillsFromSpecialistFields()
        {
            classSkills.Add("skill 1");
            specialistSkills.Add("skill 1");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills.Keys, Contains.Item("skill 1"));
            Assert.That(skills.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllSkills()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");
            specialistSkills.Add("special skill");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            foreach (var skill in crossClassSkills)
                Assert.That(skills.Keys, Contains.Item(skill));

            foreach (var skill in classSkills)
                Assert.That(skills.Keys, Contains.Item(skill));

            foreach (var skill in specialistSkills)
                Assert.That(skills.Keys, Contains.Item(skill));

            Assert.That(skills.Count, Is.EqualTo(5));
        }

        [Test]
        public void AssignStatsToSkills()
        {
            classSkills.Add("class skill");
            crossClassSkills.Add("cross class skill");
            specialistSkills.Add("specialist skill");

            var classSkillSelection = new SkillSelection();
            classSkillSelection.BaseStatName = "stat 1";

            var crossClassSkillSelection = new SkillSelection();
            crossClassSkillSelection.BaseStatName = "stat 2";

            var specialistSkillSelection = new SkillSelection();
            specialistSkillSelection.BaseStatName = "stat 3";

            mockSkillSelector.Setup(s => s.SelectFor("class skill")).Returns(classSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("cross class skill")).Returns(crossClassSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("specialist skill")).Returns(specialistSkillSelection);

            stats["stat 1"] = new Stat("stat 1");
            stats["stat 2"] = new Stat("stat 2");
            stats["stat 3"] = new Stat("stat 3");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["class skill"].BaseStat, Is.EqualTo(stats["stat 1"]));
            Assert.That(skills["cross class skill"].BaseStat, Is.EqualTo(stats["stat 2"]));
            Assert.That(skills["specialist skill"].BaseStat, Is.EqualTo(stats["stat 3"]));
        }

        [Test]
        public void SetClassSkill()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].ClassSkill, Is.True);
            Assert.That(skills["skill 2"].ClassSkill, Is.True);
            Assert.That(skills["skill 3"].ClassSkill, Is.False);
            Assert.That(skills["skill 4"].ClassSkill, Is.False);
        }

        [Test]
        public void SpecialistSkillsAreClassSkills()
        {
            specialistSkills.Add("skill 1");
            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].ClassSkill, Is.True);
        }

        [Test]
        public void SpecialistSkillOverwritesCrossClassSkill()
        {
            specialistSkills.Add("skill 1");
            crossClassSkills.Add("skill 1");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].ClassSkill, Is.True);
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongClassSkills()
        {
            skillPoints[characterClass.Name] = 1;
            characterClass.Level = 2;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(false);
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 2)))
                .Returns(classSkills[1])
                .Returns(classSkills[0])
                .Returns(classSkills[0])
                .Returns(classSkills[1])
                .Returns(classSkills[1]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(2));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(3));
            Assert.That(skills["skill 1"].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills["skill 2"].EffectiveRanks, Is.EqualTo(3));
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongCrossClassSkills()
        {
            skillPoints[characterClass.Name] = 1;
            characterClass.Level = 2;

            crossClassSkills.Add("skill 1");
            crossClassSkills.Add("skill 2");

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(ss => ss.Count() == 2)))
                .Returns(crossClassSkills[1])
                .Returns(crossClassSkills[0])
                .Returns(crossClassSkills[0])
                .Returns(crossClassSkills[1])
                .Returns(crossClassSkills[1]);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(2));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(3));
            Assert.That(skills["skill 1"].EffectiveRanks, Is.EqualTo(1));
            Assert.That(skills["skill 2"].EffectiveRanks, Is.EqualTo(1.5));
        }

        [Test]
        public void AssignPointsToClassSkills()
        {
            skillPoints[characterClass.Name] = 1;
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
            skillPoints[characterClass.Name] = 1;
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
            skillPoints[characterClass.Name] = 1;
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
        public void CannotAssignMoreThanLevelPlusThreePointsPerClassSkill()
        {
            skillPoints[characterClass.Name] = 2;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(false);

            var sequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<String>>(ss => ss.Count() == 3)));
            for (var count = 4; count > 0; count--)
                sequence = sequence.Returns("skill 2").Returns("skill 3").Returns("skill 1").Returns("skill 1");

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("skill 3");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(8));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(4));
            Assert.That(skills["skill 3"].Ranks, Is.EqualTo(4));
        }

        [Test]
        public void CannotAssignMoreThanLevelPlusThreePointsPerCrossClassSkill()
        {
            skillPoints[characterClass.Name] = 2;
            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var sequence = mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<String>>(ss => ss.Count() == 3)));
            for (var count = 4; count > 0; count--)
                sequence = sequence.Returns("skill 2").Returns("skill 3").Returns("skill 1").Returns("skill 1");

            crossClassSkills.Add("skill 1");
            crossClassSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(8));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(4));
            Assert.That(skills["skill 3"].Ranks, Is.EqualTo(4));
        }

        [Test]
        public void AllSkillsMaxedOut()
        {
            skillPoints[characterClass.Name] = 3;
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

            skillPoints[characterClass.Name] = 2;
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

            skillPoints[characterClass.Name] = 2;
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

            skillPoints[characterClass.Name] = 2;
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

            skillPoints[characterClass.Name] = 3;
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
        public void HumansGetExtraSkillPointsPerLevel(int level, int points)
        {
            characterClass.Level = level;
            skillPoints[characterClass.Name] = 0;
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            race.BaseRace = RaceConstants.BaseRaces.Human;

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(points));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(0));
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
            var cap = level + 3;
            characterClass.Level = level;
            skillPoints[characterClass.Name] = 2;
            stats[StatConstants.Intelligence].Value = 12;
            race.BaseRace = RaceConstants.BaseRaces.Human;
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("skill 3");
            classSkills.Add("skill 4");
            classSkills.Add("skill 5");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            var sum = skills.Values.Sum(s => s.Ranks);

            Assert.That(sum, Is.EqualTo(points));
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(cap));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(cap));
            Assert.That(skills["skill 3"].Ranks, Is.EqualTo(cap));
            Assert.That(skills["skill 4"].Ranks, Is.EqualTo(cap));
            Assert.That(skills["skill 5"].Ranks, Is.EqualTo(0));
            Assert.That(skills["skill 1"].RankCap, Is.EqualTo(cap));
            Assert.That(skills["skill 2"].RankCap, Is.EqualTo(cap));
            Assert.That(skills["skill 3"].RankCap, Is.EqualTo(cap));
            Assert.That(skills["skill 4"].RankCap, Is.EqualTo(cap));
            Assert.That(skills["skill 5"].RankCap, Is.EqualTo(cap));
            Assert.That(skills["skill 1"].RanksMaxedOut, Is.True);
            Assert.That(skills["skill 2"].RanksMaxedOut, Is.True);
            Assert.That(skills["skill 3"].RanksMaxedOut, Is.True);
            Assert.That(skills["skill 4"].RanksMaxedOut, Is.True);
            Assert.That(skills["skill 5"].RanksMaxedOut, Is.False);
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
        public void MonstersGetMoreSkillPointsByMonsterHitDice(int hitDie, int totalRanks)
        {
            race.BaseRace = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "baserace", "otherbaserace" });

            characterClass.Level = 20;
            skillPoints[characterClass.Name] = 0;
            classSkills.Add("skill 1");
            classSkills.Add("class skill");
            crossClassSkills.Add("skill 2");
            stats[StatConstants.Intelligence].Value = 10;

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var monsterClassSkills = new List<string>();
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace)).Returns(monsterClassSkills);

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice[race.BaseRace] = hitDie;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].RankCap, Is.EqualTo(26 + hitDie));
            Assert.That(skills["skill 2"].RankCap, Is.EqualTo(26 + hitDie));
            Assert.That(skills["class skill"].RankCap, Is.EqualTo(23));
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(totalRanks));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(20));
            Assert.That(skills["class skill"].Ranks, Is.EqualTo(0));
        }

        [Test]
        public void NewMonsterSkillsAreCapped()
        {
            race.BaseRace = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "baserace", "otherbaserace" });

            characterClass.Level = 20;
            skillPoints[characterClass.Name] = 0;
            stats[StatConstants.Intelligence].Value = 10;

            var monsterClassSkills = new List<string>();
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace)).Returns(monsterClassSkills);

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice[race.BaseRace] = 1;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].RankCap, Is.EqualTo(4));
            Assert.That(skills["skill 2"].RankCap, Is.EqualTo(4));
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(4));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(4));
        }

        [Test]
        public void OldMonsterSkillsHaveCapIncreased()
        {
            race.BaseRace = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "baserace", "otherbaserace" });

            characterClass.Level = 20;
            skillPoints[characterClass.Name] = 0;
            classSkills.Add("skill 1");
            crossClassSkills.Add("skill 2");
            stats[StatConstants.Intelligence].Value = 10;

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var monsterClassSkills = new List<string>();
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace)).Returns(monsterClassSkills);

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice[race.BaseRace] = 1;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].RankCap, Is.EqualTo(27));
            Assert.That(skills["skill 2"].RankCap, Is.EqualTo(27));
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(8));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(20));
        }

        [TestCase(1, 12)]
        [TestCase(2, 15)]
        [TestCase(3, 18)]
        [TestCase(4, 21)]
        [TestCase(5, 24)]
        [TestCase(6, 27)]
        [TestCase(7, 30)]
        [TestCase(8, 33)]
        public void MonstersApplyIntelligenceBonusToMonsterSkillPoints(int hitDie, int totalRanks)
        {
            race.BaseRace = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "baserace", "otherbaserace" });

            characterClass.Level = 20;
            skillPoints[characterClass.Name] = 0;
            classSkills.Add("skill 1");
            classSkills.Add("class skill");
            crossClassSkills.Add("skill 2");
            stats[StatConstants.Intelligence].Value = 12;

            mockBooleanPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill))
                .Returns(true);

            var monsterClassSkills = new List<string>();
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace)).Returns(monsterClassSkills);

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice[race.BaseRace] = hitDie;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].RankCap, Is.EqualTo(26 + hitDie));
            Assert.That(skills["skill 2"].RankCap, Is.EqualTo(26 + hitDie));
            Assert.That(skills["class skill"].RankCap, Is.EqualTo(23));
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(totalRanks));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(20));
            Assert.That(skills["class skill"].Ranks, Is.EqualTo(0));
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
        public void MonstersCannotHaveFewerThan1SkillPointPerMonsterHitDie(int hitDie, int totalRanks)
        {
            race.BaseRace = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "baserace", "otherbaserace" });

            characterClass.Level = 1;
            skillPoints[characterClass.Name] = 0;
            stats[StatConstants.Intelligence].Value = -600;

            var monsterClassSkills = new List<string>();
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace)).Returns(monsterClassSkills);

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice[race.BaseRace] = hitDie;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(totalRanks));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(0));
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
            race.BaseRace = "baserace";
            stats[StatConstants.Intelligence].Value = -9266;
            characterClass.Level = level;
            skillPoints[characterClass.Name] = 1;
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(level));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(0));
        }

        [Test]
        public void ExpertGets10RandomSkillsAsClassSkills()
        {
            characterClass.Name = CharacterClassConstants.Expert;
            skillPoints[CharacterClassConstants.Expert] = 0;

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
            skillPoints[CharacterClassConstants.Expert] = 0;

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
        public void IfCharacterDoesNotHaveBaseStat_CannotGetSkill()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");
            specialistSkills.Add("skill 5");
            specialistSkills.Add("skill 6");

            var intelligenceSelection = new SkillSelection { BaseStatName = StatConstants.Intelligence };
            var constitutionSelection = new SkillSelection { BaseStatName = StatConstants.Constitution };
            mockSkillSelector.Setup(s => s.SelectFor("skill 1")).Returns(intelligenceSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 3")).Returns(intelligenceSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 5")).Returns(intelligenceSelection);
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
            skillPoints[characterClass.Name] = 1;
            characterClass.Level = 2;

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            specialistSkills.Add("skill 5");
            specialistSkills.Add("skill 6");

            var intelligenceSelection = new SkillSelection { BaseStatName = StatConstants.Intelligence };
            var constitutionSelection = new SkillSelection { BaseStatName = StatConstants.Constitution };
            mockSkillSelector.Setup(s => s.SelectFor("skill 1")).Returns(intelligenceSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 5")).Returns(intelligenceSelection);
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
            skillPoints[characterClass.Name] = 1;
            characterClass.Level = 2;

            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");

            var intelligenceSelection = new SkillSelection { BaseStatName = StatConstants.Intelligence };
            var constitutionSelection = new SkillSelection { BaseStatName = StatConstants.Constitution };
            mockSkillSelector.Setup(s => s.SelectFor("skill 3")).Returns(intelligenceSelection);
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
            race.BaseRace = "baserace";
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.Monsters))
                .Returns(new[] { "baserace", "otherbaserace" });

            characterClass.Level = 20;
            skillPoints[characterClass.Name] = 0;
            stats[StatConstants.Intelligence].Value = 10;

            var monsterClassSkills = new List<string>();
            monsterClassSkills.Add("skill 1");
            monsterClassSkills.Add("skill 2");
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassSkills, race.BaseRace)).Returns(monsterClassSkills);

            var intelligenceSelection = new SkillSelection { BaseStatName = StatConstants.Intelligence };
            var constitutionSelection = new SkillSelection { BaseStatName = StatConstants.Constitution };
            mockSkillSelector.Setup(s => s.SelectFor("skill 1")).Returns(constitutionSelection);
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(intelligenceSelection);

            var monsterHitDice = new Dictionary<string, int>();
            monsterHitDice["monster"] = 1234;
            monsterHitDice[race.BaseRace] = 2;
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.MonsterHitDice)).Returns(monsterHitDice);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(5));
            Assert.That(skills["skill 2"].RanksMaxedOut, Is.True);
            Assert.That(skills.Keys, Is.All.Not.EqualTo("skill 1"));
            Assert.That(skills.Count, Is.EqualTo(1));
        }
    }
}