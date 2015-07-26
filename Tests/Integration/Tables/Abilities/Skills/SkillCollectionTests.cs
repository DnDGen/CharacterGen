using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Mappers;
using CharacterGen.Tables;
using CharacterGen.Tests.Integration.Common;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillCollectionTests : IntegrationTests
    {
        [Inject]
        public ICollectionsMapper Mapper { get; set; }

        private Dictionary<String, IEnumerable<String>> classSkills;
        private Dictionary<String, IEnumerable<String>> crossClassSkills;

        [SetUp]
        public void Setup()
        {
            classSkills = Mapper.Map(TableNameConstants.Set.Collection.ClassSkills);
            crossClassSkills = Mapper.Map(TableNameConstants.Set.Collection.CrossClassSkills);
        }

        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Druid)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Paladin)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Wizard)]
        public void NoIntersectionBetweenClassAndCrossClassSkills(String className)
        {
            var intersect = classSkills[className].Intersect(crossClassSkills[className]);
            Assert.That(intersect, Is.Empty);
        }

        [TestCase(SkillConstants.DecipherScript)]
        [TestCase(SkillConstants.DisableDevice)]
        [TestCase(SkillConstants.HandleAnimal)]
        [TestCase(SkillConstants.KnowledgeArcana)]
        [TestCase(SkillConstants.KnowledgeArchitectureAndEngineering)]
        [TestCase(SkillConstants.KnowledgeDungeoneering)]
        [TestCase(SkillConstants.KnowledgeGeography)]
        [TestCase(SkillConstants.KnowledgeHistory)]
        [TestCase(SkillConstants.KnowledgeLocal)]
        [TestCase(SkillConstants.KnowledgeNature)]
        [TestCase(SkillConstants.KnowledgeNobilityAndRoyalty)]
        [TestCase(SkillConstants.KnowledgeReligion)]
        [TestCase(SkillConstants.KnowledgeThePlanes)]
        [TestCase(SkillConstants.OpenLock)]
        [TestCase(SkillConstants.SleightOfHand)]
        [TestCase(SkillConstants.Spellcraft)]
        [TestCase(SkillConstants.Tumble)]
        [TestCase(SkillConstants.UseMagicDevice)]
        public void TrainedSkills(String skill)
        {
            var classNames = new[] {
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard
            };

            foreach (var className in classNames)
                Assert.That(crossClassSkills[className], Is.Not.Contains(skill));
        }

        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Druid)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Paladin)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Wizard)]
        public void AllUntrainedSkills(String className)
        {
            var trainedSkills = new[]
            {
                SkillConstants.DecipherScript,
                SkillConstants.DisableDevice,
                SkillConstants.HandleAnimal,
                SkillConstants.KnowledgeArcana,
                SkillConstants.KnowledgeArchitectureAndEngineering,
                SkillConstants.KnowledgeDungeoneering,
                SkillConstants.KnowledgeGeography,
                SkillConstants.KnowledgeHistory,
                SkillConstants.KnowledgeLocal,
                SkillConstants.KnowledgeNature,
                SkillConstants.KnowledgeNobilityAndRoyalty,
                SkillConstants.KnowledgeReligion,
                SkillConstants.KnowledgeThePlanes,
                SkillConstants.OpenLock,
                SkillConstants.SleightOfHand,
                SkillConstants.Spellcraft,
                SkillConstants.Tumble,
                SkillConstants.UseMagicDevice
            };

            var untrainedSkills = SkillConstants.GetSkills().Except(trainedSkills);
            var allSkills = classSkills[className].Union(crossClassSkills[className]);

            foreach (var skill in untrainedSkills)
                Assert.That(allSkills, Contains.Item(skill));
        }
    }
}