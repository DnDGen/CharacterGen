using CharacterGen.Abilities;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Abilities
{
    [TestFixture]
    public class StrengthAbilityAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.ABILITYAbilityAdjustments, AbilityConstants.Strength); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);

            var names = baseRaceGroups[GroupConstants.All].Union(metaraceGroups[GroupConstants.All]).ToArray();

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.AquaticElf, 0)]
        [TestCase(RaceConstants.BaseRaces.Azer, 2)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, 12)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 4)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 8)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 24)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, 10)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, -2)]
        [TestCase(RaceConstants.BaseRaces.Derro, 0)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 2)]
        [TestCase(RaceConstants.BaseRaces.Drow, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 20)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, -2)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 18)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, 4)]
        [TestCase(RaceConstants.BaseRaces.Githyanki, 0)]
        [TestCase(RaceConstants.BaseRaces.Githzerai, 0)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 4)]
        [TestCase(RaceConstants.BaseRaces.Goblin, -2)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, -2)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, 8)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, 12)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, 4)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 2)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 0)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, 14)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, 4)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Janni, 6)]
        [TestCase(RaceConstants.BaseRaces.Kapoacinth, 4)]
        [TestCase(RaceConstants.BaseRaces.Kobold, -4)]
        [TestCase(RaceConstants.BaseRaces.KuoToa, 2)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, -2)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.Locathah, 0)]
        [TestCase(RaceConstants.BaseRaces.Merfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.Merrow, 10)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 2)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 8)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 10)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 10)]
        [TestCase(RaceConstants.BaseRaces.Orc, 4)]
        [TestCase(RaceConstants.BaseRaces.Pixie, -4)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 2)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, 10)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, -2)]
        [TestCase(RaceConstants.BaseRaces.Sahuagin, 4)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 0)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 8)]
        [TestCase(RaceConstants.BaseRaces.Scrag, 12)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 16)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 28)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, -2)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, -2)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 0)]
        [TestCase(RaceConstants.BaseRaces.Troll, 12)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 2)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, 8)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, 4)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, 0)]
        [TestCase(RaceConstants.Metaraces.Ghost, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 8)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 4)]
        [TestCase(RaceConstants.Metaraces.Lich, 0)]
        [TestCase(RaceConstants.Metaraces.Mummy, 14)]
        [TestCase(RaceConstants.Metaraces.None, 0)]
        [TestCase(RaceConstants.Metaraces.Vampire, 6)]
        [TestCase(RaceConstants.Metaraces.Werebear, 2)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 2)]
        [TestCase(RaceConstants.Metaraces.Wererat, 0)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 2)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 2)]
        public void StrengthAbilityAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}