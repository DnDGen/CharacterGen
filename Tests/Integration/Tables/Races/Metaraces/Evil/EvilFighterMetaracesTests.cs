using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilFighterMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilFighterMetaraces"; }
        }

        [Test]
        public void EvilFighterEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 96);
        }

        [TestCase(RaceConstants.Metaraces.Wererat, 97)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 98)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 99)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 100)]
        public void Percentile(String content, Int32 roll)
        {
            AssertPercentile(content, roll);
        }
    }
}