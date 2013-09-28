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
    public class GoodMonkBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodMonkBaseRaces";
        }

        [Test]
        public void GoodMonkAasimarPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Aasimar, 1, 2);
        }

        [Test]
        public void GoodMonkHillDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HillDwarf, 3);
        }

        [Test]
        public void GoodMonkHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 4, 13);
        }

        [Test]
        public void GoodMonkHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 14, 18);
        }

        [Test]
        public void GoodMonkLightfootHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.LightfootHalfling, 19);
        }

        [Test]
        public void GoodMonkDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 20);
        }

        [Test]
        public void GoodMonkHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 21, 25);
        }

        [Test]
        public void GoodMonkHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 26, 97);
        }

        [Test]
        public void GoodMonkEmptyPercentile()
        {
            AssertEmpty(98, 100);
        }
    }
}