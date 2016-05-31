using System;
using CharacterGen.Abilities.Skills;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillSynergyTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.SkillSynergy; }
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

        [TestCase(SkillConstants.Bluff,
            SkillConstants.Diplomacy,
            SkillConstants.Intimidate,
            SkillConstants.SleightOfHand)]
        [TestCase(SkillConstants.HandleAnimal,
            SkillConstants.Ride)]
        [TestCase(SkillConstants.Jump,
            SkillConstants.Tumble)]
        [TestCase(SkillConstants.KnowledgeArcana,
            SkillConstants.Spellcraft)]
        [TestCase(SkillConstants.KnowledgeLocal,
            SkillConstants.GatherInformation)]
        [TestCase(SkillConstants.KnowledgeNobilityAndRoyalty,
            SkillConstants.Diplomacy)]
        [TestCase(SkillConstants.SenseMotive,
            SkillConstants.Diplomacy)]
        [TestCase(SkillConstants.Survival,
            SkillConstants.KnowledgeNature)]
        [TestCase(SkillConstants.Tumble,
            SkillConstants.Balance,
            SkillConstants.Jump)]
        [TestCase(SkillConstants.Appraise)]
        [TestCase(SkillConstants.Balance)]
        [TestCase(SkillConstants.Climb)]
        [TestCase(SkillConstants.Concentration)]
        [TestCase(SkillConstants.DecipherScript)]
        [TestCase(SkillConstants.Diplomacy)]
        [TestCase(SkillConstants.DisableDevice)]
        [TestCase(SkillConstants.Disguise)]
        [TestCase(SkillConstants.EscapeArtist)]
        [TestCase(SkillConstants.Forgery)]
        [TestCase(SkillConstants.GatherInformation)]
        [TestCase(SkillConstants.Heal)]
        [TestCase(SkillConstants.Hide)]
        [TestCase(SkillConstants.Intimidate)]
        [TestCase(SkillConstants.KnowledgeArchitectureAndEngineering)]
        [TestCase(SkillConstants.KnowledgeDungeoneering)]
        [TestCase(SkillConstants.KnowledgeGeography)]
        [TestCase(SkillConstants.KnowledgeHistory)]
        [TestCase(SkillConstants.KnowledgeNature)]
        [TestCase(SkillConstants.KnowledgeReligion)]
        [TestCase(SkillConstants.KnowledgeThePlanes)]
        [TestCase(SkillConstants.Listen)]
        [TestCase(SkillConstants.MoveSilently)]
        [TestCase(SkillConstants.OpenLock)]
        [TestCase(SkillConstants.Perform)]
        [TestCase(SkillConstants.Ride)]
        [TestCase(SkillConstants.Search)]
        [TestCase(SkillConstants.SleightOfHand)]
        [TestCase(SkillConstants.Spellcraft)]
        [TestCase(SkillConstants.Spot)]
        [TestCase(SkillConstants.Swim)]
        [TestCase(SkillConstants.UseMagicDevice)]
        [TestCase(SkillConstants.UseRope)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}