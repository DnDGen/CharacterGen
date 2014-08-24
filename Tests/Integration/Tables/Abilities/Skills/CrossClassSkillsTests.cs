﻿using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class CrossClassSkillsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "CrossClassSkills"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get { return CharacterClassConstants.GetClassNames(); }
        }

        [TestCase(CharacterClassConstants.Barbarian,
            SkillConstants.Appraise,
            SkillConstants.Balance,
            SkillConstants.Bluff,
            SkillConstants.Concentration,
            SkillConstants.Diplomacy,
            SkillConstants.Disguise,
            SkillConstants.EscapeArtist,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Heal,
            SkillConstants.Hide,
            SkillConstants.MoveSilently,
            SkillConstants.Perform,
            SkillConstants.Search,
            SkillConstants.SenseMotive,
            SkillConstants.Spot,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Bard,
            SkillConstants.Forgery,
            SkillConstants.Heal,
            SkillConstants.Intimidate,
            SkillConstants.Ride,
            SkillConstants.Search,
            SkillConstants.Spot,
            SkillConstants.Survival,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Cleric,
            SkillConstants.Appraise,
            SkillConstants.Balance,
            SkillConstants.Bluff,
            SkillConstants.Climb,
            SkillConstants.Disguise,
            SkillConstants.EscapeArtist,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Hide,
            SkillConstants.Intimidate,
            SkillConstants.Jump,
            SkillConstants.Listen,
            SkillConstants.MoveSilently,
            SkillConstants.Perform,
            SkillConstants.Ride,
            SkillConstants.Search,
            SkillConstants.SenseMotive,
            SkillConstants.Spot,
            SkillConstants.Survival,
            SkillConstants.Swim,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Druid,
            SkillConstants.Appraise,
            SkillConstants.Balance,
            SkillConstants.Bluff,
            SkillConstants.Climb,
            SkillConstants.Disguise,
            SkillConstants.EscapeArtist,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Hide,
            SkillConstants.Intimidate,
            SkillConstants.Jump,
            SkillConstants.MoveSilently,
            SkillConstants.Perform,
            SkillConstants.Search,
            SkillConstants.SenseMotive,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Fighter,
            SkillConstants.Appraise,
            SkillConstants.Balance,
            SkillConstants.Bluff,
            SkillConstants.Concentration,
            SkillConstants.Diplomacy,
            SkillConstants.Disguise,
            SkillConstants.EscapeArtist,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Heal,
            SkillConstants.Hide,
            SkillConstants.Listen,
            SkillConstants.MoveSilently,
            SkillConstants.Perform,
            SkillConstants.Search,
            SkillConstants.SenseMotive,
            SkillConstants.Spot,
            SkillConstants.Survival,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Monk,
            SkillConstants.Appraise,
            SkillConstants.Bluff,
            SkillConstants.Disguise,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Heal,
            SkillConstants.Intimidate,
            SkillConstants.Ride,
            SkillConstants.Search,
            SkillConstants.Survival,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Paladin,
            SkillConstants.Appraise,
            SkillConstants.Balance,
            SkillConstants.Bluff,
            SkillConstants.Climb,
            SkillConstants.Disguise,
            SkillConstants.EscapeArtist,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Hide,
            SkillConstants.Intimidate,
            SkillConstants.Jump,
            SkillConstants.Listen,
            SkillConstants.MoveSilently,
            SkillConstants.Perform,
            SkillConstants.Search,
            SkillConstants.Spot,
            SkillConstants.Survival,
            SkillConstants.Swim,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Ranger,
            SkillConstants.Appraise,
            SkillConstants.Balance,
            SkillConstants.Bluff,
            SkillConstants.Diplomacy,
            SkillConstants.Disguise,
            SkillConstants.EscapeArtist,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Intimidate,
            SkillConstants.Perform,
            SkillConstants.SenseMotive)]
        [TestCase(CharacterClassConstants.Rogue,
            SkillConstants.Concentration,
            SkillConstants.Heal,
            SkillConstants.Ride,
            SkillConstants.Survival)]
        [TestCase(CharacterClassConstants.Sorcerer,
            SkillConstants.Appraise,
            SkillConstants.Balance,
            SkillConstants.Climb,
            SkillConstants.Diplomacy,
            SkillConstants.Disguise,
            SkillConstants.EscapeArtist,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Heal,
            SkillConstants.Hide,
            SkillConstants.Intimidate,
            SkillConstants.Jump,
            SkillConstants.Listen,
            SkillConstants.MoveSilently,
            SkillConstants.Perform,
            SkillConstants.Ride,
            SkillConstants.Search,
            SkillConstants.SenseMotive,
            SkillConstants.Spot,
            SkillConstants.Survival,
            SkillConstants.Swim,
            SkillConstants.UseRope)]
        [TestCase(CharacterClassConstants.Wizard,
            SkillConstants.Appraise,
            SkillConstants.Balance,
            SkillConstants.Bluff,
            SkillConstants.Climb,
            SkillConstants.Diplomacy,
            SkillConstants.Disguise,
            SkillConstants.EscapeArtist,
            SkillConstants.Forgery,
            SkillConstants.GatherInformation,
            SkillConstants.Heal,
            SkillConstants.Hide,
            SkillConstants.Intimidate,
            SkillConstants.Jump,
            SkillConstants.Listen,
            SkillConstants.MoveSilently,
            SkillConstants.Perform,
            SkillConstants.Ride,
            SkillConstants.Search,
            SkillConstants.SenseMotive,
            SkillConstants.Spot,
            SkillConstants.Survival,
            SkillConstants.Swim,
            SkillConstants.UseRope)]
        public override void DistinctCollection(String name, params String[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}