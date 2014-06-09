using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralRangerMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralRangerMetaraces";
        }

        [Test]
        public void NeutralRangerEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void NeutralRangerWereboarPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralRangerWeretigerPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}