using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.SkillGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                CharacterClassConstants.Domains.Air,
                CharacterClassConstants.Domains.Animal,
                CharacterClassConstants.Domains.Chaos,
                CharacterClassConstants.Domains.Death,
                CharacterClassConstants.Domains.Destruction,
                CharacterClassConstants.Domains.Earth,
                CharacterClassConstants.Domains.Evil,
                CharacterClassConstants.Domains.Fire,
                CharacterClassConstants.Domains.Good,
                CharacterClassConstants.Domains.Healing,
                CharacterClassConstants.Domains.Law,
                CharacterClassConstants.Domains.Luck,
                CharacterClassConstants.Domains.Magic,
                CharacterClassConstants.Domains.Plant,
                CharacterClassConstants.Domains.Protection,
                CharacterClassConstants.Domains.Strength,
                CharacterClassConstants.Domains.Sun,
                CharacterClassConstants.Domains.Travel,
                CharacterClassConstants.Domains.Trickery,
                CharacterClassConstants.Domains.War,
                CharacterClassConstants.Domains.Water,
                CharacterClassConstants.Schools.Abjuration,
                CharacterClassConstants.Schools.Conjuration,
                CharacterClassConstants.Schools.Divination,
                CharacterClassConstants.Schools.Enchantment,
                CharacterClassConstants.Schools.Evocation,
                CharacterClassConstants.Schools.Illusion,
                CharacterClassConstants.Schools.Necromancy,
                CharacterClassConstants.Schools.Transmutation,
                CharacterClassConstants.Domains.Knowledge,
                GroupConstants.Skills,
                FeatConstants.AnimalAffinity,
                FeatConstants.Athletic,
                FeatConstants.Deceitful,
                FeatConstants.MagicalAptitude,
                FeatConstants.Persuasive,
                FeatConstants.SelfSufficient
            };

            AssertCollectionNames(names);
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
        [TestCase(FeatConstants.AnimalAffinity,
            SkillConstants.HandleAnimal,
            SkillConstants.Ride)]
        [TestCase(FeatConstants.Athletic,
            SkillConstants.Climb,
            SkillConstants.Swim)]
        [TestCase(FeatConstants.Deceitful,
            SkillConstants.Disguise,
            SkillConstants.Forgery)]
        [TestCase(FeatConstants.MagicalAptitude,
            SkillConstants.Spellcraft,
            SkillConstants.UseMagicDevice)]
        [TestCase(FeatConstants.Persuasive,
            SkillConstants.Bluff,
            SkillConstants.Intimidate)]
        [TestCase(FeatConstants.SelfSufficient,
            SkillConstants.Heal,
            SkillConstants.Survival)]
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