using CharacterGen.Common.Races;
using CharacterGen.Tables;
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

        [TestCase(RaceConstants.BaseRaces.Aasimar, "2d8")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "2d6")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Derro, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Drow, "2d6")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "2d10")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "2d4")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "2d8")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "2d12")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "2d8")]
        [TestCase(RaceConstants.BaseRaces.Human, "2d10")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "2d4")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "2d10")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "2d8")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "2d6")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "2d6")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Orc, "2d12")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "2d4")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "2d8")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "2d6")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "2d6")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "2d6")]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
