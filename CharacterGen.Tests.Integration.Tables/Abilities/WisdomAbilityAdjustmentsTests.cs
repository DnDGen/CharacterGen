using CharacterGen.Abilities;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Abilities
{
    [TestFixture]
    public class WisdomAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.ABILITYAbilityAdjustments, AbilityConstants.Wisdom); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);

            var names = baseRaceGroups[GroupConstants.All].Union(metaraceGroups[GroupConstants.All]).ToArray();

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 2)]
        [TestCase(RaceConstants.BaseRaces.AquaticElf, 0)]
        [TestCase(RaceConstants.BaseRaces.Azer, 2)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, -4)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 0)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 2)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 6)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, 8)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Derro, -6)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 4)]
        [TestCase(RaceConstants.BaseRaces.Drow, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, 0)]
        [TestCase(RaceConstants.BaseRaces.Githyanki, -2)]
        [TestCase(RaceConstants.BaseRaces.Githzerai, 2)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 0)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 0)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, 4)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, 0)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, -2)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 2)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, -4)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, 2)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Janni, 4)]
        [TestCase(RaceConstants.BaseRaces.Kapoacinth, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.KuoToa, 4)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.Locathah, 2)]
        [TestCase(RaceConstants.BaseRaces.Merfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.Merrow, -4)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 6)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.Ogre, -4)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 4)]
        [TestCase(RaceConstants.BaseRaces.Orc, -2)]
        [TestCase(RaceConstants.BaseRaces.Pixie, 4)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 2)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, -4)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Sahuagin, 2)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 2)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 4)]
        [TestCase(RaceConstants.BaseRaces.Scrag, -2)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 4)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 2)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 0)]
        [TestCase(RaceConstants.BaseRaces.Troll, -2)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 0)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, 10)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, 8)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, 0)]
        [TestCase(RaceConstants.Metaraces.Ghost, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 0)]
        [TestCase(RaceConstants.Metaraces.Lich, 2)]
        [TestCase(RaceConstants.Metaraces.Mummy, 4)]
        [TestCase(RaceConstants.Metaraces.None, 0)]
        [TestCase(RaceConstants.Metaraces.Vampire, 2)]
        [TestCase(RaceConstants.Metaraces.Werebear, 0)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 0)]
        [TestCase(RaceConstants.Metaraces.Wererat, 0)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 0)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 4)]
        public void WisdomAbilityAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}