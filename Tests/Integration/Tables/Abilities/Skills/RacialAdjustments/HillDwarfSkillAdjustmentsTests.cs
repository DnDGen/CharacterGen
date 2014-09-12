using System;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class HillDwarfSkillAdjustmentsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return "HillDwarfSkillAdjustments"; }
        }

        [Test]
        public override void NoAdjustments()
        {
            base.NoAdjustments();
        }
    }
}