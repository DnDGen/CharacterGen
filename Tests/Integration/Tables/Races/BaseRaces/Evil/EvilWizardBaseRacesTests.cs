using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilWizardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HighElf, 1, 10)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 12, 26)]
        [TestCase(RaceConstants.BaseRaces.Human, 29, 78)]
        [TestCase(RaceConstants.BaseRaces.Hobgoblin, 79, 80)]
        [TestCase(RaceConstants.BaseRaces.Drow, 82, 91)]
        [TestCase(RaceConstants.BaseRaces.OgreMage, 95, 96)]
        [TestCase(EmptyContent, 97, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.WoodElf, 11)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 27)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 28)]
        [TestCase(RaceConstants.BaseRaces.Tiefling, 81)]
        [TestCase(RaceConstants.BaseRaces.Gnoll, 92)]
        [TestCase(RaceConstants.BaseRaces.Bugbear, 93)]
        [TestCase(RaceConstants.BaseRaces.MindFlayer, 94)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}