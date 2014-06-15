using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralClericBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralClericBaseRaces"; }
        }

        [Test]
        public void NeutralClericDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 1, 15);
        }

        [Test]
        public void NeutralClericHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 16, 25);
        }

        [Test]
        public void NeutralClericMountainDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.MountainDwarf, 26);
        }

        [Test]
        public void NeutralClericHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 27);
        }

        [Test]
        public void NeutralClericWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 28);
        }

        [Test]
        public void NeutralClericWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 29, 38);
        }

        [Test]
        public void NeutralClericRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 39);
        }

        [Test]
        public void NeutralClericHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 40, 48);
        }

        [Test]
        public void NeutralClericLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 49, 58);
        }

        [Test]
        public void NeutralClericDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 59);
        }

        [Test]
        public void NeutralClericTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 60);
        }

        [Test]
        public void NeutralClericHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 61, 62);
        }

        [Test]
        public void NeutralClericHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 63, 90);
        }

        [Test]
        public void NeutralClericLizardfolkPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Lizardfolk, 91, 97);
        }

        [Test]
        public void NeutralClericDoppelgangerPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralClericEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}