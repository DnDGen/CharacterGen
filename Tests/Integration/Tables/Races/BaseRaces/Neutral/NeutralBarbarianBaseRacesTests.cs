using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBarbarianBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Barbarian); }
        }

        [TestCase(RaceConstants.BaseRaces.WildElf, 3, 13)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 15, 16)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 17, 18)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 20, 58)]
        [TestCase(RaceConstants.BaseRaces.Human, 59, 87)]
        [TestCase(RaceConstants.BaseRaces.Lizardfolk, 88, 98)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 1)]
        [TestCase(RaceConstants.BaseRaces.HillDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 14)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 19)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}