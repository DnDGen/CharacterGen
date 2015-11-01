using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class LeadershipMovementTests : PercentileTests
    {
        protected override String tableName
        {
            get
            {
                return TableNameConstants.Set.Percentile.LeadershipMovement;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase("Moves around a lot", 1, 20)]
        [TestCase(EmptyContent, 21, 90)]
        [TestCase("Has a stronghold, base of operations, guildhouse, or the like", 91, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}
