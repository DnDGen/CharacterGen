using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Rolls
{
    [TestFixture]
    public class SelfTaughtAgeRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.SelfTaught); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, "6d6")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Centaur, "2d6")]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, "3d20")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "5d6")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Derro, "5d6")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Drow, "6d6")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "5d6")]
        [TestCase(RaceConstants.BaseRaces.FireGiant, "9d6")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "6d6")]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, "6d6")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "1d6")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "6d6")]
        [TestCase(RaceConstants.BaseRaces.Grimlock, "1d6")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Harpy, "1d8")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "6d6")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "5d6")]
        [TestCase(RaceConstants.BaseRaces.HillGiant, "5d6")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Human, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Janni, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "1d6")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "1d6")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "4d6")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "1d6")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "5d6")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "3d6")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Orc, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Pixie, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, "1d6")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "6d6")]
        [TestCase(RaceConstants.BaseRaces.Satyr, "2d8")]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, "1d6")]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, "6d20")]
        [TestCase(RaceConstants.BaseRaces.StormGiant, "9d10")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "6d6")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "6d6")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Troll, "1d6")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "6d6")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "6d6")]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
