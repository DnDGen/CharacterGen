using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilMonkBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Monk); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(11, 20, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(21, 90, RaceConstants.BaseRaces.Human)]
        [TestCase(91, 93, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(94, 94, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(95, 96, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(97, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}