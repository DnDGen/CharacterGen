using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralBarbarianMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralBarbarianMetaraces";
        }

        [Test]
        public void NeutralBarbarianEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void NeutralBarbarianWereboarPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralBarbarianWeretigerPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}