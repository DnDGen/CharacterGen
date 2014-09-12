using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class ForestGnomeSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "ForestGnomeSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Hide, 4)]
        [TestCase(SkillConstants.Listen, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}