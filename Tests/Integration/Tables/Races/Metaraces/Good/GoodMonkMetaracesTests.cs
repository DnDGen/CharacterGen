using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodMonkMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodMonkMetaraces"; }
        }

        [TestCase(EmptyContent, 1, 97)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.Metaraces.HalfCelestial, 98)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 99)]
        [TestCase(RaceConstants.Metaraces.Werebear, 100)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}