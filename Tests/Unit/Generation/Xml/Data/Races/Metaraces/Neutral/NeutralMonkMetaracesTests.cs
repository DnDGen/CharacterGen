using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralMonkMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralMonkMetaraces";
        }

        [Test]
        public void NeutralMonkEmptyPercentile()
        {
            AssertEmpty(1, 100);
        }
    }
}