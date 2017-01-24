using CharacterGen.Abilities.Skills;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Mappers.Collections;
using CharacterGen.Domain.Tables;
using Ninject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    [Table]
    public class CrossSkillCollectionTests : IntegrationTests
    {
        [Inject]
        internal CollectionsMapper Mapper { get; set; }

        private Dictionary<string, IEnumerable<string>> classSkills;
        private Dictionary<string, IEnumerable<string>> crossClassSkills;
        private IEnumerable<string> allSkills;

        [SetUp]
        public void Setup()
        {
            classSkills = Mapper.Map(TableNameConstants.Set.Collection.ClassSkills);
            crossClassSkills = Mapper.Map(TableNameConstants.Set.Collection.CrossClassSkills);

            var skills = Mapper.Map(TableNameConstants.Set.Collection.SkillGroups);
            allSkills = skills[GroupConstants.Skills];
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
        [TestCase(CharacterClassConstants.Adept)]
        [TestCase(CharacterClassConstants.Aristocrat)]
        [TestCase(CharacterClassConstants.Commoner)]
        [TestCase(CharacterClassConstants.Expert)]
        [TestCase(CharacterClassConstants.Warrior)]
        public void NoIntersectionBetweenClassAndCrossClassSkills(string className)
        {
            var intersect = classSkills[className].Intersect(crossClassSkills[className]);
            Assert.That(intersect, Is.Empty, className);
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
        public void TrainedSkills(string skill)
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
                CharacterClassConstants.Wizard,
                CharacterClassConstants.Adept,
                CharacterClassConstants.Aristocrat,
                CharacterClassConstants.Commoner,
                CharacterClassConstants.Expert,
                CharacterClassConstants.Warrior
            };

            foreach (var className in classNames)
                Assert.That(crossClassSkills[className], Is.All.Not.EqualTo(skill), className);
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
        [TestCase(CharacterClassConstants.Adept)]
        [TestCase(CharacterClassConstants.Aristocrat)]
        [TestCase(CharacterClassConstants.Commoner)]
        [TestCase(CharacterClassConstants.Expert)]
        [TestCase(CharacterClassConstants.Warrior)]
        public void AllUntrainedSkills(string className)
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

            var untrainedSkills = allSkills.Except(trainedSkills);
            var allCharacterSkills = classSkills[className].Union(crossClassSkills[className]);

            foreach (var skill in untrainedSkills)
                Assert.That(allCharacterSkills, Contains.Item(skill));
        }
    }
}