using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Rangers
{
    [TestFixture]
    public class Level9RangerSpellsPerDayTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 9, CharacterClassConstants.Ranger);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(1, 2).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(1, 1)]
        [TestCase(2, 0)]
        public void Adjustment(Int32 spellLevel, Int32 quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
