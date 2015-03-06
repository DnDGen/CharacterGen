using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodClericBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Cleric); }
        }

        [TestCase(RaceConstants.BaseRaces.Aasimar, 1)]
        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 2)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 25)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 41)]
        [TestCase(RaceConstants.BaseRaces.ForestGnome, 42)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 67)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 70)]
        [TestCase(RaceConstants.BaseRaces.Svirfneblin, 96)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 3, 22)]
        [TestCase(RaceConstants.BaseRaces.MountainDwarf, 23, 24)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 26, 35)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 36, 40)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 43, 51)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 52, 56)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 57, 66)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 68, 69)]
        [TestCase(RaceConstants.BaseRaces.Human, 71, 95)]
        [TestCase(EmptyContent, 97, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}