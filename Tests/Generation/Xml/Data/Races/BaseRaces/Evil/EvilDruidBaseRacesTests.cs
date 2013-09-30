using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilDruidBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilDruidBaseRaces";
        }

        [Test]
        public void EvilDruidWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 1, 2);
        }

        [Test]
        public void EvilDruidHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 3);
        }

        [Test]
        public void EvilDruidTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 4);
        }

        [Test]
        public void EvilDruidHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 5, 6);
        }

        [Test]
        public void EvilDruidHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 7, 56);
        }

        [Test]
        public void EvilDruidLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 57, 71);
        }

        [Test]
        public void EvilDruidGoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Goblin, 72);
        }

        [Test]
        public void EvilDruidHobgoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Hobgoblin, 73);
        }

        [Test]
        public void EvilDruidKoboldPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Kobold, 74);
        }

        [Test]
        public void EvilDruidOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Orc, 75);
        }

        [Test]
        public void EvilDruidGnollPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Gnoll, 76, 100);
        }
    }
}