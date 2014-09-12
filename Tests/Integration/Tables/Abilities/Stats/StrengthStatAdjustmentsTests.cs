using System;
using System.Collections.Generic;
using System.Linq;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Stats
{
    [TestFixture]
    public class StrengthStatAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "StrengthStatAdjustments"; }
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
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}