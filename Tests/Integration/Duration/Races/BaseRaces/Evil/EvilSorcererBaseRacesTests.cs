using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilSorcererBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilSorcererBaseRaces";
        }

        [Test]
        public void EvilSorcererWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 1);
        }

        [Test]
        public void EvilSorcererHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 2, 16);
        }

        [Test]
        public void EvilSorcererLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 17, 21);
        }

        [Test]
        public void EvilSorcererDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 22);
        }

        [Test]
        public void EvilSorcererTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 23);
        }

        [Test]
        public void EvilSorcererHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 24, 28);
        }

        [Test]
        public void EvilSorcererHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 29, 68);
        }

        [Test]
        public void EvilSorcererLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 69);
        }

        [Test]
        public void EvilSorcererGoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Goblin, 70);
        }

        [Test]
        public void EvilSorcererHobgoblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Hobgoblin, 71);
        }

        [Test]
        public void EvilSorcererKoboldPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Kobold, 72, 86);
        }

        [Test]
        public void EvilSorcererGnollPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Gnoll, 87);
        }

        [Test]
        public void EvilSorcererTroglodytePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Troglodyte, 88, 90);
        }

        [Test]
        public void EvilSorcererBugbearPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Bugbear, 91);
        }

        [Test]
        public void EvilSorcererOgrePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Ogre, 92);
        }

        [Test]
        public void EvilSorcererMinotaurPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Minotaur, 93);
        }

        [Test]
        public void EvilSorcererMindFlayerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MindFlayer, 94);
        }

        [Test]
        public void EvilSorcererOgreMagePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.OgreMage, 95);
        }

        [Test]
        public void EvilSorcererEmptyPercentile()
        {
            AssertEmpty(96, 100);
        }
    }
}