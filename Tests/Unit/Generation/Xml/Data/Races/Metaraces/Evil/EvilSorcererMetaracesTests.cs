using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilSorcererMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilSorcererMetaraces";
        }

        [Test]
        public void EvilSorcererEmptyPercentile()
        {
            AssertEmpty(1, 95);
        }

        [Test]
        public void EvilSorcererWereratPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wererat, 96);
        }

        [Test]
        public void EvilSorcererWerewolfPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werewolf, 97);
        }

        [Test]
        public void EvilSorcererHalfFiendPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfFiend, 98);
        }

        [Test]
        public void EvilSorcererHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 99, 100);
        }
    }
}