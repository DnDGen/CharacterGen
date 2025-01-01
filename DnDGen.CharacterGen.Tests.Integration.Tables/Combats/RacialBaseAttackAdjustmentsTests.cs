using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class RacialBaseAttackAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = GetTable(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = GetTable(TableNameConstants.Set.Collection.MetaraceGroups);

            var names = metaraceGroups[GroupConstants.All].Union(baseRaceGroups[GroupConstants.All]);
            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.AquaticElf, 0)]
        [TestCase(RaceConstants.BaseRaces.Azer, 2)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, 8)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 2)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 4)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 12)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, 15)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 4)]
        [TestCase(RaceConstants.BaseRaces.Derro, 3)]
        [TestCase(RaceConstants.BaseRaces.Drow, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 11)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 10)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, 4)]
        [TestCase(RaceConstants.BaseRaces.Githyanki, 0)]
        [TestCase(RaceConstants.BaseRaces.Githzerai, 0)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 1)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 0)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, 10)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, 9)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 7)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, 9)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, 6)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Janni, 6)]
        [TestCase(RaceConstants.BaseRaces.Kapoacinth, 4)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.KuoToa, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 1)]
        [TestCase(RaceConstants.BaseRaces.Locathah, 0)]
        [TestCase(RaceConstants.BaseRaces.Merfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.Merrow, 3)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 6)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 6)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 3)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 3)]
        [TestCase(RaceConstants.BaseRaces.Orc, 0)]
        [TestCase(RaceConstants.BaseRaces.Pixie, 0)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 7)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, 7)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Sahuagin, 2)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 2)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 12)]
        [TestCase(RaceConstants.BaseRaces.Scrag, 4)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 10)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 14)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 1)]
        [TestCase(RaceConstants.BaseRaces.Troll, 4)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 0)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, 9)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, 7)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, 4)]
        [TestCase(RaceConstants.Metaraces.Ghost, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 0)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 0)]
        [TestCase(RaceConstants.Metaraces.Lich, 0)]
        [TestCase(RaceConstants.Metaraces.None, 0)]
        [TestCase(RaceConstants.Metaraces.Vampire, 0)]
        [TestCase(RaceConstants.Metaraces.Werebear, 5)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 3)]
        [TestCase(RaceConstants.Metaraces.Wereboar_Dire, 5)]
        [TestCase(RaceConstants.Metaraces.Wererat, 1)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 5)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 2)]
        [TestCase(RaceConstants.Metaraces.Werewolf_Dire, 4)]
        public void RacialBaseAttackAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}