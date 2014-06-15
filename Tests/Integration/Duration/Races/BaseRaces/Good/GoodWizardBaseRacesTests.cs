using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodWizardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodWizardBaseRaces"; }
        }

        [Test]
        public void GoodWizardAasimarPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodWizardHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 2);
        }

        [Test]
        public void GoodWizardGrayElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.GrayElf, 3, 7);
        }

        [Test]
        public void GoodWizardHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 8, 41);
        }

        [Test]
        public void GoodWizardWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 42);
        }

        [Test]
        public void GoodWizardForestGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.ForestGnome, 43);
        }

        [Test]
        public void GoodWizardRockGnomeDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 44, 48);
        }

        [Test]
        public void GoodWizardHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 49, 58);
        }

        [Test]
        public void GoodWizardLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 59, 63);
        }

        [Test]
        public void GoodWizardDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 64);
        }

        [Test]
        public void GoodWizardTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 65, 67);
        }

        [Test]
        public void GoodWizardHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 68);
        }

        [Test]
        public void GoodWizardHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 69, 96);
        }

        [Test]
        public void GoodWizardSvirfneblinPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Svirfneblin, 97);
        }

        [Test]
        public void GoodWizardEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 98, 100);
        }
    }
}