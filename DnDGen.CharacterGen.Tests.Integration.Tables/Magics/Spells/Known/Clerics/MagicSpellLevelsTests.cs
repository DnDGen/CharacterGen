using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class MagicSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Magic);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.NystulsMagicAura,
                SpellConstants.Identify,
                SpellConstants.DispelMagic,
                SpellConstants.ImbueWithSpellAbility,
                SpellConstants.SpellResistance,
                SpellConstants.AntimagicField,
                SpellConstants.SpellTurning,
                SpellConstants.ProtectionFromSpells,
                SpellConstants.MordenkainensDisjunction
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllMagicSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Magic]);
        }

        [TestCase(SpellConstants.NystulsMagicAura, 1)]
        [TestCase(SpellConstants.Identify, 2)]
        [TestCase(SpellConstants.DispelMagic, 3)]
        [TestCase(SpellConstants.ImbueWithSpellAbility, 4)]
        [TestCase(SpellConstants.SpellResistance, 5)]
        [TestCase(SpellConstants.AntimagicField, 6)]
        [TestCase(SpellConstants.SpellTurning, 7)]
        [TestCase(SpellConstants.ProtectionFromSpells, 8)]
        [TestCase(SpellConstants.MordenkainensDisjunction, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
