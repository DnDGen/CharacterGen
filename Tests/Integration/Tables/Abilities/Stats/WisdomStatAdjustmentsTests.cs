using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class WisdomStatAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, StatConstants.Wisdom); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                RaceConstants.BaseRaces.Aasimar,
                RaceConstants.BaseRaces.Bugbear,
                RaceConstants.BaseRaces.Derro,
                RaceConstants.BaseRaces.Doppelganger,
                RaceConstants.BaseRaces.Drow,
                RaceConstants.BaseRaces.DuergarDwarf,
                RaceConstants.BaseRaces.DeepDwarf,
                RaceConstants.BaseRaces.HillDwarf,
                RaceConstants.BaseRaces.MountainDwarf,
                RaceConstants.BaseRaces.GrayElf,
                RaceConstants.BaseRaces.HighElf,
                RaceConstants.BaseRaces.WildElf,
                RaceConstants.BaseRaces.WoodElf,
                RaceConstants.BaseRaces.Gnoll,
                RaceConstants.BaseRaces.ForestGnome,
                RaceConstants.BaseRaces.RockGnome,
                RaceConstants.BaseRaces.Svirfneblin,
                RaceConstants.BaseRaces.Goblin,
                RaceConstants.Metaraces.HalfCelestial,
                RaceConstants.Metaraces.HalfDragon,
                RaceConstants.BaseRaces.HalfElf,
                RaceConstants.Metaraces.HalfFiend,
                RaceConstants.BaseRaces.HalfOrc,
                RaceConstants.BaseRaces.DeepHalfling,
                RaceConstants.BaseRaces.LightfootHalfling,
                RaceConstants.BaseRaces.TallfellowHalfling,
                RaceConstants.BaseRaces.Hobgoblin,
                RaceConstants.BaseRaces.Human,
                RaceConstants.BaseRaces.Kobold,
                RaceConstants.BaseRaces.Lizardfolk,
                RaceConstants.BaseRaces.MindFlayer,
                RaceConstants.BaseRaces.Minotaur,
                RaceConstants.BaseRaces.Ogre,
                RaceConstants.BaseRaces.OgreMage,
                RaceConstants.BaseRaces.Orc,
                RaceConstants.BaseRaces.Tiefling,
                RaceConstants.BaseRaces.Troglodyte,
                RaceConstants.Metaraces.Werebear,
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Wererat,
                RaceConstants.Metaraces.Weretiger,
                RaceConstants.Metaraces.Werewolf,
                RaceConstants.Metaraces.None,
                RaceConstants.Metaraces.Ghost,
                RaceConstants.Metaraces.Lich,
                RaceConstants.Metaraces.Vampire
            };

            AssertCollectionNames(names);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 2)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 0)]
        [TestCase(RaceConstants.BaseRaces.Derro, -6)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 4)]
        [TestCase(RaceConstants.BaseRaces.Drow, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 0)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 0)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 2)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 6)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 0)]
        [TestCase(RaceConstants.BaseRaces.Ogre, -4)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 4)]
        [TestCase(RaceConstants.BaseRaces.Orc, -2)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 0)]
        [TestCase(RaceConstants.Metaraces.Werebear, 0)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 0)]
        [TestCase(RaceConstants.Metaraces.Wererat, 0)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 0)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 4)]
        [TestCase(RaceConstants.Metaraces.None, 0)]
        [TestCase(RaceConstants.Metaraces.Ghost, 0)]
        [TestCase(RaceConstants.Metaraces.Lich, 2)]
        [TestCase(RaceConstants.Metaraces.Vampire, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}