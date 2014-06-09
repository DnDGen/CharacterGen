using NPCGen.Core.Data.Races;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Good
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
            AssertContent(RaceConstants.BaseRaces.Aasimar, 1, 2);
        }

        [Test]
        public void GoodMonkHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 3);
        }

        [Test]
        public void GoodMonkHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 4, 13);
        }

        [Test]
        public void GoodMonkHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 14, 18);
        }

        [Test]
        public void GoodMonkLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 19);
        }

        [Test]
        public void GoodMonkDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 20);
        }

        [Test]
        public void GoodMonkHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 21, 25);
        }

        [Test]
        public void GoodMonkHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 26, 97);
        }

        [Test]
        public void GoodMonkEmptyPercentile()
        {
            AssertEmpty(98, 100);
        }
    }
}