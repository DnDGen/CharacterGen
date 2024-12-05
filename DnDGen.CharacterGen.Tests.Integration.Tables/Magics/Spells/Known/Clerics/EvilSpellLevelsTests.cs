using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class EvilSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Evil);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.ProtectionFromGood,
                SpellConstants.Desecrate,
                SpellConstants.MagicCircleAgainstGood,
                SpellConstants.UnholyBlight,
                SpellConstants.DispelGood,
                SpellConstants.CreateUndead,
                SpellConstants.Blasphemy,
                SpellConstants.UnholyAura,
                SpellConstants.SummonMonsterIX
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllEvilSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Evil]);
        }

        [TestCase(SpellConstants.ProtectionFromGood, 1)]
        [TestCase(SpellConstants.Desecrate, 2)]
        [TestCase(SpellConstants.MagicCircleAgainstGood, 3)]
        [TestCase(SpellConstants.UnholyBlight, 4)]
        [TestCase(SpellConstants.DispelGood, 5)]
        [TestCase(SpellConstants.CreateUndead, 6)]
        [TestCase(SpellConstants.Blasphemy, 7)]
        [TestCase(SpellConstants.UnholyAura, 8)]
        [TestCase(SpellConstants.SummonMonsterIX, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
