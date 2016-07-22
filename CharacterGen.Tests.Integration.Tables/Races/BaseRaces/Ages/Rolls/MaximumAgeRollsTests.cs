using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Rolls
{
    [TestFixture]
    public class MaximumAgeRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.MaximumAgeRolls; }
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

        [TestCase(RaceConstants.BaseRaces.Aasimar, "2d20")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "2d10")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "2d100")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "5d20")]
        [TestCase(RaceConstants.BaseRaces.Derro, "2d100")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "2d20")]
        [TestCase(RaceConstants.BaseRaces.Drow, "4d100")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "2d100")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "3d100")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "2d10")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "1d20")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "4d100")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "3d20")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "2d10")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "4d100")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "2d100")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "2d10")]
        [TestCase(RaceConstants.BaseRaces.Human, "2d20")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "1d20")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "5d20")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "2d20")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "3d10")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "2d10")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "2d100")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "3d20")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "3d20")]
        [TestCase(RaceConstants.BaseRaces.Orc, "1d20")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "3d100")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "3d100")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "5d20")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "2d20")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "1d20")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "4d100")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "4d100")]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
