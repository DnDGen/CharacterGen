﻿using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Alignments
{
    [TestFixture]
    public class AlignmentLawfulnessTests : PercentileTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Percentile.AlignmentLawfulness; }
        }

        [TestCase(1, 33, AlignmentConstants.Lawful)]
        [TestCase(34, 67, AlignmentConstants.Neutral)]
        [TestCase(68, 100, AlignmentConstants.Chaotic)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }
    }
}
