using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class TallfellowHalflingSkillAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "TallfellowHalflingSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Listen, 2)]
        [TestCase(SkillConstants.Search, 2)]
        [TestCase(SkillConstants.Spot, 2)]
        public void Collection(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            base.Collection(name, collection);
        }
    }
}