using CharacterGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level20PowerTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 20); }
        }

        [TestCase(1, 25, PowerConstants.Minor)]
        [TestCase(26, 65, PowerConstants.Medium)]
        [TestCase(66, 100, PowerConstants.Major)]
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
