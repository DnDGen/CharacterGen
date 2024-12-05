using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class GoodSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Good);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.ProtectionFromEvil,
                SpellConstants.Aid,
                SpellConstants.MagicCircleAgainstEvil,
                SpellConstants.HolySmite,
                SpellConstants.DispelEvil,
                SpellConstants.BladeBarrier,
                SpellConstants.HolyWord,
                SpellConstants.HolyAura,
                SpellConstants.SummonMonsterIX
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllGoodSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Good]);
        }

        [TestCase(SpellConstants.ProtectionFromEvil, 1)]
        [TestCase(SpellConstants.Aid, 2)]
        [TestCase(SpellConstants.MagicCircleAgainstEvil, 3)]
        [TestCase(SpellConstants.HolySmite, 4)]
        [TestCase(SpellConstants.DispelEvil, 5)]
        [TestCase(SpellConstants.BladeBarrier, 6)]
        [TestCase(SpellConstants.HolyWord, 7)]
        [TestCase(SpellConstants.HolyAura, 8)]
        [TestCase(SpellConstants.SummonMonsterIX, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
