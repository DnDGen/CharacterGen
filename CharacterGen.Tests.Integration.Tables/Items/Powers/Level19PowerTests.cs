using CharacterGen.Domain.Tables;
using NUnit.Framework;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level19PowerTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 19); }
        }

        [TestCase(1, 4, PowerConstants.Minor)]
        [TestCase(5, 70, PowerConstants.Medium)]
        [TestCase(71, 100, PowerConstants.Major)]
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
