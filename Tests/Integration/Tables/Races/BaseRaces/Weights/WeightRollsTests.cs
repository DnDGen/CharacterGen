using CharacterGen.Common.Races;
using CharacterGen.Tables;
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

        [TestCase(RaceConstants.BaseRaces.Aasimar, "2d4")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "1")]
        [TestCase(RaceConstants.BaseRaces.Derro, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Drow, "1d6")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "1")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "1")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "1d6")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "2d6")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "1d6")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Human, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "1")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "1")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "2d6")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "4d6")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "4d6")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "4d6")]
        [TestCase(RaceConstants.BaseRaces.Orc, "2d6")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "1")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "1")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "1")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "2d4")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "1d6")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "1d6")]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
