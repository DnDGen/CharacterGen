using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class ForestGnomeSkillAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "ForestGnomeSkillAdjustments"; }
        }

        [TestCase(SkillConstants.Hide, 4)]
        [TestCase(SkillConstants.Listen, 2)]
        public void DistinctCollection(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            base.DistinctCollection(name, collection);
        }
    }
}