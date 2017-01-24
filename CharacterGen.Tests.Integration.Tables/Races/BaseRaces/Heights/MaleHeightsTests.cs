using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class MaleHeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Male"); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 62)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 66)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 71)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 204)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 45)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 32)]
        [TestCase(RaceConstants.BaseRaces.Derro, 45)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 66)]
        [TestCase(RaceConstants.BaseRaces.Drow, 53)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 45)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 132)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 36)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 168)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 66)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 32)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 53)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, 58)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 55)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 58)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 58)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 53)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 45)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, 114)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 50)]
        [TestCase(RaceConstants.BaseRaces.Human, 58)]
        [TestCase(RaceConstants.BaseRaces.Janni, 58)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 30)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 32)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 58)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 74)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 78)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 45)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 108)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 108)]
        [TestCase(RaceConstants.BaseRaces.Orc, 61)]
        [TestCase(RaceConstants.BaseRaces.Pixie, 18)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 58)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 36)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 70)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 70)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 132)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 240)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 36)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 48)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 62)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 53)]
        [TestCase(RaceConstants.BaseRaces.Troll, 96)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 53)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 53)]
        public void MaleHeight(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
