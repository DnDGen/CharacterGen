using System;
using NPCGen.Common.Alignments;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilBarbarianBaseRacesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Barbarian); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.WildElfId, 1)]
        [TestCase(RaceConstants.BaseRaces.HalfElfId, 4)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalflingId, 5)]
        [TestCase(RaceConstants.BaseRaces.DeepHalflingId, 6)]
        [TestCase(RaceConstants.BaseRaces.GoblinId, 45)]
        [TestCase(RaceConstants.BaseRaces.HobgoblinId, 46)]
        [TestCase(RaceConstants.BaseRaces.KoboldId, 47)]
        [TestCase(RaceConstants.BaseRaces.TieflingId, 78)]
        [TestCase(RaceConstants.BaseRaces.TroglodyteId, 84)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }

        [TestCase(RaceConstants.BaseRaces.WoodElfId, 2, 3)]
        [TestCase(RaceConstants.BaseRaces.HalfOrcId, 7, 29)]
        [TestCase(RaceConstants.BaseRaces.HumanId, 30, 39)]
        [TestCase(RaceConstants.BaseRaces.LizardfolkId, 40, 44)]
        [TestCase(RaceConstants.BaseRaces.OrcId, 48, 77)]
        [TestCase(RaceConstants.BaseRaces.GnollId, 79, 83)]
        [TestCase(RaceConstants.BaseRaces.BugbearId, 85, 86)]
        [TestCase(RaceConstants.BaseRaces.OgreId, 87, 90)]
        [TestCase(RaceConstants.BaseRaces.MinotaurId, 91, 94)]
        [TestCase(EmptyContent, 95, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}