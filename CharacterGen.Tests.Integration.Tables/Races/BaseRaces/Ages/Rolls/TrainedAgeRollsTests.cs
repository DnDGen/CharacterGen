using CharacterGen.Races;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Rolls
{
    [TestFixture]
    public class TrainedAgeRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, GroupConstants.Trained); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                RaceConstants.BaseRaces.Aasimar,
                RaceConstants.BaseRaces.Bugbear,
                RaceConstants.BaseRaces.DeepDwarf,
                RaceConstants.BaseRaces.DeepHalfling,
                RaceConstants.BaseRaces.Derro,
                RaceConstants.BaseRaces.Doppelganger,
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.DuergarDwarf,
                RaceConstants.BaseRaces.ForestGnome,
                RaceConstants.BaseRaces.Gnoll,
                RaceConstants.BaseRaces.Goblin,
                RaceConstants.BaseRaces.GrayElf,
                RaceConstants.BaseRaces.HalfElf,
                RaceConstants.BaseRaces.HalfOrc,
                RaceConstants.BaseRaces.HighElf,
                RaceConstants.BaseRaces.HillDwarf,
                RaceConstants.BaseRaces.Hobgoblin,
                RaceConstants.BaseRaces.Human,
                RaceConstants.BaseRaces.Kobold,
                RaceConstants.BaseRaces.LightfootHalfling,
                RaceConstants.BaseRaces.Lizardfolk,
                RaceConstants.BaseRaces.MindFlayer,
                RaceConstants.BaseRaces.Minotaur,
                RaceConstants.BaseRaces.MountainDwarf,
                RaceConstants.BaseRaces.Ogre,
                RaceConstants.BaseRaces.OgreMage,
                RaceConstants.BaseRaces.Orc,
                RaceConstants.BaseRaces.RockGnome,
                RaceConstants.BaseRaces.Svirfneblin,
                RaceConstants.BaseRaces.TallfellowHalfling,
                RaceConstants.BaseRaces.Tiefling,
                RaceConstants.BaseRaces.Troglodyte,
                RaceConstants.BaseRaces.WildElf,
                RaceConstants.BaseRaces.WoodElf
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, "8d6")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "2d6")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "7d6")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "4d6")]
        [TestCase(RaceConstants.BaseRaces.Derro, "7d6")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Drow, "10d6")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "7d6")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "9d6")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "2d6")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "10d6")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "3d6")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "2d6")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "10d6")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "7d6")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Human, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "2d6")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "1d10")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "5d8")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "2d6")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "7d6")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "4d6")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "4d6")]
        [TestCase(RaceConstants.BaseRaces.Orc, "2d6")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "9d6")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "9d6")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "8d6")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "2d6")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "10d6")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "10d6")]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
