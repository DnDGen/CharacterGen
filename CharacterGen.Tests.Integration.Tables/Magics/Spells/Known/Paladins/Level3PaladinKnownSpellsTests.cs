using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Paladins
{
    [TestFixture]
    public class Level3PaladinKnownSpellsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, 3, CharacterClassConstants.Paladin);
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
