using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Clerics
{
    [TestFixture]
    public class Level17ClericSpellsPerDayTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 17, CharacterClassConstants.Cleric);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 10).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 6)]
        [TestCase(1, 5)]
        [TestCase(2, 5)]
        [TestCase(3, 5)]
        [TestCase(4, 5)]
        [TestCase(5, 4)]
        [TestCase(6, 4)]
        [TestCase(7, 3)]
        [TestCase(8, 2)]
        [TestCase(9, 1)]
        public void Adjustment(Int32 spellLevel, Int32 quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
