using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Magics;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
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
            var names = new[]
            {
            SpellConstants.ObscuringMist,
            SpellConstants.FogCloud,
            SpellConstants.WaterBreathing,
            SpellConstants.ControlWater,
            SpellConstants.IceStorm,
            SpellConstants.ConeOfCold,
            SpellConstants.AcidFog,
            SpellConstants.HorridWilting,
            SpellConstants.ElementalSwarm
        };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllWaterSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
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
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
