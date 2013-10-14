using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralFighterMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralFighterMetaraces";
        }

        [Test]
        public void NeutralFighterEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void NeutralFighterWereboarPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralFighterWeretigerPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}