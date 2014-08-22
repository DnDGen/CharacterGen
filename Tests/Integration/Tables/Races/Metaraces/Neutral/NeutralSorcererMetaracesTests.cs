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

        [TestCase(EmptyContent, 1, 98)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.Metaraces.Wereboar, 99)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 100)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}