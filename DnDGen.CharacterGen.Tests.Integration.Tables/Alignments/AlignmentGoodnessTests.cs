using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Alignments
{
    [TestFixture]
    public class AlignmentGoodnessTests : PercentileTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Percentile.AlignmentGoodness; }
        }

        [TestCase(1, 20, AlignmentConstants.Good)]
        [TestCase(21, 50, AlignmentConstants.Neutral)]
        [TestCase(51, 100, AlignmentConstants.Evil)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }
    }
}