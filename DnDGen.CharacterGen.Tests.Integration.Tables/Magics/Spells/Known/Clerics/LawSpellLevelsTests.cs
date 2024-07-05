using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Magics;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class LawSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Law);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.ProtectionFromAlignment,
                SpellConstants.CalmEmotions,
                SpellConstants.MagicCircleAgainstAlignment,
                SpellConstants.OrdersWrath,
                SpellConstants.DispelAlignment,
                SpellConstants.HoldMonster,
                SpellConstants.Dictum,
                SpellConstants.ShieldOfLaw,
                SpellConstants.SummonMonsterIX
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllLawSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Law]);
        }

        [TestCase(SpellConstants.ProtectionFromAlignment, 1)]
        [TestCase(SpellConstants.CalmEmotions, 2)]
        [TestCase(SpellConstants.MagicCircleAgainstAlignment, 3)]
        [TestCase(SpellConstants.OrdersWrath, 4)]
        [TestCase(SpellConstants.DispelAlignment, 5)]
        [TestCase(SpellConstants.HoldMonster, 6)]
        [TestCase(SpellConstants.Dictum, 7)]
        [TestCase(SpellConstants.ShieldOfLaw, 8)]
        [TestCase(SpellConstants.SummonMonsterIX, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
