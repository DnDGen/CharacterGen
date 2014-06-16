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

        [Test]
        public void EvilSorcererEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 95);
        }

        [Test]
        public void EvilSorcererWereratPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wererat, 96);
        }

        [Test]
        public void EvilSorcererWerewolfPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werewolf, 97);
        }

        [Test]
        public void EvilSorcererHalfFiendPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfFiend, 98);
        }

        [Test]
        public void EvilSorcererHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 99, 100);
        }
    }
}