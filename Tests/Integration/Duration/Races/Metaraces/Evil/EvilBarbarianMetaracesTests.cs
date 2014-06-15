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

        [Test]
        public void EvilBarbarianEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 94);
        }

        [Test]
        public void EvilBarbarianWerewolfPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werewolf, 95, 96);
        }

        [Test]
        public void EvilBarbarianHalfFiendPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfFiend, 97, 98);
        }

        [Test]
        public void EvilBarbarianHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 99, 100);
        }
    }
}