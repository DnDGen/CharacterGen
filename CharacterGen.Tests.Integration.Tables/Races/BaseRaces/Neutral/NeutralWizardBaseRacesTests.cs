using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralWizardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(2, 26, RaceConstants.BaseRaces.HighElf)]
        [TestCase(27, 28, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(29, 29, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(30, 44, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(45, 47, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(48, 49, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(50, 50, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(51, 95, RaceConstants.BaseRaces.Human)]
        [TestCase(96, 96, RaceConstants.BaseRaces.GraySlaad)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Azer)]
        [TestCase(98, 98, RaceConstants.BaseRaces.Satyr)]
        [TestCase(99, 99, RaceConstants.BaseRaces.Doppelganger)]
        [TestCase(100, 100, RaceConstants.BaseRaces.Janni)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}