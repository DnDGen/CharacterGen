﻿using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Wizards
{
    [TestFixture]
    public class Level11WizardKnownSpellsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, 11, CharacterClassConstants.Wizard);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 7).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 4)]
        [TestCase(1, 4)]
        [TestCase(2, 4)]
        [TestCase(3, 4)]
        [TestCase(4, 3)]
        [TestCase(5, 2)]
        [TestCase(6, 1)]
        public void Adjustment(int spellLevel, int quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}