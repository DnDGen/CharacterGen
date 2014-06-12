using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralClericMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralClericMetaraces";
        }

        [Test]
        public void NeutralClericEmptyPercentile()
        {
            AssertEmpty(1, 98);
        }

        [Test]
        public void NeutralClericWereboarPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralClericWeretigerPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}