using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralRogueMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralRogueMetaraces"; }
        }

        [Test]
        public void NeutralRogueEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 98);
        }

        [Test]
        public void NeutralRogueWereboarPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralRogueWeretigerPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}