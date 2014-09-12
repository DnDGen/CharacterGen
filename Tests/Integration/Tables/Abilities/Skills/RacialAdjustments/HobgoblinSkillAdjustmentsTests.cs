using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class HobgoblinSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "HobgoblinSkillAdjustments"; }
        }

        [TestCase(SkillConstants.MoveSilently, 4)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}