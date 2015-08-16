using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level9PowerTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 9); }
        }

        [TestCase(PowerConstants.Mundane, 1, 43)]
        [TestCase(PowerConstants.Minor, 44, 91)]
        [TestCase(PowerConstants.Medium, 92, 100)]
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
