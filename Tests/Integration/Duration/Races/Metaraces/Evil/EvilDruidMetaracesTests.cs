using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilDruidMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilDruidMetaraces";
        }

        [Test]
        public void EvilDruidEmptyPercentile()
        {
            AssertEmpty(1, 100);
        }
    }
}