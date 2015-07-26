using System;
using CharacterGen.Common.Alignments;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Alignments
{
    [TestFixture]
    public class AlignmentGoodnessTests : PercentileTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Percentile.AlignmentGoodness; }
        }

        [TestCase(AlignmentConstants.Good, 1, 20)]
        [TestCase(AlignmentConstants.Neutral, 21, 50)]
        [TestCase(AlignmentConstants.Evil, 51, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }
    }
}