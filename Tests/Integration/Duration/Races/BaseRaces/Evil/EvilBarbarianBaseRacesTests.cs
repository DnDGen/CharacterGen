using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBarbarianBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return "EvilBarbarianBaseRaces"; }
        }

        [TestCase(RaceConstants.BaseRaces.WildElf, 1)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 4)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 5)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 6)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 45)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 46)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 47)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 78)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 84)]
        public void Percentile(String content, Int32 roll)
        {
            AssertPercentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.WoodElf, 2, 3)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 7, 29)]
        [TestCase(RaceConstants.BaseRaces.Human, 30, 39)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 40, 44)]
        [TestCase(RaceConstants.BaseRaces.Orc, 48, 77)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 79, 83)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 85, 86)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 87, 90)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 91, 94)]
        [TestCase(EmptyContent, 95, 100)]
        public void Percentile(String content, Int32 lower, Int32 upper)
        {
            AssertPercentile(content, lower, upper);
        }
    }
}