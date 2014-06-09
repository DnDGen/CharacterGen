using System;
using NPCGen.Common.Alignments;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Alignments
{
    [TestFixture]
    public class AlignmentGoodnessTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "AlignmentGoodness"; }
        }

        [TestCase(AlignmentConstants.Good, 1, 20)]
        [TestCase(AlignmentConstants.Neutral, 21, 50)]
        [TestCase(AlignmentConstants.Evil, 51, 100)]
        public void Percentile(String content, Int32 lower, Int32 upper)
        {
            AssertPercentile(content, lower, upper);
        }
    }
}