using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class BugbearSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "BugbearSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Climb, 2)]
        [TestCase(SkillConstants.Hide, 4)]
        [TestCase(SkillConstants.Listen, 2)]
        [TestCase(SkillConstants.MoveSilently, 6)]
        [TestCase(SkillConstants.Spot, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}