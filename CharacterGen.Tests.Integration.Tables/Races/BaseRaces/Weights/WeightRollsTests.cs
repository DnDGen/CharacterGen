using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Weights
{
    [TestFixture]
    public class WeightRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.WeightRolls; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Centaur, "2d4")]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, "15d6")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "1")]
        [TestCase(RaceConstants.BaseRaces.Derro, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Drow, "1d6")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.FireGiant, "15d6")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "1")]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, "15d6")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "1")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Grimlock, "1d6")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Harpy, "2d6")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "1d6")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.HillGiant, "15d6")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Human, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Janni, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "1")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "1")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "2d6")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "4d6")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "4d6")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "4d6")]
        [TestCase(RaceConstants.BaseRaces.Orc, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Pixie, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, "2d6")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "1")]
        [TestCase(RaceConstants.BaseRaces.Satyr, "8d8")]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, "3d4")]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, "15d6")]
        [TestCase(RaceConstants.BaseRaces.StormGiant, "15d6")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "1")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "1")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Troll, "2d4")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "1d6")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "1d6")]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
