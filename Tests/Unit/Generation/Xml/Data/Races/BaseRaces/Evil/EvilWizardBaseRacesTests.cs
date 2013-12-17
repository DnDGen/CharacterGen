using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilWizardBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilWizardBaseRaces";
        }

        [Test]
        public void EvilWizardHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 1, 10);
        }

        [Test]
        public void EvilWizardWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 11);
        }

        [Test]
        public void EvilWizardHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 12, 26);
        }

        [Test]
        public void EvilWizardLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 27);
        }

        [Test]
        public void EvilWizardTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 28);
        }

        [Test]
        public void EvilWizardHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 29, 78);
        }

        [Test]
        public void EvilWizardHobgoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Hobgoblin, 79, 80);
        }

        [Test]
        public void EvilWizardTieflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Tiefling, 81);
        }

        [Test]
        public void EvilWizardDrowPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Drow, 82, 91);
        }

        [Test]
        public void EvilWizardGnollPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Gnoll, 92);
        }

        [Test]
        public void EvilWizardBugbearPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Bugbear, 93);
        }

        [Test]
        public void EvilWizardMindFlayerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MindFlayer, 94);
        }

        [Test]
        public void EvilWizardOgreMagePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.OgreMage, 95, 96);
        }

        [Test]
        public void EvilWizardEmptyPercentile()
        {
            AssertEmpty(97, 100);
        }
    }
}