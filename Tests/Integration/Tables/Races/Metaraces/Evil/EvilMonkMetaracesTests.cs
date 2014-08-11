using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilMonkMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilMonkMetaraces"; }
        }

        [TestCase(EmptyContent, 1, 96)]
        [TestCase(RaceConstants.Metaraces.Wererat, 97, 98)]
        public void Percentile(String content, Int32 lower, Int32 upper)
        {
            AssertPercentile(content, lower, upper);
        }

        [TestCase(RaceConstants.Metaraces.HalfFiend, 99)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 100)]
        public void Percentile(String content, Int32 roll)
        {
            AssertPercentile(content, roll);
        }
    }
}