using CharacterGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level18PowerTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 18); }
        }

        [TestCase(1, 24, PowerConstants.Minor)]
        [TestCase(25, 80, PowerConstants.Medium)]
        [TestCase(81, 100, PowerConstants.Major)]
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
