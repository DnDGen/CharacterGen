using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRogueBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralRogueBaseRaces"; }
        }

        [Test]
        public void NeutralRogueDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void NeutralRogueHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 2, 4);
        }

        [Test]
        public void NeutralRogueHighElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HighElf, 5, 8);
        }

        [Test]
        public void NeutralRogueWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 9);
        }

        [Test]
        public void NeutralRogueRockGnomePercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.RockGnome, 10);
        }

        [Test]
        public void NeutralRogueHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 11, 25);
        }

        [Test]
        public void NeutralRogueLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 26, 53);
        }

        [Test]
        public void NeutralRogueDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 54, 58);
        }

        [Test]
        public void NeutralRogueTallfellowHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.TallfellowHalfling, 59, 63);
        }

        [Test]
        public void NeutralRogueHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 64, 73);
        }

        [Test]
        public void NeutralRogueHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 74, 97);
        }

        [Test]
        public void NeutralRogueDoppelgangerPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralRogueEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}