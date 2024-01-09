using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Magics;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class WaterSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Water);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var spellGroups = collectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Water]);
        }

        [TestCase(SpellConstants.ObscuringMist, 1)]
        [TestCase(SpellConstants.FogCloud, 2)]
        [TestCase(SpellConstants.WaterBreathing, 3)]
        [TestCase(SpellConstants.ControlWater, 4)]
        [TestCase(SpellConstants.IceStorm, 5)]
        [TestCase(SpellConstants.ConeOfCold, 6)]
        [TestCase(SpellConstants.AcidFog, 7)]
        [TestCase(SpellConstants.HorridWilting, 8)]
        [TestCase(SpellConstants.ElementalSwarm, 9)]
        public void WaterSpellLevel(string spell, int level)
        {
            base.Adjustment(spell, level);
        }
    }
}
