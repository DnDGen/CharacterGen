using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilSorcererBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilSorcererBaseRaces"; }
        }

        [Test]
        public void EvilSorcererWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 1);
        }

        [Test]
        public void EvilSorcererHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 2, 16);
        }

        [Test]
        public void EvilSorcererLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 17, 21);
        }

        [Test]
        public void EvilSorcererDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 22);
        }

        [Test]
        public void EvilSorcererTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 23);
        }

        [Test]
        public void EvilSorcererHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 24, 28);
        }

        [Test]
        public void EvilSorcererHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 29, 68);
        }

        [Test]
        public void EvilSorcererLizardfolkPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Lizardfolk, 69);
        }

        [Test]
        public void EvilSorcererGoblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Goblin, 70);
        }

        [Test]
        public void EvilSorcererHobgoblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Hobgoblin, 71);
        }

        [Test]
        public void EvilSorcererKoboldPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Kobold, 72, 86);
        }

        [Test]
        public void EvilSorcererGnollPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Gnoll, 87);
        }

        [Test]
        public void EvilSorcererTroglodytePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Troglodyte, 88, 90);
        }

        [Test]
        public void EvilSorcererBugbearPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Bugbear, 91);
        }

        [Test]
        public void EvilSorcererOgrePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Ogre, 92);
        }

        [Test]
        public void EvilSorcererMinotaurPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Minotaur, 93);
        }

        [Test]
        public void EvilSorcererMindFlayerPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MindFlayer, 94);
        }

        [Test]
        public void EvilSorcererOgreMagePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.OgreMage, 95);
        }

        [Test]
        public void EvilSorcererEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 96, 100);
        }
    }
}