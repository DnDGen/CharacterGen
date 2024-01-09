using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Bard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.HighElf)]
        [TestCase(2, 2, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(3, 17, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(18, 18, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(19, 19, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(20, 20, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(21, 22, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(23, 92, RaceConstants.BaseRaces.Human)]
        [TestCase(93, 93, RaceConstants.BaseRaces.DeathSlaad)]
        [TestCase(94, 94, RaceConstants.BaseRaces.YuanTiPureblood)]
        [TestCase(95, 95, RaceConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(96, 96, RaceConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Harpy)]
        [TestCase(98, 98, RaceConstants.BaseRaces.Goblin)]
        [TestCase(99, 99, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(100, 100, RaceConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}