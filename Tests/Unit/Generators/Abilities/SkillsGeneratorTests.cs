using System;
using System.Collections.Generic;
using D20Dice;
using Moq;
using NPCGen.Common.Abilities.Stats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
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
        private Race race;
        private Dictionary<String, Stat> stats;
        private List<String> classSkills;
        private List<String> crossClassSkills;
        private Mock<ISkillSelector> mockSkillSelector;
        private Mock<IDice> mockDice;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockSkillSelector = new Mock<ISkillSelector>();
            mockDice = new Mock<IDice>();
            skillsGenerator = new SkillsGenerator();
            characterClass = new CharacterClass();
            race = new Race();
            stats = new Dictionary<String, Stat>();
            classSkills = new List<String>();
            crossClassSkills = new List<String>();

            characterClass.ClassName = "class name";
            characterClass.Level = 5;
            mockCollectionsSelector.Setup(s => s.SelectFrom("class nameClassSkills")).Returns(classSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom("class nameCrossClassSkills")).Returns(crossClassSkills);
        }

        [Test]
        public void ReturnSkills()
        {
            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills, Is.Not.Null);
        }

        [Test]
        public void GetSkillPointsForClass()
        {
            var skillPoints = new Dictionary<String, Int32>();
            skillPoints["class name"] = 2;
            mockAdjustmentsSelector.Setup(s => s.SelectAdjustmentsFrom("SkillPointsForClasses")).Returns(skillPoints);

            classSkills.Add("skill");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["skill"].Ranks, Is.EqualTo(16));
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
        public void GetBothClassSkillsAndCrossClassSkills()
        {
            classSkills.Add("skill 1");
            classSkills.Add("skill 2");
            crossClassSkills.Add("skill 3");
            crossClassSkills.Add("skill 4");

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

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

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["class skill"].BaseStat, Is.EqualTo(stats["stat 1"]));
            Assert.That(skills["cross class skill"].BaseStat, Is.EqualTo(stats["stat 2"]));
        }

        [Test]
        public void SetArmorCheckPenalty()
        {
            classSkills.Add("penalty");
            crossClassSkills.Add("no penalty");

            var penaltySkillSelection = new SkillSelection();
            penaltySkillSelection.ArmorCheckPenalty = true;

            var noPenaltySkillSelection = new SkillSelection();
            noPenaltySkillSelection.ArmorCheckPenalty = false;

            mockSkillSelector.Setup(s => s.SelectFor("penalty")).Returns(penaltySkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("no penalty")).Returns(noPenaltySkillSelection);

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
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

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);

            Assert.That(skills["skill 1"].ClassSkill, Is.True);
            Assert.That(skills["skill 2"].ClassSkill, Is.True);
            Assert.That(skills["skill 3"].ClassSkill, Is.False);
            Assert.That(skills["skill 4"].ClassSkill, Is.False);
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongClassSkills()
        {
            Assert.Fail();
        }

        [Test]
        public void AssignSkillPointsRandomlyAmongCrossClassSkills()
        {
            Assert.Fail();
        }

        [Test]
        public void AssignPointsToClassSkillsOn1OfD3()
        {
            Assert.Fail();
        }

        [Test]
        public void AssignPointsToClassSkillsOn2OfD3()
        {
            Assert.Fail();
        }

        [Test]
        public void AssignPointsToCrossClassSkillsOn3OfD3()
        {
            Assert.Fail();
        }

        [Test]
        public void CannotAssignMoreThanLevelPlusThreePointsPerSkill()
        {
            Assert.Fail();
        }
    }
}