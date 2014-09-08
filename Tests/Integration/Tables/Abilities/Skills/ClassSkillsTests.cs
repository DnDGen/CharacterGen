using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class ClassSkillsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "ClassSkills"; }
        }

        [TestCase(CharacterClassConstants.Barbarian,
            SkillConstants.Climb,
            SkillConstants.HandleAnimal,
            SkillConstants.Intimidate,
            SkillConstants.Jump,
            SkillConstants.Listen,
            SkillConstants.Ride,
            SkillConstants.Survival,
            SkillConstants.Swim)]
        [TestCase(CharacterClassConstants.Cleric,
            SkillConstants.Concentration,
            SkillConstants.Diplomacy,
            SkillConstants.Heal,
            SkillConstants.KnowledgeArcana,
            SkillConstants.KnowledgeHistory,
            SkillConstants.KnowledgeReligion,
            SkillConstants.KnowledgeThePlanes,
            SkillConstants.Spellcraft)]
        [TestCase(CharacterClassConstants.Druid,
            SkillConstants.Concentration,
            SkillConstants.Diplomacy,
            SkillConstants.HandleAnimal,
            SkillConstants.Heal,
            SkillConstants.KnowledgeNature,
            SkillConstants.Listen,
            SkillConstants.Ride,
            SkillConstants.Spellcraft,
            SkillConstants.Spot,
            SkillConstants.Survival,
            SkillConstants.Swim)]
        [TestCase(CharacterClassConstants.Fighter,
            SkillConstants.Climb,
            SkillConstants.HandleAnimal,
            SkillConstants.Intimidate,
            SkillConstants.Jump,
            SkillConstants.Ride,
            SkillConstants.Swim)]
        [TestCase(CharacterClassConstants.Monk,
            SkillConstants.Balance,
            SkillConstants.Climb,
            SkillConstants.Concentration,
            SkillConstants.Diplomacy,
            SkillConstants.EscapeArtist,
            SkillConstants.Hide,
            SkillConstants.Jump,
            SkillConstants.KnowledgeArcana,
            SkillConstants.KnowledgeReligion,
            SkillConstants.Listen,
            SkillConstants.MoveSilently,
            SkillConstants.Perform,
            SkillConstants.SenseMotive,
            SkillConstants.Spot,
            SkillConstants.Swim,
            SkillConstants.Tumble)]
        [TestCase(CharacterClassConstants.Paladin,
            SkillConstants.Concentration,
            SkillConstants.Diplomacy,
            SkillConstants.HandleAnimal,
            SkillConstants.Heal,
            SkillConstants.KnowledgeNobilityAndRoyalty,
            SkillConstants.KnowledgeReligion,
            SkillConstants.Ride,
            SkillConstants.SenseMotive)]
        [TestCase(CharacterClassConstants.Ranger,
            SkillConstants.Climb,
            SkillConstants.Concentration,
            SkillConstants.HandleAnimal,
            SkillConstants.Heal,
            SkillConstants.Hide,
            SkillConstants.Jump,
            SkillConstants.KnowledgeDungeoneering,
            SkillConstants.KnowledgeGeography,
            SkillConstants.KnowledgeNature,
            SkillConstants.Listen,
            SkillConstants.MoveSilently,
            SkillConstants.Ride,
            SkillConstants.Search,
            SkillConstants.Spot,
            SkillConstants.Survival,
            SkillConstants.Swim,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Rogue,
            SkillConstants.Appraise,
            SkillConstants.Balance,
            SkillConstants.Bluff,
            SkillConstants.Climb,
            SkillConstants.DecipherScript,
            SkillConstants.Diplomacy,
            SkillConstants.DisableDevice,
            SkillConstants.Disguise,
            SkillConstants.EscapeArtist,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Hide,
            SkillConstants.Intimidate,
            SkillConstants.Jump,
            SkillConstants.KnowledgeLocal,
            SkillConstants.Listen,
            SkillConstants.MoveSilently,
            SkillConstants.OpenLock,
            SkillConstants.Perform,
            SkillConstants.Search,
            SkillConstants.SenseMotive,
            SkillConstants.SleightOfHand,
            SkillConstants.Spot,
            SkillConstants.Swim,
            SkillConstants.Tumble,
            SkillConstants.UseMagicDevice,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Sorcerer,
            SkillConstants.Bluff,
            SkillConstants.Concentration,
            SkillConstants.KnowledgeArcana,
            SkillConstants.Spellcraft)]
        [TestCase(CharacterClassConstants.Wizard,
            SkillConstants.Concentration,
            SkillConstants.DecipherScript,
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
            SkillConstants.Spellcraft)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        //HACK: Doing this as a test case is too long a name for NUnit
        public void BardClassSkills()
        {

            var classSkills = new[] 
            {
                SkillConstants.Appraise,
                SkillConstants.Balance,
                SkillConstants.Bluff,
                SkillConstants.Climb,
                SkillConstants.Concentration,
                SkillConstants.DecipherScript,
                SkillConstants.Diplomacy,
                SkillConstants.Disguise,
                SkillConstants.EscapeArtist,
                SkillConstants.GatherInformation,
                SkillConstants.Hide,
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
                SkillConstants.Perform,
                SkillConstants.SenseMotive,
                SkillConstants.SleightOfHand,
                SkillConstants.Spellcraft,
                SkillConstants.Swim,
                SkillConstants.Tumble,
                SkillConstants.UseMagicDevice
            };

            DistinctCollection(CharacterClassConstants.Bard, classSkills);
        }
    }
}