using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodSorcererMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodSorcererMetaraces"; }
        }

        [TestCase(EmptyContent, 1, 96)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 98, 99)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.Metaraces.HalfCelestial, 97)]
        [TestCase(RaceConstants.Metaraces.Werebear, 100)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}