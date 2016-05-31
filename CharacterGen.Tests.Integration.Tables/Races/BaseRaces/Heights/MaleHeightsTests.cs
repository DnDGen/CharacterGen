using CharacterGen.Races;
using CharacterGen.Domain.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class MaleHeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Male"); }
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

        [TestCase(RaceConstants.BaseRaces.Aasimar, 62)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 66)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 45)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 32)]
        [TestCase(RaceConstants.BaseRaces.Derro, 45)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 66)]
        [TestCase(RaceConstants.BaseRaces.Drow, 53)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 45)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 36)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 66)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 32)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 53)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 55)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 58)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 53)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 45)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 50)]
        [TestCase(RaceConstants.BaseRaces.Human, 58)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 30)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 32)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 58)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 74)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 78)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 45)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 108)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 108)]
        [TestCase(RaceConstants.BaseRaces.Orc, 61)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 36)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 36)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 48)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 62)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 53)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 53)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 53)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
