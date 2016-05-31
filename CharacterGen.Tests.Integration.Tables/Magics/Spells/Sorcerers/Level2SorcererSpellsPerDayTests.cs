using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Sorcerers
{
    [TestFixture]
    public class Level2SorcererSpellsPerDayTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 2, CharacterClassConstants.Sorcerer);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 2).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 6)]
        [TestCase(1, 4)]
        public void Adjustment(Int32 spellLevel, Int32 quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
