using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRogueBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Rogue); }
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 2, 4)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 5, 8)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 11, 25)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 26, 53)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 54, 58)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 59, 63)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 64, 73)]
        [TestCase(RaceConstants.BaseRaces.Human, 74, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 1)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 9)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 10)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}