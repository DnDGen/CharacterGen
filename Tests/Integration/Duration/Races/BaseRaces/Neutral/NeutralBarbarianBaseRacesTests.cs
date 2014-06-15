using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBarbarianBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "NeutralBarbarianBaseRaces"; }
        }

        [Test]
        public void NeutralBarbarianDeepDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void NeutralBarbarianHillDwarfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HillDwarf, 2);
        }

        [Test]
        public void NeutralBarbarianWildElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WildElf, 3, 13);
        }

        [Test]
        public void NeutralBarbarianWoodElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.WoodElf, 14);
        }

        [Test]
        public void NeutralBarbarianHalfElfPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfElf, 15, 16);
        }

        [Test]
        public void NeutralBarbarianLightfootHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.LightfootHalfling, 17, 18);
        }

        [Test]
        public void NeutralBarbarianDeepHalflingPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.DeepHalfling, 19);
        }

        [Test]
        public void NeutralBarbarianHalfOrcPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.HalfOrc, 20, 58);
        }

        [Test]
        public void NeutralBarbarianHumanPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Human, 59, 87);
        }

        [Test]
        public void NeutralBarbarianLizardfolkPercentile()
        {
            AssertPercentile(RaceConstants.BaseRaces.Lizardfolk, 88, 98);
        }

        [Test]
        public void NeutralBarbarianEmptyPercentile()
        {
            AssertPercentile(EmptyContent, 99, 100);
        }
    }
}