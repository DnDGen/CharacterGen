using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.Abilities.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillDataTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "SkillData"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get { return SkillConstants.GetSkills(); }
        }

        [TestCase(SkillConstants.Appraise, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.Balance, StatConstants.Dexterity, true)]
        [TestCase(SkillConstants.Bluff, StatConstants.Charisma, false)]
        [TestCase(SkillConstants.Climb, StatConstants.Strength, true)]
        [TestCase(SkillConstants.Concentration, StatConstants.Constitution, false)]
        [TestCase(SkillConstants.DecipherScript, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.Diplomacy, StatConstants.Charisma, false)]
        [TestCase(SkillConstants.DisableDevice, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.Disguise, StatConstants.Charisma, false)]
        [TestCase(SkillConstants.EscapeArtist, StatConstants.Dexterity, true)]
        [TestCase(SkillConstants.Forgery, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.GatherInformation, StatConstants.Charisma, false)]
        [TestCase(SkillConstants.HandleAnimal, StatConstants.Charisma, false)]
        [TestCase(SkillConstants.Heal, StatConstants.Wisdom, false)]
        [TestCase(SkillConstants.Hide, StatConstants.Dexterity, true)]
        [TestCase(SkillConstants.Intimidate, StatConstants.Charisma, false)]
        [TestCase(SkillConstants.Jump, StatConstants.Strength, true)]
        [TestCase(SkillConstants.KnowledgeArcana, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.KnowledgeArchitectureAndEngineering, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.KnowledgeDungeoneering, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.KnowledgeGeography, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.KnowledgeHistory, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.KnowledgeLocal, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.KnowledgeNature, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.KnowledgeNobilityAndRoyalty, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.KnowledgeReligion, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.KnowledgeThePlanes, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.Listen, StatConstants.Wisdom, false)]
        [TestCase(SkillConstants.MoveSilently, StatConstants.Dexterity, true)]
        [TestCase(SkillConstants.OpenLock, StatConstants.Dexterity, false)]
        [TestCase(SkillConstants.Perform, StatConstants.Charisma, false)]
        [TestCase(SkillConstants.Ride, StatConstants.Dexterity, false)]
        [TestCase(SkillConstants.Search, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.SenseMotive, StatConstants.Wisdom, false)]
        [TestCase(SkillConstants.SleightOfHand, StatConstants.Dexterity, true)]
        [TestCase(SkillConstants.Spellcraft, StatConstants.Intelligence, false)]
        [TestCase(SkillConstants.Spot, StatConstants.Wisdom, false)]
        [TestCase(SkillConstants.Survival, StatConstants.Wisdom, false)]
        [TestCase(SkillConstants.Swim, StatConstants.Strength, true)]
        [TestCase(SkillConstants.Tumble, StatConstants.Dexterity, true)]
        [TestCase(SkillConstants.UseMagicDevice, StatConstants.Charisma, false)]
        [TestCase(SkillConstants.UseRope, StatConstants.Dexterity, false)]
        public void OrderedCollection(String name, String baseStat, Boolean armorCheckPenalty)
        {
            var collection = new[] { baseStat, Convert.ToString(armorCheckPenalty) };
            base.OrderedCollection(name, collection);
        }
    }
}