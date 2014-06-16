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

        [Test]
        public void EvilClericEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 95);
        }

        [Test]
        public void EvilClericWereratPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wererat, 96);
        }

        [Test]
        public void EvilClericWerewolfPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werewolf, 97);
        }

        [Test]
        public void EvilClericHalfFiendPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfFiend, 98, 99);
        }

        [Test]
        public void EvilClericHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}