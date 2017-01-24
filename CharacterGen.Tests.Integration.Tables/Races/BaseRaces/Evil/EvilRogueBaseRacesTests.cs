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
        [TestCase(51, 73, RaceConstants.BaseRaces.Human)]
        [TestCase(74, 74, RaceConstants.BaseRaces.Scorpionfolk)]
        [TestCase(75, 75, RaceConstants.BaseRaces.Harpy)]
        [TestCase(76, 87, RaceConstants.BaseRaces.Goblin)]
        [TestCase(88, 88, RaceConstants.BaseRaces.Troll)]
        [TestCase(89, 89, RaceConstants.BaseRaces.Rakshasa)]
        [TestCase(90, 90, RaceConstants.BaseRaces.Grimlock)]
        [TestCase(91, 91, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(92, 92, RaceConstants.BaseRaces.Kobold)]
        [TestCase(93, 94, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(95, 98, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(99, 99, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(100, 100, RaceConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}