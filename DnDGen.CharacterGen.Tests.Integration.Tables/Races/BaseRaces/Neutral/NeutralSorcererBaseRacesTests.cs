using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralSorcererBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(2, 2, RaceConstants.BaseRaces.HighElf)]
        [TestCase(3, 12, RaceConstants.BaseRaces.WildElf)]
        [TestCase(13, 15, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(16, 16, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(17, 31, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(32, 41, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(42, 42, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(43, 43, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(44, 48, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(49, 90, RaceConstants.BaseRaces.Human)]
        [TestCase(91, 91, RaceConstants.BaseRaces.Githzerai)]
        [TestCase(92, 92, RaceConstants.BaseRaces.GreenSlaad)]
        [TestCase(93, 94, RaceConstants.BaseRaces.GraySlaad)]
        [TestCase(95, 95, RaceConstants.BaseRaces.Satyr)]
        [TestCase(96, 96, RaceConstants.BaseRaces.Janni)]
        [TestCase(97, 98, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(99, 99, RaceConstants.BaseRaces.Doppelganger)]
        [TestCase(100, 100, RaceConstants.BaseRaces.StoneGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}