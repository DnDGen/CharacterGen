using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class FemaleHeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Female"); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 60)]
        [TestCase(RaceConstants.BaseRaces.AquaticElf, 53)]
        [TestCase(RaceConstants.BaseRaces.Azer, 43)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, 114)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 66)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 71)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 198)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, 53)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 43)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 30)]
        [TestCase(RaceConstants.BaseRaces.Derro, 43)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 66)]
        [TestCase(RaceConstants.BaseRaces.Drow, 53)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 43)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 126)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 34)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 162)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, 53)]
        [TestCase(RaceConstants.BaseRaces.Githyanki, 63)]
        [TestCase(RaceConstants.BaseRaces.Githzerai, 63)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 63)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 30)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 53)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, 53)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, 114)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, 58)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 53)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 53)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 55)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 53)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 43)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, 108)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 48)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, 70)]
        [TestCase(RaceConstants.BaseRaces.Human, 53)]
        [TestCase(RaceConstants.BaseRaces.Janni, 53)]
        [TestCase(RaceConstants.BaseRaces.Kapoacinth, 53)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 28)]
        [TestCase(RaceConstants.BaseRaces.KuoToa, 59)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 30)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 50)]
        [TestCase(RaceConstants.BaseRaces.Locathah, 50)]
        [TestCase(RaceConstants.BaseRaces.Merfolk, 57)]
        [TestCase(RaceConstants.BaseRaces.Merrow, 100)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 72)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 74)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 43)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 100)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 100)]
        [TestCase(RaceConstants.BaseRaces.Orc, 57)]
        [TestCase(RaceConstants.BaseRaces.Pixie, 14)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 53)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, 90)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 34)]
        [TestCase(RaceConstants.BaseRaces.Sahuagin, 56)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 65)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 72)]
        [TestCase(RaceConstants.BaseRaces.Scrag, 102)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 126)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 234)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 34)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 46)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 60)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 53)]
        [TestCase(RaceConstants.BaseRaces.Troll, 102)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 53)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 53)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, 96)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, 53)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, 53)]
        public void FemaleHeight(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
