using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Weights
{
    [TestFixture]
    public class DrowWeightsTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.RACEWeights, RaceConstants.BaseRaces.Drow); }
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

        [TestCase(AdjustmentConstants.Base + FalseString, 80)]
        [TestCase(AdjustmentConstants.Base + TrueString, 85)]
        [TestCase(AdjustmentConstants.Quantity, 1)]
        [TestCase(AdjustmentConstants.Die, 6)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
