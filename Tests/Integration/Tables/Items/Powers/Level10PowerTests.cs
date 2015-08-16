using CharacterGen.Tables;
using NUnit.Framework;
using System;
using TreasureGen.Common.Items;

namespace CharacterGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class Level10PowerTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.LevelXPower, 10); }
        }

        [TestCase(PowerConstants.Mundane, 1, 40)]
        [TestCase(PowerConstants.Minor, 41, 88)]
        [TestCase(PowerConstants.Medium, 89, 99)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(PowerConstants.Major, 100)]
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
