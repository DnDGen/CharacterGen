using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables
{
    [TestFixture]
    public class LeadershipModifiersTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get
            {
                return TableNameConstants.Set.Adjustments.LeadershipModifiers;
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                String.Empty,
                "Cruelty",
                "Aloofness",
                "Failure",
                "Special power",
                "Fairness and generosity",
                "Great renown"
            };

            AssertCollectionNames(names);
        }

        [TestCase("", 0)]
        [TestCase("Cruelty", -2)]
        [TestCase("Aloofness", -1)]
        [TestCase("Failure", -1)]
        [TestCase("Special power", 1)]
        [TestCase("Fairness and generosity", 1)]
        [TestCase("Great renown", 2)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
