using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralSorcererMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralSorcererMetaraces";
        }

        [Test]
        public void NeutralSorcererEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void NeutralSorcererWereboarPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralSorcererWeretigerPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}