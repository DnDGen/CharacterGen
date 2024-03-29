﻿using DnDGen.CharacterGen.Tables;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Leadership
{
    [TestFixture]
    public class AttractCohortOfDifferentAlignmentTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.TrueOrFalse.AttractCohortOfDifferentAlignment;
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 50, true)]
        [TestCase(51, 100, false)]
        public override void BooleanPercentile(int lower, int upper, bool isTrue)
        {
            base.BooleanPercentile(lower, upper, isTrue);
        }
    }
}
