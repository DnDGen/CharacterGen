using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Rolls
{
    [TestFixture]
    public class IntuitiveAgeRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.Intuitive); }
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

        [TestCase(RaceConstants.BaseRaces.Aasimar, "4d6")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "1d4")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "3d6")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Derro, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Drow, "4d6")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "1d4")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "4d6")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "1d6")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "1d4")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "4d6")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Human, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "1d4")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "1d3")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "1d4")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "2d6")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Orc, "1d4")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "4d6")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "4d6")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "4d6")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "1d4")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "4d6")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "4d6")]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
