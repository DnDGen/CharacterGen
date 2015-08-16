using CharacterGen.Common.Alignments;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Alignments
{
    [TestFixture]
    public class AlignmentLawfulnessTests : PercentileTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Percentile.AlignmentLawfulness; }
        }

        [TestCase(AlignmentConstants.Lawful, 1, 33)]
        [TestCase(AlignmentConstants.Neutral, 34, 67)]
        [TestCase(AlignmentConstants.Chaotic, 68, 100)]
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
