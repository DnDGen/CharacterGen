using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilSorcererBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.WildElf)]
        [TestCase(2, 16, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(17, 21, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(22, 22, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(23, 23, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(24, 28, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(29, 68, RaceConstants.BaseRaces.Human)]
        [TestCase(69, 69, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(70, 70, RaceConstants.BaseRaces.Goblin)]
        [TestCase(71, 71, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(72, 86, RaceConstants.BaseRaces.Kobold)]
        [TestCase(87, 87, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(88, 90, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(91, 91, RaceConstants.BaseRaces.Bugbear)]
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