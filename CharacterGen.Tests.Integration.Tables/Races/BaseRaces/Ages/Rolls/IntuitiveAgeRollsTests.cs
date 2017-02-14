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
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Azer, "3d6")]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Bugbear, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Centaur, "1d6")]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, "2d4")]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, "2d4")]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, "3d6")]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Derro, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Drow, "4d6")]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, "3d6")]
        [TestCase(RaceConstants.BaseRaces.FireGiant, "2d4")]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, "4d6")]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Gnoll, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Goblin, "1d4")]
        [TestCase(RaceConstants.BaseRaces.GrayElf, "4d6")]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, "2d4")]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Grimlock, "1d4")]
        [TestCase(RaceConstants.BaseRaces.HalfElf, "1d6")]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Harpy, "1d3")]
        [TestCase(RaceConstants.BaseRaces.HighElf, "4d6")]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, "3d6")]
        [TestCase(RaceConstants.BaseRaces.HillGiant, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, "1d4")]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Human, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Janni, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Kobold, "1d4")]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, "1d3")]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Minotaur, "1d4")]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, "3d6")]
        [TestCase(RaceConstants.BaseRaces.Ogre, "2d6")]
        [TestCase(RaceConstants.BaseRaces.OgreMage, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Orc, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Pixie, "1d2-1")]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, "1d4")]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, "2d4")]
        [TestCase(RaceConstants.BaseRaces.RockGnome, "4d6")]
        [TestCase(RaceConstants.BaseRaces.Satyr, "2d6")]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, "1d4")]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, "2d4")]
        [TestCase(RaceConstants.BaseRaces.StormGiant, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, "4d6")]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, "2d4")]
        [TestCase(RaceConstants.BaseRaces.Tiefling, "1d6")]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, "1d4")]
        [TestCase(RaceConstants.BaseRaces.Troll, "1d4")]
        [TestCase(RaceConstants.BaseRaces.WildElf, "4d6")]
        [TestCase(RaceConstants.BaseRaces.WoodElf, "4d6")]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, "1d4")]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, "1d4")]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, "1d4")]
        public void IntuitiveAgeRoll(string name, string ageRoll)
        {
            DistinctCollection(name, ageRoll);
        }
    }
}
