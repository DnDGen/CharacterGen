﻿using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Druids
{
    [TestFixture]
    public class Level3DruidKnownSpellsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, 3, CharacterClassConstants.Druid);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 3).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 4)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void Adjustment(int spellLevel, int quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
