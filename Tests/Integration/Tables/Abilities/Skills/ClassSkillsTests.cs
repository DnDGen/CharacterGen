using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class ClassSkillsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Collection.ClassSkills; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
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
                RaceConstants.BaseRaces.Lizardfolk,
                RaceConstants.BaseRaces.Gnoll,
                RaceConstants.BaseRaces.Ogre,
                RaceConstants.BaseRaces.Troglodyte,
                RaceConstants.BaseRaces.Bugbear,
                RaceConstants.BaseRaces.OgreMage
            };

            AssertCollectionNames(names);
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
        [TestCase(RaceConstants.BaseRaces.Lizardfolk,
            SkillConstants.Balance,
            SkillConstants.Jump,
            SkillConstants.Swim)]
        [TestCase(RaceConstants.BaseRaces.Ogre,
            SkillConstants.Climb,
            SkillConstants.Listen,
            SkillConstants.Spot)]
        [TestCase(RaceConstants.BaseRaces.Gnoll,
            SkillConstants.Listen,
            SkillConstants.Spot)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte,
            SkillConstants.Listen,
            SkillConstants.Hide)]
        [TestCase(RaceConstants.BaseRaces.OgreMage,
            SkillConstants.Listen,
            SkillConstants.Spot,
            SkillConstants.Spellcraft,
            SkillConstants.Concentration)]
        [TestCase(RaceConstants.BaseRaces.Bugbear,
            SkillConstants.Climb,
            SkillConstants.Hide,
            SkillConstants.Listen,
            SkillConstants.MoveSilently,
            SkillConstants.Search,
            SkillConstants.Spot)]
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