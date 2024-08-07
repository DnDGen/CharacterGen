﻿using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Magics;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class EarthSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Earth);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.MagicStone,
                SpellConstants.SoftenEarthAndStone,
                SpellConstants.StoneShape,
                SpellConstants.SpikeStones,
                SpellConstants.WallOfStone,
                SpellConstants.Stoneskin,
                SpellConstants.Earthquake,
                SpellConstants.IronBody,
                SpellConstants.ElementalSwarm
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllEarthSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Earth]);
        }

        [TestCase(SpellConstants.MagicStone, 1)]
        [TestCase(SpellConstants.SoftenEarthAndStone, 2)]
        [TestCase(SpellConstants.StoneShape, 3)]
        [TestCase(SpellConstants.SpikeStones, 4)]
        [TestCase(SpellConstants.WallOfStone, 5)]
        [TestCase(SpellConstants.Stoneskin, 6)]
        [TestCase(SpellConstants.Earthquake, 7)]
        [TestCase(SpellConstants.IronBody, 8)]
        [TestCase(SpellConstants.ElementalSwarm, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
