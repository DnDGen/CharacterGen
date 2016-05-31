using CharacterGen.Abilities.Stats;
using CharacterGen.Races;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class StrengthStatAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, StatConstants.Strength); }
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

        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 4)]
        [TestCase(RaceConstants.BaseRaces.Derro, 0)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 2)]
        [TestCase(RaceConstants.BaseRaces.Drow, 0)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, -2)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 2)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 4)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, -2)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, -2)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, -2)]
        [TestCase(RaceConstants.BaseRaces.Goblin, -2)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 8)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 4)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 2)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, -2)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, -2)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, -2)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, -4)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 2)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 8)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 10)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 10)]
        [TestCase(RaceConstants.BaseRaces.Orc, 4)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 0)]
        [TestCase(RaceConstants.Metaraces.Werebear, 2)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 2)]
        [TestCase(RaceConstants.Metaraces.Wererat, 0)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 2)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 2)]
        [TestCase(RaceConstants.Metaraces.None, 0)]
        [TestCase(RaceConstants.Metaraces.Ghost, 0)]
        [TestCase(RaceConstants.Metaraces.Lich, 0)]
        [TestCase(RaceConstants.Metaraces.Vampire, 6)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}