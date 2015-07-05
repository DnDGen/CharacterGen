using System;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.SkillGroups; }
        }

        [TestCase(CharacterClassConstants.Domains.Air)]
        [TestCase(CharacterClassConstants.Domains.Animal,
            SkillConstants.KnowledgeNature)]
        [TestCase(CharacterClassConstants.Domains.Chaos)]
        [TestCase(CharacterClassConstants.Domains.Death)]
        [TestCase(CharacterClassConstants.Domains.Destruction)]
        [TestCase(CharacterClassConstants.Domains.Earth)]
        [TestCase(CharacterClassConstants.Domains.Evil)]
        [TestCase(CharacterClassConstants.Domains.Fire)]
        [TestCase(CharacterClassConstants.Domains.Good)]
        [TestCase(CharacterClassConstants.Domains.Healing)]
        [TestCase(CharacterClassConstants.Domains.Law)]
        [TestCase(CharacterClassConstants.Domains.Luck)]
        [TestCase(CharacterClassConstants.Domains.Magic)]
        [TestCase(CharacterClassConstants.Domains.Plant,
            SkillConstants.KnowledgeNature)]
        [TestCase(CharacterClassConstants.Domains.Protection)]
        [TestCase(CharacterClassConstants.Domains.Strength)]
        [TestCase(CharacterClassConstants.Domains.Sun)]
        [TestCase(CharacterClassConstants.Domains.Travel,
            SkillConstants.Survival)]
        [TestCase(CharacterClassConstants.Domains.Trickery,
            SkillConstants.Bluff,
            SkillConstants.Disguise,
            SkillConstants.Hide)]
        [TestCase(CharacterClassConstants.Domains.War)]
        [TestCase(CharacterClassConstants.Domains.Water)]
        [TestCase(CharacterClassConstants.Schools.Abjuration)]
        [TestCase(CharacterClassConstants.Schools.Conjuration)]
        [TestCase(CharacterClassConstants.Schools.Divination)]
        [TestCase(CharacterClassConstants.Schools.Enchantment)]
        [TestCase(CharacterClassConstants.Schools.Evocation)]
        [TestCase(CharacterClassConstants.Schools.Illusion)]
        [TestCase(CharacterClassConstants.Schools.Necromancy)]
        [TestCase(CharacterClassConstants.Schools.Transmutation)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void AllSkills()
        {
            var skills = new[] 
            {
                SkillConstants.Appraise,
                SkillConstants.Balance,
                SkillConstants.Bluff,
                SkillConstants.Climb,
                SkillConstants.Concentration,
                SkillConstants.DecipherScript,
                SkillConstants.Diplomacy,
                SkillConstants.DisableDevice,
                SkillConstants.Disguise,
                SkillConstants.EscapeArtist,
                SkillConstants.Forgery,
                SkillConstants.GatherInformation,
                SkillConstants.HandleAnimal,
                SkillConstants.Heal,
                SkillConstants.Hide,
                SkillConstants.Intimidate,
                SkillConstants.Jump,
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
                SkillConstants.Listen,
                SkillConstants.MoveSilently,
                SkillConstants.OpenLock,
                SkillConstants.Perform,
                SkillConstants.Ride,
                SkillConstants.Search,
                SkillConstants.SenseMotive,
                SkillConstants.SleightOfHand,
                SkillConstants.Spellcraft,
                SkillConstants.Spot,
                SkillConstants.Survival,
                SkillConstants.Swim,
                SkillConstants.Tumble,
                SkillConstants.UseMagicDevice,
                SkillConstants.UseRope
            };

            base.DistinctCollection(GroupConstants.Skills, skills);
        }

        [Test]
        public void KnowledgeSkills()
        {
            var skills = new[] 
            {
                SkillConstants.KnowledgeArcana,
                SkillConstants.KnowledgeArchitectureAndEngineering,
                SkillConstants.KnowledgeDungeoneering,
                SkillConstants.KnowledgeGeography,
                SkillConstants.KnowledgeHistory,
                SkillConstants.KnowledgeLocal,
                SkillConstants.KnowledgeNature,
                SkillConstants.KnowledgeNobilityAndRoyalty,
                SkillConstants.KnowledgeReligion,
                SkillConstants.KnowledgeThePlanes
            };

            base.DistinctCollection(GroupConstants.Knowledge, skills);
        }

        [Test]
        public void KnowledgeDomainSkills()
        {
            var skills = new[] 
            {
                SkillConstants.KnowledgeArcana,
                SkillConstants.KnowledgeArchitectureAndEngineering,
                SkillConstants.KnowledgeDungeoneering,
                SkillConstants.KnowledgeGeography,
                SkillConstants.KnowledgeHistory,
                SkillConstants.KnowledgeLocal,
                SkillConstants.KnowledgeNature,
                SkillConstants.KnowledgeNobilityAndRoyalty,
                SkillConstants.KnowledgeReligion,
                SkillConstants.KnowledgeThePlanes
            };

            base.DistinctCollection(CharacterClassConstants.Domains.Knowledge, skills);
        }
    }
}