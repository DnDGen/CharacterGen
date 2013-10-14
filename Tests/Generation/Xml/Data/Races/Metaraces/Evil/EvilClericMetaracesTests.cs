using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilClericMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilClericMetaraces";
        }

        [Test]
        public void EvilClericEmptyPercentile()
        {
            AssertEmpty(1, 95);
        }

        [Test]
        public void EvilClericWereratPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wererat, 96);
        }

        [Test]
        public void EvilClericWerewolfPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werewolf, 97);
        }

        [Test]
        public void EvilClericHalfFiendPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfFiend, 98, 99);
        }

        [Test]
        public void EvilClericHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}