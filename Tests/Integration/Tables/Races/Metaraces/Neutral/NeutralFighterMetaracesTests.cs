using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralFighterMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralFighterMetaraces"; }
        }

        [Test]
        public void NeutralFighterEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 98);
        }

        [Test]
        public void NeutralFighterWereboarPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wereboar, 99);
        }

        [Test]
        public void NeutralFighterWeretigerPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Weretiger, 100);
        }
    }
}