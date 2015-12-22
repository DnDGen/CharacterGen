using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses.HasSpecialistFields
{
    [TestFixture]
    public class CommonerHasSpecialistFieldsTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, CharacterClassConstants.Commoner);
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(false, 1, 100)]
        public override void BooleanPercentile(bool isTrue, int lower, int upper)
        {
            base.BooleanPercentile(isTrue, lower, upper);
        }
    }
}
