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

        [Test]
        public void EvilRogueEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 1, 94);
        }

        [Test]
        public void EvilRogueWereratPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Wererat, 95, 96);
        }

        [Test]
        public void EvilRogueWerewolfPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.Werewolf, 97);
        }

        [Test]
        public void EvilRogueHalfFiendPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfFiend, 98, 99);
        }

        [Test]
        public void EvilRogueHalfDragonPercentile()
        {
            AssertPercentile(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}