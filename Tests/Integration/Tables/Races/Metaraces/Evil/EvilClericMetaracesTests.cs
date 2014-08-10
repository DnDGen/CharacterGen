using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilClericMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilClericMetaraces"; }
        }

        [TestCase(EmptyContent, 1, 95)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 98, 99)]
        public void Percentile(String content, Int32 lower, Int32 upper)
        {
            AssertPercentile(content, lower, upper);
        }

        [TestCase(RaceConstants.Metaraces.Wererat, 96)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 97)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 100)]
        public void Percentile(String content, Int32 roll)
        {
            AssertPercentile(content, roll);
        }
    }
}