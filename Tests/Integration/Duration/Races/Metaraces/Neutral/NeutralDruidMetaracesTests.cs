using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralDruidMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralDruidMetaraces";
        }

        [Test]
        public void NeutralDruidEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void NeutralDruidWereboarPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralDruidWeretigerPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}