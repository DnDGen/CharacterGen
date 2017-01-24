using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class HeightRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.HeightRolls; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, "2d8")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Centaur, "3d4")]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, "2d12")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Derro, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Drow, "2d6")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.FireGiant, "2d12")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "2d4")]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, "2d12")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "2d10")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "2d4")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Grimlock, "2d4")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "2d8")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "2d12")]
        [TestCase(RaceConstants.BaseRaces.Harpy, "2d6")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.HillGiant, "2d12")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "2d8")]
        [TestCase(RaceConstants.BaseRaces.Human, "2d10")]
        [TestCase(RaceConstants.BaseRaces.Janni, "2d10")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "2d4")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "2d10")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "2d8")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "2d6")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "2d6")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Orc, "2d12")]
        [TestCase(RaceConstants.BaseRaces.Pixie, "3d8")]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, "2d10")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Satyr, "1d10")]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, "3d10")]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, "2d12")]
        [TestCase(RaceConstants.BaseRaces.StormGiant, "2d12")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "2d4")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "2d8")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Troll, "2d10")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "2d6")]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
