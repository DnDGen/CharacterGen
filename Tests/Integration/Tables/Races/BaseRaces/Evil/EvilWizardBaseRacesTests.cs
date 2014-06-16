using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilWizardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilWizardBaseRaces"; }
        }

        [Test]
        public void EvilWizardHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 1, 10);
        }

        [Test]
        public void EvilWizardWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 11);
        }

        [Test]
        public void EvilWizardHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 12, 26);
        }

        [Test]
        public void EvilWizardLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 27);
        }

        [Test]
        public void EvilWizardTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 28);
        }

        [Test]
        public void EvilWizardHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 29, 78);
        }

        [Test]
        public void EvilWizardHobgoblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Hobgoblin, 79, 80);
        }

        [Test]
        public void EvilWizardTieflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Tiefling, 81);
        }

        [Test]
        public void EvilWizardDrowPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Drow, 82, 91);
        }

        [Test]
        public void EvilWizardGnollPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Gnoll, 92);
        }

        [Test]
        public void EvilWizardBugbearPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Bugbear, 93);
        }

        [Test]
        public void EvilWizardMindFlayerPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MindFlayer, 94);
        }

        [Test]
        public void EvilWizardOgreMagePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.OgreMage, 95, 96);
        }

        [Test]
        public void EvilWizardEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 97, 100);
        }
    }
}