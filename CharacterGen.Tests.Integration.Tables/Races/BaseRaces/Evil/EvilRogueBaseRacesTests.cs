using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRogueBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(2, 2, RaceConstants.BaseRaces.HighElf)]
        [TestCase(3, 3, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(4, 18, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(19, 38, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(39, 39, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(40, 40, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(41, 50, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(51, 70, RaceConstants.BaseRaces.Human)]
        [TestCase(71, 85, RaceConstants.BaseRaces.Goblin)]
        [TestCase(86, 86, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(87, 87, RaceConstants.BaseRaces.Kobold)]
        [TestCase(88, 89, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(90, 93, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(94, 94, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(95, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}