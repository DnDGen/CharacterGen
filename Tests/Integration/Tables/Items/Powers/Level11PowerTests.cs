using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level11PowerTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 11); }
        }

        [TestCase(PowerConstants.Mundane, 1, 31)]
        [TestCase(PowerConstants.Minor, 32, 84)]
        [TestCase(PowerConstants.Medium, 85, 98)]
        [TestCase(PowerConstants.Major, 99, 100)]
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
