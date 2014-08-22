using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilSorcererMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilSorcererMetaraces"; }
        }

        [TestCase(EmptyContent, 1, 95)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.Metaraces.Wererat, 96)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 97)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}