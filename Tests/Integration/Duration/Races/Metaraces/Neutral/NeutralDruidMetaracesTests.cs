using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralDruidMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralDruidMetaraces"; }
        }

        [Test]
        public void NeutralDruidEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 98);
        }

        [Test]
        public void NeutralDruidWereboarPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralDruidWeretigerPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}