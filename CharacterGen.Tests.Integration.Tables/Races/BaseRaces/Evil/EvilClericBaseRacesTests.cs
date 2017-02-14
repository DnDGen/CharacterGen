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
        [TestCase(26, 58, RaceConstants.BaseRaces.Human)]
        [TestCase(59, 59, RaceConstants.BaseRaces.FrostGiant)]
        [TestCase(60, 60, RaceConstants.BaseRaces.FireGiant)]
        [TestCase(61, 67, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(68, 68, RaceConstants.BaseRaces.Goblin)]
        [TestCase(69, 69, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(70, 70, RaceConstants.BaseRaces.Kobold)]
        [TestCase(71, 71, RaceConstants.BaseRaces.Orc)]
        [TestCase(72, 72, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(73, 75, RaceConstants.BaseRaces.Drow)]
        [TestCase(76, 76, RaceConstants.BaseRaces.DuergarDwarf)]
        [TestCase(77, 78, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(79, 79, RaceConstants.BaseRaces.Scorpionfolk)]
        [TestCase(80, 87, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(88, 88, RaceConstants.BaseRaces.DeathSlaad)]
        [TestCase(89, 90, RaceConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(91, 91, RaceConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(92, 92, RaceConstants.BaseRaces.Rakshasa)]
        [TestCase(93, 93, RaceConstants.BaseRaces.Harpy)]
        [TestCase(94, 95, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(96, 96, RaceConstants.BaseRaces.Ogre)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Minotaur)]
        [TestCase(98, 98, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(99, 99, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, RaceConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}