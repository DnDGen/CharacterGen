using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class SkillPointsForClassesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "SkillPointsForClasses"; }
        }

        [TestCase(CharacterClassConstants.Barbarian, 4)]
        [TestCase(CharacterClassConstants.Bard, 6)]
        [TestCase(CharacterClassConstants.Cleric, 2)]
        [TestCase(CharacterClassConstants.Druid, 4)]
        [TestCase(CharacterClassConstants.Fighter, 2)]
        [TestCase(CharacterClassConstants.Monk, 4)]
        [TestCase(CharacterClassConstants.Paladin, 2)]
        [TestCase(CharacterClassConstants.Ranger, 6)]
        [TestCase(CharacterClassConstants.Rogue, 8)]
        [TestCase(CharacterClassConstants.Sorcerer, 2)]
        [TestCase(CharacterClassConstants.Wizard, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}