﻿using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Sorcerers
{
    [TestFixture]
    public class Level4SorcererKnownSpellsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, 4, CharacterClassConstants.Sorcerer);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 3).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 6)]
        [TestCase(1, 3)]
        [TestCase(2, 1)]
        public void Adjustment(int spellLevel, int quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
