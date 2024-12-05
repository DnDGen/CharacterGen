using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class HealingSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Healing);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.CureLightWounds,
                SpellConstants.CureModerateWounds,
                SpellConstants.CureSeriousWounds,
                SpellConstants.CureCriticalWounds,
                SpellConstants.CureLightWounds_Mass,
                SpellConstants.Heal,
                SpellConstants.Regenerate,
                SpellConstants.CureCriticalWounds_Mass,
                SpellConstants.Heal_Mass
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllHealingSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Healing]);
        }

        [TestCase(SpellConstants.CureLightWounds, 1)]
        [TestCase(SpellConstants.CureModerateWounds, 2)]
        [TestCase(SpellConstants.CureSeriousWounds, 3)]
        [TestCase(SpellConstants.CureCriticalWounds, 4)]
        [TestCase(SpellConstants.CureLightWounds_Mass, 5)]
        [TestCase(SpellConstants.Heal, 6)]
        [TestCase(SpellConstants.Regenerate, 7)]
        [TestCase(SpellConstants.CureCriticalWounds_Mass, 8)]
        [TestCase(SpellConstants.Heal_Mass, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
