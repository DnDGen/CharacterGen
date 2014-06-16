using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralWizardMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralWizardMetaraces"; }
        }

        [TestCase(EmptyContent, 1, 98)]
        public void Percentile(String content, Int32 lower, Int32 upper)
        {
            AssertPercentile(content, lower, upper);
        }

        [TestCase(RaceConstants.Metaraces.Wereboar, 99)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 100)]
        public void Percentile(String content, Int32 roll)
        {
            AssertPercentile(content, roll);
        }
    }
}