using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses.HasSpecialistFields
{
    [TestFixture]
    public class ClericHasSpecialistFieldsTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, CharacterClassConstants.Cleric);
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 100, true)]
        public override void BooleanPercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}
