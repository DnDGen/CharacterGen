using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralSorcererMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralSorcererMetaraces"; }
        }

        [Test]
        public void Percentile()
        {
            AssertPercentile(EmptyContent, 1, 98);
        }

        [Test]
        public void NeutralSorcererWereboarPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralSorcererWeretigerPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}