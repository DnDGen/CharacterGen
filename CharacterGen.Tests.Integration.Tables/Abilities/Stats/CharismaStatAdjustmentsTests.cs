using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class CharismaStatAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, StatConstants.Charisma); }
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
        [TestCase(RaceConstants.BaseRaces.Azer, -2)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, 0)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, -2)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 0)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 2)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, 8)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, -2)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Derro, 6)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 2)]
        [TestCase(RaceConstants.BaseRaces.Drow, 2)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, -4)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 2)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, -4)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, -2)]
        [TestCase(RaceConstants.BaseRaces.Goblin, -2)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 0)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, 4)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, 2)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, -4)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, -2)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 6)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, -2)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, -4)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, 2)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Janni, 2)]
        [TestCase(RaceConstants.BaseRaces.Kapoacinth, -4)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.Locathah, 0)]
        [TestCase(RaceConstants.BaseRaces.Merfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.Merrow, -4)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 6)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, -2)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, -2)]
        [TestCase(RaceConstants.BaseRaces.Ogre, -4)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 6)]
        [TestCase(RaceConstants.BaseRaces.Orc, -2)]
        [TestCase(RaceConstants.BaseRaces.Pixie, 6)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 6)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, -2)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Sahuagin, -2)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 2)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 4)]
        [TestCase(RaceConstants.BaseRaces.Scrag, -4)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 0)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 4)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, -4)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, -2)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 0)]
        [TestCase(RaceConstants.BaseRaces.Troll, -4)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 0)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, 8)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, 6)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, 2)]
        [TestCase(RaceConstants.Metaraces.Ghost, 4)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 2)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 2)]
        [TestCase(RaceConstants.Metaraces.Lich, 2)]
        [TestCase(RaceConstants.Metaraces.Mummy, 4)]
        [TestCase(RaceConstants.Metaraces.None, 0)]
        [TestCase(RaceConstants.Metaraces.Vampire, 4)]
        [TestCase(RaceConstants.Metaraces.Werebear, -2)]
        [TestCase(RaceConstants.Metaraces.Wereboar, -2)]
        [TestCase(RaceConstants.Metaraces.Wererat, -2)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 0)]
        [TestCase(RaceConstants.Metaraces.Werewolf, -2)]
        public void CharismaStatAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}