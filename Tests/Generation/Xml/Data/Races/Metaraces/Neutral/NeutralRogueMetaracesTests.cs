using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralRogueMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralRogueMetaraces";
        }

        [Test]
        public void NeutralRogueEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void NeutralRogueWereboarPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralRogueWeretigerPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}