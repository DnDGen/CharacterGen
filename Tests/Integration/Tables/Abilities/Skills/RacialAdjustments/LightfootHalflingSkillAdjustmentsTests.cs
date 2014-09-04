using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Skills;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Abilities.Skills.RacialAdjustments
{
    [TestFixture]
    public class LightfootHalflingSkillAdjustmentsTests : CollectionTests
    {
        protected override String tableName
        {
            get { return "LightfootHalflingSkillAdjustments"; }
        }

        protected override IEnumerable<String> nameCollection
        {
            get
            {
                return new[]
                {
                    SkillConstants.Climb,
                    SkillConstants.Jump,
                    SkillConstants.Listen,
                    SkillConstants.MoveSilently
                };
            }
        }

        [TestCase(SkillConstants.Climb, 2)]
        [TestCase(SkillConstants.Jump, 2)]
        [TestCase(SkillConstants.Listen, 2)]
        [TestCase(SkillConstants.MoveSilently, 2)]
        public void Collection(String name, Int32 adjustment)
        {
            var collection = new[] { Convert.ToString(adjustment) };
            base.Collection(name, collection);
        }
    }
}