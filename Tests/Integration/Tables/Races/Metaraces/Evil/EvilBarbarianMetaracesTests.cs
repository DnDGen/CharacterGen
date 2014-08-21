using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilBarbarianMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilBarbarianMetaraces"; }
        }

        [TestCase(EmptyContent, 1, 94)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 95, 96)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 97, 98)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}