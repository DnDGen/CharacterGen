using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.PerDay.Paladins
{
    [TestFixture]
    public class Level2PaladinSpellsPerDayTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 2, CharacterClassConstants.Paladin);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Empty<String>();
            AssertCollectionNames(names);
        }
    }
}
