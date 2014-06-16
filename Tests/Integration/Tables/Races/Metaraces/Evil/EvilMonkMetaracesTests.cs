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

        [Test]
        public void EvilMonkEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 96);
        }

        [Test]
        public void EvilMonkWereratPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wererat, 97, 98);
        }

        [Test]
        public void EvilMonkHalfFiendPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfFiend, 99);
        }

        [Test]
        public void EvilMonkHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}