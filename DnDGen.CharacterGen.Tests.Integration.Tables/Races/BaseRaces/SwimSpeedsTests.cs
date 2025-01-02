using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class SwimSpeedsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.SwimSpeeds; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = GetTable(TableNameConstants.Set.Collection.BaseRaceGroups);
            AssertCollectionNames(baseRaceGroups[GroupConstants.All]);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.AquaticElf, 40)]
        [TestCase(RaceConstants.BaseRaces.Azer, 0)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, 0)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 0)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 0)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Derro, 0)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 0)]
        [TestCase(RaceConstants.BaseRaces.Drow, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, 0)]
        [TestCase(RaceConstants.BaseRaces.Githyanki, 0)]
        [TestCase(RaceConstants.BaseRaces.Githzerai, 0)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 0)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 0)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, 0)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, 0)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 0)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, 0)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Janni, 0)]
        [TestCase(RaceConstants.BaseRaces.Kapoacinth, 60)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.KuoToa, 50)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.Locathah, 60)]
        [TestCase(RaceConstants.BaseRaces.Merfolk, 50)]
        [TestCase(RaceConstants.BaseRaces.Merrow, 40)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 0)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.Mummy, 0)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 0)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 0)]
        [TestCase(RaceConstants.BaseRaces.Orc, 0)]
        [TestCase(RaceConstants.BaseRaces.Pixie, 0)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 0)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, 0)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Sahuagin, 60)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 0)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.Scrag, 40)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 40)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 0)]
        [TestCase(RaceConstants.BaseRaces.Troll, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 0)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, 20)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, 0)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, 0)]
        public void SwimSpeed(string name, int speed)
        {
            base.Adjustment(name, speed);
        }
    }
}