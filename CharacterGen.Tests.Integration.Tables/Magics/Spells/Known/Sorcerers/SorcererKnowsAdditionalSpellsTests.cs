using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Magics.Spells.Known.Sorcerers
{
    [TestFixture]
    public class SorcererKnowsAdditionalSpellsTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSKnowsAdditionalSpells, CharacterClassConstants.Sorcerer);
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 100, false)]
        public override void BooleanPercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}
