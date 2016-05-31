using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilClericBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Cleric); }
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
        [TestCase(26, 56, RaceConstants.BaseRaces.Human)]
        [TestCase(57, 63, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(64, 64, RaceConstants.BaseRaces.Goblin)]
        [TestCase(65, 65, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(66, 66, RaceConstants.BaseRaces.Kobold)]
        [TestCase(67, 67, RaceConstants.BaseRaces.Orc)]
        [TestCase(68, 68, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(69, 71, RaceConstants.BaseRaces.Drow)]
        [TestCase(72, 72, RaceConstants.BaseRaces.DuergarDwarf)]
        [TestCase(73, 74, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(75, 89, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(90, 91, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(92, 92, RaceConstants.BaseRaces.Ogre)]
        [TestCase(93, 93, RaceConstants.BaseRaces.Minotaur)]
        [TestCase(94, 94, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(95, 95, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(96, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}