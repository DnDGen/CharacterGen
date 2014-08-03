using System;
using System.Collections.Generic;
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
        private Mock<ISkillStatSelector> mockSkillStatSelector;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionsSelector>();
            mockSkillStatSelector = new Mock<ISkillStatSelector>();
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
        public void GetAndAssignStatsToSkills()
        {
            classSkills.Add("class skill");
            crossClassSkills.Add("cross class skill");

            mockSkillStatSelector.Setup(s => s.SelectFor("class skill")).Returns("stat 1");
            mockSkillStatSelector.Setup(s => s.SelectFor("cross class skill")).Returns("stat 2");

            stats["stat 1"] = new Stat();
            stats["stat 2"] = new Stat();

            var skills = skillsGenerator.GenerateWith(characterClass, race, stats);
            Assert.That(skills["class skill"].BaseStat, Is.EqualTo(stats["stat 1"]));
            Assert.That(skills["cross class skill"].BaseStat, Is.EqualTo(stats["stat 2"]));
        }

        [Test]
        public void SetArmorCheckPenalty()
        {
            Assert.Fail();
        }

        [Test]
        public void SetClassSkill()
        {
            Assert.Fail();
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