using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilDruidBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(3, 3, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(4, 4, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(5, 6, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(7, 56, RaceConstants.BaseRaces.Human)]
        [TestCase(57, 71, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(72, 72, RaceConstants.BaseRaces.Goblin)]
        [TestCase(73, 73, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(74, 74, RaceConstants.BaseRaces.Kobold)]
        [TestCase(75, 75, RaceConstants.BaseRaces.Orc)]
        [TestCase(76, 99, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(100, 100, RaceConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}