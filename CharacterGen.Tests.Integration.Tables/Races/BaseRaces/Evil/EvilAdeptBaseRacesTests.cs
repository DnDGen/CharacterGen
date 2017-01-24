using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilAdeptBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Adept); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(3, 3, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(4, 4, RaceConstants.BaseRaces.HighElf)]
        [TestCase(5, 5, RaceConstants.BaseRaces.WildElf)]
        [TestCase(6, 8, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(9, 18, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(19, 20, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(21, 21, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(22, 22, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(23, 25, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(26, 83, RaceConstants.BaseRaces.Human)]
        [TestCase(84, 84, RaceConstants.BaseRaces.FrostGiant)]
        [TestCase(85, 85, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(86, 86, RaceConstants.BaseRaces.Goblin)]
        [TestCase(87, 87, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(88, 88, RaceConstants.BaseRaces.Kobold)]
        [TestCase(89, 89, RaceConstants.BaseRaces.Orc)]
        [TestCase(90, 90, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(91, 91, RaceConstants.BaseRaces.Drow)]
        [TestCase(92, 92, RaceConstants.BaseRaces.DuergarDwarf)]
        [TestCase(93, 93, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(94, 94, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(95, 95, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(96, 96, RaceConstants.BaseRaces.Ogre)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Minotaur)]
        [TestCase(98, 98, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(99, 99, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, RaceConstants.BaseRaces.FireGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
