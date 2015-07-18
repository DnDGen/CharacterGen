using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodWizardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.GrayElf, 3, 7)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 8, 41)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 44, 48)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 49, 58)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 59, 63)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 65, 67)]
        [TestCase(RaceConstants.BaseRaces.Human, 69, 96)]
        [TestCase(EmptyContent, 98, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 1)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 42)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 43)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 64)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 68)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 97)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}