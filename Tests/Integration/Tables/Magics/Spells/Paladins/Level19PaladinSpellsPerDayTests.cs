using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Paladins
{
    [TestFixture]
    public class Level19PaladinSpellsPerDayTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 19, CharacterClassConstants.Paladin);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(1, 4).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(1, 3)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        [TestCase(4, 2)]
        public void Adjustment(Int32 spellLevel, Int32 quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
