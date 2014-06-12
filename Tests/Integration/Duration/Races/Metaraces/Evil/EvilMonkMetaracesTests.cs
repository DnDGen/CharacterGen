using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilMonkMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilMonkMetaraces";
        }

        [Test]
        public void EvilMonkEmptyPercentile()
        {
            AssertEmpty(1, 96);
        }

        [Test]
        public void EvilMonkWereratPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wererat, 97, 98);
        }

        [Test]
        public void EvilMonkHalfFiendPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfFiend, 99);
        }

        [Test]
        public void EvilMonkHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}