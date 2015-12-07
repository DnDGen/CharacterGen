using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.SkillData; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
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

            AssertCollectionNames(names);
        }

        [TestCase(SkillConstants.Appraise, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Balance, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Bluff, StatConstants.Charisma)]
        [TestCase(SkillConstants.Climb, StatConstants.Strength)]
        [TestCase(SkillConstants.Concentration, StatConstants.Constitution)]
        [TestCase(SkillConstants.DecipherScript, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Diplomacy, StatConstants.Charisma)]
        [TestCase(SkillConstants.DisableDevice, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Disguise, StatConstants.Charisma)]
        [TestCase(SkillConstants.EscapeArtist, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Forgery, StatConstants.Intelligence)]
        [TestCase(SkillConstants.GatherInformation, StatConstants.Charisma)]
        [TestCase(SkillConstants.HandleAnimal, StatConstants.Charisma)]
        [TestCase(SkillConstants.Heal, StatConstants.Wisdom)]
        [TestCase(SkillConstants.Hide, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Intimidate, StatConstants.Charisma)]
        [TestCase(SkillConstants.Jump, StatConstants.Strength)]
        [TestCase(SkillConstants.KnowledgeArcana, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeArchitectureAndEngineering, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeDungeoneering, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeGeography, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeHistory, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeLocal, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeNature, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeNobilityAndRoyalty, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeReligion, StatConstants.Intelligence)]
        [TestCase(SkillConstants.KnowledgeThePlanes, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Listen, StatConstants.Wisdom)]
        [TestCase(SkillConstants.MoveSilently, StatConstants.Dexterity)]
        [TestCase(SkillConstants.OpenLock, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma)]
        [TestCase(SkillConstants.Ride, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Search, StatConstants.Intelligence)]
        [TestCase(SkillConstants.SenseMotive, StatConstants.Wisdom)]
        [TestCase(SkillConstants.SleightOfHand, StatConstants.Dexterity)]
        [TestCase(SkillConstants.Spellcraft, StatConstants.Intelligence)]
        [TestCase(SkillConstants.Spot, StatConstants.Wisdom)]
        [TestCase(SkillConstants.Survival, StatConstants.Wisdom)]
        [TestCase(SkillConstants.Swim, StatConstants.Strength)]
        [TestCase(SkillConstants.Tumble, StatConstants.Dexterity)]
        [TestCase(SkillConstants.UseMagicDevice, StatConstants.Charisma)]
        [TestCase(SkillConstants.UseRope, StatConstants.Dexterity)]
        public void OrderedCollection(String name, String baseStat)
        {
            var collection = new[] { baseStat };
            base.OrderedCollection(name, collection);
        }
    }
}