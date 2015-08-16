using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level6PowerTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 6); }
        }

        [TestCase(PowerConstants.Mundane, 1, 59)]
        [TestCase(PowerConstants.Minor, 60, 99)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(PowerConstants.Medium, 100)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }
    }
}
