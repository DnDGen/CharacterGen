using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Druids
{
    [TestFixture]
    public class Level3DruidSpellsPerDayTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 3, CharacterClassConstants.Druid);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 3).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 4)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void Adjustment(Int32 spellLevel, Int32 quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
