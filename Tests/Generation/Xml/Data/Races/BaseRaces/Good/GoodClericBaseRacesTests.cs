using NPCGen.Core.Data.Races;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodClericBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodClericBaseRaces";
        }

        [Test]
        public void GoodClericAasimarPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodClericHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 2, 6);
        }

        [Test]
        public void GoodClericGrayElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.GrayElf, 7, 11);
        }

        [Test]
        public void GoodClericHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 12, 36);
        }

        [Test]
        public void GoodClericWildElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WildElf, 37);
        }

        [Test]
        public void GoodClericWoodElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WoodElf, 38);
        }

        [Test]
        public void GoodClericForestGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.ForestGnome, 39);
        }

        [Test]
        public void GoodClericRockGnomePercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.RockGnome, 40, 44);
        }

        [Test]
        public void GoodClericHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 45, 53);
        }

        [Test]
        public void GoodClericLightfootHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.LightfootHalfling, 54);
        }

        [Test]
        public void GoodClericDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 55);
        }

        [Test]
        public void GoodClericTallfellowHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.TallfellowHalfling, 56, 57);
        }

        [Test]
        public void GoodClericHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 58, 97);
        }

        [Test]
        public void GoodClericSvirfneblinPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Svirfneblin, 98);
        }

        [Test]
        public void GoodClericEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}