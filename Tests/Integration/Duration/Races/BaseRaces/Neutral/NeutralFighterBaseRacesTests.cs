using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralFighterBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralFighterBaseRaces"; }
        }

        [Test]
        public void NeutralFighterDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 1, 10);
        }

        [Test]
        public void NeutralFighterHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 11, 29);
        }

        [Test]
        public void NeutralFighterMountainDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MountainDwarf, 30, 34);
        }

        [Test]
        public void NeutralFighterHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 35);
        }

        [Test]
        public void NeutralFighterWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 36, 41);
        }

        [Test]
        public void NeutralFighterHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 42, 46);
        }

        [Test]
        public void NeutralFighterLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 47);
        }

        [Test]
        public void NeutralFighterDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 48);
        }

        [Test]
        public void NeutralFighterHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 49, 58);
        }

        [Test]
        public void NeutralFighterHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 59, 96);
        }

        [Test]
        public void NeutralFighterLizardfolkPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Lizardfolk, 97);
        }

        [Test]
        public void NeutralFighterDoppelgangerPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralFighterEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}