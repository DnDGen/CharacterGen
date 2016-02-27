using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Weights
{
    [TestFixture]
    public class TrueWeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, true); }
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

        [TestCase(RaceConstants.BaseRaces.Aasimar, 110)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 250)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 130)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 30)]
        [TestCase(RaceConstants.BaseRaces.Derro, 130)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 120)]
        [TestCase(RaceConstants.BaseRaces.Drow, 85)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 130)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 40)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 170)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 30)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 85)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 100)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 150)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 85)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 130)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 165)]
        [TestCase(RaceConstants.BaseRaces.Human, 120)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 25)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 30)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 150)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 110)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 500)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 130)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 620)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 620)]
        [TestCase(RaceConstants.BaseRaces.Orc, 160)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 40)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 40)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 30)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 110)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 120)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 85)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 85)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
