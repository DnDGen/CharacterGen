using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilFighterMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilFighterMetaraces";
        }

        [Test]
        public void EvilFighterEmptyPercentile()
        {
            AssertEmpty(1, 96);
        }

        [Test]
        public void EvilFighterWereratPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wererat, 97);
        }

        [Test]
        public void EvilFighterWerewolfPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werewolf, 98);
        }

        [Test]
        public void EvilFighterHalfFiendPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfFiend, 99);
        }

        [Test]
        public void EvilFighterHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}