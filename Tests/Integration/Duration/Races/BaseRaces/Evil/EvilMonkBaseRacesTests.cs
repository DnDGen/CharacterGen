using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilMonkBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilMonkBaseRaces";
        }

        [Test]
        public void EvilMonkHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 1, 10);
        }

        [Test]
        public void EvilMonkHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 11, 20);
        }

        [Test]
        public void EvilMonkHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 21, 90);
        }

        [Test]
        public void EvilMonkHobgoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Hobgoblin, 91, 93);
        }

        [Test]
        public void EvilMonkTieflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Tiefling, 94);
        }

        [Test]
        public void EvilMonkOgreMagePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.OgreMage, 95, 96);
        }

        [Test]
        public void EvilMonkEmptyPercentile()
        {
            AssertEmpty(97, 100);
        }
    }
}