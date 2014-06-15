using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilRangerMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilRangerMetaraces"; }
        }

        [Test]
        public void EvilRangerEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 95);
        }

        [Test]
        public void EvilRangerWereratPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wererat, 96);
        }

        [Test]
        public void EvilRangerWerewolfPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werewolf, 97, 98);
        }

        [Test]
        public void EvilRangerHalfFiendPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfFiend, 99);
        }

        [Test]
        public void EvilRangerHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}