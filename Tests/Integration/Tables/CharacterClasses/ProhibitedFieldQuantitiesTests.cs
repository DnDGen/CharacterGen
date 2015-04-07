using System;
using NPCGen.Common.CharacterClasses;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class ProhibitedFieldQuantitiesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Adjustments.ProhibitedFieldQuantities; }
        }

        [TestCase(CharacterClassConstants.Schools.Abjuration, 2)]
        [TestCase(CharacterClassConstants.Schools.Conjuration, 2)]
        [TestCase(CharacterClassConstants.Schools.Divination, 1)]
        [TestCase(CharacterClassConstants.Schools.Enchantment, 2)]
        [TestCase(CharacterClassConstants.Schools.Evocation, 2)]
        [TestCase(CharacterClassConstants.Schools.Illusion, 2)]
        [TestCase(CharacterClassConstants.Schools.Necromancy, 2)]
        [TestCase(CharacterClassConstants.Schools.Transmutation, 2)]
        [TestCase(CharacterClassConstants.Domains.Air, 0)]
        [TestCase(CharacterClassConstants.Domains.Animal, 0)]
        [TestCase(CharacterClassConstants.Domains.Chaos, 0)]
        [TestCase(CharacterClassConstants.Domains.Death, 0)]
        [TestCase(CharacterClassConstants.Domains.Destruction, 0)]
        [TestCase(CharacterClassConstants.Domains.Earth, 0)]
        [TestCase(CharacterClassConstants.Domains.Evil, 0)]
        [TestCase(CharacterClassConstants.Domains.Fire, 0)]
        [TestCase(CharacterClassConstants.Domains.Good, 0)]
        [TestCase(CharacterClassConstants.Domains.Healing, 0)]
        [TestCase(CharacterClassConstants.Domains.Knowledge, 0)]
        [TestCase(CharacterClassConstants.Domains.Law, 0)]
        [TestCase(CharacterClassConstants.Domains.Luck, 0)]
        [TestCase(CharacterClassConstants.Domains.Magic, 0)]
        [TestCase(CharacterClassConstants.Domains.Plant, 0)]
        [TestCase(CharacterClassConstants.Domains.Protection, 0)]
        [TestCase(CharacterClassConstants.Domains.Strength, 0)]
        [TestCase(CharacterClassConstants.Domains.Sun, 0)]
        [TestCase(CharacterClassConstants.Domains.Travel, 0)]
        [TestCase(CharacterClassConstants.Domains.Trickery, 0)]
        [TestCase(CharacterClassConstants.Domains.War, 0)]
        [TestCase(CharacterClassConstants.Domains.Water, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}