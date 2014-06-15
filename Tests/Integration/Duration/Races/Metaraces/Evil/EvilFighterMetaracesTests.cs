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

        [Test]
        public void EvilFighterWereratPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wererat, 97);
        }

        [Test]
        public void EvilFighterWerewolfPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werewolf, 98);
        }

        [Test]
        public void EvilFighterHalfFiendPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfFiend, 99);
        }

        [Test]
        public void EvilFighterHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}