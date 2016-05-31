using System;
using CharacterGen.Races;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces
{
    [TestFixture]
    public class LandSpeedsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.LandSpeeds; }
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
                RaceConstants.BaseRaces.WoodElf
            };

            AssertCollectionNames(names);
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
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}