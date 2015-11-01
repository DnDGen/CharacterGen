using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class AttractCohortOfDifferentAlignmentTests : BooleanPercentileTests
    {
        protected override String tableName
        {
            get
            {
                return TableNameConstants.Set.TrueOrFalse.AttractCohortOfDifferentAlignment;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(true, 1, 50)]
        [TestCase(false, 51, 100)]
        public override void BooleanPercentile(Boolean isTrue, Int32 lower, Int32 upper)
        {
            base.BooleanPercentile(isTrue, lower, upper);
        }
    }
}
