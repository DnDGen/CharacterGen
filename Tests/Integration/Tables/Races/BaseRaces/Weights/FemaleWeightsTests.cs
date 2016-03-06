using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Weights
{
    [TestFixture]
    public class FemaleWeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Female"); }
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

        [TestCase(RaceConstants.BaseRaces.Aasimar, 90)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 250)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 100)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 25)]
        [TestCase(RaceConstants.BaseRaces.Derro, 100)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 120)]
        [TestCase(RaceConstants.BaseRaces.Drow, 80)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 100)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 35)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 150)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 25)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 80)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 80)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 110)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 80)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 100)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 145)]
        [TestCase(RaceConstants.BaseRaces.Human, 85)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 20)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 25)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 130)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 105)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 400)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 100)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 600)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 600)]
        [TestCase(RaceConstants.BaseRaces.Orc, 120)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 35)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 35)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 25)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 90)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 120)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 80)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 80)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
