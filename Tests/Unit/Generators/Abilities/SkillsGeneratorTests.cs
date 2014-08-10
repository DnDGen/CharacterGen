using System;
using System.Collections.Generic;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Abilities;
using NPCGen.Generators.Interfaces.Abilities;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class SkillsGeneratorTests
    {
        private ISkillsGenerator skillsGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionsSelector> mockCollectionsSelector;
        private CharacterClass characterClass;
        private Dictionary<String, Stat> stats;
        private List<String> classSkills;
        private List<String> crossClassSkills;
        private Mock<ISkillSelector> mockSkillSelector;
        private Mock<IDice> mockDice;
        private Stat intelligence;
        private Dictionary<String, Int32> skillPoints;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockSkillSelector = new Mock<ISkillSelector>();
            mockDice = new Mock<IDice>();
            skillsGenerator = new SkillsGenerator(mockSkillSelector.Object, mockDice.Object, mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);
            characterClass = new CharacterClass();
            stats = new Dictionary<String, Stat>();
            classSkills = new List<String>();
            crossClassSkills = new List<String>();
            intelligence = new Stat { Value = 10 };

            characterClass.ClassName = "class name";
            characterClass.Level = 5;
            mockCollectionsSelector.Setup(s => s.SelectFrom("ClassSkills", "class name")).Returns(classSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom("CrossClassSkills", "class name")).Returns(crossClassSkills);
            var selection = new SkillSelection { BaseStatName = StatConstants.Intelligence };
            mockSkillSelector.Setup(s => s.SelectFor(It.IsAny<String>())).Returns(selection);
            stats[StatConstants.Intelligence] = intelligence;

            skillPoints = new Dictionary<String, Int32>();
            skillPoints[characterClass.ClassName] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectAdjustmentsFrom("SkillPointsForClasses")).Returns(skillPoints);

        }

        [Test]
        public void GetSkillPointsForClass()
        {
            skillPoints[characterClass.ClassName] = 1;
            classSkills.Add("skill");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);
            Assert.That(skills["skill"].Ranks, Is.EqualTo(8));
        }

        [Test]
        public void AddIntelligenceBonusToSkillPoints()
        {
            classSkills.Add("skill");
            intelligence.Value = 12;

            var skills = skillsGenerator.GenerateWith(characterClass, stats);
            Assert.That(skills["skill"].Ranks, Is.EqualTo(8));
        }

        [Test]
        public void GetClassSkills()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("skill 3");
            classSkills.Add("skill 4");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);
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

            var skills = skillsGenerator.GenerateWith(characterClass, stats);
            foreach (var skill in crossClassSkills)
                Assert.That(skills.Keys, Contains.Item(skill));
            Assert.That(skills.Count, Is.EqualTo(4));
        }

        [Test]
        public void GetBothClassSkillsAndCrossClassSkills()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            foreach (var skill in crossClassSkills)
                Assert.That(skills.Keys, Contains.Item(skill));

            foreach (var skill in classSkills)
                Assert.That(skills.Keys, Contains.Item(skill));
            Assert.That(skills.Count, Is.EqualTo(4));
        }

        [Test]
        public void AssignStatsToSkills()
        {
            classSkills.Add("class skill");
            crossClassSkills.Add("cross class skill");

            var classSkillSelection = new SkillSelection();
            classSkillSelection.BaseStatName = "stat 1";

            var crossClassSkillSelection = new SkillSelection();
            crossClassSkillSelection.BaseStatName = "stat 2";

            mockSkillSelector.Setup(s => s.SelectFor("class skill")).Returns(classSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("cross class skill")).Returns(crossClassSkillSelection);

            stats["stat 1"] = new Stat();
            stats["stat 2"] = new Stat();

            var skills = skillsGenerator.GenerateWith(characterClass, stats);
            Assert.That(skills["class skill"].BaseStat, Is.EqualTo(stats["stat 1"]));
            Assert.That(skills["cross class skill"].BaseStat, Is.EqualTo(stats["stat 2"]));
        }

        [Test]
        public void SetArmorCheckPenalty()
        {
            classSkills.Add("penalty");
            crossClassSkills.Add("no penalty");

            var penaltySkillSelection = new SkillSelection { ArmorCheckPenalty = true, BaseStatName = StatConstants.Intelligence };
            var noPenaltySkillSelection = new SkillSelection { ArmorCheckPenalty = false, BaseStatName = StatConstants.Intelligence };

            mockSkillSelector.Setup(s => s.SelectFor("penalty")).Returns(penaltySkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("no penalty")).Returns(noPenaltySkillSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, stats);
            Assert.That(skills["penalty"].ArmorCheckPenalty, Is.True);
            Assert.That(skills["no penalty"].ArmorCheckPenalty, Is.False);
        }

        [Test]
        public void SetClassSkill()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            Assert.That(skills["skill 1"].ClassSkill, Is.True);
            Assert.That(skills["skill 2"].ClassSkill, Is.True);
            Assert.That(skills["skill 3"].ClassSkill, Is.False);
            Assert.That(skills["skill 4"].ClassSkill, Is.False);
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongClassSkills()
        {
            skillPoints[characterClass.ClassName] = 1;
            characterClass.Level = 2;

            mockDice.Setup(d => d.d3(1)).Returns(1);
            mockDice.SetupSequence(d => d.Roll("1d2-1")).Returns(1).Returns(0).Returns(0).Returns(1).Returns(1);

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(2));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(3));
            Assert.That(skills["skill 1"].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills["skill 2"].EffectiveRanks, Is.EqualTo(3));
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongCrossClassSkills()
        {
            skillPoints[characterClass.ClassName] = 1;
            characterClass.Level = 2;

            mockDice.Setup(d => d.d3(1)).Returns(3);
            mockDice.SetupSequence(d => d.Roll("1d2-1")).Returns(1).Returns(0).Returns(0).Returns(1).Returns(1);

            crossClassSkills.Add("skill 1");
            crossClassSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(2));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(3));
            Assert.That(skills["skill 1"].EffectiveRanks, Is.EqualTo(1));
            Assert.That(skills["skill 2"].EffectiveRanks, Is.EqualTo(1.5));
        }

        [TestCase(1)]
        [TestCase(2)]
        public void AssignPointsToClassSkills(Int32 roll)
        {
            skillPoints[characterClass.ClassName] = 1;
            mockDice.Setup(d => d.d3(1)).Returns(roll);
            classSkills.Add("class skill");
            crossClassSkills.Add("cross-class skill");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            Assert.That(skills["class skill"].Ranks, Is.EqualTo(8));
            Assert.That(skills["cross-class skill"].Ranks, Is.EqualTo(0));
        }

        [Test]
        public void AssignPointsToCrossClassSkills()
        {
            skillPoints[characterClass.ClassName] = 1;
            mockDice.Setup(d => d.d3(1)).Returns(3);
            classSkills.Add("class skill");
            crossClassSkills.Add("cross-class skill");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            Assert.That(skills["class skill"].Ranks, Is.EqualTo(0));
            Assert.That(skills["cross-class skill"].Ranks, Is.EqualTo(8));
        }

        [Test]
        public void AssignPointsRandomlyBetweenClassAndCrossClassSkills()
        {
            skillPoints[characterClass.ClassName] = 1;
            characterClass.Level = 2;
            mockDice.SetupSequence(d => d.d3(1)).Returns(3).Returns(1).Returns(2).Returns(3).Returns(2);

            classSkills.Add("class skill");
            crossClassSkills.Add("cross-class skill");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            Assert.That(skills["class skill"].Ranks, Is.EqualTo(3));
            Assert.That(skills["cross-class skill"].Ranks, Is.EqualTo(2));
        }

        [Test]
        public void CannotAssignMoreThanLevelPlusThreePointsPerClassSkill()
        {
            skillPoints[characterClass.ClassName] = 2;
            mockDice.SetupSequence(d => d.d3(1)).Returns(1);

            var sequence = mockDice.SetupSequence(d => d.Roll("1d3-1"));
            for (var i = 0; i < 9; i++)
                sequence = sequence.Returns(0);

            for (var i = 0; i < 4; i++)
                sequence = sequence.Returns(1).Returns(2);

            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            classSkills.Add("skill 3");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(8));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(4));
            Assert.That(skills["skill 3"].Ranks, Is.EqualTo(4));
        }

        [Test]
        public void CannotAssignMoreThanLevelPlusThreePointsPerCrossClassSkill()
        {
            skillPoints[characterClass.ClassName] = 2;
            mockDice.SetupSequence(d => d.d3(1)).Returns(3);

            var sequence = mockDice.SetupSequence(d => d.Roll("1d3-1"));
            for (var i = 0; i < 9; i++)
                sequence = sequence.Returns(0);

            for (var i = 0; i < 4; i++)
                sequence = sequence.Returns(1).Returns(2);

            crossClassSkills.Add("skill 1");
            crossClassSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(8));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(4));
            Assert.That(skills["skill 3"].Ranks, Is.EqualTo(4));
        }

        [Test]
        public void AllSkillsMaxedOut()
        {
            skillPoints[characterClass.ClassName] = 3;
            crossClassSkills.Add("skill 1");
            classSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateWith(characterClass, stats);

            Assert.That(skills["skill 1"].Ranks, Is.EqualTo(characterClass.Level + 3));
            Assert.That(skills["skill 2"].Ranks, Is.EqualTo(characterClass.Level + 3));
        }
    }
}