using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Middle
{
    [TestFixture]
    public class MiddleHumanAgesTests : AdjustmentsTests
    {
        protected override String tableName
        {
            get { return String.Format(TableNameConstants.Formattable.Adjustments.AGEGROUPRACEAges, GroupConstants.Middle, RaceConstants.BaseRaces.Human); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                AdjustmentConstants.Adulthood,
                AdjustmentConstants.Quantity,
                AdjustmentConstants.Die,
                AdjustmentConstants.MiddleAge,
                AdjustmentConstants.Old,
                AdjustmentConstants.Venerable
            };

            AssertCollectionNames(names);
        }

        [TestCase(AdjustmentConstants.Adulthood, 15)]
        [TestCase(AdjustmentConstants.Quantity, 1)]
        [TestCase(AdjustmentConstants.Die, 6)]
        [TestCase(AdjustmentConstants.MiddleAge, 35)]
        [TestCase(AdjustmentConstants.Old, 53)]
        [TestCase(AdjustmentConstants.Venerable, 70)]
        public override void Adjustment(String name, Int32 adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
