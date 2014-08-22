using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilRogueMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilRogueMetaraces"; }
        }

        [TestCase(EmptyContent, 1, 94)]
        [TestCase(RaceConstants.Metaraces.Wererat, 95, 96)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 98, 99)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.Metaraces.Werewolf, 97)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 100)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}