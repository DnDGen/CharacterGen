using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilSorcererMetaracesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.Metaraces.None, 1, 91)]
        [TestCase(RaceConstants.Metaraces.HalfDragon, 99, 100)]
        [TestCase(RaceConstants.Metaraces.Lich, 94, 95)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.Metaraces.Ghost, 92)]
        [TestCase(RaceConstants.Metaraces.Vampire, 93)]
        [TestCase(RaceConstants.Metaraces.Wererat, 96)]
        [TestCase(RaceConstants.Metaraces.Werewolf, 97)]
        [TestCase(RaceConstants.Metaraces.HalfFiend, 98)]
        public override void Percentile(String content, Int32 roll)
        {
            base.Percentile(content, roll);
        }
    }
}