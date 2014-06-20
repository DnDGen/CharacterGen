using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class LevelAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "LevelAdjustments"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get
            {
                var baseRaces = RaceConstants.BaseRaces.GetBaseRaces();
                var metaraces = RaceConstants.Metaraces.GetMetaraces();

                return baseRaces.Union(metaraces).Union(new[] { String.Empty });
            }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 0)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 2)]
        [TestCase(RaceConstants.BaseRaces.Derro, 1)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 3)]
        [TestCase(RaceConstants.BaseRaces.Drow, 1)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 1)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 0)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 0)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 0)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 1)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 0)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 1)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 0)]
        [TestCase(RaceConstants.Metaraces.HalfCelestial, 1)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 0)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 2)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(RaceConstants.BaseRaces.Human, 0)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 0)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 0)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 8)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 4)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 2)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 8)]
        [TestCase(RaceConstants.BaseRaces.Orc, 0)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 0)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 1)]
        [TestCase(RaceConstants.Metaraces.Werebear, 2)]
        [TestCase(RaceConstants.Metaraces.Wereboar, 1)]
        [TestCase(RaceConstants.Metaraces.Wererat, 1)]
        [TestCase(RaceConstants.Metaraces.Weretiger, 1)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 1)]
        [TestCase("", 0)]
        public void Collection(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            AssertCollection(name, collection);
        }
    }
}