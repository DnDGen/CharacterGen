using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralBardMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralBardMetaraces";
        }

        [Test]
        public void NeutralBardEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void NeutralBardWereboarPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralBardWeretigerPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}