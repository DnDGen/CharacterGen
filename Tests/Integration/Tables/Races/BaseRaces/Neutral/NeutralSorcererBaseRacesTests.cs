using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralSorcererBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralSorcererBaseRaces"; }
        }

        [Test]
        public void NeutralSorcererHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 1);
        }

        [Test]
        public void NeutralSorcererHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 2);
        }

        [Test]
        public void NeutralSorcererWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 3, 12);
        }

        [Test]
        public void NeutralSorcererWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 13, 15);
        }

        [Test]
        public void NeutralSorcererRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 16);
        }

        [Test]
        public void NeutralSorcererHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 17, 31);
        }

        [Test]
        public void NeutralSorcererLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 32, 41);
        }

        [Test]
        public void NeutralSorcererDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 42);
        }

        [Test]
        public void NeutralSorcererTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 43);
        }

        [Test]
        public void NeutralSorcererHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 44, 48);
        }

        [Test]
        public void NeutralSorcererHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 49, 95);
        }

        [Test]
        public void NeutralSorcererLizardfolkPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Lizardfolk, 96, 97);
        }

        [Test]
        public void NeutralSorcererDoppelgangerPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralSorcererEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}