using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class IntelligenceStatAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "IntelligenceStatAdjustments"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get
            {
                var baseRaces = RaceConstants.BaseRaces.GetBaseRaces();
                var metaraces = RaceConstants.Metaraces.GetMetaraces();

                return baseRaces.Union(metaraces);
            }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 0)]
        [TestCase(RaceConstants.BaseRaces.Derro, 0)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 2)]
        [TestCase(RaceConstants.BaseRaces.Drow, 2)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 2)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElf, -2)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, -2)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, -2)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 2)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 0)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 4)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, -2)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, -2)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 8)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, -4)]
        [TestCase(RaceConstants.BaseRaces.Ogre, -4)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 4)]
        [TestCase(RaceConstants.BaseRaces.Orc, -2)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 2)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, -2)]
        [TestCase(RaceConstants.Metaraces.Werebear, 0)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 0)]
        [TestCase(RaceConstants.Metaraces.Wererat, 2)]
        [TestCase(RaceConstants.Metaraces.Weretiger, -2)]
        [TestCase(RaceConstants.Metaraces.Werewolf, -2)]
        [TestCase("", 0)]
        public void Collection(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            AssertCollection(name, collection);
        }
    }
}