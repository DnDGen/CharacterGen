using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilMonkBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilMonkBaseRaces"; }
        }

        [Test]
        public void EvilMonkHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 1, 10);
        }

        [Test]
        public void EvilMonkHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 11, 20);
        }

        [Test]
        public void EvilMonkHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 21, 90);
        }

        [Test]
        public void EvilMonkHobgoblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Hobgoblin, 91, 93);
        }

        [Test]
        public void EvilMonkTieflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Tiefling, 94);
        }

        [Test]
        public void EvilMonkOgreMagePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.OgreMage, 95, 96);
        }

        [Test]
        public void EvilMonkEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 97, 100);
        }
    }
}