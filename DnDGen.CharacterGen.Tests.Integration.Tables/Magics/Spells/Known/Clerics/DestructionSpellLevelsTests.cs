using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class DestructionSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Destruction);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.InflictLightWounds,
                SpellConstants.Shatter,
                SpellConstants.Contagion,
                SpellConstants.InflictCriticalWounds,
                SpellConstants.InflictLightWounds_Mass,
                SpellConstants.Harm,
                SpellConstants.Disintegrate,
                SpellConstants.Earthquake,
                SpellConstants.Implosion
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllDestructionSpellsInAdjustmentsTable()
        {
            var spellGroups = GetTable(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Destruction]);
        }

        [TestCase(SpellConstants.InflictLightWounds, 1)]
        [TestCase(SpellConstants.Shatter, 2)]
        [TestCase(SpellConstants.Contagion, 3)]
        [TestCase(SpellConstants.InflictCriticalWounds, 4)]
        [TestCase(SpellConstants.InflictLightWounds_Mass, 5)]
        [TestCase(SpellConstants.Harm, 6)]
        [TestCase(SpellConstants.Disintegrate, 7)]
        [TestCase(SpellConstants.Earthquake, 8)]
        [TestCase(SpellConstants.Implosion, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
