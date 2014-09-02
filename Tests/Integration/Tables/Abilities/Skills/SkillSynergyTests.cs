using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillSynergyTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "SkillSynergy"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get { return SkillConstants.GetSkills(); }
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