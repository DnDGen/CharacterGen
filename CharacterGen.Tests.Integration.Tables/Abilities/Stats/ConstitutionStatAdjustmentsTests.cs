using CharacterGen.Abilities.Stats;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class ConstitutionStatAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, StatConstants.Constitution); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);

            var names = baseRaceGroups[GroupConstants.All].Union(metaraceGroups[GroupConstants.All]);

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.Azer, 2)]
        [TestCase(RaceConstants.BaseRaces.BlueSlaad, 8)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 2)]
        [TestCase(RaceConstants.BaseRaces.Centaur, 4)]
        [TestCase(RaceConstants.BaseRaces.CloudGiant, 12)]
        [TestCase(RaceConstants.BaseRaces.DeathSlaad, 10)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Derro, 2)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 2)]
        [TestCase(RaceConstants.BaseRaces.Drow, -2)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.FireGiant, 10)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 2)]
        [TestCase(RaceConstants.BaseRaces.FrostGiant, 10)]
        [TestCase(RaceConstants.BaseRaces.Gargoyle, 8)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 2)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, -2)]
        [TestCase(RaceConstants.BaseRaces.GraySlaad, 10)]
        [TestCase(RaceConstants.BaseRaces.GreenSlaad, 8)]
        [TestCase(RaceConstants.BaseRaces.Grimlock, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.Harpy, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.HillGiant, 8)]
        [TestCase(RaceConstants.BaseRaces.HighElf, -2)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 2)]
        [TestCase(RaceConstants.BaseRaces.HoundArchon, 2)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Janni, 2)]
        [TestCase(RaceConstants.BaseRaces.Kobold, -2)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 2)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 4)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 4)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 6)]
        [TestCase(RaceConstants.BaseRaces.Orc, 0)]
        [TestCase(RaceConstants.BaseRaces.Pixie, 0)]
        [TestCase(RaceConstants.BaseRaces.Rakshasa, 6)]
        [TestCase(RaceConstants.BaseRaces.RedSlaad, 6)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 2)]
        [TestCase(RaceConstants.BaseRaces.Satyr, 2)]
        [TestCase(RaceConstants.BaseRaces.Scorpionfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.StoneGiant, 8)]
        [TestCase(RaceConstants.BaseRaces.StormGiant, 12)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 4)]
        [TestCase(RaceConstants.BaseRaces.Troll, 12)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, -2)]
        [TestCase(RaceConstants.BaseRaces.YuanTiAbomination, 6)]
        [TestCase(RaceConstants.BaseRaces.YuanTiHalfblood, 2)]
        [TestCase(RaceConstants.BaseRaces.YuanTiPureblood, 0)]
        [TestCase(RaceConstants.Metaraces.Ghost, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 2)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 2)]
        [TestCase(RaceConstants.Metaraces.Lich, 0)]
        [TestCase(RaceConstants.Metaraces.Mummy, 0)]
        [TestCase(RaceConstants.Metaraces.None, 0)]
        [TestCase(RaceConstants.Metaraces.Vampire, 0)]
        [TestCase(RaceConstants.Metaraces.Werebear, 2)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 2)]
        [TestCase(RaceConstants.Metaraces.Wererat, 2)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 2)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 0)]
        public void ConstitutionStatAdjustment(string name, int adjustment)
        {
            Adjustment(name, adjustment);
        }
    }
}