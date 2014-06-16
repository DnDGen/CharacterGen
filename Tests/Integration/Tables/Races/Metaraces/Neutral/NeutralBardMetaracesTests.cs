using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralBardMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralBardMetaraces"; }
        }

        [Test]
        public void NeutralBardEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 98);
        }

        [Test]
        public void NeutralBardWereboarPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralBardWeretigerPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}