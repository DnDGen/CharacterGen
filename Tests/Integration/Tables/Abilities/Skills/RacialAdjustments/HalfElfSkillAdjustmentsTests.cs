using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class HalfElfSkillAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "HalfElfSkillAdjustments"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get { return new[] { SkillConstants.Listen, SkillConstants.Search, SkillConstants.Spot }; }
        }

        [TestCase(SkillConstants.Listen, 1)]
        [TestCase(SkillConstants.Search, 1)]
        [TestCase(SkillConstants.Spot, 1)]
        public void DistinctCollection(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            base.DistinctCollection(name, collection);
        }
    }
}