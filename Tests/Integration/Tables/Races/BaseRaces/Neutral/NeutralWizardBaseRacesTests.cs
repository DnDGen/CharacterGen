using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralWizardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Wizard); }
        }

        [TestCase(RaceConstants.BaseRaces.HighElf, 2, 26)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 27, 28)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 30, 44)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 45, 47)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 48, 49)]
        [TestCase(RaceConstants.BaseRaces.Human, 51, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.GrayElf, 1)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 29)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 50)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}