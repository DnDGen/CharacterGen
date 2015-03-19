using System;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class SpecialistFieldQuantities : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.SpecialistFieldQuantities; }
        }

        [TestCase(CharacterClassConstants.Barbarian, 0)]
        [TestCase(CharacterClassConstants.Bard, 0)]
        [TestCase(CharacterClassConstants.Cleric, 2)]
        [TestCase(CharacterClassConstants.Druid, 0)]
        [TestCase(CharacterClassConstants.Fighter, 0)]
        [TestCase(CharacterClassConstants.Monk, 0)]
        [TestCase(CharacterClassConstants.Paladin, 0)]
        [TestCase(CharacterClassConstants.Ranger, 0)]
        [TestCase(CharacterClassConstants.Rogue, 0)]
        [TestCase(CharacterClassConstants.Sorcerer, 0)]
        [TestCase(CharacterClassConstants.Wizard, 1)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}