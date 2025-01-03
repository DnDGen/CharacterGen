﻿using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilSorcererMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 91, RaceConstants.Metaraces.None)]
        [TestCase(92, 92, RaceConstants.Metaraces.Ghost)]
        [TestCase(93, 93, RaceConstants.Metaraces.Vampire)]
        [TestCase(94, 95, RaceConstants.Metaraces.Lich)]
        [TestCase(96, 96, RaceConstants.Metaraces.Wererat)]
        [TestCase(97, 97, RaceConstants.Metaraces.Werewolf)]
        [TestCase(98, 98, RaceConstants.Metaraces.HalfFiend)]
        [TestCase(99, 100, RaceConstants.Metaraces.HalfDragon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}