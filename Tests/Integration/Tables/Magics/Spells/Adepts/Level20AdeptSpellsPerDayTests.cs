using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Adepts
{
    [TestFixture]
    public class Level20AdeptSpellsPerDayTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 20, CharacterClassConstants.Adept);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 6).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 3)]
        [TestCase(1, 3)]
        [TestCase(2, 3)]
        [TestCase(3, 3)]
        [TestCase(4, 3)]
        [TestCase(5, 2)]
        public void Adjustment(int spellLevel, int quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
