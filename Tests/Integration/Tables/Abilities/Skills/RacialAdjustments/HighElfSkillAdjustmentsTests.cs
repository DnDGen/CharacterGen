using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class HighElfSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "HighElfSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Listen, 2)]
        [TestCase(SkillConstants.Search, 2)]
        [TestCase(SkillConstants.Spot, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}