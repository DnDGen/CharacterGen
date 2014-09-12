using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class RockGnomeSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "RockGnomeSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Listen, 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}