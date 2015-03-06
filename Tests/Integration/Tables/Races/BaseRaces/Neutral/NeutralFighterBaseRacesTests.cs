using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralFighterBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Fighter); }
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 1, 10)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 11, 29)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 30, 34)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 36, 41)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 42, 46)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 49, 58)]
        [TestCase(RaceConstants.BaseRaces.Human, 59, 96)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.HighElf, 35)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 47)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 48)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 97)]
        [TestCase(RaceConstants.BaseRaces.Doppelganger, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}