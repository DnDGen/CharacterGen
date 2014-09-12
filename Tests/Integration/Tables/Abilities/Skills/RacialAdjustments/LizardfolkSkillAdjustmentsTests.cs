using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class LizardfolkSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "LizardfolkSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Balance, 5)]
        [TestCase(SkillConstants.Jump, 6)]
        [TestCase(SkillConstants.Swim, 6)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}