using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodSorcererBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "GoodSorcererBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 4, 5)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 7, 8)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 9, 11)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 12, 36)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 39, 40)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 41, 45)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 46, 54)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 57, 58)]
        [TestCase(RaceConstants.BaseRaces.Human, 59, 95)]
        [TestCase(EmptyContent, 97, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 3)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 6)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 37)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 38)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 55)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 56)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 96)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}