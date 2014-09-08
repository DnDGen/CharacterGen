using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class ConstitutionStatAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "ConstitutionStatAdjustments"; }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 2)]
        [TestCase(RaceConstants.BaseRaces.Derro, 2)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 2)]
        [TestCase(RaceConstants.BaseRaces.Drow, -2)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, -2)]
        [TestCase(RaceConstants.BaseRaces.HighElf, -2)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, -2)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 2)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 2)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 2)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 4)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 2)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, -2)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 2)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 4)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 4)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 6)]
        [TestCase(RaceConstants.BaseRaces.Orc, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 4)]
        [TestCase(RaceConstants.Metaraces.Werebear, 2)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 2)]
        [TestCase(RaceConstants.Metaraces.Wererat, 2)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 2)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 0)]
        [TestCase("", 0)]
        public void Collection(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            Collection(name, collection);
        }
    }
}