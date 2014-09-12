using System;
using NPCGen.Common.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class LandSpeedsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "LandSpeeds"; }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 30)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 30)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 20)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 20)]
        [TestCase(RaceConstants.BaseRaces.Derro, 20)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 30)]
        [TestCase(RaceConstants.BaseRaces.Drow, 30)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 20)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 20)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 30)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 30)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 30)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 30)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 30)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 30)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 20)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 30)]
        [TestCase(RaceConstants.BaseRaces.Human, 30)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 30)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 20)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 30)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 30)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 30)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 20)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 40)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 50)]
        [TestCase(RaceConstants.BaseRaces.Orc, 30)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 20)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 20)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 20)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 30)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 30)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 30)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 30)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}