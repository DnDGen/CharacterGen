﻿using System;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class EvilCharacterClassesTests : PercentileTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Percentile.GOODNESSCharacterClasses, AlignmentConstants.Evil); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(CharacterClassConstants.Barbarian, 1, 10)]
        [TestCase(CharacterClassConstants.Bard, 11, 15)]
        [TestCase(CharacterClassConstants.Cleric, 16, 35)]
        [TestCase(CharacterClassConstants.Druid, 36, 40)]
        [TestCase(CharacterClassConstants.Fighter, 41, 50)]
        [TestCase(CharacterClassConstants.Monk, 51, 55)]
        [TestCase(CharacterClassConstants.Ranger, 56, 60)]
        [TestCase(CharacterClassConstants.Rogue, 61, 80)]
        [TestCase(CharacterClassConstants.Sorcerer, 81, 85)]
        [TestCase(CharacterClassConstants.Wizard, 86, 100)]
        public override void Percentile(String content, Int32 lower, Int32 upper)
        {
            base.Percentile(content, lower, upper);
        }
    }
}