using System;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class LevelAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.Adjustments.LevelAdjustments; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                RaceConstants.BaseRaces.Aasimar, 
                RaceConstants.BaseRaces.Bugbear, 
                RaceConstants.BaseRaces.DeepDwarf, 
                RaceConstants.BaseRaces.DeepHalfling, 
                RaceConstants.BaseRaces.Derro, 
                RaceConstants.BaseRaces.Doppelganger, 
                RaceConstants.BaseRaces.Drow, 
                RaceConstants.BaseRaces.DuergarDwarf, 
                RaceConstants.BaseRaces.ForestGnome, 
                RaceConstants.BaseRaces.Gnoll, 
                RaceConstants.BaseRaces.Goblin, 
                RaceConstants.BaseRaces.GrayElf, 
                RaceConstants.BaseRaces.HalfElf, 
                RaceConstants.BaseRaces.HalfOrc, 
                RaceConstants.BaseRaces.HighElf,
                RaceConstants.BaseRaces.HillDwarf,
                RaceConstants.BaseRaces.Hobgoblin, 
                RaceConstants.BaseRaces.Human,
                RaceConstants.BaseRaces.Kobold, 
                RaceConstants.BaseRaces.LightfootHalfling, 
                RaceConstants.BaseRaces.Lizardfolk, 
                RaceConstants.BaseRaces.MindFlayer, 
                RaceConstants.BaseRaces.Minotaur, 
                RaceConstants.BaseRaces.MountainDwarf, 
                RaceConstants.BaseRaces.Ogre, 
                RaceConstants.BaseRaces.OgreMage, 
                RaceConstants.BaseRaces.Orc, 
                RaceConstants.BaseRaces.RockGnome, 
                RaceConstants.BaseRaces.Svirfneblin, 
                RaceConstants.BaseRaces.TallfellowHalfling, 
                RaceConstants.BaseRaces.Tiefling, 
                RaceConstants.BaseRaces.Troglodyte, 
                RaceConstants.BaseRaces.WildElf, 
                RaceConstants.BaseRaces.WoodElf,
                RaceConstants.Metaraces.HalfCelestial, 
                RaceConstants.Metaraces.HalfDragon, 
                RaceConstants.Metaraces.HalfFiend, 
                RaceConstants.Metaraces.None, 
                RaceConstants.Metaraces.Werebear, 
                RaceConstants.Metaraces.Wereboar,
                RaceConstants.Metaraces.Wererat, 
                RaceConstants.Metaraces.Weretiger, 
                RaceConstants.Metaraces.Werewolf
            };

            AssertCollectionNames(names);
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
        [TestCase(RaceConstants.Metaraces.None, 0)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}