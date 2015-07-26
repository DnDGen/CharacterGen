﻿using System;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses.HasSpecialistFields
{
    [TestFixture]
    public class BardHasSpecialistFieldsTests : BooleanPercentileTests
    {
        protected override string tableName
        {
            get
            {
                return String.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, CharacterClassConstants.Bard);
            }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(false, 1, 100)]
        public override void BooleanPercentile(Boolean isTrue, Int32 lower, Int32 upper)
        {
            base.BooleanPercentile(isTrue, lower, upper);
        }
    }
}