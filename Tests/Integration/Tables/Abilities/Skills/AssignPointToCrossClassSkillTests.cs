using System;
using NPCGen.Tables.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills
{
    [TestFixture]
    public class AssignPointToCrossClassSkillTests : BooleanPercentileTests
    {
        protected override String tableName
        {
            get { return TableNameConstants.Set.TrueOrFalse.AssignPointToCrossClassSkill; }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(false, 1, 67)]
        [TestCase(true, 68, 100)]
        public override void BooleanPercentile(Boolean isTrue, Int32 lower, Int32 upper)
        {
            base.BooleanPercentile(isTrue, lower, upper);
        }
    }
}