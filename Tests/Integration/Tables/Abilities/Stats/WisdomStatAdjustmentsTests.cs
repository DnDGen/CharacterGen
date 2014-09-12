using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class WisdomStatAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "WisdomStatAdjustments"; }
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
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}