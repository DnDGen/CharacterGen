using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class ReputationTests : PercentileTests
    {
        protected override String tableName
        {
            get
            {
                return TableNameConstants.Set.Percentile.Reputation;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase("Cruelty", 1, 5)]
        [TestCase("Aloofness", 6, 15)]
        [TestCase("Failure", 16, 25)]
        [TestCase(EmptyContent, 26, 75)]
        [TestCase("Special power", 76, 85)]
        [TestCase("Fairness and generosity", 86, 95)]
        [TestCase("Great renown", 96, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}
