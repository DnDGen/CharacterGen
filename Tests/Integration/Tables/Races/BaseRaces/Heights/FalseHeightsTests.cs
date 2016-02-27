using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class FalseHeightsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, false); }
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

        [TestCase(RaceConstants.BaseRaces.Aasimar, 60)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 66)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 43)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 30)]
        [TestCase(RaceConstants.BaseRaces.Derro, 43)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 66)]
        [TestCase(RaceConstants.BaseRaces.Drow, 53)]
        [TestCase(RaceConstants.BaseRaces.DuergarDwarf, 43)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 34)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 63)]
        [TestCase(RaceConstants.BaseRaces.Goblin, 30)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 53)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 53)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 53)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 53)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 43)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 48)]
        [TestCase(RaceConstants.BaseRaces.Human, 53)]
        [TestCase(RaceConstants.BaseRaces.Kobold, 28)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 30)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 50)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 72)]
        [TestCase(RaceConstants.BaseRaces.Minotaur, 74)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 43)]
        [TestCase(RaceConstants.BaseRaces.Ogre, 100)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 100)]
        [TestCase(RaceConstants.BaseRaces.Orc, 57)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 34)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 34)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 46)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 60)]
        [TestCase(RaceConstants.BaseRaces.Troglodyte, 53)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 53)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 53)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
