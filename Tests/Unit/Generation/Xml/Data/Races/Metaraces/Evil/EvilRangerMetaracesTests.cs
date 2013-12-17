using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilRangerMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilRangerMetaraces";
        }

        [Test]
        public void EvilRangerEmptyPercentile()
        {
            AssertEmpty(1, 95);
        }

        [Test]
        public void EvilRangerWereratPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wererat, 96);
        }

        [Test]
        public void EvilRangerWerewolfPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werewolf, 97, 98);
        }

        [Test]
        public void EvilRangerHalfFiendPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfFiend, 99);
        }

        [Test]
        public void EvilRangerHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}