using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Sorcerers
{
    [TestFixture]
    public class Level9SorcererSpellsPerDayTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 9, CharacterClassConstants.Sorcerer);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 6).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 6)]
        [TestCase(1, 6)]
        [TestCase(2, 6)]
        [TestCase(3, 6)]
        [TestCase(4, 4)]
        public void Adjustment(Int32 spellLevel, Int32 quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
