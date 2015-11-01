using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class KilledFollowersTests : BooleanPercentileTests
    {
        protected override String tableName
        {
            get
            {
                return TableNameConstants.Set.TrueOrFalse.KilledFollowers;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(true, 1, 20)]
        [TestCase(false, 21, 100)]
        public override void BooleanPercentile(Boolean isTrue, Int32 lower, Int32 upper)
        {
            base.BooleanPercentile(isTrue, lower, upper);
        }
    }
}
