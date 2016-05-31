using CharacterGen.Abilities.Stats;
using CharacterGen.Races;
using CharacterGen.Domain.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class DexterityStatAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.STATStatAdjustments, StatConstants.Dexterity); }
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
        [TestCase(RaceConstants.BaseRaces.Bugbear, 2)]
        [TestCase(RaceConstants.BaseRaces.Derro, 4)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 2)]
        [TestCase(RaceConstants.BaseRaces.Drow, 2)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 2)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 2)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 2)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 2)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 0)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 2)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 2)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 2)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 4)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 2)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 2)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 2)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 2)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 2)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 4)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 0)]
        [TestCase(RaceConstants.BaseRaces.Ogre, -2)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 0)]
        [TestCase(RaceConstants.BaseRaces.Orc, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 2)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, -2)]
        [TestCase(RaceConstants.Metaraces.Werebear, 0)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 0)]
        [TestCase(RaceConstants.Metaraces.Wererat, 2)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 0)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 0)]
        [TestCase(RaceConstants.Metaraces.None, 0)]
        [TestCase(RaceConstants.Metaraces.Ghost, 0)]
        [TestCase(RaceConstants.Metaraces.Lich, 0)]
        [TestCase(RaceConstants.Metaraces.Vampire, 4)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}