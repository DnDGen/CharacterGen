using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class ClassHitDiceTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Adjustments.ClassHitDice; }
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
                CharacterClassConstants.Adept,
                CharacterClassConstants.Aristocrat,
                CharacterClassConstants.Commoner,
                CharacterClassConstants.Expert,
                CharacterClassConstants.Warrior
            };

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Barbarian, 12)]
        [TestCase(CharacterClassConstants.Bard, 6)]
        [TestCase(CharacterClassConstants.Cleric, 8)]
        [TestCase(CharacterClassConstants.Druid, 8)]
        [TestCase(CharacterClassConstants.Fighter, 10)]
        [TestCase(CharacterClassConstants.Monk, 8)]
        [TestCase(CharacterClassConstants.Paladin, 10)]
        [TestCase(CharacterClassConstants.Ranger, 8)]
        [TestCase(CharacterClassConstants.Rogue, 6)]
        [TestCase(CharacterClassConstants.Sorcerer, 4)]
        [TestCase(CharacterClassConstants.Wizard, 4)]
        [TestCase(CharacterClassConstants.Adept, 6)]
        [TestCase(CharacterClassConstants.Aristocrat, 8)]
        [TestCase(CharacterClassConstants.Commoner, 4)]
        [TestCase(CharacterClassConstants.Expert, 6)]
        [TestCase(CharacterClassConstants.Warrior, 8)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}