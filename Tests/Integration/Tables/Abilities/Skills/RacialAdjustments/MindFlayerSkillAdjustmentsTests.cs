using System;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class MindFlayerSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "MindFlayerSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Bluff, 8)]
        [TestCase(SkillConstants.Concentration, 6)]
        [TestCase(SkillConstants.Hide, 8)]
        [TestCase(SkillConstants.Intimidate, 4)]
        [TestCase(SkillConstants.Listen, 8)]
        [TestCase(SkillConstants.MoveSilently, 8)]
        [TestCase(SkillConstants.SenseMotive, 4)]
        [TestCase(SkillConstants.Spot, 8)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}