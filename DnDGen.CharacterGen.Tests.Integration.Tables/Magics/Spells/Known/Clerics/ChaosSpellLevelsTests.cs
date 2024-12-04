using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class ChaosSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Chaos);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.ProtectionFromLaw,
                SpellConstants.Shatter,
                SpellConstants.MagicCircleAgainstLaw,
                SpellConstants.ChaosHammer,
                SpellConstants.DispelLaw,
                SpellConstants.AnimateObjects,
                SpellConstants.WordOfChaos,
                SpellConstants.CloakOfChaos,
                SpellConstants.SummonMonsterIX
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllChaosSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Chaos]);
        }

        [TestCase(SpellConstants.ProtectionFromLaw, 1)]
        [TestCase(SpellConstants.Shatter, 2)]
        [TestCase(SpellConstants.MagicCircleAgainstLaw, 3)]
        [TestCase(SpellConstants.ChaosHammer, 4)]
        [TestCase(SpellConstants.DispelLaw, 5)]
        [TestCase(SpellConstants.AnimateObjects, 6)]
        [TestCase(SpellConstants.WordOfChaos, 7)]
        [TestCase(SpellConstants.CloakOfChaos, 8)]
        [TestCase(SpellConstants.SummonMonsterIX, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
