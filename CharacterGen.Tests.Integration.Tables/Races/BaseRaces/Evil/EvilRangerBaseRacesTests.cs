using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilRangerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.HighElf)]
        [TestCase(2, 11, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(12, 28, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(29, 29, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(30, 30, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(31, 39, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(40, 72, RaceConstants.BaseRaces.Human)]
        [TestCase(73, 74, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(75, 75, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(76, 84, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(85, 85, RaceConstants.BaseRaces.DeathSlaad)]
        [TestCase(86, 87, RaceConstants.BaseRaces.YuanTiPureblood)]
        [TestCase(88, 89, RaceConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(90, 90, RaceConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(91, 91, RaceConstants.BaseRaces.Gargoyle)]
        [TestCase(92, 92, RaceConstants.BaseRaces.FrostGiant)]
        [TestCase(93, 93, RaceConstants.BaseRaces.FireGiant)]
        [TestCase(94, 94, RaceConstants.BaseRaces.CloudGiant)]
        [TestCase(95, 95, RaceConstants.BaseRaces.Troll)]
        [TestCase(96, 96, RaceConstants.BaseRaces.Troglodyte)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(98, 98, RaceConstants.BaseRaces.Ogre)]
        [TestCase(99, 100, RaceConstants.BaseRaces.Scorpionfolk)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}