using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Rangers
{
    [TestFixture]
    public class Level13RangerSpellsPerDayTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 13, CharacterClassConstants.Ranger);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(1, 3).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        public void Adjustment(Int32 spellLevel, Int32 quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
