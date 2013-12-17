using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilBarbarianMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilBarbarianMetaraces";
        }

        [Test]
        public void EvilBarbarianEmptyPercentile()
        {
            AssertEmpty(1, 94);
        }

        [Test]
        public void EvilBarbarianWerewolfPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werewolf, 95, 96);
        }

        [Test]
        public void EvilBarbarianHalfFiendPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfFiend, 97, 98);
        }

        [Test]
        public void EvilBarbarianHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 99, 100);
        }
    }
}