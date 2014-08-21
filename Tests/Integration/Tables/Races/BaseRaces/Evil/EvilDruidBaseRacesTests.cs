using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilDruidBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilDruidBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.WoodElf, 1, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 5, 6)]
        [TestCase(RaceConstants.BaseRaces.Human, 7, 56)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 57, 71)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 76, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HalfElf, 3)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 4)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 72)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 73)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 74)]
        [TestCase(RaceConstants.BaseRaces.Orc, 75)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}