using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodBardBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Bard); }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 1)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 37)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 38)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 39)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 54)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 55)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 2, 6)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 7, 11)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 12, 36)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 40, 44)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 45, 53)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 56, 57)]
        [TestCase(RaceConstants.BaseRaces.Human, 58, 97)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}