using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Evil
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