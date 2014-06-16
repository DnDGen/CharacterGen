using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralWizardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralWizardBaseRaces"; }
        }

        [Test]
        public void NeutralWizardGrayElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.GrayElf, 1);
        }

        [Test]
        public void NeutralWizardHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 2, 26);
        }

        [Test]
        public void NeutralWizardWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 27, 28);
        }

        [Test]
        public void NeutralWizardRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 29);
        }

        [Test]
        public void NeutralWizardHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 30, 44);
        }

        [Test]
        public void NeutralWizardLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 45, 47);
        }

        [Test]
        public void NeutralWizardTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 48, 49);
        }

        [Test]
        public void NeutralWizardHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 50);
        }

        [Test]
        public void NeutralWizardHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 51, 97);
        }

        [Test]
        public void NeutralWizardDoppelgangerPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralWizardEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}