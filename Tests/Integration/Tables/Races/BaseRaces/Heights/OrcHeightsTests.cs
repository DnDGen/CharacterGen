using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Heights
{
    [TestFixture]
    public class OrcHeightsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.RACEHeights, RaceConstants.BaseRaces.Orc); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                AdjustmentConstants.Base + FalseString,
                AdjustmentConstants.Base + TrueString,
                AdjustmentConstants.Quantity,
                AdjustmentConstants.Die
            };

            AssertCollectionNames(names);
        }

        [TestCase(AdjustmentConstants.Base + FalseString, 57)]
        [TestCase(AdjustmentConstants.Base + TrueString, 61)]
        [TestCase(AdjustmentConstants.Quantity, 2)]
        [TestCase(AdjustmentConstants.Die, 12)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
